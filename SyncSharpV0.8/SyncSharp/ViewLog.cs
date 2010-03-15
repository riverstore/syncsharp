using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Business;
using System.IO;

namespace SyncSharp.GUI
{
    public partial class ViewLog : Form
    {
        string logFile;
        
        public ViewLog(string taskName, string logFile)
        {
            InitializeComponent();

            this.Text = "[" + taskName + "]" + " log file";
            this.logFile = logFile;
        }

        private void ViewLog_Load(object sender, EventArgs e)
        {
            try
            {
                string contents = File.ReadAllText(logFile);
                txtLog.Text = (contents == "") ? "Empty log file" : contents;
                txtLog.Select(0, 0);
            }
            catch
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(logFile, "");
                txtLog.Clear();
            }
            catch
            {
            }

        }
    }
}
