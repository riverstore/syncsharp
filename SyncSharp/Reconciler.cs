using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;

namespace SyncSharp.Business
{
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
        SkipNExclude,
        CollisionPromptUser
    }

    static class SynActionExtension
    {
        public static string Text(this SyncAction action)
        {
            string retVal = "";
            switch (action)
            {
                case SyncAction.CopyFileToSource:
                    retVal = "Copy To Source";
                    break;
                case SyncAction.CopyFileToTarget:
                    retVal = "Copy To Target";
                    break;
                case SyncAction.DeleteBothFile:
                    retVal = "Delete Both";
                    break;
                case SyncAction.DeleteSourceFile:
                    retVal = "Delete from Source";
                    break;
                case SyncAction.DeleteTargetFile:
                    retVal = "Delete from Target";
                    break;
                case SyncAction.SkipNExclude:
                    retVal = "Skip and Exclude";
                    break;
                case SyncAction.CollisionPromptUser:
                    retVal = "Collision, prompt me";
                    break;
            }
            return retVal;
        }
    }

    public class Reconciler
    {
        public static void update(Detector detector, TaskSettings settings)
        {
            Dictionary<string, FileUnit> sMeta = new Dictionary<string, FileUnit>();
            Dictionary<string, FileUnit> tMeta = new Dictionary<string, FileUnit>();

            foreach (FileUnit u in detector.NewSourceFilesList)
            {
                if (!u.IsDirectory)
                    updateFile(u, null, true, false, sMeta, tMeta);
                else
                    updateDir(u, null, true, false, sMeta, tMeta);
            }

            foreach (FileUnit u in detector.NewTargetFilesList)
            {
                if (!u.IsDirectory)
                    updateFile(null, u, false, true, sMeta, tMeta);
                else
                    updateDir(null, u, false, true, sMeta, tMeta);
            }

            foreach (FileUnit u in detector.ConflictFilesList)
            {
                if (u.Match != null)
                    updateFile(u, u.Match, true, true, sMeta, tMeta);
                else
                {
                    if (u.AbsolutePath.StartsWith(detector.Source))
                        updateFile(u, null, true, true, sMeta, tMeta);
                    else
                        updateFile(null, u, true, true, sMeta, tMeta);
                }
            }

            detector.DeleteSourceFilesList.Reverse();
            foreach (FileUnit u in detector.DeleteSourceFilesList)
            {
                switch (chkFileDelete(u, null))
                {
                    case SyncAction.DeleteSourceFile:
                        if (!u.IsDirectory) 
                            File.Delete(u.AbsolutePath);
                        deleteEmptyFolders(u);
                        break;
                }
            }

            detector.DeleteTargetFilesList.Reverse();
            foreach (FileUnit u in detector.DeleteTargetFilesList)
            {
                switch (chkFileDelete(null, u))
                {
                    case SyncAction.DeleteTargetFile:
                        if (!u.IsDirectory)
                            File.Delete(u.AbsolutePath);
                        deleteEmptyFolders(u);
                        break;
                }
            }

            detector.UnChangedFilesList.Reverse();
            foreach (FileUnit u in detector.UnChangedFilesList)
            {
                sMeta.Add(u.AbsolutePath, u);
                tMeta.Add(u.Match.AbsolutePath, u.Match);
                u.Match.Match = null;
                u.Match = null;
                
                if (u.IsDirectory)
                    deleteEmptyFolders(u);
            }

            SyncMetaData.WriteMetaData(detector.Source, sMeta);
            SyncMetaData.WriteMetaData(detector.Target, tMeta);
        }

        static void deleteEmptyFolders(FileUnit u)
        {
            string sDir = (u.IsDirectory) ? u.AbsolutePath : 
                Directory.GetParent(u.AbsolutePath).FullName;
            string tDir = (u.IsDirectory) ? u.MatchingPath : 
                Directory.GetParent(u.MatchingPath).FullName;

            try
            {
                Directory.Delete(sDir);
                Directory.Delete(tDir);
            }
            catch
            {
            }
        }

        static void updateFile(FileUnit sFile, FileUnit tFile, bool isSDirty,
            bool isTDirty, Dictionary<string, FileUnit> sMeta,
            Dictionary<string, FileUnit> tMeta)
        {
            switch (chkFileUpdate(sFile, tFile, isSDirty, isTDirty))
            {
                case SyncAction.CopyFileToSource:
                    
                    string parent = Directory.GetParent(tFile.MatchingPath).FullName;
                    
                    if (!Directory.Exists(parent))
                        Directory.CreateDirectory(parent);

                    File.Copy(tFile.AbsolutePath, tFile.MatchingPath, true);
                    tMeta.Add(tFile.AbsolutePath, tFile);
                    FileUnit newSource = new FileUnit(tFile.MatchingPath);
                    sMeta.Add(newSource.AbsolutePath, newSource);

                    break;
                case SyncAction.CopyFileToTarget:

                    parent = Directory.GetParent(sFile.MatchingPath).FullName;

                    if (!Directory.Exists(parent))
                        Directory.CreateDirectory(parent);
                    
                    File.Copy(sFile.AbsolutePath, sFile.MatchingPath, true);
                    sMeta.Add(sFile.AbsolutePath, sFile);
                    FileUnit newTarget = new FileUnit(sFile.MatchingPath);
                    tMeta.Add(newTarget.AbsolutePath, newTarget);
                    
                    break;
            }
        }

        public static SyncAction chkFileUpdate(FileUnit sFile, FileUnit tFile, bool isSDirty, bool isTDirty)
        {
            if (isSDirty == true && isTDirty == false)
                return SyncAction.CopyFileToTarget;
            else if (isSDirty == false && isTDirty == true)
                return SyncAction.CopyFileToSource;
            else
            {
                if (sFile != null && tFile != null)
                {
                    if (sFile.LastWriteTime > tFile.LastWriteTime)
                        return SyncAction.CopyFileToTarget;
                    else
                        return SyncAction.CopyFileToSource;
                }
                else
                {
                    if (sFile == null && tFile != null)
                        return SyncAction.CopyFileToSource;
                    else
                        return SyncAction.CopyFileToTarget;
                }
            }
        }

        static void updateDir(FileUnit sDir, FileUnit tDir, bool isSDirty,
            bool isTDirty, Dictionary<string, FileUnit> sMeta,
            Dictionary<string, FileUnit> tMeta)
        {
            switch (chkDirUpdate(sDir, tDir, isSDirty, isTDirty))
            {
                case SyncAction.CreateSourceDir:

                    Directory.CreateDirectory(tDir.MatchingPath);
                    tMeta.Add(tDir.AbsolutePath, tDir);
                    FileUnit newSource = new FileUnit(tDir.MatchingPath);
                    sMeta.Add(newSource.AbsolutePath, newSource);
                    
                    break;
                case SyncAction.CreateTargetDir:
                    
                    Directory.CreateDirectory(sDir.MatchingPath);
                    sMeta.Add(sDir.AbsolutePath, sDir);
                    FileUnit newTarget = new FileUnit(sDir.MatchingPath);
                    tMeta.Add(newTarget.AbsolutePath, newTarget);
                    
                    break;
            }
        }

        public static SyncAction chkDirUpdate(FileUnit sDir, FileUnit tDir, bool isSDirty, bool isTDirty)
        {
            if (sDir != null && tDir == null)
                return SyncAction.CreateTargetDir;
            else
                return SyncAction.CreateSourceDir;
        }

        static SyncAction chkFileDelete(FileUnit sFile, FileUnit tFile)
        {
            if (sFile == null)
                return SyncAction.DeleteTargetFile;
            else
                return SyncAction.DeleteSourceFile;

        }

        static SyncAction fileRename(FileUnit sFile, FileUnit tFile)
        {
            if (sFile.LastWriteTime < tFile.LastWriteTime)
                return SyncAction.RenameSourceFile;
            else
                return SyncAction.RenameTargetFile;
        }
    }
}