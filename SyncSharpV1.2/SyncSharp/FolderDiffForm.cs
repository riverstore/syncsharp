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
using SyncSharp.Storage;


namespace SyncSharp.GUI
{
	public partial class FolderDiffForm : Form
	{
		#region attributes

        private const int MIN_WIDTH = 50;

		private DataView _dvCompare;
		private DataTable _dtCompare;
        
        private CustomDictionary<string, string, PreviewUnit> _previewFilesList;
        private CustomDictionary<string, string, PreviewUnit> _previewFoldersList;

		private string _source, _target;
		private int _sortColumn;

		// Counters for comparision statistics
		private int _numOfSFilesCpy;
		private long _sFilesCpySize;
		private int _numOfTFilesCpy;
		private long _tFilesCpySize;
		private int _numOfSFilesDel;
		private long _sFilesDelSize;
		private int _numOfTFilesDel;
		private long _tFilesDelSize;
		private int _numOfSFilesOW;
		private long _sFilesOWSize;
		private int _numOfTFilesOW;
		private long _tFilesOWSize;
		private int _numOfSFoldersC;
		private int _numOfTFoldersC;
		private int _numOfSFoldersDel;
		private int _numOfTFoldersDel;

		#endregion

		#region constructors

        public FolderDiffForm(CustomDictionary<string,string, PreviewUnit> previewFilesList,
                              CustomDictionary<string,string, PreviewUnit> previewFoldersList, 
                              SyncTask task)
        {
            InitializeComponent();
            _previewFilesList = previewFilesList;
            _previewFoldersList = previewFoldersList;
            _source = task.Source;
            _target = task.Target;
            _sortColumn = -1;
            lblName.Text = task.Name;
            lblSource.Text = _source;
            lblTarget.Text = _target;
            this.Text = "[" + lblName.Text + "] Synchronization Preview";
        }

		#endregion

		#region methods

		public void AddDataRow(FileUnit u, SyncAction action)
		{
			AddImageToList(u);
			DataRow row = _dtCompare.NewRow();
			
            if (u.AbsolutePath.StartsWith(_source))
			{
				row[0] = u.AbsolutePath.Substring(_source.Length);
                row[4] = u.MatchingPath;
				if (!u.IsDirectory)
				{
					row[1] = u.Size;
					row[2] = u.LastWriteTime;
				}
				if (u.Match != null)
				{
					row[5] = u.Match.Size;
					row[6] = u.Match.LastWriteTime;
				}
			}
			else
			{
				row[4] = u.AbsolutePath.Substring(_target.Length);
                row[0] = u.MatchingPath;
				if (!u.IsDirectory)
				{
					row[5] = u.Size;
					row[6] = u.LastWriteTime;
				}
			}

			row[3] = row[9] = action;
			row[7] = u.Extension;
			row[8] = u;
			_dtCompare.Rows.Add(row);
		}

		private void AddImageToList(FileUnit u)
		{
			if (!imageList.Images.ContainsKey(u.Extension))
				imageList.Images.Add(u.Extension, ShellIcon.GetSmallIcon(u.AbsolutePath).ToBitmap());
		}

		private void PopulateListView()
		{
            ReadFromPreviewList(_previewFilesList);
            ReadFromPreviewList(_previewFoldersList);
		}

        private void ReadFromPreviewList(CustomDictionary<string, string, PreviewUnit> previewList)
        {
            foreach (var item in previewList.PriSub)
            {
                string srcRelativePath = item.Key;
                string tgtRelativePath = item.Value;
                PreviewUnit unit = previewList.getByPrimary(srcRelativePath);

                if (unit.sAction != SyncAction.NoAction)
                {
                    FileUnit u = null;
                    if (!unit.srcFlag.Equals("D"))
                    {
                        u = new FileUnit(_source + srcRelativePath);
                        u.MatchingPath = tgtRelativePath;

                        if (!unit.tgtFlag.Equals("D"))
                            u.Match = new FileUnit(_target + tgtRelativePath);
                    }
                    else
                    {
                        if (!unit.tgtFlag.Equals("D"))
                        {
                            u = new FileUnit(_target + tgtRelativePath);
                            u.MatchingPath = srcRelativePath;
                        }
                    }
                    if (u == null) continue;
                    ComputeStatisticsResult(u, unit.sAction);
                    AddDataRow(u, unit.sAction);
                }
            }
        }

