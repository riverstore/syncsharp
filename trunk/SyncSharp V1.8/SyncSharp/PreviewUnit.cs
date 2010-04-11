using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.DataModel;

namespace SyncSharp.Business
{
    public class PreviewUnit
    {
        public int intDirtyType;
        public SyncAction sAction;
        public String cleanRelativePath;
        public FileUnit cleanFileUnit;
        public String srcFlag;
        public String tgtFlag;
        public FileUnit srcFile;
        public FileUnit tgtFile;
        public String srcOldRelativePath;
        public String tgtOldRelativePath;
        public Boolean isPathDiff;
    }
}
