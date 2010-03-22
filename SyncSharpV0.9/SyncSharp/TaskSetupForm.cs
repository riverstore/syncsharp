using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SyncSharp.Business;
using SyncSharp.Storage;

namespace SyncSharp.GUI
{
	public partial class TaskSetupForm : Form
	{
		SyncTask currentTask;
		SyncProfile currentProfile;

		public TaskSetupForm(SyncProfile profile, SyncTask task)
		{
			InitializeComponent();
			currentTask = task;
			currentProfile = profile;
			ReadUserSettings();
			this.tcTaskSetup.TabPages.Remove(tcTaskSetup.TabPages["tpCopyDel"]);
			this.tcTaskSetup.TabPages.Remove(tcTaskSetup.TabPages["tpLogSettings"]);
		}

		private void ReadUserSettings()
		{
			txtSource.Text = currentTask.Source;
			txtTarget.Text = currentTask.Target;
			lblTaskName.Text = currentTask.Name;
			lblSource.Text = currentTask.Source;
			lblTarget.Text = currentTask.Target;
			lblLastRun.Text = currentTask.LastRun;
			if (currentTask.TypeOfSync)
				radSync.Checked = true;
			else
				radBackup.Checked = true;
			lblFileInclude.Text = currentTask.Settings.FilesInclusion;
            if (lblFileInclude.Text.Equals(""))
                lblFileInclude.Text = "*.*";
			lblFileExclude.Text = currentTask.Settings.FilesExclusion;
			if (lblFileExclude.Text.Equals(""))
				lblFileExclude.Text = "None";
			txtExclude.Text = currentTask.Settings.FilesExclusion;
			txtInclude.Text = currentTask.Settings.FilesInclusion;
			chkSkipHidden.Checked = currentTask.Filters.Hidden;
			chkSkipRO.Checked = currentTask.Filters.ReadOnly;
			chkSkipSystem.Checked = currentTask.Filters.System;
			chkSkipTemp.Checked = currentTask.Filters.Temp;
			chkMove.Checked = currentTask.Settings.MoveDelToRecycleBin;
			chkVerify.Checked = currentTask.Settings.VerifyCopy;
			chkSafe.Checked = currentTask.Settings.SafeCopy;
			chkReset.Checked = currentTask.Settings.ResetArhiveAttributes;
			chkConfirm.Checked = currentTask.Settings.ConfirmFileDel;
			chkAutoSync.Checked = currentTask.Settings.PlugSync;
			if (currentTask.Settings.SrcConflict ==
					TaskSettings.ConflictSrcAction.CopyFileToTarget)
				radToTarget.Checked = true;
			else
				radDelSource.Checked = true;
			if (currentTask.Settings.TgtConflict ==
					TaskSettings.ConflictTgtAction.CopyFileToSource)
				radToSource.Checked = true;
			else
				radDelTarget.Checked = true;
			switch (currentTask.Settings.SrcTgtConflict)
			{
				case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
					radKeepBoth.Checked = true;
					break;
				case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
					radNewOld.Checked = true;
					break;
				case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
					radSrcTarget.Checked = true;
					break;
				case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
					radTargetSrc.Checked = true;
					break;
			}
			if (currentTask.Settings.FolderConflict == TaskSettings.ConflictFolderAction.KeepSourceName)
				radRenameSrc.Checked = true;
			else
				radRenameTgt.Checked = true;
			ndTime.Value = currentTask.Settings.IgnoreTimeChange;
		}

		private void SaveUserSettings()
		{
			String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
			root = root.Substring(0, 1);
			currentTask.SrcOnRemovable = Source.StartsWith(root);
			currentTask.DestOnRemovable = Target.StartsWith(root);
			currentTask.Source = Source;
			currentTask.Target = Target;
			currentTask.TypeOfSync = radSync.Checked;
			UpdateFilters(txtInclude.Text, txtExclude.Text);
			currentTask.Filters.Hidden = chkSkipHidden.Checked;
			currentTask.Filters.ReadOnly = chkSkipRO.Checked;
			currentTask.Filters.System = chkSkipSystem.Checked;
			currentTask.Filters.Temp = chkSkipTemp.Checked;
			currentTask.Settings.MoveDelToRecycleBin = chkMove.Checked;
			currentTask.Settings.VerifyCopy = chkVerify.Checked;
			currentTask.Settings.SafeCopy = chkSafe.Checked;
			currentTask.Settings.ConfirmFileDel = chkConfirm.Checked;
			currentTask.Settings.ResetArhiveAttributes = chkReset.Checked;
			currentTask.Settings.PlugSync = chkAutoSync.Checked;
			currentTask.Settings.SrcConflict = radToTarget.Checked ? TaskSettings.ConflictSrcAction.CopyFileToTarget : TaskSettings.ConflictSrcAction.DeleteSourceFile;
			currentTask.Settings.TgtConflict = radToSource.Checked ? TaskSettings.ConflictTgtAction.CopyFileToSource : TaskSettings.ConflictTgtAction.DeleteTargetFile;
			if (radKeepBoth.Checked == true)
			{
				currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.KeepBothCopies;
			}
			else if (radNewOld.Checked == true)
			{
				currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.KeepLatestCopy;
			}
			else if (radSrcTarget.Checked == true)
			{
				currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget;
			}
			else
				currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource;
      currentTask.Settings.FolderConflict = radRenameSrc.Checked == true ? TaskSettings.ConflictFolderAction.KeepSourceName : TaskSettings.ConflictFolderAction.KeepTargetName;
			currentTask.Settings.IgnoreTimeChange = (int)ndTime.Value;
		}

