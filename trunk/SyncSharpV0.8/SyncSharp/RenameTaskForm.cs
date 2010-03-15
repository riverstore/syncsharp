using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Storage;

namespace SyncSharp.GUI
{
	public partial class RenameTaskForm : Form
	{
		SyncProfile currentProfile;
		SyncTask currentTask;
		public RenameTaskForm(SyncProfile profile, SyncTask task)
		{
			currentProfile = profile;
			currentTask = task;
			InitializeComponent();
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
					"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else if (txtNewName.Text.Trim().Equals(""))
			{
				MessageBox.Show("Please enter a task name",
					"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				currentTask.Name = txtNewName.Text.Trim();
				this.Close();
			}
		}

	}
}
