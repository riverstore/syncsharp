using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using SyncSharp.Business;
using SyncSharp.Storage;

namespace SyncSharp.GUI
{
	public partial class MainForm : Form
	{
		SyncSharpLogic _logicController;
		
        private delegate void SyncDelegate(SyncTask task, ToolStripStatusLabel status, bool isPlugSync);
        private delegate DialogResult AnalyzeDelegate(SyncTask task, ToolStripStatusLabel status);
        private delegate void RestoreDelegate(SyncTask task, ToolStripStatusLabel status);
        private delegate void SyncAfterAnalyzeDelegate(SyncTask task, ToolStripStatusLabel status);

        private SyncDelegate _syncCaller;
        private AnalyzeDelegate _analyzeCaller;
        private RestoreDelegate _restoreCaller;
        private SyncAfterAnalyzeDelegate _syncAfterAnalyzeCaller;

        private delegate void SyncAllFolderPair(ToolStripStatusLabel status, bool isPlugSync);
		private SyncAllFolderPair _syncAllCaller;
        private delegate void UpdateListViewDelegate();
		private UpdateListViewDelegate _listViewCallback;

        private string _sourceDir;

        private const int MIN_WIDTH = 50;

        public MainForm(SyncSharpLogic logic)
		{
			InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            _logicController = logic;
		        
            if (!Directory.Exists(_logicController.MetaDataDir + @"\"))
			    _logicController.SaveProfile();

            _syncCaller = new SyncDelegate(_logicController.SyncFolderPair);
            _analyzeCaller = new AnalyzeDelegate(_logicController.AnalyzeFolderPair);
            _restoreCaller = new RestoreDelegate(_logicController.RestoreSource);
		    _listViewCallback = new UpdateListViewDelegate(UpdateListView);
		    _syncAllCaller = new SyncAllFolderPair(_logicController.SyncAllFolderPairs);
            _syncAfterAnalyzeCaller = new SyncAfterAnalyzeDelegate(_logicController.SyncAfterAnalyze);

            _sourceDir = "";
            
            _logicController.UpdateRemovableRoot();
            UpdateListView();
		}

		private void ShowProgress(bool Visible)
		{
			progressBar.Visible = Visible;
			lblStatus.BorderSides = (Visible) ? ToolStripStatusLabelBorderSides.Right
					: ToolStripStatusLabelBorderSides.None;
		}

		public ListView GetTaskListView
		{
			get { return taskListView; }
		}

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowProgress(false);
            lblStatus.Text = "Ready";
        }

		private void modifyMenuItem_Click(object sender, EventArgs e)
		{
			btnModify_Click(sender, e);
		}

		private void analyzeMenuItem_Click(object sender, EventArgs e)
		{
			btnAnalyze_Click(sender, e);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			_logicController.AddNewTask();
			UpdateListView();
		}

		private void UpdateListView()
		{
            EnableButtonControls(false);
			taskListView.Items.Clear();
			foreach (var item in _logicController.Profile.TaskCollection)
			{
				if (showBackupMenuItem.Checked && showSyncMenuItem.Checked)
				{
					AddTaskViewItem(item);
				}
				else if (showBackupMenuItem.Checked && !showSyncMenuItem.Checked)
				{
					if (item.TypeOfSync) continue;
					AddTaskViewItem(item);
				}
				else
				{
					if (!item.TypeOfSync) continue;
					AddTaskViewItem(item);
				}
			}

            if (_logicController.Profile.TaskCollection.Count > 0)
            {
                for (int i = 4; i <= 5; i++)
                    taskListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
			if (taskListView.Enabled) lblStatus.Text = "Ready";
		}

		private void AddTaskViewItem(SyncTask item)
		{
			ListViewItem lvi = new ListViewItem(item.Name);
			string type = (item.TypeOfSync) ? "Synchronize" : "Backup";
			lvi.SubItems.Add(type);
			lvi.SubItems.Add(item.LastRun);
			lvi.SubItems.Add(item.Result);
			lvi.SubItems.Add(item.Source);
			lvi.SubItems.Add(item.Target);
			taskListView.Items.Add(lvi);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_logicController.CheckAutorun(_logicController.Profile.AutoPlay);
			_logicController.SaveProfile();
			if (!taskListView.Enabled) e.Cancel = true;
		}

		private void LockForm()
		{
			ShowProgress(true);
			taskListView.Enabled = false;
		}

		private void btnSync_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;

            foreach (ListViewItem selItem in taskListView.SelectedItems)
            {
                string name = selItem.SubItems[0].Text;
                SyncTask selTask = _logicController.Profile.GetTask(name);
                if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
                {
                    LockForm();
                    taskListView.SelectedItems.Clear();
                    _syncCaller.BeginInvoke(selTask, lblStatus, false, SyncCompleted, _syncCaller);
                }
            }
		}

        private void RestoreSource()
        {
            if (taskListView.SelectedItems.Count == 0) return;

            foreach (ListViewItem selItem in taskListView.SelectedItems)
            {
                string name = selItem.SubItems[0].Text;
                SyncTask selTask = _logicController.Profile.GetTask(name);
                if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
                {
                    LockForm();
                    taskListView.SelectedItems.Clear();
                    _restoreCaller.BeginInvoke(selTask, lblStatus, SyncCompleted, _restoreCaller);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0) return;

            if (MessageBox.Show("Delete all selected task(s) ?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem lvItem in taskListView.SelectedItems)
                    _logicController.RemoveTask(lvItem.SubItems[0].Text, _logicController.MetaDataDir);
                UpdateListView();
            }
        }

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAnalyze_Click(object sender, EventArgs e)
		{
            if (taskListView.SelectedItems.Count == 0 || 
                taskListView.SelectedItems.Count > 1)
                return;

            string name = taskListView.FocusedItem.SubItems[0].Text;
            SyncTask selTask = _logicController.Profile.GetTask(name);

            if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
            {
                LockForm();
                _analyzeCaller.BeginInvoke(selTask, lblStatus, AnalyzeCompleted, _analyzeCaller);
            }
            else
            {
                MessageBox.Show("Either source or target folder does not exist",
                "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableButtonControls(false);
            }
		}

		private void SyncCompleted(IAsyncResult result)
		{
			taskListView.Enabled = true;
			ShowProgress(false);
            UpdateListView();
		}

        private void AnalyzeCompleted(IAsyncResult result)
        {
            DialogResult dr = _analyzeCaller.EndInvoke(result);
            string name = taskListView.FocusedItem.SubItems[0].Text;
            SyncTask selTask = _logicController.Profile.GetTask(name);
            
            if (dr == DialogResult.OK)
                _syncAfterAnalyzeCaller.BeginInvoke(selTask, lblStatus, SyncCompleted, _syncAfterAnalyzeCaller); 
            else
            {
                if (dr == DialogResult.Abort)
                    _logicController.UpdateSyncTaskResult(selTask, "Aborted");

                taskListView.Enabled = true;
                taskListView.SelectedItems.Clear();
                ShowProgress(false);
            }
            UpdateListView();
        }

		private void syncMenuItem_Click(object sender, EventArgs e)
		{
			btnSync_Click(sender, e);
		}

		private void newMenuItem_Click(object sender, EventArgs e)
		{
			btnNew_Click(sender, e);
		}

		private void deleteMenuItem_Click(object sender, EventArgs e)
		{
			btnDelete_Click(sender, e);
		}

		private void renameMenuItem_Click(object sender, EventArgs e)
		{
            if (taskListView.SelectedItems.Count == 0 ||
                taskListView.SelectedItems.Count > 1)
                return;

			_logicController.RenameSelectedTask(taskListView.FocusedItem.SubItems[0].Text, _logicController.MetaDataDir);
			UpdateListView();
		}

		private void OpenFolder(string path)
		{
			if (System.IO.Directory.Exists(path))
				System.Diagnostics.Process.Start(path);
			else
				MessageBox.Show(path + " cannot be found");
		}

		private void openSourceMenuItem_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;

            foreach (ListViewItem selItem in taskListView.SelectedItems)
                OpenFolder(selItem.SubItems[4].Text);
		}

		private void openTargetMenuItem_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;

            foreach (ListViewItem selItem in taskListView.SelectedItems)
                OpenFolder(selItem.SubItems[5].Text);
		}

		private void btnModify_Click(object sender, EventArgs e)
		{
            if (taskListView.SelectedItems.Count == 0 ||
                taskListView.SelectedItems.Count > 1)
                return;

			_logicController.ModifySelectedTask(taskListView.FocusedItem.SubItems[0].Text);
			UpdateListView();
		}

        private void DisableAnalyzeForBackup()
        {
            string name = taskListView.SelectedItems[0].SubItems[0].Text;
            SyncTask selTask = _logicController.Profile.GetTask(name);

            if (!selTask.TypeOfSync)
            {
                analyzeCMItem.Enabled = false;
                analyzeMenuItem.Enabled = false;
                btnAnalyze.Enabled = false;
            }
        }

        private void DisableRestoreButtons()
        {
            string name = taskListView.SelectedItems[0].SubItems[0].Text;
            SyncTask selTask = _logicController.Profile.GetTask(name);

            if (selTask.TypeOfSync || (!selTask.TypeOfSync && !SyncMetaData.IsMetaDataExists
                (_logicController.MetaDataDir + @"\" + selTask.Name + ".bkp")))
            {
                restoreCMItem.Enabled = false;
                restoreMenuItem.Enabled = false;
            }
        }

        private void taskListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0)
            {
                lblStatus.Text = (taskListView.Enabled) ? "" : lblStatus.Text;
                EnableButtonControls(false);
                return;
            }

            EnableButtonControls(true);

            if (taskListView.SelectedItems.Count > 1)
                EnableMultiSelectionControls(false);
            else
            {
                DisableAnalyzeForBackup();
                DisableRestoreButtons();
            }
            
            if (taskListView.SelectedItems.Count == taskListView.Items.Count)
            {
                lblStatus.Text = "Selected all tasks";
                return;
            }

            lblStatus.Text = "Selected ";
            
            foreach (ListViewItem lvItem in taskListView.SelectedItems)
                lblStatus.Text += lvItem.SubItems[0].Text + ", ";
            
            lblStatus.Text = lblStatus.Text.Remove(lblStatus.Text.Length - 2);
        }

		private void syncAllMenuItem_Click(object sender, EventArgs e)
		{
			LockForm();
			_syncAllCaller.BeginInvoke(lblStatus, true, SyncCompleted, _syncAllCaller);
		}

		private void showBackupMenuItem_Click(object sender, EventArgs e)
		{
			if (!showSyncMenuItem.Checked && !showBackupMenuItem.Checked)
			{
				showBackupMenuItem.Checked = true;
			}
			UpdateListView();
		}

		private void showSyncMenuItem_Click(object sender, EventArgs e)
		{
			if (!showSyncMenuItem.Checked && !showBackupMenuItem.Checked)
			{
				showSyncMenuItem.Checked = true;
			}
			UpdateListView();
		}

		private void importMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog importFrom = new OpenFileDialog();
			importFrom.Filter = "SyncSharp Profile|*.profile";
			importFrom.DefaultExt = "profile";
			if (importFrom.ShowDialog() == DialogResult.OK)
			{
				_logicController.ImportProfile(importFrom.FileName);
                UpdateListView();
			}
		}

		private void exportMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog exportTo = new SaveFileDialog();
			exportTo.Title = "Export Profile";
			exportTo.Filter = "SyncSharp Profile|*.profile";
			exportTo.DefaultExt = "profile";
			if (exportTo.ShowDialog() == DialogResult.OK)
			{
				_logicController.ExportProfile(exportTo.FileName);
			}
		}

		private void optionsMenuItem_Click(object sender, EventArgs e)
		{
			_logicController.ModifyGlobalSettings(_logicController.Profile);
		}

		private void aboutMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.ShowDialog();
		}

