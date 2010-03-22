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

namespace SyncSharp.GUI
{
	public partial class TaskWizardForm : Form
	{
		SyncSharpLogic logic;
		public TaskWizardForm(SyncSharpLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}

		private bool CheckFolderPair()
		{
			string source = Environment.ExpandEnvironmentVariables(txtSource.Text);
			string target = Environment.ExpandEnvironmentVariables(txtTarget.Text);
			txtSource.Text = source;
			txtTarget.Text = target;
			if (String.IsNullOrEmpty(source))
			{
				MessageBox.Show("Please provide a source directory.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (String.IsNullOrEmpty(target))
			{
				MessageBox.Show("Please provide a target directory.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!Directory.Exists(source))
			{
				MessageBox.Show("Source directory does not exist.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!Directory.Exists(target))
			{
				MessageBox.Show("Target directory does not exist.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (String.Equals(source, target))
			{
				MessageBox.Show("Source directory cannot be the same as the target directory.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (source.StartsWith(target + "\\"))
			{
				MessageBox.Show("Source directory cannot be a subdirectory of the target directory.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

            if (target.StartsWith(source + "\\"))
			{
				MessageBox.Show("Target directory cannot be a subdirectory of the source directory.", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

		private void btnAccept_Click(object sender, EventArgs e)
		{
			if (txtName.Text.Trim().Equals(""))
			{
				MessageBox.Show("Please enter a task name", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (logic.Profile.taskExists(txtName.Text.Trim()))
			{
				MessageBox.Show("Task name '" + txtName.Text.Trim() + "' already exists. Please enter another name", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!radBackup.Checked && !radSync.Checked)
			{
				MessageBox.Show("Please choose a synchronization type", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (CheckFolderPair())
			{
				try
				{
					String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
					root = root.Substring(0, 1);
					SyncTask task = new SyncTask(txtName.Text, txtSource.Text, txtTarget.Text, radSync.Checked, txtSource.Text.StartsWith(root),
							txtTarget.Text.StartsWith(root), new TaskSettings(), new Filters());
					logic.Profile.addTask(task);
					Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void AutoGenerateTaskName()
		{
			if (!String.IsNullOrEmpty(txtSource.Text.Trim()) &&
				 !String.IsNullOrEmpty(txtTarget.Text.Trim()) &&
				 Directory.Exists(txtSource.Text.Trim()) &&
				 Directory.Exists(txtTarget.Text.Trim()))
			{
				string name = (radSync.Checked) ? "Sync" : "Backup";
				name += Path.GetFileName(txtSource.Text.Trim());
				name += Path.GetFileName(txtTarget.Text.Trim());
				int counter = 1;
				string newName = name;
				while (true)
				{
					if (logic.Profile.taskExists(newName))
					{
						newName = name;
						newName += counter.ToString();
						counter++;
					}
					else
						break;
				}
				txtName.Text = newName;
			}
		}

		private void radSync_CheckedChanged(object sender, EventArgs e)
		{
			AutoGenerateTaskName();
		}

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			AutoGenerateTaskName();
		}

		private void txtTarget_TextChanged(object sender, EventArgs e)
		{
			AutoGenerateTaskName();
		}
	}
}