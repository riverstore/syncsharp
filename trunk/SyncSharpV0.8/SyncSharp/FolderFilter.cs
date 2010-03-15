using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SyncSharp.Storage;
using SyncSharp.Business;

namespace SyncSharp.GUI
{
    public partial class FolderFilter : Form
    {
        private SyncTask currentTask;
        private string source, target;
        private delegate void PopulateTreeViewCallback(string dir, TreeNode node);

        private PopulateTreeViewCallback initSourceTreeView;
        private PopulateTreeViewCallback initTargetTreeView;

        public FolderFilter(SyncTask task)
        {
            InitializeComponent();

            currentTask = task;

            this.source = task.Source;
            this.target = task.Target;

            srcTreeView.Nodes.Add(source);
            tarTreeView.Nodes.Add(target);

            initSourceTreeView = new PopulateTreeViewCallback(PopulateTreeView);
            initTargetTreeView = new PopulateTreeViewCallback(PopulateTreeView);
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
            //IAsyncResult src = initSourceTreeView.BeginInvoke(source, srcTreeView.Nodes[0], null, null);
            //IAsyncResult tar = initTargetTreeView.BeginInvoke(target, tarTreeView.Nodes[0], null, null);

            //initSourceTreeView.EndInvoke(src);
            //initTargetTreeView.EndInvoke(tar);
            PopulateTreeView(source, srcTreeView.Nodes[0]);
            PopulateTreeView(target, tarTreeView.Nodes[0]);

            ReadFolderFilters(srcTreeView.Nodes[0], 
                currentTask.Filters.SourceFolderExcludeList);
            ReadFolderFilters(tarTreeView.Nodes[0], 
                currentTask.Filters.TargetFolderExcludeList);

            srcTreeView.Nodes[0].ExpandAll();
            tarTreeView.Nodes[0].ExpandAll();
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
                currentTask.Filters.SourceFolderExcludeList);
            AddFolderFilters(tarTreeView.Nodes[0], 
                currentTask.Filters.TargetFolderExcludeList);

            this.Close();
        }

        private void AddFolderFilters(TreeNode treeNode, List<string> excludeList)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Checked)
                   excludeList.Add(node.FullPath);

                if (node.Nodes.Count > 0)
                    AddFolderFilters(node, excludeList);
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
                    node.FullPath.Substring(source.Length) :
                    node.FullPath.Substring(target.Length);

                string selPath = (isSource) ?
                    selNode.FullPath.Substring(target.Length) :
                    selNode.FullPath.Substring(source.Length);

                if (path.Equals(selPath))
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
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                    CheckMatchNode(tarTreeView.Nodes[0], e.Node, false);
                }

                if (e.Node.Parent == null) e.Node.Checked = false;
            }
        }

        private void tarTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count >= 0 && e.Node.Parent != null)
                {
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                    CheckMatchNode(srcTreeView.Nodes[0], e.Node, true);
                }

                if (e.Node.Parent == null) e.Node.Checked = false;
            }
        }
    }
}
