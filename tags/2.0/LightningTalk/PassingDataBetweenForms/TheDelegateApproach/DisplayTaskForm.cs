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

        /*The delegate approach*/
        public void RetrieveTaskInfo(string taskName, string source, string target)
        {
            this.taskName = taskName;
            this.source = source;
            this.target = target;

            populateListView();
        }
    }
}
