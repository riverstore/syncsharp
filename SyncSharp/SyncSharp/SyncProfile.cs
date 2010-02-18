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
		// Data Members
		private String id;
		private List<SyncTask> taskCollection;

		// Properties
		internal String ID
		{
			get { return id; }
			//set { id = value; }
		}
		internal List<SyncTask> TaskCollection
		{
			get { return taskCollection; }
			//set { tasks = value; }
		}

		// Constructor
		// computerID:  ID to identify unique computer
		internal SyncProfile(String computerID)
		{
			Debug.Assert(computerID.Length > 0);
			this.id = computerID;
			taskCollection = new List<SyncTask>();
		}

		// Methods:

		// Adds a new SyncTask to this profile
		// task:  SyncTask to be added to this profile
		// Exceptions:  If non-unique task name used
		internal void addTask(SyncTask task)
		{
			if (this.taskExists(task.Name))
				throw new ApplicationException("Task Name already Exists in this Profile");
			taskCollection.Add(task);
		}

		// Removes a SyncTask from this profile
		// task:  SyncTask to be removed from this profile
		// Exceptions:  if null parameter used
		internal void removeTask(SyncTask task)
		{
			if (task == null)
				throw new ApplicationException("Task Name does not Exist in this Profile");
			taskCollection.Remove(task);
		}

		// Checks if a task exists in this profile
		// name:  Name of SyncTask to check if exists
		// returns:  true if exists, else false
		internal bool taskExists(String name)
		{
			foreach (SyncTask task in taskCollection)
			{
				if (task.Name.Equals(name.Trim()))
					return true;
			}
			return false;
		}

		// Retrieves a SyncTask
		// name:  Name of SyncTask to retrieve
		// returns:  SyncTask object if exists, else null
		internal SyncTask getTask(String name)
		{
			foreach (SyncTask task in taskCollection)
			{
				if (task.Name.Equals(name.Trim()))
					return task;
			}
			return null;
		}

		internal void updateSyncTaskTime(string name, string time)
		{
			getTask(name).LastRun = time;
		}

		internal void updateSyncTaskResult(string name, string result)
		{
			getTask(name).Result = result;
		}
	}
}
