using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TextSplit
{
    // The class that contains the global settings and functions that are reachable from all classes
    public static class Globals
    {
        public static TextSplitMain TSM { get; set; }
        private static TextSplitShow _currentWindow;
        public static TextSplitShow CurrentWindow { 
            get {
                return _currentWindow;
            }
            set {
                if (value != null) {
                    _currentWindow = value;
                    TSM.lCurrWindow.Text = Path.GetFileNameWithoutExtension(_currentWindow.fileName);
                }
            } 
        }
        public static ArrayList WindowList { get; set; }

        public static Dictionary<string, Theme> Themes;

        public static Dictionary<string, Theme> UserThemes;

        private static KeyboardHook Hook;

        static Globals() {
            // Resets default settigns if in debug mode
            #if DEBUG
                Properties.Settings.Default.Reset();
            #endif

            TSM = null;
            WindowList = new ArrayList();
            CurrentWindow = null;

            if (Properties.Settings.Default.FileNames == null) {
                Properties.Settings.Default.FileNames = new ArrayList();
            }
            
            // v1.7 Changes:
            // - Numpad6 and Numpad4 are default for next and prev slide, respectively
            // - Added 7 preset themes and users can save their layouts in custom themes
            // - Ctrl+S and Ctrl+Shift+S to save progress now works from within text editors and not just from the main window
            if (Properties.Settings.Default.UserThemes == null) {
                Properties.Settings.Default.UserThemes = new ArrayList();
            }

            ShowPropertiesUserThemes();

            InitializeHotkeys();
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
            foreach (TextSplitShow TSS in WindowList) {
                for (int i = 0; i < TSS.TST.Hotkeys.Length; i++) {
                    if (TSS.TST.Hotkeys[i] != Keys.None) {
                        Hook.RegisterHotKey(TSS.TST.Hotkeys[i]);
                    }
                }
            }
        }

        public static void ClearHotkeys() {
            Hook.Dispose();
        }

        public static void OpenNewWindow(TextSplitShow TSS) {
            UpdateSlideInfo();
            TSS.Show();
            TSS.DisplaySlide();
            TSS.FileChangeActions();
            TSS.Focus();
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
            TSM.lSlideCount.Text = "of " + CurrentWindow.TST.TextList.Count.ToString();
            TSM.tGoToSlide.Text = (CurrentWindow.currentSlide + 1).ToString();
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
                if (e.Key == TSS.TST.Hotkeys[0] || e.Key == TSS.TST.Hotkeys[1]) {
                    TSS.GoToNext();
                }
                if (e.Key == TSS.TST.Hotkeys[2] || e.Key == TSS.TST.Hotkeys[3]) {
                    TSS.GoToPrev();
                }
                if (e.Key == TSS.TST.Hotkeys[4] || e.Key == TSS.TST.Hotkeys[5]) {
                    TSS.GoToFirst();
                }
                if (e.Key == TSS.TST.Hotkeys[6] || e.Key == TSS.TST.Hotkeys[7]) {
                    TSS.GoToLast();
                }
            }
        }
    }
}
