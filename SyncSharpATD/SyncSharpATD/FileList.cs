using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.DataModel;
using SyncSharp.Storage;

namespace SyncSharp
{
	public class FileList
	{
		#region Attributes
		private CustomDictionary<string, string, FileUnit> _cleanFiles;
		private CustomDictionary<string, string, FileUnit> _dirtyFiles;
		private CustomDictionary<string, string, FileUnit> _dirtyDirs;
		private CustomDictionary<string, string, FileUnit> _cleanDirs; 
		#endregion

		#region Properties
		public CustomDictionary<string, string, FileUnit> CleanFiles
		{
			get { return _cleanFiles; }
		}
		public CustomDictionary<string, string, FileUnit> DirtyFiles
		{
			get { return _dirtyFiles; }
		}
		public CustomDictionary<string, string, FileUnit> CleanDirs
		{
			get { return _cleanDirs; }
		}
		public CustomDictionary<string, string, FileUnit> DirtyDirs
		{
			get { return _dirtyDirs; }
		} 
		#endregion

		#region Constructor
		public FileList(CustomDictionary<string, string, FileUnit> cleanFiles,
		CustomDictionary<string, string, FileUnit> dirtyFiles,
		CustomDictionary<string, string, FileUnit> dirtyDirs,
		CustomDictionary<string, string, FileUnit> cleanDirs)
		{
			this._cleanFiles = cleanFiles;
			this._dirtyFiles = dirtyFiles;
			this._dirtyDirs = dirtyDirs;
			this._cleanDirs = cleanDirs;
		} 
		#endregion
	}
}