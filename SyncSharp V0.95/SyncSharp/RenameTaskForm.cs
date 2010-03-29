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
            if (currentProfile.taskExists(txtNewName.Text.Trim()))
            {
                MessageBox.Show("Task name '" + txtNewName.Text.Trim() +
                    "' already exists, please choose a unique name",
                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtNewName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please enter a task name",
                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (File.Exists(metaDataDir + @"\" + currentTask.Name + "src.meta"))
                    File.Move(metaDataDir + @"\" + currentTask.Name + "src.meta",
                        metaDataDir + @"\" + txtNewName.Text.Trim() + "src.meta");
                if (File.Exists(metaDataDir + @"\" + currentTask.Name + "tgt.meta"))
                    File.Move(metaDataDir + @"\" + currentTask.Name + "tgt.meta",
                        metaDataDir + @"\" + txtNewName.Text.Trim() + "tgt.meta");
                if (File.Exists(metaDataDir + @"\" + currentTask.Name + ".log"))
                    File.Move(metaDataDir + @"\" + currentTask.Name + ".log",
                        metaDataDir + @"\" + txtNewName.Text.Trim() + ".log");

                currentTask.Name = txtNewName.Text.Trim();
                this.Close();
            }
		}
	}
}