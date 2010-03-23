using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PassingDataBetweenForms
{
    public partial class AddTaskForm : Form
    {
        public AddTaskForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /*The constructor approach*/
            DisplayTaskForm frm = new DisplayTaskForm(txtName.Text, txtSource.Text, txtTarget.Text);
            frm.ShowDialog();
        }
    }
}
