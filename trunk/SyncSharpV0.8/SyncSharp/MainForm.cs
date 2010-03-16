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
        private delegate void SyncAnalyzeCaller(SyncTask task,
                                        ToolStripStatusLabel status, bool isPlugSync);
        private SyncAnalyzeCaller syncCaller, analyzeCaller;

        private delegate void SyncAllFolderPair(ToolStripStatusLabel status, bool isPlugSync);
        private SyncAllFolderPair syncAll;

        private delegate void UpdateListViewCallback();
        private UpdateListViewCallback listViewCallback;

        public MainForm(SyncSharpLogic logic)
        {
            InitializeComponent();

            Form.CheckForIllegalCrossThreadCalls = false;

            logicController = logic;
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
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
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
            SyncTask selTask = logicController.Profile.getTask(name);

            if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
            {
                lockFormForSyncAnalyze();

                syncCaller.BeginInvoke(selTask, lblStatus, false, SyncAnalyzeCompleted, syncCaller);
            }
            else
            {
                MessageBox.Show("Either source or target folder does not exist",
                   "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0) return;

            string taskName = taskListView.SelectedItems[0].SubItems[0].Text;
            if (MessageBox.Show("Delete " + taskName + "?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                logicController.removeTask(taskName);
            }

            updateListView();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0) return;

            string name = taskListView.FocusedItem.SubItems[0].Text;
            SyncTask selTask = logicController.Profile.getTask(name);

            if (Directory.Exists(selTask.Source) && Directory.Exists(selTask.Target))
            {
                lockFormForSyncAnalyze();

                analyzeCaller.BeginInvoke(selTask, lblStatus, false, SyncAnalyzeCompleted, analyzeCaller);
            }
            else
            {
                MessageBox.Show("Either source or target folder does not exist",
                "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            logicController.renameSelectedTask(taskListView.FocusedItem.SubItems[0].Text);
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
            if (taskListView.SelectedItems.Count == 0) return;

            logicController.copySelectedTask(taskListView.FocusedItem.SubItems[0].Text);
            updateListView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
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
                return;
            }

            lblStatus.Text = "Selected " + taskListView.SelectedItems[0].SubItems[0].Text;
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
            string logFile = @".\Profiles\" + logicController.Profile.ID +
                             @"\" + taskName + ".log";

            if (!File.Exists(logFile))
            {
                MessageBox.Show("No log file generated for " + taskName + " yet.", "SyncSharp",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ViewLog logForm = new ViewLog(taskName, logFile);
            logForm.ShowDialog();
        }
    }
}