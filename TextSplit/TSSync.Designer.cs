namespace TextSplit
{
    partial class TextSplitSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSplitSync));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.cSync = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tDelimiter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bBrowseSync = new System.Windows.Forms.Button();
            this.tBrowseSync = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cAutoSync = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(273, 185);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(229, 185);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(38, 23);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // cSync
            // 
            this.cSync.AutoSize = true;
            this.cSync.Location = new System.Drawing.Point(12, 12);
            this.cSync.Name = "cSync";
            this.cSync.Size = new System.Drawing.Size(102, 17);
            this.cSync.TabIndex = 0;
            this.cSync.Text = "Sync with txt file";
            this.cSync.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tDelimiter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bBrowseSync);
            this.groupBox1.Controls.Add(this.tBrowseSync);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cAutoSync);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 135);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tDelimiter
            // 
            this.tDelimiter.Location = new System.Drawing.Point(90, 39);
            this.tDelimiter.Multiline = true;
            this.tDelimiter.Name = "tDelimiter";
            this.tDelimiter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tDelimiter.Size = new System.Drawing.Size(240, 66);
            this.tDelimiter.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Slide delimiter";
            // 
            // bBrowseSync
            // 
            this.bBrowseSync.Location = new System.Drawing.Point(255, 10);
            this.bBrowseSync.Name = "bBrowseSync";
            this.bBrowseSync.Size = new System.Drawing.Size(75, 23);
            this.bBrowseSync.TabIndex = 2;
            this.bBrowseSync.Text = "Browse...";
            this.bBrowseSync.UseVisualStyleBackColor = true;
            // 
            // tBrowseSync
            // 
            this.tBrowseSync.Enabled = false;
            this.tBrowseSync.Location = new System.Drawing.Point(90, 12);
            this.tBrowseSync.Name = "tBrowseSync";
            this.tBrowseSync.ReadOnly = true;
            this.tBrowseSync.Size = new System.Drawing.Size(159, 20);
            this.tBrowseSync.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Txt file location";
            // 
            // cAutoSync
            // 
            this.cAutoSync.AutoSize = true;
            this.cAutoSync.Location = new System.Drawing.Point(9, 111);
            this.cAutoSync.Name = "cAutoSync";
            this.cAutoSync.Size = new System.Drawing.Size(128, 17);
            this.cAutoSync.TabIndex = 5;
            this.cAutoSync.Text = "Auto sync txt on save";
            this.cAutoSync.UseVisualStyleBackColor = true;
            // 
            // TextSplitSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 220);
            this.Controls.Add(this.cSync);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextSplitSync";
            this.Text = "Edit sync settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.CheckBox cSync;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bBrowseSync;
        private System.Windows.Forms.TextBox tBrowseSync;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cAutoSync;
        private System.Windows.Forms.TextBox tDelimiter;
    }
}