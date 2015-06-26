namespace TextSplit
{
    partial class TextSplitShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSplitShow));
            this.tTextBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lFocusHandler = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tTextBox
            // 
            this.tTextBox.BackColor = System.Drawing.Color.White;
            this.tTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tTextBox.ForeColor = System.Drawing.Color.Black;
            this.tTextBox.Location = new System.Drawing.Point(0, 0);
            this.tTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.tTextBox.Name = "tTextBox";
            this.tTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tTextBox.Size = new System.Drawing.Size(284, 262);
            this.tTextBox.TabIndex = 0;
            this.tTextBox.Text = "";
            // 
            // lFocusHandler
            // 
            this.lFocusHandler.AutoSize = true;
            this.lFocusHandler.Location = new System.Drawing.Point(16, 13);
            this.lFocusHandler.Name = "lFocusHandler";
            this.lFocusHandler.Size = new System.Drawing.Size(0, 15);
            this.lFocusHandler.TabIndex = 1;
            // 
            // TextSplitShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lFocusHandler);
            this.Controls.Add(this.tTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextSplitShow";
            this.Text = "TextSplit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox tTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label lFocusHandler;
    }
}