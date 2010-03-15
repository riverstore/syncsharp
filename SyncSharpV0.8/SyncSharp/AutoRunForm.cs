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
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        SyncSharpLogic logic;
        List<SyncTask> plugSyncList;

        private delegate void UpdateListViewCallback();
        private delegate void StartSyncCallback();
        private delegate void StartUpCallback();

        private UpdateListViewCallback listViewCallback;
        private StartSyncCallback startSyncCallback;

        System.Threading.Timer myTimer;

        private int counter;

        private delegate void AsyncMethodCaller(SyncTask task, bool isPlugSync);

        private AsyncMethodCaller syncCaller;

        public AutoRunForm(SyncSharpLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            this.counter = logic.Profile.CountDown;

            myTimer = new System.Threading.Timer(
                new TimerCallback(Timer_Tick), null, 0, 1000);

            listViewCallback = new UpdateListViewCallback(updateListView);
            startSyncCallback = new StartSyncCallback(startSync);
            syncCaller = new AsyncMethodCaller(logic.syncFolderPair);
            plugSyncList = new List<SyncTask>();
        }

        private void startSync()
        {
            if (lvTaskList.Items.Count == 0)
            {
                this.Close();
                return;
            }
            string name = lvTaskList.Items[0].SubItems[0].Text;
            SyncTask curTask = logic.Profile.getTask(name);

            lblStatus.Text = "Synchronizing folder pair in " + name;
            statusBar.Refresh();
            
            syncCaller.BeginInvoke(curTask,true, AsyncMethodCompleted, name);
        }

        private void AsyncMethodCompleted(IAsyncResult result)
        {
            plugSyncList.Remove(
                new SyncTask(result.AsyncState.ToString(), "", ""));

            this.Invoke(listViewCallback);
            this.Invoke(startSyncCallback);
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

            if (lvTaskList.Items.Count > 0 && counter <= 0)
                lvTaskList.Items[0].ForeColor = Color.Silver;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.myTimer.Dispose();
            this.Close();
            return;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lvTaskList.SelectedItems.Count == 0) return;

            int selIdx = lvTaskList.SelectedItems[0].Index;

            bool condition = (counter <= 0) ? selIdx > 1 : selIdx > 0;

            if (condition)
            {
                for (int i = 0; i < lvTaskList.Items[selIdx].SubItems.Count; i++)
                {
                    string temp = lvTaskList.Items[selIdx-1].SubItems[i].Text;
                    lvTaskList.Items[selIdx - 1].SubItems[i].Text =
                        lvTaskList.Items[selIdx].SubItems[i].Text;
                    lvTaskList.Items[selIdx].SubItems[i].Text = temp;
                }
                lvTaskList.Items[selIdx - 1].Selected = true;
                lvTaskList.RedrawItems(selIdx - 1, selIdx, true);
                lvTaskList.Focus();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lvTaskList.SelectedItems.Count == 0) return;

            int selIdx = lvTaskList.SelectedItems[0].Index;

            bool condition = (selIdx < lvTaskList.Items.Count - 1);

            if (counter <= 0) condition = condition && (selIdx > 0);

            if (condition)
            {
                for (int i = 0; i < lvTaskList.Items[selIdx].SubItems.Count; i++)
                {
                    string temp = lvTaskList.Items[selIdx + 1].SubItems[i].Text;
                    lvTaskList.Items[selIdx + 1].SubItems[i].Text =
                        lvTaskList.Items[selIdx].SubItems[i].Text;
                    lvTaskList.Items[selIdx].SubItems[i].Text = temp;
                }
                lvTaskList.Items[selIdx + 1].Selected = true;
                lvTaskList.RedrawItems(selIdx, selIdx + 1, true);
                lvTaskList.Focus();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvTaskList.SelectedItems.Count == 0) return;

            plugSyncList.Remove(new SyncTask(
                lvTaskList.FocusedItem.SubItems[0].Text, "", ""));
            updateListView();
        }

        private void Timer_Tick(object state)
        {
            this.lblStatus.Text = "Performing synchronization in "
                            + counter.ToString() + " seconds...";
            if (counter-- == 0)
            {
                myTimer.Dispose();
                this.Invoke(new StartUpCallback(ProcessStartUp));
                this.Invoke(startSyncCallback);
            }
        }

        private void ProcessStartUp()
        {
            btnCancel.Enabled = false;
        }

        private void AutoRunForm_Load(object sender, EventArgs e)
        {
            foreach (var item in logic.Profile.TaskCollection)
            {
                if (item.Settings.PlugSync)
                    plugSyncList.Add(new SyncTask(item.Name, 
                                     item.Source, item.Target));
            }

            updateListView();
        }
    }
}
