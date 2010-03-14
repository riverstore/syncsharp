using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SyncSharp.GUI
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = "About SyncSharp";
            this.labelProductName.Text = "SyncSharp";
            this.labelVersion.Text = "Version 0.9";
            this.labelCopyright.Text = "Copyright © Excalibur 2010. All rights reserved.";
        }

    }
}
