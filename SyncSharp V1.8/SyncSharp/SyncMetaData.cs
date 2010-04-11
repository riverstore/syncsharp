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
		#region Methods
		public static bool IsMetaDataExists(string name)
		{
			return File.Exists(name);
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
		#endregion
	}
}