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

        enum LogType { Copy, CopyErr, Delete, DeleteErr, Rename, RenameErr };


        private static int _filesMovedCntMachine;
        private static int _filesCopiedCntMachine;
        private static int _filesDeletedCntMachine;

        private static long _filesMovedSizeMachine;
        private static long _filesCopiedSizeMachine;
        private static long _filesDeletedSizeMachine;

        private static int _filesMovedCntUsb;
        private static int _filesCopiedCntUsb;
        private static int _filesDeletedCntUsb;

        private static long _filesMovedSizeUsb;
        private static long _filesCopiedSizeUsb;
        private static long _filesDeletedSizeUsb;


        private static int _filesMovedCntMachineErr;
        private static int _filesCopiedCntMachineErr;
        private static int _filesDeletedCntMachineErr;

        private static long _filesMovedSizeMachineErr;
        private static long _filesCopiedSizeMachineErr;
        private static long _filesDeletedSizeMachineErr;

        private static int _filesMovedCntUsbErr;
        private static int _filesCopiedCntUsbErr;
        private static int _filesDeletedCntUsbErr;

        private static long _filesMovedSizeUsbErr;
        private static long _filesCopiedSizeUsbErr;
        private static long _filesDeletedSizeUsbErr;
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
			return true;
		}
		public static bool WriteLog(int logType, string machineId, string syncTaskName, string machineSrcPath, long machineSrcSize, string machineDestPath, long machineDestSize, string usbSrcPath, long usbSrcSize, string usbDestPath, long usbDestSize) 
		{
			return true;
		}
        public static string ReadLog(string syncTaskName)
        { 
			return "";
        }     
        public static bool DeleteLog(string syncTaskName)
        {          
            return true;
        }
        #endregion
    }
}
