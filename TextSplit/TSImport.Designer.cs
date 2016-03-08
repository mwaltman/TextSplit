namespace TextSplit
{
    partial class TextSplitImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSplitImport));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bBrowseImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tBrowseImport = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.bBrowseExport = new System.Windows.Forms.Button();
            this.tBrowseExport = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tDelimiter = new System.Windows.Forms.TextBox();
            this.cSaveAsNewTST = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bBrowseImport
            // 
            this.bBrowseImport.Location = new System.Drawing.Point(267, 13);
            this.bBrowseImport.Name = "bBrowseImport";
            this.bBrowseImport.Size = new System.Drawing.Size(75, 23);
            this.bBrowseImport.TabIndex = 2;
            this.bBrowseImport.Text = "Browse...";
            this.bBrowseImport.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Slide delimiter";
            // 
            // tBrowseImport
            // 
            this.tBrowseImport.Location = new System.Drawing.Point(102, 15);
            this.tBrowseImport.Name = "tBrowseImport";
            this.tBrowseImport.ReadOnly = true;
            this.tBrowseImport.Size = new System.Drawing.Size(159, 20);
            this.tBrowseImport.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Import txt file";
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(267, 172);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Enabled = false;
            this.bOK.Location = new System.Drawing.Point(223, 172);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(38, 23);
            this.bOK.TabIndex = 9;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bBrowseExport
            // 
            this.bBrowseExport.Enabled = false;
            this.bBrowseExport.Location = new System.Drawing.Point(267, 64);
            this.bBrowseExport.Name = "bBrowseExport";
            this.bBrowseExport.Size = new System.Drawing.Size(75, 23);
            this.bBrowseExport.TabIndex = 6;
            this.bBrowseExport.Text = "Browse...";
            this.bBrowseExport.UseVisualStyleBackColor = true;
            // 
            // tBrowseExport
            // 
            this.tBrowseExport.Enabled = false;
            this.tBrowseExport.Location = new System.Drawing.Point(102, 66);
            this.tBrowseExport.Name = "tBrowseExport";
            this.tBrowseExport.ReadOnly = true;
            this.tBrowseExport.Size = new System.Drawing.Size(159, 20);
            this.tBrowseExport.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Export tst file";
            // 
            // tDelimiter
            // 
            this.tDelimiter.Location = new System.Drawing.Point(102, 93);
            this.tDelimiter.Multiline = true;
            this.tDelimiter.Name = "tDelimiter";
            this.tDelimiter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tDelimiter.Size = new System.Drawing.Size(240, 66);
            this.tDelimiter.TabIndex = 8;
            // 
            // cSaveAsNewTST
            // 
            this.cSaveAsNewTST.AutoSize = true;
            this.cSaveAsNewTST.Location = new System.Drawing.Point(15, 42);
            this.cSaveAsNewTST.Name = "cSaveAsNewTST";
            this.cSaveAsNewTST.Size = new System.Drawing.Size(118, 17);
            this.cSaveAsNewTST.TabIndex = 3;
            this.cSaveAsNewTST.Text = "Save as new tst file";
            this.cSaveAsNewTST.UseVisualStyleBackColor = true;
            // 
            // TextSplitImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 207);
            this.Controls.Add(this.cSaveAsNewTST);
            this.Controls.Add(this.tDelimiter);
            this.Controls.Add(this.bBrowseExport);
            this.Controls.Add(this.tBrowseExport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bBrowseImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBrowseImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextSplitImport";
            this.Text = "Import from txt file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button bBrowseImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBrowseImport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bBrowseExport;
        private System.Windows.Forms.TextBox tBrowseExport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox tDelimiter;
        private System.Windows.Forms.CheckBox cSaveAsNewTST;
    }
}