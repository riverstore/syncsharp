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

        //private static FileStream _currFile;
        //private static StreamWriter _sw;
        //private static StreamReader _sr;

        enum LogType {Copy, CopyErr, Delete, DeleteErr, Rename, RenameErr};

        
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

//#if true
        
//#endif

        #endregion
        #region properties

        #endregion
        #region methods

        public static bool SyncPlanWriteLog(string machineId, string syncTaskName, int machineCopyTotal, long machineCopySize, int machineDeleteTotal, long machineDeleteSize, int machineRenameTotal, long machineRenameSize, int usbCopyTotal, long usbCopySize, int usbDeleteTotal, long usbDeleteSize, int usbRenameTotal, long usbRenameSize)
        {
            return true;
        }

        public static bool SyncSetWriteLog(string machineId, string syncTaskName, bool start)
        {
            FileStream currFile;
            StreamWriter sw;
  

            if (syncTaskName == null)
            {
                Console.WriteLine("string null: syncTaskName in Logger.StartSyncWriteLog()");
                return false;
            }
            if (machineId == null)
            {
                Console.WriteLine("string null: machineId in Logger.StartSyncWriteLog()");
                return false;
            }
           
            try
            {
                // *** Write to file ***

                // Specify file, instructions, and privilegdes
                DirectoryInfo di = new DirectoryInfo(@".\Profiles\" + machineId + @"\Logs\Full");
                // Try to create the directory.
                di.Create();

                currFile = new FileStream(@".\Profiles\" + machineId + @"\Logs\Full\" + syncTaskName + ".log", FileMode.Append, FileAccess.Write, FileShare.Read);

                // Create a new stream to write to the file
                sw = new StreamWriter(currFile);

                // Write to the file
                if (start)
                {
                    sw.WriteLine("==START============================================================");
                    sw.WriteLine("Sync started on {0}\t{1}", DateTime.Now.ToShortDateString(),
                                 DateTime.Now.ToLongTimeString());
                }
                else
                {                    
                    sw.WriteLine("Sync started on {0}\t{1}", DateTime.Now.ToShortDateString(),
                                 DateTime.Now.ToLongTimeString());
                    sw.WriteLine("==END==============================================================");
                }

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
            return true;
        }
  
        public static bool WriteLog(int logType, string machineId, string syncTaskName, string machineSrcPath, long machineSrcSize, string machineDestPath, long machineDestSize, string usbSrcPath, long usbSrcSize, string usbDestPath, long usbDestSize) 
        {
            FileStream currFile;
            StreamWriter sw;
      

            if (machineId == null) throw new ArgumentNullException("machineId");
            if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");
/*
            if (machineSrcPath == null) throw new ArgumentNullException("machineSrcPath");
            if (machineDestPath == null) throw new ArgumentNullException("machineDestPath");
            if (usbSrcPath == null) throw new ArgumentNullException("usbSrcPath");
            if (usbDestPath == null) throw new ArgumentNullException("usbDestPath");*/
  

            try
            {
                DirectoryInfo di;
                string status;

                switch (logType)
                {
                    case (int) LogType.Copy:
                        status = "[COPY OK]";
                        break;
                    case (int) LogType.Rename:
                        status = "[RENAME OK]";
                        break;
                    case (int) LogType.Delete:
                        status = "[DELETE OK]";
                        break;
                    case (int) LogType.CopyErr:
                        status = "[COPY ERR]";
                        break;
                    case (int) LogType.RenameErr:
                        status = "[RENAME ERR]";
                        break;
                    case (int) LogType.DeleteErr:
                        status = "[DELETE ERR]";
                        break;
                    default:
                        //status = "[MISC]";
                        throw new ArgumentException("logType must be of type enum LogType {Copy, CopyErr, Delete, DeleteErr, Rename, RenameErr};", "logType");
                        //break;
                }

                switch (logType)
                {
                    case (int) LogType.Rename:
                    case (int) LogType.Copy:
                    case (int) LogType.Delete:

                        di = new DirectoryInfo(@".\Profiles\" + machineId + @"\Logs\Full");
                        di.Create();
                        currFile = new FileStream(@".\Profiles\" + machineId + @"\Logs\Full\" + syncTaskName + ".log", FileMode.Append, FileAccess.Write, FileShare.Read);
                        sw = new StreamWriter(currFile);                                                

                        break;

                    case (int) LogType.RenameErr:
                    case (int) LogType.CopyErr:
                    case (int) LogType.DeleteErr:

                        di = new DirectoryInfo(@".\Profiles\" + machineId + @"\Logs\Err");
                        di.Create();
                        currFile = new FileStream(@".\Profiles\" + machineId + @"\Logs\Err\" + syncTaskName + ".log", FileMode.Append, FileAccess.Write, FileShare.Read);
                        sw = new StreamWriter(currFile);

                        break;     
                    default:
                        di = new DirectoryInfo(@".\Profiles\" + machineId + @"\Logs\Err");
                        di.Create();
                        currFile = new FileStream(@".\Profiles\" + machineId + @"\Logs\Err\" + syncTaskName + ".log", FileMode.Append, FileAccess.Write, FileShare.Read);
                        sw = new StreamWriter(currFile);
                        break;

                }

                    
                    
                switch (logType)
                {
                    case (int) LogType.Delete:
                    case (int) LogType.DeleteErr:
                        //delete OK/ERROR originate from machine side
                        if(machineSrcPath != null && machineDestPath == null && usbSrcPath == null && usbDestPath == null)
                        {      
                            //log entry format:
                            //|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|
                            sw.WriteLine("{0}\t{1}\t" + status + "\t{2}\t{3}\t---\t---\t---\t---\t---\t---",
                                         DateTime.Now.ToShortDateString(),
                                         DateTime.Now.ToLongTimeString(), machineSrcPath, machineSrcSize);                            
                                        
                            //update counter
                            switch (logType)
                            {
                                case (int) LogType.Delete:
                                    _filesDeletedCntMachine++;
                                    _filesDeletedSizeMachine += machineSrcSize;
                                    break;
                                case (int) LogType.DeleteErr:
                                    _filesDeletedCntMachineErr++;
                                    _filesDeletedSizeMachineErr += machineSrcSize;
                                    break;
                            }
                        }
                        //delete OK/ERROR originate from usb side
                        else if(machineSrcPath != null && machineDestPath != null && usbSrcPath == null && usbDestPath != null)
                        {
                            //log entry format:
                            //|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|
                            sw.WriteLine("{0}\t{1}\t" + status + "\t---\t---\t---\t---\t{2}\t{3}\t---\t---",
                                         DateTime.Now.ToShortDateString(),
                                         DateTime.Now.ToLongTimeString(), usbSrcPath, usbSrcSize);

                            //update counter
                            switch (logType)
                            {
                                case (int) LogType.Delete:
                                    _filesDeletedCntUsb++;
                                    _filesDeletedSizeUsb += usbSrcSize;
                                    break;
                                case (int) LogType.DeleteErr:
                                    _filesDeletedCntUsbErr++;
                                    _filesDeletedSizeUsbErr += usbSrcSize;
                                    break;
                            }
                        }
                        break;

                    case (int) LogType.Rename:
                    case (int) LogType.Copy:
                    case (int) LogType.RenameErr:
                    case (int) LogType.CopyErr:
                        //copy or rename OK/ERROR originate from machine side
                        if(machineSrcPath != null && machineDestPath != null && usbSrcPath == null && usbDestPath == null)
                        {
                            //log entry format:
                            //|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|
                            sw.WriteLine("{0}\t{1}\t" + status + "\t{2}\t{3}\t{4}\t{5}\t---\t---\t---\t---",
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
                                case (int)LogType.CopyErr:
                                    _filesCopiedCntMachineErr++;
                                    _filesCopiedSizeMachineErr += machineSrcSize;
                                    break;
                                case (int)LogType.Rename:
                                    _filesRenamedCntMachine++;
                                    _filesRenamedSizeMachine += machineSrcSize;
                                    break;
                                case (int)LogType.RenameErr:
                                    _filesRenamedCntMachineErr++;
                                    _filesRenamedSizeMachineErr += machineSrcSize;
                                    break;
                            }
                        }

                        //copy or rename OK/ERROR originate from usb side
                        else if(machineSrcPath == null && machineDestPath == null && usbSrcPath != null && usbDestPath != null)
                        {
                            //log entry format:
                            //|date|time|status|machineSrcPath|machineSrcSize|machineDestPath|machineDestSize|usbSrcPath|usbSrcSize|usbDestPath|usbDestSize|
                            sw.WriteLine("{0}\t{1}\t" + status + "\t---\t---\t---\t---\t{2}\t{3}\t{4}\t{5}",
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
                                case (int)LogType.CopyErr:
                                    _filesCopiedCntUsbErr++;
                                    _filesCopiedSizeUsbErr += usbSrcSize;
                                    break;
                                case (int)LogType.Rename:
                                    _filesRenamedCntUsb++;
                                    _filesRenamedSizeUsb += usbSrcSize;
                                    break;
                                case (int)LogType.RenameErr:
                                    _filesRenamedCntUsbErr++;
                                    _filesRenamedSizeUsbErr += usbSrcSize;
                                    break;
                            }
                        }
                        break;
                }
                ////////////////////////////////////////////////

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
            return true;

        }

  /*        public static bool WriteLog(string syncTaskName, string logEntry, string logType) 
      {
            FileStream currFile;
            StreamWriter sw;
            StreamReader sr;

            if (syncTaskName == null)
            {
                Console.WriteLine("string null: syncTaskName in Logger.WriteLog()");
                return false;
            }
            if (logEntry == null)
            {
                Console.WriteLine("string null: logEntry in Logger.WriteLog()");
                return false;
            }
            if (logType == null)
            {
                Console.WriteLine("string null: logType in Logger.WriteLog()");
                return false;
            }

            try
            {
                // *** Write to file ***
                
                // Specify file, instructions, and privilegdes
                DirectoryInfo di = new DirectoryInfo(@".\Log");
                // Try to create the directory.
                di.Create();               

                currFile = new FileStream(@".\Log\" + syncTaskName + ".log", FileMode.Append, FileAccess.Write,FileShare.Read);

                // Create a new stream to write to the file
                sw = new StreamWriter(currFile);                

                // Write a string to the file
                sw.WriteLine("{0}\t{1}\t{2}\t{3}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), logType, logEntry);
                

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
            return true;

        }*/

        public static bool DeleteLog(string machineId, string syncTaskName, bool errorLog)
        {

            string logFileToDeletePath;
            if (machineId == null) throw new ArgumentNullException("machineId");
            if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");

            if (errorLog)
            {
                logFileToDeletePath = @".\Profiles\" + machineId + @"\Logs\Err\" + syncTaskName + ".log";   
            }
            else
            {
                logFileToDeletePath = @".\Profiles\" + machineId + @"\Logs\Full\" + syncTaskName + ".log";   
            }                       

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
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return true;
        }

        public static string ReadLog(string machineId, string syncTaskName)
        {
            FileStream currFile;
            StreamReader sr;

            if (machineId == null) throw new ArgumentNullException("machineId");
            if (syncTaskName == null) throw new ArgumentNullException("syncTaskName");

            // *** Read from file ***
            string retrievedLogEntry;
            try
            {
                // Specify file, instructions, and privilegdes
                currFile = new FileStream(@".\Log\" + syncTaskName + ".log", FileMode.Open, FileAccess.Read);

                // Create a new stream to read from a file
                sr = new StreamReader(currFile);

                // Read contents of file into a string
                retrievedLogEntry = sr.ReadToEnd();

                // Close StreamReader
                sr.Close();

                // Close file
                currFile.Close();
            }

            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;

                //throw;
            }

            return retrievedLogEntry;
        }
        #endregion
    }
}
