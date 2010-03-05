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

        public FileComparator()
            : this(true, true, true, false)
        {
        }

        public FileComparator(bool name, bool size, bool time, bool hash)
        {
            this._name = name;
            this._size = size;
            this._time = time;
            this._hash = hash;
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
                if (x.LastWriteTime < y.LastWriteTime) return -1;
                if (x.LastWriteTime > y.LastWriteTime) return 1;
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
