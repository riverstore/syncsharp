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
    public class SyncMetaData
    {
        public static bool isMetaDataExists(string name)
        {
            return File.Exists(name);
        }

        public static void WriteMetaData(string path)
        {
            try
            {
                Dictionary<string, FileUnit> metadata = GetMetaContents(path);

                if (File.Exists(path + "\\syncsharp.meta"))
                    File.SetAttributes(path + "\\syncsharp.meta", FileAttributes.Normal);

                FileStream fsMetaFile = null;
                fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Create);
                var bfMetaFile = new BinaryFormatter();
                bfMetaFile.Serialize(fsMetaFile, metadata);
                fsMetaFile.Close();
                File.SetAttributes(path + "\\syncsharp.meta", FileAttributes.Hidden);
            }
            catch
            {
            }
        }

        public static void WriteMetaData(string path, Dictionary<string, FileUnit> metadata)
        {
            try
            {
                if (File.Exists(path + "\\syncsharp.meta"))
                    File.SetAttributes(path + "\\syncsharp.meta", FileAttributes.Normal);

                FileStream fsMetaFile = null;
                fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Create);
                var bfMetaFile = new BinaryFormatter();
                bfMetaFile.Serialize(fsMetaFile, metadata);
                fsMetaFile.Close();
                File.SetAttributes(path + "\\syncsharp.meta", FileAttributes.Hidden);
            }
            catch
            {
            }
        }

        public static Dictionary<string, FileUnit> ReadMetaData(string path)
        {
            try
            {
                FileStream fsMetaFile = null;
                fsMetaFile = new FileStream(path + "\\syncsharp.meta", FileMode.Open);
                BinaryFormatter bfMetaFile = new BinaryFormatter();
                Dictionary<string, FileUnit> metadata = 
                        (Dictionary<string, FileUnit>)bfMetaFile.Deserialize(fsMetaFile);
                fsMetaFile.Close();
                return metadata;
            }
            catch
            {
                return null;
            }
        }

        private static Dictionary<string, FileUnit> GetMetaContents(string path)
        {
            Dictionary<string, FileUnit> metadata = new Dictionary<string,FileUnit>();

            Stack<string> stack = new Stack<string>();
            stack.Push(path);

            while (stack.Count > 0)
            {
                string folder = stack.Pop();

                foreach (string fileName in Directory.GetFiles(folder))
                {
                    if (!String.Equals(fileName, "syncsharp.meta"))
                        metadata.Add(fileName, new FileUnit(fileName));
                }

                foreach (string folderName in Directory.GetDirectories(folder))
                {
                    stack.Push(folderName);
                }
            }

            return metadata;
        }
    }
}