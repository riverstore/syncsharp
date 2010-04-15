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
    /// <summary>
    /// Written by Guo Jiayuan and Loh Jianxiong Christoper
    /// </summary>
	public partial class TaskSetupForm : Form
	{
		#region Attributes
		private SyncTask _currentTask;
		private SyncProfile _currentProfile;
		private string _source, _target, _metaDataDir;
		#endregion

		#region Constructor
		public TaskSetupForm(SyncProfile profile, SyncTask task, string metaDataDir)
		{
			InitializeComponent();
			_currentTask = task;
			_currentProfile = profile;
			this._metaDataDir = metaDataDir;

			ReadUserSettings();
			this.tcTaskSetup.TabPages.Remove(tcTaskSetup.TabPages["tpCopyDel"]);
			this.tcTaskSetup.TabPages.Remove(tcTaskSetup.TabPages["tpLogSettings"]);
		}

		#endregion

		#region Methods
		private void ReadUserSettings()
		{
			txtSource.Text = _currentTask.Source;
			txtTarget.Text = _currentTask.Target;
			txtName.Text = _currentTask.Name;
			lblSource.Text = _currentTask.Source;
			lblTarget.Text = _currentTask.Target;
			lblLastRun.Text = _currentTask.LastRun;
			if (_currentTask.TypeOfSync)
				radSync.Checked = true;
			else
				radBackup.Checked = true;

			lblFileInclude.Text = _currentTask.Settings.FilesInclusion;
			if (lblFileInclude.Text.Equals(""))
				lblFileInclude.Text = "*.*";
			lblFileExclude.Text = _currentTask.Settings.FilesExclusion;
			if (lblFileExclude.Text.Equals(""))
				lblFileExclude.Text = "None";
			txtExclude.Text = _currentTask.Settings.FilesExclusion;
			txtInclude.Text = _currentTask.Settings.FilesInclusion;
			if (txtInclude.Text.Equals(""))
				txtInclude.Text = "*.*";

			chkSkipHidden.Checked = _currentTask.Filters.Hidden;
			chkSkipRO.Checked = _currentTask.Filters.ReadOnly;
			chkSkipSystem.Checked = _currentTask.Filters.System;
			chkSkipTemp.Checked = _currentTask.Filters.Temp;
			chkMove.Checked = _currentTask.Settings.MoveDelToRecycleBin;
			chkVerify.Checked = _currentTask.Settings.VerifyCopy;
			chkSafe.Checked = _currentTask.Settings.SafeCopy;
			chkReset.Checked = _currentTask.Settings.ResetArhiveAttributes;
			chkConfirm.Checked = _currentTask.Settings.ConfirmFileDel;
			chkAutoSync.Checked = _currentTask.Settings.PlugSync;

			if (_currentTask.Settings.SrcConflict ==
										TaskSettings.ConflictSrcAction.CopyFileToTarget)
				radToTarget.Checked = true;
			else
				radDelSource.Checked = true;
			if (_currentTask.Settings.TgtConflict ==
					TaskSettings.ConflictTgtAction.CopyFileToSource)
				radToSource.Checked = true;
			else
				radDelTarget.Checked = true;
			switch (_currentTask.Settings.SrcTgtConflict)
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
			if (_currentTask.Settings.FolderConflict == TaskSettings.ConflictFolderAction.KeepSourceName)
				radRenameSrc.Checked = true;
			else
				radRenameTgt.Checked = true;
			ndTime.Value = _currentTask.Settings.IgnoreTimeChange;
		}

		private void SaveUserSettings()
		{
			String root = Path.GetPathRoot(Directory.GetCurrentDirectory());
			root = root.Substring(0, 1);
			_currentTask.SrcOnRemovable = _source.StartsWith(root);
			_currentTask.DestOnRemovable = _target.StartsWith(root);
			_currentTask.Source = _source;
			_currentTask.Target = _target;
			_currentTask.TypeOfSync = radSync.Checked;
			UpdateFilters(txtInclude.Text, txtExclude.Text);
			_currentTask.Filters.Hidden = chkSkipHidden.Checked;
			_currentTask.Filters.ReadOnly = chkSkipRO.Checked;
			_currentTask.Filters.System = chkSkipSystem.Checked;
			_currentTask.Filters.Temp = chkSkipTemp.Checked;
			_currentTask.Settings.MoveDelToRecycleBin = chkMove.Checked;
			_currentTask.Settings.VerifyCopy = chkVerify.Checked;
			_currentTask.Settings.SafeCopy = chkSafe.Checked;
			_currentTask.Settings.ConfirmFileDel = chkConfirm.Checked;
			_currentTask.Settings.ResetArhiveAttributes = chkReset.Checked;
			_currentTask.Settings.PlugSync = chkAutoSync.Checked;
			_currentTask.Settings.SrcConflict = radToTarget.Checked ? TaskSettings.ConflictSrcAction.CopyFileToTarget :
								TaskSettings.ConflictSrcAction.DeleteSourceFile;
			_currentTask.Settings.TgtConflict = radToSource.Checked ? TaskSettings.ConflictTgtAction.CopyFileToSource :
								TaskSettings.ConflictTgtAction.DeleteTargetFile;
			if (radKeepBoth.Checked == true)
			{
				_currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.KeepBothCopies;
			}
			else if (radNewOld.Checked == true)
			{
				_currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.KeepLatestCopy;
			}
			else if (radSrcTarget.Checked == true)
			{
				_currentTask.Settings.SrcTgtConflict =
						TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget;
			}
			else
				_currentTask.Settings.SrcTgtConflict = TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource;

			_currentTask.Settings.FolderConflict = radRenameSrc.Checked == true ? TaskSettings.ConflictFolderAction.KeepSourceName :
								TaskSettings.ConflictFolderAction.KeepTargetName;
			_currentTask.Settings.IgnoreTimeChange = (int)ndTime.Value;
		}

		private void UpdateFilters(string inc, string exc)
		{
			_currentTask.Filters.FileIncludeList.Clear();
			_currentTask.Filters.FileExcludeList.Clear();
			_currentTask.Settings.FilesInclusion = ProcessFilters(inc,
					_currentTask.Filters.FileIncludeList);
			_currentTask.Settings.FilesExclusion = ProcessFilters(exc,
					_currentTask.Filters.FileExcludeList);
		}

		private string ProcessFilters(string input, List<string> filterList)
		{
			char[] delimiters = new char[] { ';' };
			string formatted = ""; string temp;
			string[] masks = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			foreach (string s in masks)
			{
				temp = s.Trim().ToLower();
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

		private void btnSwitch_Click(object sender, EventArgs e)
		{
			string temp = txtSource.Text;
			txtSource.Text = txtTarget.Text;
			txtTarget.Text = temp;
		}

		private void btnClear_Click(object sender, EventArgs e)
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

		/// <summary>
		/// Perform validation on source and target directories.
		/// </summary>
		/// <returns>True if source and target directories are entered correctly.</returns>
		private bool ValidateSourceTargetDirs()
		{
			_source = Environment.ExpandEnvironmentVariables(txtSource.Text.Trim());
			_target = Environment.ExpandEnvironmentVariables(txtTarget.Text.Trim());

			Validation.ErrorMsgCode errcode =
					Validation.CheckFolderPair(ref _source, ref _target, _currentProfile, _currentTask);

			if (errcode != Validation.ErrorMsgCode.NoError)
			{
				Validation.DisplayErrorMessage(errcode, "");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Perform validation on task name.
		/// </summary>
		/// <returns>True if task name is entered correctly.</returns>
		private bool ValidateTaskName()
		{
			if (!_currentTask.Name.Equals(txtName.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
			{
				Validation.ErrorMsgCode errcode = Validation.CheckTaskName(txtName.Text, _currentProfile);

				if (errcode != Validation.ErrorMsgCode.NoError)
				{
					Validation.DisplayErrorMessage(errcode, txtName.Text);
					txtName.Focus();
					return false;
				}
			}

			Utility.RenameMetaFiles(_currentTask.Name, txtName.Text.Trim(), _metaDataDir);
			_currentTask.Name = txtName.Text.Trim();

			return true;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			if (!ValidateSourceTargetDirs())
			{
				this.tcTaskSetup.SelectTab("tpFolderPair");
				this.tpFolderPair.Focus();
				return;
			}

			if (!ValidateTaskName())
			{
				this.tcTaskSetup.SelectTab("tpGeneral");
				this.tpGeneral.Focus();
				txtName.Focus();
				return;
			}

			UpdateFolderFilters();
			SaveUserSettings();

			if (!((Button)sender).Text.Equals("Apply"))
				this.Close();
		}

		private void UpdateFolderFilters()
		{
			if (String.Compare(_currentTask.Source, _source, true) != 0 ||
													String.Compare(_currentTask.Target, _target, true) != 0)
			{
				if (String.Compare(_currentTask.Source, _target, true) == 0 &&
				String.Compare(_currentTask.Target, _source, true) == 0)
				{
					List<String> temp = _currentTask.Filters.SourceFolderExcludeList;
					_currentTask.Filters.SourceFolderExcludeList = _currentTask.Filters.TargetFolderExcludeList;
					_currentTask.Filters.TargetFolderExcludeList = temp;
				}
				else
				{
					_currentTask.Filters.SourceFolderExcludeList.Clear();
					_currentTask.Filters.TargetFolderExcludeList.Clear();
				}
			}
		}

		private void btnSubFolders_Click(object sender, EventArgs e)
		{
			FolderFilter form = new FolderFilter(_currentTask);
			form.ShowDialog();
			btnApply.Enabled = true;
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			btnAccept_Click(sender, e);
			btnApply.Enabled = false;
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radSync_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radBackup_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void txtTarget_TextChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void txtExclude_TextChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void txtInclude_TextChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void chkSkipHidden_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void chkSkipRO_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void chkSkipSystem_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void chkSkipTemp_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radToTarget_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radDelSource_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radToSource_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radDelTarget_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radNewOld_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radKeepBoth_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radSrcTarget_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radTargetSrc_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radRenameSrc_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void radRenameTgt_CheckedChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void ndTime_ValueChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		#endregion
	}
}