using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SyncSharp.Storage
{
    [Serializable]
    class TaskSettings
    {
        // Contains information on settings Related to a particular SyncTask
        // 1) AutoSync, RecycleBin toggle
        // 2) Conflict settings for synching

        // Data Members
        private bool plugSync, useRecycleBin;
        // 0:  Default actions for conflicts as specified by the reconciler
        // 1:  FolderA always have the priority in conflict cases
        // -1:  FolderB always have the priority in conflict cases
        private int update, updateDelete, cCreatePDelete, folderNameConflict,
            cCFileNameConflict, cRFileNameConflict;

        // Properties
        public bool PlugSync
        {
            get { return plugSync; }
            set { plugSync = value; }
        }
        public bool UseRecycleBin
        {
            get { return useRecycleBin; }
            set { useRecycleBin = value; }
        }
        public int Update
        {
            get { return update; }
            set
            {
                Debug.Assert(value >= -1 && value <= 1);
                update = value;
            }
        }
        public int UpdateDelete
        {
            get { return updateDelete; }
            set
            {
                Debug.Assert(value >= -1 && value <= 1);
                updateDelete = value;
            }
        }
        public int CCreatePDelete
        {
            get { return cCreatePDelete; }
            set
            {
                Debug.Assert(value >= -1 && value <= 1);
                cCreatePDelete = value;
            }
        }
        public int FolderNameConflict
        {
            get { return folderNameConflict; }
            set
            {
                Debug.Assert(value >= -1 && value <= 1);
                folderNameConflict = value;
            }
        }
        public int CCFileNameConflict
        {
            get { return cCFileNameConflict; }
            set
            {
                Debug.Assert(value >= -1 && value <= 1);
                cCFileNameConflict = value;
            }
        }
        public int CRFileNameConflict
        {
            get { return cRFileNameConflict; }
            set
            {
                Debug.Assert(value >= -1 && value <= 1);
                cRFileNameConflict = value;
            }
        }

        // Constructor
        public TaskSettings()
        {
            this.plugSync = false;
            this.useRecycleBin = true;
            this.update = 0;
            this.updateDelete = 0;
            this.cCreatePDelete = 0;
            this.folderNameConflict = 0;
            this.cCFileNameConflict = 0;
            this.cRFileNameConflict = 0;
        }
    }
}
