using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SyncSharp.Storage
{
	public static class Logger
	{
		#region attributes

		public enum LogType { CopySRC, CopyTGT, DeleteSRC, DeleteTGT, RenameSRC, RenameTGT, CreateSRC, CreateTGT };

		private static readonly string CopyStatusSRC = "[COPY OK] from SOURCE";
		private static readonly string DeleteStatusSRC = "[DELETE OK] from SOURCE";
		private static readonly string RenameStatusSRC = "[RENAME OK] from SOURCE";
		private static readonly string CreateStatusSRC = "[CREATE FOLDER OK] from SOURCE";

		private static readonly string CopyStatusTGT = "[COPY OK] from TARGET";
		private static readonly string DeleteStatusTGT = "[DELETE OK] from TARGET";
		private static readonly string RenameStatusTGT = "[RENAME OK] from TARGET";
		private static readonly string CreateStatusTGT = "[CREATE FOLDER OK] from TARGET";

		/*        internal static readonly ulong MaxUlong = 18446744073709551615;
						internal static readonly uint MaxUint = 4294967295;*/

		private static FileStream _currFile;
		private static StreamWriter _sw;

		private static string _logFileName;
		private static string _logFileLocation;

		private static int _filesRenamedCntSRC;
		private static int _filesCopiedCntSRC;
		private static int _filesDeletedCntSRC;

		private static long _filesRenamedSizeSRC;
		private static long _filesCopiedSizeSRC;
		private static long _filesDeletedSizeSRC;

		private static int _foldersCreatedCntSRC;

		private static int _filesRenamedCntTGT;
		private static int _filesCopiedCntTGT;
		private static int _filesDeletedCntTGT;

		private static long _filesRenamedSizeTGT;
		private static long _filesCopiedSizeTGT;
		private static long _filesDeletedSizeTGT;

		private static int _foldersCreatedCntTGT;

		#endregion

		#region properties

		#endregion

		#region methods

		public static bool SyncPlanWriteLog(string metaDataDir, string syncTaskName, int srcCopyTotal, long srcCopySize, int srcDeleteTotal, long srcDeleteSize, int srcRenameTotal, long srcRenameSize, int tgtCopyTotal, long tgtCopySize, int tgtDeleteTotal, long tgtDeleteSize, int tgtRenameTotal, long tgtRenameSize)
		{
			if (metaDataDir == null) throw new ArgumentNullException("metaDataDir");
			if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
			if (metaDataDir.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "metaDataDir");
			if (syncTaskName.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "syncTaskName");

			/*      if (srcCopySize < 0 || srcCopySize > MaxUlong) throw new ArgumentOutOfRangeException("srcCopySize");
						if (srcDeleteSize < 0 || srcCopySize > MaxUlong) throw new ArgumentOutOfRangeException("srcDeleteSize");
						if (srcRenameSize < 0 || srcCopySize > MaxUlong) throw new ArgumentOutOfRangeException("srcRenameSize");
						if (tgtCopySize < 0 || srcCopySize > MaxUlong) throw new ArgumentOutOfRangeException("tgtCopySize");
						if (tgtDeleteSize < 0 || srcCopySize > MaxUlong) throw new ArgumentOutOfRangeException("tgtDeleteSize");
						if (tgtRenameSize < 0 || srcCopySize > MaxUlong) throw new ArgumentOutOfRangeException("tgtRenameSize");

						if (srcCopyTotal < 0 || srcCopyTotal > MaxUint) throw new ArgumentOutOfRangeException("srcCopyTotal");
						if (srcDeleteTotal < 0 || srcCopyTotal > MaxUint) throw new ArgumentOutOfRangeException("srcDeleteTotal");
						if (srcRenameTotal < 0 || srcCopyTotal > MaxUint) throw new ArgumentOutOfRangeException("srcRenameTotal");
						if (tgtCopyTotal < 0 || srcCopyTotal > MaxUint) throw new ArgumentOutOfRangeException("tgtCopyTotal");
						if (tgtDeleteTotal < 0 || srcCopyTotal > MaxUint) throw new ArgumentOutOfRangeException("tgtDeleteTotal");
						if (tgtRenameTotal < 0 || srcCopyTotal > MaxUint) throw new ArgumentOutOfRangeException("tgtRenameTotal");*/

			try
			{
				// *** Write to file ***

				// Specify file, instructions, and privilegdes
				DirectoryInfo di = new DirectoryInfo(_logFileLocation);
				// Try to create the directory.
				di.Create();
				_currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

				// Create a new stream to write to the file
				_sw = new StreamWriter(_currFile);

				// Write to the file

				_sw.WriteLine("*** Sync Plan ***");
				_sw.WriteLine("ACTION          \tMACHINE\t\tUSB");

				_sw.WriteLine("Files to COPY  :\t{0}\t{1}\t{2}\t{3}\t", srcCopyTotal, srcCopySize, tgtCopyTotal, tgtCopySize);
				_sw.WriteLine("Files to DELETE:\t{0}\t{1}\t{2}\t{3}\t", srcDeleteTotal, srcDeleteSize, tgtDeleteTotal, tgtDeleteSize);
				_sw.WriteLine("Files to RENAME:\t{0}\t{1}\t{2}\t{3}\t", srcRenameTotal, srcRenameSize, tgtRenameTotal, tgtRenameSize);
				_sw.WriteLine("*****************");
				_sw.WriteLine("#Sync executing#");
				_sw.WriteLine("#Format:|date|time|status|machine source|size|machine dest|size|usb source|size|usb dest|size|error msg|");
				_sw.WriteLine("#");
			}
			catch (IOException e)
			{
				WriteErrorLog(e.Message);
				return false;

				//throw;
			}
			finally
			{
				// Close StreamWriter
				_sw.Close();

				// Close file
				_currFile.Close();
			}
			return true;
		}

		public static bool SyncSetWriteLog(string metaDataDir, string syncTaskName, bool start)
		{
			if (metaDataDir == null) throw new ArgumentNullException("metaDataDir");
			if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
			if (metaDataDir.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "metaDataDir");
			if (syncTaskName.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "syncTaskName");

			_logFileName = metaDataDir + @"\" + syncTaskName + ".log";
			_logFileLocation = metaDataDir + @"\";

			try
			{
				// *** Write to file ***

				// Specify file, instructions, and privilegdes
				DirectoryInfo di = new DirectoryInfo(_logFileLocation);
				// Try to create the directory.
				di.Create();

				_currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

				// Create a new stream to write to the file
				_sw = new StreamWriter(_currFile);

				// Write to the file
				if (start)
				{
					_sw.WriteLine("==START============================================================");
					_sw.WriteLine("Sync started on {0}\t{1}", DateTime.Now.ToShortDateString(),
											 DateTime.Now.ToLongTimeString());

					_filesRenamedCntSRC = 0;
					_filesCopiedCntSRC = 0;
					_filesDeletedCntSRC = 0;

					_filesRenamedSizeSRC = 0;
					_filesCopiedSizeSRC = 0;
					_filesDeletedSizeSRC = 0;

					_foldersCreatedCntSRC = 0;

					_filesRenamedCntTGT = 0;
					_filesCopiedCntTGT = 0;
					_filesDeletedCntTGT = 0;

					_filesRenamedSizeTGT = 0;
					_filesCopiedSizeTGT = 0;
					_filesDeletedSizeTGT = 0;

					_foldersCreatedCntTGT = 0;
				}
				else
				{
					_sw.WriteLine("*** Sync Results ***");
					_sw.WriteLine("ACTION          \t|FROM SOURCE\t\t|FROM TARGET");

					_sw.WriteLine("Files Copied :  \t[{0}\t({1} bytes)]\t\t[{2}\t({3} bytes)]", _filesCopiedCntSRC, _filesCopiedSizeSRC, _filesCopiedCntTGT, _filesCopiedSizeTGT);
					_sw.WriteLine("Files Deleted:  \t[{0}\t({1} bytes)]\t\t[{2}\t({3} bytes)]", _filesDeletedCntSRC, _filesDeletedSizeSRC, _filesDeletedCntTGT, _filesDeletedSizeTGT);
					_sw.WriteLine("Files Renamed:  \t[{0}\t({1} bytes)]\t\t[{2}\t({3} bytes)]", _filesRenamedCntSRC, _filesRenamedSizeSRC, _filesRenamedCntTGT, _filesRenamedSizeTGT);
					_sw.WriteLine("Folders Created:  \t[{0}]\t\t\t[{1}]", _foldersCreatedCntSRC, _foldersCreatedCntTGT);
					_sw.WriteLine("*********************");
					_sw.WriteLine("Sync ended on {0}\t{1}", DateTime.Now.ToShortDateString(),
											 DateTime.Now.ToLongTimeString());
					_sw.WriteLine("==END==============================================================");
					_sw.WriteLine();
					_sw.WriteLine();
				}
			}
			catch (IOException e)
			{
				WriteErrorLog(e.Message);
				return false;

				//throw;
			}
			finally
			{
				// Close StreamWriter
				_sw.Close();

				// Close file
				_currFile.Close();
			}
			return true;
		}

		public static bool WriteLog(LogType logType, string srcPath, long srcSize, string tgtPath, long tgtSize)
		{
			/*            if (srcSize < 0 || srcSize > MaxUlong) throw new ArgumentOutOfRangeException("srcSize");
									if (tgtSize < 0 || tgtSize > MaxUlong) throw new ArgumentOutOfRangeException("tgtSize");*/

			try
			{
				string status;
				string oriPath = "";
				long oriSize = 0;
				string destPath = "";
				long destSize = 0;

				switch (logType)
				{
					case LogType.CopySRC:
						status = CopyStatusSRC;
						break;
					case LogType.CopyTGT:
						status = CopyStatusTGT;
						break;
					case LogType.DeleteSRC:
						status = DeleteStatusSRC;
						break;
					case LogType.DeleteTGT:
						status = DeleteStatusTGT;
						break;
					case LogType.RenameSRC:
						status = RenameStatusSRC;
						break;
					case LogType.RenameTGT:
						status = RenameStatusTGT;
						break;
					case LogType.CreateSRC:
						status = CreateStatusSRC;
						break;
					case LogType.CreateTGT:
						status = CreateStatusTGT;
						break;
					default:
						throw new ArgumentException("logType must be of type enum LogType {CopySRC, CopyTGT, DeleteSRC, DeleteTGT, RenameSRC, RenameTGT, CreateSRC, CreateTGT }", "logType");
				}

				DirectoryInfo di = new DirectoryInfo(_logFileLocation);
				di.Create();
				_currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
				_sw = new StreamWriter(_currFile);

				switch (logType)
				{
					case LogType.DeleteSRC:
					case LogType.DeleteTGT:

						//delete OK or create folder OK originate from machine side or from usb side
						if ((srcPath != null && tgtPath == null) || (srcPath == null && tgtPath != null))
						{
							//update counter
							switch (logType)
							{
								case LogType.DeleteSRC:
									_filesDeletedCntSRC++;
									_filesDeletedSizeSRC += srcSize;
									oriPath = srcPath;
									oriSize = srcSize;
									//log entry format:
									//|date|time|status|oriPath|oriSize|destPath|destSize|
									_sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}\t({3} bytes)]",
																					 DateTime.Now.ToShortDateString(),
																					 DateTime.Now.ToLongTimeString(), oriPath, oriSize);
									break;
								case LogType.DeleteTGT:
									_filesDeletedCntTGT++;
									_filesDeletedSizeTGT += srcSize;
									oriPath = tgtPath;
									oriSize = tgtSize;
									//log entry format:
									//|date|time|status|oriPath|oriSize|destPath|destSize|
									_sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}\t({3} bytes)]",
																					 DateTime.Now.ToShortDateString(),
																					 DateTime.Now.ToLongTimeString(), oriPath, oriSize);
									break;
								case LogType.CreateSRC:
									_foldersCreatedCntSRC++;
									oriPath = srcPath;
									//log entry format:
									//|date|time|status|oriPath|oriSize|destPath|destSize|
									_sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}]",
																					 DateTime.Now.ToShortDateString(),
																					 DateTime.Now.ToLongTimeString(), oriPath);
									break;
								case LogType.CreateTGT:
									_foldersCreatedCntTGT++;
									oriPath = tgtPath;
									//log entry format:
									//|date|time|status|oriPath|oriSize|destPath|destSize|
									_sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}]",
																					 DateTime.Now.ToShortDateString(),
																					 DateTime.Now.ToLongTimeString(), oriPath);
									break;
							}
						}
						else
						{
							return false;
						}
						break;

					case LogType.CopySRC:
					case LogType.CopyTGT:
					case LogType.RenameSRC:
					case LogType.RenameTGT:

						//copy or rename OK originate from machine side or from usb side
						if (srcPath != null && tgtPath != null)
						{
							//update counter
							switch (logType)
							{
								case LogType.CopySRC:
									_filesCopiedCntSRC++;
									_filesCopiedSizeSRC += srcSize;
									oriPath = srcPath;
									oriSize = srcSize;
									destPath = tgtPath;
									destSize = tgtSize;
									break;
								case LogType.RenameSRC:
									_filesRenamedCntSRC++;
									_filesRenamedSizeSRC += srcSize;
									oriPath = srcPath;
									oriSize = srcSize;
									destPath = tgtPath;
									destSize = tgtSize;
									break;
								case LogType.CopyTGT:
									_filesCopiedCntTGT++;
									_filesCopiedSizeTGT += tgtSize;
									oriPath = tgtPath;
									oriSize = tgtSize;
									destPath = srcPath;
									destSize = srcSize;
									break;
								case LogType.RenameTGT:
									_filesRenamedCntTGT++;
									_filesRenamedSizeTGT += tgtSize;
									oriPath = tgtPath;
									oriSize = tgtSize;
									destPath = srcPath;
									destSize = srcSize;
									break;
							}

							//log entry format:
							//|date|time|status|oriPath|oriSize|destPath|destSize|
							_sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}\t({3} bytes)] =-> [{4}\t({5} bytes)]",
													 DateTime.Now.ToShortDateString(),
																										 DateTime.Now.ToLongTimeString(), oriPath, oriSize,
																										 destPath, destSize);
						}
						else
						{
							return false;
						}
						break;
				}
			}
			catch (IOException e)
			{
				WriteErrorLog(e.Message);
				return false;

				//throw;
			}
			finally
			{
				// Close StreamWriter
				_sw.Close();

				// Close file
				_currFile.Close();
			}
			return true;

		}

		//public static bool DeleteLog(string metaDataDir, string syncTaskName, bool errorLog)
		//{

		//  string logFileToDeletePath;
		//  if (metaDataDir == null) throw new ArgumentNullException("machineId");
		//  if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");

		//  if (errorLog)
		//  {
		//    logFileToDeletePath = metaDataDir + @"\" + syncTaskName + ".log";
		//  }
		//  else
		//  {
		//    logFileToDeletePath = metaDataDir + @"\" + syncTaskName + ".log";
		//  }

		//  if (File.Exists(logFileToDeletePath))
		//  {
		//    // Use a try block to catch IOExceptions, to
		//    // handle the case of the file already being
		//    // opened by another process.
		//    try
		//    {
		//      File.Delete(logFileToDeletePath);
		//    }
		//    catch (IOException e)
		//    {
		//      WriteErrorLog(metaDataDir, syncTaskName, e.Message);
		//      return false;
		//    }
		//  }
		//  return true;
		//}

		//public static string ReadLog(string metaDataDir, string syncTaskName)
		//{
		//  FileStream _currFile;
		//  StreamReader sr;

		//  if (metaDataDir == null) throw new ArgumentNullException("machineId");
		//  if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");

		//  // *** Read from file ***
		//  string retrievedLogEntry;
		//  try
		//  {
		//    // Specify file, instructions, and privilegdes
		//    _currFile = new FileStream(@".\Log\" + syncTaskName + ".log", FileMode.Open, FileAccess.Read);

		//    // Create a new stream to read from a file
		//    sr = new StreamReader(_currFile);

		//    // Read contents of file into a string
		//    retrievedLogEntry = sr.ReadToEnd();

		//    // Close StreamReader
		//    sr.Close();

		//    // Close file
		//    _currFile.Close();
		//  }

		//  catch (IOException e)
		//  {
		//    WriteErrorLog(metaDataDir, syncTaskName, e.Message);
		//    return null;

		//    //throw;
		//  }

		//  return retrievedLogEntry;
		//}

		//general program error log
		public static bool WriteErrorLog(string errorMsg)
		{

			if (errorMsg == null) throw new ArgumentNullException("errorMsg");
			if (errorMsg.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "errorMsg");


			try
			{
				DirectoryInfo di;

				di = new DirectoryInfo(_logFileLocation);
				di.Create();
				_currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
				_sw = new StreamWriter(_currFile);

				//log entry format:
				//|date|time|errorMsg
				_sw.WriteLine("{0}\t{1}\t[***ERROR***]: {2}",
				DateTime.Now.ToShortDateString(),
				DateTime.Now.ToLongTimeString(), errorMsg);

			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message);
				return false;

				//throw;
			}
			finally
			{
				// Close StreamWriter
				_sw.Close();

				// Close file
				_currFile.Close();
			}
			return true;
		}
		#endregion
	}
}