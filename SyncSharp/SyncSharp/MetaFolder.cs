using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.DataModel;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SyncSharp
{
    [Serializable]
    class MetaFolder
    {
        FileUnit currentFolder;
        Dictionary<string, FileUnit> localFiles;
        List<MetaFolder> localFolders;

        public MetaFolder(string path)
        {
            currentFolder = new FileUnit(path);
            localFiles = new Dictionary<string, FileUnit>();
            localFolders = new List<MetaFolder>();
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

        public MetaFolder ReadMetaData(string path)
        {
            try
            {
                FileStream fsMetaFile = null;
                fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Open);
                BinaryFormatter bfMetaFile = new BinaryFormatter();
                MetaFolder metadata = (MetaFolder)bfMetaFile.Deserialize(fsMetaFile);
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

        public void UpdateMetaData()
        {
            ConstructNode();
            foreach (MetaFolder meta in localFolders)
            {
                meta.UpdateMetaData();
            }
        }

        private void ConstructNode()
        {
            foreach (string fileName in Directory.GetFiles(currentFolder.AbsolutePath))
            {
               localFiles.Add(fileName, new FileUnit(fileName));
            }

            foreach (string folderName in Directory.GetDirectories(currentFolder.AbsolutePath))
            {
                localFolders.Add(new MetaFolder(folderName));
            }
        }
    }
}
