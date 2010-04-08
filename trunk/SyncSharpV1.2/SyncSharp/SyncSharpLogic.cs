using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SyncSharp.Storage;
using SyncSharp.GUI;

namespace SyncSharp.Business
{
	public class SyncSharpLogic
	{
		private SyncProfile currentProfile;
		private string metaDataDir = "";

		public string MetaDataDir
		{
			get { return metaDataDir; }
		}

		public SyncProfile Profile
		{
			get { return currentProfile; }
		}

		public void loadProfile()
		{
			string ID = getMachineID();
			metaDataDir = @".\Profiles\" + ID;

			if (checkProfileExists(ID))
			{
				Stream str = File.OpenRead(metaDataDir + @"\" + ID);
				try
				{
					BinaryFormatter formatter = new BinaryFormatter();
					currentProfile = (SyncProfile)formatter.Deserialize(str);
				}
				catch
				{
				}
				finally
				{
					str.Close();
				}

				if (currentProfile == null)
					File.Delete(metaDataDir + @"\" + ID);
			}

			if (currentProfile == null)
				currentProfile = new SyncProfile(ID);
		}

		public void saveProfile()
		{
			if (!Directory.Exists(metaDataDir + @"\"))
				Directory.CreateDirectory(metaDataDir + @"\");

			Stream str = File.OpenWrite(metaDataDir + @"\" + currentProfile.ID);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(str, currentProfile);
			str.Close();
		}

		private bool checkProfileExists(string id)
		{
			return File.Exists(metaDataDir + @"\" + id);
		}

		private String getMachineID()
		{
			string cpuID = "";
			ManagementClass mc = new ManagementClass("win32_processor");
			ManagementObjectCollection moc = mc.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				cpuID = mo["processorID"].ToString();
				break;
			}
			String drive = "";
			foreach (DriveInfo di in DriveInfo.GetDrives())
			{
				if (di.IsReady)
				{
					drive = di.RootDirectory.ToString();
					break;
				}
			}
			drive = drive.Substring(0, 1);
			ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
			dsk.Get();
			string volumeSerial = dsk["VolumeSerialNumber"].ToString();
			return cpuID + volumeSerial;
		}

		public void addNewTask()
		{
			CreateTaskForm form = new CreateTaskForm(this);
			form.ShowDialog();
		}

		public void analyzeFolderPair(SyncTask curTask, ToolStripStatusLabel status, bool isPlugSync)
		{
			status.Text = "Analyzing " + curTask.Name + "...";
			Detector detector = new Detector(metaDataDir, curTask);
			detector.CompareFolders();
			if (!detector.IsSynchronized())
			{
				try
				{
					Reconciler reconciler = new Reconciler(detector.SourceList, detector.TargetList, curTask, metaDataDir);
                    reconciler.Preview();
                    FolderDiffForm form = new FolderDiffForm(reconciler._previewFilesList, reconciler._previewFoldersList, curTask);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        if (!checkSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
                            !checkSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
                        {
                            throw new Exception("Insufficient disk space");
                        }
                        status.Text = "Synchronizing " + curTask.Name + "...";
                        Logger.SyncSetWriteLog(metaDataDir, curTask.Name, true);
                        reconciler.SyncPreview();
                        SyncMetaData.WriteMetaData(metaDataDir + @"\" + curTask.Name + "src.meta", reconciler._updatedList);
                        SyncMetaData.WriteMetaData(metaDataDir + @"\" + curTask.Name + "tgt.meta", reconciler._updatedList);
                        this.updateSyncTaskResult(curTask, "Successful");
                    }
                    else
                        this.updateSyncTaskResult(curTask, "Aborted");

				}
				catch (Exception e)
				{
					//Logger.CreateLog(metaDataDir + @"\" + curTask.Name + ".log");
					//Logger.WriteEntry(e.Message);
					Logger.WriteErrorLog(e.Message);
					this.updateSyncTaskResult(curTask, "Unsuccessful");
				}
				finally
				{
					Logger.SyncSetWriteLog(metaDataDir, curTask.Name, false);
				}
			}
			else
			{
				MessageBox.Show("Task: " + curTask.Name + "\n\n" +
				"There are no differences between the source and target folders contents.",
				"SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.updateSyncTaskResult(curTask, "Successful");
			}
			this.updateSyncTaskTime(curTask, DateTime.Now.ToString());
		}

		public void syncFolderPair(SyncTask curTask, ToolStripStatusLabel status, bool isPlugSync)
		{
			try
			{
				Logger.SyncSetWriteLog(metaDataDir, curTask.Name, true);
				status.Text = "Analyzing " + curTask.Name + "...";
				if (curTask.TypeOfSync)
				{
					Detector detector = new Detector(metaDataDir, curTask);
					detector.CompareFolders();

					if (!detector.IsSynchronized())
					{
						if (!checkSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
							!checkSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
						{
							throw new Exception("Insufficient disk space");
						}
						if (checkForOpenFiles(detector.OpenFileDetected, isPlugSync))
							throw new Exception("Open files detected");
						Reconciler reconciler = new Reconciler(detector.SourceList, detector.TargetList, curTask, metaDataDir);
						status.Text = "Synchronizing " + curTask.Name + "...";
						reconciler.Sync();
						SyncMetaData.WriteMetaData(metaDataDir + @"\" + curTask.Name + "src.meta", reconciler._updatedList);
						SyncMetaData.WriteMetaData(metaDataDir + @"\" + curTask.Name + "tgt.meta", reconciler._updatedList);
					}
				}
				else
				{
					Detector detector = new Detector(metaDataDir, curTask);
					detector.CompareFolders();
					if (!checkSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
								!checkSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
					{
						throw new Exception("Insufficient disk space");
					}
					if (checkForOpenFiles(detector.OpenFileDetected, isPlugSync))
						throw new Exception("Open files detected");
					Reconciler reconciler = new Reconciler(detector.SourceList, detector.TargetList, curTask, metaDataDir);
					status.Text = "Synchronizing " + curTask.Name + "...";
					reconciler.BackupSource(detector.BackupFiles);
				}

				this.updateSyncTaskResult(curTask, "Successful");
			}
			catch (Exception e)
			{
				//Logger.CreateLog(metaDataDir + @"\" + curTask.Name + ".log");
				//Logger.WriteEntry("Error: " + e.Message);
				Logger.WriteErrorLog(e.Message);
				this.updateSyncTaskResult(curTask, "Unsuccessful");
			}
			finally
			{
				//Logger.CloseLog();
				Logger.SyncSetWriteLog(metaDataDir, curTask.Name, false);
			}

			this.updateSyncTaskTime(curTask, DateTime.Now.ToString());
		}

		public void syncAllFolderPairs(ToolStripStatusLabel status, bool isPlugSync)
		{
			foreach (SyncTask task in currentProfile.TaskCollection)
			{
				try
				{
					Logger.SyncSetWriteLog(metaDataDir, task.Name, true);
					if (Directory.Exists(task.Source) && Directory.Exists(task.Target))
						syncFolderPair(task, status, true);
					else
						throw new Exception("Source or target folder does not exist");
				}
				catch (Exception e)
				{
					//Logger.CreateLog(metaDataDir + @"\" + task.Name + ".log");
					//Logger.WriteEntry(e.Message);
					Logger.WriteErrorLog(e.Message);
					this.updateSyncTaskResult(task, "Unsuccessful");
					this.updateSyncTaskTime(task, DateTime.Now.ToString());
				}
				finally
				{
					//Logger.CloseLog();
					Logger.SyncSetWriteLog(metaDataDir, task.Name, false);
				}
			}
		}

		public void removeTask(string name, string metaDataDir)
		{
			currentProfile.RemoveTask(currentProfile.GetTask(name), metaDataDir);
		}

		public void updateSyncTaskTime(SyncTask task, string time)
		{
			currentProfile.UpdateSyncTaskTime(task, time);
		}

		public void updateSyncTaskResult(SyncTask task, string result)
		{
			currentProfile.UpdateSyncTaskResult(task, result);
		}

		public void modifySelectedTask(string name)
		{
			TaskSetupForm form = new TaskSetupForm(currentProfile, currentProfile.GetTask(name), metaDataDir);
			form.ShowDialog();
		}

		public void renameSelectedTask(string name, string metaDataDir)
		{
			RenameTaskForm form = new RenameTaskForm(currentProfile, currentProfile.GetTask(name), metaDataDir);
			form.ShowDialog();
		}

		public void updateRemovableRoot()
		{
			String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
			root = root.Substring(0, 1);
			currentProfile.UpdateRemovableRoot(root);
		}

		public void copySelectedTask(string name)
		{
			currentProfile.CopyTask(name);
		}

		public void checkAutorun(bool autoPlayEnabled)
		{
			if (autoPlayEnabled)
			{
				bool needAction = true;
				bool needOpen = true;
				if (File.Exists(@".\Autorun.inf"))
				{
					StreamReader sr = new StreamReader(@".\Autorun.inf");
					String line = sr.ReadLine();
					while (line != null)
					{
						if (line.Contains("OPEN=SyncSharp.exe"))
						{
							needOpen = false;
						}
						if (line.Contains("Action=Run SyncSharp"))
						{
							needAction = false;
						}
						line = sr.ReadLine();
					}
					sr.Close();
					if (needAction || needOpen)
					{
						File.Move(@".\Autorun.inf", @".\Autorun.inf.backup");
						StreamWriter sw = new StreamWriter(@".\Autorun.inf");
						sw.WriteLine("[AutoRun]");
						sw.WriteLine("OPEN=SyncSharp.exe");
						sw.WriteLine("Action=Run SyncSharp");
						sw.Close();
					}
				}
				else
				{
					StreamWriter sw = File.CreateText(@".\Autorun.inf");
					sw.WriteLine("[AutoRun]");
					sw.WriteLine("OPEN=SyncSharp.exe");
					sw.WriteLine("Action=Run SyncSharp");
					sw.Close();
				}
			}
			else
			{
				if (File.Exists(@".\Autorun.inf"))
				{
					bool mustRemove = false;
					StreamReader sr = new StreamReader(@".\Autorun.inf");
					String line = sr.ReadLine();
					while (line != null)
					{
						if (line.Contains("OPEN=SyncSharp.exe"))
						{
							mustRemove = true;
						}
						if (line.Contains("Action=Run SyncSharp"))
						{
							mustRemove = true;
						}
						line = sr.ReadLine();
					}
					sr.Close();
					if (mustRemove)
					{
						File.Delete(@".\Autorun.inf");
						if (File.Exists(@".\Autorun.inf.backup"))
						{
							File.Move(@".\Autorun.inf.backup", @".\Autorun.inf");
						}
					}
				}
			}
		}

		public void importProfile(string fileName)
		{
			Stream str = File.OpenRead(fileName);
			try
			{
				SyncProfile importedProfile;
				BinaryFormatter formatter = new BinaryFormatter();
				importedProfile = (SyncProfile)formatter.Deserialize(str);

				if (!importedProfile.ID.Equals(currentProfile.ID))
				{
					MessageBox.Show("The selected profile was not created on this computer. " +
													 "\nOnly SyncTask's with valid source/target paths will be imported",
													 "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				foreach (SyncTask item in importedProfile.TaskCollection)
				{
					if (!Directory.Exists(item.Source) || !Directory.Exists(item.Target) ||
							 currentProfile.IsFolderPairExists(item.Source, item.Target))
						continue;

					if (!currentProfile.TaskExists(item.Name))
						currentProfile.AddTask(item);
					else
					{
						item.Name = item.Name + " - Imported(1)";
						for (int i = 1; true; i++)
						{
							item.Name = item.Name.Substring(0, item.Name.LastIndexOf("(") + 1);
							item.Name = item.Name + i + ")";
							if (!currentProfile.TaskExists(item.Name))
							{
								currentProfile.AddTask(item);
								break;
							}
						}
					}
				}
			}
			catch
			{
				MessageBox.Show("Selected SyncProfile is not valid", "SyncSharp",
												MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				str.Close();
			}
		}

		private bool checkSufficientDiskSpace(string drive, long dirtySize, bool isPlugSync)
		{
			ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
			disk.Get();
			DriveInfo d = new DriveInfo(drive);
			if (!(d.AvailableFreeSpace >= dirtySize) && !isPlugSync)
			{
				string display = drive + ":\\ drive does not have enough disk space for synchronization\n";
				display += "Estimated required disk space: " + Utility.FormatSize(dirtySize) + "\n";
				display += "Disk space currently available: " + Utility.FormatSize(d.AvailableFreeSpace) + "\n";
				MessageBox.Show(display, "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return (d.AvailableFreeSpace >= dirtySize);
		}

		private bool checkForOpenFiles(bool openFileDetected, bool isPlugSync)
		{
			if (openFileDetected && !isPlugSync)
			{
				string display =
					"Synchronization can't be completed because one or more files are current open in another program" +
					"\nClose all files and try again";
				MessageBox.Show(display, "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return openFileDetected;
		}

		public void exportProfile(string fileName)
		{
			Stream str = File.OpenWrite(fileName);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(str, currentProfile);
			str.Close();
		}

		public void modifyGlobalSettings(SyncProfile currentProfile)
		{
			GlobalSettings form = new GlobalSettings(currentProfile);
			form.ShowDialog();
		}
	}
}