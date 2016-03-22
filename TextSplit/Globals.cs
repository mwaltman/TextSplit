<<<<<<< HEAD
﻿using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace TextSplit
{
    // The class that contains the global settings and functions that are reachable from all classes
    public static class Globals
    {
        public static string VersionNumber { get; set; }
        public static TextSplitMain TSM { get; set; }
        public static TextSplitShow CurrentWindow { get; set; }
        public static ArrayList WindowList { get; set; }
        public static Dictionary<string, Theme> Themes;
        public static Dictionary<string, Theme> UserThemes;
        public static Serializer Serializer;
        public static AutoUpdater AutoUpdater;
        public static LogFile LogFile;
        public static ToolTip ToolTip;
        private static KeyboardHook Hook;

        public static void StartGlobals() {
            // Resets default settings if in debug mode
            #if DEBUG
                //Properties.Settings.Default.Reset();
            #endif

            // Manually set this for each new version that comes out
            VersionNumber = "2.2";

            // First time setup
            Properties.Settings.Default.Reload();
            if (Properties.Settings.Default.FileNames == null) {
                Properties.Settings.Default.FileNames = new ArrayList();
            }
            if (Properties.Settings.Default.UserThemes == null) {
                Properties.Settings.Default.UserThemes = new ArrayList();
            }
            if (Properties.Settings.Default.Hotkeys == null) {
                Properties.Settings.Default.Hotkeys = new ArrayList { 102, 0, 100, 0, 0, 0, 0, 0 };
            }
            //Properties.Settings.Default.TimeSinceLastCheck = DateTime.MinValue; // To enable constant update checks
            if (Properties.Settings.Default.TimeSinceLastCheck == null) {
                Properties.Settings.Default.TimeSinceLastCheck = DateTime.MinValue;
            }

            TSM = null;
            WindowList = new ArrayList();
            CurrentWindow = null;
            Serializer = new Serializer();
            string versionNumberFileURL = "https://raw.github.com/mwaltman/TextSplit/master/Misc/VersionNumber.txt";
            string downloadBasePathURL = "https://raw.github.com/mwaltman/TextSplit/master/TextSplit/bin/Release/";
            AutoUpdater = new AutoUpdater(VersionNumber, versionNumberFileURL, downloadBasePathURL, 7);
            ToolTip = new ToolTip();
            ToolTip.ShowAlways = true;
            ToolTip.AutoPopDelay = 10000;
            LogFile = new LogFile("logfile.txt", 100);

            InitializeHotkeys();

            // To test exporting and importing of settings
            //string filename = "ExportSetting.txt";
            //AutoUpdater.ExportSettingsToTxtFile(filename);
            //AutoUpdater.ImportSettingsFromTxtFile(filename);
        }

        public static void ShowPropertiesFileNames() {
            #if DEBUG
                Console.Write("FILENAMES: ");
                foreach (string str in Properties.Settings.Default.FileNames) {
                    Console.Write("'" + str + "' ");
                }
                Console.WriteLine();
            #endif
        }

        public static void ShowPropertiesUserThemes() {
            #if DEBUG
                Console.Write("USERTHEMES: ");
                foreach (string str in Properties.Settings.Default.UserThemes) {
                    Console.Write("'" + str + "' ");
                }
                Console.WriteLine();
            #endif
        }

        public static void InitializeHotkeys() {
            Hook = new KeyboardHook();
            Hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            for (int i = 0; i < Properties.Settings.Default.Hotkeys.Count; i++) {
                if ((Keys)Properties.Settings.Default.Hotkeys[i] != Keys.None) {
                    Hook.RegisterHotKey((Keys)Properties.Settings.Default.Hotkeys[i]);
                }
            }
        }

        public static void ClearHotkeys() {
            Hook.Dispose();
        }

        public static void OpenNewWindow(TextSplitShow TSS) {
            if (WindowList.Count <= 1000) {
                UpdateSlideInfo();
                if (Properties.Settings.Default.DisplayVerticalScrollBars == 0)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                if (Properties.Settings.Default.DisplayVerticalScrollBars == 1)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
                if (Properties.Settings.Default.DisplayVerticalScrollBars == 2)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                TSS.Show();
                TSS.DisplaySlide();
                TSS.FileChangeActions();
                TSS.Focus();
            } else {
                ShowErrorMessage("Cannot open a new window. Maximum number of windows (1000) has been reached.");
            }
        }

        public static bool CloseWindow(TextSplitShow TSS, bool save) {
            int index = WindowList.IndexOf(TSS);
            WindowList.RemoveAt(index);
            if (!save) {
                Properties.Settings.Default.FileNames.RemoveAt(index);
            }
            if (WindowList.Count > 0) {
                ((TextSplitShow)WindowList[0]).Focus();
                ShowPropertiesFileNames();
                return true;
            } else {
                return false;
            }
        }

        public static void UpdateSlideInfo() {
            UpdateNavigationControls();
            UpdateSyncControls();
        }

        public static void UpdateNavigationControls() {
            TSM.lCurrWindow.Text = Path.GetFileNameWithoutExtension(CurrentWindow.fileName);
            TSM.lSlideCount.Text = "of " + CurrentWindow.TST.TextList.Count.ToString();
            TSM.tGoToSlide.Text = (CurrentWindow.currentSlide + 1).ToString();
        }

        public static void UpdateSyncControls() {
            TSM.bSyncTst.Enabled = CurrentWindow.TST.SyncTxtPath != null;
            TSM.bSyncTxt.Enabled = TSM.bSyncTst.Enabled;
            UpdateUpdatedIcon();
        }

        public static void UpdateUpdatedIcon() {
            // Sets the Updated icon based on whether the contents of the two files are identical
            TSM.bUpdated.Enabled = TSM.bSyncTst.Enabled;
            if (TSM.bSyncTst.Enabled) {
                string tstText = GetExportText(CurrentWindow.TST.TextList, CurrentWindow.TST.SyncDelimiterText, null, false, true);
                string txtText = File.ReadAllText(CurrentWindow.TST.SyncTxtPath);
                UpdateUpdatedIcon(tstText.Equals(txtText));
            }
        }

        public static void UpdateUpdatedIcon(bool check) {
            if (check) {
                TSM.bUpdated.Image = Properties.Resources.CheckMarkIcon;
            } else {
                TSM.bUpdated.Image = Properties.Resources.RedCrossIcon;
            }
        }

        public static void ChangeReadOnly() {
            TSM.cReadOnly.Checked = Properties.Settings.Default.ReadOnly;
        }

        public static void ChangeDisableHK() {
            TSM.cDisableHK.Checked = Properties.Settings.Default.DisableHK;
        }

        public static void AddBefore() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Insert(CurrentWindow.currentSlide, "");
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddAfter() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Insert(CurrentWindow.currentSlide + 1, "");
                CurrentWindow.currentSlide += 1;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddBegin() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Insert(0, "");
                CurrentWindow.currentSlide = 0;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddEnd() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Add("");
                CurrentWindow.currentSlide = CurrentWindow.TST.TextList.Count - 1;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void Remove() {
            if (CurrentWindow.TST.TextList.Count > 1) {
                CurrentWindow.TST.TextList.RemoveAt(CurrentWindow.currentSlide);
                CurrentWindow.currentSlide = CurrentWindow.TST.TextList.Count - 1;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void ImportFromTxt(string txtPath, string tstPath, string delimiterText, bool saveAsNewTST) {
            if (!Directory.Exists(Path.GetDirectoryName(txtPath))) { // Additional safeguard
                ShowErrorMessage("Cannot import txt file. The given import directory does not exist.");
            } else if (saveAsNewTST) { // Additional safeguard
                if (!Directory.Exists(Path.GetDirectoryName(tstPath)))
                    ShowErrorMessage("Cannot import txt file. The given export directory does not exist.");
            } else {
                try {
                    // Creates a new TST object from the txt file. Uses the current layout if saveAsNewTST = false
                    TextSplitText newTST;
                    if (saveAsNewTST) {
                        newTST = new TextSplitText();
                        newTST.SetEmpty();
                    } else {
                        newTST = CurrentWindow.TST;
                    }
                    SetTextSlides(newTST, delimiterText, txtPath);

                    // Saves the tst file and opens a new display window with that file loaded (if saveAsNewTST = true)
                    if (saveAsNewTST) {
                        Serializer.SerializeObject(tstPath, newTST);
                        TextSplitShow newTSS = new TextSplitShow(tstPath, true);
                        OpenNewWindow(newTSS);
                    } else {
                        CurrentWindow.FileChangeActions();
                        CurrentWindow.ChangeFilenameUnsaved();
                        CurrentWindow.fileLoaded = false;
                    }
                } catch (UnauthorizedAccessException) {
                    ShowErrorMessage("Cannot import txt file. You do not have permission to read from a txt file at the given import directory.");
                } catch (Exception) {
                    ShowErrorMessage("Cannot import txt file. An error has occurred.");
                }
            }
        }

        public static void ExportToTxt(string txtPath, string delimiterText, string infoText, bool displaySlideInfo, bool displayAboveSlide) {
            if (!Directory.Exists(Path.GetDirectoryName(txtPath))) { // Additional safeguard
                ShowErrorMessage("Cannot save as txt file. The given export directory does not exist.");
            } else {
                try {
                    using (StreamWriter sw = File.CreateText(txtPath)) {
                        // Writes each string from the list of texts from the current display window to a txt file
                        sw.NewLine = Environment.NewLine;
                        ArrayList textList = CurrentWindow.TST.TextList;
                        sw.Write(GetExportText(textList, delimiterText, infoText, displaySlideInfo, displayAboveSlide));
                    }
                } catch (UnauthorizedAccessException) {
                    ShowErrorMessage("Cannot save as txt file. You do not have permission to write to a txt file at the given export directory.");
                } catch (Exception) {
                    ShowErrorMessage("Cannot save as txt file. An error has occurred.");
                }
            }
        }

        public static void SetTextSlides(TextSplitText tst, string delimiterText, string txtPath) {
            tst.TextList.Clear();
            string slideText = File.ReadAllText(txtPath).Replace(Environment.NewLine, "\n");
            string currentSlideText;
            delimiterText = "\n" + delimiterText.Replace(Environment.NewLine, "\n") + "\n";
            while (slideText.Contains(delimiterText)) {
                currentSlideText = slideText.Remove(slideText.IndexOf(delimiterText));
                slideText = slideText.Substring(slideText.IndexOf(delimiterText) + delimiterText.Length);
                tst.TextList.Add(currentSlideText); // Adds current slideText to the TST's textList
            }
            tst.TextList.Add(slideText); // Adds current slideText to the TST's textList
        }


        public static string GetExportText(ArrayList textList, string delimiterText, string infoText, bool displaySlideInfo, bool displayAboveSlide) {
            string result = "";
            int count = 0;
            foreach (string s in textList) {
                count++;
                if (displaySlideInfo && displayAboveSlide) {
                    result += WriteInfoTextToStream(infoText, count, textList.Count) + Environment.NewLine; // Information text above slide
                }
                result += s.Replace("\n", Environment.NewLine) + Environment.NewLine; // Slide text
                if (displaySlideInfo && !displayAboveSlide) {
                    result += WriteInfoTextToStream(infoText, count, textList.Count) + Environment.NewLine; // Information text below slide
                }
                if (textList.LastIndexOf(s) != textList.Count - 1) {
                    result += delimiterText + Environment.NewLine; // Delimiter text
                }
            }
            result = result.Remove(result.LastIndexOf(Environment.NewLine)); // Removes the last Environment.NewLine, which is obsolete
            return result;
        }

        private static string WriteInfoTextToStream(string infoText, int currentSlides, int totalSlides) {
            return infoText.Replace("\n", Environment.NewLine)
                .Replace("$C$", currentSlides.ToString())
                .Replace("$T$", totalSlides.ToString());
        }

        public static void SyncTxt() {
            // Syncing txt = clear txt file, then perform export to txt
            // If the file is already opened, then the changes will only take into effect after the txt file is restarted
            string syncText = GetExportText(CurrentWindow.TST.TextList, CurrentWindow.TST.SyncDelimiterText, null, false, true);
            string txtText = File.ReadAllText(CurrentWindow.TST.SyncTxtPath);
            if (!syncText.Equals(txtText)) {
                DialogResult result = DialogResult.Yes;
                if (!CurrentWindow.TST.SyncDisableTxtDialog)
                    result = MessageBox.Show("The contents of the txt file '" + CurrentWindow.TST.SyncTxtPath + "' are different from the contents of the tst file. Do you want to overwrite the txt file?", "Overwrite txt file?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    try {
                        File.WriteAllText(CurrentWindow.TST.SyncTxtPath, syncText);
                        UpdateUpdatedIcon(true);
                    } catch (Exception) {
                        ShowErrorMessage("Cannot sync txt file. An error has occurred.");
                    }
                }
            }
        }

        public static void SyncTst() {
            // Syncing tst = import textlist from txt into tst, then serialize tst, then display new tst on current window
            try {
                SetTextSlides(CurrentWindow.TST, CurrentWindow.TST.SyncDelimiterText, CurrentWindow.TST.SyncTxtPath);
                // Updating the slides can cause the new tst file to have less slides. If the current slide index is invalid, then it sets the index to 0
                if (CurrentWindow.currentSlide >= CurrentWindow.TST.TextList.Count) {
                    CurrentWindow.currentSlide = 0;
                }
                CurrentWindow.DisplaySlide();
                UpdateNavigationControls();
                UpdateUpdatedIcon(true);
            } catch (UnauthorizedAccessException) {
                ShowErrorMessage("Cannot sync tst file. You do not have permission to read from a txt file at the given txt directory.");
            } catch (Exception) {
                ShowErrorMessage("Cannot sync tst file. An error has occurred.");
            }
        }

        public static void OpenURL(string url) {
            Process.Start(new ProcessStartInfo(url));
        }

        public static void ShowErrorMessage(string message) {
            MessageBox.Show(message, "Error");
        }

        public static void ShowErrorMessage(string message, Exception e) {
            ShowErrorMessage(string.Format(message + "\n\nSource:\t{0}\nMessage:\t{1}\nStackTrace:\n{2}", e.Source, e.Message, e.StackTrace));
        }

        public static void WriteToLogFile(string message) {
            LogFile.AddEntry(message);
        }

        public static void WriteToLogFile(Exception e) {
            WriteToLogFile(string.Format("Error: {0}", e.Message));
        }

        public static DialogResult ShowInputDialog(string title, ref string input) {
            int pad = 12;
            int minipad = 8;
            int buttonHeight = 23;
            Size size = new Size(220, 2*pad + minipad + 2* buttonHeight);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedSingle;
            inputBox.ClientSize = size;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ControlBox = true;
            inputBox.Icon = TSM.Icon;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            TextBox textBox = new TextBox();
            textBox.Size = new Size(size.Width - 2*pad, buttonHeight);
            textBox.Location = new Point(pad, pad);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, buttonHeight);
            okButton.Text = "OK";
            okButton.Location = new Point(size.Width - 2*okButton.Size.Width - pad - minipad, size.Height - pad - buttonHeight);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, buttonHeight);
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(size.Width - cancelButton.Size.Width - pad, size.Height - pad - buttonHeight);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        /*
         * Events
         */

        private static void hook_KeyPressed(object sender, KeyPressedEventArgs e) {
            foreach (TextSplitShow TSS in WindowList) {
                if (Properties.Settings.Default.NavigateAll || (!Properties.Settings.Default.NavigateAll && TSS.Equals(CurrentWindow))) {
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[0] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[1]) {
                        TSS.GoToNext();
                    }
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[2] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[3]) {
                        TSS.GoToPrev();
                    }
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[4] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[5]) {
                        TSS.GoToFirst();
                    }
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[6] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[7]) {
                        TSS.GoToLast();
                    }
                }
            }
        }
    }
}
=======
﻿using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace TextSplit
{
    // The class that contains the global settings and functions that are reachable from all classes
    public static class Globals
    {
        public static string VersionNumber { get; set; }
        public static TextSplitMain TSM { get; set; }
        public static TextSplitShow CurrentWindow { get; set; }
        public static ArrayList WindowList { get; set; }
        public static Dictionary<string, Theme> Themes;
        public static Dictionary<string, Theme> UserThemes;
        public static Serializer Serializer;
        public static AutoUpdater AutoUpdater;
        public static ToolTip ToolTip;
        private static KeyboardHook Hook;

        public static void StartGlobals() {
            // Resets default settings if in debug mode
            #if DEBUG
                //Properties.Settings.Default.Reset();
            #endif

            // Manually set this for each new version that comes out
            VersionNumber = "2.1";

            TSM = null;
            WindowList = new ArrayList();
            CurrentWindow = null;
            Serializer = new Serializer();
            string versionNumberFileURL = "https://raw.github.com/mwaltman/TextSplit/master/Misc/VersionNumber.txt";
            string downloadBasePathURL = "https://raw.github.com/mwaltman/TextSplit/master/TextSplit/bin/Release/";
            AutoUpdater = new AutoUpdater(VersionNumber, versionNumberFileURL, downloadBasePathURL, 5/(24F*60F));
            ToolTip = new ToolTip();
            ToolTip.ShowAlways = true;
            ToolTip.AutoPopDelay = 10000;

            Properties.Settings.Default.Reload();

            // First time setup
            if (Properties.Settings.Default.FileNames == null) {
                Properties.Settings.Default.FileNames = new ArrayList();
            }
            if (Properties.Settings.Default.UserThemes == null) {
                Properties.Settings.Default.UserThemes = new ArrayList();
            }
            if (Properties.Settings.Default.Hotkeys == null) {
                Properties.Settings.Default.Hotkeys = new ArrayList{ 102, 0, 100, 0, 0, 0, 0, 0 };
            }
            //Properties.Settings.Default.TimeSinceLastCheck = DateTime.MinValue; // To enable constant update checks
            if (Properties.Settings.Default.TimeSinceLastCheck == null) {
                Properties.Settings.Default.TimeSinceLastCheck = DateTime.MinValue;
            }

            InitializeHotkeys();

            // To test exporting and importing of settings
            //string filename = "ExportSetting.txt";
            //AutoUpdater.ExportSettingsToTxtFile(filename);
            //AutoUpdater.ImportSettingsFromTxtFile(filename);
        }

        public static void ShowPropertiesFileNames() {
            #if DEBUG
                Console.Write("FILENAMES: ");
                foreach (string str in Properties.Settings.Default.FileNames) {
                    Console.Write("'" + str + "' ");
                }
                Console.WriteLine();
            #endif
        }

        public static void ShowPropertiesUserThemes() {
            #if DEBUG
                Console.Write("USERTHEMES: ");
                foreach (string str in Properties.Settings.Default.UserThemes) {
                    Console.Write("'" + str + "' ");
                }
                Console.WriteLine();
            #endif
        }

        public static void InitializeHotkeys() {
            Hook = new KeyboardHook();
            Hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            for (int i = 0; i < Properties.Settings.Default.Hotkeys.Count; i++) {
                if ((Keys)Properties.Settings.Default.Hotkeys[i] != Keys.None) {
                    Hook.RegisterHotKey((Keys)Properties.Settings.Default.Hotkeys[i]);
                }
            }
        }

        public static void ClearHotkeys() {
            Hook.Dispose();
        }

        public static void OpenNewWindow(TextSplitShow TSS) {
            if (WindowList.Count <= 1000) {
                UpdateSlideInfo();
                if (Properties.Settings.Default.DisplayVerticalScrollBars == 0)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                if (Properties.Settings.Default.DisplayVerticalScrollBars == 1)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
                if (Properties.Settings.Default.DisplayVerticalScrollBars == 2)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                TSS.Show();
                TSS.DisplaySlide();
                TSS.FileChangeActions();
                TSS.Focus();
            } else {
                ShowErrorMessage("Cannot open a new window. Maximum number of windows (1000) has been reached.");
            }
        }

        public static bool CloseWindow(TextSplitShow TSS, bool save) {
            int index = WindowList.IndexOf(TSS);
            WindowList.RemoveAt(index);
            if (!save) {
                Properties.Settings.Default.FileNames.RemoveAt(index);
            }
            if (WindowList.Count > 0) {
                ((TextSplitShow)WindowList[0]).Focus();
                ShowPropertiesFileNames();
                return true;
            } else {
                return false;
            }
        }

        public static void UpdateSlideInfo() {
            UpdateNavigationControls();
            UpdateSyncControls();
        }

        public static void UpdateNavigationControls() {
            TSM.lCurrWindow.Text = Path.GetFileNameWithoutExtension(CurrentWindow.fileName);
            TSM.lSlideCount.Text = "of " + CurrentWindow.TST.TextList.Count.ToString();
            TSM.tGoToSlide.Text = (CurrentWindow.currentSlide + 1).ToString();
        }

        public static void UpdateSyncControls() {
            TSM.bSyncTst.Enabled = CurrentWindow.TST.SyncTxtPath != null;
            TSM.bSyncTxt.Enabled = TSM.bSyncTst.Enabled;
            UpdateUpdatedIcon();
        }

        public static void UpdateUpdatedIcon() {
            // Sets the Updated icon based on whether the contents of the two files are identical
            TSM.bUpdated.Enabled = TSM.bSyncTst.Enabled;
            if (TSM.bSyncTst.Enabled) {
                string tstText = GetExportText(CurrentWindow.TST.TextList, CurrentWindow.TST.SyncDelimiterText, null, false, true);
                string txtText = File.ReadAllText(CurrentWindow.TST.SyncTxtPath);
                UpdateUpdatedIcon(tstText.Equals(txtText));
            }
        }

        public static void UpdateUpdatedIcon(bool check) {
            if (check) {
                TSM.bUpdated.Image = Properties.Resources.CheckMarkIcon;
            } else {
                TSM.bUpdated.Image = Properties.Resources.RedCrossIcon;
            }
        }

        public static void ChangeReadOnly() {
            TSM.cReadOnly.Checked = Properties.Settings.Default.ReadOnly;
        }

        public static void ChangeDisableHK() {
            TSM.cDisableHK.Checked = Properties.Settings.Default.DisableHK;
        }

        public static void AddBefore() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Insert(CurrentWindow.currentSlide, "");
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddAfter() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Insert(CurrentWindow.currentSlide + 1, "");
                CurrentWindow.currentSlide += 1;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddBegin() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Insert(0, "");
                CurrentWindow.currentSlide = 0;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddEnd() {
            if (CurrentWindow.TST.TextList.Count <= 999) {
                CurrentWindow.TST.TextList.Add("");
                CurrentWindow.currentSlide = CurrentWindow.TST.TextList.Count - 1;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void Remove() {
            if (CurrentWindow.TST.TextList.Count > 1) {
                CurrentWindow.TST.TextList.RemoveAt(CurrentWindow.currentSlide);
                CurrentWindow.currentSlide = CurrentWindow.TST.TextList.Count - 1;
                CurrentWindow.DisplaySlide();
                CurrentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void ImportFromTxt(string txtPath, string tstPath, string delimiterText, bool saveAsNewTST) {
            if (!Directory.Exists(Path.GetDirectoryName(txtPath))) { // Additional safeguard
                ShowErrorMessage("Cannot import txt file. The given import directory does not exist.");
            } else if (saveAsNewTST) { // Additional safeguard
                if (!Directory.Exists(Path.GetDirectoryName(tstPath)))
                    ShowErrorMessage("Cannot import txt file. The given export directory does not exist.");
            } else {
                try {
                    // Creates a new TST object from the txt file. Uses the current layout if saveAsNewTST = false
                    TextSplitText newTST;
                    if (saveAsNewTST) {
                        newTST = new TextSplitText();
                        newTST.SetEmpty();
                    } else {
                        newTST = CurrentWindow.TST;
                    }
                    SetTextSlides(newTST, delimiterText, txtPath);

                    // Saves the tst file and opens a new display window with that file loaded (if saveAsNewTST = true)
                    if (saveAsNewTST) {
                        Serializer.SerializeObject(tstPath, newTST);
                        TextSplitShow newTSS = new TextSplitShow(tstPath, true);
                        OpenNewWindow(newTSS);
                    } else {
                        CurrentWindow.FileChangeActions();
                        CurrentWindow.ChangeFilenameUnsaved();
                        CurrentWindow.fileLoaded = false;
                    }
                } catch (UnauthorizedAccessException) {
                    ShowErrorMessage("Cannot import txt file. You do not have permission to read from a txt file at the given import directory.");
                } catch (Exception) {
                    ShowErrorMessage("Cannot import txt file. An error has occurred.");
                }
            }
        }

        public static void ExportToTxt(string txtPath, string delimiterText, string infoText, bool displaySlideInfo, bool displayAboveSlide) {
            if (!Directory.Exists(Path.GetDirectoryName(txtPath))) { // Additional safeguard
                ShowErrorMessage("Cannot save as txt file. The given export directory does not exist.");
            } else {
                try {
                    using (StreamWriter sw = File.CreateText(txtPath)) {
                        // Writes each string from the list of texts from the current display window to a txt file
                        sw.NewLine = Environment.NewLine;
                        ArrayList textList = CurrentWindow.TST.TextList;
                        sw.Write(GetExportText(textList, delimiterText, infoText, displaySlideInfo, displayAboveSlide));
                    }
                } catch (UnauthorizedAccessException) {
                    ShowErrorMessage("Cannot save as txt file. You do not have permission to write to a txt file at the given export directory.");
                } catch (Exception) {
                    ShowErrorMessage("Cannot save as txt file. An error has occurred.");
                }
            }
        }

        public static void SetTextSlides(TextSplitText tst, string delimiterText, string txtPath) {
            tst.TextList.Clear();
            string slideText = File.ReadAllText(txtPath).Replace(Environment.NewLine, "\n");
            string currentSlideText;
            delimiterText = "\n" + delimiterText.Replace(Environment.NewLine, "\n") + "\n";
            while (slideText.Contains(delimiterText)) {
                currentSlideText = slideText.Remove(slideText.IndexOf(delimiterText));
                slideText = slideText.Substring(slideText.IndexOf(delimiterText) + delimiterText.Length);
                tst.TextList.Add(currentSlideText); // Adds current slideText to the TST's textList
            }
            tst.TextList.Add(slideText); // Adds current slideText to the TST's textList
        }


        public static string GetExportText(ArrayList textList, string delimiterText, string infoText, bool displaySlideInfo, bool displayAboveSlide) {
            string result = "";
            int count = 0;
            foreach (string s in textList) {
                count++;
                if (displaySlideInfo && displayAboveSlide) {
                    result += WriteInfoTextToStream(infoText, count, textList.Count) + Environment.NewLine; // Information text above slide
                }
                result += s.Replace("\n", Environment.NewLine) + Environment.NewLine; // Slide text
                if (displaySlideInfo && !displayAboveSlide) {
                    result += WriteInfoTextToStream(infoText, count, textList.Count) + Environment.NewLine; // Information text below slide
                }
                if (textList.LastIndexOf(s) != textList.Count - 1) {
                    result += delimiterText + Environment.NewLine; // Delimiter text
                }
            }
            result = result.Remove(result.LastIndexOf(Environment.NewLine)); // Removes the last Environment.NewLine, which is obsolete
            return result;
        }

        private static string WriteInfoTextToStream(string infoText, int currentSlides, int totalSlides) {
            return infoText.Replace("\n", Environment.NewLine)
                .Replace("$C$", currentSlides.ToString())
                .Replace("$T$", totalSlides.ToString());
        }

        public static void SyncTxt() {
            // Syncing txt = clear txt file, then perform export to txt
            // If the file is already opened, then the changes will only take into effect after the txt file is restarted
            string syncText = GetExportText(CurrentWindow.TST.TextList, CurrentWindow.TST.SyncDelimiterText, null, false, true);
            string txtText = File.ReadAllText(CurrentWindow.TST.SyncTxtPath);
            if (!syncText.Equals(txtText)) {
                DialogResult result = DialogResult.Yes;
                if (!CurrentWindow.TST.SyncDisableTxtDialog)
                    result = MessageBox.Show("The contents of the txt file '" + CurrentWindow.TST.SyncTxtPath + "' are different from the contents of the tst file. Do you want to overwrite the txt file?", "Overwrite txt file?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    try {
                        File.WriteAllText(CurrentWindow.TST.SyncTxtPath, syncText);
                        UpdateUpdatedIcon(true);
                    } catch (Exception) {
                        ShowErrorMessage("Cannot sync txt file. An error has occurred.");
                    }
                }
            }
        }

        public static void SyncTst() {
            // Syncing tst = import textlist from txt into tst, then serialize tst, then display new tst on current window
            try {
                SetTextSlides(CurrentWindow.TST, CurrentWindow.TST.SyncDelimiterText, CurrentWindow.TST.SyncTxtPath);
                // Updating the slides can cause the new tst file to have less slides. If the current slide index is invalid, then it sets the index to 0
                if (CurrentWindow.currentSlide >= CurrentWindow.TST.TextList.Count) {
                    CurrentWindow.currentSlide = 0;
                }
                CurrentWindow.DisplaySlide();
                UpdateNavigationControls();
                UpdateUpdatedIcon(true);
            } catch (UnauthorizedAccessException) {
                ShowErrorMessage("Cannot sync tst file. You do not have permission to read from a txt file at the given txt directory.");
            } catch (Exception) {
                ShowErrorMessage("Cannot sync tst file. An error has occurred.");
            }
        }

        public static void OpenURL(string url) {
            Process.Start(new ProcessStartInfo(url));
        }

        public static void ShowErrorMessage(string message) {
            MessageBox.Show(message, "Error");
        }

        public static void ShowErrorMessage(string message, Exception e) {
            ShowErrorMessage(string.Format(message + "\n\nSource:\t{0}\nMessage:\t{1}\nStackTrace:\n{2}", e.Source, e.Message, e.StackTrace));
        }

        public static DialogResult ShowInputDialog(string title, ref string input) {
            int pad = 12;
            int minipad = 8;
            int buttonHeight = 23;
            Size size = new Size(220, 2*pad + minipad + 2* buttonHeight);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedSingle;
            inputBox.ClientSize = size;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ControlBox = true;
            inputBox.Icon = TSM.Icon;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            TextBox textBox = new TextBox();
            textBox.Size = new Size(size.Width - 2*pad, buttonHeight);
            textBox.Location = new Point(pad, pad);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, buttonHeight);
            okButton.Text = "OK";
            okButton.Location = new Point(size.Width - 2*okButton.Size.Width - pad - minipad, size.Height - pad - buttonHeight);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, buttonHeight);
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(size.Width - cancelButton.Size.Width - pad, size.Height - pad - buttonHeight);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        /*
         * Events
         */

        private static void hook_KeyPressed(object sender, KeyPressedEventArgs e) {
            foreach (TextSplitShow TSS in WindowList) {
                if (Properties.Settings.Default.NavigateAll || (!Properties.Settings.Default.NavigateAll && TSS.Equals(CurrentWindow))) {
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[0] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[1]) {
                        TSS.GoToNext();
                    }
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[2] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[3]) {
                        TSS.GoToPrev();
                    }
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[4] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[5]) {
                        TSS.GoToFirst();
                    }
                    if (e.Key == (Keys)Properties.Settings.Default.Hotkeys[6] || e.Key == (Keys)Properties.Settings.Default.Hotkeys[7]) {
                        TSS.GoToLast();
                    }
                }
            }
        }
    }
}
>>>>>>> origin/master
