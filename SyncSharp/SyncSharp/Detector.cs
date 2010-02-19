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

        private List<FileUnit> _copyToTargetList;
        private List<FileUnit> _copyToSourceList;
        private List<FileUnit> _deleteFilesFrmSourceList;
        private List<FileUnit> _deleteFilesFrmTargetList;
        private List<FileUnit> _conflictFilesList;

        private string _source, _target;
        
        #endregion

        #region constructors

        public Detector()
        {
            _copyToTargetList = new List<FileUnit>();
            _copyToSourceList = new List<FileUnit>();
            _deleteFilesFrmSourceList = new List<FileUnit>();
            _deleteFilesFrmTargetList = new List<FileUnit>();
            _conflictFilesList = new List<FileUnit>();
        }

        #endregion

        #region properties

        public List<FileUnit> CopyToTargetList
        {
            get { return _copyToTargetList; }
        }

        public List<FileUnit> CopyToSourceList
        {
            get { return _copyToSourceList; }
        }

        public List<FileUnit> DeleteFilesFrmSourceList
        {
            get { return _deleteFilesFrmSourceList; }
        }

        public List<FileUnit> DeleteFilesFrmTargetList
        {
            get { return _deleteFilesFrmTargetList; }
        }

        public List<FileUnit> ConflictFilesList
        {
            get { return _conflictFilesList; }
        }

        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }

        public string Target
        {
            set { _target = value; }
            get { return _target; }
        }

        #endregion

        #region Methods

        public void CompareFolderPair(string source, string target, 
            SyncMetaData sMetaData, SyncMetaData tMetaData)
        {
            Source = source; Target = target;
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

                PerformSourceTargetMatching(sFiles, tFiles);
                PerformSourceTargetMatching(sDirs, tDirs);

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

                foreach (FileUnit u in sFiles)
                {
                    string tDir = targetDir + "\\" + u.Name;

                    FileUnit sPrevState = null;
                    FileUnit tPrevState = null;

                    if (sMetaData != null && tMetaData != null)
                    {
                        try
                        {
                            sPrevState = sMetaData.MetaData[u.AbsolutePath];
                            tPrevState = tMetaData.MetaData[tDir];
                        }
                        catch
                        {
                        }
                    }

                    u.TargetPath = tDir;
                    if (u.Match == null)
                        CheckSourceFileConflict(u, sPrevState, tPrevState);
                    else
                    {
                        u.Match.TargetPath = sourceDir + "\\" + u.Name;
                        CheckMatchFilesConflict(u, sPrevState, tPrevState);
                    }
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

                foreach (FileUnit u in tFiles)
                {
                    if (u.Match == null)
                    {
                        string sDir = sourceDir + "\\" + u.Name;

                         FileUnit sPrevState = null;
                         FileUnit tPrevState = null;

                        if (sMetaData != null && tMetaData != null)
                        {
                            try
                            {
                                tPrevState = tMetaData.MetaData[u.AbsolutePath];
                                sPrevState = sMetaData.MetaData[sDir];
                            }
                            catch
                            {
                            }
                        }

                        u.TargetPath = sDir;
                        CheckTargetFileConflict(u, sPrevState, tPrevState);
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
                        this._conflictFilesList.Add(u);
                }
                // source change, target unchanged
                else if (sPrevState.LastWriteTime != u.LastWriteTime &&
                    tPrevState.LastWriteTime == u.Match.LastWriteTime)
                    this._copyToTargetList.Add(u);
                //target changed, source unchanged
                else if (sPrevState.LastWriteTime == u.LastWriteTime &&
                    tPrevState.LastWriteTime != u.Match.LastWriteTime)
                    this._copyToSourceList.Add(u.Match);
            }
            else
            {
                FileComparator comparator = new FileComparator(true, true, true, true);
                if (comparator.Compare(u, u.Match) != 0)
                {
                        this._conflictFilesList.Add(u);
                }
            }
        }

        private void CheckSourceFileConflict(FileUnit u, FileUnit sPrevState, FileUnit tPrevState)
        {
            if (tPrevState != null && sPrevState != null)
            {
                // target deleted only
                if (sPrevState.LastWriteTime == u.LastWriteTime)
                {
                        this._deleteFilesFrmSourceList.Add(u);
                }
                // source changed, target deleted
                else if (sPrevState.LastWriteTime < u.LastWriteTime)
                {
                        this._conflictFilesList.Add(u);
                }
            }
            else
            {
                  this._copyToTargetList.Add(u);
            }
        }

        private void CheckTargetFileConflict(FileUnit u, FileUnit sPrevState, FileUnit tPrevState)
        {
            if (tPrevState != null && sPrevState != null)
            {
                // source deleted only
                if (tPrevState.LastWriteTime == u.LastWriteTime)
                {
                        this._deleteFilesFrmTargetList.Add(u);
                }
                //source deleted, target changed
                else if (tPrevState.LastWriteTime < u.LastWriteTime)
                {
                        this._conflictFilesList.Add(u);
                }
            }
            else
            {
                  this._copyToSourceList.Add(u);
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