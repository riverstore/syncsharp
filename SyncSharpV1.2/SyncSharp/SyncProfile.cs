using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using SyncSharp.DataModel;
using System.IO;
using System.Diagnostics;

namespace SyncSharp.Storage
{
	[Serializable]
	public class SyncProfile
	{
		#region Attributes
		private String _id;
		private List<SyncTask> _taskCollection;
		private int _countDown;
		private bool _autoPlay;
		#endregion

		#region Properties
		public int CountDown
		{
			get { return _countDown; }
			set { _countDown = value; }
		}
		public bool AutoPlay
		{
			get { return _autoPlay; }
			set { _autoPlay = value; }
		}
		public String ID
		{
			get { return _id; }
		}
		public List<SyncTask> TaskCollection
		{
			get { return _taskCollection; }
		}
		#endregion

		#region Constructor
		public SyncProfile(String computerID)
		{
			Debug.Assert(computerID.Length > 0);
			this._id = computerID;
			_taskCollection = new List<SyncTask>();
			_countDown = 5;
			_autoPlay = true;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Adds a new SyncTask to the collection
		/// </summary>
		/// <param name="task"></param>
		public void AddTask(SyncTask task)
		{
			if (this.TaskExists(task.Name))
				throw new ApplicationException("Task Name already Exists in this Profile");
			_taskCollection.Add(task);
		}

		/// <summary>
		/// Removes a SyncTask from the collection
		/// </summary>
		/// <param name="task"></param>
		/// <param name="metaDataDir"></param>
		public void RemoveTask(SyncTask task, string metaDataDir)
		{
			if (task == null)
				throw new ApplicationException("Task Name does not Exist in this Profile");
			_taskCollection.Remove(task);
			if (File.Exists(metaDataDir + @"\" + task.Name + "src.meta"))
				File.Delete(metaDataDir + @"\" + task.Name + "src.meta");
			if (File.Exists(metaDataDir + @"\" + task.Name + "tgt.meta"))
				File.Delete(metaDataDir + @"\" + task.Name + "tgt.meta");
			if (File.Exists(metaDataDir + @"\" + task.Name + ".log"))
				File.Delete(metaDataDir + @"\" + task.Name + ".log");
		}

		/// <summary>
		/// Returns true if a task name currently exists in the collection
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool TaskExists(String name)
		{
			foreach (SyncTask task in _taskCollection)
			{
				if (task.Name.ToUpper().Equals(name.ToUpper().Trim()))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the specified folder pair already exists
		/// </summary>
		/// <param name="newSource"></param>
		/// <param name="newTarget"></param>
		/// <returns></returns>
		public bool IsFolderPairExists(string newSource, string newTarget)
		{
			StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
			foreach (SyncTask task in _taskCollection)
			{
				if ((task.Source.Equals(newSource, ignoreCase) &&
						task.Target.Equals(newTarget, ignoreCase)) ||
						(task.Source.Equals(newTarget, ignoreCase) &&
						task.Target.Equals(newSource, ignoreCase)))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Retrieves a SyncTask based on task name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public SyncTask GetTask(String name)
		{
			foreach (SyncTask task in _taskCollection)
			{
				if (task.Name.Equals(name.Trim()))
					return task;
			}
			return null;
		}

		/// <summary>
		/// Updates the specified SyncTask with the Last Run sync time
		/// </summary>
		/// <param name="task"></param>
		/// <param name="time"></param>
		public void UpdateSyncTaskTime(SyncTask task, string time)
		{
			task.LastRun = time;
		}

		/// <summary>
		/// Updates the specified SyncTask with the Result status
		/// </summary>
		/// <param name="task"></param>
		/// <param name="result"></param>
		public void UpdateSyncTaskResult(SyncTask task, string result)
		{
			task.Result = result;
		}

		/// <summary>
		/// Updates the root drive letter for Source/Target directories which are determined to be on a removable drive
		/// </summary>
		/// <param name="root"></param>
		public void UpdateRemovableRoot(string root)
		{
			foreach (var task in _taskCollection)
			{
				if (task.SrcOnRemovable && !task.Source.StartsWith(root))
					task.Source = root + task.Source.Substring(1);
				if (task.DestOnRemovable && !task.Target.StartsWith(root))
					task.Target = root + task.Target.Substring(1);
			}
		}

		/// <summary>
		/// Make a duplicate of the specified SyncTask
		/// </summary>
		/// <param name="name"></param>
		public void CopyTask(string name)
		{
			SyncTask newTask = new SyncTask();
			SyncTask temp = GetTask(name);
			newTask.Source = temp.Source;
			newTask.Target = temp.Target;
			newTask.Result = "";
			newTask.LastRun = "Never";
			newTask.TypeOfSync = temp.TypeOfSync;
			newTask.Settings = temp.Settings;
			newTask.Filters = temp.Filters;
			newTask.SrcOnRemovable = temp.SrcOnRemovable;
			newTask.DestOnRemovable = temp.DestOnRemovable;
			int newValue;
			if (!temp.Name.Contains("("))
			{
				newTask.Name = temp.Name + " Copy(1)";
				newValue = 0;
				while (true)
				{
					newValue++;
					newTask.Name = newTask.Name.Substring(0, newTask.Name.LastIndexOf("(") + 1) +
							newValue.ToString() + ")";
					if (!TaskExists(newTask.Name)) break;
				}
			}
			else
			{
				newValue = Int32.Parse(temp.Name.Substring(temp.Name.LastIndexOf("(") + 1,
						temp.Name.LastIndexOf(")") - temp.Name.LastIndexOf("(") - 1));
				while (true)
				{
					newValue++;
					newTask.Name = temp.Name.Substring(0, temp.Name.LastIndexOf("(") + 1) +
							newValue.ToString() + ")";
					if (!TaskExists(newTask.Name)) break;
				}
			}
			_taskCollection.Add(newTask);
		} 
		#endregion
	}
}