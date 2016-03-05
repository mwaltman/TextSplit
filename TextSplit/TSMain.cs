using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace TextSplit
{
    // The main window used for navigating the slides
    public partial class TextSplitMain : Form
    {
        public ToolStripMenuItem saveUserThemeItem;

        public TextSplitMain() {
            InitializeComponent();

            bPrevSlide.Click += new EventHandler(bPrevSlide_Click);
            fileToolStripMenuItem1.Click += new EventHandler(fileToolStripMenuItem1_Click);
            windowToolStripMenuItem.Click += new EventHandler(windowToolStripMenuItem_Click);
            saveToolStripMenuItem.Click += new EventHandler(saveToolStripMenuItem_Click);
            saveAsToolStripMenuItem.Click += new EventHandler(saveAsToolStripMenuItem_Click);
            loadToolStripMenuItem.Click += new EventHandler(openToolStripMenuItem_Click);
            openInNewWindowToolStripMenuItem.Click += new EventHandler(openInNewWindowToolStripMenuItem_Click);
            aboutToolStripMenuItem.Click += new EventHandler(aboutToolStripMenuItem_Click);
            importFromTxtToolStripMenuItem.Click += new EventHandler(importFromTxtToolStripMenuItem_Click);
            exportToTxtToolStripMenuItem.Click += new EventHandler(exportToTxtToolStripMenuItem_Click);
            exitToolStripMenuItem.Click += new EventHandler(exitToolStripMenuItem_Click);
            editLayoutToolStripMenuItem.Click += new EventHandler(editLayoutToolStripMenuItem_Click);
            editHotkeysToolStripMenuItem.Click += new EventHandler(editHotkeysToolStripMenuItem_Click);
            editSyncSettingsToolStripMenuItem.Click += new EventHandler(editSyncSettingsToolStripMenuItem_Click);
            editPreferencesToolStripMenuItem.Click += new EventHandler(editPreferencesToolStripMenuItem_Click);
            openDocumentationToolStripMenuItem.Click += new EventHandler(openDocumentationToolStripMenuItem_Click);
            cReadOnly.CheckedChanged += new EventHandler(cReadOnly_CheckedChanged);
            cDisableHK.CheckedChanged += new EventHandler(cDisableHK_CheckedChanged);
            bSyncTxt.Click += new EventHandler(bSyncTxt_Click);
            bSyncTst.Click += new EventHandler(bSyncTst_Click);
            bAddAfter.Click += new EventHandler(bAddAfter_Click);
            bRemove.Click += new EventHandler(bRemove_Click);
            bAddBefore.Click += new EventHandler(bAddBefore_Click);
            bAddBegin.Click += new EventHandler(bAddBegin_Click);
            bAddEnd.Click += new EventHandler(bAddEnd_Click);
            bLastSlide.Click += new EventHandler(bLastSlide_Click);
            bFirstSlide.Click += new EventHandler(bFirstSlide_Click);
            tGoToSlide.KeyDown += new KeyEventHandler(tGoToSlide_Enter);
            bNextSlide.Click += new EventHandler(bNextSlide_Click);
            FormClosing += new FormClosingEventHandler(TSM_Closing);
            Resize += new EventHandler(TSM_Resize);

            Globals.TSM = this;

            cReadOnly.Checked = Properties.Settings.Default.ReadOnly;
            cDisableHK.Checked = Properties.Settings.Default.DisableHK;

            // Initializes the themes
            Globals.Themes = new Dictionary<string, Theme>();
            Globals.Themes.Add("Standard",  new Theme("Standard",   new Font("Microsoft Sans Serif", 11, FontStyle.Regular),    Color.FromArgb(0, 0, 0),        Color.FromArgb(255, 255, 255)));
            Globals.Themes.Add("Notepad",   new Theme("Notepad",    new Font("Consolas", 11, FontStyle.Regular),                Color.FromArgb(0, 0, 0),        Color.FromArgb(251, 251, 251)));
            Globals.Themes.Add("Carmine",   new Theme("Carmine",    new Font("Verdana", 11, FontStyle.Regular),                 Color.FromArgb(166, 0, 24),     Color.FromArgb(251, 251, 251)));
            Globals.Themes.Add("Blue Neon", new Theme("Blue Neon",  new Font("Segoe UI", 11, FontStyle.Regular),                Color.FromArgb(54, 162, 241),   Color.FromArgb(13, 13, 13)));
            Globals.Themes.Add("Camo",      new Theme("Camo",       new Font("Segoe UI", 11, FontStyle.Regular),                Color.FromArgb(162, 175, 114),  Color.FromArgb(38, 36, 26)));
            Globals.Themes.Add("Capuccino", new Theme("Capuccino",  new Font("Verdana", 11, FontStyle.Regular),                 Color.FromArgb(206, 103, 0),    Color.FromArgb(36, 24, 15)));
            Globals.Themes.Add("Midnight",  new Theme("Midnight",   new Font("Consolas", 11, FontStyle.Regular),                Color.FromArgb(229, 229, 229),  Color.FromArgb(28, 28, 28)));

            // Adds menuitems to themesToolStripMenuItem
            foreach (string k in Globals.Themes.Keys) {
                ToolStripDropDownItem item = new ToolStripMenuItem(k);
                item.Name = k;
                item.Click += new EventHandler(themeMenuItem_Click);
                themesToolStripMenuItem.DropDownItems.Add(item);
            }
            ToolStripSeparator sep = new ToolStripSeparator();
            sep.Name = "  ";
            themesToolStripMenuItem.DropDownItems.Add(sep);
            saveUserThemeItem = new ToolStripMenuItem("Save layout as theme...");
            saveUserThemeItem.Click += new EventHandler(saveUserTheme_Click);
            themesToolStripMenuItem.DropDownItems.Add(saveUserThemeItem);

            // Unpacks the values of UserThemes to Globals.themes
            Globals.UserThemes = new Dictionary<string, Theme>();
            foreach (string s in Properties.Settings.Default.UserThemes) {
                Theme t = UnpackThemeFromString(s);
                Globals.Themes.Add(t.Name, t);
                Globals.UserThemes.Add(t.Name, t);
            }

            // Adds user created themes menu items to themesToolStripMenuItem
            if (Globals.UserThemes.Count > 0) {
                AddUserThemeSeparator();
                foreach (string s in Globals.UserThemes.Keys) {
                    Theme t = Globals.UserThemes[s];
                    AddUserThemeMenuItem(t.Name);
                }
            }

            Globals.ToolTip.SetToolTip(cReadOnly,       "Enables or disables editing the text on the slides");
            Globals.ToolTip.SetToolTip(cDisableHK,      "Enables or disables the global hotkeys");
            Globals.ToolTip.SetToolTip(bSyncTxt,        "Sets the txt file contents to that of the tst slides if syncing is enabled");
            Globals.ToolTip.SetToolTip(bSyncTst,        "Sets the tst slides contents to that of the txt file if syncing is enabled");
            Globals.ToolTip.SetToolTip(bAddAfter,       "Adds a new slide after this slide");
            Globals.ToolTip.SetToolTip(bAddBefore,      "Adds a new slide before this slide");
            Globals.ToolTip.SetToolTip(bAddBegin,       "Adds a new slide before the first slide");
            Globals.ToolTip.SetToolTip(bAddEnd,         "Adds a new slide after the last slide");
            Globals.ToolTip.SetToolTip(bRemove,         "Removes the current slide");
            Globals.ToolTip.SetToolTip(bNextSlide,      "Next slide");
            Globals.ToolTip.SetToolTip(bPrevSlide,      "Previous slide");
            Globals.ToolTip.SetToolTip(bFirstSlide,     "First slide");
            Globals.ToolTip.SetToolTip(bLastSlide,      "Last slide");

            bool exampleOpen = true;
            int j = 0;
            while (j < Properties.Settings.Default.FileNames.Count) {
                TextSplitShow TSS = new TextSplitShow((string)Properties.Settings.Default.FileNames[j], false);
                if (TSS.fileLoaded) {
                    Globals.OpenNewWindow(TSS);
                    if (exampleOpen) {
                        exampleOpen = false;
                    }
                    j++;
                } else {
                    Globals.WindowList.RemoveAt(Globals.WindowList.Count - 1);
                    Properties.Settings.Default.FileNames.RemoveAt(j);
                }
            }

            // Opens example 
            if (exampleOpen) {
                TextSplitShow example = new TextSplitShow("", true);
                Globals.OpenNewWindow(example);
                example.fileLoaded = false;
            }
        }

        private Theme UnpackThemeFromString(string s) {
            Theme t = new Theme();
            char[] separators = { ',' };
            string[] parts = s.Split(separators);
            t.Name = parts[0];
            t.Font = new Font(parts[1], float.Parse(parts[2]), (FontStyle)int.Parse(parts[3]));
            t.ColorT = Color.FromArgb(int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
            t.ColorB = Color.FromArgb(int.Parse(parts[7]), int.Parse(parts[8]), int.Parse(parts[9]));
            return t;
        }

        private void AddUserThemeSeparator() {
            ToolStripSeparator sep = new ToolStripSeparator();
            sep.Name = " ";
            themesToolStripMenuItem.DropDownItems.Insert(themesToolStripMenuItem.DropDownItems.IndexOfKey("  "), sep);
        }

        private void AddUserThemeMenuItem(string k) {
            ToolStripDropDownItem item = new ToolStripMenuItem(k);
            item.Name = k;
            themesToolStripMenuItem.DropDownItems.Insert(themesToolStripMenuItem.DropDownItems.IndexOfKey("  "), item);
            ToolStripItem applyItem = item.DropDownItems.Add("Apply");
            applyItem.Click += new EventHandler(themeMenuItem_Click);
            ToolStripItem removeItem = item.DropDownItems.Add("Remove");
            removeItem.Click += new EventHandler(removeUserTheme_Click);
        }

        /*
         * Events
         */

        private void tGoToSlide_Enter(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                int n;
                if (!int.TryParse(tGoToSlide.Text, out n)) {
                    tGoToSlide.Text = (Globals.CurrentWindow.currentSlide + 1).ToString();
                } else {
                    int s = Convert.ToInt32(tGoToSlide.Text);
                    if (s <= 0 || s > Globals.CurrentWindow.TST.TextList.Count) {
                        tGoToSlide.Text = (Globals.CurrentWindow.currentSlide + 1).ToString();
                    } else {
                        Globals.CurrentWindow.currentSlide = s - 1;
                        Globals.UpdateSlideInfo();
                        Globals.CurrentWindow.DisplaySlide();
                    }
                }
                lSlideCount.Focus();
            }
        }

        private void bAddAfter_Click(object sender, EventArgs e) {
            Globals.AddAfter();
            Globals.UpdateSlideInfo();
        }

        private void bAddBefore_Click(object sender, EventArgs e) {
            Globals.AddBefore();
            Globals.UpdateSlideInfo();
        }

        private void bAddBegin_Click(object sender, EventArgs e) {
            Globals.AddBegin();
            Globals.UpdateSlideInfo();
        }

        private void bAddEnd_Click(object sender, EventArgs e) {
            Globals.AddEnd();
            Globals.UpdateSlideInfo();
        }

        private void bRemove_Click(object sender, EventArgs e) {
            Globals.Remove();
            Globals.UpdateSlideInfo();
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.NewFile();
        }

        private void windowToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.OpenNewWindow(new TextSplitShow("", true));
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.SaveFile();
        }

        public void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.SaveAsFile("Save file as");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.OpenFile("Open file");
        }

        private void openInNewWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.OpenFileNewWindow("Open file");
        }

        private void importFromTxtToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitImport().ShowDialog();
        }

        private void exportToTxtToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitExport().ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitAbout().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void editHotkeysToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!Properties.Settings.Default.DisableHK) {
                Globals.ClearHotkeys();
            }
            new TextSplitHotkeys(Globals.CurrentWindow).ShowDialog();
            if (!Properties.Settings.Default.DisableHK) {
                Globals.InitializeHotkeys();
            }
        }

        private void editLayoutToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitLayout(Globals.CurrentWindow).ShowDialog();
        }

        private void editSyncSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitSync().ShowDialog();
        }

        private void editPreferencesToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitPreferences().ShowDialog();
        }

        private void openDocumentationToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.OpenURL("https://github.com/mwaltman/TextSplit/wiki");
        }

        private void themeMenuItem_Click(object sender, EventArgs e) {
            foreach (var item in themesToolStripMenuItem.DropDownItems) {
                if (item is ToolStripMenuItem)
                    ((ToolStripMenuItem)item).Checked = false;
            }
            if (((ToolStripMenuItem)sender).Text == "Apply") {
                Globals.Themes[((ToolStripMenuItem)sender).OwnerItem.Text].Apply(Globals.CurrentWindow.TST, true);
                ((ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem).Checked = true;
            } else {
                Globals.Themes[((ToolStripMenuItem)sender).Text].Apply(Globals.CurrentWindow.TST, true);
                ((ToolStripMenuItem)sender).Checked = true;
            }
        }

        private void saveUserTheme_Click(object sender, EventArgs e) {
            bool repeat = true;
            string input = "";
            while (repeat) {
                input = "";
                if (Globals.ShowInputDialog("Enter theme name", ref input) != DialogResult.Cancel) {
                    if (string.IsNullOrWhiteSpace(input)) {
                        Globals.ShowErrorMessage("The name cannot be empty or consist of only white spaces. Please enter a different name.");
                    } else if (Globals.Themes.ContainsKey(input) || Globals.UserThemes.ContainsKey(input)) {
                        Globals.ShowErrorMessage("A theme with that name already exists. Please enter a different name.");
                    } else if (input.IndexOfAny(new char[]{ ',', '.' }) != -1) {
                        Globals.ShowErrorMessage("The name must not contain a comma or a period. Please enter a different name.");
                    } else {
                        repeat = false;
                    }
                } else {
                    repeat = false;
                }
            }
            if (input != "") {
                Theme t = new Theme(input, Globals.CurrentWindow.TST.TextFont, Globals.CurrentWindow.TST.Colors[0], Globals.CurrentWindow.TST.Colors[1]);
                if (Globals.UserThemes.Count == 0) {
                    AddUserThemeSeparator();
                }
                AddUserThemeMenuItem(t.Name);
                Globals.Themes.Add(t.Name, t);
                Globals.UserThemes.Add(t.Name, t);
                Properties.Settings.Default.UserThemes.Add(t.PackToString());
                if (Properties.Settings.Default.UserThemes.Count == 10) {
                    saveUserThemeItem.Enabled = false;
                }
            }
            Globals.ShowPropertiesUserThemes();
        }

        private void removeUserTheme_Click(object sender, EventArgs e) {
            string s = ((ToolStripMenuItem)sender).OwnerItem.Text;
            themesToolStripMenuItem.DropDownItems.RemoveByKey(s);
            Globals.Themes.Remove(s);
            Globals.UserThemes.Remove(s);
            if (Globals.UserThemes.Count == 0) {
                themesToolStripMenuItem.DropDownItems.RemoveByKey(" ");
            }
            for (int i = 0; i < Properties.Settings.Default.UserThemes.Count; i++) {
                if (((string)Properties.Settings.Default.UserThemes[i]).Substring(0, ((string)Properties.Settings.Default.UserThemes[i]).IndexOf(',')) == s) {
                    Properties.Settings.Default.UserThemes.RemoveAt(i);
                }
            }
            if (!saveUserThemeItem.Enabled) {
                // If saveItem.Enabled was false because there were 10 user themes, then now there are less so it should be enabled again
                saveUserThemeItem.Enabled = true;
            }
            Globals.ShowPropertiesUserThemes();
        }

        private void bPrevSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                if (Properties.Settings.Default.NavigateAll || (!Properties.Settings.Default.NavigateAll && TSS.Equals(Globals.CurrentWindow)))
                    TSS.GoToPrev();
            }
        }

        private void bNextSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                if (Properties.Settings.Default.NavigateAll || (!Properties.Settings.Default.NavigateAll && TSS.Equals(Globals.CurrentWindow)))
                    TSS.GoToNext();
            }
        }

        private void bFirstSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                if (Properties.Settings.Default.NavigateAll || (!Properties.Settings.Default.NavigateAll && TSS.Equals(Globals.CurrentWindow)))
                    TSS.GoToFirst();
            }
        }

        private void bLastSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                if (Properties.Settings.Default.NavigateAll || (!Properties.Settings.Default.NavigateAll && TSS.Equals(Globals.CurrentWindow)))
                    TSS.GoToLast();
            }
        }

        private void bPrevWindow_Click(object sender, EventArgs e) {
            int index = Globals.WindowList.IndexOf(Globals.CurrentWindow);
            Globals.CurrentWindow = (TextSplitShow)Globals.WindowList[(index + Globals.WindowList.Count - 1) % Globals.WindowList.Count];
            Globals.UpdateSlideInfo();
        }

        private void bNextWindow_Click(object sender, EventArgs e) {
            int index = Globals.WindowList.IndexOf(Globals.CurrentWindow);
            Globals.CurrentWindow = (TextSplitShow)Globals.WindowList[(index + 1) % Globals.WindowList.Count];
            Globals.UpdateSlideInfo();
        }

        private void cReadOnly_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.ReadOnly = cReadOnly.Checked;
            if (cReadOnly.Checked) {
                foreach (TextSplitShow TSS in Globals.WindowList) {
                    TSS.FormBorderStyle = FormBorderStyle.FixedSingle;
                }
            } else {
                foreach (TextSplitShow TSS in Globals.WindowList) {
                    TSS.FormBorderStyle = FormBorderStyle.Sizable;
                }
            }
        }

        private void cDisableHK_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.DisableHK = cDisableHK.Checked;
            if (cDisableHK.Checked) {
                Globals.ClearHotkeys();
            } else {
                Globals.InitializeHotkeys();
            }
        }

        private void bSyncTxt_Click(object sender, EventArgs e) {
            Globals.SyncTxt();
        }

        private void bSyncTst_Click(object sender, EventArgs e) {
            Globals.SyncTst();
        }

        private void TSM_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                foreach (TextSplitShow TSS in Globals.WindowList) {
                    TSS.WindowState = FormWindowState.Minimized;
                }
            }
            if (WindowState == FormWindowState.Normal) {
                foreach (TextSplitShow TSS in Globals.WindowList) {
                    TSS.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void TSM_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Globals.ClearHotkeys();
            int windowCount = Globals.WindowList.Count;
            for (int i = 0; i < windowCount; i++) {
                ((TextSplitShow)Globals.WindowList[0]).CheckSaveChange();
                Globals.CloseWindow(((TextSplitShow)Globals.WindowList[0]), true);
            }
            Properties.Settings.Default.Save();
        }
    }
}
