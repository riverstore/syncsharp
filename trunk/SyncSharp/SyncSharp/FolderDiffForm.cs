using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Business;


namespace SyncSharp
{
    public partial class FolderDiffForm : Form
    {
        private DataView _itemView;
        private DataTable _lvTable;
        private Detector _detector;

        private string _source, _target;
        private int _sortColumn = -1;

        public FolderDiffForm(string source, string target)
        {
            InitializeComponent();
            _source = source;
            _target = target;
        }

        enum SyncAction
        {
            CopyToSource,       // copy file to source
            CopyToTarget,       // copy file to target
            PromptUser,         // conflict occurs, ask user what to do
            Exclude,            // exclude file/folder to sync
            Delete,             // delete from both source & target
        }

        private void AddImageToList(FileUnit u)
        {
            if (!imageList.Images.ContainsKey(u.Extension))
                imageList.Images.Add(u.Extension,
                   ShellIcon.GetSmallIcon(u.AbsolutePath).ToBitmap());
        }

        private void AddNotInTargetItem(FileUnit s)
        {
            string path = s.AbsolutePath.Substring(_source.Length);

            AddImageToList(s);

            DataRow row = _lvTable.NewRow();
            row[0] = path;
            if(!s.IsDirectory) row[1] = s.Size;
            row[2] = s.Time;
            row[3] = "Copy to target";
            row[4] = path;
            row[7] = s.Extension;
            row[8] = SyncAction.CopyToTarget.ToString();

            _lvTable.Rows.Add(row);
        }

        private void AddNotInSourceItem(FileUnit t)
        {
            string path = t.AbsolutePath.Substring(_target.Length);

            AddImageToList(t);

            DataRow row = _lvTable.NewRow();
            row[0] = path;
            row[3] = "Copy to source";
            row[4] = path;
            if (!t.IsDirectory) row[5] = t.Size;
            row[6] = t.Time;
            row[7] = t.Extension;
            row[8] = SyncAction.CopyToSource.ToString();

            _lvTable.Rows.Add(row);
        }

        private void AddChangedItem(FileUnit u)
        {
            string path = u.AbsolutePath.Substring(_source.Length);

            DataRow row = _lvTable.NewRow();

            AddImageToList(u);

            row[0] = path;
            row[1] = u.Size;
            row[2] = u.Time;
            row[3] = "Conflict, prompt me";
            row[4] = path;
            row[5] = u.Match.Size;
            row[6] = u.Match.Time;
            row[7] = u.Extension;
            row[8] = SyncAction.PromptUser.ToString();

            _lvTable.Rows.Add(row);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvCompare.VirtualListSize = 0;
            _lvTable.Clear();

            if (_itemView != null) _itemView.Sort = "";
        }

        private void PopulateListView()
        {
            foreach (FileUnit u in _detector.MatchedFiles)
            {
                if (u.Size != u.Match.Size || 
                    u.Time != u.Match.Time)
                    AddChangedItem(u);
            }

            foreach (FileUnit u in _detector.UnMatchedSourceFiles)
                AddNotInTargetItem(u);

            foreach (FileUnit u in _detector.UnMatchedSourceDirs)
                AddNotInTargetItem(u);

            foreach (FileUnit u in _detector.UnMatchedTargetFiles)
                AddNotInSourceItem(u);

            foreach (FileUnit u in _detector.UnMatchedTargetDirs)
                AddNotInSourceItem(u);

        }

        private DataTable createDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Source", typeof(string));
            table.Columns.Add("SourceSize", typeof(long));
            table.Columns.Add("SourceDate", typeof(DateTime));
            table.Columns.Add("SyncAction", typeof(string));
            table.Columns.Add("Target", typeof(string));
            table.Columns.Add("TargetSize", typeof(long));
            table.Columns.Add("TargetDate", typeof(DateTime));
            table.Columns.Add("Extension", typeof(string));
            table.Columns.Add("Tag", typeof(string));
            
            return table;
        }

        private ListViewItem createListViewItem(int idx)
        {
            ListViewItem item;

            int imageIdx = imageList.Images.IndexOfKey(
                _itemView[idx][7].ToString());

            item = new ListViewItem(_itemView[idx][0].ToString(), imageIdx);
           
            for (int i = 1; i < 7; i++)
                 item.SubItems.Add(_itemView[idx][i].ToString());

            if (String.Equals(_itemView[idx][8].ToString(),
                SyncAction.CopyToSource.ToString()))
            {
                item.SubItems[0].ForeColor = Color.Silver;
                item.SubItems[4].BackColor = Color.PaleGreen;
            }
            else if (String.Equals(_itemView[idx][8].ToString(),
                SyncAction.CopyToTarget.ToString()))
            {
                item.SubItems[4].ForeColor = Color.Silver;
                item.SubItems[0].BackColor = Color.PaleGreen;
            }
            else
            {
                item.SubItems[0].BackColor = Color.LightPink;
                item.SubItems[4].BackColor = Color.LightPink;
            }

            item.ToolTipText = _itemView[idx][0].ToString();

            return item;
        }

        private void lvDiff_DrawColumnHeader(object sender, 
                        DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvDiff_DrawSubItem(object sender, 
                                    DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.DrawDefault = false;
                e.DrawBackground();

                if (e.Item.Selected)
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.SubItem.Bounds);

                Color txtColor = (e.Item.Selected) ? Color.White : e.SubItem.ForeColor;

                Image img = null;
                if (e.SubItem.Text.Equals("Copy to source"))
                    img = Properties.Resources.left_copy;
                else if (e.SubItem.Text.Equals("Copy to target"))
                    img = Properties.Resources.right_copy;
                else
                    img = Properties.Resources.error_small;

                e.Graphics.DrawImage(img, e.SubItem.Bounds.X, 
                    e.SubItem.Bounds.Y, img.Width, img.Height);

                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font,
                    new SolidBrush(txtColor), e.SubItem.Bounds.Location.X
                    + img.Width, e.SubItem.Bounds.Location.Y);
            }
            else
                e.DrawDefault = true;

        }

        private void lvDiff_RetrieveVirtualItem(object sender, 
                            RetrieveVirtualItemEventArgs e)
        {
            e.Item = createListViewItem(e.ItemIndex);
        }

        private void lvDiff_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                lvCompare.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvCompare.Sorting == SortOrder.Ascending)
                    lvCompare.Sorting = SortOrder.Descending;
                else
                    lvCompare.Sorting = SortOrder.Ascending;
            }

            string order = (lvCompare.Sorting == SortOrder.Ascending) ? "ASC" : "DESC";
            _itemView.Sort =
                _lvTable.Columns[e.Column].ColumnName + " " + order;

            lvCompare.Invalidate();
        }

        private void FolderDiffForm_Load(object sender, EventArgs e)
        {
            _lvTable = createDataTable();

            imageList.ColorDepth = ColorDepth.Depth32Bit;
            lvCompare.SmallImageList = imageList;
            lvCompare.ShowItemToolTips = true;

            _detector = new Detector();
            _detector.CompareFolderPair(_source, _target);
               
             PopulateListView();

             _itemView = _lvTable.DefaultView;
             lvCompare.VirtualListSize = _itemView.Count;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}