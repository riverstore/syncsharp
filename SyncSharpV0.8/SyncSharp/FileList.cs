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
		private CustomDictionary<string, string, FileUnit> cleanFiles;
		private CustomDictionary<string, string, FileUnit> dirtyFiles;
		private CustomDictionary<string, string, FileUnit> dirtyDirs;
		private CustomDictionary<string, string, FileUnit> cleanDirs;

		public CustomDictionary<string, string, FileUnit> CleanFiles
		{
			get { return cleanFiles; }
		}
		public CustomDictionary<string, string, FileUnit> DirtyFiles
		{
			get { return dirtyFiles; }
		}
		public CustomDictionary<string, string, FileUnit> CleanDirs
		{
			get { return cleanDirs; }
		}
		public CustomDictionary<string, string, FileUnit> DirtyDirs
		{
			get { return dirtyDirs; }
		}

		public FileList(CustomDictionary<string, string, FileUnit> cleanFiles,
			CustomDictionary<string, string, FileUnit> dirtyFiles,
			CustomDictionary<string, string, FileUnit> dirtyDirs,
			CustomDictionary<string, string, FileUnit> cleanDirs)
		{
			this.cleanFiles = cleanFiles;
			this.dirtyFiles = dirtyFiles;
			this.dirtyDirs = dirtyDirs;
			this.cleanDirs = cleanDirs;
		}
	}
}
