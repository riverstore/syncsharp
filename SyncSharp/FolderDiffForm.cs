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
using System.Reflection;
using SyncSharp.DataModel;
using SyncSharp.Business;


namespace SyncSharp.GUI
{
    public partial class FolderDiffForm : Form
    {
        #region attributes

        private DataView dvCompare;
        private DataTable dtCompare;
        private Detector detector;

        private string source, target;
        private int sortColumn = -1;

        // Counters for comparision statistics
        private int numOfSFilesCpy; 
        private long sFilesCpySize;
        private int numOfTFilesCpy; 
        private long tFilesCpySize;
        private int numOfSFilesDel; 
        private long sFilesDelSize;
        private int numOfTFilesDel;
        private long tFilesDelSize;
        private int numOfSFilesOW;
        private long sFilesOWSize;
        private int numOfTFilesOW;
        private long tFilesOWSize;
        private int numOfSFoldersC;
        private int numOfTFoldersC;
        private int numOfSFoldersDel;
        private int numOfTFoldersDel;

        #endregion

        #region constructors

        public FolderDiffForm(Detector detector)
        {
            InitializeComponent();

            this.detector = detector;
            this.source = detector.Source;
            this.target = detector.Target;
        }

        #endregion 

        #region methods

        public void AddDataRow(FileUnit u, SyncAction action)
        {
            string path = "";
            AddImageToList(u);
            
            DataRow row = dtCompare.NewRow();

            if (u.AbsolutePath.StartsWith(source))
            {
                path = u.AbsolutePath.Substring(source.Length);
                if (!u.IsDirectory)
                {
                    row[1] = u.Size;
                    row[2] = u.LastWriteTime;
                }
            }
            else
            {
                path = u.AbsolutePath.Substring(target.Length);
                if (!u.IsDirectory)
                {
                    row[5] = u.Size;
                    row[6] = u.LastWriteTime;
                }
            }

            if (u.Match != null)
            {
               row[5] = u.Match.Size;
               row[6] = u.Match.LastWriteTime;
            }

            row[0] = path;
            row[3] = action;
            row[4] = path;
            row[7] = u.Extension;
            row[8] = u;

            dtCompare.Rows.Add(row);
        }

        private void AddImageToList(FileUnit u)
        {
            if (!imageList.Images.ContainsKey(u.Extension))
                imageList.Images.Add(u.Extension,
                   ShellIcon.GetSmallIcon(u.AbsolutePath).ToBitmap());
        }

        private void PopulateListView()
        {
            foreach (FileUnit u in detector.ConflictFilesList)
            {
                SyncAction action;
                if (u.Match != null)
                    action = Reconciler.chkFileUpdate(u, u.Match, true, true);
                else
                {
                    if (u.AbsolutePath.StartsWith(source))
                        action = Reconciler.chkFileUpdate(u, null, true, true);
                    else
                        action = Reconciler.chkFileUpdate(null, u, true, true);
                }

                if (action == SyncAction.CopyFileToSource)
                {
                    numOfTFilesCpy++;
                    if (u.Match != null)
                    {
                        numOfSFilesOW++;
                        sFilesOWSize += u.Size;
                        tFilesCpySize += u.Match.Size;
                    }
                    else
                        tFilesCpySize += u.Size;
                }
                else if (action == SyncAction.CopyFileToTarget)
                {
                    numOfSFilesCpy++;
                    if (u.Match != null)
                    {
                        numOfTFilesOW++;
                        tFilesOWSize += u.Match.Size;
                        sFilesCpySize += u.Size;
                    }
                    else
                        sFilesCpySize += u.Size;
                }
                AddDataRow(u, action);
            }

            foreach (FileUnit u in detector.NewSourceFilesList)
            {
                if (u.IsDirectory)
                    numOfSFoldersC++;
                else
                {
                    numOfSFilesCpy++;
                    sFilesCpySize += u.Size;
                }
                AddDataRow(u, SyncAction.CopyFileToTarget);
            }

            foreach (FileUnit u in detector.NewTargetFilesList)
            {
                if (u.IsDirectory)
                    numOfTFoldersC++;
                else
                {
                    numOfTFilesCpy++;
                    tFilesCpySize += u.Size;
                }
                AddDataRow(u, SyncAction.CopyFileToSource);
            }

            foreach (FileUnit u in detector.DeleteSourceFilesList)
            {
                if (u.IsDirectory)
                    numOfSFoldersDel++;
                else
                {
                    numOfSFilesDel++;
                    sFilesDelSize += u.Size;
                }
                sFilesOWSize += u.Size;
                AddDataRow(u, SyncAction.DeleteSourceFile);
            }

            foreach (FileUnit u in detector.DeleteTargetFilesList)
            {
                if (u.IsDirectory)
                    numOfTFoldersDel++;
                else
                {
                    numOfTFilesDel++;
                    tFilesDelSize += u.Size;
                }
                AddDataRow(u, SyncAction.DeleteTargetFile);
            }
        }

