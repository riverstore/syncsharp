using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.Storage;
using SyncSharp.DataModel;
using System.IO;

namespace SyncSharp.Business
{
	public class Detector
	{
        private CustomDictionary<string, string, FileUnit> sCleanFiles;
        private CustomDictionary<string, string, FileUnit> sDirtyFiles;
        private CustomDictionary<string, string, FileUnit> tCleanFiles;
        private CustomDictionary<string, string, FileUnit> tDirtyFiles;
        private CustomDictionary<string, string, FileUnit> sDirtyDirs;
        private CustomDictionary<string, string, FileUnit> sCleanDirs;
        private CustomDictionary<string, string, FileUnit> tDirtyDirs;
        private CustomDictionary<string, string, FileUnit> tCleanDirs;
        private List<String> fileExclusions;
        private FileList sourceList;
        private FileList destList;
        private SyncTask task;
        private long srcDirtySize, tgtDirtySize;
        private bool openFileDetected;
        private CustomDictionary<string, string, FileUnit> sMetaData, tMetaData;
        private List<FileUnit> srcFiles = new List<FileUnit>();
        private List<FileUnit> destFiles = new List<FileUnit>();

        public CustomDictionary<string, string, FileUnit> SCleanFiles
        {
            get { return sCleanFiles; }
            set { sCleanFiles = value; }
        }
        public CustomDictionary<string, string, FileUnit> SDirtyFiles
        {
            get { return sDirtyFiles; }
            set { sDirtyFiles = value; }
        }
        public CustomDictionary<string, string, FileUnit> TCleanFiles
        {
            get { return tCleanFiles; }
            set { tCleanFiles = value; }
        }
        public CustomDictionary<string, string, FileUnit> TDirtyFiles
        {
            get { return tDirtyFiles; }
            set { tDirtyFiles = value; }
        }
        public CustomDictionary<string, string, FileUnit> SDirtyDirs
        {
            get { return sDirtyDirs; }
            set { sDirtyDirs = value; }
        }
        public CustomDictionary<string, string, FileUnit> SCleanDirs
        {
            get { return sCleanDirs; }
            set { sCleanDirs = value; }
        }
        public CustomDictionary<string, string, FileUnit> TDirtyDirs
        {
            get { return tDirtyDirs; }
            set { tDirtyDirs = value; }
        }
        public CustomDictionary<string, string, FileUnit> TCleanDirs
        {
            get { return tCleanDirs; }
            set { tCleanDirs = value; }
        }
        public List<String> FileExclusions
        {
            get { return fileExclusions; }
            set { fileExclusions = value; }
        }
        public FileList SourceList
        {
            get { return sourceList; }
            set { sourceList = value; }
        }
        public FileList DestList
        {
            get { return destList; }
            set { destList = value; }
        }
        public SyncTask Task
        {
            get { return task; }
            set { task = value; }
        }
        public long SrcDirtySize
        {
            get { return srcDirtySize; }
            set { srcDirtySize = value; }
        }
        public long TgtDirtySize
        {
            get { return tgtDirtySize; }
            set { tgtDirtySize = value; }
        }
        public bool OpenFileDetected
        {
            get { return openFileDetected; }
            set { openFileDetected = value; }
        }
        public CustomDictionary<string, string, FileUnit> SMetaData
        {
            get { return sMetaData; }
            set { sMetaData = value; }
        }
        public CustomDictionary<string, string, FileUnit> TMetaData
        {
            get { return tMetaData; }
            set { tMetaData = value; }
        }
        public List<FileUnit> SrcFiles
        {
            get { return srcFiles; }
            set { srcFiles = value; }
        }
        public List<FileUnit> DestFiles
        {
            get { return destFiles; }
            set { destFiles = value; }
        }

