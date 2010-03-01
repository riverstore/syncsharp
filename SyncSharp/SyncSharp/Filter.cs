using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Business
{
	[Serializable]
    public class Filter
    {
        // Data Members
		private List<String> fileNameInclude, fileNameExclude, subDirectoryExclude, fileAttributeExclude;

		// Properties
		public List<String> FileNameInclude
		{
			get { return fileNameInclude; }
			set { fileNameInclude = value; }
		}
		public List<String> FileNameExclude
		{
			get { return fileNameExclude; }
			set { fileNameExclude = value; }
		}
		public List<String> SubDirectoryExclude
		{
			get { return subDirectoryExclude; }
			set { subDirectoryExclude = value; }
		}
		public List<String> FileAttributeExclude
		{
			get { return fileAttributeExclude; }
			set { fileAttributeExclude = value; }
		}

		// Constructor
		public Filter()
		{
			this.fileNameInclude = new List<String>();
			this.fileNameExclude = new List<String>();
			this.subDirectoryExclude = new List<String>();
			this.fileAttributeExclude = new List<String>();
		}
    }
}
