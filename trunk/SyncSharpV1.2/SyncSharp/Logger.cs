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

		private static FileStream _currFile;
		private static StreamWriter _sw;

		private static string _logFileName;
		private static string _logFileLocation;

		private static uint _filesRenamedCntSRC;
		private static uint _filesCopiedCntSRC;
		private static uint _filesDeletedCntSRC;

		private static ulong _filesRenamedSizeSRC;
		private static ulong _filesCopiedSizeSRC;
		private static ulong _filesDeletedSizeSRC;

        private static uint _foldersCreatedCntSRC;

		private static uint _filesRenamedCntTGT;
		private static uint _filesCopiedCntTGT;
		private static uint _filesDeletedCntTGT;

		private static ulong _filesRenamedSizeTGT;
		private static ulong _filesCopiedSizeTGT;
		private static ulong _filesDeletedSizeTGT;
        
        private static uint _foldersCreatedCntTGT;

		#endregion

		#region properties

		#endregion

		#region methods

        /// <summary>
        /// Sync preview details log
        /// </summary>
        /// <param name="metaDataDir"></param>
        /// <param name="syncTaskName"></param>
        /// <param name="srcCopyTotal"></param>
        /// <param name="srcCopySize"></param>
        /// <param name="srcDeleteTotal"></param>
        /// <param name="srcDeleteSize"></param>
        /// <param name="srcRenameTotal"></param>
        /// <param name="srcRenameSize"></param>
        /// <param name="tgtCopyTotal"></param>
        /// <param name="tgtCopySize"></param>
        /// <param name="tgtDeleteTotal"></param>
        /// <param name="tgtDeleteSize"></param>
        /// <param name="tgtRenameTotal"></param>
        /// <param name="tgtRenameSize"></param>
        /// <returns>successful</returns>
        public static bool SyncPlanWriteLog(string metaDataDir, string syncTaskName, uint srcCopyTotal, ulong srcCopySize, uint srcDeleteTotal, ulong srcDeleteSize, uint srcRenameTotal, ulong srcRenameSize, uint tgtCopyTotal, ulong tgtCopySize, uint tgtDeleteTotal, ulong tgtDeleteSize, uint tgtRenameTotal, ulong tgtRenameSize)
		{
            if (metaDataDir == null) throw new ArgumentNullException("metaDataDir");
			if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
            if (metaDataDir.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "metaDataDir");
			if (syncTaskName.Equals(string.Empty)) throw new ArgumentException("Empty string passed", "syncTaskName");

			try
			{
				// Specify file, instructions, and privilegdes
				DirectoryInfo di = new DirectoryInfo(_logFileLocation);
				// Try to create the directory.
				di.Create();
				_currFile = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);

				// Create a new stream to write to the file
				_sw = new StreamWriter(_currFile);

				// Write to log
				SyncPlanLogEntry(srcCopyTotal, srcCopySize, tgtCopyTotal, tgtCopySize, srcDeleteTotal, srcDeleteSize, tgtDeleteTotal, tgtDeleteSize, srcRenameTotal, srcRenameSize, tgtRenameTotal, tgtRenameSize);
			}
			catch (IOException e)
			{
				WriteErrorLog(e.Message);
				return false;
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

	    private static void SyncPlanLogEntry(uint srcCopyTotal, ulong srcCopySize, uint tgtCopyTotal, ulong tgtCopySize, uint srcDeleteTotal, ulong srcDeleteSize, uint tgtDeleteTotal, ulong tgtDeleteSize, uint srcRenameTotal, ulong srcRenameSize, uint tgtRenameTotal, ulong tgtRenameSize)
	    {
	        _sw.WriteLine("*** Sync Plan ***");
	        _sw.WriteLine("ACTION          \tsource\t\ttarget");

	        _sw.WriteLine("Files to COPY  :\t{0}\t{1}\t{2}\t{3}\t", srcCopyTotal, srcCopySize, tgtCopyTotal, tgtCopySize);
	        _sw.WriteLine("Files to DELETE:\t{0}\t{1}\t{2}\t{3}\t", srcDeleteTotal, srcDeleteSize, tgtDeleteTotal, tgtDeleteSize);
	        _sw.WriteLine("Files to RENAME:\t{0}\t{1}\t{2}\t{3}\t", srcRenameTotal, srcRenameSize, tgtRenameTotal, tgtRenameSize);
	        _sw.WriteLine("*****************");
	        _sw.WriteLine("#Sync executing#");
	        _sw.WriteLine("#Format:|date|time|status|source source|size|source dest|size|target source|size|target dest|size|error msg|");
	        _sw.WriteLine("#");
	    }

        /// <summary>
        /// Set begin or end of log recording for a sync task
        /// </summary>
        /// <param name="metaDataDir"></param>
        /// <param name="syncTaskName"></param>
        /// <param name="start">true: begin, false: end</param>
        /// <returns>successful</returns>
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
					SyncBeginLogEntry();
				}
				else
				{
					SyncResultsLogEntry();
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

	    private static void SyncBeginLogEntry()
	    {
            _sw.WriteLine("==============================START==============================");
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

	    private static void SyncResultsLogEntry()
	    {
            _sw.WriteLine();
	        _sw.WriteLine("*** Sync Results ***");
	        _sw.WriteLine("ACTION          \t|FROM SOURCE\t\t|FROM TARGET");

	        _sw.WriteLine("Files Copied :  \t[{0}\t({1} bytes)]\t\t[{2}\t({3} bytes)]", _filesCopiedCntSRC, _filesCopiedSizeSRC, _filesCopiedCntTGT, _filesCopiedSizeTGT);
	        _sw.WriteLine("Files Deleted:  \t[{0}\t({1} bytes)]\t\t[{2}\t({3} bytes)]", _filesDeletedCntSRC, _filesDeletedSizeSRC, _filesDeletedCntTGT, _filesDeletedSizeTGT);
	        _sw.WriteLine("Files Renamed:  \t[{0}\t({1} bytes)]\t\t[{2}\t({3} bytes)]", _filesRenamedCntSRC, _filesRenamedSizeSRC, _filesRenamedCntTGT, _filesRenamedSizeTGT);
	        _sw.WriteLine("Folders Created:  \t[{0}]\t\t\t[{1}]", _foldersCreatedCntSRC, _foldersCreatedCntTGT);
	        _sw.WriteLine("*********************");
	        _sw.WriteLine("Sync ended on {0}\t{1}", DateTime.Now.ToShortDateString(),
	                      DateTime.Now.ToLongTimeString());
            _sw.WriteLine("==============================END==============================");
	        _sw.WriteLine();
	        _sw.WriteLine();
	    }

        /// <summary>
        /// Write log for copy, rename, delete files or create folders.
        ///  </summary>
        /// <param name="logType"></param>
        /// <param name="srcPath"></param>
        /// <param name="srcSize"></param>
        /// <param name="tgtPath"></param>
        /// <param name="tgtSize"></param>
        /// <returns>successful</returns>
	    public static bool WriteLog(LogType logType, string srcPath, ulong srcSize, string tgtPath, ulong tgtSize)
		{
			try
			{
			    string status;
			    string oriPath = "";
			    ulong oriSize = 0;
			    string destPath = "";
                ulong destSize = 0;

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
                    case LogType.CreateSRC:
                    case LogType.CreateTGT:
                    
                        //delete OK or create folder OK originate from source side or from target side
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
                                    DeleteLogEntry(status, oriPath, oriSize);	
                                break;

                                case LogType.DeleteTGT:
                                    _filesDeletedCntTGT++;
                                    _filesDeletedSizeTGT += srcSize;	
                                    oriPath = tgtPath;
                                    oriSize = tgtSize;                                    
                                    DeleteLogEntry(status, oriPath, oriSize);	
                                break;

                                case LogType.CreateSRC:
                                    _foldersCreatedCntSRC++;
							        oriPath = srcPath;                                    
                                    CreateLogEntry(status, oriPath);	
                                break;

                                case LogType.CreateTGT:
                                    _foldersCreatedCntTGT++;
                                    oriPath = tgtPath;
                                    CreateLogEntry(status, oriPath);	
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

                        //copy or rename OK originate from source side or from target side
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
                            //write to log
                            CopyRenameLogEntry(status, oriPath, oriSize, destPath, destSize);
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

	    private static void CopyRenameLogEntry(string status, string oriPath, ulong oriSize, string destPath, ulong destSize)
        {
            //file copy or Rename entry format:
            //|date|time|status|oriPath|oriSize|destPath|destSize|
	        _sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}\t({3} bytes)] =-> [{4}\t({5} bytes)]" ,
	                      DateTime.Now.ToShortDateString(),
	                      DateTime.Now.ToLongTimeString(), oriPath, oriSize,
	                      destPath, destSize);
	    }

	    private static void CreateLogEntry(string status, string oriPath)
	    {
            //file create Entry format:
            //|date|time|status|oriPath|
	        _sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}]",
	                      DateTime.Now.ToShortDateString(),
	                      DateTime.Now.ToLongTimeString(), oriPath);
	    }

	    private static void DeleteLogEntry(string status, string oriPath, ulong oriSize)
	    {
            //file delete Entry format:
            //|date|time|status|oriPath|oriSize|
	        _sw.WriteLine("{0}\t{1}\t" + status + "\t[{2}\t({3} bytes)]",
	                      DateTime.Now.ToShortDateString(),
	                      DateTime.Now.ToLongTimeString(), oriPath, oriSize);
	    }

        /// <summary>
        /// delete log file
        /// </summary>
        /// <param name="metaDataDir"></param>
        /// <param name="syncTaskName"></param>
        /// <returns>successful</returns>
	    public static bool DeleteLog(string metaDataDir, string syncTaskName)
		{
		  string logFileToDeletePath;
          if (metaDataDir == null) throw new ArgumentNullException("metaDataDir");
		  if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
	
		  logFileToDeletePath = metaDataDir + @"\" + syncTaskName + ".log";

		  if (File.Exists(logFileToDeletePath))
		  {
		    // Use a try block to catch IOExceptions, to
		    // handle the case of the file already being
		    // opened by another process.
		    try
		    {
		      File.Delete(logFileToDeletePath);
		    }
		    catch (IOException e)
		    {
		      WriteErrorLog(e.Message);
              WriteErrorLog(e.StackTrace);
		      return false;
		    }
		  }
		  return true;
		}

        /// <summary>
        /// read log file contents
        /// </summary>
        /// <param name="metaDataDir"></param>
        /// <param name="syncTaskName"></param>
        /// <returns>log contents</returns>
		public static string ReadLog(string metaDataDir, string syncTaskName)
		{
		  StreamReader sr;

          if (metaDataDir == null) throw new ArgumentNullException("metaDataDir");
		  if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");

		  // *** Read from file ***
		  string retrievedLogEntry;
		  try
		  {
		    // Specify file, instructions, and privilegdes
		    _currFile = new FileStream(metaDataDir + @".\" + syncTaskName + ".log", FileMode.Open, FileAccess.Read);

		    // Create a new stream to read from a file
		    sr = new StreamReader(_currFile);

		    // Read contents of file into a string
		    retrievedLogEntry = sr.ReadToEnd();

		    // Close StreamReader
		    sr.Close();

		    // Close file
		    _currFile.Close();
		  }

		  catch (IOException e)
		  {
		    WriteErrorLog(e.Message);
		    return null;
		  }

		  return retrievedLogEntry;
		}

        /// <summary>
        /// general program error log
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns>successful</returns>
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