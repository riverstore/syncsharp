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
		private SyncProfile _currentProfile;
		private string _metaDataDir = "";
		private Reconciler _previewReconciler;

		public string MetaDataDir
		{
			get { return _metaDataDir; }
		}

		public SyncProfile Profile
		{
			get { return _currentProfile; }
		}

		public void loadProfile()
		{
			string ID = GetMachineID();
			_metaDataDir = @".\Profiles\" + ID;

			if (CheckProfileExists(ID))
			{
				Stream str = File.OpenRead(_metaDataDir + @"\" + ID);
				try
				{
					BinaryFormatter formatter = new BinaryFormatter();
					_currentProfile = (SyncProfile)formatter.Deserialize(str);
				}
				catch
				{
				}
				finally
				{
					str.Close();
				}

				if (_currentProfile == null)
					File.Delete(_metaDataDir + @"\" + ID);
			}
			if (_currentProfile == null)
				_currentProfile = new SyncProfile(ID);
		}

		public void SaveProfile()
		{
			if (!Directory.Exists(_metaDataDir + @"\"))
				Directory.CreateDirectory(_metaDataDir + @"\");

			Stream str = File.OpenWrite(_metaDataDir + @"\" + _currentProfile.ID);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(str, _currentProfile);
			str.Close();
		}

		private bool CheckProfileExists(string id)
		{
			return File.Exists(_metaDataDir + @"\" + id);
		}

		private String GetMachineID()
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

		public void AddNewTask()
		{
			CreateTaskForm form = new CreateTaskForm(this);
			form.ShowDialog();
		}

		public DialogResult AnalyzeFolderPair(SyncTask curTask, ToolStripStatusLabel status)
		{
			DialogResult result = DialogResult.None;
			try
			{
				status.Text = "Analyzing " + curTask.Name + "...";
				Detector detector = new Detector(_metaDataDir, curTask);
				detector.CompareFolders();
				if (!detector.IsSynchronized())
				{
					if (!CheckSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, false) ||
							!CheckSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, false))
					{
						throw new Exception("Insufficient disk space");
					}
					_previewReconciler = new Reconciler(detector.SourceList, detector.TargetList, curTask, _metaDataDir);
					_previewReconciler.Preview();
					FolderDiffForm form = new FolderDiffForm(_previewReconciler.PreviewFilesList,
																									_previewReconciler.PreviewFoldersList, curTask);
					result = form.ShowDialog();
				}
				else
				{
					MessageBox.Show("Task: " + curTask.Name + "\n\n" +
					"There are no differences between the source and target folders contents.",
					"SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.UpdateSyncTaskResult(curTask, "Successful");
				}
			}
			catch (Exception e)
			{
				Logger.LogFileLocation = _metaDataDir + @"\";
				Logger.LogFileName = Logger.LogFileLocation + curTask.Name + ".log";
				Logger.WriteErrorLog(e.Message);
				this.UpdateSyncTaskResult(curTask, "Unsuccessful");
			}

			this.UpdateSyncTaskTime(curTask, DateTime.Now.ToString());
			return result;
		}

		public void SyncAfterAnalyze(SyncTask curTask, ToolStripStatusLabel status)
		{
			try
			{
				Logger.WriteSyncLog(_metaDataDir, curTask.Name, true);
				status.Text = "Synchronizing " + curTask.Name + "...";
				_previewReconciler.SyncPreview();

				SyncMetaData.WriteMetaData(_metaDataDir + @"\" + curTask.Name + ".meta", _previewReconciler.UpdatedList);

				this.UpdateSyncTaskResult(curTask, "Successful");
			}
			catch (Exception e)
			{
				Logger.WriteErrorLog(e.Message);
				this.UpdateSyncTaskResult(curTask, "Unsuccessful");
			}
			finally
			{
				Logger.WriteSyncLog(_metaDataDir, curTask.Name, false);
			}

			this.UpdateSyncTaskTime(curTask, DateTime.Now.ToString());
		}

		public void SyncFolderPair(SyncTask curTask, ToolStripStatusLabel status, bool isPlugSync)
		{
			try
			{
				Logger.WriteSyncLog(_metaDataDir, curTask.Name, true);
				status.Text = "Analyzing " + curTask.Name + "...";

				Detector detector = new Detector(_metaDataDir, curTask);
				detector.CompareFolders();

				if (!detector.IsSynchronized())
				{
					if (!CheckSufficientDiskSpace(curTask.Source.Substring(0, 1), detector.TgtDirtySize, isPlugSync) ||
							!CheckSufficientDiskSpace(curTask.Target.Substring(0, 1), detector.SrcDirtySize, isPlugSync))
					{
						throw new Exception("Insufficient disk space");
					}

					Reconciler reconciler = new Reconciler(detector.SourceList, detector.TargetList, curTask, _metaDataDir);
					status.Text = "Synchronizing " + curTask.Name + "...";
					if (curTask.TypeOfSync)
					{
						reconciler.Sync();
						SyncMetaData.WriteMetaData(_metaDataDir + @"\" + curTask.Name + ".meta", reconciler.UpdatedList);
					}
					else
					{
						reconciler.BackupSource(detector.BackupFiles);
						SyncMetaData.WriteMetaData(_metaDataDir + @"\" + curTask.Name + ".bkp", detector.BackupFiles);
					}
				}

				this.UpdateSyncTaskResult(curTask, "Successful");
			}
			catch (Exception e)
			{
				Logger.WriteErrorLog(e.Message);
				this.UpdateSyncTaskResult(curTask, "Unsuccessful");
			}
			finally
			{
				Logger.WriteSyncLog(_metaDataDir, curTask.Name, false);
			}

			this.UpdateSyncTaskTime(curTask, DateTime.Now.ToString());
		}

		public void RestoreSource(SyncTask curTask, ToolStripStatusLabel status)
		{
			try
			{
				Logger.LogFileLocation = _metaDataDir + @"\";
				Logger.LogFileName = Logger.LogFileLocation + curTask.Name + ".log";
				Reconciler reconciler = new Reconciler(null, null, curTask, _metaDataDir);
				reconciler.RestoreSource(SyncMetaData.ReadMetaData(_metaDataDir + @"\" + curTask.Name + ".bkp"));
				this.UpdateSyncTaskResult(curTask, "Successful");
			}
			catch (Exception e)
			{
				Logger.WriteErrorLog(e.Message);
				this.UpdateSyncTaskResult(curTask, "Unsuccessful");
			}
		}

		public void SyncAllFolderPairs(ToolStripStatusLabel status, bool isPlugSync)
		{
			foreach (SyncTask task in _currentProfile.TaskCollection)
			{
				try
				{
					if (Directory.Exists(task.Source) && Directory.Exists(task.Target))
						SyncFolderPair(task, status, true);
					else
						throw new Exception("Source or target folder does not exist");
				}
				catch (Exception e)
				{
					Logger.WriteErrorLog(e.Message);
					this.UpdateSyncTaskResult(task, "Unsuccessful");
					this.UpdateSyncTaskTime(task, DateTime.Now.ToString());
				}
				finally
				{
					Logger.WriteSyncLog(_metaDataDir, task.Name, false);
				}
			}
		}

		public void RemoveTask(string name, string metaDataDir)
		{
			_currentProfile.RemoveTask(_currentProfile.GetTask(name), metaDataDir);
		}

		public void UpdateSyncTaskTime(SyncTask task, string time)
		{
			_currentProfile.UpdateSyncTaskTime(task, time);
		}

		public void UpdateSyncTaskResult(SyncTask task, string result)
		{
			_currentProfile.UpdateSyncTaskResult(task, result);
		}

		public void ModifySelectedTask(string name)
		{
			TaskSetupForm form = new TaskSetupForm(_currentProfile, _currentProfile.GetTask(name), _metaDataDir);
			form.ShowDialog();
		}

		public void RenameSelectedTask(string name, string metaDataDir)
		{
			RenameTaskForm form = new RenameTaskForm(_currentProfile, _currentProfile.GetTask(name), metaDataDir);
			form.ShowDialog();
		}

		public void UpdateRemovableRoot()
		{
			String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
			root = root.Substring(0, 1);
			_currentProfile.UpdateRemovableRoot(root);
		}

		public void CheckAutorun(bool autoPlayEnabled)
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
							needOpen = false;

						if (line.Contains("Action=Run SyncSharp"))
							needAction = false;

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
							mustRemove = true;

						if (line.Contains("Action=Run SyncSharp"))
							mustRemove = true;

						line = sr.ReadLine();
					}
					sr.Close();
					if (mustRemove)
					{
						File.Delete(@".\Autorun.inf");
						if (File.Exists(@".\Autorun.inf.backup"))
							File.Move(@".\Autorun.inf.backup", @".\Autorun.inf");
					}
				}
			}
		}

		public void ImportProfile(string fileName)
		{
			Stream str = File.OpenRead(fileName);
			try
			{
				SyncProfile importedProfile;
				BinaryFormatter formatter = new BinaryFormatter();
				importedProfile = (SyncProfile)formatter.Deserialize(str);

				if (!importedProfile.ID.Equals(_currentProfile.ID))
				{
					MessageBox.Show("The selected profile was not created on this computer. " +
													 "\nOnly SyncTask's with valid source/target paths will be imported",
													 "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				foreach (SyncTask item in importedProfile.TaskCollection)
				{
					if (!Directory.Exists(item.Source) || !Directory.Exists(item.Target) ||
							 _currentProfile.IsFolderPairExists(item.Source, item.Target))
						continue;

					if (!_currentProfile.TaskExists(item.Name))
						_currentProfile.AddTask(item);
					else
					{
						item.Name = item.Name + " - Imported(1)";
						for (int i = 1; true; i++)
						{
							item.Name = item.Name.Substring(0, item.Name.LastIndexOf("(") + 1);
							item.Name = item.Name + i + ")";
							if (!_currentProfile.TaskExists(item.Name))
							{
								_currentProfile.AddTask(item);
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

		private bool CheckSufficientDiskSpace(string drive, long dirtySize, bool isPlugSync)
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

		public void ExportProfile(string fileName)
		{
			Stream str = File.OpenWrite(fileName);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(str, _currentProfile);
			str.Close();
		}

		public void ModifyGlobalSettings(SyncProfile currentProfile)
		{
			GlobalSettings form = new GlobalSettings(currentProfile);
			form.ShowDialog();
		}
	}
}