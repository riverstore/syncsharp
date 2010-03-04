using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SyncSharp.DataModel
{
    [Serializable]
    public class FileUnit
    {
        #region attributes

        private readonly string name;
        private readonly string absolutePath;
        private readonly long size;
        private readonly DateTime lastWriteTime;
        private readonly long hashValue;
        private readonly string extension;
        private string matchingPath;

        private readonly bool isDirectory;

        private FileUnit match = null;

        #endregion

        #region constructors

        public FileUnit(string path)
        {
            isDirectory = Directory.Exists(path);
            name = Path.GetFileName(path);
            absolutePath = path;
            matchingPath = "";

            if (isDirectory)
            {
                size = 0L;
                hashValue = 0L;
                //lastWriteTime = Directory.GetLastWriteTime(path);
                lastWriteTime = DateTime.MinValue;
                extension = "dir";
            }
            else
            {
                FileInfo file = new FileInfo(path);
                size = file.Length;
                lastWriteTime = File.GetLastWriteTime(path);
                extension = file.Extension;
            }
        }

        #endregion

        #region properties

        public string MatchingPath
        {
            set { matchingPath = value; }
            get { return matchingPath; }
        }

        public FileUnit Match
        {
            get { return match; }
            set { match = value; }
        }

        public string AbsolutePath
        {
            get { return absolutePath; }
        }

        public string Name
        {
            get { return name; }
        }

        public long Size
        {
            get { return size; }
        }

        public long Hash
        {
            get { return hashValue; }
        }

        public string Extension
        {
            get { return extension; }
        }

        public DateTime LastWriteTime
        {
            get { return lastWriteTime; }
        }

        public bool IsDirectory
        {
            get { return isDirectory; }
        }

        #endregion

    }
}