        public Detector(String metaDataDir, SyncTask syncTask)
        {
            sCleanFiles = new CustomDictionary<string, string, FileUnit>();
            sDirtyFiles = new CustomDictionary<string, string, FileUnit>();
            tCleanFiles = new CustomDictionary<string, string, FileUnit>();
            tDirtyFiles = new CustomDictionary<string, string, FileUnit>();
            sDirtyDirs = new CustomDictionary<string, string, FileUnit>();
            sCleanDirs = new CustomDictionary<string, string, FileUnit>();
            tDirtyDirs = new CustomDictionary<string, string, FileUnit>();
            tCleanDirs = new CustomDictionary<string, string, FileUnit>();
            fileExclusions = new List<string>();
            task = syncTask;
            sMetaData = SyncMetaData.ReadMetaData(metaDataDir + @"\" + syncTask.Name + "src.meta");
            tMetaData = SyncMetaData.ReadMetaData(metaDataDir + @"\" + syncTask.Name + "tgt.meta");
            srcDirtySize = 0;
            tgtDirtySize = 0;
        }

        public bool isSynchronized()
        {
            return (sDirtyFiles.Primary.Count == 0 && tDirtyFiles.Primary.Count == 0 && sDirtyDirs.Primary.Count == 0 &&
                            tDirtyDirs.Primary.Count == 0);
        }

        public bool metaDataExists()
        {
            return (sMetaData != null && tMetaData != null);
        }

        public void compareFolders()
        {
            int sRevPathLen = task.Source.Length;
            int tRevPathLen = task.Target.Length;

            Stack<string> stack = new Stack<string>();

            stack.Push(task.Source);
            getCurrentSrcInfo(srcFiles, stack);

            stack.Push(task.Target);
            getCurrentTgtInfo(destFiles, stack);

            removeExclusions(sRevPathLen, tRevPathLen);

            compareSrcFileUnits(sRevPathLen, srcFiles, destFiles);
            compareTgtFileUnits(tRevPathLen, destFiles, srcFiles);

            addSrcDeletionToList();
            addTgtDeletionToList();

            createFileLists();
        }

        private void removeExclusions(int sRevPathLen, int tRevPathLen)
        {
            List<FileUnit> tempSrcFiles = new List<FileUnit>();
            List<FileUnit> tempDestFiles = new List<FileUnit>();

            #region removeSrcExclusions
            foreach (FileUnit u in srcFiles)
            {
                String folderRelativePath = u.AbsolutePath.Substring(sRevPathLen);
                if (u.IsDirectory)
                {
                    if (!task.Filters.isSourceDirExcluded(u.AbsolutePath))
                        tempSrcFiles.Add(u);
                    else
                    {
                        if (sMetaData != null && sMetaData.containsPriKey(folderRelativePath))
                            sMetaData.removeByPrimary(folderRelativePath);
                        u.Hash = folderRelativePath;
                        sCleanDirs.add(folderRelativePath, u.Hash, u);
                    }
                }
                else
                {
                    String relativePath = u.AbsolutePath.Substring(sRevPathLen);
                    bool targetExcluded = false;
                    if (File.Exists(task.Target + relativePath))
                        targetExcluded = task.Filters.isFileExcluded(new FileInfo(task.Target + relativePath));
                    if (!task.Filters.isFileExcluded(new FileInfo(u.AbsolutePath)) && !task.Filters.isSourceDirExcluded(u.AbsolutePath)
                        && !targetExcluded)
                    {
                        tempSrcFiles.Add(u);
                    }
                    else
                    {
                        if (sMetaData != null && sMetaData.containsPriKey(relativePath))
                            sMetaData.removeByPrimary(relativePath);
                        if (tMetaData != null && tMetaData.containsPriKey(relativePath))
                            tMetaData.removeByPrimary(relativePath);
                        fileExclusions.Add(relativePath);
                        u.Hash = Utility.computeMyHash(u);
                        sCleanFiles.add(relativePath, u.Hash, u);
                        if (File.Exists(task.Target + relativePath))
                        {
                            FileUnit targetUnit = new FileUnit(task.Target + relativePath);
                            targetUnit.Hash = Utility.computeMyHash(targetUnit);
                            tCleanFiles.add(relativePath, targetUnit.Hash, targetUnit);
                        }
                    }
                }
            }
            #endregion

            #region removeTgtExclusions
            foreach (FileUnit u in destFiles)
            {
                String folderRelativePath = u.AbsolutePath.Substring(tRevPathLen);
                if (u.IsDirectory)
                {
                    if (!task.Filters.isTargetDirExcluded(u.AbsolutePath))
                        tempDestFiles.Add(u);
                    else
                    {
                        if (tMetaData != null && tMetaData.containsPriKey(folderRelativePath))
                            tMetaData.removeByPrimary(folderRelativePath);
                        u.Hash = folderRelativePath;
                        tCleanDirs.add(folderRelativePath, u.Hash, u);
                    }
                }
                else
                {
                    String relativePath = u.AbsolutePath.Substring(tRevPathLen);
                    //if (!task.Filters.isFileExcluded(new FileInfo(u.AbsolutePath)) && !task.Filters.isTargetDirExcluded(u.AbsolutePath) &&
                    //  !fileExclusions.Contains(relativePath))
                    //{

                    if (!fileExclusions.Contains(relativePath))
                        tempDestFiles.Add(u);
                    //  }
                    //  else
                    //  {
                    //    if (sMetaData.containsPriKey(relativePath))
                    //      sMetaData.removeByPrimary(relativePath);
                    //    if (tMetaData.containsPriKey(relativePath))
                    //      tMetaData.removeByPrimary(relativePath);
                    //    fileExclusions.Add(relativePath);
                    //    u.Hash = Utility.computeMyHash(u);
                    //    tCleanFiles.add(relativePath, u.Hash, u);
                    //  }
                }
            }
            #endregion

            srcFiles = tempSrcFiles;
            destFiles = tempDestFiles;
        }

        private void getCurrentSrcInfo(List<FileUnit> srcFiles, Stack<string> stack)
        {
            while (stack.Count > 0)
            {
                string folder = stack.Pop();
                foreach (string fileName in Directory.GetFiles(folder))
                {
                    try
                    {
                        FileStream stream = File.OpenRead(fileName);
                        stream.Close();
                        //if (!task.Filters.isFileExcluded(new FileInfo(fileName)))
                        srcFiles.Add(new FileUnit(fileName));
                    }
                    catch
                    {
                        openFileDetected = true;
                        break;
                    }
                }

                foreach (string folderName in Directory.GetDirectories(folder))
                {
                    //if (!task.Filters.isSourceDirExcluded(folderName))
                    //{
                    stack.Push(folderName);
                    srcFiles.Add(new FileUnit(folderName));
                    //}
                    //else
                    //{
                    //  sMetaData.removeByPrimary(folderName);
                    //}
                }
            }
        }

        private void getCurrentTgtInfo(List<FileUnit> destFiles, Stack<string> stack)
        {
            while (stack.Count > 0)
            {
                string folder = stack.Pop();
                foreach (string fileName in Directory.GetFiles(folder))
                {
                    try
                    {
                        FileStream stream = File.OpenRead(fileName);
                        stream.Close();
                        //if (!task.Filters.isFileExcluded(new FileInfo(fileName)))
                        destFiles.Add(new FileUnit(fileName));
                    }
                    catch
                    {
                        openFileDetected = true;
                        break;
                    }
                }

                foreach (string folderName in Directory.GetDirectories(folder))
                {
                    //if (!task.Filters.isTargetDirExcluded(folderName))
                    //{
                    stack.Push(folderName);
                    destFiles.Add(new FileUnit(folderName));
                    //}
                    //else
                    //{
                    //  tMetaData.removeByPrimary(folderName);
                    //}
                }
            }
        }

        private void compareSrcFileUnits(int sRevPathLen, List<FileUnit> srcFiles, List<FileUnit> destFiles)
        {
            foreach (FileUnit u in srcFiles)
            {
                String folderRelativePath = u.AbsolutePath.Substring(sRevPathLen);
                if (u.IsDirectory)
                {
                    //if (!task.Filters.isSourceDirExcluded(u.AbsolutePath))
                    compareSrcDirs(u, folderRelativePath);
                    //else
                    //{
                    //  sMetaData.removeByPrimary(folderRelativePath);
                    //  u.Hash = folderRelativePath;
                    //  sCleanDirs.add(folderRelativePath, u.Hash, u);
                    //}
                }
                else
                {
                    String relativePath = u.AbsolutePath.Substring(sRevPathLen);
                    //if (!task.Filters.isFileExcluded(new FileInfo(u.AbsolutePath)) && !task.Filters.isSourceDirExcluded(u.AbsolutePath))
                    //{
                    compareSrcFiles(u, relativePath);
                    //}
                    //else
                    //{
                    //  if (sMetaData.containsPriKey(relativePath))
                    //    sMetaData.removeByPrimary(relativePath);
                    //  if (tMetaData.containsPriKey(relativePath))
                    //    tMetaData.removeByPrimary(relativePath);
                    //  fileExclusions.Add(relativePath);
                    //  u.Hash = Utility.computeMyHash(u);
                    //  sCleanFiles.add(relativePath, u.Hash, u);
                    //}
                }
            }
        }

        private void compareSrcDirs(FileUnit u, String folderRelativePath)
        {
            if (metaDataExists())
            {
                if (sMetaData.Primary.ContainsKey(folderRelativePath))
                {
                    u.Hash = folderRelativePath;
                    sCleanDirs.add(folderRelativePath, u.Hash, u);
                    sMetaData.removeByPrimary(folderRelativePath);
                }
                else
                {
                    u.Hash = "C-" + folderRelativePath;
                    sDirtyDirs.add(folderRelativePath, u.Hash, u);
                }
            }
            else
            {
                u.Hash = "C-" + folderRelativePath;
                sDirtyDirs.add(folderRelativePath, u.Hash, u);
            }
        }

        private void compareSrcFiles(FileUnit u, String relativePath)
        {
            if (metaDataExists())
            {
                if (sMetaData.Primary.ContainsKey(relativePath))
                {
                    if ((u.LastWriteTime - sMetaData.getByPrimary(relativePath).LastWriteTime).Duration().TotalSeconds <= task.Settings.IgnoreTimeChange)
                    {
                        sCleanFiles.add(relativePath, sMetaData.PriSub[relativePath], u);
                        sMetaData.removeByPrimary(relativePath);
                    }
                    else
                    {
                        srcDirtySize += u.Size;
                        u.Hash = "M-" + sMetaData.PriSub[relativePath];
                        sDirtyFiles.add(relativePath, u.Hash, u);
                        sMetaData.removeByPrimary(relativePath);
                    }
                }
                else
                {
                    srcDirtySize += u.Size;
                    u.Hash = "C-" + Utility.computeMyHash(u);
                    sDirtyFiles.add(relativePath, u.Hash, u);
                }
            }
            else
            {
                srcDirtySize += u.Size;
                u.Hash = "C-" + Utility.computeMyHash(u);
                sDirtyFiles.add(relativePath, u.Hash, u);
            }
        }

        private void compareTgtFileUnits(int tRevPathLen, List<FileUnit> destFiles, List<FileUnit> srcFiles)
        {
            foreach (FileUnit u in destFiles)
            {
                String folderRelativePath = u.AbsolutePath.Substring(tRevPathLen);
                if (u.IsDirectory)
                {
                    //if (!task.Filters.isTargetDirExcluded(u.AbsolutePath))
                    compareTgtDirs(u, folderRelativePath);
                    //else
                    //{
                    //  tMetaData.removeByPrimary(folderRelativePath);
                    //  u.Hash = folderRelativePath;
                    //  tCleanDirs.add(folderRelativePath, u.Hash, u);
                    //}
                }
                else
                {
                    String relativePath = u.AbsolutePath.Substring(tRevPathLen);
                    //if (!task.Filters.isFileExcluded(new FileInfo(u.AbsolutePath)) && !task.Filters.isTargetDirExcluded(u.AbsolutePath))
                    //{
                    compareTgtFiles(u, relativePath);
                    //}
                    //else
                    //{
                    //  if (sMetaData.containsPriKey(relativePath))
                    //    sMetaData.removeByPrimary(relativePath);
                    //  if (tMetaData.containsPriKey(relativePath))
                    //    tMetaData.removeByPrimary(relativePath);
                    //  fileExclusions.Add(relativePath);
                    //  u.Hash = Utility.computeMyHash(u);
                    //  sCleanFiles.add(relativePath, u.Hash, u);
                    //}
                }
            }
        }

        private void compareTgtDirs(FileUnit u, String folderRelativePath)
        {
            if (metaDataExists())
            {
                if (tMetaData.Primary.ContainsKey(folderRelativePath))
                {
                    u.Hash = folderRelativePath;
                    tCleanDirs.add(folderRelativePath, u.Hash, u);
                    tMetaData.removeByPrimary(folderRelativePath);
                }
                else
                {
                    u.Hash = "C-" + folderRelativePath;
                    tDirtyDirs.add(folderRelativePath, u.Hash, u);
                }
            }
            else
            {
                u.Hash = "C-" + folderRelativePath;
                tDirtyDirs.add(folderRelativePath, u.Hash, u);
            }
        }

        private void compareTgtFiles(FileUnit u, String relativePath)
        {
            if (metaDataExists())
            {
                if (tMetaData.Primary.ContainsKey(relativePath))
                {
                    if ((u.LastWriteTime - tMetaData.getByPrimary(relativePath).LastWriteTime).Duration().TotalSeconds <= task.Settings.IgnoreTimeChange)
                    {
                        u.Hash = tMetaData.PriSub[relativePath];
                        tCleanFiles.add(relativePath, u.Hash, u);
                        tMetaData.removeByPrimary(relativePath);
                    }
                    else
                    {
                        tgtDirtySize += u.Size;
                        u.Hash = "M-" + tMetaData.PriSub[relativePath];
                        tDirtyFiles.add(relativePath, u.Hash, u);
                        tMetaData.removeByPrimary(relativePath);
                    }
                }
                else
                {
                    tgtDirtySize += u.Size;
                    u.Hash = "C-" + Utility.computeMyHash(u);
                    tDirtyFiles.add(relativePath, u.Hash, u);
                }
            }
            else
            {
                tgtDirtySize += u.Size;
                u.Hash = "C-" + Utility.computeMyHash(u);
                tDirtyFiles.add(relativePath, u.Hash, u);
            }
        }

        private void addSrcDeletionToList()
        {
            if (metaDataExists())
            {
                foreach (var item in sMetaData.Primary)
                {
                    if (item.Value.IsDirectory)
                    {
                        item.Value.Hash = "D-" + item.Key;
                        sDirtyDirs.add(item.Key, item.Value.Hash, item.Value);
                    }
                    else
                    {
                        item.Value.Hash = "D-" + sMetaData.PriSub[item.Key];
                        sDirtyFiles.add(item.Key, item.Value.Hash, item.Value);
                    }
                }
            }
        }

        private void addTgtDeletionToList()
        {
            if (metaDataExists())
            {
                foreach (var item in tMetaData.Primary)
                {
                    if (item.Value.IsDirectory)
                    {
                        item.Value.Hash = "D-" + item.Key;
                        tDirtyDirs.add(item.Key, item.Value.Hash, item.Value);
                    }
                    else
                    {
                        item.Value.Hash = "D-" + tMetaData.PriSub[item.Key];
                        tDirtyFiles.add(item.Key, item.Value.Hash, item.Value);
                    }
                }
            }
        }

        private void createFileLists()
        {
            sourceList = new FileList(sCleanFiles, sDirtyFiles, sDirtyDirs, sCleanDirs);
            destList = new FileList(tCleanFiles, tDirtyFiles, tDirtyDirs, tCleanDirs);
        }

        public FileList getSrcList()
        {
            return sourceList;
        }

        public FileList getDestList()
        {
            return destList;
        }
	}
}