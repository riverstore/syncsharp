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

namespace SyncSharp.Business
{
	public class SyncSharpLogic
	{
		private SyncProfile currentProfile;

		public SyncProfile Profile
		{
			get { return currentProfile; }
		}

		public void loadProfile()
		{
			String ID = getMachineID();
			if (checkProfileExists(ID))
			{
				Stream str = File.OpenRead(@".\Profiles\" + ID);
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
			String ID = getMachineID();
			if (!Directory.Exists(@".\Profiles\"))
				Directory.CreateDirectory(@".\Profiles\");
			Stream str = File.OpenWrite(@".\Profiles\" + ID);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(str, currentProfile);
			str.Close();
		}

		private bool checkProfileExists(String id)
		{
			return File.Exists(@".\Profiles\" + id);
		}

		private String getMachineID()
		{
			string cpuInfo = "";
			ManagementClass mc = new ManagementClass("win32_processor");
			ManagementObjectCollection moc = mc.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				if (cpuInfo == "")
				{
					cpuInfo = mo.Properties["processorID"].Value.ToString();
					break;
				}
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
			String uniqueID = cpuInfo + volumeSerial;
			//MessageBox.Show(uniqueID);
			return cpuInfo;
		}

		public void addNewTask()
		{
			TaskWizardForm form = new TaskWizardForm(this);
			form.GetSelectTypePanel.Hide();
			form.GetFolderPairPanel.Show();
			form.ShowDialog();
		}

        internal void syncFolderPair(string source, string target)
        {
            Detector detect = new Detector();
            SyncMetaData meta = new SyncMetaData();
            detect.CompareFolderPair(source, target, meta.ReadMetaData(source), meta.ReadMetaData(target));
            Reconciler.update(detect, null);
            meta.getContent(source);
            meta.getContent(target);
        }
    }
}
