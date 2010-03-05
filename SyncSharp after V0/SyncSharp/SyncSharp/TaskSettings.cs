using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SyncSharp.Storage
{
	[Serializable]
	public class TaskSettings
	{
		// Advanced
		public bool PlugSync { get; set; }

		// Comparison Rules
		public bool SkipDifferenceWindow { get; set; }
		public bool CompareSize { get; set; }
		public bool CompareHashCode { get; set; }
		public bool CompareTimeStamp { get; set; }
		public bool CompareAttribute { get; set; }
		public bool IgnoreDSTDifference { get; set; }
		public bool SkipHiddenFiles { get; set; }
		public bool SkipReadOnlyFiles { get; set; }
		public bool SkipSystemFiles { get; set; }
		public bool SkipTempFiles { get; set; }
		public int IgnoreTimeDifference { get; set; }
		public int IgnoreLastModifiedDifference { get; set; }

		// Copy/Delete

		public bool UseRecycleBin { get; set; }
		public bool ConfirmActions { get; set; }
		public bool DelEmptySrcFolders { get; set; }
		public bool DelEmptyTarFolders { get; set; }
		public bool VerifySync { get; set; }
		public bool RetryPrompt { get; set; }
		public bool ResetArhiveAttributes { get; set; }
		public bool UseSafeCopy { get; set; }

		// Log Settings
		public bool DisplayLog { get; set; }
		public bool DisplayLogOnlyOnError { get; set; }
		public bool LogReasons { get; set; }
		public bool NoLogFiltered { get; set; }
		public bool LogDriveSerial { get; set; }
		public bool LogFailedOps { get; set; }

		// Conflict Settings
		// 0:  Default actions for conflicts as specified by the reconciler
		// 1:  FolderA always have the priority in conflict cases
		// -1:  FolderB always have the priority in conflict cases
		public int UpdateConflict { get; set; }
		public int UpdateDeleteConflict { get; set; }
		public int CCreatePDeleteConflict { get; set; }
		public int FolderNameConflict { get; set; }
		public int CCFileNameConflict { get; set; }
		public int CRFileNameConflict { get; set; }

		// Constructor
		internal TaskSettings()
		{
			//this.PlugSync = true;

			//this.SkipDifferenceWindow = true;
			//this.CompareSize = true;
			//this.CompareHashCode = true;
			//this.CompareTimeStamp = true;
			//this.CompareAttribute = true;
			//this.IgnoreDSTDifference = true;
			//this.SkipHiddenFiles = true;
			//this.SkipReadOnlyFiles = true;
			//this.SkipSystemFiles = true;
			//this.SkipTempFiles = true;
			//this.IgnoreTimeDifference = 0;
			//this.IgnoreLastModifiedDifference = 0;

			//this.UseRecycleBin = true;
			//this.ConfirmActions = true;
			//this.DelEmptySrcFolders = true;
			//this.DelEmptyTarFolders = true;
			//this.VerifySync = true;
			//this.RetryPrompt = true;
			//this.ResetArhiveAttributes = true;
			//this.UseSafeCopy = true;

			//this.DisplayLog = true;
			//this.DisplayLogOnlyOnError = true;
			//this.LogReasons = true;
			//this.NoLogFiltered = true;
			//this.LogDriveSerial = true;
			//this.LogFailedOps = true;

			this.UpdateConflict = 0;
			this.UpdateDeleteConflict = 0;
			this.CCreatePDeleteConflict = 0;
			this.FolderNameConflict = 0;
			this.CCFileNameConflict = 0;
			this.CRFileNameConflict = 0;
		}
	}
}
