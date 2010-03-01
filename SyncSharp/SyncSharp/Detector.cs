using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;

namespace SyncSharp.Business
{
    public class Detector
    {
        #region attributes

        private List<FileUnit> newSourceFilesList;
        private List<FileUnit> newTargetFilesList;
        private List<FileUnit> deleteSourceFilesList;
        private List<FileUnit> deleteTargetFilesList;
        private List<FileUnit> conflictFilesList;
        private List<FileUnit> unchangedFilesList;

        private string source, target;

        private Dictionary<string, FileUnit> sMetaData, tMetaData;
        
        #endregion

        #region constructors

        public Detector(string source, string target)
        {
            newSourceFilesList = new List<FileUnit>();
            newTargetFilesList = new List<FileUnit>();
            deleteSourceFilesList = new List<FileUnit>();
            deleteTargetFilesList = new List<FileUnit>();
            conflictFilesList = new List<FileUnit>();
            unchangedFilesList = new List<FileUnit>();

            this.source = source;
            this.target = target;

            sMetaData = SyncMetaData.ReadMetaData(source);
            tMetaData = SyncMetaData.ReadMetaData(target);

            this.CompareFolderPair();
        }

        #endregion

        #region properties

        public List<FileUnit> NewSourceFilesList
        {
            get { return newSourceFilesList; }
        }

        public List<FileUnit> NewTargetFilesList
        {
            get { return newTargetFilesList; }
        }

        public List<FileUnit> DeleteSourceFilesList
        {
            get { return deleteSourceFilesList; }
        }

        public List<FileUnit> UnChangedFilesList
        {
            get { return unchangedFilesList; }
        }

        public List<FileUnit> DeleteTargetFilesList
        {
            get { return deleteTargetFilesList; }
        }

        public List<FileUnit> ConflictFilesList
        {
            get { return conflictFilesList; }
        }

        public string Source
        {
            set { source = value; }
            get { return source; }
        }

        public string Target
        {
            set { target = value; }
            get { return target; }
        }

        public Dictionary<string, FileUnit> SourceMetaData
        {
            set { sMetaData = value; }
            get { return sMetaData; }
        }

        public Dictionary<string, FileUnit> TargetMetaData
        {
            set { tMetaData = value; }
            get { return tMetaData; }
        }

        #endregion

        #region methods

        public bool IsFolderPairSync()
        {
            return (
                newTargetFilesList.Count == 0 &&
                newSourceFilesList.Count == 0 &&
                deleteSourceFilesList.Count == 0 &&
                deleteTargetFilesList.Count == 0 &&
                conflictFilesList.Count == 0
                );
        }

        private void CompareFolderPair()
        {
            Stack<SyncTask> stack = new Stack<SyncTask>();
            stack.Push(new SyncTask(source, target));

            while (stack.Count > 0)
            {
                SyncTask pair = stack.Pop();

                string sDirPath = pair.Source;
                string tDirPath = pair.Target;

                List<FileUnit> sFiles = new List<FileUnit>();
                List<FileUnit> sDirs = new List<FileUnit>();

                if (Directory.Exists(sDirPath))
                    GetFolderContents(sDirPath, sFiles, sDirs);

                List<FileUnit> tFiles = new List<FileUnit>();
                List<FileUnit> tDirs = new List<FileUnit>();

                if (Directory.Exists(tDirPath))
                    GetFolderContents(tDirPath, tFiles, tDirs);

                PerformSourceTargetMatching(sDirs, tDirs, sDirPath, tDirPath, stack);
                PerformSourceTargetMatching(sFiles, tFiles, sDirPath, tDirPath, stack);
            }
        }

        private void ProcessSourceFileUnit(FileUnit s, string sDirPath, 
                                           string tDirPath, Stack<SyncTask> stack)
        {
            s.MatchingPath = tDirPath + "\\" + s.Name;

            FileUnit sLastSync = null, tLastSync = null;

            if (sMetaData != null && tMetaData != null) {
                try
                {
                    sLastSync = sMetaData[s.AbsolutePath];
                    tLastSync = tMetaData[s.MatchingPath];
                }
                catch
                {
                }
            }

            if (s.Match == null)
                CheckSourceFileConflict(s, sLastSync, tLastSync);
            else
            {
                s.Match.MatchingPath = sDirPath + "\\" + s.Name;
                CheckMatchFilesConflict(s, sLastSync, tLastSync);
            }

            if (s.IsDirectory) 
                stack.Push(new SyncTask(s.AbsolutePath, s.MatchingPath));
        }

