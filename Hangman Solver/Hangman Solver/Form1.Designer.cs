namespace Hangman_Solver
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
            this.nextButton = new System.Windows.Forms.Button();
            this.possibleWordsBox = new System.Windows.Forms.ListBox();
            this.wordBox = new System.Windows.Forms.TextBox();
            this.wrongGuessCounterBox = new System.Windows.Forms.TextBox();
            this.solveButton = new System.Windows.Forms.Button();
            this.currentGuessBox = new System.Windows.Forms.TextBox();
            this.wrongLettersBox = new System.Windows.Forms.ListBox();
            this.numPossibleWordsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(147, 43);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(61, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // possibleWordsBox
            // 
            this.possibleWordsBox.FormattingEnabled = true;
            this.possibleWordsBox.Location = new System.Drawing.Point(13, 69);
            this.possibleWordsBox.Name = "possibleWordsBox";
            this.possibleWordsBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.possibleWordsBox.Size = new System.Drawing.Size(128, 160);
            this.possibleWordsBox.TabIndex = 3;
            // 
            // wordBox
            // 
            this.wordBox.Location = new System.Drawing.Point(13, 12);
            this.wordBox.Name = "wordBox";
            this.wordBox.Size = new System.Drawing.Size(128, 20);
            this.wordBox.TabIndex = 0;
            // 
            // wrongGuessCounterBox
            // 
            this.wrongGuessCounterBox.Location = new System.Drawing.Point(160, 12);
            this.wrongGuessCounterBox.Name = "wrongGuessCounterBox";
            this.wrongGuessCounterBox.ReadOnly = true;
            this.wrongGuessCounterBox.Size = new System.Drawing.Size(33, 20);
            this.wrongGuessCounterBox.TabIndex = 3;
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(147, 72);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(61, 23);
            this.solveButton.TabIndex = 2;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // currentGuessBox
            // 
            this.currentGuessBox.Location = new System.Drawing.Point(12, 38);
            this.currentGuessBox.Name = "currentGuessBox";
            this.currentGuessBox.ReadOnly = true;
            this.currentGuessBox.Size = new System.Drawing.Size(128, 20);
            this.currentGuessBox.TabIndex = 3;
            // 
            // wrongLettersBox
            // 
            this.wrongLettersBox.FormattingEnabled = true;
            this.wrongLettersBox.Location = new System.Drawing.Point(148, 108);
            this.wrongLettersBox.Name = "wrongLettersBox";
            this.wrongLettersBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.wrongLettersBox.Size = new System.Drawing.Size(57, 95);
            this.wrongLettersBox.TabIndex = 3;
            // 
            // numPossibleWordsBox
            // 
            this.numPossibleWordsBox.Location = new System.Drawing.Point(148, 209);
            this.numPossibleWordsBox.Name = "numPossibleWordsBox";
            this.numPossibleWordsBox.ReadOnly = true;
            this.numPossibleWordsBox.Size = new System.Drawing.Size(57, 20);
            this.numPossibleWordsBox.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 241);
            this.Controls.Add(this.wrongLettersBox);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.numPossibleWordsBox);
            this.Controls.Add(this.wrongGuessCounterBox);
            this.Controls.Add(this.currentGuessBox);
            this.Controls.Add(this.wordBox);
            this.Controls.Add(this.possibleWordsBox);
            this.Controls.Add(this.nextButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Hangman Solver";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.ListBox possibleWordsBox;
        private System.Windows.Forms.TextBox wordBox;
        private System.Windows.Forms.TextBox wrongGuessCounterBox;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.TextBox currentGuessBox;
        private System.Windows.Forms.ListBox wrongLettersBox;
        private System.Windows.Forms.TextBox numPossibleWordsBox;

    }
}

