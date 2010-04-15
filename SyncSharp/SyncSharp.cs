using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SyncSharp.GUI;
using SyncSharp.Business;
using SyncSharp.Storage;

namespace SyncSharp
{
	static class SyncSharp
	{
        // Written by Guo Jiayuan and Loh Jianxiong Christoper 
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			try
			{
				SyncSharpLogic logic = new SyncSharpLogic();
				logic.LoadProfile();
				if (!logic.SaveProfile()) return;

				logic.UpdateRemovableRoot();
				bool runAutoForm = false;

				if (logic.Profile.TaskCollection != null)
				{
					foreach (SyncTask task in logic.Profile.TaskCollection)
					{
						runAutoForm = task.Settings.PlugSync;
						if (runAutoForm) break;
					}
				}

				if (runAutoForm)
				{
					AutoRunForm autoRunFrm = new AutoRunForm(logic);
					logic.AddUI(autoRunFrm);
					Application.Run(autoRunFrm);
				}
				MainForm mainFrm = new MainForm(logic);
				logic.RemoveAllUIs();
				logic.AddUI(mainFrm);
				Application.Run(mainFrm);
			}
			catch (Exception e)
			{
				Logger.WriteSystemErrorLog(e.StackTrace);
			}
		}
	}
}