        private DataTable createDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Source", typeof(string));
            dt.Columns.Add("SourceSize", typeof(long));
            dt.Columns.Add("SourceDate", typeof(DateTime));
            dt.Columns.Add("SyncAction", typeof(SyncAction));
            dt.Columns.Add("Target", typeof(string));
            dt.Columns.Add("TargetSize", typeof(long));
            dt.Columns.Add("TargetDate", typeof(DateTime));
            dt.Columns.Add("Extension", typeof(string));
            dt.Columns.Add("FileUnit", typeof(FileUnit));
            
            return dt;
        }

        private ListViewItem createListViewItem(int idx)
        {
            ListViewItem item;

            int imageIdx = imageList.Images.IndexOfKey(
                dvCompare[idx][7].ToString());

            item = new ListViewItem(dvCompare[idx][0].ToString(), imageIdx);
           
            for (int i = 1; i < lvCompare.Columns.Count; i++)
                 item.SubItems.Add(dvCompare[idx][i].ToString());

            switch ((SyncAction)dvCompare[idx][3])
            {
                case SyncAction.CopyFileToSource:
                    item.SubItems[3].Text = SyncAction.CopyFileToSource.Text();
                    break;
                case SyncAction.CopyFileToTarget:
                    item.SubItems[3].Text = SyncAction.CopyFileToTarget.Text();
                    break;
                case SyncAction.DeleteBothFile:
                    item.SubItems[3].Text = SyncAction.DeleteBothFile.Text();
                    break;
                case SyncAction.DeleteSourceFile:
                    item.SubItems[3].Text = SyncAction.DeleteSourceFile.Text();
                    break;
                case SyncAction.DeleteTargetFile:
                    item.SubItems[3].Text = SyncAction.DeleteTargetFile.Text();
                    break;
                case SyncAction.SkipNExclude:
                    item.SubItems[3].Text = SyncAction.SkipNExclude.Text();
                    break;
               default:
                    item.SubItems[3].Text = SyncAction.CollisionPromptUser.Text();
                    break;
            }

            FileUnit cur = (FileUnit)dvCompare[idx][8];

            if (cur.Match == null)
            {
                if (cur.AbsolutePath.StartsWith(source))
                {
                    item.SubItems[4].ForeColor = Color.Silver;
                    item.SubItems[0].BackColor = Color.FromArgb(200,255,200);
                }
                else
                {
                    item.SubItems[0].ForeColor = Color.Silver;
                    item.SubItems[4].BackColor = Color.FromArgb(200, 255, 200);
                }
            }
            else
            {
                item.SubItems[0].BackColor = Color.FromArgb(255, 220, 220);
                item.SubItems[4].BackColor = Color.FromArgb(255, 220, 220);
            }

            item.ToolTipText = dvCompare[idx][0].ToString();

            return item;
        }

