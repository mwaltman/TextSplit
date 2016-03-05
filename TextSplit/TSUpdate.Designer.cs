namespace TextSplit
{
    partial class TextSplitUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pProgressBar
            // 
            this.pProgressBar.Location = new System.Drawing.Point(13, 13);
            this.pProgressBar.Name = "pProgressBar";
            this.pProgressBar.Size = new System.Drawing.Size(259, 23);
            this.pProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pProgressBar.TabIndex = 0;
            // 
            // TextSplitUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 49);
            this.Controls.Add(this.pProgressBar);
            this.Name = "TextSplitUpdate";
            this.Text = "TextSplit update";
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ProgressBar pProgressBar;
    }
}