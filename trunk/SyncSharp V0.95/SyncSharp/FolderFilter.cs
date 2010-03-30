using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using SyncSharp.Storage;
using SyncSharp.Business;

namespace SyncSharp.GUI
{
	public partial class FolderFilter : Form
	{
		private SyncTask currentTask;
		private delegate void PopulateTreeViewCallback(string dir, TreeNode node);

		private PopulateTreeViewCallback initSourceTreeView;
		private PopulateTreeViewCallback initTargetTreeView;

        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;

		public FolderFilter(SyncTask task)
		{
			InitializeComponent();

			currentTask = task;
			
			srcTreeView.Nodes.Add(currentTask.Source);
			tarTreeView.Nodes.Add(currentTask.Target);

            srcTreeView.ImageList = imageList;
            tarTreeView.ImageList = imageList;

			initSourceTreeView = new PopulateTreeViewCallback(PopulateTreeView);
			initTargetTreeView = new PopulateTreeViewCallback(PopulateTreeView);
		}

        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam,
        IntPtr lParam);

        private void HideTreeNodeCheckBox(TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
            Marshal.StructureToPtr(tvi, lparam, false);
            SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
        } 

		public void PopulateTreeView(string root, TreeNode parentNode)
		{
			Stack<string> paths = new Stack<string>();
			Stack<TreeNode> nodes = new Stack<TreeNode>();

			paths.Push(root);
			nodes.Push(parentNode);

			while (paths.Count > 0)
			{
				root = paths.Pop();
				parentNode = nodes.Pop();
				try
				{
					foreach (string dir in Directory.GetDirectories(root))
					{
						string substringDir = Path.GetFileName(dir);

						TreeNode myNode = new TreeNode(substringDir);
                        myNode.ImageIndex = 0;
						parentNode.Nodes.Add(myNode);

						paths.Push(dir);
						nodes.Push(myNode);
					}
				}
				catch (UnauthorizedAccessException)
				{
				}
			}
		}

		private void FolderFilter_Load(object sender, EventArgs e)
		{
			PopulateTreeView(currentTask.Source, srcTreeView.Nodes[0]);
			PopulateTreeView(currentTask.Target, tarTreeView.Nodes[0]);

			ReadFolderFilters(srcTreeView.Nodes[0],
					currentTask.Filters.SourceFolderExcludeList);
			ReadFolderFilters(tarTreeView.Nodes[0],
					currentTask.Filters.TargetFolderExcludeList);

			srcTreeView.Nodes[0].ExpandAll();
			tarTreeView.Nodes[0].ExpandAll();

            HideTreeNodeCheckBox(srcTreeView.Nodes[0]);
            HideTreeNodeCheckBox(tarTreeView.Nodes[0]);
		}

		private void CheckAllTreeNode(bool isCheck)
		{
			CheckAllChildNodes(srcTreeView.Nodes[0], isCheck);
			CheckAllChildNodes(tarTreeView.Nodes[0], isCheck);
		}

		private void btnUnSelectAll_Click(object sender, EventArgs e)
		{
			CheckAllTreeNode(false);
		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			CheckAllTreeNode(true);
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			if (currentTask.Filters == null)
				currentTask.Filters = new Filters();

			currentTask.Filters.SourceFolderExcludeList.Clear();
			currentTask.Filters.TargetFolderExcludeList.Clear();

			AddFolderFilters(srcTreeView.Nodes[0], 
                    currentTask.Source, currentTask.Target,
					currentTask.Filters.SourceFolderExcludeList, 
                    currentTask.Filters.TargetFolderExcludeList);
			AddFolderFilters(tarTreeView.Nodes[0],
                    currentTask.Target, currentTask.Source,
					currentTask.Filters.TargetFolderExcludeList,
                    currentTask.Filters.SourceFolderExcludeList);

			this.Close();
		}

		private void AddFolderFilters(TreeNode treeNode, string source, string target, 
                                List<string> srcExcludeList, List<string> tgtExcludeList)
		{
			foreach (TreeNode node in treeNode.Nodes)
			{
                if (node.Checked)
                {
                    srcExcludeList.Add(node.FullPath);
                    string targetPath = target + node.FullPath.Substring(source.Length);
                    if (!tgtExcludeList.Contains(targetPath))
                        tgtExcludeList.Add(targetPath);
                }

				if (node.Nodes.Count > 0)
					AddFolderFilters(node, source, target, srcExcludeList, tgtExcludeList);
			}
		}

		private void ReadFolderFilters(TreeNode treeNode, List<string> excludeList)
		{
			foreach (TreeNode node in treeNode.Nodes)
			{
				if (excludeList.Contains(node.FullPath))
					node.Checked = true;

				if (node.Nodes.Count > 0)
					ReadFolderFilters(node, excludeList);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void CheckMatchNode(TreeNode root, TreeNode selNode, bool isSource)
		{
			foreach (TreeNode node in root.Nodes)
			{
				string path = (isSource) ?
						node.FullPath.Substring(currentTask.Source.Length) :
						node.FullPath.Substring(currentTask.Target.Length);

				string selPath = (isSource) ?
                        selNode.FullPath.Substring(currentTask.Target.Length) :
                        selNode.FullPath.Substring(currentTask.Source.Length);

				if (String.Compare(path,selPath,true) == 0)
				{
					node.Checked = selNode.Checked;
					CheckAllChildNodes(node, selNode.Checked);
					break;
				}
				else
					CheckMatchNode(node, selNode, isSource);
			}
		}

		private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
		{
			foreach (TreeNode node in treeNode.Nodes)
			{
				node.Checked = nodeChecked;
				if (node.Nodes.Count >= 0)
					this.CheckAllChildNodes(node, nodeChecked);
			}
		}

		private void srcTreeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count >= 0 && e.Node.Parent != null)
                {
                    if (!e.Node.Checked && e.Node.Parent.Checked)
                    {
                        e.Node.Checked = true;
                        return;
                    }

                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                    CheckMatchNode(tarTreeView.Nodes[0], e.Node, false);
                }
            }
		}

		private void tarTreeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count >= 0 && e.Node.Parent != null)
                {
                    if (!e.Node.Checked && e.Node.Parent.Checked)
                    {
                        e.Node.Checked = true;
                        return;
                    }

                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                    CheckMatchNode(srcTreeView.Nodes[0], e.Node, true);
                }
            }
		}

        private void srcTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            SuppressKeyPress(e);
        }

        private void tarTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            SuppressKeyPress(e);
        }

        private void SuppressKeyPress(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }
	}
}