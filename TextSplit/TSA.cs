using System;
using System.Windows.Forms;

namespace TextSplit
{
    // The window showing information about TextSplit
    public partial class TextSplitAbout : Form
    {
        public TextSplitAbout() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
