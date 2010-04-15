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
    /// <summary>
    /// Written by Loh Jianxiong Christoper
    /// </summary>
	public partial class GlobalSettings : Form
	{
		#region Attributes
		private SyncProfile _currentProfile;
		#endregion

		#region Constructor
		public GlobalSettings(SyncProfile profile)
		{
			InitializeComponent();
			_currentProfile = profile;
			ReadSettings(_currentProfile);
		}
		#endregion

		#region Methods
		private void ReadSettings(SyncProfile currentProfile)
		{
			countDown.Value = currentProfile.CountDown;
			enableAutoPlay.Checked = currentProfile.AutoPlay;
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			SaveSettings(_currentProfile);
			this.Close();
		}

		private void SaveSettings(SyncProfile currentProfile)
		{
			currentProfile.AutoPlay = enableAutoPlay.Checked;
			currentProfile.CountDown = (int)countDown.Value;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion
	}
}