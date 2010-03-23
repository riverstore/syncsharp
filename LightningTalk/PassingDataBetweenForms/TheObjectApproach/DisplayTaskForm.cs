using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TheObjectApproach
{
    public partial class DisplayTaskForm : Form
    {
        private string taskName, source, target;

        /*The object approach*/
        public AddTaskForm addTaskFrm;

        public DisplayTaskForm()
        {
            InitializeComponent();
        }

        private void populateListView()
        {
            ListViewItem task = new ListViewItem(taskName);
            task.SubItems.Add(source);
            task.SubItems.Add(target);

            lvTask.Items.Add(task);
        }
       
        /*The object approach*/
        private void DisplayTaskForm_Load(object sender, EventArgs e)
        {
            taskName = addTaskFrm.txtName.Text;
            source = addTaskFrm.txtSource.Text;
            target = addTaskFrm.txtTarget.Text;

            populateListView();
        }

    }
}
