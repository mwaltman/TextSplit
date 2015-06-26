using System.Windows.Forms;
using System;
using System.Collections;

namespace TextSplit
{
    // The class that contains the global settings and functions that are reachable from all classes
    public static class Globals
    {
        public static TextSplitMain TSM { get; set; }
        public static TextSplitShow currentWindow { get; set; }
        public static ArrayList windowList { get; set; }

        private static KeyboardHook hook;

        static Globals() {
#if DEBUG
            Properties.Settings.Default.Reset();
#endif

            TSM = null;
            windowList = new ArrayList();
            currentWindow = null;

            if (Properties.Settings.Default.Hotkeys == null) {
                Properties.Settings.Default.Hotkeys = new Keys[8];
            }
            if (Properties.Settings.Default.FileName == null) {
                Properties.Settings.Default.FileName = new ArrayList();
                Properties.Settings.Default.FileName.Add("");
            }

            InitializeHotkeys();
        }

        public static void ShowPropertiesFileName() {
            Console.Write("FILENAME: ");
            foreach (string str in Properties.Settings.Default.FileName) {
                Console.Write(str + " --- ");
            }
            Console.WriteLine();
        }

        public static void InitializeHotkeys() {
            hook = new KeyboardHook();
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            for (int i = 0; i < Properties.Settings.Default.Hotkeys.Length; i++) {
                if (Properties.Settings.Default.Hotkeys[i] != Keys.None) {
                    hook.RegisterHotKey(Properties.Settings.Default.Hotkeys[i]);
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
            Globals.ShowPropertiesFileName();
        }

        public static void CloseWindow(TextSplitShow TSS) {
            int index = Globals.windowList.IndexOf(TSS);
            Globals.windowList.RemoveAt(index);
            Properties.Settings.Default.FileName.RemoveAt(index);
            Globals.currentWindow = (TextSplitShow)Globals.windowList[0];
            Globals.ShowPropertiesFileName();
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

        public static void GoToNext() {
            currentWindow.DetectTextChange();
            if (Properties.Settings.Default.SlideWrap) {
                currentWindow.currentSlide = (currentWindow.currentSlide + 1) % currentWindow.TST.TextList.Count;
            } else {
                if (currentWindow.currentSlide < currentWindow.TST.TextList.Count - 1) {
                    currentWindow.currentSlide += 1;
                }
            }
            currentWindow.DisplaySlide();
            UpdateSlideInfo();
        }

        public static void GoToPrev() {
            currentWindow.DetectTextChange();
            if (Properties.Settings.Default.SlideWrap) {
                currentWindow.currentSlide = (currentWindow.currentSlide + currentWindow.TST.TextList.Count - 1) % currentWindow.TST.TextList.Count;
            } else {
                if (currentWindow.currentSlide > 0) {
                    currentWindow.currentSlide -= 1;
                }
            }
            currentWindow.DisplaySlide();
            UpdateSlideInfo();
        }

        public static void GoToFirst() {
            currentWindow.DetectTextChange();
            currentWindow.currentSlide = 0;
            currentWindow.DisplaySlide();
            UpdateSlideInfo();
        }

        public static void GoToLast() {
            currentWindow.DetectTextChange();
            currentWindow.currentSlide = currentWindow.TST.TextList.Count - 1;
            currentWindow.DisplaySlide();
            UpdateSlideInfo();
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
            if (e.Key == Properties.Settings.Default.Hotkeys[0] || e.Key == Properties.Settings.Default.Hotkeys[1]) {
                Globals.GoToNext();
            }
            if (e.Key == Properties.Settings.Default.Hotkeys[2] || e.Key == Properties.Settings.Default.Hotkeys[3]) {
                Globals.GoToPrev();
            }
            if (e.Key == Properties.Settings.Default.Hotkeys[4] || e.Key == Properties.Settings.Default.Hotkeys[5]) {
                Globals.GoToFirst();
            }
            if (e.Key == Properties.Settings.Default.Hotkeys[6] || e.Key == Properties.Settings.Default.Hotkeys[7]) {
                Globals.GoToLast();
            }
        }
    }
}
