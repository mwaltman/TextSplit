using System.Windows.Forms;
using System;
using System.Collections;
using System.IO;

namespace TextSplit
{
    // The class that contains the global settings and functions that are reachable from all classes
    public static class Globals
    {
        public static TextSplitMain TSM { get; set; }
        private static TextSplitShow _currentWindow;
        public static TextSplitShow currentWindow { 
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
        public static ArrayList windowList { get; set; }

        private static KeyboardHook hook;

        static Globals() {
            // Resets default settigns if in debug mode
            #if DEBUG
                //Properties.Settings.Default.Reset();
            #endif

            TSM = null;
            windowList = new ArrayList();
            currentWindow = null;

            if (Properties.Settings.Default.FileName == null) {
                Properties.Settings.Default.FileName = new ArrayList();
            }

            InitializeHotkeys();
        }

        public static void ShowPropertiesFileName() {
            //Console.Write("FILENAME: ");
            //foreach (string str in Properties.Settings.Default.FileName) {
            //    Console.Write("'" + str + "' ");
            //}
            //Console.WriteLine();
        }

        public static void InitializeHotkeys() {
            hook = new KeyboardHook();
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            foreach (TextSplitShow TSS in windowList) {
                for (int i = 0; i < TSS.TST.Hotkeys.Length; i++) {
                    if (TSS.TST.Hotkeys[i] != Keys.None) {
                        hook.RegisterHotKey(TSS.TST.Hotkeys[i]);
                    }
                }
            }
        }

        public static void ClearHotkeys() {
            hook.Dispose();
        }

        public static void OpenNewWindow(TextSplitShow TSS) {
            Globals.UpdateSlideInfo();
            TSS.Show();
            TSS.DisplaySlide();
            TSS.FileChangeActions();
            TSS.Focus();
        }

        public static bool CloseWindow(TextSplitShow TSS, bool save) {
            int index = Globals.windowList.IndexOf(TSS);
            Globals.windowList.RemoveAt(index);
            if (!save) {
                Properties.Settings.Default.FileName.RemoveAt(index);
            }
            if (windowList.Count > 0) {
                ((TextSplitShow)Globals.windowList[0]).Focus();
                Globals.ShowPropertiesFileName();
                return true;
            } else {
                return false;
            }
        }

        public static void UpdateSlideInfo() {
            TSM.lSlideCount.Text = "of " + currentWindow.TST.TextList.Count.ToString();
            TSM.tGoToSlide.Text = (currentWindow.currentSlide + 1).ToString();
        }

        public static void ChangeReadOnly() {
            TSM.cReadOnly.Checked = Properties.Settings.Default.ReadOnly;
        }

        public static void ChangeDisableHK() {
            TSM.cDisableHK.Checked = Properties.Settings.Default.DisableHK;
        }

        public static void AddBefore() {
            if (currentWindow.TST.TextList.Count <= 999) {
                currentWindow.TST.TextList.Insert(currentWindow.currentSlide, "");
                currentWindow.DisplaySlide();
                currentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddAfter() {
            if (currentWindow.TST.TextList.Count <= 999) {
                currentWindow.TST.TextList.Insert(currentWindow.currentSlide + 1, "");
                currentWindow.currentSlide += 1;
                currentWindow.DisplaySlide();
                currentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void AddEnd() {
            if (currentWindow.TST.TextList.Count <= 999) {
                currentWindow.TST.TextList.Add("");
                currentWindow.currentSlide = currentWindow.TST.TextList.Count - 1;
                currentWindow.DisplaySlide();
                currentWindow.ChangeFilenameUnsaved();
            }
        }

        public static void Remove() {
            if (currentWindow.TST.TextList.Count > 1) {
                currentWindow.TST.TextList.RemoveAt(currentWindow.currentSlide);
                currentWindow.currentSlide = currentWindow.TST.TextList.Count - 1;
                currentWindow.DisplaySlide();
                currentWindow.ChangeFilenameUnsaved();
            }
        }

        /*
         * Events
         */

        private static void hook_KeyPressed(object sender, KeyPressedEventArgs e) {
            foreach (TextSplitShow TSS in Globals.windowList) {
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
