using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using SyncSharp.DataModel;

namespace SyncSharp.Storage
{
	static class Utility
	{
		public static String ComputeMyHash(FileUnit myFile)
		{
			FileStream myFileStream = new FileStream(myFile.AbsolutePath, FileMode.Open, FileAccess.Read);
			String strHash = "";
			if (myFile.Size < 30000)
			{
				byte[] tmpHash = new SHA1Managed().ComputeHash(myFileStream);
				strHash = BitConverter.ToString(tmpHash, 0).Replace("-", "");
			}
			else
			{
				byte[] fileHeader = new byte[30000];
				myFileStream.Read(fileHeader, 0, 30000);
				byte[] tmpHash = new SHA1Managed().ComputeHash(fileHeader);
				strHash = BitConverter.ToString(tmpHash, 0).Replace("-", "");
			}
			myFileStream.Dispose();
			return strHash;
		}

        public static String FormatSize(long size)
        {
            string sizeString = "";
            if (size < 1024)
                sizeString = String.Format("{0:0.00 B}", size);
            else if (size < 1048576)
                sizeString = String.Format("{0:0.00 KB}", size / 1024.0);
            else if (size < 1073741824)
                sizeString = String.Format("{0:0.00 MB}", size / 1048576.0);
            else
                sizeString = String.Format("{0:0.00 GB}", size / 1073741824.0);
            return sizeString;
        }

        public static void RenameMetaFiles(string oldName, string newName, string metaDataDir)
        {
            try
            {
                if (File.Exists(metaDataDir + @"\" + oldName + ".meta"))
                    File.Move(metaDataDir + @"\" + oldName + ".meta",
                            metaDataDir + @"\" + newName + ".meta");
                if (File.Exists(metaDataDir + @"\" + oldName + ".log"))
                    File.Move(metaDataDir + @"\" + oldName + ".log",
                            metaDataDir + @"\" + newName + ".log");
                if (File.Exists(metaDataDir + @"\" + oldName + ".bkp"))
                    File.Move(metaDataDir + @"\" + oldName + ".bkp",
                            metaDataDir + @"\" + newName + ".bkp");
            }
            catch
            {
            }
        }
	}
}