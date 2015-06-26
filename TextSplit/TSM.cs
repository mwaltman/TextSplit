using System;
using System.Windows.Forms;

namespace TextSplit
{
    // The main window used for navigating the slides
    public partial class TextSplitMain : Form
    {
        public TextSplitMain() {
            InitializeComponent();

            this.bPrevSlide.Click += new System.EventHandler(this.bPrevSlide_Click);
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            this.windowToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            this.openInNewWindowToolStripMenuItem.Click += new System.EventHandler(this.openInNewWindowToolStripMenuItem_Click);
            this.toolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.editLayoutToolStripMenuItem.Click += new System.EventHandler(this.editLayoutToolStripMenuItem_Click);
            this.editHotkeysToolStripMenuItem.Click += new System.EventHandler(this.editHotkeysToolStripMenuItem_Click);
            this.cReadOnly.CheckedChanged += new System.EventHandler(this.cReadOnly_CheckedChanged);
            this.cDisableHK.CheckedChanged += new System.EventHandler(this.cDisableHK_CheckedChanged);
            this.bAddAfter.Click += new System.EventHandler(this.bAddAfter_Click);
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            this.bAddBefore.Click += new System.EventHandler(this.bAddBefore_Click);
            this.bAddEnd.Click += new System.EventHandler(this.bAddEnd_Click);
            this.bLastSlide.Click += new System.EventHandler(this.bLastSlide_Click);
            this.bFirstSlide.Click += new System.EventHandler(this.bFirstSlide_Click);
            this.tGoToSlide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tGoToSlide_Enter);
            this.bNextSlide.Click += new System.EventHandler(this.bNextSlide_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TSM_Closing);
            this.Resize += new System.EventHandler(this.TSM_Resize);

            Globals.TSM = this;

            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(cReadOnly, "Toggles between Edit Mode and Read-Only Mode");
            toolTip.SetToolTip(cDisableHK, "Toggle this to disable the global hotkeys");
            toolTip.SetToolTip(bAddAfter, "Adds a new slide after this slide");
            toolTip.SetToolTip(bAddBefore, "Adds a new slide before this slide");
            toolTip.SetToolTip(bAddEnd, "Adds a new slide after the last slide");
            toolTip.SetToolTip(bRemove, "Removes the current slide");
            toolTip.SetToolTip(bNextSlide, "Next Slide");
            toolTip.SetToolTip(bPrevSlide, "Previous Slide");
            toolTip.SetToolTip(bFirstSlide, "First Slide");
            toolTip.SetToolTip(bLastSlide, "Last Slide");

            bool exampleOpen = true;
            for (int i = 0; i < Properties.Settings.Default.FileName.Count; i++) {
                TextSplitShow TSS = new TextSplitShow((string)Properties.Settings.Default.FileName[i]);
                if (TSS.fileLoaded) {
                    Globals.OpenNewWindow(TSS);
                    if (exampleOpen) {
                        exampleOpen = false;
                    }
                }
            }

            // Opens example 
            if (exampleOpen) {
                TextSplitShow example = new TextSplitShow("");
                Globals.OpenNewWindow(example);
            }
        }

        /*
         * Events
         */

        private void tGoToSlide_Enter(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                int n;
                if (!int.TryParse(tGoToSlide.Text, out n)) {
                    tGoToSlide.Text = (Globals.currentWindow.currentSlide + 1).ToString();
                } else {
                    int s = Convert.ToInt32(tGoToSlide.Text);
                    if (s <= 0 || s > Globals.currentWindow.TST.TextList.Count) {
                        tGoToSlide.Text = (Globals.currentWindow.currentSlide + 1).ToString();
                    } else {
                        Globals.currentWindow.currentSlide = s - 1;
                        Globals.UpdateSlideInfo();
                        Globals.currentWindow.DisplaySlide();
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

        private void bAddEnd_Click(object sender, EventArgs e) {
            Globals.AddEnd();
            Globals.UpdateSlideInfo();
        }

        private void bRemove_Click(object sender, EventArgs e) {
            Globals.Remove();
            Globals.UpdateSlideInfo();
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e) {
            Globals.currentWindow.NewFile();
        }

        private void windowToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.OpenNewWindow(new TextSplitShow(""));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.currentWindow.SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.currentWindow.SaveAsFile("Save As...");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.currentWindow.OpenFile("Open File...");
        }

        private void openInNewWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.currentWindow.OpenFileNewWindow("Open File...");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitAbout().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void editHotkeysToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!Properties.Settings.Default.DisableHK) {
                Globals.ClearHotkeys();
            }
            new TextSplitHotkeys(Globals.currentWindow).ShowDialog();
            if (!Properties.Settings.Default.DisableHK) {
                Globals.InitializeHotkeys();
            }
        }

        private void editLayoutToolStripMenuItem_Click(object sender, EventArgs e) {
            new TextSplitLayout(Globals.currentWindow).ShowDialog();
        }

        private void bPrevSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.windowList) {
                TSS.GoToPrev();
            }
        }

        private void bNextSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.windowList) {
                TSS.GoToNext();
            }
        }

        private void bFirstSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.windowList) {
                TSS.GoToFirst();
            }
        }

        private void bLastSlide_Click(object sender, EventArgs e) {
            foreach (TextSplitShow TSS in Globals.windowList) {
                TSS.GoToLast();
            }
        }

        private void bPrevWindow_Click(object sender, EventArgs e) {
            int index = Globals.windowList.IndexOf(Globals.currentWindow);
            Globals.currentWindow = (TextSplitShow)Globals.windowList[(index + Globals.windowList.Count - 1) % Globals.windowList.Count];
            Globals.UpdateSlideInfo();
        }

        private void bNextWindow_Click(object sender, EventArgs e) {
            int index = Globals.windowList.IndexOf(Globals.currentWindow);
            Globals.currentWindow = (TextSplitShow)Globals.windowList[(index + 1) % Globals.windowList.Count];
            Globals.UpdateSlideInfo();
        }

        private void cReadOnly_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.ReadOnly = cReadOnly.Checked;
            if (cReadOnly.Checked) {
                foreach (TextSplitShow TSS in Globals.windowList) {
                    TSS.FormBorderStyle = FormBorderStyle.FixedSingle;
                }
            } else {
                foreach (TextSplitShow TSS in Globals.windowList) {
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
                foreach (TextSplitShow TSS in Globals.windowList) {
                    TSS.WindowState = FormWindowState.Minimized;
                }
            }
            if (WindowState == FormWindowState.Normal) {
                foreach (TextSplitShow TSS in Globals.windowList) {
                    TSS.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void TSM_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Globals.ClearHotkeys();
            int windowCount = Globals.windowList.Count;
            for (int i = 0; i < windowCount; i++) {
                ((TextSplitShow)Globals.windowList[0]).CheckSaveChange();
                Globals.CloseWindow(((TextSplitShow)Globals.windowList[0]), true);
            }
            Properties.Settings.Default.Save();
        }
    }
}
