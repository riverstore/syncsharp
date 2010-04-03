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

		#region Attributes
		private List<String> _fileIncludeList, _fileExcludeList, _srcFolderExcludeList, _tgtFolderExcludeList;
		private bool _hidden, _readOnly, _system, _temp;
		#endregion

		#region Properties
		public bool Hidden
		{
			get { return _hidden; }
			set { _hidden = value; }
		}
		public bool ReadOnly
		{
			get { return _readOnly; }
			set { _readOnly = value; }
		}
		public bool System
		{
			get { return _system; }
			set { _system = value; }
		}
		public bool Temp
		{
			get { return _temp; }
			set { _temp = value; }
		}
		public List<String> FileIncludeList
		{
			get { return _fileIncludeList; }
			set { _fileIncludeList = value; }
		}
		public List<String> FileExcludeList
		{
			get { return _fileExcludeList; }
			set { _fileExcludeList = value; }
		}
		public List<String> SourceFolderExcludeList
		{
			get { return _srcFolderExcludeList; }
			set { _srcFolderExcludeList = value; }
		}
		public List<String> TargetFolderExcludeList
		{
			get { return _tgtFolderExcludeList; }
			set { _tgtFolderExcludeList = value; }
		}
		#endregion

		#region Constructor
		public Filters()
		{
			_fileIncludeList = new List<String>();
			_fileExcludeList = new List<String>();
			_srcFolderExcludeList = new List<String>();
			_tgtFolderExcludeList = new List<String>();
			_system = true;
			_temp = true;
			_hidden = true;
			_readOnly = true;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns true if file is to be excluded from the synchronization
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public bool IsFileExcluded(FileInfo file)
		{
			bool match = IsFileAttributeMatched(file);
			if (match) return true;
			match = CheckFilter(file, this._fileIncludeList);
			if (_fileIncludeList.Count > 0 && !match) return true;
			match = CheckFilter(file, this._fileExcludeList);
			return match;
		}

		/// <summary>
		/// Returns true if file is contained in the specified filter
		/// </summary>
		/// <param name="file"></param>
		/// <param name="filterList"></param>
		/// <returns></returns>
		private bool CheckFilter(FileInfo file, List<string> filterList)
		{
			StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
			foreach (string pattern in filterList)
			{
				bool isMatch = (pattern.Equals("*.*", ignoreCase) ||
								pattern.Equals("*", ignoreCase) ||
								file.Name.Equals(pattern, ignoreCase) ||
								("*" + file.Extension).Equals(pattern, ignoreCase));
				if (isMatch) return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the file contains a matching attribute filter
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private bool IsFileAttributeMatched(FileInfo file)
		{
			if (_hidden)
				return (File.GetAttributes(file.FullName) & FileAttributes.Hidden) == FileAttributes.Hidden;
			if (_readOnly)
				return (File.GetAttributes(file.FullName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
			if (_system)
				return (File.GetAttributes(file.FullName) & FileAttributes.System) == FileAttributes.System;
			if (_temp)
				return (File.GetAttributes(file.FullName) & FileAttributes.Temporary) == FileAttributes.Temporary;
			return false;
		}

		/// <summary>
		/// Returns true if the source directory is excluded
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public bool IsSourceDirExcluded(string dir)
		{
			foreach (string subdir in _srcFolderExcludeList)
			{
				if (dir.StartsWith(subdir, true, null))
					return true;
			}
			return false;
		}

		/// <summary>
		/// returns true if the target directory is excluded
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public bool IsTargetDirExcluded(string dir)
		{
			foreach (string subdir in _tgtFolderExcludeList)
			{
				if (dir.StartsWith(subdir, true, null))
					return true;
			}
			return false;
		} 
		#endregion
	}
}