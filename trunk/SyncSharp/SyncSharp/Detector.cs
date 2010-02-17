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

        private List<FileUnit> _unMatchedSFiles;
        private List<FileUnit> _unMatchedTFiles;
        private List<FileUnit> _unMatchedSDirs;
        private List<FileUnit> _unMatchedTDirs;
        private List<FileUnit> _matchedFiles;

        #endregion

        public Detector()
        {
            _unMatchedSFiles = new List<FileUnit>();
            _unMatchedTFiles = new List<FileUnit>();
            _unMatchedSDirs = new List<FileUnit>();
            _unMatchedTDirs = new List<FileUnit>();

            _matchedFiles = new List<FileUnit>();
        }

        #region properties

        public List<FileUnit> UnMatchedSourceFiles
        {
            get { return _unMatchedSFiles; }
        }

        public List<FileUnit> UnMatchedTargetFiles
        {
            get { return _unMatchedTFiles; }
        }

        public List<FileUnit> UnMatchedSourceDirs
        {
            get { return _unMatchedSDirs; }
        }

        public List<FileUnit> UnMatchedTargetDirs
        {
            get { return _unMatchedTDirs; }
        }

        public List<FileUnit> MatchedFiles
        {
            get { return _matchedFiles; }
        }

        #endregion

        public void CompareFolderPair(string source, string target)
        {
            int sNameLength = source.Length;
            int tNameLength = target.Length;

            Stack<SyncTask> stack = new Stack<SyncTask>();

            stack.Push(new SyncTask(source, target));

            while (stack.Count > 0)
            {
                SyncTask pair = stack.Pop();

                string sourceDir = pair.Source;
                string sourceDirName = sourceDir.Substring(sNameLength);

                string targetDir = pair.Target;
                string targetDirName = targetDir.Substring(tNameLength);

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
                    if (u.Match == null)
                        _unMatchedSFiles.Add(u);
                    else
                        _matchedFiles.Add(u);
                }

                foreach (FileUnit u in tFiles)
                {
                    if (u.Match == null)
                        _unMatchedTFiles.Add(u);
                }

                PerformSourceTargetMatching(sDirs, tDirs);

                /* Recurse into subdirectories */
                foreach (FileUnit u in sDirs)
                {
                    string tDir = "";

                    if (u.Match == null)
                    {
                        tDir = targetDir + sourceDirName +
                                    Path.DirectorySeparatorChar + u.Name;
                        _unMatchedSDirs.Add(u);
                    }
                    else
                        tDir = u.Match.AbsolutePath;

                    stack.Push(new SyncTask(u.AbsolutePath, tDir));
                }

                foreach (FileUnit u in tDirs)
                {
                    if (u.Match == null)
                    {
                        string sDir = sourceDir + targetDirName +
                                Path.DirectorySeparatorChar + u.Name;
                        _unMatchedTDirs.Add(u);

                        stack.Push(new SyncTask(sDir, u.AbsolutePath));
                    }

                }

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
    }
}