		private void UpdateFilters(string inc, string exc)
		{
			currentTask.Filters.FileIncludeList.Clear();
			currentTask.Filters.FileExcludeList.Clear();
			currentTask.Settings.FilesInclusion = processFilters(inc,
					currentTask.Filters.FileIncludeList);
			currentTask.Settings.FilesExclusion = processFilters(exc,
					currentTask.Filters.FileExcludeList);
		}

		private string processFilters(string input, List<string> filterList)
		{
			char[] delimiters = new char[] { ';' };
			string formatted = ""; string temp;
			string[] masks = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			foreach (string s in masks)
			{
				temp = s.Trim();
				if (temp.Equals("")) continue;
				filterList.Add(temp);
				formatted += temp + "; ";
				if (temp.Equals("*.*") || temp.Equals("*"))
				{
					filterList.Clear();
					filterList.Add("*.*");
					break;
				}
			}
			return String.IsNullOrEmpty(formatted.Trim()) ?
			formatted : formatted.Remove(formatted.Length - 2);
		}

		public string Source
		{
			get { return txtSource.Text; }
			set { txtSource.Text = value; }
		}

		public string Target
		{
			get { return txtTarget.Text; }
			set { txtTarget.Text = value; }
		}

		private void btnSwitch_Click(object sender, EventArgs e)
		{
			string temp = txtSource.Text;
			txtSource.Text = txtTarget.Text;
			txtTarget.Text = temp;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			txtSource.Clear();
			txtTarget.Clear();
		}

		private void btnTarget_Click(object sender, EventArgs e)
		{
			fbDialog = new FolderBrowserDialog();
			if (fbDialog.ShowDialog() == DialogResult.OK)
			{
				txtTarget.Text = fbDialog.SelectedPath;
			}
		}

		private void btnSource_Click(object sender, EventArgs e)
		{
			fbDialog = new FolderBrowserDialog();
			if (fbDialog.ShowDialog() == DialogResult.OK)
			{
				txtSource.Text = fbDialog.SelectedPath;
			}
		}

        private bool CheckFolderPair()
        {
            string source = Environment.ExpandEnvironmentVariables(txtSource.Text);
            string target = Environment.ExpandEnvironmentVariables(txtTarget.Text);
            Source = source;
            Target = target;
            if (String.IsNullOrEmpty(source))
            {
                MessageBox.Show("Please provide a source directory.",
                        "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (String.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please provide a target directory.",
                        "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(source))
            {
                MessageBox.Show("Source directory does not exist.",
                        "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(target))
            {
                MessageBox.Show("Target directory does not exist.",
                        "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (String.Equals(source, target))
            {
                MessageBox.Show("Source directory cannot be the same " +
                        "as the target directory.",
                        "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (source.StartsWith(target + "\\"))
            {
                MessageBox.Show("Source directory cannot be a " +
                     "subdirectory of the target directory.",
                     "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (target.StartsWith(source + "\\"))
            {
                MessageBox.Show("Target directory cannot be a " +
                     "subdirectory of the source directory.",
                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			btnApply_Click(sender, e);
			this.Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			if (CheckFolderPair())
			{
				SaveUserSettings();
			}
		}

		private void btnSubFolders_Click(object sender, EventArgs e)
		{
			FolderFilter form = new FolderFilter(currentTask);
			form.ShowDialog();
		}

		private void lblChange_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RenameTaskForm form = new RenameTaskForm(currentProfile, currentTask);
			form.ShowDialog();
			lblTaskName.Text = currentTask.Name;
		}
	}
}