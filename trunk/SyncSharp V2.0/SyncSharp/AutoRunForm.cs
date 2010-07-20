using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Storage;
using SyncSharp.Business;

namespace SyncSharp.GUI
{
    /// <summary>
    /// Written by Guo Jiayuan and Loh Jianxiong Christoper
    /// </summary>
	public partial class AutoRunForm : Form, IObserver
	{
		private SyncSharpLogic _logic;
		private List<SyncTask> _plugSyncList;
		private delegate void SyncDelegate(SyncTask task, bool isPlugSync);
		private delegate void UpdateListViewDelegate();
		private delegate void StartSyncDelegate();

        private UpdateListViewDelegate _listViewCallback;
        private StartSyncDelegate _startSyncCallback;
		
		private int _counter;
		private SyncDelegate _syncCaller;

        private const int MIN_WIDTH = 50;

		public AutoRunForm(SyncSharpLogic logic)
		{
			InitializeComponent();
			
			this._logic = logic;
			_counter = logic.Profile.CountDown;
			_listViewCallback = new UpdateListViewDelegate(UpdateListView);
            _startSyncCallback = new StartSyncDelegate(StartSync);
			_syncCaller = new SyncDelegate(logic.SyncFolderPair);
			_plugSyncList = new List<SyncTask>();
		}

		private void StartSync()
		{
            if (lvTaskList.Items.Count == 0) {
                Close();
                return;
            }

			string name = lvTaskList.Items[0].SubItems[0].Text;
			SyncTask curTask = _logic.Profile.GetTask(name);
			_syncCaller.BeginInvoke(curTask, true, SyncCompleted, name);
		}

        public void Update(string status)
        {
            lblStatus.Text = status;
        }

		private void SyncCompleted(IAsyncResult result)
		{
			_plugSyncList.Remove(new SyncTask(result.AsyncState.ToString(), "", ""));
			Invoke(_listViewCallback);
			Invoke(_startSyncCallback);
		}

		private void UpdateListView()
		{
			lvTaskList.Items.Clear();
			foreach (var item in _plugSyncList)
			{
				ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add("In queue");
				lvi.SubItems.Add(item.Source);
				lvi.SubItems.Add(item.Target);
				lvTaskList.Items.Add(lvi);
			}
            if (lvTaskList.Items.Count > 0 && _counter <= 0)
            {
                lvTaskList.Items[0].ForeColor = Color.Silver;
                lvTaskList.Items[0].SubItems[1].Text = "Processing";
            }
		}

		private void ShowProgressBar()
		{
			btnBack.Enabled = false;
			lblTimer.BorderSides = ToolStripStatusLabelBorderSides.Right;
			progressBar.Visible = true;
		}

		private void AutoRunForm_Load(object sender, EventArgs e)
		{
            lblStatus.Text = "Performing synchronization in";
            lblTimer.Text = _counter + " second(s)..";
			foreach (var item in _logic.Profile.TaskCollection)
			{
				if (item.Settings.PlugSync)
					_plugSyncList.Add(new SyncTask(item.Name,item.Source, item.Target));
			}
			UpdateListView();

            for (int i = 2; i <= 3; i++)
                lvTaskList.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
		}

        private void lvTaskList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.NewWidth < MIN_WIDTH)
            {
                e.Cancel = true;
                e.NewWidth = lvTaskList.Columns[e.ColumnIndex].Width;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lvTaskList.SelectedItems.Count == 0) return;
            int selIdx = lvTaskList.SelectedItems[0].Index;
            bool condition = (_counter <= 0) ? selIdx > 1 : selIdx > 0;
            if (condition)
            {
                for (int i = 0; i < lvTaskList.Items[selIdx].SubItems.Count; i++)
                {
                    string temp = lvTaskList.Items[selIdx - 1].SubItems[i].Text;
                    lvTaskList.Items[selIdx - 1].SubItems[i].Text = lvTaskList.Items[selIdx].SubItems[i].Text;
                    lvTaskList.Items[selIdx].SubItems[i].Text = temp;
                }
                lvTaskList.Items[selIdx - 1].Selected = true;
                lvTaskList.Items[selIdx - 1].Focused = true;
                lvTaskList.RedrawItems(selIdx - 1, selIdx, true);
                lvTaskList.Focus();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lvTaskList.SelectedItems.Count == 0) return;
            int selIdx = lvTaskList.SelectedItems[0].Index;
            bool condition = (selIdx < lvTaskList.Items.Count - 1);
            if (_counter <= 0) condition = condition && (selIdx > 0);
            if (condition)
            {
                for (int i = 0; i < lvTaskList.Items[selIdx].SubItems.Count; i++)
                {
                    string temp = lvTaskList.Items[selIdx + 1].SubItems[i].Text;
                    lvTaskList.Items[selIdx + 1].SubItems[i].Text = lvTaskList.Items[selIdx].SubItems[i].Text;
                    lvTaskList.Items[selIdx].SubItems[i].Text = temp;
                }
                lvTaskList.Items[selIdx + 1].Selected = true;
                lvTaskList.Items[selIdx + 1].Focused = true;
                lvTaskList.RedrawItems(selIdx, selIdx + 1, true);
                lvTaskList.Focus();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvTaskList.SelectedItems.Count == 0 ||
               (_counter <= 0 && lvTaskList.SelectedItems[0].Index == 0))
                return;

            _plugSyncList.Remove(new SyncTask(lvTaskList.FocusedItem.SubItems[0].Text, "", ""));
            UpdateListView();
            if (_plugSyncList.Count == 0)
            {
                timer.Stop();
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = _counter + " second(s)..";
            if (_counter-- == 0)
            {
                timer.Stop();
                lblTimer.Text = "";
                ShowProgressBar();
                StartSync();
            }
        }
	}
}