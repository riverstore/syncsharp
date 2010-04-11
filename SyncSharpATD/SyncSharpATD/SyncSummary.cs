using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Business
{
    public class SyncSummary
    {
        public int iSrcFileCopy;
        public int iSrcFileDelete;
        public int iSrcFileRename;
        public int iSrcFileOverwrite;
        public int iSrcFolderCreate;
        public int iSrcFolderDelete;

        public int iTgtFileCopy;
        public int iTgtFileDelete;
        public int iTgtFileRename;
        public int iTgtFileOverwrite;
        public int iTgtFolderCreate;
        public int iTgtFolderDelete;

        public DateTime startTime;
        public DateTime endTime;
        public String logFile;

        public SyncSummary()
        {
            iSrcFileCopy = iSrcFileDelete = iSrcFileRename = iSrcFileOverwrite = 0;
            iSrcFolderCreate = iSrcFolderDelete = 0;
            iTgtFileCopy = iTgtFileDelete = iTgtFileRename = iTgtFileOverwrite = 0;
            iTgtFolderCreate = iTgtFolderDelete = 0;
        }
    }
}
