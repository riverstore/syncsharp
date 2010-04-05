using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SyncSharp.Storage;
using SyncSharp.Business;
using System.Text.RegularExpressions;

namespace SyncSharp.GUI
{
	public partial class RenameTaskForm : Form
	{
		SyncProfile currentProfile;
		SyncTask currentTask;
		string metaDataDir;

		public RenameTaskForm(SyncProfile profile, SyncTask task, string metaDataDir)
		{
			InitializeComponent();

			currentProfile = profile;
			currentTask = task;
			this.metaDataDir = metaDataDir;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
            if (!currentTask.Name.Equals(txtNewName.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                Validation.ErrorMsgCode errcode = Validation.CheckTaskName(txtNewName.Text, currentProfile);

                if (errcode != Validation.ErrorMsgCode.NoError)
                {
                    Validation.DisplayErrorMessage(errcode, txtNewName.Text);
                    txtNewName.Focus();
                    return;
                }

                RenameMetaFiles(currentTask.Name, txtNewName.Text.Trim(), metaDataDir);
                currentTask.Name = txtNewName.Text.Trim();
            }
            this.Close();
		}

        public static void RenameMetaFiles(string oldName, string newName, string metaDataDir)
        {
            try
            {
                if (File.Exists(metaDataDir + @"\" + oldName + "src.meta"))
                    File.Move(metaDataDir + @"\" + oldName + "src.meta",
                            metaDataDir + @"\" + newName + "src.meta");
                if (File.Exists(metaDataDir + @"\" + oldName + "tgt.meta"))
                    File.Move(metaDataDir + @"\" + oldName + "tgt.meta",
                            metaDataDir + @"\" + newName + "tgt.meta");
                if (File.Exists(metaDataDir + @"\" + oldName + ".log"))
                    File.Move(metaDataDir + @"\" + oldName + ".log",
                            metaDataDir + @"\" + newName + ".log");
            }
            catch
            {
            }
        }
	}
}