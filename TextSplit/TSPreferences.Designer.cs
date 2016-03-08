namespace TextSplit
{
    partial class TextSplitPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSplitPreferences));
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.cContinuous = new System.Windows.Forms.CheckBox();
            this.cNavigateAll = new System.Windows.Forms.CheckBox();
            this.cNavigationWindowAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.cDisplayVerticalScrollbars = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(246, 118);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(202, 118);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(38, 23);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // cContinuous
            // 
            this.cContinuous.AutoSize = true;
            this.cContinuous.Location = new System.Drawing.Point(12, 58);
            this.cContinuous.Name = "cContinuous";
            this.cContinuous.Size = new System.Drawing.Size(128, 17);
            this.cContinuous.TabIndex = 2;
            this.cContinuous.Text = "Continuous slideshow";
            this.cContinuous.UseVisualStyleBackColor = true;
            // 
            // cNavigateAll
            // 
            this.cNavigateAll.AutoSize = true;
            this.cNavigateAll.Location = new System.Drawing.Point(12, 12);
            this.cNavigateAll.Name = "cNavigateAll";
            this.cNavigateAll.Size = new System.Drawing.Size(278, 17);
            this.cNavigateAll.TabIndex = 0;
            this.cNavigateAll.Text = "Navigation controls affect all opened display windows";
            this.cNavigateAll.UseVisualStyleBackColor = true;
            // 
            // cNavigationWindowAlwaysOnTop
            // 
            this.cNavigationWindowAlwaysOnTop.AutoSize = true;
            this.cNavigationWindowAlwaysOnTop.Location = new System.Drawing.Point(12, 35);
            this.cNavigationWindowAlwaysOnTop.Name = "cNavigationWindowAlwaysOnTop";
            this.cNavigationWindowAlwaysOnTop.Size = new System.Drawing.Size(194, 17);
            this.cNavigationWindowAlwaysOnTop.TabIndex = 1;
            this.cNavigationWindowAlwaysOnTop.Text = "Navigation window is always on top";
            this.cNavigationWindowAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // cDisplayVerticalScrollbars
            // 
            this.cDisplayVerticalScrollbars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cDisplayVerticalScrollbars.FormattingEnabled = true;
            this.cDisplayVerticalScrollbars.Location = new System.Drawing.Point(167, 81);
            this.cDisplayVerticalScrollbars.Name = "cDisplayVerticalScrollbars";
            this.cDisplayVerticalScrollbars.Size = new System.Drawing.Size(154, 21);
            this.cDisplayVerticalScrollbars.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Display vertical scrollbars";
            // 
            // TextSplitPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 155);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cDisplayVerticalScrollbars);
            this.Controls.Add(this.cContinuous);
            this.Controls.Add(this.cNavigateAll);
            this.Controls.Add(this.cNavigationWindowAlwaysOnTop);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextSplitPreferences";
            this.Text = "Edit preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.CheckBox cContinuous;
        private System.Windows.Forms.CheckBox cNavigateAll;
        private System.Windows.Forms.CheckBox cNavigationWindowAlwaysOnTop;
        private System.Windows.Forms.ComboBox cDisplayVerticalScrollbars;
        private System.Windows.Forms.Label label1;
    }
}