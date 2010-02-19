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

        public SyncMetaData()
        {
            _metaData = new Dictionary<string, FileUnit>();
        }

        public bool isMetaDataExists(string name)
        {
            return File.Exists(name);
        }

        public void WriteMetaData(string path)
        {
            try
            {
                FileStream fsMetaFile = null;
                fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Create);
                var bfMetaFile = new BinaryFormatter();
                bfMetaFile.Serialize(fsMetaFile, this);
                fsMetaFile.Close();
            }
            catch
            {
            }
            finally
            {
                //fsMetaFile.Close();
            }

        }

        public SyncMetaData ReadMetaData(string path)
        {
            try
            {
                FileStream fsMetaFile = null;
                fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Open);
                BinaryFormatter bfMetaFile = new BinaryFormatter();
                SyncMetaData metadata = (SyncMetaData)bfMetaFile.Deserialize(fsMetaFile);
                fsMetaFile.Close();
                return metadata;
            }
            catch
            {
                return null;
            }
            finally
            {
                //fsMetaFile.Close();
            }
        }

        public void UpdateMetaData(string fullPath)
        {
            _metaData.Clear();

            Stack<string> stack = new Stack<string>();
            stack.Push(fullPath);

            while (stack.Count > 0)
            {
                string folder = stack.Pop();

                foreach (string fileName in Directory.GetFiles(folder))
                {
                    if (!String.Equals(fileName, "syncsharp.meta"))
                        _metaData.Add(fileName, new FileUnit(fileName));
                }

                foreach (string folderName in Directory.GetDirectories(folder))
                {
                    stack.Push(folderName);
                }
            }

            this.WriteMetaData(fullPath);
        }
    }
}