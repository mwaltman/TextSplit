using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace TextSplit
{
    // The main window used for navigating the slides
    public partial class TextSplitMain : Form
    {
        public TextSplitMain() {
            InitializeComponent();

            bPrevSlide.Click += new EventHandler(bPrevSlide_Click);
            fileToolStripMenuItem1.Click += new EventHandler(fileToolStripMenuItem1_Click);
            windowToolStripMenuItem.Click += new EventHandler(windowToolStripMenuItem_Click);
            saveToolStripMenuItem.Click += new EventHandler(saveToolStripMenuItem_Click);
            saveAsToolStripMenuItem.Click += new EventHandler(saveAsToolStripMenuItem_Click);
            loadToolStripMenuItem.Click += new EventHandler(openToolStripMenuItem_Click);
            openInNewWindowToolStripMenuItem.Click += new EventHandler(openInNewWindowToolStripMenuItem_Click);
            toolStripMenuItem1.Click += new EventHandler(aboutToolStripMenuItem_Click);
            exitToolStripMenuItem.Click += new EventHandler(exitToolStripMenuItem_Click);
            editLayoutToolStripMenuItem.Click += new EventHandler(editLayoutToolStripMenuItem_Click);
            editHotkeysToolStripMenuItem.Click += new EventHandler(editHotkeysToolStripMenuItem_Click);
            cReadOnly.CheckedChanged += new EventHandler(cReadOnly_CheckedChanged);
            cDisableHK.CheckedChanged += new EventHandler(cDisableHK_CheckedChanged);
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

            // Initializes the themes
            Globals.Themes = new Dictionary<string, Theme>();
            Globals.Themes.Add("Standard",  new Theme("Standard",   new Font("Microsoft Sans Serif", 11, FontStyle.Regular),    Color.FromArgb(0, 0, 0),        Color.FromArgb(255, 255, 255)));
            Globals.Themes.Add("Notepad",   new Theme("Notepad",    new Font("Consolas", 11, FontStyle.Regular),                Color.FromArgb(0, 0, 0),        Color.FromArgb(251, 251, 251)));
            Globals.Themes.Add("Carmine",   new Theme("Carmine",    new Font("Verdana", 11, FontStyle.Regular),                 Color.FromArgb(166, 0, 24),     Color.FromArgb(251, 251, 251)));
            Globals.Themes.Add("Blue Neon", new Theme("Blue Neon",  new Font("Segoe UI", 11, FontStyle.Regular),                Color.FromArgb(54, 162, 241),   Color.FromArgb(13, 13, 13)));
            Globals.Themes.Add("Camo",      new Theme("Camo",       new Font("Segoe UI", 11, FontStyle.Regular),                Color.FromArgb(162, 175, 114),  Color.FromArgb(38, 36, 26)));
            Globals.Themes.Add("Capuccino", new Theme("Capuccino",  new Font("Verdana", 11, FontStyle.Regular),                 Color.FromArgb(206, 103, 0),    Color.FromArgb(36, 24, 15)));
            Globals.Themes.Add("Midnight",  new Theme("Midnight",   new Font("Consolas", 11, FontStyle.Regular),                Color.FromArgb(229, 229, 229),  Color.FromArgb(28, 28, 28)));

            foreach (string k in Globals.Themes.Keys) {
                ToolStripDropDownItem item = new ToolStripMenuItem(k);
                item.Name = k;
                item.Click += new EventHandler(themeMenuItem_Click);
                themeMenuItem.DropDownItems.Add(item);
            }

            ToolStripSeparator sep2 = new ToolStripSeparator();
            sep2.Name = "  ";
            themeMenuItem.DropDownItems.Add(sep2);
            ToolStripMenuItem saveItem = new ToolStripMenuItem("Save Layout As Theme...");
            saveItem.Click += new EventHandler(saveUserTheme_Click);
            themeMenuItem.DropDownItems.Add(saveItem);

            // Unpacks the values of UserThemes to Globals.themes
            Globals.UserThemes = new Dictionary<string, Theme>();
            foreach (string s in Properties.Settings.Default.UserThemes) {
                Theme t = UnpackThemeFromString(s);
                Globals.Themes.Add(t.Name, t);
                Globals.UserThemes.Add(t.Name, t);
            }

            // Adds user created themes menu items
            if (Globals.UserThemes.Count > 0) {
                AddUserThemeSeparator();
                foreach (string s in Globals.UserThemes.Keys) {
                    Theme t = Globals.UserThemes[s];
                    AddUserThemeMenuItem(t.Name);
                }
            }

            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(cReadOnly, "Toggles between Edit Mode and Read-Only Mode");
            toolTip.SetToolTip(cDisableHK, "Toggle this to disable the global hotkeys");
            toolTip.SetToolTip(bAddAfter, "Adds a new slide after this slide");
            toolTip.SetToolTip(bAddBefore, "Adds a new slide before this slide");
            toolTip.SetToolTip(bAddBegin, "Adds a new slide before the first slide");
            toolTip.SetToolTip(bAddEnd, "Adds a new slide after the last slide");
            toolTip.SetToolTip(bRemove, "Removes the current slide");
            toolTip.SetToolTip(bNextSlide, "Next Slide");
            toolTip.SetToolTip(bPrevSlide, "Previous Slide");
            toolTip.SetToolTip(bFirstSlide, "First Slide");
            toolTip.SetToolTip(bLastSlide, "Last Slide");

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
            themeMenuItem.DropDownItems.Insert(themeMenuItem.DropDownItems.IndexOfKey("  "), sep);
        }

        private void AddUserThemeMenuItem(string k) {
            ToolStripDropDownItem item = new ToolStripMenuItem(k);
            item.Name = k;
            themeMenuItem.DropDownItems.Insert(themeMenuItem.DropDownItems.IndexOfKey("  "), item);
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
            Globals.CurrentWindow.SaveAsFile("Save As...");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.OpenFile("Open File...");
        }

        private void openInNewWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.CurrentWindow.OpenFileNewWindow("Open File...");
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

        private void themeMenuItem_Click(object sender, EventArgs e) {
            foreach (var item in themeMenuItem.DropDownItems) {
                if (item is ToolStripMenuItem)
                    ((ToolStripMenuItem)item).Checked = false;
            }
            if (((ToolStripMenuItem)sender).Text == "Apply") {
                Globals.Themes[((ToolStripMenuItem)sender).OwnerItem.Text].Apply();
                ((ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem).Checked = true;
            } else {
                Globals.Themes[((ToolStripMenuItem)sender).Text].Apply();
                ((ToolStripMenuItem)sender).Checked = true;
            }
        }

        private void saveUserTheme_Click(object sender, EventArgs e) {
            bool repeat = true;
            string input = "";
            while (repeat) {
                input = "";
                if (Globals.ShowInputDialog("Enter Theme Name", ref input) != DialogResult.Cancel) {
                    if (string.IsNullOrWhiteSpace(input)) {
                        MessageBox.Show("The name cannot be empty or consist of only white spaces.", "Error", MessageBoxButtons.OK);
                    } else if (Globals.Themes.ContainsKey(input) || Globals.UserThemes.ContainsKey(input)) {
                        MessageBox.Show("A theme with that name already exists.", "Error", MessageBoxButtons.OK);
                    } else if (input.IndexOfAny(new char[]{ ',', '.' }) != -1) {
                        MessageBox.Show("The name must not contain a comma or a period.", "Error", MessageBoxButtons.OK);
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
            }
            Globals.ShowPropertiesUserThemes();
        }

        private void removeUserTheme_Click(object sender, EventArgs e) {
            string s = ((ToolStripMenuItem)sender).OwnerItem.Text;
            themeMenuItem.DropDownItems.RemoveByKey(s);
            Globals.Themes.Remove(s);
            Globals.UserThemes.Remove(s);
            if (Globals.UserThemes.Count == 0) {
                themeMenuItem.DropDownItems.RemoveByKey(" ");
            }
            for (int i = 0; i < Properties.Settings.Default.UserThemes.Count; i++) {
                if (((string)Properties.Settings.Default.UserThemes[i]).Substring(0, ((string)Properties.Settings.Default.UserThemes[i]).IndexOf(',')) == s) {
                    Properties.Settings.Default.UserThemes.RemoveAt(i);
                }
            }
            Globals.ShowPropertiesUserThemes();
        }

        private void bPrevSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                TSS.GoToPrev();
            }
        }

        private void bNextSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                TSS.GoToNext();
            }
        }

        private void bFirstSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
                TSS.GoToFirst();
            }
        }

        private void bLastSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.WindowList) {
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
