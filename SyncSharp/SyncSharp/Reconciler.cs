using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;

namespace SyncSharp.Business
{
    class Reconciler
    {

        public enum SyncAction
        {
            CopyFileToSource,
            CopyFileToTarget,
            DeleteFileFromSource,
            DeleteFileFromTarget,
            CreateSourceDir,
            CreateTargetDir,
            RenameSourceFile,
            RenameTargetFile,
            RenameSourceFolder,
            RenameTargetFolder,
            DeleteSourceFolder,
            DeleteTargetFolder
        }

        public static void update(Detector detector, TaskSettings settings)
        {
					foreach (FileUnit u in detector.FilesInSourceOnly)
					{
						if (!u.IsDirectory)
						{
							updateFile(u, null, true, false);
						}
						else
						{   // u is a directory.
							updateDir(u, null, true, false);
						}
					}

					foreach (FileUnit u in detector.FilesInTargetOnly)
					{
						if (!u.IsDirectory)
						{
							updateFile(null, u, false, true);
						}
						else
						{   // u is a directory.
							updateDir(null, u, false, true);
						}
					}

					foreach (FileUnit u in detector.ConflictFiles)
					{
						if (u.Match != null)
						{
							updateFile(u, u.Match, true, true);
						}
						else
						{
							if (u.AbsolutePath.StartsWith(detector.Source))
								updateFile(u, null, true, true);
							else
								updateFile(null, u, true, true);
						}

					}

            foreach (FileUnit u in detector.DeleteFilesFromSource)
            {
                switch (chkFileDelete(u, null))
                {
                    case SyncAction.DeleteFileFromSource:
                        File.Delete(u.AbsolutePath);
                        break;
                }
            }

            foreach (FileUnit u in detector.DeleteFilesFromTarget)
            {
                switch (chkFileDelete(null, u))
                {
                    case SyncAction.DeleteFileFromTarget:
                        File.Delete(u.AbsolutePath);
                        break;
                }
            }

        }

        static void updateFile(FileUnit srcFile, FileUnit dstFile, bool srcStatus, bool dstStatus)
        {
            string strTarget;
            switch (chkFileUpdate(srcFile, dstFile, srcStatus, dstStatus))
            {
                case SyncAction.CopyFileToSource:
                    strTarget = dstFile.TargetPath + "\\" + dstFile.Name;
                    File.Copy(dstFile.AbsolutePath, strTarget, true);
                    break;
                case SyncAction.CopyFileToTarget:
                    strTarget = srcFile.TargetPath +  "\\" + srcFile.Name;
                    File.Copy(srcFile.AbsolutePath, strTarget, true);
                    break;
            }
        }

        static SyncAction chkFileUpdate(FileUnit sFile, FileUnit dFile, bool srcFile, bool dstFile)
        {
            if (srcFile == true && dstFile == false)// source file is dirty,but destination file is not dirty
            {
                return SyncAction.CopyFileToTarget; // copy source file to destination file
            }
            else if (srcFile == false && dstFile == true)// destination file is dirty, but source file is not dirty
            {
                return SyncAction.CopyFileToSource; // copy destination file to source file
            }
            //else if (srcFile == true && dstFile == true)// both source file and destination file are dirty
            else
            {
                if (sFile != null && dFile != null)
                {
                    if (sFile.LastWriteTime > dFile.LastWriteTime)
                    {
                        return SyncAction.CopyFileToTarget; // source file is the latest, so copy source file to destination file
                    }
                    else
                        return SyncAction.CopyFileToSource; // destination file is the latest, so copy destination file to source file
                }
                else
                {
                    if (sFile == null && dFile != null)
                    {
                        return SyncAction.CopyFileToSource;// copy destination file to source file
                    }
                    //else if (sFile != null && dFile == null)
                    else
                    {
                        return SyncAction.CopyFileToTarget;// copy source file to destination file
                    }
                }
            }

        }

        static void updateDir(FileUnit srcDir, FileUnit dstDir, bool srcStatus, bool dstStatus)
        {
            switch (chkDirUpdate(srcDir, dstDir, srcStatus, dstStatus))
            {
                case SyncAction.CreateSourceDir:
                    Directory.CreateDirectory(dstDir.TargetPath);
                    break;
                case SyncAction.CreateTargetDir:
                    Directory.CreateDirectory(srcDir.TargetPath);
                    break;
            }
        }


        static SyncAction chkDirUpdate(FileUnit sDirectory, FileUnit dDirectory, bool srcStatus, bool dstStatus)
        {
            if (sDirectory != null && dDirectory == null)
            { // source directory is dirty, but destination directory is not dirty
                return SyncAction.CreateTargetDir; // copy source directory to destination directory
            }
            //else if (sDirectory == null && dDirectory != null)
            else
            { // destination directory is dirty, but source directory is not dirty
                return SyncAction.CreateSourceDir;
            }

        }

        static SyncAction chkFileDelete(FileUnit sFile, FileUnit dFile)
        {
            if (sFile == null)
            {
                return SyncAction.DeleteFileFromTarget; //delete from destination
            }

            /*else if(dFile == null){
                return SyncAction.DeleteFileFromSource; // delete from source
            }*/
            else
                return SyncAction.DeleteFileFromSource;

        }

        /*static SyncAction childCreateParentDelete(FileUnit sFile, FileUnit dFile)
        {
            if(!dFile.Exits && !dFolder.Exits){
                return SyncAction.CopyFileToTarget; // copy all files under source folder to destinatination
            }
            else if(!sFile.Exits && sFolder.Exists){
                return SyncAction.CopyFileToSource; // copy all files under destination folder to source
            }
        }*/

        /*static SyncAction folderRename(FileUnit sDirectory, FileUnit dDirectory){
            if(sDirectory.GetHashCode() == dDirectory.GetHashCode() && 
                sDirectory.LastWriteTime == dDirectory.LastWriteTime){
                return SyncAction.RenameSourceFile ; // two folders have the same contents
            }
            else{
                return SyncAction.RenameTargetFolder;
            }
           
        }
        
        static SyncAction fileRename(FileUnit sFile, FileUnit dFile){
            if(sFile.GetHashCode() == dFile.GetHashCode() && sFile.LastWriteTime == dFile.LastWriteTime){
                return SyncAction.RenameSourceFile; // two files have the same contents
            }
            else{
                return SyncAction.RenameTargetFile;
            }
            }
        } */
    }
}
