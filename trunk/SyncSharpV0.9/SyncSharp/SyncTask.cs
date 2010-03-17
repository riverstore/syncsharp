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
		private String sourceFolder, targetFolder, name, result, lastRun;
		private TaskSettings settings;
		private Filters filters;
		private bool srcOnRemovable, destOnRemovable, typeOfSync;

		internal String Source
		{
			get { return sourceFolder; }
			set { sourceFolder = value; }
		}
		internal String Target
		{
			get { return targetFolder; }
			set { targetFolder = value; }
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
    internal TaskSettings Settings
		{
			get { return settings; }
			set { settings = value; }
		}
		internal Filters Filters
		{
			get { return filters; }
			set { filters = value; }
		}
    internal String LastRun
		{
			get { return lastRun; }
			set { lastRun = value; }
		}
    internal bool SrcOnRemovable
		{
			get { return srcOnRemovable; }
			set { srcOnRemovable = value; }
		}
    internal bool DestOnRemovable
		{
			get { return destOnRemovable; }
			set { destOnRemovable = value; }
		}

		public SyncTask()
		{
		}

		public SyncTask(string source, string target) :
			this("", source, target, true,
														 false, false, null, null)
		{
		}

		public SyncTask(string name, string source, string target) :
			this(name, source, target, true,
												 false, false, null, null)
		{
		}

		public SyncTask(string name, string source,
										string target, bool type,
										bool srcOnRemovable, bool destOnRemovable,
										TaskSettings settings, Filters filters)
		{
			this.name = name.Trim();
			this.sourceFolder = source.Trim();
			this.targetFolder = target.Trim();
			this.settings = settings;
			this.filters = filters;
			this.typeOfSync = type;
			this.lastRun = "Never";
			this.result = "";
			this.srcOnRemovable = srcOnRemovable;
			this.destOnRemovable = destOnRemovable;
		}

		public override bool Equals(object obj)
		{
			return (obj is SyncTask && ((SyncTask)obj).Name.Equals(this.Name));
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}