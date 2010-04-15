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
    /// <summary>
    /// Written by Loh Jianxiong Christoper
    /// </summary>
	[Serializable]
	public class SyncTask
	{
		#region Attributes
		private String _sourceFolder, _targetFolder, _name, _result, _lastRun;
		private TaskSettings _settings;
		private Filters _filters;
		private bool _srcOnRemovable, _destOnRemovable, _typeOfSync;
		#endregion

		#region Properties
		public String Source
		{
			get { return _sourceFolder; }
			set { _sourceFolder = value; }
		}
		public String Target
		{
			get { return _targetFolder; }
			set { _targetFolder = value; }
		}
		public String Name
		{
			get { return _name; }
			set { _name = value; }
		}
		public String Result
		{
			get { return _result; }
			set { _result = value; }
		}
		public bool TypeOfSync
		{
			get { return _typeOfSync; }
			set { _typeOfSync = value; }
		}
		public TaskSettings Settings
		{
			get { return _settings; }
			set { _settings = value; }
		}
		public Filters Filters
		{
			get { return _filters; }
			set { _filters = value; }
		}
		public String LastRun
		{
			get { return _lastRun; }
			set { _lastRun = value; }
		}
		public bool SrcOnRemovable
		{
			get { return _srcOnRemovable; }
			set { _srcOnRemovable = value; }
		}
		public bool DestOnRemovable
		{
			get { return _destOnRemovable; }
			set { _destOnRemovable = value; }
		}
		#endregion

		#region Constructor
		public SyncTask()
		{
		}

		public SyncTask(string source, string target) :
			this("", source, target, true, false, false, null, null)
		{
		}

		public SyncTask(string name, string source, string target) :
			this(name, source, target, true, false, false, null, null)
		{
		}

		public SyncTask(string name, string source,
																		string target, bool type,
																		bool srcOnRemovable, bool destOnRemovable,
																		TaskSettings settings, Filters filters)
		{
			this._name = name.Trim();
			this._sourceFolder = source.Trim();
			this._targetFolder = target.Trim();
			this._settings = settings;
			this._filters = filters;
			this._typeOfSync = type;
			this._lastRun = "Never";
			this._result = "";
			this._srcOnRemovable = srcOnRemovable;
			this._destOnRemovable = destOnRemovable;
		}
		#endregion

		#region Methods
		public override bool Equals(object obj)
		{
			return (obj is SyncTask && ((SyncTask)obj).Name.Equals(this.Name));
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}