        private void ComputeStatisticsResult(FileUnit u, SyncAction action)
        {
            switch (action)
            {
                case SyncAction.CopyFileToSource:
                    _numOfTFilesCpy++;
                    if (u.Match != null)
                    {
                        _tFilesCpySize += u.Match.Size;
                        _numOfSFilesOW++;
                        _sFilesOWSize += u.Size;
                    }
                    else
                        _tFilesCpySize += u.Size;
                    
                    break;
                
                case SyncAction.CopyFileToTarget:
                    _numOfSFilesCpy++;
                    _sFilesCpySize += u.Size;
                    if (u.Match != null)
                    {
                        _numOfTFilesOW++;
                        _tFilesOWSize += u.Match.Size;
                    }
                    break;
                
                case SyncAction.DeleteSourceFile:
                    _numOfSFilesDel++;
                    _sFilesDelSize += u.Size;
                    break;
                
                case SyncAction.DeleteTargetFile:
                    _numOfTFilesDel++;
                    _tFilesDelSize += u.Size;
                    break;
                
                case SyncAction.KeepBothCopies:
                    _numOfSFilesCpy++;
                    _numOfTFilesCpy++;
                    _sFilesCpySize += u.Size;
                    _tFilesCpySize += u.Match.Size;
                    break;
                
                case SyncAction.CreateSourceDir:
                    _numOfSFoldersC++;
                    break;
                
                case SyncAction.CreateTargetDir:
                    _numOfTFoldersC++;
                    break;
                
                case SyncAction.DeleteBothDir:
                    _numOfSFoldersDel++;
                    _numOfTFoldersDel++;
                    break;
                
                case SyncAction.DeleteSourceDir:
                    _numOfSFoldersDel++;
                    break;
                
                case SyncAction.DeleteTargetDir:
                    _numOfTFoldersDel++;
                    break;
            }
        }

		private DataTable CreateDataTable()
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
            dt.Columns.Add("DefaultSync", typeof(SyncAction));
			return dt;
		}

