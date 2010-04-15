using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Business
{
    /// <summary>
    /// Written by Guo Jiayuan
    /// </summary>
	public enum SyncAction
	{
		CopyFileToSource,
		CopyFileToTarget,
		DeleteBothFile,
		DeleteSourceFile,
		DeleteTargetFile,
		CreateSourceDir,
		CreateTargetDir,
		RenameSourceFile,
		RenameTargetFile,
		KeepBothCopies,
		NoAction,
		DeleteBothDir,
		DeleteTargetDir,
		DeleteSourceDir,
        Skip
	}

	static class SynActionExtension
	{
		public static string Text(this SyncAction action)
		{
			string retVal = "";
			switch (action)
			{
				case SyncAction.KeepBothCopies:
					retVal = "Keep Both";
					break;
				case SyncAction.CopyFileToSource:
					retVal = "Copy to Source";
					break;
				case SyncAction.CopyFileToTarget:
					retVal = "Copy to Target";
					break;
                case SyncAction.DeleteBothDir:
				case SyncAction.DeleteBothFile:
					retVal = "Delete Both";
					break;
                case SyncAction.DeleteSourceDir:
				case SyncAction.DeleteSourceFile:
					retVal = "Delete from Source";
					break;
                case SyncAction.DeleteTargetDir:
				case SyncAction.DeleteTargetFile:
					retVal = "Delete from Target";
					break;
				case SyncAction.CreateSourceDir:
					retVal = "Create Source";
					break;
				case SyncAction.CreateTargetDir:
					retVal = "Create Target";
					break;
                case SyncAction.RenameSourceFile:
                    retVal = "Rename Source";
                    break;
                case SyncAction.RenameTargetFile:
                    retVal = "Rename Target";
                    break;
				case SyncAction.NoAction:
					retVal = "No Action";
					break;
                case SyncAction.Skip:
                    retVal = "Skip";
                    break;
			}
			return retVal;
		}
	}
}