using System;
using System.Windows.Forms;

namespace TextSplit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initiates Globals
            Globals.StartGlobals();

            // Cleans up after possible PerformUpdate call
            Globals.AutoUpdater.DeletePreviousUpdateFiles();

            // Checks for new updates
            bool check = Globals.AutoUpdater.DoUpdateProcedure();

            // If there are no new updates
            if (!check) {
                try {
                    Application.Run(new TextSplitMain());
                } catch (Exception e) {
                    Globals.ShowErrorMessage("Failed to run application", e);
                }
            }
        }
    }
}
