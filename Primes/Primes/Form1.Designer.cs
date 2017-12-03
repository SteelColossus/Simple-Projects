namespace Primes
{
    partial class Form1
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
            this.primeOutputBox = new System.Windows.Forms.ListBox();
            this.calcButton = new System.Windows.Forms.Button();
            this.limitNumericUpDown = new MagnitudeNumericUpDown();
            this.limitLabel = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.totalLabel = new System.Windows.Forms.Label();
            this.totalBox = new System.Windows.Forms.TextBox();
            this.percentLabel = new System.Windows.Forms.Label();
            this.percentBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.limitNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // primeOutputBox
            // 
            this.primeOutputBox.FormattingEnabled = true;
            this.primeOutputBox.Location = new System.Drawing.Point(13, 13);
            this.primeOutputBox.Name = "primeOutputBox";
            this.primeOutputBox.Size = new System.Drawing.Size(210, 186);
            this.primeOutputBox.TabIndex = 0;
            // 
            // calcButton
            // 
            this.calcButton.Location = new System.Drawing.Point(13, 205);
            this.calcButton.Name = "calcButton";
            this.calcButton.Size = new System.Drawing.Size(91, 23);
            this.calcButton.TabIndex = 1;
            this.calcButton.Text = "Calculate";
            this.calcButton.UseVisualStyleBackColor = true;
            this.calcButton.Click += new System.EventHandler(this.calcButton_Click);
            // 
            // limitNumericUpDown
            // 
            this.limitNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.limitNumericUpDown.Location = new System.Drawing.Point(144, 208);
            this.limitNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.limitNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.limitNumericUpDown.Name = "limitNumericUpDown";
            this.limitNumericUpDown.Size = new System.Drawing.Size(79, 20);
            this.limitNumericUpDown.TabIndex = 2;
            this.limitNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // limitLabel
            // 
            this.limitLabel.AutoSize = true;
            this.limitLabel.Location = new System.Drawing.Point(110, 210);
            this.limitLabel.Name = "limitLabel";
            this.limitLabel.Size = new System.Drawing.Size(28, 13);
            this.limitLabel.TabIndex = 3;
            this.limitLabel.Text = "Limit";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.FileName = "primes.txt";
            this.saveFileDialog.Filter = "Text file|*.txt";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Location = new System.Drawing.Point(12, 237);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(31, 13);
            this.totalLabel.TabIndex = 4;
            this.totalLabel.Text = "Total";
            // 
            // totalBox
            // 
            this.totalBox.Location = new System.Drawing.Point(49, 234);
            this.totalBox.Name = "totalBox";
            this.totalBox.ReadOnly = true;
            this.totalBox.Size = new System.Drawing.Size(55, 20);
            this.totalBox.TabIndex = 5;
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Location = new System.Drawing.Point(110, 237);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(62, 13);
            this.percentLabel.TabIndex = 6;
            this.percentLabel.Text = "Percentage";
            // 
            // percentBox
            // 
            this.percentBox.Location = new System.Drawing.Point(178, 234);
            this.percentBox.Name = "percentBox";
            this.percentBox.ReadOnly = true;
            this.percentBox.Size = new System.Drawing.Size(45, 20);
            this.percentBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 260);
            this.Controls.Add(this.percentBox);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.totalBox);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.limitLabel);
            this.Controls.Add(this.limitNumericUpDown);
            this.Controls.Add(this.calcButton);
            this.Controls.Add(this.primeOutputBox);
            this.Name = "Form1";
            this.Text = "Primes";
            ((System.ComponentModel.ISupportInitialize)(this.limitNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox primeOutputBox;
        private System.Windows.Forms.Button calcButton;
        private MagnitudeNumericUpDown limitNumericUpDown;
		private System.Windows.Forms.Label limitLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Label totalLabel;
		private System.Windows.Forms.TextBox totalBox;
		private System.Windows.Forms.Label percentLabel;
		private System.Windows.Forms.TextBox percentBox;
    }
}

