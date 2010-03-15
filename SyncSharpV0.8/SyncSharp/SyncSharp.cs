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
            foreach (SyncTask task in logic.Profile.TaskCollection) {
                if (task.Settings.PlugSync) {
                    runAutoForm = true;
                    break;
                }
            }
            
            if (runAutoForm)
                Application.Run(new AutoRunForm(logic));

            Application.Run(new MainForm(logic));
        }
    }
}
