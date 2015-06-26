using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextSplit
{
    // The window that lets you change the layout
    public partial class TextSplitLayout : Form
    {
        private Font newFont;
        private Color newTextColor;
        private Color newBGColor;
        private int[] newSize;
        private bool newWrap;
        private int[] newMargins;
        private int[] initialSize;
        private int[] initialMargins;
        private int[] maxSizes;
        private TextBox[] textboxList;
        private TextSplitShow TSS;

        public TextSplitLayout(TextSplitShow TSS) {
            InitializeComponent();

            this.bTextColor.Click += new System.EventHandler(this.bTextColor_Click);
            this.bBGColor.Click += new System.EventHandler(this.bBGColor_Click);
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.bChooseFont.Click += new System.EventHandler(this.bChooseFont_Click);

            this.TSS = TSS;

            bTextColor.BackColor = TSS.TST.Colors[0];
            bBGColor.BackColor = TSS.TST.Colors[1];
            checkBox1.Checked = TSS.TST.SlideWrap;

            textboxList = new TextBox[] { tWidth, tHeight, tLeft, tRight, tTop, tBottom };
            maxSizes = new int[] { Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height };

            newFont = TSS.TST.TextFont;
            newTextColor = TSS.TST.Colors[0];
            newBGColor = TSS.TST.Colors[1];
            newWrap = TSS.TST.SlideWrap;
            newSize = (int[])TSS.TST.Size.Clone();
            newMargins = (int[])TSS.TST.Margins.Clone();

            initialSize = (int[])TSS.TST.Size.Clone();
            initialMargins = (int[])TSS.TST.Margins.Clone();

            for (int i = 0; i < textboxList.Length; i++) {
                if (i < 2) {
                    textboxList[i].Text = TSS.TST.Size[i].ToString();
                    textboxList[i].LostFocus += new System.EventHandler(this.TextBoxWindow_Unfocus);
                } else {
                    textboxList[i].Text = TSS.TST.Margins[i - 2].ToString();
                    textboxList[i].LostFocus += new System.EventHandler(this.TextBoxMargin_Unfocus);
                }
                textboxList[i].KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Enter);
            }

            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 10000;
            toolTip.SetToolTip(checkBox1, "If this is checked, then the 'Next Slide' action on the last slide gets you to the first slide and the 'Previous Slide' action on the first slide gets you to the last slide");
        }

        /*
         * Events
         */

        private void bCancel_Click(object sender, EventArgs e) {
            TSS.TST.Size = initialSize;
            TSS.TST.Margins = initialMargins;
            TSS.DisplaySlide();
            this.Close();
        }

        private void bOK_Click(object sender, EventArgs e) {
            TSS.TST.TextFont = newFont;
            TSS.TST.Colors[0] = newTextColor;
            TSS.TST.Colors[1] = newBGColor;
            TSS.TST.Size = newSize;
            TSS.TST.SlideWrap = newWrap;
            TSS.TST.Margins = newMargins;
            TSS.DisplaySlide();
            TSS.ChangeFilenameUnsaved();
            this.Close();
        }

        private void bChooseFont_Click(object sender, EventArgs e) {
            fontDialog.Font = newFont;
            fontDialog.ShowDialog();
            newFont = fontDialog.Font;
            TSS.DisplaySlide(newFont, newTextColor, newBGColor, newSize, newMargins);
        }

        private void bTextColor_Click(object sender, EventArgs e) {
            colorDialog.Color = newTextColor;
            colorDialog.ShowDialog();
            newTextColor = colorDialog.Color;
            bTextColor.BackColor = newTextColor;
            TSS.DisplaySlide(newFont, newTextColor, newBGColor, newSize, newMargins);
        }

        private void bBGColor_Click(object sender, EventArgs e) {
            colorDialog.Color = newBGColor;
            colorDialog.ShowDialog();
            newBGColor = colorDialog.Color;
            bBGColor.BackColor = newBGColor;
            TSS.DisplaySlide(newFont, newTextColor, newBGColor, newSize, newMargins);
        }

        private void TextBox_Enter(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                label4.Focus();
            }
        }

        private void TextBoxWindow_Unfocus(object sender, EventArgs e) {
            int n;
            TextBox senderBox = (TextBox)sender;
            int j = Array.IndexOf(textboxList, senderBox);
            if (!int.TryParse(senderBox.Text, out n)) {
                senderBox.Text = newSize[j].ToString();
            } else {
                int s = Convert.ToInt32(senderBox.Text);
                if (s > maxSizes[j] || s < 0) {
                    senderBox.Text = newSize[j].ToString();
                } else {
                    newSize[j] = s;
                    TSS.DisplaySlide(newFont, newTextColor, newBGColor, newSize, newMargins);
                }
            }
        }

        private void TextBoxMargin_Unfocus(object sender, EventArgs e) {
            int n;
            TextBox senderBox = (TextBox)sender;
            int j = Array.IndexOf(textboxList, senderBox) - 2;
            if (!int.TryParse(senderBox.Text, out n)) {
                senderBox.Text = newMargins[j].ToString();
            } else {
                int s = Convert.ToInt32(senderBox.Text);
                int k, l;
                switch (j) {
                    case 0:
                        k = 1; l = 0;
                        break;
                    case 1:
                        k = 0; l = 0;
                        break;
                    case 2:
                        k = 3; l = 1;
                        break;
                    default:
                        k = 2; l = 1;
                        break;
                }
                if (newSize[l] - newMargins[k] - s < 0 || s < 0) {
                    senderBox.Text = newMargins[j].ToString();
                } else {
                    newMargins[j] = s;
                    TSS.DisplaySlide(newFont, newTextColor, newBGColor, newSize, newMargins);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            newWrap = ((CheckBox)sender).Checked;
        }
    }
}