		private void hideToolBarMenuItem_Click(object sender, EventArgs e)
		{
			this.toolBar.Visible = !this.toolBar.Visible;
			this.hideToolBarMenuItem.Text = (toolBar.Visible) ? "&Hide Toolbar" : "&Unhide Toolbar";
		}

		private void viewLogMenuItem_Click(object sender, EventArgs e)
		{
            if (taskListView.SelectedItems.Count == 0 ||
                taskListView.SelectedItems.Count > 1)
                return;

            string taskName = taskListView.FocusedItem.SubItems[0].Text;
			string logFile = _logicController.MetaDataDir + @"\" + taskName + ".log";
			if (!File.Exists(logFile))
			{
				MessageBox.Show("No log file generated for " + taskName + " yet.", "SyncSharp",
							 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ViewLog logForm = new ViewLog(taskName, logFile);
			logForm.ShowDialog();
		}

        private void selectAllMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in taskListView.Items)
                item.Selected = true;
        }

        private void EnableButtonControls(bool isEnabled)
        {
            btnAnalyze.Enabled = isEnabled;
            btnSync.Enabled = isEnabled;
            btnModify.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;

            renameMenuItem.Enabled = isEnabled;
            modifyMenuItem.Enabled = isEnabled;
            deleteMenuItem.Enabled = isEnabled;
            analyzeMenuItem.Enabled = isEnabled;
            syncMenuItem.Enabled = isEnabled;
            restoreMenuItem.Enabled = isEnabled;
            syncAllMenuItem.Enabled = isEnabled;
            openSourceMenuItem.Enabled = isEnabled;
            openTargetMenuItem.Enabled = isEnabled;
            viewLogMenuItem.Enabled = isEnabled;

            deleteCMItem.Enabled = isEnabled;
            syncCMItem.Enabled = isEnabled;
            restoreCMItem.Enabled = isEnabled;
            analyzeCMItem.Enabled = isEnabled;
            modifyCMItem.Enabled = isEnabled;
            renameCMItem.Enabled = isEnabled;
            viewLogCMItem.Enabled = isEnabled;
        }