		private ListViewItem CreateListViewItem(int idx)
		{
			ListViewItem item;
			int imageIdx = imageList.Images.IndexOfKey(_dvCompare[idx][7].ToString());
			item = new ListViewItem(_dvCompare[idx][0].ToString(), imageIdx);
			
            for (int i = 1; i < lvCompare.Columns.Count; i++)
				item.SubItems.Add(_dvCompare[idx][i].ToString());

            item.SubItems[3].Text = ((SyncAction)_dvCompare[idx][3]).Text();

			FileUnit cur = (FileUnit)_dvCompare[idx][8];

			if (cur.Match == null)
			{
				if (cur.AbsolutePath.StartsWith(_source))
				{
					item.SubItems[4].ForeColor = Color.Silver;
					item.SubItems[0].BackColor = Color.FromArgb(200, 255, 200);
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

			item.ToolTipText = _dvCompare[idx][0].ToString();

			return item;
		}

		private void ResetActionList(bool isVisible)
		{
			this.deleteMenuItem.Visible = isVisible;
			this.delFrmSourceMenuItem.Visible = isVisible;
			this.delFrmTargetMenuItem.Visible = isVisible;
			this.copyToSourceMenuItem.Visible = isVisible;
			this.copyToTargetMenuItem.Visible = isVisible;
			this.keepBothMenuItem.Visible = isVisible;
			this.createSourceMenuItem.Visible = isVisible;
			this.createTargetMenuItem.Visible = isVisible;
            this.renameSrcMenuItem.Visible = isVisible;
            this.renameTgtMenuItem.Visible = isVisible;
		}

		private void ChangeSyncAction(SyncAction action)
		{
            if (!lvCompare.FocusedItem.Selected) return;
			int idx = lvCompare.FocusedItem.Index;
			_dvCompare[idx][3] = action;

            UpdatePreviewList(idx, action);

			lvCompare.Invalidate(lvCompare.FocusedItem.SubItems[3].Bounds);
		}

		private void UpdateStatisticsView()
		{
			lblSourceCpy.Text = (_numOfSFilesCpy == 0) ? "None" :
					"" + _numOfSFilesCpy + " [" + Utility.FormatSize(_sFilesCpySize) + "]";
			lblTargetCpy.Text = (_numOfTFilesCpy == 0) ? "None" :
					"" + _numOfTFilesCpy + " [" + Utility.FormatSize(_tFilesCpySize) + "]";
			lblSourceDel.Text = (_numOfSFilesDel == 0) ? "None" :
					"" + _numOfSFilesDel + " [" + Utility.FormatSize(_sFilesDelSize) + "]";
			lblTargetDel.Text = (_numOfTFilesDel == 0) ? "None" :
					"" + _numOfTFilesDel + " [" + Utility.FormatSize(_tFilesDelSize) + "]";
			lblSourceOW.Text = (_numOfSFilesOW == 0) ? "None" :
					"" + _numOfSFilesOW + " [" + Utility.FormatSize(_sFilesOWSize) + "]";
			lblTargetOW.Text = (_numOfTFilesOW == 0) ? "None" :
					"" + _numOfTFilesOW + " [" + Utility.FormatSize(_tFilesOWSize) + "]";
			lblSourceCreate.Text = (_numOfSFoldersC == 0) ? "None" :
					"" + _numOfSFoldersC;
			lblTargetCreate.Text = (_numOfTFoldersC == 0) ? "None" :
					"" + _numOfTFoldersC;
			lblSourceRemove.Text = (_numOfSFoldersDel == 0) ? "None" :
					"" + _numOfSFoldersDel;
			lblTargetRemove.Text = (_numOfTFoldersDel == 0) ? "None" :
					"" + _numOfTFoldersDel;
			lblSourceTotal.Text = _numOfSFilesCpy + _numOfSFilesDel +
					_numOfSFilesOW + _numOfSFoldersC + _numOfSFoldersDel + " [" +
					Utility.FormatSize(_sFilesCpySize + _sFilesDelSize + _sFilesOWSize) + "]";
			lblTargetTotal.Text = _numOfTFilesCpy + _numOfTFilesDel +
					_numOfTFilesOW + _numOfTFoldersC + _numOfTFoldersDel + " [" +
					Utility.FormatSize(_tFilesCpySize + _tFilesDelSize + _tFilesOWSize) + "]";
		}

        private void UpdatePreviewList(int idx, SyncAction action)
        {
            string path = _dvCompare[idx][0].ToString();
            FileUnit cur = (FileUnit)_dvCompare[idx][8];

            if (cur.IsDirectory)
                _previewFoldersList.getByPrimary(path).sAction = action;
            else
                _previewFilesList.getByPrimary(path).sAction = action;
        }

		private void SetChildSyncAction(SyncAction action)
		{
            if (!lvCompare.FocusedItem.Selected) return;
			int idx = lvCompare.FocusedItem.Index;
            if (_dvCompare[idx][7].ToString().Equals("dir"))
            {
                String parent = ((FileUnit)_dvCompare[idx][8]).AbsolutePath;

                foreach (DataRowView r in _dvCompare)
                {
                    FileUnit cur = (FileUnit)r[8];
                    if (cur.AbsolutePath.StartsWith(parent))
                    {
                        r[3] = action;
                        UpdatePreviewList(idx, action);
                    }
                }
            }
            else
            {
                _dvCompare[idx][3] = action;
                UpdatePreviewList(idx, action);
            }

			lvCompare.Invalidate();
		}

		#endregion

		#region events handling

		private void lvCompare_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void lvCompare_DrawSubItem(object sender,DrawListViewSubItemEventArgs e)
		{
			if (e.ColumnIndex == 3)
			{
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
                if (e.SubItem.Text.Equals(SyncAction.CopyFileToSource.Text()) ||
                        e.SubItem.Text.Equals(SyncAction.CreateSourceDir.Text()))
                    syncIcon = Properties.Resources.left_copy;
                else if (e.SubItem.Text.Equals(SyncAction.CopyFileToTarget.Text()) ||
                        e.SubItem.Text.Equals(SyncAction.CreateTargetDir.Text()))
                    syncIcon = Properties.Resources.right_copy;
                else if (e.SubItem.Text.Equals(SyncAction.DeleteBothFile.Text()))
                    syncIcon = Properties.Resources.remove;
                else if (e.SubItem.Text.Equals(SyncAction.DeleteSourceFile.Text()))
                    syncIcon = Properties.Resources.delete_left;
                else if (e.SubItem.Text.Equals(SyncAction.DeleteTargetFile.Text()))
                    syncIcon = Properties.Resources.delete_right;
                else if (e.SubItem.Text.Equals(SyncAction.Skip.Text()))
                    syncIcon = Properties.Resources.skip;
                else if (e.SubItem.Text.Equals(SyncAction.KeepBothCopies.Text()))
                    syncIcon = Properties.Resources.KeepBoth;
                else if (e.SubItem.Text.Equals(SyncAction.RenameSourceFile.Text()) ||
                        e.SubItem.Text.Equals(SyncAction.RenameTargetFile.Text()))
                    syncIcon = Properties.Resources.rename;

				e.Graphics.DrawImage(syncIcon, e.SubItem.Bounds.X,
						e.SubItem.Bounds.Y, syncIcon.Width, syncIcon.Height);
				e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font,
						new SolidBrush(txtColor), e.SubItem.Bounds.Location.X
						+ syncIcon.Width, e.SubItem.Bounds.Location.Y);
			}
			else
				e.DrawDefault = true;
		}

		private void lvCompare_RetrieveVirtualItem(object sender,RetrieveVirtualItemEventArgs e)
		{
			e.Item = CreateListViewItem(e.ItemIndex);
		}

		private void lvCompare_ColumnClick(object sender, ColumnClickEventArgs e)
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
			_dvCompare.Sort =
					_dtCompare.Columns[e.Column].ColumnName + " " + order;
			lvCompare.Invalidate();
		}

