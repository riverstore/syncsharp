using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SyncSharp.Storage
{

	public static class Logger
	{
		public static TextWriter logText;
		public static void CreateLog(String strFileLog)
		{
            CloseLog();
            logText = new StreamWriter(strFileLog, true);
		}

		public static void WriteEntry(String strEntry)
		{
			if (logText != null)
			{
				String strDate = DateTime.Now.ToShortDateString();
				String strTime = DateTime.Now.ToShortTimeString();
				String strLog = strDate + " " + strTime + "\t" + strEntry;
				logText.WriteLine(strLog);
			}
		}

		public static void CloseLog()
		{
			if (logText != null)
				logText.Close();
		}
	}
}