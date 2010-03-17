using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SyncSharp.DataModel;
using SyncSharp.Storage;

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
				CustomDictionary<string, string, FileUnit> metadata = GetMetaContents(path);

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

		public static void WriteMetaData(string path, CustomDictionary<string, string, FileUnit> metadata)
		{
			try
			{
				if (File.Exists(path))
					File.SetAttributes(path, FileAttributes.Normal);
				FileStream fsMetaFile = null;
				fsMetaFile = new FileStream(path, FileMode.Create);
				var bfMetaFile = new BinaryFormatter();
				bfMetaFile.Serialize(fsMetaFile, metadata);
				fsMetaFile.Close();
			}
			catch
			{
			}
		}

		public static CustomDictionary<string, string, FileUnit> ReadMetaData(string path)
		{
			try
			{
				FileStream fsMetaFile = null;
				fsMetaFile = new FileStream(path, FileMode.Open);
				BinaryFormatter bfMetaFile = new BinaryFormatter();
				CustomDictionary<string, string, FileUnit> metadata =
								(CustomDictionary<string, string, FileUnit>)bfMetaFile.Deserialize(fsMetaFile);
				fsMetaFile.Close();
				return metadata;
			}
			catch
			{
				return null;
			}
		}

		private static CustomDictionary<string, string, FileUnit> GetMetaContents(string path)
		{
			CustomDictionary<string, string, FileUnit> metadata = new CustomDictionary<string, string, FileUnit>();
			Stack<string> stack = new Stack<string>();
			stack.Push(path);
			while (stack.Count > 0)
			{
				string folder = stack.Pop();
				foreach (string fileName in Directory.GetFiles(folder))
				{
					if (!String.Equals(fileName, "syncsharp.meta"))
						metadata.add(fileName, new FileUnit(fileName));
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