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
				BinaryFormatter formatter = new BinaryFormatter();
				currentProfile = (SyncProfile)formatter.Deserialize(str);
				str.Close();
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
			ManagementObject dsk = new ManagementObject(
                    @"win32_logicaldisk.deviceid=""" + drive + @":""");
			dsk.Get();
			string volumeSerial = dsk["VolumeSerialNumber"].ToString();
			
            return cpuID + volumeSerial;
		}

		public void addNewTask()
		{
			TaskWizardForm form = new TaskWizardForm(this);
			form.GetSelectTypePanel.Hide();
			form.GetFolderPairPanel.Show();
			form.ShowDialog();
		}

        public void analyzeFolderPair(string source, string target, string taskname)
        {
            Detector detector = new Detector(source, target);
            if (!detector.IsFolderPairSync())
            {
                try
                {
                    FolderDiffForm form = new FolderDiffForm(detector);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Reconciler.update(detector, null);
                        this.updateSyncTaskResult(taskname, "Successful");
                        this.updateSyncTaskTime(taskname, DateTime.Now.ToString());
                    }
                }
                catch
                {
                    this.updateSyncTaskResult(taskname, "Unsuccessful");
                    this.updateSyncTaskTime(taskname, DateTime.Now.ToString());
                }
            }
            else
                MessageBox.Show(source + " is in sync with " + target, 
                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

		public void syncFolderPair(string source, string target)
		{
			Detector detector = new Detector(source, target);
            if (!detector.IsFolderPairSync())
                Reconciler.update(detector, null);
		}

		public void removeTask(string name)
		{
			if (MessageBox.Show("Delete task: " + name + "?", "Confirm task deletion",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				currentProfile.removeTask(currentProfile.getTask(name));
			}
		}

		public void updateSyncTaskTime(string name, string time)
		{
			currentProfile.updateSyncTaskTime(name, time);
		}

		public void updateSyncTaskResult(string name, string result)
		{
			currentProfile.updateSyncTaskResult(name, result);
		}

        public void modifySelectedTask(string name)
        {
            TaskSetupForm form = new TaskSetupForm(currentProfile.getTask(name));
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

        public void checkAutorun()
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
    }
}