        private void ResetActionList(bool isVisible)
        {
            this.deleteMenuItem.Visible = isVisible;
            this.delFrmSourceMenuItem.Visible = isVisible;
            this.delFrmTargetMenuItem.Visible = isVisible;
            this.copyToSourceMenuItem.Visible = isVisible;
            this.copyToTargetMenuItem.Visible = isVisible;
            this.excludeMenuItem.Visible = isVisible;
            this.collisionMenuItem.Visible = isVisible;
        }

        private void ChangeSyncAction(SyncAction action)
        {
            if (lvCompare.FocusedItem == null) return;

            int idx = lvCompare.FocusedItem.Index;
            dvCompare[idx][3] = action;
            lvCompare.Invalidate(lvCompare.FocusedItem.SubItems[3].Bounds);
        }

        private void UpdateStatisticsView()
        {
            lblSourceCpy.Text = (numOfSFilesCpy == 0) ? "None" : 
                "" + numOfSFilesCpy + " [" + sFilesCpySize + " bytes]";
            lblTargetCpy.Text = (numOfTFilesCpy == 0) ? "None" : 
                "" + numOfTFilesCpy + " [" + tFilesCpySize + " bytes]";
            lblSourceDel.Text = (numOfSFilesDel == 0) ? "None" :
                "" + numOfSFilesDel + " [" + sFilesDelSize + " bytes]";
            lblTargetDel.Text = (numOfTFilesDel == 0) ? "None" :
                "" + numOfTFilesDel + " [" + tFilesDelSize + " bytes]";
            lblSourceOW.Text = (numOfSFilesOW == 0) ? "None" :
                "" + numOfSFilesOW + " [" + sFilesOWSize + " bytes]";
            lblTargetOW.Text = (numOfTFilesOW == 0) ? "None" :
                "" + numOfTFilesOW + " [" + tFilesOWSize + " bytes]";
            lblSourceCreate.Text = (numOfSFoldersC == 0) ? "None" : 
                "" + numOfSFoldersC;
            lblTargetCreate.Text = (numOfTFoldersC == 0) ? "None" : 
                "" + numOfTFoldersC;
            lblSourceRemove.Text = (numOfSFoldersDel == 0) ? "None" :
                "" + numOfSFoldersDel;
            lblTargetRemove.Text = (numOfTFoldersDel == 0) ? "None" :
                "" + numOfTFoldersDel;

            lblSourceTotal.Text = numOfSFilesCpy + numOfSFilesDel +
                numOfSFilesOW + numOfSFoldersC + numOfSFoldersDel + " [" +
                (sFilesCpySize + sFilesDelSize + sFilesOWSize) + " bytes]";

            lblTargetTotal.Text = numOfTFilesCpy + numOfTFilesDel +
                numOfTFilesOW + numOfTFoldersC + numOfTFoldersDel + " [" +
                (tFilesCpySize + tFilesDelSize + tFilesOWSize) + " bytes]";
        }

        private void setChildSyncAction(SyncAction action)
        {
            if (lvCompare.FocusedItem == null) return;

            int idx = lvCompare.FocusedItem.Index;

            if (dvCompare[idx][7].ToString().Equals("dir"))
            {
                String parent = ((FileUnit)dvCompare[idx][8]).AbsolutePath;

                foreach (DataRowView r in dvCompare)
                {
                    FileUnit cur = (FileUnit)r[8];
                    if (cur.AbsolutePath.StartsWith(parent))
                        r[3] = action;
                }
            }
            else
                dvCompare[idx][3] = action;

            lvCompare.Invalidate();
        }

        #endregion

        #region events handling

        private void lvCompare_DrawColumnHeader(object sender, 
                        DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvCompare_DrawSubItem(object sender, 
                                    DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.DrawDefault = false;
                e.DrawBackground();

                Color txtColor;

                if (e.Item.Selected && lvCompare.Focused)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.SubItem.Bounds);
                    txtColor = Color.White;
                }
                else
                {
                    e.Graphics.FillRectangle(SystemBrushes.Window, e.SubItem.Bounds);
                    txtColor = e.SubItem.ForeColor;
                }

