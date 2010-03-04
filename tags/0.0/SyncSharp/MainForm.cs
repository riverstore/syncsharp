using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Business;
using SyncSharp.Storage;

namespace SyncSharp.GUI
{
	public partial class MainForm : Form
	{

		public MainForm(SyncSharpLogic logic)
		{
            InitializeComponent();
            logicController = logic;
            logicController.updateRemovableRoot();
            logicController.checkAutorun();
            updateListView();
		}

		SyncSharpLogic logicController;

		internal ListView GetTaskListView
		{
			get { return taskListView; }
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
            
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
				ListViewItem lvi = new ListViewItem(item.Name);
				lvi.SubItems.Add(item.LastRun);
				lvi.SubItems.Add(item.Result);
				lvi.SubItems.Add(item.Source);
				lvi.SubItems.Add(item.Target);
				taskListView.Items.Add(lvi);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			logicController.saveProfile();
		}

		private void btnSync_Click(object sender, EventArgs e)
		{
            if (taskListView.FocusedItem == null) return;

			string source = taskListView.FocusedItem.SubItems[3].Text;
			string target = taskListView.FocusedItem.SubItems[4].Text;
            string taskname = taskListView.FocusedItem.SubItems[0].Text;

		    logicController.syncFolderPair(source, target, taskname);
			updateListView();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
            if (taskListView.FocusedItem == null) return;

			string name = taskListView.FocusedItem.SubItems[0].Text;
			logicController.removeTask(name);
			updateListView();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (taskListView.FocusedItem == null) return;

            string source = taskListView.FocusedItem.SubItems[3].Text;
            string target = taskListView.FocusedItem.SubItems[4].Text;
            string taskname = taskListView.FocusedItem.SubItems[0].Text;

            logicController.analyzeFolderPair(source, target, taskname);
            updateListView();
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
            if (taskListView.FocusedItem == null) return;

            string name = taskListView.FocusedItem.SubItems[0].Text;
            logicController.renameSelectedTask(name);
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
            openFolder(taskListView.FocusedItem.SubItems[3].Text);
        }

        private void openTargetMenuItem_Click(object sender, EventArgs e)
        {
            openFolder(taskListView.FocusedItem.SubItems[4].Text);
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            if (taskListView.FocusedItem == null) return;

            string name = taskListView.FocusedItem.SubItems[0].Text;
            logicController.copySelectedTask(name);
            updateListView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (taskListView.FocusedItem == null) return;

            string name = taskListView.FocusedItem.SubItems[0].Text;
            logicController.modifySelectedTask(name);
            updateListView();
        }
	}
}