        private void ProcessTargetFileUnit(FileUnit t, string sDirPath, 
                            string tDirPath, Stack<SyncTask> stack)
        {
            t.MatchingPath = sDirPath + "\\" + t.Name;

            FileUnit sLastSync = null, tLastSync = null;

            if (sMetaData != null && tMetaData != null) {
                try
                {
                    tLastSync = tMetaData[t.AbsolutePath];
                    sLastSync = sMetaData[t.MatchingPath];
                }
                catch
                {
                }
            }

            CheckTargetFileConflict(t, sLastSync, tLastSync);
                
            if (t.IsDirectory) 
                stack.Push(new SyncTask(t.MatchingPath, t.AbsolutePath));
        }

        private void CheckMatchFilesConflict(FileUnit u, FileUnit sLastSync, FileUnit tLastSync)
        {
            if (sLastSync != null && tLastSync != null)
            {
                // source & target files changed
                if (sLastSync.LastWriteTime != u.LastWriteTime &&
                    tLastSync.LastWriteTime != u.Match.LastWriteTime)
                {
                    this.conflictFilesList.Add(u);
                }
                // source change, target unchanged
                else if (sLastSync.LastWriteTime != u.LastWriteTime &&
                    tLastSync.LastWriteTime == u.Match.LastWriteTime)
                {
                    this.newSourceFilesList.Add(u);
                }
                //target changed, source unchanged
                else if (sLastSync.LastWriteTime == u.LastWriteTime &&
                    tLastSync.LastWriteTime != u.Match.LastWriteTime)
                {
                    this.newTargetFilesList.Add(u.Match);
                }
                else
                    this.unchangedFilesList.Add(u);
            }
            else
            {
                FileComparator comparator = new FileComparator(true, true, 
                    true, false);

                if (!u.IsDirectory && (comparator.Compare(u, u.Match) != 0))
                    this.conflictFilesList.Add(u);
                else
                    this.unchangedFilesList.Add(u);
            }
        }

        private void CheckSourceFileConflict(FileUnit u, FileUnit sLastSync, FileUnit tLastSync)
        {
            if (tLastSync != null && sLastSync != null)
            {
                // target deleted only
                if (sLastSync.LastWriteTime == u.LastWriteTime)
                {
                    this.deleteSourceFilesList.Add(u);
                }
                // source changed, target deleted
                else if (sLastSync.LastWriteTime != u.LastWriteTime)
                {
                    this.conflictFilesList.Add(u);
                }
            }
            else
                this.newSourceFilesList.Add(u);
        }

        private void CheckTargetFileConflict(FileUnit u, FileUnit sLastSync, FileUnit tLastSync)
        {
            if (tLastSync != null && sLastSync != null)
            {
                // source deleted only
                if (tLastSync.LastWriteTime == u.LastWriteTime)
                {
                    this.deleteTargetFilesList.Add(u);
                }
                //source deleted, target changed
                else if (tLastSync.LastWriteTime != u.LastWriteTime)
                {
                    this.conflictFilesList.Add(u);
                }
            }
            else
                this.newTargetFilesList.Add(u);
        }

        private void PerformSourceTargetMatching(List<FileUnit> sFileList, List<FileUnit> tFileList,
                                    string sDirPath, string tDirPath, Stack<SyncTask> stack)
        {
            FileComparator nameComparator = new FileComparator(true,
                false, false, false);

            tFileList.Sort(nameComparator);
            FileUnit [] tFiles = tFileList.ToArray(); 

            foreach (FileUnit s in sFileList)
            {
                int i = Array.BinarySearch(tFiles, s, nameComparator);

                if (i >= 0)
                {
                    s.Match = tFileList[i];
                    tFileList[i].Match = s;
                }

                ProcessSourceFileUnit(s, sDirPath, tDirPath, stack);
            }

            foreach (FileUnit t in tFileList)
            {
                if (t.Match == null)
                    ProcessTargetFileUnit(t, sDirPath, tDirPath, stack);
            }
        }

        private void GetFolderContents(string path, List<FileUnit> files, List<FileUnit> dirs)
        {
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {

                    if (!string.Equals(Path.GetFileName(file), "syncsharp.meta"))
                        files.Add(new FileUnit(file));
                }

                foreach (string dir in Directory.GetDirectories(path))
                    dirs.Add(new FileUnit(dir));
            }
            catch
            {
            }
        }

        #endregion
    }
}