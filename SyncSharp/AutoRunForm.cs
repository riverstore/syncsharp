using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SyncSharp.Storage;
using SyncSharp.Business;

namespace SyncSharp.GUI
{
    public partial class AutoRunForm : Form
    {
        SyncSharpLogic logic;
        List<SyncTask> plugSyncList;
        static int counter = 5;

        private delegate void AsyncMethodCaller(string source, string target, string taskName);

        public AutoRunForm(SyncSharpLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            countDownTimer = new System.Windows.Forms.Timer();
            plugSyncList = new List<SyncTask>();
        }

        private void startSync()
        {
            AsyncMethodCaller caller = new AsyncMethodCaller(logic.syncFolderPair);

            while (lvTaskList.Items.Count > 0)
            {
                string taskName = lvTaskList.Items[0].SubItems[0].Text;
                
                lblTimer.Text = "Synchronizing folder pair in " + taskName;
                statusBar.Refresh();
                
                string source = lvTaskList.Items[0].SubItems[1].Text;
                string target = lvTaskList.Items[0].SubItems[2].Text;

                //IAsyncResult result = caller.BeginInvoke(source, target, taskName, null, null);
                logic.syncFolderPair(source, target, taskName);

                plugSyncList.Remove(new SyncTask(taskName, source, target));
                updateListView();
            }
        }

        private void updateListView()
        {
            lvTaskList.Items.Clear();
			foreach (var item in plugSyncList)
			{
				ListViewItem lvi = new ListViewItem(item.Name);
				lvi.SubItems.Add(item.Source);
				lvi.SubItems.Add(item.Target);
				lvTaskList.Items.Add(lvi);
			}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {

        }

        private void btnDown_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            this.lblTimer.Text = "Performing synchronization in "
                            + counter.ToString() + " seconds...";
            counter--;
            if (counter == 0)
            {
                countDownTimer.Enabled = false;
                countDownTimer.Stop();
                lblTimer.Text = "";
                startSync();
                this.Close();
            }
        }

        
        private void AutoRunForm_Load(object sender, EventArgs e)
        {
            countDownTimer.Start();
            foreach (var item in logic.Profile.TaskCollection)
            {
                plugSyncList.Add(new SyncTask(item.Name, item.Source, item.Target));
            }

            updateListView();
        }
    }
}
