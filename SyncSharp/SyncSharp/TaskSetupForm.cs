using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SyncSharp
{
    public partial class TaskSetupForm : Form
    {
        public TaskSetupForm()
        {
            InitializeComponent();
        }

        public string Source
        {
            get { return txtSource.Text; }
        }

        public string Target
        {
            get { return txtTarget.Text; }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string temp = txtSource.Text;
            txtSource.Text = txtTarget.Text;
            txtTarget.Text = temp;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtSource.Clear();
            txtTarget.Clear();
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            fbDialog = new FolderBrowserDialog();
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                txtTarget.Text = fbDialog.SelectedPath;
            }
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            fbDialog = new FolderBrowserDialog();
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fbDialog.SelectedPath;
            }
        }

        private bool CheckFolderPair()
        {
            string source = Environment.ExpandEnvironmentVariables(txtSource.Text);
            string target = Environment.ExpandEnvironmentVariables(txtTarget.Text);

            if (String.IsNullOrEmpty(source))
            {
                MessageBox.Show("Please provide a source directory.",
                    "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (String.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please provide a destination directory.",
                    "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(source))
            {
                MessageBox.Show("Source directory does not exist.",
                    "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(target))
            {
                MessageBox.Show("Destination directory does not exist.",
                    "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (String.Equals(source, target))
            {
                MessageBox.Show("Source directory cannot be the same " +
                    "as the destination directory.",
                    "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (source.StartsWith(target))
            {
                MessageBox.Show("Source directory cannot be a " +
                   "subdirectory of the destination directory.",
                   "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (target.StartsWith(source))
            {
                MessageBox.Show("Destination directory cannot be a " +
                   "subdirectory of the source directory.",
                  "FolderDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (CheckFolderPair()) this.Hide();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (CheckFolderPair()) this.Hide();
        }
      
    }
}
