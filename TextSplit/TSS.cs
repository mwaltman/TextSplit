using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace TextSplit
{
    // The window that displays the current slide
    public partial class TextSplitShow : Form
    {
        private Serializer serializer;
        private bool saveCancel;

        public TextSplitText TST;
        public int currentSlide;
        public string fileName;
        public bool fileLoaded;
        public bool saveChanged;

        // Opens a new window and loads the .tst file with the given fileName
        // If the file doesn't exist (e.g. fileName = ""), then it will open the example file
        public TextSplitShow(string fileName, bool addToFileNames) {
            InitializeComponent();

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

            serializer = new Serializer();
            try {
                TST = serializer.DeSerializeObject(fileName);
                FileChangeActions();
            } catch (Exception) {
                this.fileName = "Example";
                TST.Empty();

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
            if (tTextBox.Font != textFont) {
                tTextBox.Font = textFont;
            }
            if (tTextBox.BackColor != bgColor) {
                tTextBox.BackColor = bgColor;
            }
            if (BackColor != bgColor) {
                BackColor = bgColor;
            }
            if (tTextBox.ForeColor != textColor) {
                tTextBox.ForeColor = textColor;
            }
            if (Size != new Size(size[0] + 16, size[1] + 38)) {
                Size = new Size(size[0] + 16, size[1] + 38);
            }
            if (tTextBox.Size != new Size(size[0] - (margins[0] + margins[1]), size[1] - (margins[2] + margins[3]))) {
                tTextBox.Size = new Size(size[0] - (margins[0] + margins[1]), size[1] - (margins[2] + margins[3]));
            }
            if (tTextBox.Location != new Point(margins[0], margins[2])) {
                tTextBox.Location = new Point(margins[0], margins[2]);
            }
            tTextBox.Text = TST.TextList[currentSlide].ToString();
        }

        public void DetectTextChange() {
            if ((string)TST.TextList[currentSlide] != tTextBox.Text) {
                TST.TextList[currentSlide] = tTextBox.Text;
                ChangeFilenameUnsaved();
            }
        }

        public void ChangeFilenameSaved() {
            Text = Path.GetFileNameWithoutExtension(fileName) + " - TextSplit";
            saveChanged = false;
        }

        public void ChangeFilenameUnsaved() {
            Text = Path.GetFileNameWithoutExtension(fileName) + "* - TextSplit";
            saveChanged = true;
        }

        public void CheckSaveChange() {
            if (fileLoaded && saveChanged) {
                if (MessageBox.Show("Save changes to " + Path.GetFileNameWithoutExtension(fileName) + ".tst first?", "Save changes?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
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
            TST.Empty();
            SaveAsFile("New File");
            if (saveCancel) {
                TST = TST_temp;
            } else {
                Width = 300;
                Height = 300;
            }
        }

        public void SaveFile() {
            if (!fileLoaded) {
                SaveAsFile("Save As...");
            } else {
                serializer.SerializeObject(fileName, TST);
                ChangeFilenameSaved();
            }
        }

        public void SaveAsFile(string title) {
            saveFileDialog.Filter = "TextSplit Text File|*.tst";
            saveFileDialog.Title = title;
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel && saveFileDialog.FileName != "") {
                fileName = saveFileDialog.FileName;
                serializer.SerializeObject(fileName, TST);
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
                TST = serializer.DeSerializeObject(fileName);
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
                TSS.TST = serializer.DeSerializeObject(newFileName);
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
            if (TST.SlideWrap) {
                currentSlide = (currentSlide + 1) % TST.TextList.Count;
            } else {
                if (currentSlide < TST.TextList.Count - 1) {
                    currentSlide += 1;
                }
            }
            DisplaySlide();
            if (this == Globals.CurrentWindow) {
                Globals.UpdateSlideInfo();
            }
        }

        public void GoToPrev() {
            DetectTextChange();
            if (TST.SlideWrap) {
                currentSlide = (currentSlide + TST.TextList.Count - 1) % TST.TextList.Count;
            } else {
                if (currentSlide > 0) {
                    currentSlide -= 1;
                }
            }
            DisplaySlide();
            if (this == Globals.CurrentWindow) {
                Globals.UpdateSlideInfo();
            }
        }

        public void GoToFirst() {
            DetectTextChange();
            currentSlide = 0;
            DisplaySlide();
            if (this == Globals.CurrentWindow) {
                Globals.UpdateSlideInfo();
            }
        }

        public void GoToLast() {
            DetectTextChange();
            currentSlide = TST.TextList.Count - 1;
            DisplaySlide();
            if (this == Globals.CurrentWindow) {
                Globals.UpdateSlideInfo();
            }
        }

        /*
         * Events
         */

        private void TSS_SizeChanged(object sender, EventArgs e) {
            TST.Size[1] = Height - 38;
            TST.Size[0] = Width - 16;
            tTextBox.Height = TST.Size[1] - (TST.Margins[2] + TST.Margins[3]);
            tTextBox.Width = TST.Size[0] - (TST.Margins[0] + TST.Margins[1]);
            tTextBox.Location = new Point(TST.Margins[0], TST.Margins[2]);
            ChangeFilenameUnsaved();
        }

        private void TSS_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal && Globals.TSM.WindowState == FormWindowState.Minimized) {
                Globals.TSM.WindowState = FormWindowState.Normal;
            }
        }

        private void tTextBox_GotFocus(object sender, EventArgs e) {
            Globals.CurrentWindow = this;
            Globals.UpdateSlideInfo();
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
