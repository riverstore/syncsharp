using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Main
{
	class SyncTask
	{
		// Data Members
		private String folderA, folderB, name;
		private List<FileInfo> fiA, fiB;
		private TaskSettings settings;
		private Filters filters;

		// Properties
		public String FolderA
		{
			get { return folderA; }
			//set { folderA = value; }
		}
		public String FolderB
		{
			get { return folderB; }
			//set { folderB = value; }
		}
		public String Name
		{
			get { return name; }
			//set { name = value; }
		}
		public List<FileInfo> FileInfoA
		{
			get { return fiA; }
			set 
			{ 
				Debug.Assert(value != null); 
				fiA = value; 
			}
		}
		public List<FileInfo> FileInfoB
		{
			get { return fiB; }
			set 
			{ 
				Debug.Assert(value != null); 
				fiB = value; 
			}
		}
		public TaskSettings Settings
		{
			get { return settings; }
			set 
			{ 
				Debug.Assert(value != null); 
				settings = value; 
			}
		}
		public Filters Filters
		{
			get { return filters; }
			set 
			{ 
				Debug.Assert(value != null); 
				filters = value; 
			}
		}

		// Constructor
		// name:  Unique name for SyncTask
		// folderA:  Path for folder A
		// folderB:  Path for folder B
		public SyncTask()
		{
		}
		public SyncTask(String name, String folderA, String folderB)
		{
			if (name.Trim().Equals(""))
				throw new ApplicationException("SyncTask name cannot be empty");
			if (folderA.Trim().Equals(""))
				throw new ApplicationException("Folder A cannot be empty");
			if (folderB.Trim().Equals(""))
				throw new ApplicationException("Folder B cannot be empty");
			this.name = name.Trim();
			this.folderA = folderA.Trim();
			this.folderB = folderB.Trim();
			this.fiA = new List<FileInfo>();
			this.fiB = new List<FileInfo>();
			this.settings = new TaskSettings();
			this.filters = new Filters();
		}
	
		// Methods:


	}
}