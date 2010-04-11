using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SyncSharp.Storage;
using SyncSharp.Business;
using System.Text.RegularExpressions;

namespace SyncSharp.GUI
{
	public partial class RenameTaskForm : Form
	{
		#region Attributes
		private SyncProfile _currentProfile;
		private SyncTask _currentTask;
		private string _metaDataDir;
		#endregion

		#region Methods
		public RenameTaskForm(SyncProfile profile, SyncTask task, string metaDataDir)
		{
			InitializeComponent();

			_currentProfile = profile;
			_currentTask = task;
			this._metaDataDir = metaDataDir;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			if (!_currentTask.Name.Equals(txtNewName.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
			{
				Validation.ErrorMsgCode errcode = Validation.CheckTaskName(txtNewName.Text, _currentProfile);

				if (errcode != Validation.ErrorMsgCode.NoError)
				{
					Validation.DisplayErrorMessage(errcode, txtNewName.Text);
					txtNewName.Focus();
					return;
				}


				Utility.RenameMetaFiles(_currentTask.Name, txtNewName.Text.Trim(), _metaDataDir);
				_currentTask.Name = txtNewName.Text.Trim();
			}
			this.Close();
		}
		#endregion
	}
}