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
		#region Conflict Settings
		public enum ConflictSrcTgtAction
		{
			KeepBothCopies,
			KeepLatestCopy,
			SourceOverwriteTarget,
			TargetOverwriteSource
		}

		public enum ConflictSrcAction
		{
			CopyFileToTarget,
			DeleteSourceFile
		}

		public enum ConflictTgtAction
		{
			CopyFileToSource,
			DeleteTargetFile
		}

		public enum ConflictFolderAction
		{
			KeepSourceName,
			KeepTargetName
		}
		#endregion

		#region Properties
		// General
		public string FilesInclusion { get; set; }
		public string FilesExclusion { get; set; }

		// Copy/Delete
		public bool MoveDelToRecycleBin { get; set; }
		public bool VerifyCopy { get; set; }
		public bool SafeCopy { get; set; }
		public bool ConfirmFileDel { get; set; }
		public bool ResetArhiveAttributes { get; set; }

		// Log Settings
		public bool DisplayLog { get; set; }
		public bool DisplayLogOnlyOnError { get; set; }
		public bool LogReasons { get; set; }
		public bool NoLogFiltered { get; set; }
		public bool LogDriveSerial { get; set; }
		public bool LogFailedOps { get; set; }

		// Advanced
		public bool PlugSync { get; set; }
		public int IgnoreTimeChange { get; set; }
		public ConflictSrcTgtAction SrcTgtConflict { get; set; }
		public ConflictSrcAction SrcConflict { get; set; }
		public ConflictTgtAction TgtConflict { get; set; }
		public ConflictFolderAction FolderConflict { get; set; }
		#endregion

		#region Constructor
		public TaskSettings()
		{
			this.FilesExclusion = "";
			this.FilesInclusion = "*.*";

			this.MoveDelToRecycleBin = false;
			this.VerifyCopy = false;
			this.SafeCopy = false;
			this.ConfirmFileDel = false;
			this.ResetArhiveAttributes = false;

			this.PlugSync = true;
			this.IgnoreTimeChange = 1;

			this.SrcConflict = ConflictSrcAction.CopyFileToTarget;
			this.TgtConflict = ConflictTgtAction.CopyFileToSource;
			this.SrcTgtConflict = ConflictSrcTgtAction.KeepBothCopies;
			this.FolderConflict = ConflictFolderAction.KeepSourceName;
		} 
		#endregion
	}
}