using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SyncSharp.Storage;
using SyncSharp.Business;
using System.IO;

namespace SyncSharp.GUI
{
	public partial class TaskWizardForm : Form
	{
		SyncSharpLogic logic;
        string source, target;

		public TaskWizardForm(SyncSharpLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}

		private bool CheckFolderPair()
		{
            ExpandSourceTargetPaths();
			
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

			if (String.Compare(source, target, true) == 0)
			{
				MessageBox.Show("Source directory cannot be the same as the target directory.", "SyncSharp", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
			}

            if (source.StartsWith(target + "\\", true, null))
            {
                MessageBox.Show("Source directory cannot be a subdirectory of the target directory.", "SyncSharp",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (target.StartsWith(source + "\\", true, null))
            {
                MessageBox.Show("Target directory cannot be a subdirectory of the source directory.", "SyncSharp",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (logic.Profile.IsFolderPairExists(source, target))
            {
                MessageBox.Show("Source & target directories have been defined in another task." +
                    "\nPlease select a different source or target directory.",
                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool CheckTaskName()
        {
            if (txtName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please enter a task name", "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (logic.Profile.TaskExists(txtName.Text.Trim()))
            {
                MessageBox.Show("Task name '" + txtName.Text.Trim() + "' already exists. Please enter another name",
                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            invalidChars = string.Format(@"[{0}]", invalidChars);

            if (Regex.IsMatch(txtName.Text.Trim(), invalidChars))
            {
                MessageBox.Show(@"Task name cannot contain \ / : * ? < > | characters.",
                       "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            return true;
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!CheckFolderPair() || !CheckTaskName()) return;
            
            try
            {
                String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
                root = root.Substring(0, 1);

                //source = source.TrimEnd(new char[] { '\\' });
                //target = target.TrimEnd(new char[] { '\\' });

                SyncTask task = new SyncTask(txtName.Text.Trim(), source, target, radSync.Checked,
                                            source.StartsWith(root), target.StartsWith(root),
                                            new TaskSettings(), new Filters());
                logic.Profile.AddTask(task);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExpandSourceTargetPaths()
        {
            source = Environment.ExpandEnvironmentVariables(txtSource.Text.Trim());
            target = Environment.ExpandEnvironmentVariables(txtTarget.Text.Trim());
        }

		private void btnAbort_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void AutoGenerateTaskName()
		{
            ExpandSourceTargetPaths();

			if (!String.IsNullOrEmpty(source) && !String.IsNullOrEmpty(target) &&
                 String.IsNullOrEmpty(txtName.Text.Trim()) && Directory.Exists(source) && 
                 Directory.Exists(target))
			{
				string name = (radSync.Checked) ? "Sync" : "Backup";
				name += Path.GetFileName(txtSource.Text.Trim());
				name += Path.GetFileName(txtTarget.Text.Trim());
				int counter = 1;
				string newName = name;
				while (true)
				{
					if (logic.Profile.TaskExists(newName))
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