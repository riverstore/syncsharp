using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThePropertiesApproach
{
    public partial class AddTaskForm : Form
    {
        public AddTaskForm()
        {
            InitializeComponent();
        }


        /*The properties approach*/
        public string GetTaskName
        {
            get { return txtName.Text; }
        }

        public string GetTaskSource
        {
            get { return txtSource.Text; }
        }

        public string GetTaskTarget
        {
            get { return txtTarget.Text; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /*The properties approach*/
            DisplayTaskForm frm = new DisplayTaskForm();
            frm.SetTaskName = this.GetTaskName;
            frm.SetTaskSource = this.GetTaskSource;
            frm.SetTaskTarget = this.GetTaskTarget;
            frm.ShowDialog();
        }
    }
}
