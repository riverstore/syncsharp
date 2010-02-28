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

namespace SyncSharp
{
	public partial class MainForm : Form
	{

		public MainForm()
		{
			InitializeComponent();
		}

		SyncSharpLogic logicController;

		internal ListView GetTaskListView
		{
			get { return taskListView; }
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			logicController = new SyncSharpLogic();
			logicController.loadProfile();
			logicController.updateRemovableRoot();
			logicController.checkAutoRun();
			updateListView();
		}

		private void editMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string name = taskListView.FocusedItem.SubItems[0].Text;
				logicController.modifySelectedTask(name);
				updateListView();
			}
			catch (Exception)
			{
			}		
		}

		private void analyzeMenuItem_Click(object sender, EventArgs e)
		{

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
			try
			{
				string source = taskListView.FocusedItem.SubItems[3].Text;
				string target = taskListView.FocusedItem.SubItems[4].Text;
				logicController.syncFolderPair(source, target);
				logicController.updateSyncTaskResult(taskListView.FocusedItem.SubItems[0].Text, "Successful");
				logicController.updateSyncTaskTime(taskListView.FocusedItem.SubItems[0].Text, DateTime.Now.ToString());
			}
			catch (NullReferenceException)
			{
			}
			catch (Exception)
			{
				logicController.updateSyncTaskResult(taskListView.FocusedItem.SubItems[0].Text, "Unsuccessful");
			}
			updateListView();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string name = taskListView.FocusedItem.SubItems[0].Text;
				logicController.removeTask(name);
				updateListView();
			}
			catch (Exception)
			{
			}		
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				string name = taskListView.FocusedItem.SubItems[0].Text;
				logicController.modifySelectedTask(name);
				updateListView();
			}
			catch (Exception)
			{
			}
		}

		private void newMenuItem_Click(object sender, EventArgs e)
		{
			logicController.addNewTask();
			updateListView();
		}

		private void deleteMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string name = taskListView.FocusedItem.SubItems[0].Text;
				logicController.removeTask(name);
				updateListView();
			}
			catch (Exception)
			{
			}
		}

		private void renameMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string name = taskListView.FocusedItem.SubItems[0].Text;
				logicController.renameSelectedTask(name);
				updateListView();
			}
			catch (Exception)
			{
			}
		}

		private void syncMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string source = taskListView.FocusedItem.SubItems[3].Text;
				string target = taskListView.FocusedItem.SubItems[4].Text;
				logicController.syncFolderPair(source, target);
				logicController.updateSyncTaskResult(taskListView.FocusedItem.SubItems[0].Text, "Successful");
				logicController.updateSyncTaskTime(taskListView.FocusedItem.SubItems[0].Text, DateTime.Now.ToString());
			}
			catch (NullReferenceException)
			{
			}
			catch (Exception)
			{
				logicController.updateSyncTaskResult(taskListView.FocusedItem.SubItems[0].Text, "Unsuccessful");
			}
			updateListView();
		}

		private void openSourceMenuItem_Click(object sender, EventArgs e)
		{
			if(System.IO.Directory.Exists(taskListView.FocusedItem.SubItems[3].Text))
			{
			System.Diagnostics.Process.Start(taskListView.FocusedItem.SubItems[3].Text);
			}
			else
				MessageBox.Show("Source folder cannot be found");
		}

		private void openTargetMenuItem_Click(object sender, EventArgs e)
		{
			if(System.IO.Directory.Exists(taskListView.FocusedItem.SubItems[3].Text))
			{
			System.Diagnostics.Process.Start(taskListView.FocusedItem.SubItems[4].Text);
			}
			else
				MessageBox.Show("Target folder cannot be found");
		}

		private void copyMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string name = taskListView.FocusedItem.SubItems[0].Text;
				logicController.copySelectedTask(name);
				updateListView();
			}
			catch (Exception)
			{
			}
		}
	}
}
