using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Storage;
using SyncSharp.Business;
using System.IO;

namespace SyncSharp
{
	public partial class TaskWizardForm : Form
	{
		int state = 1;
		SyncSharpLogic logic;
		public TaskWizardForm(SyncSharpLogic o)
		{
			logic = o;
			InitializeComponent();
		}

		public Panel GetSelectTypePanel
		{
			get { return this.pSelectType; }
		}

		public Panel GetFolderPairPanel
		{
			get { return this.pSetFolderPair; }
		}

		public Panel GetTaskNamePanel
		{
			get { return this.pTaskName; }
		}

		public String GetTaskName
		{
			get { return this.txtName.Text; }
		}

		public bool GetTaskType
		{
			get
			{
				return this.radBackup.Checked;
			}
		}

		public String GetSource
		{
			get { return this.txtSource.Text; }
		}

		public String GetTarget
		{
			get { return this.txtTarget.Text; }
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (state == 1)
			{
				if (logic.Profile.taskExists(GetTaskName.Trim()))
				{
					MessageBox.Show("Task name '" + GetTaskName.Trim() + "' already exists, please choose a unique name");
				}
				else
				{
					GetTaskNamePanel.Hide();
					GetSelectTypePanel.Show();
					GetFolderPairPanel.Hide();
					state = 2;
				}
			}
			else if (state == 2)
			{
				if (!radBackup.Checked && !radSync.Checked)
				{
					MessageBox.Show("Please choose a synchronization type");
				}
				else
				{
					GetTaskNamePanel.Hide();
					GetSelectTypePanel.Hide();
					GetFolderPairPanel.Show();
					btnNext.Text = "Done";
					btnNext.ImageIndex = 1;
					btnNext.TextImageRelation = TextImageRelation.ImageBeforeText;
					state = 3;
				}
			}
			else if (state == 3)
			{
				if (CheckFolderPair())
				{
					try
					{
						SyncTask task = new SyncTask(GetTaskName, GetSource, GetTarget, GetTaskType);
						logic.Profile.addTask(task);

						this.Close();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			if (state == 3)
			{
				GetTaskNamePanel.Hide();
				GetSelectTypePanel.Show();
				GetFolderPairPanel.Hide();
				btnNext.Text = "Next";
				btnNext.ImageIndex = 0;
				btnNext.TextImageRelation = TextImageRelation.TextBeforeImage;
				state = 2;
			}
			else if (state == 2)
			{
				GetTaskNamePanel.Show();
				GetSelectTypePanel.Hide();
				GetFolderPairPanel.Hide();
				state = 1;
			}
		}
		private bool CheckFolderPair()
		{
			string source = Environment.ExpandEnvironmentVariables(txtSource.Text);
			string target = Environment.ExpandEnvironmentVariables(txtTarget.Text);

			if (String.IsNullOrEmpty(source))
			{
				MessageBox.Show("Please provide a source directory.",
						"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (String.IsNullOrEmpty(target))
			{
				MessageBox.Show("Please provide a destination directory.",
						"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!Directory.Exists(source))
			{
				MessageBox.Show("Source directory does not exist.",
						"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!Directory.Exists(target))
			{
				MessageBox.Show("Destination directory does not exist.",
						"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (String.Equals(source, target))
			{
				MessageBox.Show("Source directory cannot be the same " +
						"as the destination directory.",
						"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (source.StartsWith(target))
			{
				MessageBox.Show("Source directory cannot be a " +
					 "subdirectory of the destination directory.",
					 "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (target.StartsWith(source))
			{
				MessageBox.Show("Destination directory cannot be a " +
					 "subdirectory of the source directory.",
					"FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			return true;
		}

		private void btnSource_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbDialog = new FolderBrowserDialog();
			if (fbDialog.ShowDialog() == DialogResult.OK)
			{
				txtSource.Text = fbDialog.SelectedPath;
			}
		}

		private void btnTarget_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbDialog = new FolderBrowserDialog();
			if (fbDialog.ShowDialog() == DialogResult.OK)
			{
				txtTarget.Text = fbDialog.SelectedPath;
			}
		}
	}
}
