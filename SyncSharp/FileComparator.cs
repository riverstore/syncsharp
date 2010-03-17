using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.DataModel;

namespace SyncSharp
{
	class FileComparator : IComparer<FileUnit>
	{
		readonly bool _name;
		readonly bool _size;
		readonly bool _time;
		readonly bool _hash;
		int _timeTolerance;

		public FileComparator(int timeTolerance) :
			this(true, true, true, false, timeTolerance)
		{
		}

		public FileComparator(bool name, bool size, bool time, bool hash, int timeTolerance)
		{
			_name = name;
			_size = size;
			_time = time;
			_hash = hash;
			_timeTolerance = timeTolerance;
		}

		public int Compare(FileUnit x, FileUnit y)
		{
			if (_name)
			{
				int i = x.Name.CompareTo(y.Name);
				if (i != 0) return i;
			}
			if (_size)
			{
				if (x.Size < y.Size) return -1;
				if (x.Size > y.Size) return 1;
			}
			if (_time)
			{
				int interval = (x.LastWriteTime - y.LastWriteTime).Seconds;
				//if (x.LastWriteTime < y.LastWriteTime) return -1;
				//if (x.LastWriteTime > y.LastWriteTime) return 1;
				if (interval < 0 && Math.Abs(interval) > _timeTolerance) return -1;
				if (interval > 0 && interval > _timeTolerance) return 1;
			}
			if (_hash)
			{
				if (x.Hash.Equals(y.Hash)) return 1;
				else return -1;
			}
			return 0;
		}
	}
}