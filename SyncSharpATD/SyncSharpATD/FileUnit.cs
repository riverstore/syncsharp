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

		private readonly string _name;
		private readonly string _absolutePath;
		private readonly long _size;
		private readonly DateTime _lastWriteTime;
		private string _hashValue;
		private readonly string _extension;
		private string _matchingPath;

		private readonly bool _isDirectory;

		private FileUnit _match = null;

		#endregion

		#region constructors

		public FileUnit(string path)
		{
			_isDirectory = Directory.Exists(path);
			_name = Path.GetFileName(path);
			_absolutePath = path;
			_matchingPath = "";
			_hashValue = "";

			if (_isDirectory)
			{
				_size = 0L;
				_lastWriteTime = DateTime.MinValue;
				_extension = "dir";
				if (!_absolutePath.EndsWith(@"\"))
					_absolutePath += @"\";
			}
			else
			{
				FileInfo file = new FileInfo(path);
				_size = file.Length;
				_lastWriteTime = File.GetLastWriteTime(path);
				_extension = file.Extension.ToLower();
			}
		}

		#endregion

		#region properties

		public string MatchingPath
		{
			set { _matchingPath = value; }
			get { return _matchingPath; }
		}
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
		public string Hash
		{
			get { return _hashValue; }
			set { _hashValue = value; }
		}
		public string Extension
		{
			get { return _extension; }
		}
		public DateTime LastWriteTime
		{
			get { return _lastWriteTime; }
		}
		public bool IsDirectory
		{
			get { return _isDirectory; }
		}

		#endregion
	}
}