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
	public partial class CreateTaskForm : Form
	{
		SyncSharpLogic logic;
        string source, target;

		public CreateTaskForm(SyncSharpLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}

        public CreateTaskForm(SyncSharpLogic logic, string source, 
                              string target) : this(logic)
        {
            txtSource.Text =  source;
            txtTarget.Text =  target;
            AutoGenerateTaskName();
        }

        /// <summary>
        /// Perform validation on source directory, target directory and task name inputs.
        /// </summary>
        /// <returns>True if inputs are entered correctly.</returns>
        private bool ValidateUserInput()
        {
            ExpandSourceTargetPaths();

            Validation.ErrorMsgCode errcode = 
                Validation.CheckFolderPair(ref source, ref target, logic.Profile, null);

            if (errcode != Validation.ErrorMsgCode.NoError)
            {
                Validation.DisplayErrorMessage(errcode, "");
                return false;
            }

            errcode = Validation.CheckTaskName(txtName.Text, logic.Profile);

            if (errcode != Validation.ErrorMsgCode.NoError)
            {
                Validation.DisplayErrorMessage(errcode, txtName.Text);
                txtName.Focus();
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
            if (!ValidateUserInput()) return;
            
            try
            {
                String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
                root = root.Substring(0, 1);

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