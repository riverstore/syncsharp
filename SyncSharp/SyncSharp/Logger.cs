using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SyncSharp.Storage
{
    class Logger
    {
        #region attributes

        private FileStream _currFile;
        private StreamWriter _sw;
        private StreamReader _sr;

        #endregion
        #region properties

        #endregion
        #region methods

        public string ReadLog(string syncTaskName)
        {
            if (syncTaskName == null)
            {
                Console.WriteLine("string null: syncTaskName in Logger.WriteLog()");
                return null;
            }
  
            // *** Read from file ***
            string retrievedLogEntry;
            try
            {
                // Specify file, instructions, and privilegdes
                _currFile = new FileStream(syncTaskName + ".log", FileMode.OpenOrCreate, FileAccess.Read);

                // Create a new stream to read from a file
                _sr = new StreamReader(_currFile);

                // Read contents of file into a string
                retrievedLogEntry = _sr.ReadToEnd();

                // Close StreamReader
                _sr.Close();

                // Close file
                _currFile.Close();
            }

            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            
                //throw;
            }

            return retrievedLogEntry;
        }

        public bool WriteLog(string syncTaskName, string logEntry, string logType) 
        {
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
                _currFile = new FileStream(syncTaskName + ".log", FileMode.OpenOrCreate, FileAccess.Write);

                // Create a new stream to write to the file
                _sw = new StreamWriter(_currFile);

                // Write a string to the file
                _sw.Write(DateTime.Now);
                _sw.Write("\t");
                _sw.Write(logType);
                _sw.Write("\t");
                _sw.Write(logEntry);
                _sw.Write("\n");

                // Close StreamWriter
                _sw.Close();

                // Close file
                _currFile.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return false;

                //throw;
            }
            return true;

        }
        public bool DeleteLog(string syncTaskName)
        {
            string logFileToDeletePath;
            if(syncTaskName != null)          
                logFileToDeletePath = @".\Log\" + syncTaskName + ".log";
            else
            {
                Console.WriteLine("string null: syncTaskName in Logger.DeleteLog()");
                return false;
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
        #endregion
    }
}
