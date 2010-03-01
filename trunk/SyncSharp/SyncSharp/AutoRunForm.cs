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
        //SyncProfile profile;
        SyncSharpLogic logic;
        List<SyncTask> plugSyncList;
        static int counter = 5;

        private delegate void AsyncMethodCaller(string source, string target);

        public AutoRunForm(SyncSharpLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            countDownTimer = new System.Windows.Forms.Timer();
            countDownTimer.Start();

            plugSyncList = new List<SyncTask>();

            foreach (var item in logic.Profile.TaskCollection)
            {
                plugSyncList.Add(new SyncTask(item.Name,item.Source,item.Target,false,false,false));
            }

            updateListView();
        }

        private void startSync()
        {
            AsyncMethodCaller caller = new AsyncMethodCaller(logic.syncFolderPair);

            while ( lvTaskList.Items.Count > 0)
            {
                lblDisplay.Text = "Syncing " + lvTaskList.Items[0].SubItems[0].Text;
                try
                {
                    IAsyncResult result = caller.BeginInvoke(lvTaskList.Items[0].SubItems[1].Text,
                    lvTaskList.Items[0].SubItems[2].Text, null, null);
                    logic.Profile.updateSyncTaskResult(lvTaskList.Items[0].SubItems[0].Text, "Successful");
                }
                catch (Exception)
                {
                    logic.Profile.updateSyncTaskResult(lvTaskList.Items[0].SubItems[0].Text, "Unsuccessful");
                }
                logic.Profile.updateSyncTaskTime(lvTaskList.Items[0].SubItems[0].Text, DateTime.Now.ToString());

                plugSyncList.Remove(new SyncTask(lvTaskList.Items[0].SubItems[0].Text,
                    "source", "target", false, false, false));
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
            lblTimer.Text = counter.ToString();
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
        }
    }
}
