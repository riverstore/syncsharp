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
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            SyncSharpLogic logic = new SyncSharpLogic();
            logic.loadProfile();
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
	}
}