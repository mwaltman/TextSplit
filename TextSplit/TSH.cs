using System;
using System.Windows.Forms;

namespace TextSplit
{
    // The window that lets you change the global hotkeys
    public partial class TextSplitHotkeys : Form
    {
        private Keys[] newHotkeys;
        private String[] textboxNames;
        private TextSplitShow TSS;

        public TextSplitHotkeys(TextSplitShow TSS) {
            InitializeComponent();

            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            this.bOK.Click += new System.EventHandler(this.bOK_Click);

            this.TSS = TSS;

            textboxNames = new String[] { "tNext1", "tNext2", "tPrev1", "tPrev2", "tFirst1", "tFirst2", "tLast1", "tLast2" };
            newHotkeys = new Keys[8];
            for (int i = 0; i < newHotkeys.Length; i++) {
                newHotkeys[i] = TSS.TST.Hotkeys[i];
            }

            foreach (var c in this.Controls) {
                if (c is System.Windows.Forms.TextBox) {
                    for (int i = 0; i < textboxNames.Length; i++) {
                        if (((TextBox)c).Name == textboxNames[i]) {
                            ((TextBox)c).Text = newHotkeys[i].ToString();
                        }
                    }
                    ((TextBox)c).GotFocus += new EventHandler(this.tKeyPrompt_Focus);
                    ((TextBox)c).LostFocus += new EventHandler(this.tKeyPrompt_LostFocus);
                    ((TextBox)c).KeyDown += new KeyEventHandler(this.tKeyPrompt_Key);
                }
            }
        }

        /*
         * Events
         */

        private void bOK_Click(object sender, EventArgs e) {
            TSS.TST.Hotkeys = newHotkeys;
            TSS.ChangeFilenameUnsaved();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tKeyPrompt_Focus(object sender, EventArgs e) {
            ((TextBox)sender).Text = "Press Key...";
        }

        private void tKeyPrompt_LostFocus(object sender, EventArgs e) {
            int j = Array.IndexOf(textboxNames, ((TextBox)sender).Name);
            ((TextBox)sender).Text = newHotkeys[j].ToString();
        }

        private void tKeyPrompt_Key(object sender, KeyEventArgs e) {
            ((TextBox)sender).ReadOnly = true;

            bool check = false;
            for (int i = 0; i < newHotkeys.Length; i++) {
                if (newHotkeys[i] == e.KeyCode) {
                    check = true;
                }
            }

            if (!check) {
                for (int i = 0; i < textboxNames.Length; i++) {
                    if (((TextBox)sender).Name == textboxNames[i]) {
                        if (e.KeyCode == Keys.Escape) {
                            newHotkeys[i] = Keys.None;
                        } else {
                            newHotkeys[i] = e.KeyCode;
                        }
                    }
                }
            }

            e.SuppressKeyPress = true;
            label1.Focus();
        }
    }
}
