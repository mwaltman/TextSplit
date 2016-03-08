using System;
using System.Windows.Forms;

namespace TextSplit
{
    public partial class TextSplitAbout : Form
    {
        public TextSplitAbout() {
            InitializeComponent();

            button1.Click += new EventHandler(button1_Click);
            label2.Text = "TextSplit v" + Globals.VersionNumber + "\n\nCopyright © 2016 Marijn Waltman";
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
