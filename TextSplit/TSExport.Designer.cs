namespace TextSplit
{
    partial class TextSplitExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSplitExport));
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tInfoText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rBelowSlide = new System.Windows.Forms.RadioButton();
            this.rAboveSlide = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bBrowseExport = new System.Windows.Forms.Button();
            this.tBrowseExport = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cDisplaySlideInfo = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cOpenAfterExport = new System.Windows.Forms.CheckBox();
            this.tDelimiter = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(264, 287);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Enabled = false;
            this.bOK.Location = new System.Drawing.Point(220, 287);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(38, 23);
            this.bOK.TabIndex = 8;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tInfoText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rBelowSlide);
            this.groupBox1.Controls.Add(this.rAboveSlide);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 117);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // tInfoText
            // 
            this.tInfoText.Location = new System.Drawing.Point(91, 19);
            this.tInfoText.Multiline = true;
            this.tInfoText.Name = "tInfoText";
            this.tInfoText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tInfoText.Size = new System.Drawing.Size(230, 66);
            this.tInfoText.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Position";
            // 
            // rBelowSlide
            // 
            this.rBelowSlide.AutoSize = true;
            this.rBelowSlide.Location = new System.Drawing.Point(197, 91);
            this.rBelowSlide.Name = "rBelowSlide";
            this.rBelowSlide.Size = new System.Drawing.Size(98, 17);
            this.rBelowSlide.TabIndex = 4;
            this.rBelowSlide.Text = "Below slide text";
            this.rBelowSlide.UseVisualStyleBackColor = true;
            // 
            // rAboveSlide
            // 
            this.rAboveSlide.AutoSize = true;
            this.rAboveSlide.Checked = true;
            this.rAboveSlide.Location = new System.Drawing.Point(91, 91);
            this.rAboveSlide.Name = "rAboveSlide";
            this.rAboveSlide.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rAboveSlide.Size = new System.Drawing.Size(100, 17);
            this.rAboveSlide.TabIndex = 3;
            this.rAboveSlide.TabStop = true;
            this.rAboveSlide.Text = "Above slide text";
            this.rAboveSlide.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Information text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Slide delimiter";
            // 
            // bBrowseExport
            // 
            this.bBrowseExport.Location = new System.Drawing.Point(264, 10);
            this.bBrowseExport.Name = "bBrowseExport";
            this.bBrowseExport.Size = new System.Drawing.Size(75, 23);
            this.bBrowseExport.TabIndex = 2;
            this.bBrowseExport.Text = "Browse...";
            this.bBrowseExport.UseVisualStyleBackColor = true;
            // 
            // tBrowseExport
            // 
            this.tBrowseExport.AcceptsReturn = true;
            this.tBrowseExport.Location = new System.Drawing.Point(99, 12);
            this.tBrowseExport.Name = "tBrowseExport";
            this.tBrowseExport.ReadOnly = true;
            this.tBrowseExport.Size = new System.Drawing.Size(159, 20);
            this.tBrowseExport.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Export txt file";
            // 
            // cDisplaySlideInfo
            // 
            this.cDisplaySlideInfo.AutoSize = true;
            this.cDisplaySlideInfo.Location = new System.Drawing.Point(12, 111);
            this.cDisplaySlideInfo.Name = "cDisplaySlideInfo";
            this.cDisplaySlideInfo.Size = new System.Drawing.Size(138, 17);
            this.cDisplaySlideInfo.TabIndex = 5;
            this.cDisplaySlideInfo.Text = "Display slide information";
            this.cDisplaySlideInfo.UseVisualStyleBackColor = true;
            // 
            // cOpenAfterExport
            // 
            this.cOpenAfterExport.AutoSize = true;
            this.cOpenAfterExport.Checked = true;
            this.cOpenAfterExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cOpenAfterExport.Location = new System.Drawing.Point(12, 257);
            this.cOpenAfterExport.Name = "cOpenAfterExport";
            this.cOpenAfterExport.Size = new System.Drawing.Size(138, 17);
            this.cOpenAfterExport.TabIndex = 7;
            this.cOpenAfterExport.Text = "Open txt file after export";
            this.cOpenAfterExport.UseVisualStyleBackColor = true;
            // 
            // tDelimiter
            // 
            this.tDelimiter.Location = new System.Drawing.Point(99, 39);
            this.tDelimiter.Multiline = true;
            this.tDelimiter.Name = "tDelimiter";
            this.tDelimiter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tDelimiter.Size = new System.Drawing.Size(240, 66);
            this.tDelimiter.TabIndex = 4;
            // 
            // TextSplitExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 322);
            this.Controls.Add(this.tDelimiter);
            this.Controls.Add(this.cOpenAfterExport);
            this.Controls.Add(this.cDisplaySlideInfo);
            this.Controls.Add(this.bBrowseExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBrowseExport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextSplitExport";
            this.Text = "Save as txt file";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bBrowseExport;
        private System.Windows.Forms.TextBox tBrowseExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rBelowSlide;
        private System.Windows.Forms.RadioButton rAboveSlide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cDisplaySlideInfo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox cOpenAfterExport;
        private System.Windows.Forms.TextBox tDelimiter;
        private System.Windows.Forms.TextBox tInfoText;
    }
}