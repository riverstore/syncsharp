using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        public void WriteMetaData(string path)
        {
            try
            {
                var fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Create);
                var bfMetaFile = new BinaryFormatter();
                bfMetaFile.Serialize(fsMetaFile, this);
            }
            catch
            {
            }
        }

        public SyncMetaData ReadMetaData(string path)
        {
            try
            {
                var fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Open);
                var bfMetaFile = new BinaryFormatter();
                return (SyncMetaData)bfMetaFile.Deserialize(fsMetaFile);
            }
            catch
            {
                return null;
            }
        }

        public void getContent(string fullPath)
        {
            if (!(Directory.Exists(fullPath)))
            {
            }

            else
            {
                foreach (string fileName in Directory.GetFiles(fullPath))
                {
                    _metaData.Add(fileName, new FileUnit(fileName));
                }
            }

            this.WriteMetaData(this);
        }
    }
}
