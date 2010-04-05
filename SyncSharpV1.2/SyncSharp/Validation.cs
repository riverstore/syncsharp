using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SyncSharp.Storage;

namespace SyncSharp.Business
{
    public class Validation
    {
        public enum ErrorMsgCode
        {
            EmptySource,
            EmptyTarget,
            InvalidSource,
            InvalidTarget,
            SameSourceTarget,
            SourceIsASubDirOfTarget,
            TargetIsASubDirOfSource,
            EmptyTaskName,
            InvalidTaskName,
            DuplicateFolderPair,
            DuplicateTaskName,
            NoError,
        }

        /// <summary>
        /// Performs validation source and target directories
        /// </summary>
        /// <param name="source">Full path of source directory</param>
        /// <param name="target">Full path of target directory</param>
        /// <param name="profile">The profile of the current PC</param>
        /// <param name="chkDuplicateFolderPair">Boolean to indicate whether to check for duplicate folder pair</param>
        /// <returns>The error msg code of the error that occurs</returns>
        public static ErrorMsgCode CheckFolderPair(ref string source, ref string target, 
                                           SyncProfile profile, bool chkDuplicateFolderPair)
        {
            if (String.IsNullOrEmpty(source))
                return ErrorMsgCode.EmptySource;

            if (String.IsNullOrEmpty(target))
                return ErrorMsgCode.EmptyTarget;

            if (!Directory.Exists(source))
                return ErrorMsgCode.InvalidSource;

            if (!Directory.Exists(target))
                return ErrorMsgCode.InvalidTarget;

            if (String.Compare(source, target, true) == 0)
                return ErrorMsgCode.SameSourceTarget;

            source = Path.GetFullPath(source);
            target = Path.GetFullPath(target);

            if (source.StartsWith(target, true, null))
                return ErrorMsgCode.SourceIsASubDirOfTarget;

            if (target.StartsWith(source, true, null))
                return ErrorMsgCode.TargetIsASubDirOfSource;

            if (chkDuplicateFolderPair && profile.IsFolderPairExists(source, target))
                return ErrorMsgCode.DuplicateFolderPair;

            return ErrorMsgCode.NoError;
        }

        /// <summary>
        /// Perform validation on task name
        /// </summary>
        /// <param name="taskName">The task name to be validated</param>
        /// <param name="profile">The profile of the current PC</param>
        /// <returns>The error msg code of the error that occurs</returns>
        public static ErrorMsgCode CheckTaskName(string taskName, SyncProfile profile)
        {
            if (taskName.Trim().Equals(""))
                return ErrorMsgCode.EmptyTaskName;
            
            if (profile.TaskExists(taskName.Trim()))
                return ErrorMsgCode.DuplicateTaskName;

            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            invalidChars = string.Format(@"[{0}]", invalidChars);

            if (Regex.IsMatch(taskName.Trim(), invalidChars))
                return ErrorMsgCode.InvalidTaskName;
            
            return ErrorMsgCode.NoError;
        }

        /// <summary>
        /// Display the appropriate error messages based on the given error code
        /// </summary>
        /// <param name="errCode">Error code returns after validation</param>
        /// <param name="taskName">The name of the current sync task</param>
        public static void DisplayErrorMessage(ErrorMsgCode errCode, string taskName)
        {
            switch (errCode)
            {
                case ErrorMsgCode.EmptySource:
                    MessageBox.Show("Please provide a source directory.",
                                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.EmptyTarget:
                    MessageBox.Show("Please provide a target directory.",
                                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.InvalidSource:
                    MessageBox.Show("Source directory does not exist.",
                                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.InvalidTarget:
                    MessageBox.Show("Target directory does not exist.",
                                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.SameSourceTarget:
                    MessageBox.Show("Source directory cannot be the same " +
                                    "as the target directory.",
                                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.SourceIsASubDirOfTarget:
                    MessageBox.Show("Source directory cannot be a " +
                                     "subdirectory of the target directory.",
                                     "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.TargetIsASubDirOfSource:
                    MessageBox.Show("Target directory cannot be a " +
                                     "subdirectory of the source directory.",
                                    "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ErrorMsgCode.DuplicateFolderPair:
                    MessageBox.Show("Source & target directories have been defined in another task." +
                                   "\nPlease select a different source or target directory.",
                                   "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.EmptyTaskName:
                    MessageBox.Show("Please enter a task name",
                            "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.InvalidTaskName:
                    MessageBox.Show(@"Task name cannot contain \ / : * ? < > | characters.",
                             "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ErrorMsgCode.DuplicateTaskName:
                    MessageBox.Show("Task name '" + taskName.Trim() +
                        "' already exists. Please enter another name",
                        "SyncSharp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