                Image syncIcon = null;

                if (e.SubItem.Text.Equals(SyncAction.CopyFileToSource.Text()))
                    syncIcon = Properties.Resources.left_copy;
                else if (e.SubItem.Text.Equals(SyncAction.CopyFileToTarget.Text()))
                    syncIcon = Properties.Resources.right_copy;
                else if (e.SubItem.Text.Equals(SyncAction.CollisionPromptUser.Text()))
                    syncIcon = Properties.Resources.prompt;
                else if (e.SubItem.Text.Equals(SyncAction.SkipNExclude.Text()))
                    syncIcon = Properties.Resources.exclude;
                else if (e.SubItem.Text.Equals(SyncAction.DeleteBothFile.Text()))
                    syncIcon = Properties.Resources.remove;
                else if (e.SubItem.Text.Equals(SyncAction.DeleteSourceFile.Text()))
                    syncIcon = Properties.Resources.delete_left;
                else
                    syncIcon = Properties.Resources.delete_right;

                e.Graphics.DrawImage(syncIcon, e.SubItem.Bounds.X, 
                    e.SubItem.Bounds.Y, syncIcon.Width, syncIcon.Height);

                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font,
                    new SolidBrush(txtColor), e.SubItem.Bounds.Location.X
                    + syncIcon.Width, e.SubItem.Bounds.Location.Y);
            }
            else
                e.DrawDefault = true;

        }

        private void lvCompare_RetrieveVirtualItem(object sender, 
                            RetrieveVirtualItemEventArgs e)
        {
            e.Item = createListViewItem(e.ItemIndex);
        }

        private void lvCompare_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
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
            dvCompare.Sort =
                dtCompare.Columns[e.Column].ColumnName + " " + order;
            lvCompare.Invalidate();
        }

        private void FolderDiffForm_Load(object sender, EventArgs e)
        {
            dtCompare = createDataTable();

            PopulateListView();
            dvCompare = dtCompare.DefaultView;
            lvCompare.VirtualListSize = dvCompare.Count;

            UpdateStatisticsView();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectAllMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvCompare.VirtualListSize; i++)
                lvCompare.Items[i].Selected = true;
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            if (!lvCompare.FocusedItem.Selected) return;

            int idx = lvCompare.FocusedItem.Index;
            string path = ((FileUnit)dvCompare[idx][8]).AbsolutePath;

            System.Diagnostics.Process.Start(path);
        }

        private void propertiesMenuItem_Click(object sender, EventArgs e)
        {
            if (!lvCompare.FocusedItem.Selected) return;

            int idx = lvCompare.FocusedItem.Index;
            string msg = "";

            FileUnit cur = (FileUnit)dvCompare[idx][8];

            if (cur.Match == null)
            {
                if (cur.AbsolutePath.StartsWith(source))
                {
                    msg += "Source: " + lvCompare.FocusedItem.SubItems[0].Text + "\n"
                    + "Target: Does not exist\n\nSource filesize: " +
                    lvCompare.FocusedItem.SubItems[1].Text +
                    " bytes \nSource date & time: " +
                    lvCompare.FocusedItem.SubItems[2].Text + "\n";

                    if (cur.IsDirectory)
                    {
                       msg = "Source: " + lvCompare.FocusedItem.SubItems[0].Text + 
                           "\nTarget: Does not exist\n";
                    }
                }
                else
                {
                    msg += "Source: Does not exist\n" +
                    "Target: " + lvCompare.FocusedItem.SubItems[0].Text + "\n\nTarget filesize: " +
                    lvCompare.FocusedItem.SubItems[5].Text +
                    " bytes \nTarget date & time: " +
                    lvCompare.FocusedItem.SubItems[6].Text + "\n";

                    if (cur.IsDirectory)
                    {
                      msg += "Source: Does not exist\n" +
                        "Target: " + lvCompare.FocusedItem.SubItems[0].Text + "\n";
                    }
                }
            }
            else
            {
                msg += "Source & Target: " + lvCompare.FocusedItem.SubItems[0].Text
                + "\n\nSource filesize: " +
                lvCompare.FocusedItem.SubItems[1].Text +
                " bytes \nTarget filesize: " +
                lvCompare.FocusedItem.SubItems[5].Text +
                " bytes \nSource date & time: " +
                lvCompare.FocusedItem.SubItems[2].Text +
                " \nTarget date & time: " +
                lvCompare.FocusedItem.SubItems[6].Text + "\n";
            }

            MessageBox.Show(msg, "Information", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void lvCompare_DoubleClick(object sender, EventArgs e)
        {
            propertiesMenuItem_Click(sender, e);
        }

        private void lvMenu_Opening(object sender, CancelEventArgs e)
        {
            ResetActionList(false);

            int idx = lvCompare.FocusedItem.Index;
            FileUnit cur = (FileUnit)dvCompare[idx][8];

            if (cur.Match == null)
            {
                if (cur.AbsolutePath.StartsWith(source))
                {
                    this.copyToTargetMenuItem.Visible = true;
                    this.delFrmSourceMenuItem.Visible = true;
                }
                else
                {
                    this.copyToSourceMenuItem.Visible = true;
                    this.delFrmTargetMenuItem.Visible = true;
                }
                this.excludeMenuItem.Visible = true;
                //this.collisionMenuItem.Visible = true;
            }
            else
                ResetActionList(true);
            
            this.collisionMenuItem.Visible = false;
        }

        private void copyToSourceMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSyncAction(SyncAction.CopyFileToSource);   
        }

        private void copyToTargetMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSyncAction(SyncAction.CopyFileToTarget);
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSyncAction(SyncAction.DeleteBothFile);
        }

        private void delSourceMenuItem_Click(object sender, EventArgs e)
        {
            setChildSyncAction(SyncAction.DeleteSourceFile);
        }

        private void delTargetMenuItem_Click(object sender, EventArgs e)
        {
           setChildSyncAction(SyncAction.DeleteTargetFile);
        }

        private void excludeMenuItem_Click(object sender, EventArgs e)
        {
            setChildSyncAction(SyncAction.SkipNExclude);
        }

        private void collisionMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSyncAction(SyncAction.CollisionPromptUser);
        }

        private void btnSynchronize_Click(object sender, EventArgs e)
        {
            detector.NewSourceFilesList.Clear();
            detector.NewTargetFilesList.Clear();
            detector.DeleteSourceFilesList.Clear();
            detector.DeleteTargetFilesList.Clear();
            detector.ConflictFilesList.Clear();

            foreach (DataRow row in dtCompare.Rows)
            {
                FileUnit cur = (FileUnit)row[8];
                if ((SyncAction)row[3] == SyncAction.CopyFileToSource)
                {
                    if (cur.Match != null)
                        detector.NewTargetFilesList.Add(cur.Match);
                    else
                        detector.NewTargetFilesList.Add(cur);
                }
                else if ((SyncAction)row[3] == SyncAction.CopyFileToTarget)
                {
                    detector.NewSourceFilesList.Add(cur);
                }
                else if ((SyncAction)row[3] == SyncAction.DeleteSourceFile)
                {
                    detector.DeleteSourceFilesList.Add(cur);
                }
                else if ((SyncAction)row[3] == SyncAction.DeleteTargetFile)
                {
                    if (cur.Match != null)
                        detector.DeleteTargetFilesList.Add(cur.Match);
                    else
                        detector.DeleteTargetFilesList.Add(cur);
                }
                else if ((SyncAction)row[3] == SyncAction.DeleteBothFile)
                {
                    detector.DeleteSourceFilesList.Add(cur);
                    detector.DeleteTargetFilesList.Add(cur.Match);
                }
            }
        }

        #endregion
    }
}