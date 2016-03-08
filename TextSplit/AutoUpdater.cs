using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace TextSplit
{
    public class AutoUpdater
    {
        public string VersionNumber;
        public string VersionNumberFileURL;
        public string DownloadBasePathURL;
        public string LatestVersionNumber;
        public float DaysBetweenChecks;

        private string UpgradeFolderName = @"\Upgrade";
        private string[] DownloadFiles = new string[] { "TextSplit.exe", "TextSplit.exe.config" };

        public AutoUpdater(string versionNumber, string versionNumberFileURL, string downloadBasePathURL, float daysBetweenChecks) {
            VersionNumber = versionNumber;
            VersionNumberFileURL = versionNumberFileURL;
            DownloadBasePathURL = downloadBasePathURL;
            LatestVersionNumber = "";
            DaysBetweenChecks = daysBetweenChecks;
        }

        public bool DoUpdateProcedure() {
            bool check = false;
            if (Globals.AutoUpdater.IsTimeForUpdateCheck()) {
                check = Globals.AutoUpdater.CheckForUpdate();
                if (check) {
                    Properties.Settings.Default.TimeSinceLastCheck = DateTime.Now;
                    check = Globals.AutoUpdater.PerformUpdate();
                }
            }
            return check;
        }

        public bool IsTimeForUpdateCheck() {
            if ((DateTime.Now - Properties.Settings.Default.TimeSinceLastCheck).TotalDays >= DaysBetweenChecks) {
                Properties.Settings.Default.TimeSinceLastCheck = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool CheckForUpdate() {
            try {
                using (WebClient client = new WebClient()) {
                    LatestVersionNumber = client.DownloadString(VersionNumberFileURL);
                    return !LatestVersionNumber.Equals(VersionNumber);
                }
            } catch (Exception) {
                Globals.ShowErrorMessage("Failed to check for updates.");
                return false;
            }
        }

        public bool DeletePreviousUpdateFiles() {
            // Cleans up after the previous PerformUpdate call
            string currentProgramDirectory = Path.GetDirectoryName(@System.Reflection.Assembly.GetEntryAssembly().Location);
            string parentDirectory = Directory.GetParent(currentProgramDirectory).FullName;
            if (currentProgramDirectory.Substring(currentProgramDirectory.LastIndexOf(@"\")).Equals(UpgradeFolderName)) {
                // Deletes the old files in parent directory and places the new files in parent directory
                foreach (string file in DownloadFiles) {
                    // If it finds an IOException while deleting the files of the previous version, then that means the old program hasn't yet closed itself.
                    // The while loop will then continuously try to delete the previous installation files until it succeeds
                    bool retry = true;
                    while (retry) {
                        retry = false;
                        try {
                            File.Delete(parentDirectory + @"\" + file);
                        } catch (UnauthorizedAccessException) {
                            retry = true;
                        } 
                    }
                    File.Move(currentProgramDirectory + @"\" + file, parentDirectory + @"\" + file);
                }
                try {
                    Directory.Delete(currentProgramDirectory);
                } catch (Exception) {
                    DirectoryInfo di = new DirectoryInfo(currentProgramDirectory);
                    di.Attributes = di.Attributes & ~FileAttributes.Hidden;
                    Globals.ShowErrorMessage("Failed to remove upgrade folder.");
                }
                ImportSettingsFromTxtFile("SettingsExport.txt");
                return true;
            }
            return false;
        }

        public bool PerformUpdate() {
            DialogResult result = MessageBox.Show(string.Format("A newer version of TextSplit has been released.\n\nCurrent version:\t{0}\nLatest version:\t{1}\n\nWould you like to update now?", 
                Globals.VersionNumber, Globals.AutoUpdater.LatestVersionNumber), "TextSplit update", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                string currentProgramDirectory = Path.GetDirectoryName(@System.Reflection.Assembly.GetEntryAssembly().Location);
                string tempDirectory = currentProgramDirectory + UpgradeFolderName;
                bool retry = true;
                while (retry) {
                    retry = false;
                    if (Directory.Exists(tempDirectory)) {
                        DialogResult errorResult = MessageBox.Show("Failed to upgrade TextSplit. An upgrade folder (" + tempDirectory + ") already exists. Please remove the 'Upgrade' folder at the specified location and try again.", "Error", MessageBoxButtons.RetryCancel);
                        if (errorResult == DialogResult.Retry) {
                            retry = true;
                        } else {
                            Properties.Settings.Default.TimeSinceLastCheck = DateTime.MinValue;
                        }
                    } else {
                        DirectoryInfo di = Directory.CreateDirectory(tempDirectory);
                        di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                        try {
                            // Downloads all the necessary files to the temp folder
                            TextSplitUpdate updater = new TextSplitUpdate();
                            updater.pProgressBar.Maximum = updater.pProgressBar.Step * (DownloadFiles.Length);
                            updater.Show();
                            using (WebClient client = new WebClient()) {
                                for (int i = 0; i < DownloadFiles.Length; i++) {
                                    string downloadFullPath = DownloadBasePathURL + DownloadFiles[i];
                                    client.DownloadFile(downloadFullPath, tempDirectory + @"\" + DownloadFiles[i]);
                                    updater.pProgressBar.PerformStep();
                                    updater.Refresh();
                                }
                            }

                            // Exports the settings to a text file in the main folder
                            Properties.Settings.Default.Save();
                            ExportSettingsToTxtFile(tempDirectory + @"\SettingsExport.txt");

                            // Starts the newly downloaded version of TextSplit (asynchronously)
                            Process.Start(tempDirectory + @"\TextSplit.exe");

                            // Sets the setting IsJustUpdated to true and closes the application
                            Globals.ClearHotkeys();
                            Application.Exit();
                            return true;
                        } catch (Exception) {
                            Globals.ShowErrorMessage("Failed to upgrade TextSplit. The files could not be downloaded.");
                            Directory.Delete(tempDirectory);
                        }
                    }
                }
            }
            return false;
        }

        public void ExportSettingsToTxtFile(string filename) {
            using (StreamWriter sw = File.CreateText(filename)) {
                sw.WriteLine(Properties.Settings.Default.Continuous);
                sw.WriteLine(Properties.Settings.Default.DefaultDelimiter.Replace(Environment.NewLine, @"\n"));
                sw.WriteLine(Properties.Settings.Default.DefaultInfoText.Replace(Environment.NewLine, @"\n"));
                sw.WriteLine(Properties.Settings.Default.DisableHK);
                sw.WriteLine(Properties.Settings.Default.DisplayVerticalScrollBars);
                sw.WriteLine(string.Join("|", Properties.Settings.Default.FileNames.ToArray()));
                sw.WriteLine(string.Join("|", Properties.Settings.Default.Hotkeys.ToArray()));
                sw.WriteLine(Properties.Settings.Default.NavigateAll);
                sw.WriteLine(Properties.Settings.Default.NavigationWindowAlwaysOnTop);
                sw.WriteLine(Properties.Settings.Default.ReadOnly);
                sw.WriteLine(Properties.Settings.Default.TimeSinceLastCheck.ToString("O"));
                sw.WriteLine(string.Join("|", Properties.Settings.Default.UserThemes.ToArray()));
            }
        }

        public void ImportSettingsFromTxtFile(string filename) {
            try {
                string[] lines = File.ReadAllLines(filename);
                try {
                    File.Delete(filename);
                } catch (Exception) { }

                Properties.Settings.Default.Continuous = Convert.ToBoolean(lines[0]);
                Properties.Settings.Default.DefaultDelimiter = lines[1].Replace(@"\n", Environment.NewLine);
                Properties.Settings.Default.DefaultInfoText = lines[2].Replace(@"\n", Environment.NewLine);
                Properties.Settings.Default.DisableHK = Convert.ToBoolean(lines[3]);
                Properties.Settings.Default.DisplayVerticalScrollBars = Convert.ToInt32(lines[4]);
                Properties.Settings.Default.FileNames = new ArrayList();
                if (!lines[5].Equals(""))
                    Properties.Settings.Default.FileNames.AddRange(lines[5].Split(new char[] { '|' }));
                Properties.Settings.Default.Hotkeys = new ArrayList();
                if (!lines[6].Equals(""))
                    Properties.Settings.Default.Hotkeys.AddRange(lines[6].Split(new char[] { '|' }).Select(o => Convert.ToInt32(o)).ToArray());
                Properties.Settings.Default.NavigateAll = Convert.ToBoolean(lines[7]);
                Properties.Settings.Default.NavigationWindowAlwaysOnTop = Convert.ToBoolean(lines[8]);
                Properties.Settings.Default.ReadOnly = Convert.ToBoolean(lines[9]);
                Properties.Settings.Default.TimeSinceLastCheck = DateTime.ParseExact(lines[10], "O", System.Globalization.CultureInfo.InvariantCulture);
                Properties.Settings.Default.UserThemes = new ArrayList();
                if (!lines[11].Equals(""))
                    Properties.Settings.Default.UserThemes.AddRange(lines[11].Split(new char[] { '|' }));
            } catch (Exception) {
                Globals.ShowErrorMessage("Failed to import settings from previous version");
            }
        }
    }
}
