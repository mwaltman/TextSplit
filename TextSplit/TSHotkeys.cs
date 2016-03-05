using System;
using System.Collections;
using System.Windows.Forms;

namespace TextSplit
{
    // The window that lets you change the global hotkeys
    public partial class TextSplitHotkeys : Form
    {
        private ArrayList tempHotkeys;
        private string[] textboxNames;
        private TextSplitShow TSS;

        public TextSplitHotkeys(TextSplitShow TSS) {
            InitializeComponent();

            bCancel.Click += new EventHandler(bCancel_Click);
            bOK.Click += new EventHandler(bOK_Click);
            FormClosing += new FormClosingEventHandler(TSH_Closing);

            this.TSS = TSS;

            textboxNames = new string[] { "tNext1", "tNext2", "tPrev1", "tPrev2", "tFirst1", "tFirst2", "tLast1", "tLast2" };
            tempHotkeys = new ArrayList();
            for (int i = 0; i < Properties.Settings.Default.Hotkeys.Count; i++) {
                tempHotkeys.Add((Keys)Properties.Settings.Default.Hotkeys[i]);
            }

            foreach (var c in Controls) {
                if (c is TextBox) {
                    for (int i = 0; i < textboxNames.Length; i++) {
                        if (((TextBox)c).Name == textboxNames[i]) {
                            ((TextBox)c).Text = ((Keys)tempHotkeys[i]).ToString();
                        }
                    }
                    ((TextBox)c).GotFocus += new EventHandler(tKeyPrompt_Focus);
                    ((TextBox)c).LostFocus += new EventHandler(tKeyPrompt_LostFocus);
                    ((TextBox)c).KeyDown += new KeyEventHandler(tKeyPrompt_Key);
                }
            }
        }

        /*
         * Events
         */

        private void bOK_Click(object sender, EventArgs e) {
            Properties.Settings.Default.Hotkeys = tempHotkeys;
            TSS.ChangeFilenameUnsaved();
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e) {
        }

        private void TSH_Closing(object sender, EventArgs e) {
            if (!bOK.Focused && !bCancel.Focused) {
                bCancel.PerformClick();
            }
        }

        private void tKeyPrompt_Focus(object sender, EventArgs e) {
            ((TextBox)sender).Text = "Press Key...";
        }

        private void tKeyPrompt_LostFocus(object sender, EventArgs e) {
            int j = Array.IndexOf(textboxNames, ((TextBox)sender).Name);
            ((TextBox)sender).Text = ((Keys)tempHotkeys[j]).ToString();
        }

        private void tKeyPrompt_Key(object sender, KeyEventArgs e) {
            ((TextBox)sender).ReadOnly = true;

            bool check = false;
            for (int i = 0; i < tempHotkeys.Count; i++) {
                if ((Keys)tempHotkeys[i] == e.KeyCode) {
                    check = true;
                }
            }

            if (!check) {
                for (int i = 0; i < textboxNames.Length; i++) {
                    if (((TextBox)sender).Name == textboxNames[i]) {
                        if (e.KeyCode == Keys.Escape) {
                            tempHotkeys[i] = Keys.None;
                        } else {
                            tempHotkeys[i] = e.KeyCode;
                        }
                    }
                }
            }

            e.SuppressKeyPress = true;
            label1.Focus();
        }
    }
}
