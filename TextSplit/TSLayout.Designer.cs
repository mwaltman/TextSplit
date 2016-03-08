namespace TextSplit
{
    partial class TextSplitLayout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSplitLayout));
            this.label2 = new System.Windows.Forms.Label();
            this.bTextColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bBGColor = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tHeight = new System.Windows.Forms.NumericUpDown();
            this.tWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tBottom = new System.Windows.Forms.NumericUpDown();
            this.tTop = new System.Windows.Forms.NumericUpDown();
            this.tRight = new System.Windows.Forms.NumericUpDown();
            this.tLeft = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bChooseFont = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Text color";
            // 
            // bTextColor
            // 
            this.bTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTextColor.Location = new System.Drawing.Point(136, 19);
            this.bTextColor.Name = "bTextColor";
            this.bTextColor.Size = new System.Drawing.Size(23, 23);
            this.bTextColor.TabIndex = 1;
            this.bTextColor.Text = " ";
            this.bTextColor.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Background color";
            // 
            // bBGColor
            // 
            this.bBGColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBGColor.Location = new System.Drawing.Point(136, 47);
            this.bBGColor.Name = "bBGColor";
            this.bBGColor.Size = new System.Drawing.Size(23, 23);
            this.bBGColor.TabIndex = 3;
            this.bBGColor.Text = " ";
            this.bBGColor.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Height";
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(58, 340);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(38, 23);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(102, 340);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 6;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tHeight);
            this.groupBox1.Controls.Add(this.tWidth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inner window size (pixels)";
            // 
            // tHeight
            // 
            this.tHeight.Location = new System.Drawing.Point(98, 46);
            this.tHeight.Name = "tHeight";
            this.tHeight.Size = new System.Drawing.Size(61, 20);
            this.tHeight.TabIndex = 3;
            // 
            // tWidth
            // 
            this.tWidth.Location = new System.Drawing.Point(98, 20);
            this.tWidth.Name = "tWidth";
            this.tWidth.Size = new System.Drawing.Size(61, 20);
            this.tWidth.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bTextColor);
            this.groupBox2.Controls.Add(this.bBGColor);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Colors";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tBottom);
            this.groupBox3.Controls.Add(this.tTop);
            this.groupBox3.Controls.Add(this.tRight);
            this.groupBox3.Controls.Add(this.tLeft);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 126);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Margins (pixels)";
            // 
            // tBottom
            // 
            this.tBottom.Location = new System.Drawing.Point(98, 98);
            this.tBottom.Name = "tBottom";
            this.tBottom.Size = new System.Drawing.Size(61, 20);
            this.tBottom.TabIndex = 7;
            // 
            // tTop
            // 
            this.tTop.Location = new System.Drawing.Point(98, 72);
            this.tTop.Name = "tTop";
            this.tTop.Size = new System.Drawing.Size(61, 20);
            this.tTop.TabIndex = 5;
            // 
            // tRight
            // 
            this.tRight.Location = new System.Drawing.Point(98, 46);
            this.tRight.Name = "tRight";
            this.tRight.Size = new System.Drawing.Size(61, 20);
            this.tRight.TabIndex = 3;
            // 
            // tLeft
            // 
            this.tLeft.Location = new System.Drawing.Point(98, 20);
            this.tLeft.Name = "tLeft";
            this.tLeft.Size = new System.Drawing.Size(61, 20);
            this.tLeft.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Top";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Bottom";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Left";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Right";
            // 
            // bChooseFont
            // 
            this.bChooseFont.Location = new System.Drawing.Point(102, 12);
            this.bChooseFont.Name = "bChooseFont";
            this.bChooseFont.Size = new System.Drawing.Size(75, 23);
            this.bChooseFont.TabIndex = 1;
            this.bChooseFont.Text = "Choose...";
            this.bChooseFont.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text font";
            // 
            // TextSplitLayout
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(189, 375);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bChooseFont);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextSplitLayout";
            this.Text = "Edit layout";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bTextColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bBGColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown tHeight;
        private System.Windows.Forms.NumericUpDown tWidth;
        private System.Windows.Forms.NumericUpDown tBottom;
        private System.Windows.Forms.NumericUpDown tTop;
        private System.Windows.Forms.NumericUpDown tRight;
        private System.Windows.Forms.NumericUpDown tLeft;
        private System.Windows.Forms.Button bChooseFont;
        private System.Windows.Forms.Label label1;
    }
}