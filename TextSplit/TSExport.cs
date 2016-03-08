using System;
using System.Windows.Forms;
using System.IO;

namespace TextSplit
{
    public partial class TextSplitExport : Form
    {
        private string tempDelimiter;

        public TextSplitExport() {
            InitializeComponent();

            bOK.Click += new EventHandler(bOK_Click);
            bCancel.Click += new EventHandler(bCancel_Click);
            bBrowseExport.Click += new EventHandler(bBrowseExport_Click);
            Load += new EventHandler(TSSettings_Load);
            cDisplaySlideInfo.CheckedChanged += new EventHandler(cDisplaySlideInfo_Changed);
            FormClosing += new FormClosingEventHandler(TSExport_Closing);
            tDelimiter.LostFocus += new EventHandler(tDelimiter_LostFocus);

            tDelimiter.Text = Properties.Settings.Default.DefaultDelimiter;
            tInfoText.Text = Properties.Settings.Default.DefaultInfoText;

            Globals.ToolTip.SetToolTip(tInfoText, "Sets the slide info text that is shown for each slide in the txt file. Use $C$ to refer to the current slide index and $T$ for the total number of slides");

            tempDelimiter = tDelimiter.Text;
        }

        private void tDelimiter_LostFocus(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tDelimiter.Text)) {
                Globals.ShowErrorMessage("Cannot change the delimiter text. The delimiter cannot be empty or consist of only white spaces.");
                tDelimiter.Text = tempDelimiter;
            } else {
                tempDelimiter = tDelimiter.Text;
            }
        }

        private void EnableExport() {
            try {
                bOK.Enabled = Directory.Exists(Path.GetDirectoryName(tBrowseExport.Text));
            } catch (ArgumentException) {
                bOK.Enabled = false;
            }
        }

        private void bBrowseExport_Click(object sender, EventArgs e) {
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Select export txt file";
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel && saveFileDialog.FileName != "") {
                tBrowseExport.Text = saveFileDialog.FileName;
            }
            EnableExport();
        }

        private void cDisplaySlideInfo_Changed(object sender, EventArgs e) {
            groupBox1.Enabled = cDisplaySlideInfo.Checked;
        }

        private void TSSettings_Load(object sender, EventArgs e) {
            // Disables sync components when Sync with txt file is not checked
            groupBox1.Enabled = cDisplaySlideInfo.Checked;
        }

        private void bCancel_Click(object sender, EventArgs e) {
        }

        private void TSExport_Closing(object sender, EventArgs e) {
            if (!bOK.Focused && !bCancel.Focused) {
                bCancel.PerformClick();
            }
            Properties.Settings.Default.DefaultDelimiter = tDelimiter.Text;
            Properties.Settings.Default.DefaultInfoText = tInfoText.Text;
        }

        private void bOK_Click(object sender, EventArgs e) {
            Globals.ExportToTxt(tBrowseExport.Text, tDelimiter.Text, tInfoText.Text, cDisplaySlideInfo.Checked, rAboveSlide.Checked);
            
            // Opens the newly created text file
            if (cOpenAfterExport.Checked) {
                System.Diagnostics.Process.Start(@tBrowseExport.Text);
            }

            Close();
        }
    }
}