        private void EnableMultiSelectionControls(bool isEnabled)
        {
            btnAnalyze.Enabled = isEnabled;
            btnModify.Enabled = isEnabled;

            renameMenuItem.Enabled = isEnabled;
            modifyMenuItem.Enabled = isEnabled;
            analyzeMenuItem.Enabled = isEnabled;
            viewLogMenuItem.Enabled = isEnabled;

            restoreCMItem.Enabled = isEnabled;
            analyzeCMItem.Enabled = isEnabled;
            modifyCMItem.Enabled = isEnabled;
            renameCMItem.Enabled = isEnabled;
            viewLogCMItem.Enabled = isEnabled;
        }

        private void taskListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.NewWidth < MIN_WIDTH)
            {
                e.Cancel = true;
                e.NewWidth = taskListView.Columns[e.ColumnIndex].Width;
            }
        }

        private void taskListView_DoubleClick(object sender, EventArgs e)
        {
            btnModify_Click(sender, e);
        }

        private void newTaskCMItem_Click(object sender, EventArgs e)
        {
            btnNew_Click(sender, e);
        }

        private void syncCMItem_Click(object sender, EventArgs e)
        {
            btnSync_Click(sender, e);
        }

        private void modifyCMItem_Click(object sender, EventArgs e)
        {
            btnModify_Click(sender, e);
        }

        private void deleteCMItem_Click(object sender, EventArgs e)
        {
            deleteMenuItem_Click(sender, e);
        }

        private void restoreMenuItem_Click(object sender, EventArgs e)
        {
            RestoreSource();
        }

        private void restoreCMItem_Click(object sender, EventArgs e)
        {
            RestoreSource();
        }

        private void renameCMItem_Click(object sender, EventArgs e)
        {
            renameMenuItem_Click(sender, e);
        }

        private void viewLogCMItem_Click(object sender, EventArgs e)
        {
            viewLogMenuItem_Click(sender, e);
        }

        private void analyzeCMItem_Click(object sender, EventArgs e)
        {
            btnAnalyze_Click(sender, e);
        }

        private void taskListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private void taskListView_DragDrop(object sender, DragEventArgs e)
        {
            string[] directories = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (!Directory.Exists(directories[0]))
                directories[0] = Directory.GetParent(directories[0]).FullName;

            if (_sourceDir != "")
            {
                CreateTaskForm form = new CreateTaskForm(_logicController, _sourceDir, directories[0]);
                form.ShowDialog();
                UpdateListView();
                _sourceDir = "";
            }
            else
            {
                if (directories.Length >= 2)
                {
                    directories[1] = Directory.Exists(directories[1]) ? directories[1] :
                                    Directory.GetParent(directories[1]).FullName;
                    
                    CreateTaskForm form = new CreateTaskForm(_logicController, directories[0], directories[1]);
                    form.ShowDialog();
                    UpdateListView();
                }
                else
                {
                    _sourceDir = directories[0];
                    lblStatus.Text = "Dropped source folder, awaiting for target folder.";
                }
            }
        }
	}
}