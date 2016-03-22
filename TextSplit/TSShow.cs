<<<<<<< HEAD
﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace TextSplit
{
    // The window that displays the current slide
    public partial class TextSplitShow : Form
    {
        private bool saveCancel;            // Whether cancel has been pressed when saving

        public TextSplitText TST;           // The linked TST object
        public int currentSlide;            // Current slide index
        public string fileName;             // Linked TST file name
        public bool fileLoaded;             // Whether an object has been loaded or just the example file
        public bool saveChanged;            // Whether any change has occured to this file after the last change

        // Opens a new window and loads the .tst file with the given fileName
        // If the file doesn't exist (e.g. fileName = ""), then it will open the example file
        public TextSplitShow(string fileName, bool addToFileNames) {
            InitializeComponent();
            DoubleBuffered = true;

            FormClosing += new FormClosingEventHandler(TSS_Closing);
            tTextBox.GotFocus += new EventHandler(tTextBox_GotFocus);
            tTextBox.LostFocus += new EventHandler(tTextBox_Unfocus);
            tTextBox.MouseEnter += new EventHandler(tTextBox_MouseEnter);
            SizeChanged += new EventHandler(TSS_SizeChanged);
            Resize += new EventHandler(TSS_Resize);
            saveToolStripMenuItem.Click += new EventHandler(Globals.TSM.saveToolStripMenuItem_Click);
            saveAsToolStripMenuItem.Click += new EventHandler(Globals.TSM.saveAsToolStripMenuItem_Click);

            currentSlide = 0;
            fileLoaded = false;
            saveChanged = false;
            this.fileName = fileName;
            TST = new TextSplitText();
            Globals.CurrentWindow = this;
            Globals.WindowList.Add(this);
            if (addToFileNames)
                Properties.Settings.Default.FileNames.Add(fileName);

            try {
                TST = Globals.Serializer.DeSerializeObject(fileName);
                FileChangeActions();
            } catch (Exception) {
                this.fileName = "Example";
                TST.SetEmpty();

                ChangeFilenameSaved();
                Globals.UpdateSlideInfo();
                Globals.ChangeDisableHK();
                Globals.ChangeReadOnly();
            }
        }

        public void DisplaySlide() {
            DisplaySlide(TST.TextFont, TST.Colors[0], TST.Colors[1], TST.Size, TST.Margins);
        }

        public void DisplaySlide(Font textFont, Color textColor, Color bgColor, int[] size, int[] margins) {
            RichTextBox tTextBox = this.tTextBox;
            if (!tTextBox.Font.Equals(textFont))
                tTextBox.Font = textFont;
            if (!tTextBox.BackColor.Equals(bgColor))
                tTextBox.BackColor = bgColor;
            if (!BackColor.Equals(bgColor))
                BackColor = bgColor;
            if (!tTextBox.ForeColor.Equals(textColor))
                tTextBox.ForeColor = textColor;
            if (Width != size[0] + 16)
                Width = size[0] + 16;
            if (Height != size[1] + 38)
                Height = size[1] + 38;
            if (tTextBox.Width != size[0] - margins[0] + (Properties.Settings.Default.DisplayVerticalScrollBars > 0 ? 0 : SystemInformation.VerticalScrollBarWidth)) 
                tTextBox.Width = size[0] - margins[0] + (Properties.Settings.Default.DisplayVerticalScrollBars > 0 ? 0 : SystemInformation.VerticalScrollBarWidth);
            if (tTextBox.Height != size[1] - (margins[2] + margins[3]))
                tTextBox.Height = size[1] - (margins[2] + margins[3]);
            if (!tTextBox.Location.Equals(new Point(margins[0], margins[2])))
                tTextBox.Location = new Point(margins[0], margins[2]);

            tTextBox.Text = TST.TextList[currentSlide].ToString();
            tTextBox.SelectAll();
            tTextBox.SelectionRightIndent = margins[1];
            tTextBox.DeselectAll();
        }

        public void DetectTextChange() {
            if ((string)TST.TextList[currentSlide] != tTextBox.Text) {
                TST.TextList[currentSlide] = tTextBox.Text;
                ChangeFilenameUnsaved();
            }
        }

        public void ChangeFilenameSaved() {
            if (saveChanged) {
                Text = Path.GetFileNameWithoutExtension(fileName) + " - TextSplit";
                saveChanged = false;
            }
        }

        public void ChangeFilenameUnsaved() {
            if (!saveChanged) {
                Text = Path.GetFileNameWithoutExtension(fileName) + "* - TextSplit";
                saveChanged = true;
                Globals.UpdateUpdatedIcon(); // Any change in the text will set the icon to false
            }
        }

        public void CheckSaveChange() {
            if (fileLoaded && saveChanged) {
                DialogResult result = MessageBox.Show("Save changes to '" + fileName + "' first?", "Save changes?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    SaveFile();
                }
            }
        }

        /*
         * New, Save, Save As, Open
         */

        public void NewFile() {
            CheckSaveChange(); // Saves the old file if it has any changes

            TextSplitText TST_temp = new TextSplitText(TST);
            TST.SetEmpty();
            SaveAsFile("New file");
            if (saveCancel) {
                TST = TST_temp;
            } else {
                Width = 300;
                Height = 300;
            }
        }

        public void SaveFile() {
            if (!fileLoaded) {
                SaveAsFile("Save file as");
            } else {
                Globals.Serializer.SerializeObject(fileName, TST);
                ChangeFilenameSaved();
            }
            // Also syncs the txt file
            if (!saveCancel) {
                if (TST.SyncAutoSave) {
                    Globals.SyncTxt();
                }
            }
        }

        public void SaveAsFile(string title) {
            saveFileDialog.Filter = "TextSplit Text File|*.tst";
            saveFileDialog.Title = title;
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel && saveFileDialog.FileName != "") {
                fileName = saveFileDialog.FileName;
                Globals.Serializer.SerializeObject(fileName, TST);
                FileChangeActions();
                saveCancel = false;
            } else {
                saveCancel = true;
            }
        }

        public void OpenFile(string title) {
            CheckSaveChange(); // Saves the old file if it has any changes

            openFileDialog.Filter = "TextSplit Text File|*.tst";
            openFileDialog.Title = title;
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "") {
                fileName = openFileDialog.FileName;
                TST = Globals.Serializer.DeSerializeObject(fileName);
                FileChangeActions();
                Focus();
            }
        }

        public void OpenFileNewWindow(string title) {
            openFileDialog.Filter = "TextSplit Text File|*.tst";
            openFileDialog.Title = title;
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "") {
                string newFileName = openFileDialog.FileName;
                TextSplitShow TSS = new TextSplitShow(newFileName, true);
                TSS.TST = Globals.Serializer.DeSerializeObject(newFileName);
                Globals.OpenNewWindow(TSS);
            }
        }

        // All actions to currentWindow after a new file has been opened in that window
        public void FileChangeActions() {
            currentSlide = 0;
            DisplaySlide();
            Globals.UpdateSlideInfo();
            ChangeFilenameSaved();
            fileLoaded = true;
            int index = Globals.WindowList.IndexOf(this);
            Properties.Settings.Default.FileNames[index] = fileName;
            Globals.ShowPropertiesFileNames();
        }

        public void GoToNext() {
            DetectTextChange();
            if (Properties.Settings.Default.Continuous) {
                currentSlide = (currentSlide + 1) % TST.TextList.Count;
            } else {
                if (currentSlide < TST.TextList.Count - 1) {
                    currentSlide += 1;
                }
            }
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        public void GoToPrev() {
            DetectTextChange();
            if (Properties.Settings.Default.Continuous) {
                currentSlide = (currentSlide + TST.TextList.Count - 1) % TST.TextList.Count;
            } else {
                if (currentSlide > 0) {
                    currentSlide -= 1;
                }
            }
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        public void GoToFirst() {
            DetectTextChange();
            currentSlide = 0;
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        public void GoToLast() {
            DetectTextChange();
            currentSlide = TST.TextList.Count - 1;
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        /*
         * Events
         */

        private void TSS_SizeChanged(object sender, EventArgs e) {
            TST.Size[0] = Width - 16; // 16 = total window margins width
            TST.Size[1] = Height - 38; // 38 = total window margins height
            tTextBox.Width = TST.Size[0] - TST.Margins[0] + (Properties.Settings.Default.DisplayVerticalScrollBars > 0 ? 0 : SystemInformation.VerticalScrollBarWidth);
            tTextBox.Height = TST.Size[1] - (TST.Margins[2] + TST.Margins[3]);
            tTextBox.Location = new Point(TST.Margins[0], TST.Margins[2]);
            ChangeFilenameUnsaved();
        }

        private void TSS_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal && Globals.TSM.WindowState == FormWindowState.Minimized) {
                Globals.TSM.WindowState = FormWindowState.Normal;
            }
        }

        private void tTextBox_GotFocus(object sender, EventArgs e) {
            if (!Globals.CurrentWindow.Equals(this)) {
                Globals.CurrentWindow = this;
                Globals.UpdateSlideInfo();
            }
            if (Properties.Settings.Default.ReadOnly) {
                lFocusHandler.Focus();
            }
        }

        private void tTextBox_MouseEnter(object sender, EventArgs e) {
            if (Properties.Settings.Default.ReadOnly) {
                tTextBox.Cursor = Cursors.Arrow;
            } else {
                tTextBox.Cursor = Cursors.IBeam;
            }
        }

        private void tTextBox_Unfocus(object sender, EventArgs e) {
            DetectTextChange();
        }

        private void TSS_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (!Globals.CloseWindow(this, false)) { // If the window is the last window, then close main as well
                Globals.TSM.Close();
            }
        }
    }
}
=======
﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace TextSplit
{
    // The window that displays the current slide
    public partial class TextSplitShow : Form
    {
        private bool saveCancel;            // Whether cancel has been pressed when saving

        public TextSplitText TST;           // The linked TST object
        public int currentSlide;            // Current slide index
        public string fileName;             // Linked TST file name
        public bool fileLoaded;             // Whether an object has been loaded or just the example file
        public bool saveChanged;            // Whether any change has occured to this file after the last change

        // Opens a new window and loads the .tst file with the given fileName
        // If the file doesn't exist (e.g. fileName = ""), then it will open the example file
        public TextSplitShow(string fileName, bool addToFileNames) {
            InitializeComponent();
            DoubleBuffered = true;

            FormClosing += new FormClosingEventHandler(TSS_Closing);
            tTextBox.GotFocus += new EventHandler(tTextBox_GotFocus);
            tTextBox.LostFocus += new EventHandler(tTextBox_Unfocus);
            tTextBox.MouseEnter += new EventHandler(tTextBox_MouseEnter);
            SizeChanged += new EventHandler(TSS_SizeChanged);
            Resize += new EventHandler(TSS_Resize);
            saveToolStripMenuItem.Click += new EventHandler(Globals.TSM.saveToolStripMenuItem_Click);
            saveAsToolStripMenuItem.Click += new EventHandler(Globals.TSM.saveAsToolStripMenuItem_Click);

            currentSlide = 0;
            fileLoaded = false;
            saveChanged = false;
            this.fileName = fileName;
            TST = new TextSplitText();
            Globals.CurrentWindow = this;
            Globals.WindowList.Add(this);
            if (addToFileNames)
                Properties.Settings.Default.FileNames.Add(fileName);

            try {
                TST = Globals.Serializer.DeSerializeObject(fileName);
                FileChangeActions();
            } catch (Exception) {
                this.fileName = "Example";
                TST.SetEmpty();

                ChangeFilenameSaved();
                Globals.UpdateSlideInfo();
                Globals.ChangeDisableHK();
                Globals.ChangeReadOnly();
            }
        }

        public void DisplaySlide() {
            DisplaySlide(TST.TextFont, TST.Colors[0], TST.Colors[1], TST.Size, TST.Margins);
        }

        public void DisplaySlide(Font textFont, Color textColor, Color bgColor, int[] size, int[] margins) {
            RichTextBox tTextBox = this.tTextBox;
            if (!tTextBox.Font.Equals(textFont))
                tTextBox.Font = textFont;
            if (!tTextBox.BackColor.Equals(bgColor))
                tTextBox.BackColor = bgColor;
            if (!BackColor.Equals(bgColor))
                BackColor = bgColor;
            if (!tTextBox.ForeColor.Equals(textColor))
                tTextBox.ForeColor = textColor;
            if (Width != size[0] + 16)
                Width = size[0] + 16;
            if (Height != size[1] + 38)
                Height = size[1] + 38;
            if (tTextBox.Width != size[0] - margins[0] + (Properties.Settings.Default.DisplayVerticalScrollBars > 0 ? 0 : SystemInformation.VerticalScrollBarWidth)) 
                tTextBox.Width = size[0] - margins[0] + (Properties.Settings.Default.DisplayVerticalScrollBars > 0 ? 0 : SystemInformation.VerticalScrollBarWidth);
            if (tTextBox.Height != size[1] - (margins[2] + margins[3]))
                tTextBox.Height = size[1] - (margins[2] + margins[3]);
            if (!tTextBox.Location.Equals(new Point(margins[0], margins[2])))
                tTextBox.Location = new Point(margins[0], margins[2]);

            tTextBox.Text = TST.TextList[currentSlide].ToString();
            tTextBox.SelectAll();
            tTextBox.SelectionRightIndent = margins[1];
            tTextBox.DeselectAll();
        }

        public void DetectTextChange() {
            if ((string)TST.TextList[currentSlide] != tTextBox.Text) {
                TST.TextList[currentSlide] = tTextBox.Text;
                ChangeFilenameUnsaved();
            }
        }

        public void ChangeFilenameSaved() {
            if (saveChanged) {
                Text = Path.GetFileNameWithoutExtension(fileName) + " - TextSplit";
                saveChanged = false;
            }
        }

        public void ChangeFilenameUnsaved() {
            if (!saveChanged) {
                Text = Path.GetFileNameWithoutExtension(fileName) + "* - TextSplit";
                saveChanged = true;
                Globals.UpdateUpdatedIcon(); // Any change in the text will set the icon to false
            }
        }

        public void CheckSaveChange() {
            if (fileLoaded && saveChanged) {
                DialogResult result = MessageBox.Show("Save changes to '" + fileName + "' first?", "Save changes?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    SaveFile();
                }
            }
        }

        /*
         * New, Save, Save As, Open
         */

        public void NewFile() {
            CheckSaveChange(); // Saves the old file if it has any changes

            TextSplitText TST_temp = new TextSplitText(TST);
            TST.SetEmpty();
            SaveAsFile("New file");
            if (saveCancel) {
                TST = TST_temp;
            } else {
                Width = 300;
                Height = 300;
            }
        }

        public void SaveFile() {
            if (!fileLoaded) {
                SaveAsFile("Save file as");
            } else {
                Globals.Serializer.SerializeObject(fileName, TST);
                ChangeFilenameSaved();
            }
            // Also syncs the txt file
            if (!saveCancel) {
                if (TST.SyncAutoSave) {
                    Globals.SyncTxt();
                }
            }
        }

        public void SaveAsFile(string title) {
            saveFileDialog.Filter = "TextSplit Text File|*.tst";
            saveFileDialog.Title = title;
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel && saveFileDialog.FileName != "") {
                fileName = saveFileDialog.FileName;
                Globals.Serializer.SerializeObject(fileName, TST);
                FileChangeActions();
                saveCancel = false;
            } else {
                saveCancel = true;
            }
        }

        public void OpenFile(string title) {
            CheckSaveChange(); // Saves the old file if it has any changes

            openFileDialog.Filter = "TextSplit Text File|*.tst";
            openFileDialog.Title = title;
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "") {
                fileName = openFileDialog.FileName;
                TST = Globals.Serializer.DeSerializeObject(fileName);
                FileChangeActions();
                Focus();
            }
        }

        public void OpenFileNewWindow(string title) {
            openFileDialog.Filter = "TextSplit Text File|*.tst";
            openFileDialog.Title = title;
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "") {
                string newFileName = openFileDialog.FileName;
                TextSplitShow TSS = new TextSplitShow(newFileName, true);
                TSS.TST = Globals.Serializer.DeSerializeObject(newFileName);
                Globals.OpenNewWindow(TSS);
            }
        }

        // All actions to currentWindow after a new file has been opened in that window
        public void FileChangeActions() {
            currentSlide = 0;
            DisplaySlide();
            Globals.UpdateSlideInfo();
            ChangeFilenameSaved();
            fileLoaded = true;
            int index = Globals.WindowList.IndexOf(this);
            Properties.Settings.Default.FileNames[index] = fileName;
            Globals.ShowPropertiesFileNames();
        }

        public void GoToNext() {
            DetectTextChange();
            if (Properties.Settings.Default.Continuous) {
                currentSlide = (currentSlide + 1) % TST.TextList.Count;
            } else {
                if (currentSlide < TST.TextList.Count - 1) {
                    currentSlide += 1;
                }
            }
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        public void GoToPrev() {
            DetectTextChange();
            if (Properties.Settings.Default.Continuous) {
                currentSlide = (currentSlide + TST.TextList.Count - 1) % TST.TextList.Count;
            } else {
                if (currentSlide > 0) {
                    currentSlide -= 1;
                }
            }
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        public void GoToFirst() {
            DetectTextChange();
            currentSlide = 0;
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        public void GoToLast() {
            DetectTextChange();
            currentSlide = TST.TextList.Count - 1;
            DisplaySlide();
            if (Globals.CurrentWindow.Equals(this))
                Globals.UpdateNavigationControls();
        }

        /*
         * Events
         */

        private void TSS_SizeChanged(object sender, EventArgs e) {
            TST.Size[0] = Width - 16; // 16 = total window margins width
            TST.Size[1] = Height - 38; // 38 = total window margins height
            tTextBox.Width = TST.Size[0] - TST.Margins[0] + (Properties.Settings.Default.DisplayVerticalScrollBars > 0 ? 0 : SystemInformation.VerticalScrollBarWidth);
            tTextBox.Height = TST.Size[1] - (TST.Margins[2] + TST.Margins[3]);
            tTextBox.Location = new Point(TST.Margins[0], TST.Margins[2]);
            ChangeFilenameUnsaved();
        }

        private void TSS_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal && Globals.TSM.WindowState == FormWindowState.Minimized) {
                Globals.TSM.WindowState = FormWindowState.Normal;
            }
        }

        private void tTextBox_GotFocus(object sender, EventArgs e) {
            if (!Globals.CurrentWindow.Equals(this)) {
                Globals.CurrentWindow = this;
                Globals.UpdateSlideInfo();
            }
            if (Properties.Settings.Default.ReadOnly) {
                lFocusHandler.Focus();
            }
        }

        private void tTextBox_MouseEnter(object sender, EventArgs e) {
            if (Properties.Settings.Default.ReadOnly) {
                tTextBox.Cursor = Cursors.Arrow;
            } else {
                tTextBox.Cursor = Cursors.IBeam;
            }
        }

        private void tTextBox_Unfocus(object sender, EventArgs e) {
            DetectTextChange();
        }

        private void TSS_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (!Globals.CloseWindow(this, false)) { // If the window is the last window, then close main as well
                Globals.TSM.Close();
            }
        }
    }
}
>>>>>>> origin/master
