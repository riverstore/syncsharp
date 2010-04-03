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
		SyncSharpLogic logicController;
		private delegate void SyncAnalyzeCaller(SyncTask task,ToolStripStatusLabel status, bool isPlugSync);
		private SyncAnalyzeCaller syncCaller, analyzeCaller;
        private delegate void SyncAllFolderPair(ToolStripStatusLabel status, bool isPlugSync);
		private SyncAllFolderPair syncAll;
        private delegate void UpdateListViewCallback();
		private UpdateListViewCallback listViewCallback;

        private const int MIN_WIDTH = 50;

        public MainForm(SyncSharpLogic logic)
		{
			InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            logicController = logic;
		        
            if (!Directory.Exists(logicController.MetaDataDir + @"\"))
			    logicController.saveProfile();
		        
            syncCaller = new SyncAnalyzeCaller(logicController.syncFolderPair);
		    analyzeCaller = new SyncAnalyzeCaller(logicController.analyzeFolderPair);
		    listViewCallback = new UpdateListViewCallback(updateListView);
		    syncAll = new SyncAllFolderPair(logic.syncAllFolderPairs);
            
            logicController.updateRemovableRoot();
            updateListView();
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

            if (logicController.Profile.TaskCollection.Count > 0)
            {
                for (int i = 4; i <= 5; i++)
                    taskListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
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
			logicController.addNewTask();
			updateListView();
		}

		private void updateListView()
		{
            EnableButtonControls(false);
			taskListView.Items.Clear();
			foreach (var item in logicController.Profile.TaskCollection)
			{
				if (showBackupMenuItem.Checked && showSyncMenuItem.Checked)
				{
					addTaskViewItem(item);
				}
				else if (showBackupMenuItem.Checked && !showSyncMenuItem.Checked)
				{
					if (item.TypeOfSync) continue;
					addTaskViewItem(item);
				}
				else
				{
					if (!item.TypeOfSync) continue;
					addTaskViewItem(item);
				}
			}
			if (taskListView.Enabled) lblStatus.Text = "Ready";
		}

		private void addTaskViewItem(SyncTask item)
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
			logicController.checkAutorun(logicController.Profile.AutoPlay);
			logicController.saveProfile();
			if (!taskListView.Enabled) e.Cancel = true;
		}

		private void lockFormForSyncAnalyze()
		{
			ShowProgress(true);
			taskListView.Enabled = false;
			taskListView.SelectedItems.Clear();
		}

		private void btnSync_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;

			string name = taskListView.FocusedItem.SubItems[0].Text;
			SyncTask selTask = logicController.Profile.GetTask(name);
			
            if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
			{
				lockFormForSyncAnalyze();
				syncCaller.BeginInvoke(selTask, lblStatus, false, SyncAnalyzeCompleted, syncCaller);
			}
			else
			{
				MessageBox.Show("Either source or target folder does not exist",
					 "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableButtonControls(false);
			}
		}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0) return;

            if (MessageBox.Show("Delete all selected task(s) ?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem lvItem in taskListView.SelectedItems)
                    logicController.removeTask(lvItem.SubItems[0].Text, logicController.MetaDataDir);
                updateListView();
            }
        }

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAnalyze_Click(object sender, EventArgs e)
		{
            //if (taskListView.SelectedItems.Count == 0) return;

            //string name = taskListView.FocusedItem.SubItems[0].Text;
            //SyncTask selTask = logicController.Profile.GetTask(name);

            //if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
            //{
            //    lockFormForSyncAnalyze();
            //    analyzeCaller.BeginInvoke(selTask, lblStatus, false, SyncAnalyzeCompleted, analyzeCaller);
            //}
            //else
            //{
            //    MessageBox.Show("Either source or target folder does not exist",
            //    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    EnableButtonControls(false);
            //}
		}

		private void SyncAnalyzeCompleted(IAsyncResult result)
		{
			taskListView.Enabled = true;
			ShowProgress(false);
			this.Invoke(listViewCallback);
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
			if (taskListView.SelectedItems.Count == 0) return;
			logicController.renameSelectedTask(taskListView.FocusedItem.SubItems[0].Text, logicController.MetaDataDir);
			updateListView();
		}

		private void openFolder(string path)
		{
			if (System.IO.Directory.Exists(path))
				System.Diagnostics.Process.Start(path);
			else
				MessageBox.Show(path + " cannot be found");
		}

		private void openSourceMenuItem_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;
			openFolder(taskListView.FocusedItem.SubItems[4].Text);
		}

		private void openTargetMenuItem_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;
			openFolder(taskListView.FocusedItem.SubItems[5].Text);
		}

		private void copyMenuItem_Click(object sender, EventArgs e)
		{
            //if (taskListView.SelectedItems.Count == 0) return;
            //logicController.copySelectedTask(taskListView.FocusedItem.SubItems[0].Text);
            //updateListView();
		}

		private void btnModify_Click(object sender, EventArgs e)
		{
			if (taskListView.SelectedItems.Count == 0) return;
			logicController.modifySelectedTask(taskListView.FocusedItem.SubItems[0].Text);
			updateListView();
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
            lblStatus.Text = "Selected ";
            
            foreach (ListViewItem lvItem in taskListView.SelectedItems)
            {
                lblStatus.Text += lvItem.SubItems[0].Text + ", ";
            }
            lblStatus.Text = lblStatus.Text.Remove(lblStatus.Text.Length - 2);
        }

		private void syncAllMenuItem_Click(object sender, EventArgs e)
		{
			lockFormForSyncAnalyze();
			syncAll.BeginInvoke(lblStatus, true, SyncAnalyzeCompleted, syncAll);
		}

		private void showBackupMenuItem_Click(object sender, EventArgs e)
		{
			if (!showSyncMenuItem.Checked && !showBackupMenuItem.Checked)
			{
				showBackupMenuItem.Checked = true;
			}
			updateListView();
		}

		private void showSyncMenuItem_Click(object sender, EventArgs e)
		{
			if (!showSyncMenuItem.Checked && !showBackupMenuItem.Checked)
			{
				showSyncMenuItem.Checked = true;
			}
			updateListView();
		}

		private void importMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog importFrom = new OpenFileDialog();
			importFrom.Filter = "SyncSharp Profile|*.profile";
			importFrom.DefaultExt = "profile";
			if (importFrom.ShowDialog() == DialogResult.OK)
			{
				logicController.importProfile(importFrom.FileName);
                updateListView();
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
				logicController.exportProfile(exportTo.FileName);
			}
		}

		private void optionsMenuItem_Click(object sender, EventArgs e)
		{
			logicController.modifyGlobalSettings(logicController.Profile);
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
			if (taskListView.SelectedItems.Count == 0) return;
			
            string taskName = taskListView.FocusedItem.SubItems[0].Text;
			string logFile = logicController.MetaDataDir + @"\" + taskName + ".log";
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
            copyMenuItem.Enabled = isEnabled;
            analyzeMenuItem.Enabled = isEnabled;
            syncMenuItem.Enabled = isEnabled;
            syncAllMenuItem.Enabled = isEnabled;
            openSourceMenuItem.Enabled = isEnabled;
            openTargetMenuItem.Enabled = isEnabled;
            viewLogMenuItem.Enabled = isEnabled;

            syncCMItem.Enabled = isEnabled;
            modifyCMItem.Enabled = isEnabled;
            deleteCMItem.Enabled = isEnabled;
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

        private void renameCMItem_Click(object sender, EventArgs e)
        {
            renameMenuItem_Click(sender, e);
        }

        private void viewLogCMItem_Click(object sender, EventArgs e)
        {
            viewLogMenuItem_Click(sender, e);
        }
	}
}