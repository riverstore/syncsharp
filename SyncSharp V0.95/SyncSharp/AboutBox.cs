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
			Text = "About SyncSharp";
			labelProductName.Text = "SyncSharp";
			labelVersion.Text = "Version 0.9";
			labelCopyright.Text = "Copyright © Excalibur 2010. All rights reserved.";
		}
	}
}