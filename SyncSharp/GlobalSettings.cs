using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncSharp.Storage;

namespace SyncSharp.GUI
{
	public partial class GlobalSettings : Form
	{
		SyncProfile currentProfile;

		public GlobalSettings(SyncProfile profile)
		{
			InitializeComponent();
			currentProfile = profile;
			readSettings(currentProfile);
		}

		private void readSettings(SyncProfile currentProfile)
		{
			countDown.Value = currentProfile.CountDown;
			enableAutoPlay.Checked = currentProfile.AutoPlay;
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			saveSettings(currentProfile);
			this.Close();
		}

		private void saveSettings(SyncProfile currentProfile)
		{
			currentProfile.AutoPlay = enableAutoPlay.Checked;
			currentProfile.CountDown = (int)countDown.Value;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			saveSettings(currentProfile);
		}
	}
}