using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SyncSharp.Business
{
	[Serializable]
    public class Filters
    {
        // Data Members
        private List<String> fileIncludeList, fileExcludeList,
                             srcFolderExcludeList, tgtFolderExcludeList;

        private bool hidden, readOnly, system, temp;

		// Properties
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }
        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }
        public bool System
        {
            get { return system; }
            set { system = value; }
        }
        public bool Temp
        {
            get { return temp; }
            set { temp = value; }
        }

		public List<String> FileIncludeList
		{
			get { return fileIncludeList; }
			set { fileIncludeList = value; }
		}
		public List<String> FileExcludeList
		{
			get { return fileExcludeList; }
			set { fileExcludeList = value; }
		}
		public List<String> SourceFolderExcludeList
		{
			get { return srcFolderExcludeList; }
			set { srcFolderExcludeList = value; }
		}
        public List<String> TargetFolderExcludeList
        {
            get { return tgtFolderExcludeList; }
            set { tgtFolderExcludeList = value; }
        }
		
		// Constructor
		public Filters()
		{
			this.fileIncludeList = new List<String>();
			this.fileExcludeList = new List<String>();
            this.srcFolderExcludeList = new List<String>();
            this.tgtFolderExcludeList = new List<String>();
            this.system = true;
            this.temp = true;
		}

        public bool isFileExcluded(FileInfo file)
        {
            if (file.Name.Equals("syncsharp.meta"))
                return true;

            bool match = isFileAttributeMatched(file);

            if (match) return true;

            match = checkFilter(file, this.fileIncludeList);
            
            if (fileIncludeList.Count > 0 && !match) return true;

            match = checkFilter(file, this.fileExcludeList);

            return match;
        }

        private bool checkFilter(FileInfo file, List<string> filterList)
        {
            foreach (string pattern in filterList)
            {
                bool isMatch = (pattern.Equals("*.*") ||
                                pattern.Equals("*") ||
                                file.Name.Equals(pattern) ||
                                ("*" + file.Extension).Equals(pattern));
                if (isMatch) return true;
            }
            return false;
        }

        private bool isFileAttributeMatched(FileInfo file)
        {
            if (hidden)
                return file.Attributes == FileAttributes.Hidden;
            if (readOnly)
                return file.Attributes == FileAttributes.ReadOnly;
            if (system)
                return file.Attributes == FileAttributes.System;
            if (temp)
                return file.Attributes == FileAttributes.Temporary;

            return false;
        }

        public bool isSourceDirExcluded(string dir)
        {
            foreach (string subdir in srcFolderExcludeList)
            {
                if (dir.StartsWith(subdir))
                    return true;
            }
            return false;
        }

        public bool isTargetDirExcluded(string dir)
        {
            foreach (string subdir in tgtFolderExcludeList)
            {
                if (dir.StartsWith(subdir))
                    return true;
            }
            return false;
        }
    }
}
