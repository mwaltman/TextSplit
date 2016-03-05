using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextSplit
{
    // The window that lets you change the layout
    public partial class TextSplitLayout : Form
    {
        private TextSplitShow TSS;
        private Font tempFont;
        private Font oldFont;
        private Color[] oldColors;
        private int[] oldSize;
        private int[] oldMargins;

        public TextSplitLayout(TextSplitShow TSS) {
            InitializeComponent();
            ShowIcon = true;

            bTextColor.Click += new EventHandler(bTextColor_Click);
            bBGColor.Click += new EventHandler(bBGColor_Click);
            bOK.Click += new EventHandler(bOK_Click);
            bCancel.Click += new EventHandler(bCancel_Click);
            bChooseFont.Click += new EventHandler(bChooseFont_Click);
            FormClosing += new FormClosingEventHandler(TSL_Closing);

            this.TSS = TSS;

            tWidth.Maximum = Screen.PrimaryScreen.Bounds.Width;
            tHeight.Maximum = Screen.PrimaryScreen.Bounds.Height;
            tLeft.Maximum = Screen.PrimaryScreen.Bounds.Width;
            tRight.Maximum = Screen.PrimaryScreen.Bounds.Width;
            tTop.Maximum = Screen.PrimaryScreen.Bounds.Height;
            tBottom.Maximum = Screen.PrimaryScreen.Bounds.Height;

            tempFont = new Font(TSS.TST.TextFont, TSS.TST.TextFont.Style);
            oldFont = new Font(TSS.TST.TextFont, TSS.TST.TextFont.Style); ;
            oldColors = new Color[] { TSS.TST.Colors[0], TSS.TST.Colors[1] };
            oldSize = new int[] { TSS.TST.Size[0], TSS.TST.Size[1] };
            oldMargins = new int[] { TSS.TST.Margins[0], TSS.TST.Margins[1], TSS.TST.Margins[2], TSS.TST.Margins[3] };

            bTextColor.BackColor = TSS.TST.Colors[0];
            bBGColor.BackColor = TSS.TST.Colors[1];
            tWidth.Value = TSS.TST.Size[0];
            tHeight.Value = TSS.TST.Size[1];
            tLeft.Value = TSS.TST.Margins[0];
            tRight.Value = TSS.TST.Margins[1];
            tTop.Value = TSS.TST.Margins[2];
            tBottom.Value = TSS.TST.Margins[3];

            tWidth.Minimum = 137;
            tHeight.Minimum = 1;

            NumericUpDown[] numericUpDownList = new NumericUpDown[] { tWidth, tHeight, tLeft, tRight, tTop, tBottom };
            for (int i = 0; i < numericUpDownList.Length; i++) {
                numericUpDownList[i].ValueChanged += new EventHandler(NumericUpDown_ValueChanged);
            }
        }

        private void PreviewLayoutSettings() {
            TSS.DisplaySlide(tempFont, bTextColor.BackColor, bBGColor.BackColor,
                new int[] { (int)tWidth.Value, (int)tHeight.Value },
                new int[] { (int)tLeft.Value, (int)tRight.Value, (int)tTop.Value, (int)tBottom.Value });
        }

        /*
         * Events
         */

        private void bCancel_Click(object sender, EventArgs e) {
            TSS.TST.TextFont = oldFont;
            TSS.TST.Colors = oldColors;
            TSS.TST.Size = oldSize;
            TSS.TST.Margins = oldMargins;
            TSS.DisplaySlide();
        }

        private void TSL_Closing(object sender, EventArgs e) {
            if (!bOK.Focused && !bCancel.Focused) {
                bCancel.PerformClick();
            }
            tempFont.Dispose();
            oldFont.Dispose();
        }

        private void bOK_Click(object sender, EventArgs e) {
            TSS.TST.TextFont = tempFont;
            TSS.TST.Colors[0] = bTextColor.BackColor;
            TSS.TST.Colors[1] = bBGColor.BackColor;
            TSS.TST.Size = new int[] { (int)tWidth.Value, (int)tHeight.Value };
            TSS.TST.Margins = new int[] { (int)tLeft.Value, (int)tRight.Value, (int)tTop.Value, (int)tBottom.Value };
            TSS.DisplaySlide();
            TSS.ChangeFilenameUnsaved();
            Close();
        }

        private void bChooseFont_Click(object sender, EventArgs e) {
            fontDialog.Font = tempFont;
            fontDialog.ShowDialog();
            tempFont = fontDialog.Font;
            PreviewLayoutSettings();
        }

        private void bTextColor_Click(object sender, EventArgs e) {
            colorDialog.Color = bTextColor.BackColor;
            colorDialog.ShowDialog();
            bTextColor.BackColor = colorDialog.Color;
            PreviewLayoutSettings();
        }

        private void bBGColor_Click(object sender, EventArgs e) {
            colorDialog.Color = bBGColor.BackColor;
            colorDialog.ShowDialog();
            bBGColor.BackColor = colorDialog.Color;
            PreviewLayoutSettings();
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e) {
            PreviewLayoutSettings();
        }
    }
}
