using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SyncSharp.GUI
{
    public partial class ProgressForm : Form
    {
        public ProgressForm(string formTitle, string message, MainForm form)
        {
            InitializeComponent();
            this.Text = formTitle;
            this.Owner = form;
            lblMessage.Text = message;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        public void SetMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Refresh();
            progressBar.Refresh();
        }

        private void ProgressForm_Leave(object sender, EventArgs e)
        {
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }
    }
}
