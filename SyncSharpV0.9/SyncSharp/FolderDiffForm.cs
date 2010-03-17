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

		private DataView dvCompare;
		private DataTable dtCompare;
		//private Detector detector;
		private FolderDiffLists previewLists;

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

		public FolderDiffForm(FolderDiffLists diffLists, SyncTask task)
		{
			InitializeComponent();
			previewLists = diffLists;
			source = task.Source;
			target = task.Target;
      lblName.Text = task.Name;
			lblSource.Text = source;
			lblTarget.Text = target;
      this.Text = "[" + lblName.Text + "] Synchronization Preview";
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
				if (u.Match != null)
				{
					row[5] = u.Match.Size;
					row[6] = u.Match.LastWriteTime;
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
				if (u.Match != null)
				{
					row[1] = u.Match.Size;
					row[2] = u.Match.LastWriteTime;
				}
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
				imageList.Images.Add(u.Extension, ShellIcon.GetSmallIcon(u.AbsolutePath).ToBitmap());
		}

		private void ProcessSourceLists()
		{
			foreach (FileUnit u in previewLists.SingleSourceFilesList)
			{
				if (u.IsDirectory)
				{
					numOfSFoldersC++;
					AddDataRow(u, SyncAction.CreateTargetDir);
				}
				else
				{
					numOfSFilesCpy++;
					sFilesCpySize += u.Size;
					AddDataRow(u, SyncAction.CopyFileToTarget);
				}
			}
			foreach (FileUnit u in previewLists.NewSourceFilesList)
			{
				numOfSFilesCpy++;
				sFilesCpySize += u.Size;
				numOfTFilesOW++;
				tFilesOWSize += u.Match.Size;

				AddDataRow(u, SyncAction.CopyFileToTarget);
			}
			foreach (FileUnit u in previewLists.DeleteSourceFilesList)
			{
				if (u.IsDirectory)
					numOfSFoldersDel++;
				else
				{
					numOfSFilesDel++;
					sFilesDelSize += u.Size;
				}
				AddDataRow(u, SyncAction.DeleteSourceFile);
			}
		}

		private void ProcessTargetLists()
		{
			foreach (FileUnit u in previewLists.SingleTargetFilesList)
			{
				if (u.IsDirectory)
				{
					numOfTFoldersC++;
					AddDataRow(u, SyncAction.CreateSourceDir);
				}
				else
				{
					numOfTFilesCpy++;
					tFilesCpySize += u.Size;
					AddDataRow(u, SyncAction.CopyFileToSource);
				}
			}
			foreach (FileUnit u in previewLists.NewTargetFilesList)
			{
				numOfTFilesCpy++;
				tFilesCpySize += u.Size;
				numOfSFilesOW++;
				sFilesOWSize += u.Match.Size;
				AddDataRow(u, SyncAction.CopyFileToSource);
			}
			foreach (FileUnit u in previewLists.DeleteTargetFilesList)
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

		private void ProcessKeepBothFilesList()
		{
			foreach (FileUnit u in previewLists.KeepBothFilesList)
			{
				numOfSFilesCpy++;
				numOfTFilesCpy++;
				sFilesCpySize += u.Size;
				tFilesCpySize += u.Match.Size;
				AddDataRow(u, SyncAction.KeepBothFiles);
			}
		}

		private void PopulateListView()
		{
			ProcessKeepBothFilesList();
			ProcessSourceLists();
			ProcessTargetLists();
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
			int imageIdx = imageList.Images.IndexOfKey(dvCompare[idx][7].ToString());
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
				case SyncAction.CreateSourceDir:
					item.SubItems[3].Text = SyncAction.CreateSourceDir.Text();
					break;
				case SyncAction.CreateTargetDir:
					item.SubItems[3].Text = SyncAction.CreateTargetDir.Text();
					break;
				case SyncAction.KeepBothFiles:
					item.SubItems[3].Text = SyncAction.KeepBothFiles.Text();
					break;
			}

			FileUnit cur = (FileUnit)dvCompare[idx][8];

			if (cur.Match == null)
			{
				if (cur.AbsolutePath.StartsWith(source))
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
			this.keepBothMenuItem.Visible = isVisible;
			this.createSourceMenuItem.Visible = isVisible;
			this.createTargetMenuItem.Visible = isVisible;
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
					"" + numOfSFilesCpy + " [" + FormatSize(sFilesCpySize) + "]";
			lblTargetCpy.Text = (numOfTFilesCpy == 0) ? "None" :
					"" + numOfTFilesCpy + " [" + FormatSize(tFilesCpySize) + "]";
			lblSourceDel.Text = (numOfSFilesDel == 0) ? "None" :
					"" + numOfSFilesDel + " [" + FormatSize(sFilesDelSize) + "]";
			lblTargetDel.Text = (numOfTFilesDel == 0) ? "None" :
					"" + numOfTFilesDel + " [" + FormatSize(tFilesDelSize) + "]";
			lblSourceOW.Text = (numOfSFilesOW == 0) ? "None" :
					"" + numOfSFilesOW + " [" + FormatSize(sFilesOWSize) + "]";
			lblTargetOW.Text = (numOfTFilesOW == 0) ? "None" :
					"" + numOfTFilesOW + " [" + FormatSize(tFilesOWSize) + "]";
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
					FormatSize(sFilesCpySize + sFilesDelSize + sFilesOWSize) + "]";
			lblTargetTotal.Text = numOfTFilesCpy + numOfTFilesDel +
					numOfTFilesOW + numOfTFoldersC + numOfTFoldersDel + " [" +
					FormatSize(tFilesCpySize + tFilesDelSize + tFilesOWSize) + "]";
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

		public String FormatSize(long size)
		{
			string sizeString = "";
			if (size < 1024)
				sizeString = String.Format("{0:0.00 B}", size);
			else if (size < 1048576)
				sizeString = String.Format("{0:0.00 KB}", size / 1024.0);
			else if (size < 1073741824)
				sizeString = String.Format("{0:0.00 MB}", size / 1048576.0);
			else
				sizeString = String.Format("{0:0.00 GB}", size / 1073741824.0);
			return sizeString;
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
				else if (e.SubItem.Text.Equals(SyncAction.SkipNExclude.Text()))
					syncIcon = Properties.Resources.exclude;
				else if (e.SubItem.Text.Equals(SyncAction.KeepBothFiles.Text()))
					syncIcon = Properties.Resources.KeepBoth;
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
			dvCompare.Sort = dtCompare.Columns[0].ColumnName + " ASC";
			UpdateStatisticsView();
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void syncMenuItem_Click(object sender, EventArgs e)
		{
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
			ResetActionList(false);
			int idx = lvCompare.FocusedItem.Index;
			FileUnit cur = (FileUnit)dvCompare[idx][8];
			if (cur.Match == null)
			{
				if (cur.AbsolutePath.StartsWith(source))
				{
					if (cur.IsDirectory)
						createTargetMenuItem.Visible = true;
					else
						this.copyToTargetMenuItem.Visible = true;
					this.delFrmSourceMenuItem.Visible = true;
				}
				else
				{
					if (cur.IsDirectory)
						createSourceMenuItem.Visible = true;
					else
						this.copyToSourceMenuItem.Visible = true;
					this.delFrmTargetMenuItem.Visible = true;
				}
				this.excludeMenuItem.Visible = true;
			}
			else
			{
				ResetActionList(true);
				createSourceMenuItem.Visible = false;
				createTargetMenuItem.Visible = false;
			}
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

		private void ClearLists()
		{
			previewLists.SingleTargetFilesList.Clear();
			previewLists.SingleSourceFilesList.Clear();
			previewLists.NewSourceFilesList.Clear();
			previewLists.NewTargetFilesList.Clear();
			previewLists.DeleteSourceFilesList.Clear();
			previewLists.DeleteTargetFilesList.Clear();
			previewLists.KeepBothFilesList.Clear();
		}

		private void PopulateDetectorLists()
		{
			ClearLists();
			foreach (DataRow row in dtCompare.Rows)
			{
				FileUnit cur = (FileUnit)row[8];
				if ((SyncAction)row[3] == SyncAction.CopyFileToSource)
				{
					if (cur.Match != null)
					{
						if (cur.AbsolutePath.StartsWith(source))
							previewLists.NewTargetFilesList.Add(cur.Match);
						else
							previewLists.NewTargetFilesList.Add(cur);
					}
					else
						previewLists.SingleTargetFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.CopyFileToTarget)
				{
					if (cur.Match != null)
					{
						if (cur.AbsolutePath.StartsWith(source))
							previewLists.NewSourceFilesList.Add(cur);
						else
							previewLists.NewSourceFilesList.Add(cur.Match);
					}
					else
						previewLists.SingleSourceFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.CreateSourceDir)
				{
					previewLists.SingleTargetFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.CreateTargetDir)
				{
					previewLists.SingleSourceFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.DeleteSourceFile)
				{
					if (cur.Match != null)
					{
						if (cur.AbsolutePath.StartsWith(source))
							previewLists.DeleteSourceFilesList.Add(cur);
						else
							previewLists.DeleteSourceFilesList.Add(cur.Match);
					}
					else
						previewLists.DeleteSourceFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.DeleteTargetFile)
				{
					if (cur.Match != null)
					{
						if (cur.AbsolutePath.StartsWith(source))
							previewLists.DeleteTargetFilesList.Add(cur.Match);
						else
							previewLists.DeleteTargetFilesList.Add(cur);
					}
					else
						previewLists.DeleteTargetFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.DeleteBothFile)
				{
					if (cur.AbsolutePath.StartsWith(source))
					{
						previewLists.DeleteSourceFilesList.Add(cur);
						previewLists.DeleteTargetFilesList.Add(cur.Match);
					}
					else
					{
						previewLists.DeleteSourceFilesList.Add(cur.Match);
						previewLists.DeleteTargetFilesList.Add(cur);
					}
				}
				else if ((SyncAction)row[3] == SyncAction.KeepBothFiles)
				{
					previewLists.KeepBothFilesList.Add(cur);
				}
				else if ((SyncAction)row[3] == SyncAction.SkipNExclude)
					previewLists.UnChangedFilesList.Add(cur);
			}
		}

		private void btnSynchronize_Click(object sender, EventArgs e)
		{
			PopulateDetectorLists();
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
			ChangeSyncAction(SyncAction.KeepBothFiles);
		}

		#endregion
	}
}