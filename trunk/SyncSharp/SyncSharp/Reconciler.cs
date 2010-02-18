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

        static Boolean update(Detector detector, TaskSettings settings)
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
                    if (u.AbsolutePath.StartsWith(detector.source))
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
                    case SyncAction.DeleteFileFromTarget
                        File.Delete(u.AbsolutePath);
                        break;
                }
            }

        }

        static void updateFile(FileUnit srcFile, FileUnit dstFile, bool srcStatus, bool dstStatus)
        {
            switch(chkFileUpdate(srcFile, dstFile, srcStatus, dstStatus))
            {
                case SyncAction.CopyFileToSource:
                    string strTarget = dstFile.TargetPath;
                    File.Copy(dstFile.AbsolutePath, strTarget, true);
                    break;
                case SyncAction.CopyFileToTarget:
                    string strTarget = srcFile.TargetPath;
                    File.Copy(srcFile.AbsolutePath, strTarget, true);
                    break;
            }
        }

        static SyncAction chkFileUpdate(FileUnit sFile, FileUnit dFile, bool srcFile, bool dstFile)
        {
            if(scrFile == true && dstFile == false)// source file is dirty,but destination file is not dirty
            {
                return SyncAction.CopyFileToTarget; // copy source file to destination file
            }
            else if(srcFile == false && dstFile == true)// destination file is dirty, but source file is not dirty
            {
                return SyncAction.CopyFileToSource; // copy destination file to source file
            }
            else if (srcFile == true && dstFile == true)// both source file and destination file are dirty
            {
                if (sFile != null && dFile != null)
                {
                    if (sFile.LastWriteTimeUtc > dFile.LastWriteTimeUtc)
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
                    else if(sFile != null && dFile == null)
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
            if(sDirectory == true && dDirectory == false){ // source directory is dirty, but destination directory is not dirty
                return SyncAction.CreateTargetDir; // copy source directory to destination directory
            }
            else if(sDirectory == false && dDirectory == true){ // destination directory is dirty, but source directory is not dirty
                return SyncAction.CreateSourceDir;
            }
            /*
            else if(sDirectory == true && dDirectory == true){ // both directories are dirty
                if(sDirectory.LastWriteTimeUtc > dDirectory.LastWriteTimeUtc){
                    return true;// source directory is the latest, so copy source directory to destination directory
                }
                else
                    return false;// destination directory is the latest, so copy destination directory to source directory
            }
            */
        }
        static SyncAction chkFileDelete(FileInfo sFile, FileInfo dFile)
        {
            if(sFile == null){
                 return SyncAction.DeleteFileFromTarget; //delete from destination
            }               
        
            else if(dFile == null){
                return SyncAction.DeleteFileFromSource; // delete from source
            }
        }

        static SyncAction childCreateParentDelete(FileUnit sFile, FileUnit dFile)
        {
            if(!dFile.Exits && !dFolder.Exits){
                return SyncAction.CopyFileToTarget; // copy all files under source folder to destinatination
            }
            else if(!sFile.Exits && sFolder.Exists){
                return SyncAction.CopyFileToSource; // copy all files under destination folder to source
            }
        }
        static SyncAction folderRename(FileUnit sDirectory, FileUnit dDirectory){
            if(sDirectory.GetHashCode() == dDirectory.GetHashCode() && sDirectory.LastWriteTimeUtc == dDirectory.LastWriteTimeUtc){
                return SyncAction.RenameSourceFile ; // two folders have the same contents
            }
            else{
                return SyncAction.RenameTargetFolder;
            }
           
        }
        static SyncAction fileRename(FileUnit sFile, FileUnit dFile){
            if(sFile.GetHashCode() == dFile.GetHashCode() && sFile.LastWriteTimeUtc == dFile.LastWriteTimeUtc){
                return SyncAction.RenameSourceFile; // two files have the same contents
            }
            else{
                return SyncAction.RenameTargetFile;
            }
            }
        }    
}
