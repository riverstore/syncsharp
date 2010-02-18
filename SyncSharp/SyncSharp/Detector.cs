using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;

namespace SyncSharp.Business
{
    class Detector
    {
        #region attributes

        private List<FileUnit> _filesInSourceOnly;
        private List<FileUnit> _filesInTargetOnly;
        private List<FileUnit> _deleteFilesFrmSource;
        private List<FileUnit> _deleteFilesFrmTarget;
        private List<FileUnit> _conflictFiles;
        
        #endregion

        #region constructors

        public Detector()
        {
            _filesInSourceOnly = new List<FileUnit>();
            _filesInTargetOnly = new List<FileUnit>();
            _deleteFilesFrmSource = new List<FileUnit>();
            _deleteFilesFrmTarget = new List<FileUnit>();
            _conflictFiles = new List<FileUnit>();
        }

        #endregion

        #region properties

        public List<FileUnit> FilesInSourceOnly
        {
            get { return _filesInSourceOnly; }
        }

        public List<FileUnit> FilesInTargetOnly
        {
            get { return _filesInTargetOnly; }
        }

        public List<FileUnit> DeleteFilesFromSource
        {
            get { return _deleteFilesFrmSource; }
        }

        public List<FileUnit> DeleteFilesFromTarget
        {
            get { return _deleteFilesFrmTarget; }
        }

        public List<FileUnit> ConflictFiles
        {
            get { return _conflictFiles; }
        }

        #endregion

        #region Methods

        public void CompareFolderPair(string source, string target, 
            SyncMetaData sMetaData, SyncMetaData tMetaData)
        {
            int sNameLength = source.Length;
            int tNameLength = target.Length;

            Stack<SyncTask> stack = new Stack<SyncTask>();

            stack.Push(new SyncTask(source, target));

            while (stack.Count > 0)
            {
                SyncTask pair = stack.Pop();

                string sourceDir = pair.Source;
                string targetDir = pair.Target;

                /* Get contents from source sub-directory*/
                List<FileUnit> sFiles = new List<FileUnit>();
                List<FileUnit> sDirs = new List<FileUnit>();

                if (Directory.Exists(sourceDir))
                    GetFolderContents(sourceDir, sFiles, sDirs);

                /* Get contents from destination sub-directory*/
                List<FileUnit> tFiles = new List<FileUnit>();
                List<FileUnit> tDirs = new List<FileUnit>();

                if (Directory.Exists(targetDir))
                    GetFolderContents(targetDir, tFiles, tDirs);

                /* Peform file matching between the source and destination files */
                PerformSourceTargetMatching(sFiles, tFiles);

                foreach (FileUnit u in sFiles)
                {
                    string tDir = targetDir + "\\" + u.Name;

                    FileUnit sPrevState = sMetaData.MetaData[u.AbsolutePath];
                    FileUnit tPrevState = sMetaData.MetaData[tDir];

                    if (u.Match == null)
                    {
                        CheckSourceFileConflict(u, sPrevState, tPrevState);
                        u.TargetPath = tDir;
                    }
                    else
                        CheckMatchFilesConflict(u, sPrevState, tPrevState);
                }

                foreach (FileUnit u in tFiles)
                {
                    if (u.Match == null)
                    {
                        string sDir = sourceDir + "\\" + u.Name;
                        FileUnit tPrevState = sMetaData.MetaData[u.AbsolutePath];
                        FileUnit sPrevState = sMetaData.MetaData[sDir];

                        u.TargetPath = sourceDir;
                        CheckTargetFileConflict(u, sPrevState, tPrevState);
                    }
                }

                PerformSourceTargetMatching(sDirs, tDirs);

                /* Recurse into subdirectories */
                foreach (FileUnit u in sDirs)
                {
                    string tDir = targetDir + "\\" + u.Name;

                    if (u.Match == null)
                    {
                        u.TargetPath = tDir;
                        CheckSourceFileConflict(u, null, null);
                    }
                    
                    stack.Push(new SyncTask(u.AbsolutePath, tDir));
                }

                foreach (FileUnit u in tDirs)
                {
                    if (u.Match == null)
                    {
                        string sDir = sourceDir + "\\" + u.Name;
                        u.TargetPath = sDir;
                        CheckTargetFileConflict(u, null, null);

                        stack.Push(new SyncTask(sDir, u.AbsolutePath));
                    }
                }
            }
        }

        private void CheckMatchFilesConflict(FileUnit u, FileUnit sPrevState, FileUnit tPrevState)
        {
            if (sPrevState != null && tPrevState != null)
            {
                // source & target files changed
                if (sPrevState.LastWriteTime != u.LastWriteTime && 
                    tPrevState.LastWriteTime != u.Match.LastWriteTime)
                {
                    this._conflictFiles.Add(u);
                }  
            }
            else
            {
                FileComparator comparator = new FileComparator(true, true, true, true);
                if (comparator.Compare(u, u.Match)!= 0)
                    this._conflictFiles.Add(u);
            }
        }

        private void CheckSourceFileConflict(FileUnit u, FileUnit sPrevState, FileUnit tPrevState)
        {
            if (tPrevState != null && sPrevState != null)
            {
                // target deleted only
                if (sPrevState.LastWriteTime == u.LastWriteTime)
                    this._deleteFilesFrmSource.Add(u);
                // source changed, target deleted
                else if (sPrevState.LastWriteTime < u.LastWriteTime)
                    this._conflictFiles.Add(u);
            }
            else
            {
                this._filesInSourceOnly.Add(u);
            }
        }

        private void CheckTargetFileConflict(FileUnit u, FileUnit sPrevState, FileUnit tPrevState)
        {
            if (tPrevState != null && sPrevState != null)
            {
                // source deleted only
                if (tPrevState.LastWriteTime == u.LastWriteTime)
                    this._deleteFilesFrmTarget.Add(u);
                //source deleted, target changed
                else if (tPrevState.LastWriteTime < u.LastWriteTime)
                    this._conflictFiles.Add(u);
            }
            else
            {
                this._filesInTargetOnly.Add(u);
            }
        }

        private void PerformSourceTargetMatching(List<FileUnit> sFileList,
                                            List<FileUnit> tFileList)
        {
            if (sFileList.Count <= 0 || tFileList.Count <= 0) return;

            FileComparator nameComparator = new FileComparator(true,
                false, false, false);

            tFileList.Sort(nameComparator);

            foreach (FileUnit s in sFileList)
            {
                int i = Array.BinarySearch(tFileList.ToArray(), s, nameComparator);

                if (i >= 0)
                {
                    s.Match = tFileList[i];
                    tFileList[i].Match = s;
                }
              
            }
        }

        private void GetFolderContents(string path, List<FileUnit> files,
                                        List<FileUnit> dirs)
        {
            try
            {
                foreach (string file in Directory.GetFiles(path))
                    files.Add(new FileUnit(file));

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
