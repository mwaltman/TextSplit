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
        public TextSplitShow(string fileName) {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TSS_Closing);
            this.tTextBox.GotFocus += new System.EventHandler(this.tTextBox_GotFocus);
            this.tTextBox.LostFocus += new System.EventHandler(this.tTextBox_Unfocus);
            this.tTextBox.MouseEnter += new System.EventHandler(this.tTextBox_MouseEnter);
            this.SizeChanged += new System.EventHandler(this.TSS_SizeChanged);
            this.Resize += new System.EventHandler(this.TSS_Resize);

            currentSlide = 0;
            fileLoaded = false;
            saveChanged = false;
            this.fileName = fileName;
            TST = new TextSplitText();
            Globals.currentWindow = this;
            Globals.windowList.Add(this);

            serializer = new Serializer();
            try {
                TST = serializer.DeSerializeObject(fileName);
                FileChangeActions();
            } catch (Exception) {
                this.fileName = "Example";
                TST.Empty("Hello World!");

                ChangeFilenameSaved();
                DisplaySlide();
                Globals.UpdateSlideInfo();
                Globals.ChangeDisableHK();
                Globals.ChangeReadOnly();
                if (Properties.Settings.Default.FileName.Count < Globals.windowList.Count) {
                    Properties.Settings.Default.FileName.Add(fileName);
                }
                Properties.Settings.Default.FileName[Globals.windowList.IndexOf(this)] = "";
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
            if (this.BackColor != bgColor) {
                this.BackColor = bgColor;
            }
            if (tTextBox.ForeColor != textColor) {
                tTextBox.ForeColor = textColor;
            }
            if (this.Size != new Size(size[0] + 16, size[1] + 38)) {
                this.Size = new Size(size[0] + 16, size[1] + 38);
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
            if ((string)TST.TextList[currentSlide] != this.tTextBox.Text) {
                TST.TextList[currentSlide] = this.tTextBox.Text;
                ChangeFilenameUnsaved();
            }
        }

        public void ChangeFilenameSaved() {
            this.Text = Path.GetFileNameWithoutExtension(fileName) + " - TextSplit";
            saveChanged = false;
        }

        public void ChangeFilenameUnsaved() {
            this.Text = Path.GetFileNameWithoutExtension(fileName) + "* - TextSplit";
            saveChanged = true;
        }

        public void CheckSaveChange() {
            if (fileLoaded && saveChanged) {
                if (MessageBox.Show("Save changes to " + Path.GetFileNameWithoutExtension(fileName) + ".tst first?", "Save changes?", MessageBoxButtons.OKCancel) == DialogResult.OK) {
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
            TST.Empty("Hello World!");
            SaveAsFile("New File");
            if (saveCancel) {
                TST = TST_temp;
            } else {
                this.Width = 300;
                this.Height = 300;
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
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "") {
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
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "") {
                fileName = openFileDialog.FileName;
                TST = serializer.DeSerializeObject(fileName);
                FileChangeActions();
            }
        }

        public void OpenFileNewWindow(string title) {
            openFileDialog.Filter = "TextSplit Text File|*.tst";
            openFileDialog.Title = title;
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "") {
                fileName = openFileDialog.FileName;
                TST = serializer.DeSerializeObject(fileName);
                TextSplitShow TSS = new TextSplitShow(fileName);
                //Globals.windowList.Add(TSS);
                //Properties.Settings.Default.FileName.Add(TSS.fileName);
                Globals.OpenNewWindow(TSS);
            }
        }

        // All actions to currentWindow after a new file has been opened in that window
        public void FileChangeActions() {
            currentSlide = 0;
            DisplaySlide();
            Globals.UpdateSlideInfo();
            Globals.ChangeReadOnly();
            Globals.ChangeDisableHK();
            ChangeFilenameSaved();
            fileLoaded = true;
            Properties.Settings.Default.FileName[Globals.windowList.IndexOf(this)] = fileName;
            Globals.ShowPropertiesFileName();
        }

        /*
         * Events
         */

        private void TSS_SizeChanged(object sender, EventArgs e) {
            TST.Size[1] = this.Height - 38;
            TST.Size[0] = this.Width - 16;
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
            Globals.currentWindow = this;
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
            if (Globals.windowList.Count == 1) // If the window is the last window
            {
                Globals.TSM.Close();
            } else {
                Globals.CloseWindow(this);
            }
        }
    }
}
