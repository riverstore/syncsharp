using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Business;

namespace SyncSharp
{
    public partial class MainForm : Form
    {
        private TaskSetupForm _tsForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void exitTSButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void analyzeMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
