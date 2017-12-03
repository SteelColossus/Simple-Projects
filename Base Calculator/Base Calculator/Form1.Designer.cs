namespace Base_Calculator
{
    partial class MainForm
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
			this.baseNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.inputTextBox = new System.Windows.Forms.TextBox();
			this.outputTextBox = new System.Windows.Forms.TextBox();
			this.oppositeBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.baseNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// baseNumericUpDown
			// 
			this.baseNumericUpDown.Location = new System.Drawing.Point(82, 230);
			this.baseNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.baseNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.baseNumericUpDown.Name = "baseNumericUpDown";
			this.baseNumericUpDown.Size = new System.Drawing.Size(45, 20);
			this.baseNumericUpDown.TabIndex = 2;
			this.baseNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.baseNumericUpDown.ValueChanged += new System.EventHandler(this.baseNumericUpDown_ValueChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(13, 13);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.inputTextBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.outputTextBox);
			this.splitContainer1.Size = new System.Drawing.Size(259, 211);
			this.splitContainer1.SplitterDistance = 105;
			this.splitContainer1.TabIndex = 3;
			// 
			// inputTextBox
			// 
			this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputTextBox.Location = new System.Drawing.Point(0, 0);
			this.inputTextBox.Multiline = true;
			this.inputTextBox.Name = "inputTextBox";
			this.inputTextBox.Size = new System.Drawing.Size(259, 105);
			this.inputTextBox.TabIndex = 0;
			this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
			// 
			// outputTextBox
			// 
			this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputTextBox.Location = new System.Drawing.Point(0, 0);
			this.outputTextBox.Multiline = true;
			this.outputTextBox.Name = "outputTextBox";
			this.outputTextBox.ReadOnly = true;
			this.outputTextBox.Size = new System.Drawing.Size(259, 102);
			this.outputTextBox.TabIndex = 0;
			// 
			// oppositeBox
			// 
			this.oppositeBox.AutoSize = true;
			this.oppositeBox.Location = new System.Drawing.Point(162, 231);
			this.oppositeBox.Name = "oppositeBox";
			this.oppositeBox.Size = new System.Drawing.Size(68, 17);
			this.oppositeBox.TabIndex = 5;
			this.oppositeBox.Text = "Opposite";
			this.oppositeBox.UseVisualStyleBackColor = true;
			this.oppositeBox.CheckedChanged += new System.EventHandler(this.oppositeBox_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.oppositeBox);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.baseNumericUpDown);
			this.Name = "MainForm";
			this.Text = "Base Calculator";
			((System.ComponentModel.ISupportInitialize)(this.baseNumericUpDown)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown baseNumericUpDown;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox inputTextBox;
		private System.Windows.Forms.TextBox outputTextBox;
		private System.Windows.Forms.CheckBox oppositeBox;

    }
}

