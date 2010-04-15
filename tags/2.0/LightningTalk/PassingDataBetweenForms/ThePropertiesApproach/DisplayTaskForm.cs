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
    public partial class DisplayTaskForm : Form
    {
        private string taskName, source, target;

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

        /*The properties approach*/
        public string SetTaskName
        {
            set { taskName = value; }
        }

        public string SetTaskSource
        {
            set { source = value; }
        }

        public string SetTaskTarget
        {
            set { target = value; }
        }

        private void DisplayTaskForm_Load(object sender, EventArgs e)
        {
            populateListView();
        }

    }
}