		private void FolderDiffForm_Load(object sender, EventArgs e)
		{
			_dtCompare = CreateDataTable();
			PopulateListView();
			_dvCompare = _dtCompare.DefaultView;
			lvCompare.VirtualListSize = _dvCompare.Count;
			_dvCompare.Sort = _dtCompare.Columns[0].ColumnName + " ASC";
			UpdateStatisticsView();
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void openFolderMenuItem_Click(object sender, EventArgs e)
		{
            OpenSelectedItem(true);
		}

		private void openMenuItem_Click(object sender, EventArgs e)
		{
            OpenSelectedItem(false);
		}

        private void OpenSelectedItem(bool isOpenFolder)
        {
            if (!lvCompare.FocusedItem.Selected) return;
            int idx = lvCompare.FocusedItem.Index;
            FileUnit cur = ((FileUnit)_dvCompare[idx][8]);
            string path = (isOpenFolder) ? Directory.GetParent(cur.AbsolutePath).FullName
                                         : cur.AbsolutePath;
            System.Diagnostics.Process.Start(path);

            if (cur.Match != null) 
            {
                string matchPath = (isOpenFolder) ? Directory.GetParent(cur.Match.AbsolutePath).FullName
                                                  : cur.Match.AbsolutePath;
                System.Diagnostics.Process.Start(matchPath);
            }
        }

		private void propertiesMenuItem_Click(object sender, EventArgs e)
		{
			if (!lvCompare.FocusedItem.Selected) return;
			int idx = lvCompare.FocusedItem.Index;
			string msg = "";
			FileUnit cur = (FileUnit)_dvCompare[idx][8];
			if (cur.Match == null)
			{
				if (cur.AbsolutePath.StartsWith(_source))
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
						msg = "Source: Does not exist\n" +
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
            if (!lvCompare.FocusedItem.Selected)
            {
                e.Cancel = true;
                return;
            }

			int idx = lvCompare.FocusedItem.Index;
            SyncAction defaultAction = (SyncAction)_dvCompare[idx][9];

            foreach (ToolStripItem item in lvMenu.Items)
            {
                if (item is ToolStripSeparator) continue;
                item.Visible = item.Text.Equals(defaultAction.Text(), StringComparison.CurrentCultureIgnoreCase);
            }

            openMenuItem.Visible = true;
            openFolderMenuItem.Visible = true;
            skipMenuItem.Visible = true;
            propertiesMenuItem.Visible = true;
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
            ChangeSyncAction(SyncAction.DeleteSourceFile);
		}

		private void delTargetMenuItem_Click(object sender, EventArgs e)
		{
            ChangeSyncAction(SyncAction.DeleteTargetFile);
		}

		private void skipMenuItem_Click(object sender, EventArgs e)
		{
			SetChildSyncAction(SyncAction.Skip);
		}

		private void btnSynchronize_Click(object sender, EventArgs e)
		{
		}

		private void createSourceMenuItem_Click(object sender, EventArgs e)
		{
			ChangeSyncAction(SyncAction.CreateSourceDir);
		}

		private void createTargetMenuItem_Click(object sender, EventArgs e)
		{
			ChangeSyncAction(SyncAction.CreateTargetDir);
		}

		private void keepBothMenuItem_Click(object sender, EventArgs e)
		{
			ChangeSyncAction(SyncAction.KeepBothCopies);
		}

        private void lvCompare_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.NewWidth < MIN_WIDTH)
            {
                e.Cancel = true;
                e.NewWidth = lvCompare.Columns[e.ColumnIndex].Width;
            }
        }

        private void renameSrcMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSyncAction(SyncAction.RenameSourceFile);
        }

        private void renameTgtMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSyncAction(SyncAction.RenameTargetFile);
        }

		#endregion
	}
}