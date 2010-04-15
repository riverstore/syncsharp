using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SyncSharp.GUI
{
    /// <summary>
    /// Written by Guo Jiayuan
    /// </summary>
	partial class AboutBox : Form
	{
        private delegate void OpenURLDelegate();

		public AboutBox()
		{
			InitializeComponent();
			Text = "About SyncSharp";
			labelProductName.Text = "SyncSharp";
			labelVersion.Text = "Version 2.0";
			labelCopyright.Text = "Copyright © Excalibur 2010. All rights reserved.";
		}

        private void OpenURL()
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/syncsharp/");
        }

        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenURLDelegate openURLCaller = new OpenURLDelegate(OpenURL);
                openURLCaller.BeginInvoke(null, null);
            }
            catch
            {
                MessageBox.Show("Error opening http://code.google.com/p/syncsharp/", "SyncSharp",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
	}
}