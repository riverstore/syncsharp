using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SyncSharp.DataModel
{
    [Serializable]
    class FileUnit
    {
        #region attributes

        private readonly string _name;
        private readonly string _absolutePath;
        private readonly long _size;
        private readonly DateTime _time;
        private readonly long _hashValue;
        private readonly string _extension;
        private string _flag;

        private readonly bool _isDirectory;

        private FileUnit _match = null;

        #endregion

        public FileUnit(string path)
        {
            _isDirectory = Directory.Exists(path);
            _name = Path.GetFileName(path);
            _absolutePath = path;

            if (_isDirectory)
            {
                _size = 0L;
                _hashValue = 0L;
                _time = Directory.GetLastWriteTime(path);
                _extension = "dir";
            }
            else
            {
                FileInfo file = new FileInfo(path);
                _size = file.Length;
                _time = File.GetLastWriteTime(path);
                _extension = file.Extension;
            }
        }

        #region properties

        public FileUnit Match
        {
            get { return _match; }
            set { _match = value; }
        }

        public string AbsolutePath
        {
            get { return _absolutePath; }
        }

        public string Name
        {
            get { return _name; }
        }

        public long Size
        {
            get { return _size; }
        }

        public long Hash
        {
            get { return _hashValue; }
        }

        public string Extension
        {
            get { return _extension; }
        }

        public DateTime Time
        {
            get { return _time; }
        }

        public bool IsDirectory
        {
            get { return _isDirectory; }
        }

        public string GetFlag
        {
            get { return _flag; }
        }

        public void SetFlag(string f)
        {
            flag = f;
        }

        #endregion
    }
}
