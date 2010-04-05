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

		public enum LogType { Copy, CopyErr, Delete, DeleteErr, Rename, RenameErr };

		private static FileStream currFile;
		private static StreamWriter sw;

		private static string _logFileName;
		private static string _logFileLocation;

		private static int _filesRenamedCntMachine;
		private static int _filesCopiedCntMachine;
		private static int _filesDeletedCntMachine;

		private static long _filesRenamedSizeMachine;
		private static long _filesCopiedSizeMachine;
		private static long _filesDeletedSizeMachine;

		private static int _filesRenamedCntUsb;
		private static int _filesCopiedCntUsb;
		private static int _filesDeletedCntUsb;

		private static long _filesRenamedSizeUsb;
		private static long _filesCopiedSizeUsb;
		private static long _filesDeletedSizeUsb;

		private static int _filesRenamedCntMachineErr;
		private static int _filesCopiedCntMachineErr;
		private static int _filesDeletedCntMachineErr;

		private static long _filesRenamedSizeMachineErr;
		private static long _filesCopiedSizeMachineErr;
		private static long _filesDeletedSizeMachineErr;

		private static int _filesRenamedCntUsbErr;
		private static int _filesCopiedCntUsbErr;
		private static int _filesDeletedCntUsbErr;

		private static long _filesRenamedSizeUsbErr;
		private static long _filesCopiedSizeUsbErr;
		private static long _filesDeletedSizeUsbErr;

		#endregion

		#region properties

		#endregion

		#region methods

		public static bool SyncPlanWriteLog(string metaDataDir, string syncTaskName, int machineCopyTotal, long machineCopySize, int machineDeleteTotal, long machineDeleteSize, int machineRenameTotal, long machineRenameSize, int usbCopyTotal, long usbCopySize, int usbDeleteTotal, long usbDeleteSize, int usbRenameTotal, long usbRenameSize)
		{
			if (metaDataDir == null) throw new ArgumentNullException("machineId");
			if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
			if (metaDataDir.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "machineId");
			if (syncTaskName.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "syncTaskName");

			try
			{
				// *** Write to file ***

				// Specify file, instructions, and privilegdes
				DirectoryInfo di = new DirectoryInfo(_logFileLocation);
				// Try to create the directory.
				di.Create();

				currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

				// Create a new stream to write to the file
				sw = new StreamWriter(currFile);

				// Write to the file

				sw.WriteLine("*** Sync Plan ***");
				sw.WriteLine("ACTION          \tMACHINE\t\tUSB");

				sw.WriteLine("Files to COPY  :\t{0}\t{1}\t{2}\t{3}\t", machineCopyTotal, machineCopySize, usbCopyTotal, usbCopySize);
				sw.WriteLine("Files to DELETE:\t{0}\t{1}\t{2}\t{3}\t", machineDeleteTotal, machineDeleteSize, usbDeleteTotal, usbDeleteSize);
				sw.WriteLine("Files to RENAME:\t{0}\t{1}\t{2}\t{3}\t", machineRenameTotal, machineRenameSize, usbRenameTotal, usbRenameSize);
				sw.WriteLine("*****************");
				sw.WriteLine("#Sync executing#");
				sw.WriteLine("#Format:|date|time|status|machine source|size|machine dest|size|usb source|size|usb dest|size|error msg|");
				sw.WriteLine("#");			
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
				sw.Close();

				// Close file
				currFile.Close();
			}
			return true;
		}

		public static bool SyncSetWriteLog(string metaDataDir, string syncTaskName, bool start)
		{
			if (metaDataDir == null) throw new ArgumentNullException("machineId");
			if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
			if (metaDataDir.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "machineId");
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

				currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

				// Create a new stream to write to the file
				sw = new StreamWriter(currFile);

				// Write to the file
				if (start)
				{
					sw.WriteLine("==START============================================================");
					sw.WriteLine("Sync started on {0}\t{1}", DateTime.Now.ToShortDateString(),
											 DateTime.Now.ToLongTimeString());
					

					_filesRenamedCntMachine = 0;
					_filesCopiedCntMachine = 0;
					_filesDeletedCntMachine = 0;

					_filesRenamedSizeMachine = 0;
					_filesCopiedSizeMachine = 0;
					_filesDeletedSizeMachine = 0;

					_filesRenamedCntUsb = 0;
					_filesCopiedCntUsb = 0;
					_filesDeletedCntUsb = 0;

					_filesRenamedSizeUsb = 0;
					_filesCopiedSizeUsb = 0;
					_filesDeletedSizeUsb = 0;

					_filesRenamedCntMachineErr = 0;
					_filesCopiedCntMachineErr = 0;
					_filesDeletedCntMachineErr = 0;

					_filesRenamedSizeMachineErr = 0;
					_filesCopiedSizeMachineErr = 0;
					_filesDeletedSizeMachineErr = 0;

					_filesRenamedCntUsbErr = 0;
					_filesCopiedCntUsbErr = 0;
					_filesDeletedCntUsbErr = 0;

					_filesRenamedSizeUsbErr = 0;
					_filesCopiedSizeUsbErr = 0;
					_filesDeletedSizeUsbErr = 0;
				}
				else
				{
					sw.WriteLine("*** Sync Results ***");
					sw.WriteLine("ACTION          \tFROM SOURCE\t\tFROM TARGET");

					sw.WriteLine("Files Copied :  \t{0}\t[{1} bytes]\t\t{2}\t[{3} bytes]", _filesCopiedCntMachine, _filesCopiedSizeMachine, _filesCopiedCntUsb, _filesCopiedSizeUsb);
					sw.WriteLine("Files Deleted:  \t{0}\t[{1} bytes]\t\t{2}\t[{3} bytes]", _filesDeletedCntMachine, _filesDeletedSizeMachine, _filesDeletedCntUsb, _filesDeletedSizeUsb);
					sw.WriteLine("Files Renamed:  \t{0}\t[{1} bytes]\t\t{2}\t[{3} bytes]", _filesRenamedCntMachine, _filesRenamedSizeMachine, _filesRenamedCntUsb, _filesRenamedSizeUsb);
					sw.WriteLine("*********************");
					sw.WriteLine("Sync ended on {0}\t{1}", DateTime.Now.ToShortDateString(),
											 DateTime.Now.ToLongTimeString());
					sw.WriteLine("==END==============================================================");
					sw.WriteLine();
					sw.WriteLine();
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
				sw.Close();

				// Close file
				currFile.Close();
			}
			return true;
		}

		public static bool WriteLog(int logType, string machineSrcPath, long machineSrcSize, string machineDestPath, long machineDestSize, string usbSrcPath, long usbSrcSize, string usbDestPath, long usbDestSize, string errorMsg)
		{
			//if (metaDataDir == null) throw new ArgumentNullException("machineId");
			//if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
			//if (metaDataDir.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "machineId");
			//if (syncTaskName.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "syncTaskName");

			try
			{
				DirectoryInfo di;
				string status;

				switch (logType)
				{
					case (int)LogType.Copy:
						status = "[COPY OK]";
						break;
					case (int)LogType.Delete:
						status = "[DELETE OK]";
						break;
					case (int)LogType.Rename:
						status = "[RENAME OK]";
						break;
					case (int)LogType.CopyErr:
						status = "[COPY ERR]";
						break;
					case (int)LogType.DeleteErr:
						status = "[DELETE ERR]";
						break;
					case (int)LogType.RenameErr:
						status = "[RENAME ERR]";
						break;
					default:
						//status = "[MISC]";
						throw new ArgumentException("logType must be of type enum LogType {Copy, CopyErr, Delete, DeleteErr, Rename, RenameErr};", "logType");

					//break;
				}

				switch (logType)
				{
					case (int)LogType.Copy:
					case (int)LogType.Delete:
					case (int)LogType.Rename:

						di = new DirectoryInfo(_logFileLocation);
						di.Create();
						currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
						sw = new StreamWriter(currFile);

						break;

					case (int)LogType.CopyErr:
					case (int)LogType.DeleteErr:
					case (int)LogType.RenameErr:

						di = new DirectoryInfo(_logFileLocation);
						di.Create();
						currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
						sw = new StreamWriter(currFile);

						break;
					default:
						currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
						sw = new StreamWriter(currFile);
						break;
				}

				switch (logType)
				{
					case (int)LogType.Delete:
					case (int)LogType.DeleteErr:
						//delete OK/ERROR originate from machine side
						if (machineSrcPath != null && machineDestPath == null && usbSrcPath == null && usbDestPath == null)
						{
							//log entry format:
							//|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|errorMsg
							sw.WriteLine("{0}\t{1}\t" + status + " From Source" + "\t{2}\t[{3} bytes]" + ((errorMsg == null) ? "\t" : "\t" + errorMsg),
													 DateTime.Now.ToShortDateString(),
													 DateTime.Now.ToLongTimeString(), machineSrcPath, machineSrcSize);

							//update counter
							switch (logType)
							{
								case (int)LogType.Delete:
									_filesDeletedCntMachine++;
									_filesDeletedSizeMachine += machineSrcSize;
									break;
								case (int)LogType.DeleteErr:
									_filesDeletedCntMachineErr++;
									_filesDeletedSizeMachineErr += machineSrcSize;
									break;
							}
						}
						//delete OK/ERROR originate from usb side
						else if (machineSrcPath != null && machineDestPath != null && usbSrcPath == null && usbDestPath != null)
						{
							//log entry format:
							//|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|errorMsg
							sw.WriteLine("{0}\t{1}\t" + status + " From Target" + "\t{2}\t[{3} bytes]" + ((errorMsg == null) ? "\t" : "\t" + errorMsg),
									DateTime.Now.ToShortDateString(),
													 DateTime.Now.ToLongTimeString(), usbSrcPath, usbSrcSize);

							//update counter
							switch (logType)
							{
								case (int)LogType.Delete:
									_filesDeletedCntUsb++;
									_filesDeletedSizeUsb += usbSrcSize;
									break;
								case (int)LogType.DeleteErr:
									_filesDeletedCntUsbErr++;
									_filesDeletedSizeUsbErr += usbSrcSize;
									break;
							}
						}
						else
						{
							return false;
						}
						break;


					case (int)LogType.Copy:
					case (int)LogType.Rename:
					case (int)LogType.RenameErr:
					case (int)LogType.CopyErr:
						//copy or rename OK/ERROR originate from machine side
						if (machineSrcPath != null && machineDestPath != null && usbSrcPath == null && usbDestPath == null)
						{
							//log entry format:
							//|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|errorMsg
							sw.WriteLine("{0}\t{1}\t" + status + " From Source" + "\t{2}\t[{3} bytes]\t{4}\t[{5} bytes]" + ((errorMsg == null) ? "\t" : "\t" + errorMsg),
													 DateTime.Now.ToShortDateString(),
													 DateTime.Now.ToLongTimeString(), machineSrcPath, machineSrcSize,
													 machineDestPath, machineDestSize);

							//update counter
							switch (logType)
							{
								case (int)LogType.Copy:
									_filesCopiedCntMachine++;
									_filesCopiedSizeMachine += machineSrcSize;
									break;
								case (int)LogType.Rename:
									_filesRenamedCntMachine++;
									_filesRenamedSizeMachine += machineSrcSize;
									break;
								case (int)LogType.CopyErr:
									_filesCopiedCntMachineErr++;
									_filesCopiedSizeMachineErr += machineSrcSize;
									break;
								case (int)LogType.RenameErr:
									_filesRenamedCntMachineErr++;
									_filesRenamedSizeMachineErr += machineSrcSize;
									break;
							}
						}

						//copy or rename OK/ERROR originate from usb side
						else if (machineSrcPath == null && machineDestPath == null && usbSrcPath != null && usbDestPath != null)
						{
							//log entry format:
							//|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|errorMsg
							sw.WriteLine("{0}\t{1}\t" + status + " From Target" + "\t{2}\t[{3} bytes]\t{4}\t[{5} bytes]" + ((errorMsg == null) ? "\t-" : "\t" + errorMsg),
													 DateTime.Now.ToShortDateString(),
													 DateTime.Now.ToLongTimeString(), usbSrcPath, usbSrcSize,
													 usbDestPath, usbDestSize);

							//update counter
							switch (logType)
							{
								case (int)LogType.Copy:
									_filesCopiedCntUsb++;
									_filesCopiedSizeUsb += usbSrcSize;
									break;
								case (int)LogType.Rename:
									_filesRenamedCntUsb++;
									_filesRenamedSizeUsb += usbSrcSize;
									break;
								case (int)LogType.CopyErr:
									_filesCopiedCntUsbErr++;
									_filesCopiedSizeUsbErr += usbSrcSize;
									break;
								case (int)LogType.RenameErr:
									_filesRenamedCntUsbErr++;
									_filesRenamedSizeUsbErr += usbSrcSize;
									break;
							}
						}
						else
						{
							return false;
						}
						break;
				}
				////////////////////////////////////////////////
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
				sw.Close();

				// Close file
				currFile.Close();
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
		//  FileStream currFile;
		//  StreamReader sr;

		//  if (metaDataDir == null) throw new ArgumentNullException("machineId");
		//  if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");

		//  // *** Read from file ***
		//  string retrievedLogEntry;
		//  try
		//  {
		//    // Specify file, instructions, and privilegdes
		//    currFile = new FileStream(@".\Log\" + syncTaskName + ".log", FileMode.Open, FileAccess.Read);

		//    // Create a new stream to read from a file
		//    sr = new StreamReader(currFile);

		//    // Read contents of file into a string
		//    retrievedLogEntry = sr.ReadToEnd();

		//    // Close StreamReader
		//    sr.Close();

		//    // Close file
		//    currFile.Close();
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
				currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
				sw = new StreamWriter(currFile);

				//log entry format:
				//|date|time|errorMsg
				sw.WriteLine("{0}\t{1}\t***ERROR***: {2}",
				DateTime.Now.ToShortDateString(),
				DateTime.Now.ToLongTimeString(), errorMsg);

				// Close StreamWriter
				sw.Close();

				// Close file
				currFile.Close();
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
				sw.Close();

				// Close file
				currFile.Close();
			}
			return true;
		}
		#endregion
	}
}