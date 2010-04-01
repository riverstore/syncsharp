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
		private String id;
		private List<SyncTask> taskCollection;
		private int countDown;
		private bool autoPlay;

		public int CountDown
		{
			get { return countDown; }
			set { countDown = value; }
		}
		public bool AutoPlay
		{
			get { return autoPlay; }
			set { autoPlay = value; }
		}
		public String ID
		{
			get { return id; }
			//set { id = value; }
		}
		public List<SyncTask> TaskCollection
		{
			get { return taskCollection; }
			//set { tasks = value; }
		}

		public SyncProfile(String computerID)
		{
			Debug.Assert(computerID.Length > 0);
			this.id = computerID;
			taskCollection = new List<SyncTask>();
			countDown = 5;
			autoPlay = true;
		}

		public void addTask(SyncTask task)
		{
			if (this.taskExists(task.Name))
				throw new ApplicationException("Task Name already Exists in this Profile");
			taskCollection.Add(task);
		}

		public void removeTask(SyncTask task, string metaDataDir)
		{
            if (task == null)
                throw new ApplicationException("Task Name does not Exist in this Profile");
            taskCollection.Remove(task);
            if (File.Exists(metaDataDir + @"\" + task.Name + "src.meta"))
                File.Delete(metaDataDir + @"\" + task.Name + "src.meta");
            if (File.Exists(metaDataDir + @"\" + task.Name + "tgt.meta"))
                File.Delete(metaDataDir + @"\" + task.Name + "tgt.meta");
            if (File.Exists(metaDataDir + @"\" + task.Name + ".log"))
                File.Delete(metaDataDir + @"\" + task.Name + ".log");
		}

		public bool taskExists(String name)
		{
			foreach (SyncTask task in taskCollection)
			{
				if (task.Name.ToUpper().Equals(name.ToUpper().Trim()))
					return true;
			}
			return false;
		}

        public bool isFolderPairExists(string newSource, string newTarget)
        {
            StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
            foreach (SyncTask task in taskCollection)
            {
                if ((task.Source.Equals(newSource, ignoreCase) && 
                    task.Target.Equals(newTarget, ignoreCase)) ||
                    (task.Source.Equals(newTarget, ignoreCase) && 
                    task.Target.Equals(newSource, ignoreCase))) 
                    return true;
            }
            return false;
        }

		public SyncTask getTask(String name)
		{
			foreach (SyncTask task in taskCollection)
			{
				if (task.Name.Equals(name.Trim()))
					return task;
			}
			return null;
		}

		public void updateSyncTaskTime(SyncTask task, string time)
		{
			task.LastRun = time;
		}

		public void updateSyncTaskResult(SyncTask task, string result)
		{
			task.Result = result;
		}

		public void updateRemovableRoot(string root)
		{
			foreach (var task in taskCollection)
			{
				if (task.SrcOnRemovable && !task.Source.StartsWith(root))
					task.Source = root + task.Source.Substring(1);
				if (task.DestOnRemovable && !task.Target.StartsWith(root))
					task.Target = root + task.Target.Substring(1);
			}
		}

		public void copyTask(string name)
		{
			SyncTask newTask = new SyncTask();
			SyncTask temp = getTask(name);
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
					if (!taskExists(newTask.Name)) break;
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
					if (!taskExists(newTask.Name)) break;
				}
			}
			taskCollection.Add(newTask);
		}
	}
}