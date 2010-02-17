using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;

namespace SyncSharp.Storage
{
    [Serializable]
    class SyncMetaData
    {
        private Dictionary<string, FileUnit> _metaData;

        public Dictionary<string, FileUnit> MetaData
        {
            set { _metaData = value; }
            get { return _metaData; }
        }

        public bool isMetaDataExists(string name)
        {
            return File.Exists(name);
        }

        public static void WriteMetaData(string path)
        {
            var stmMetaFile   = new FileStream(path +"MetaFile", FileMode.Create);
            var bfMetaFile = new BinaryFormatter();
            bfmMetaFile.Serialize(stmMetaFile, this);
        }

        public static SyncMetaData ReadMetaData(string path)
        {
            var stmMetaFile   = new FileStream(path+"MetaFile", FileMode.Open);
            var bfMetaFile = new BinaryFormatter();
            return (SyncMetaData)bfMetaFile.Deserialize(stmMetaFile);
        }
    }
}
