using System;
using System.IO;
using System.Windows.Forms;

namespace TextSplit
{
    public partial class TextSplitSync : Form
    {
        private string tempDelimiter;

        public TextSplitSync() {
            InitializeComponent();

            bOK.Click += new EventHandler(bOK_Click);
            bCancel.Click += new EventHandler(bCancel_Click);
            bBrowseSync.Click += new EventHandler(bBrowseSync_Click);
            Load += new EventHandler(TSSettings_Load);
            cSync.CheckedChanged += new EventHandler(cSync_Changed);
            tDelimiter.LostFocus += new EventHandler(tDelimiter_LostFocus);

            cSync.Checked = Globals.CurrentWindow.TST.SyncTxtPath != null;
            tBrowseSync.Text = Globals.CurrentWindow.TST.SyncTxtPath;
            tDelimiter.Text = Globals.CurrentWindow.TST.SyncDelimiterText;
            cAutoSync.Checked = Globals.CurrentWindow.TST.SyncAutoSave;
            bOK.Enabled = cSync.Checked;

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

        private void EnableSync() {
            try {
                bOK.Enabled = Directory.Exists(Path.GetDirectoryName(tBrowseSync.Text));
            } catch (ArgumentException) {
                bOK.Enabled = false;
            }
        }

        private void cSync_Changed(object sender, EventArgs e) {
            groupBox1.Enabled = cSync.Checked;
        }

        private void TSSettings_Load(object sender, EventArgs e) {
            // Disables sync components when Sync with txt file is not checked
            groupBox1.Enabled = cSync.Checked;
        }

        private void bBrowseSync_Click(object sender, EventArgs e) {
            openFileDialog.Filter = "Text File|*.txt";
            openFileDialog.Title = "Select import txt file";
            if (openFileDialog.ShowDialog() != DialogResult.Cancel && openFileDialog.FileName != "") {
                tBrowseSync.Text = openFileDialog.FileName;
            }
            EnableSync();
        }

        private void bCancel_Click(object sender, EventArgs e) {
        }

        private void TSL_Closing(object sender, EventArgs e) {
            if (!bOK.Focused && !bCancel.Focused) {
                bCancel.PerformClick();
            }
        }

        private void bOK_Click(object sender, EventArgs e) {
            if (cSync.Checked) {
                Globals.CurrentWindow.TST.SyncTxtPath = tBrowseSync.Text;
            } else {
                Globals.CurrentWindow.TST.SyncTxtPath = null;
            }
            Globals.CurrentWindow.TST.SyncDelimiterText = tDelimiter.Text;
            Globals.CurrentWindow.TST.SyncAutoSave = cAutoSync.Checked;
            Globals.TSM.bSyncTst.Enabled = cSync.Checked;
            Globals.TSM.bSyncTxt.Enabled = cSync.Checked;
            Globals.UpdateUpdatedIcon();
            Close();
        }
    }
}
