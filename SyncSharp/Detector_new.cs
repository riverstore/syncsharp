using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;
using SyncSharp.SyncMetaData;
using CustomDictionary;

namespace SyncSharp.Business
{
    class Detector
    {
        #region attributes

        public CustomDictionary<string, string, FileUnit> sCleanFiles;
        public CustomDictionary<string, string, FileUnit> sDirtyFiles;
        public CustomDictionary<string, string, FileUnit> tCleanFiles;
        public CustomDictionary<string, string, FileUnit> tDirtyFiles;

        #endregion

        public Detector()
        {
            sCleanFiles = new CustomDictionary<string, string, FileUnit>();
            sDirtyFiles = new CustomDictionary<string, string, FileUnit>();
            tCleanFiles = new CustomDictionary<string, string, FileUnit>();
            tDirtyFiles = new CustomDictionary<string, string, FileUnit>();
            sDirtyDirs = new CustomDictionary<string, string, FileUnit>();
            tDirtyDirs = new CustomDictionary<string, string, FileUnit>();
        }

        #region properties

        public CustomDictionary<string, string, FileUnit> SCleanList
        {
            get { return sCleanFiles; }
        }

        public CustomDictionary<string, string, FileUnit> SDirtyList
        {
            get { return sDirtyFiles; }
        }

        public CustomDictionary<string, string, FileUnit> TCleanList
        {
            get { return tCleanFiles; }
        }

        public CustomDictionary<string, string, FileUnit> TDirtyList
        {
            get { return tDirtyFiles; }
        }

        #endregion

        public void CompareFolderPair(string source, string target, SyncMetaData sMetaData, SyncMetaData tMetaData)
        {
            int sRevPathLen = source.Length;
            int tRevPathLen = target.Length;

            Stack<SyncTask> stack = new Stack<SyncTask>();

            stack.Push(new SyncTask(source, target));

            while (stack.Count > 0)
            {
                SyncTask pair = stack.Pop();

                string sDirFull = pair.Source;
                string sDirName = sDirFull.Substring(sRevPathLen);

                string tDirFull = pair.Target;
                string tDirName = tDirFull.Substring(tRevPathLen);

                /* Get contents from source sub-directory*/
                List<FileUnit> srcFiles = new List<FileUnit>();
                List<FileUnit> srcDirs = new List<FileUnit>();

                if (Directory.Exists(sDirFull))
                {
                    GetFolderContents(sDirFull, srcFiles, srcDirs);
                }

                /* Get contents from destination sub-directory*/
                List<FileUnit> tarFiles = new List<FileUnit>();
                List<FileUnit> tarDirs = new List<FileUnit>();

                if (Directory.Exists(tDirFull))
                {
                    GetFolderContents(tDirFull, tarFiles, tarDirs);
                }

                //Catogarize source files
                foreach (FileUnit u in srcFile)
                {
                    FileUnit match = sMetaData.MetaData(u.AbsolutePath);
                    if (match != null)
                    {
                        if (match.Time == u.Time && match.Size == u.Size && match.GetHashCode == u.GetHashCode)
                        {
                            sCleanFiles.add(u.AbsolutePath, u.GetHashCode, u);
                        }

                        else
                        {
                            u.Flag = Flag.modified;
                            sDirtyFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                        }

                        sMetaData.Remove(u.AbsolutePath);
                    }

                    else
                    {
                        u.Flag = Flag.create;
                        sDirtyFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                    }

                    foreach (FileUnit u in sMetaData)
                    {
                        u.Flag = Flag.deleted;
                        sDirtyFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                    }
                }

                //Catogarize destination files
                foreach (FileUnit u in tarFile)
                {
                    FileUnit match = sMetaData.MetaData(u.AbsolutePath);
                    if (match != null)
                    {
                        if (match.Time == u.Time && match.Size == u.Size && match.GetHashCode == u.GetHashCode)
                        {
                            tCleanFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                        }

                        else
                        {
                            u.Flag = Flag.modified;
                            tDirtyFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                        }

                        tMetaData.Remove(u.AbsolutePath);
                    }

                    else
                    {
                        u.Flag = Flag.create;
                        tDirtyFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                    }

                    foreach (FileUnit u in tMetaData)
                    {
                        u.Flag = Flag.deleted;
                        tDirtyFiles.Add(u.AbsolutePath, u.GetHashCode, u);
                    }
                }

                //Categorize source directory
                foreach (FileUnit u in srcDirs)
                {
                    FileUnit match = sMetaData.MetaData(u.AbsolutePath);
                    if (match != null)
                    {
                        sCleanDirs.add(u.AbsolutePath, u.GetHashCode, u);
                        sMetaData.Remove(u.AbsolutePath);
                    }

                    else
                    {
                        u.Flag = Flag.create;
                        sDirtyDirs.Add(u.AbsolutePath, u.GetHashCode, u);
                    }

                    foreach (FileUnit u in sMetaData)
                    {
                        u.Flag = Flag.deleted;
                        sDirtyDirs.Add(u.AbsolutePath, u.GetHashCode, u);
                    }
                }

                //Categorize destination folders
                foreach (FileUnit u in tarDirs)
                {
                    FileUnit match = sMetaData.MetaData(u.AbsolutePath);
                    if (match != null)
                    {
                        tCleanDirs.Add(u.AbsolutePath, u.GetHashCode, u);
                        tMetaData.Remove(u.AbsolutePath);
                    }

                    else
                    {
                        u.Flag = Flag.create;
                        tDirtyDirs.Add(u.AbsolutePath, u.GetHashCode, u);
                    }

                    foreach (FileUnit u in tMetaData)
                    {
                        u.Flag = Flag.deleted;
                        tDirtyDirs.Add(u.AbsolutePath, u.GetHashCode, u);
                    }
                }
            }
        }

        private void GetFolderContents(string path, List<FileUnit> files, List<FileUnit> dirs)
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
