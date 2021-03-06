﻿namespace Word_Generator
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
            this.randomWordButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openDictionaryButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // randomWordButton
            // 
            this.randomWordButton.Location = new System.Drawing.Point(11, 97);
            this.randomWordButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.randomWordButton.Name = "randomWordButton";
            this.randomWordButton.Size = new System.Drawing.Size(226, 89);
            this.randomWordButton.TabIndex = 3;
            this.randomWordButton.Text = "Random Words!";
            this.randomWordButton.UseVisualStyleBackColor = true;
            this.randomWordButton.Click += new System.EventHandler(this.randomWordButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.FileName = "dictionary.txt";
            this.openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog.Title = "Open dictionary";
            // 
            // openDictionaryButton
            // 
            this.openDictionaryButton.Location = new System.Drawing.Point(110, 41);
            this.openDictionaryButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.openDictionaryButton.Name = "openDictionaryButton";
            this.openDictionaryButton.Size = new System.Drawing.Size(126, 51);
            this.openDictionaryButton.TabIndex = 2;
            this.openDictionaryButton.Text = "Open dictionary";
            this.openDictionaryButton.UseVisualStyleBackColor = true;
            this.openDictionaryButton.Click += new System.EventHandler(this.openDictionaryButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 10);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(225, 25);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Items.AddRange(new object[] {
            "ANSI",
            "UTF-8"});
            this.encodingComboBox.Location = new System.Drawing.Point(12, 56);
            this.encodingComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(94, 24);
            this.encodingComboBox.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 195);
            this.Controls.Add(this.encodingComboBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.openDictionaryButton);
            this.Controls.Add(this.randomWordButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Pattern Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Button randomWordButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button openDictionaryButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ComboBox encodingComboBox;
    }
}

