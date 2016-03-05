using System;
using System.Windows.Forms;
using System.IO;

namespace TextSplit
{
    public partial class TextSplitImport : Form
    {
        private string tempDelimiter;

        public TextSplitImport() {
            InitializeComponent();

            bOK.Click += new EventHandler(bOK_Click);
            bCancel.Click += new EventHandler(bCancel_Click);
            FormClosing += new FormClosingEventHandler(TSImport_Closing);
            bBrowseImport.Click += new EventHandler(bBrowseImport_Click);
            bBrowseExport.Click += new EventHandler(bBrowseExport_Click);
            tDelimiter.LostFocus += new EventHandler(tDelimiter_LostFocus);
            cSaveAsNewTST.CheckedChanged += new EventHandler(cSaveAsNewTST_CheckedChanged);

            tDelimiter.Text = Properties.Settings.Default.DefaultDelimiter;
        }

        private void cSaveAsNewTST_CheckedChanged(object sender, EventArgs e) {
            label3.Enabled = cSaveAsNewTST.Checked;
            tBrowseExport.Enabled = cSaveAsNewTST.Checked;
            bBrowseExport.Enabled = cSaveAsNewTST.Checked;
        }

        private void tDelimiter_LostFocus(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tDelimiter.Text)) {
                Globals.ShowErrorMessage("Cannot change the delimiter text. The delimiter cannot be empty or consist of only white spaces.");
                tDelimiter.Text = tempDelimiter;
            } else {
                tempDelimiter = tDelimiter.Text;
            }
        }

        private void EnableImport() {
            try {
                if (cSaveAsNewTST.Checked) {
                    bOK.Enabled = Directory.Exists(Path.GetDirectoryName(tBrowseExport.Text)) && Directory.Exists(Path.GetDirectoryName(tBrowseImport.Text));
                } else {
                    bOK.Enabled = Directory.Exists(Path.GetDirectoryName(tBrowseImport.Text));
                }
            } catch (ArgumentException) {
                bOK.Enabled = false;
            }
        }

        private void bBrowseExport_Click(object sender, EventArgs e) {
            saveFileDialog.Filter = "TextSplit Text File|*.tst";
            saveFileDialog.Title = "Select export txt file";
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel && saveFileDialog.FileName != "") {
                tBrowseExport.Text = saveFileDialog.FileName;
            }
            EnableImport();
        }

        private void bBrowseImport_Click(object sender, EventArgs e) {
            openFileDialog.Filter = "Text File|*.txt";
            openFileDialog.Title = "Select import txt file";
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "") {
                tBrowseImport.Text = openFileDialog.FileName;
            }
            EnableImport();
        }

        private void bCancel_Click(object sender, EventArgs e) {
        }

        private void TSImport_Closing(object sender, EventArgs e) {
            if (!bOK.Focused && !bCancel.Focused) {
                bCancel.PerformClick();
            }
            Properties.Settings.Default.DefaultDelimiter = tDelimiter.Text;
        }

        private void bOK_Click(object sender, EventArgs e) {
            if (cSaveAsNewTST.Checked) {
                Globals.ImportFromTxt(tBrowseImport.Text, tBrowseExport.Text, tDelimiter.Text, true);
                Close();
            } else {
                // Display "Are you sure that you want to overwrite the current slides" dialog
                DialogResult result = MessageBox.Show("You are about to overwrite the slide contents of " + Globals.CurrentWindow.fileName + ". Are you sure?", "Overwrite current slides?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    Globals.ImportFromTxt(tBrowseImport.Text, tBrowseExport.Text, tDelimiter.Text, false);
                    Close();
                }
            }
        }
    }
}
