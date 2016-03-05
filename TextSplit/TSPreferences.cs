using System;
using System.Windows.Forms;

namespace TextSplit
{
    public partial class TextSplitPreferences : Form
    {
        public TextSplitPreferences() {
            InitializeComponent();

            bOK.Click += new EventHandler(bOK_Click);
            bCancel.Click += new EventHandler(bCancel_Click);
            FormClosing += new FormClosingEventHandler(TSImport_Closing);

            cContinuous.Checked = Properties.Settings.Default.Continuous;
            cNavigateAll.Checked = Properties.Settings.Default.NavigateAll;
            cNavigationWindowAlwaysOnTop.Checked = Properties.Settings.Default.NavigationWindowAlwaysOnTop;

            cDisplayVerticalScrollbars.Items.Add("Never");
            cDisplayVerticalScrollbars.Items.Add("Only when necessary");
            cDisplayVerticalScrollbars.Items.Add("Always");
            cDisplayVerticalScrollbars.SelectedIndex = Properties.Settings.Default.DisplayVerticalScrollBars;
        }

        private void bCancel_Click(object sender, EventArgs e) {
        }

        private void TSImport_Closing(object sender, EventArgs e) {
            if (!bOK.Focused && !bCancel.Focused) {
                bCancel.PerformClick();
            }
        }

        private void bOK_Click(object sender, EventArgs e) {
            Properties.Settings.Default.Continuous = cContinuous.Checked;
            Properties.Settings.Default.NavigateAll = cNavigateAll.Checked;
            Properties.Settings.Default.NavigationWindowAlwaysOnTop = cNavigationWindowAlwaysOnTop.Checked;
            Globals.TSM.TopMost = cNavigationWindowAlwaysOnTop.Checked;
            Properties.Settings.Default.DisplayVerticalScrollBars = cDisplayVerticalScrollbars.SelectedIndex;
            foreach (TextSplitShow TSS in Globals.WindowList) {
                // I'd rather have this be Vertical than ForcedVertical, but there is the issue with Resizing. May change this in v2.1
                if (cDisplayVerticalScrollbars.SelectedIndex == 0)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                if (cDisplayVerticalScrollbars.SelectedIndex == 1)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
                if (cDisplayVerticalScrollbars.SelectedIndex == 2)
                    TSS.tTextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                TSS.DisplaySlide();
            }
            Close();
        }
    }
}
