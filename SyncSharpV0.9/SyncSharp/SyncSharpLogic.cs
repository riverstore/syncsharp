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
		private String ID = "";
		private SyncProfile currentProfile;
		public SyncProfile Profile
		{
			get { return currentProfile; }
		}
		public void loadProfile()
		{
			ID = getMachineID();
			if (checkProfileExists(ID))
			{
				Stream str = File.OpenRead(@".\Profiles\" + ID + @"\" + ID);
				try
				{
					BinaryFormatter formatter = new BinaryFormatter();
					currentProfile = (SyncProfile)formatter.Deserialize(str);
					str.Close();
				}
				catch
				{
					str.Close();
					File.Delete(@".\Profiles\" + ID + @"\" + ID);
					currentProfile = new SyncProfile(ID);
				}
			}
			else
			{
				currentProfile = new SyncProfile(ID);
			}
		}

		public void saveProfile()
		{
			if (!Directory.Exists(@".\Profiles\" + ID + @"\"))
				Directory.CreateDirectory(@".\Profiles\" + ID + @"\");

			Stream str = File.OpenWrite(@".\Profiles\" + ID + @"\" + ID);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(str, currentProfile);
			str.Close();
		}

		private bool checkProfileExists(String id)
		{
			return File.Exists(@".\Profiles\" + ID + @"\" + id);
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
			TaskWizardForm form = new TaskWizardForm(this);
			form.ShowDialog();
		}

		public void analyzeFolderPair(SyncTask curTask, ToolStripStatusLabel status, bool isPlugSync)
		{
			status.Text = "Analyzing " + curTask.Name + "...";
			Detector detector = new Detector(ID, curTask);
			detector.compareFolders();
			if (!detector.isSynchronized())
			{
				try
				{
					Reconciler reconciler = new Reconciler(detector.sourceList, detector.destList, curTask, ID);
					FolderDiffLists previewLists = reconciler.PreviewWithMetaData();
					FolderDiffForm form = new FolderDiffForm(previewLists, curTask);
					DialogResult result = form.ShowDialog();
					if (result == DialogResult.OK)
					{
						if (!checkSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
							!checkSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
						{
							throw new Exception("Insufficient disk space");
						}
						status.Text = "Synchronizing " + curTask.Name + "...";
						reconciler.SyncPreviewAction(previewLists);
						SyncMetaData.WriteMetaData(@".\Profiles\" + ID + @"\" + curTask.Name + "src.meta", reconciler._updatedList);
						SyncMetaData.WriteMetaData(@".\Profiles\" + ID + @"\" + curTask.Name + "tgt.meta", reconciler._updatedList);
						this.updateSyncTaskResult(curTask, "Successful");
					}
					else
					{
						SyncMetaData.WriteMetaData(@".\Profiles\" + ID + @"\" + curTask.Name + "src.meta", reconciler._updatedList);
						SyncMetaData.WriteMetaData(@".\Profiles\" + ID + @"\" + curTask.Name + "tgt.meta", reconciler._updatedList);
						this.updateSyncTaskResult(curTask, "Aborted");
					}

				}
				catch
				{
					this.updateSyncTaskResult(curTask, "Unsuccessful");
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
				status.Text = "Analyzing " + curTask.Name + "...";
				if (curTask.TypeOfSync)
				{
					Detector detector = new Detector(ID, curTask);
					detector.compareFolders();

					if (!detector.isSynchronized())
					{
						if (!checkSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
							!checkSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
						{
							throw new Exception("Insufficient disk space");
						}
						if (checkForOpenFiles(detector.OpenFileDetected, isPlugSync))
							throw new Exception("Open files detected");
						Reconciler reconciler = new Reconciler(detector.sourceList, detector.destList, curTask, ID);
						status.Text = "Synchronizing " + curTask.Name + "...";
						reconciler.SyncWithMeta();
						SyncMetaData.WriteMetaData(@".\Profiles\" + ID + @"\" + curTask.Name + "src.meta", reconciler._updatedList);
						SyncMetaData.WriteMetaData(@".\Profiles\" + ID + @"\" + curTask.Name + "tgt.meta", reconciler._updatedList);
					}
				}
				else
				{
					Detector detector = new Detector(ID, curTask);
					detector.compareFolders();
					if (!checkSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
								!checkSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
					{
						throw new Exception("Insufficient disk space");
					}

					Reconciler reconciler = new Reconciler(detector.sourceList, detector.destList, curTask, ID);
					status.Text = "Synchronizing " + curTask.Name + "...";
					reconciler.BackupSource(detector.sDirtyFiles);
				}

				this.updateSyncTaskResult(curTask, "Successful");
			}
			catch (Exception e)
			{

				Logger.WriteEntry("Error: " + e.Message);
				Logger.CloseLog();
				this.updateSyncTaskResult(curTask, "Unsuccessful");
			}

			this.updateSyncTaskTime(curTask, DateTime.Now.ToString());
		}

		public void syncAllFolderPairs(ToolStripStatusLabel status, bool isPlugSync)
		{
			foreach (SyncTask task in currentProfile.TaskCollection)
			{
				try
				{
					if (Directory.Exists(task.Source) && Directory.Exists(task.Target))
						syncFolderPair(task, status, true);
					else
						throw new Exception("Source or target folder does not exist");
				}
				catch (Exception)
				{
					this.updateSyncTaskResult(task, "Unsuccessful");
					this.updateSyncTaskTime(task, DateTime.Now.ToString());
				}
			}
		}

		public void removeTask(string name)
		{
			currentProfile.removeTask(currentProfile.getTask(name));
		}

		public void updateSyncTaskTime(SyncTask task, string time)
		{
			currentProfile.updateSyncTaskTime(task, time);
		}

		public void updateSyncTaskResult(SyncTask task, string result)
		{
			currentProfile.updateSyncTaskResult(task, result);
		}

		public void modifySelectedTask(string name)
		{
			TaskSetupForm form = new TaskSetupForm(currentProfile, currentProfile.getTask(name));
			form.ShowDialog();
		}

		public void renameSelectedTask(string name)
		{
			RenameTaskForm form = new RenameTaskForm(currentProfile, currentProfile.getTask(name));
			form.ShowDialog();
		}

		public void updateRemovableRoot()
		{
			String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
			root = root.Substring(0, 1);
			currentProfile.updateRemovableRoot(root);
		}

		public void copySelectedTask(string name)
		{
			currentProfile.copyTask(name);
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
					else
					{
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
					else
					{
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
				str.Close();
				if (!importedProfile.ID.Equals(currentProfile.ID))
				{
					MessageBox.Show("The selected profile was not created on this computer. " +
									 "\nOnly SyncTask's with valid source/target paths will be imported",
									 "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				foreach (SyncTask item in importedProfile.TaskCollection)
				{
					if (!currentProfile.taskExists(item.Name))
					{
						if (Directory.Exists(item.Source) && Directory.Exists(item.Target))
							currentProfile.addTask(item);
					}
					else
					{
						if (Directory.Exists(item.Source) && Directory.Exists(item.Target))
						{
							item.Name = item.Name + " - Imported";
							currentProfile.addTask(item);
						}
					}
				}
			}
			catch
			{
				str.Close();
				MessageBox.Show("Selected SyncProfile is not valid", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				display += "Estimated required disk space: " + FormatSize(dirtySize) + "\n";
				display += "Disk space currently available: " + FormatSize(d.AvailableFreeSpace) + "\n";
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

		public String FormatSize(long size)
		{
			string sizeString = "";
			if (size < 1024)
				sizeString = String.Format("{0:0.00 B}", size);
			else if (size < 1048576)
				sizeString = String.Format("{0:0.00 KB}", size / 1024.0);
			else if (size < 1073741824)
				sizeString = String.Format("{0:0.00 MB}", size / 1048576.0);
			else
				sizeString = String.Format("{0:0.00 GB}", size / 1073741824.0);
			return sizeString;
		}
	}
}