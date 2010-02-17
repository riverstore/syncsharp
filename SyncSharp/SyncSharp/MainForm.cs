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
					updateListView();
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void analyzeMenuItem_Click(object sender, EventArgs e)
        {
            
        }

				private void tsbtnNew_Click(object sender, EventArgs e)
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
    }
}
