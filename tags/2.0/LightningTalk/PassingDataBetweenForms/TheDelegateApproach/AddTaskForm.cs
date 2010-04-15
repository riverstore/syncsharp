using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TheDelegateApproach
{
    public partial class AddTaskForm : Form
    {
        public AddTaskForm()
        {
            InitializeComponent();
        }

        /*The delegate approach*/
        public delegate void delPassTaskInfo(string taskName, 
                                             string source, string target);

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /*The delegate approach*/
            DisplayTaskForm frm = new DisplayTaskForm();
            delPassTaskInfo del = new delPassTaskInfo(frm.RetrieveTaskInfo);
            del(txtName.Text, txtSource.Text, txtTarget.Text);
            frm.ShowDialog();
        }
    }
}
