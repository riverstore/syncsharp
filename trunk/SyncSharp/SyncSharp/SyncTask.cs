using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.DataModel;
using SyncSharp.Business;
using System.IO;
using System.Diagnostics;

namespace SyncSharp.Storage
{
	[Serializable]
	public class SyncTask
	{
		// Data Members
		private String sourceFolder, targetFolder, name, result, lastRun;
		private bool typeOfSync;
		private List<FileInfo> fiA, fiB;
		private TaskSettings settings;
		private Filter filters;

		// Properties
		internal String Source
		{
			get { return sourceFolder; }
			//set { folderA = value; }
		}
		internal String Target
		{
			get { return targetFolder; }
			//set { folderB = value; }
		}
		internal String Name
		{
			get { return name; }
			set { name = value; }
		}
		internal String Result
		{
			get { return result; }
			set { result = value; }
		}
		internal bool TypeOfSync
		{
			get { return typeOfSync; }
			set { typeOfSync = value; }
		}
		internal List<FileInfo> FileInfoA
		{
			get { return fiA; }
			set
			{
				Debug.Assert(value != null);
				fiA = value;
			}
		}
		internal List<FileInfo> FileInfoB
		{
			get { return fiB; }
			set
			{
				Debug.Assert(value != null);
				fiB = value;
			}
		}
		internal TaskSettings Settings
		{
			get { return settings; }
			set
			{
				Debug.Assert(value != null);
				settings = value;
			}
		}
		internal Filter Filters
		{
			get { return filters; }
			set
			{
				Debug.Assert(value != null);
				filters = value;
			}
		}
		internal String LastRun
		{
			get { return lastRun; }
			set { lastRun = value; }
		}

		// Constructor
		// name:  Unique name for SyncTask
		// folderA:  Path for folder A
		// folderB:  Path for folder B
		// type:  Type of synchronization
		public SyncTask()
		{
		}

		public SyncTask(string source, string target)
			: this("", source, target, true)
		{

		}

		public SyncTask(String name, String folderA, String folderB, bool type)
		{
			//if (name.Trim().Equals(""))
			//    throw new ApplicationException("SyncTask name cannot be empty");
			if (folderA.Trim().Equals(""))
				throw new ApplicationException("Folder A cannot be empty");
			if (folderB.Trim().Equals(""))
				throw new ApplicationException("Folder B cannot be empty");
			this.name = name.Trim();
			this.sourceFolder = folderA.Trim();
			this.targetFolder = folderB.Trim();
			this.fiA = new List<FileInfo>();
			this.fiB = new List<FileInfo>();
			this.settings = new TaskSettings();
			this.filters = new Filter();
			this.typeOfSync = type;
			this.lastRun = "Never";
			this.result = "";
		}
		// Methods:
	}
}
