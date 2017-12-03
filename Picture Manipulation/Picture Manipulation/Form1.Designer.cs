namespace PictureManipulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.invertButton = new System.Windows.Forms.Button();
            this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.resetButton = new System.Windows.Forms.Button();
            this.redFilterButton = new System.Windows.Forms.Button();
            this.greenFilterButton = new System.Windows.Forms.Button();
            this.blueFilterButton = new System.Windows.Forms.Button();
            this.cycleColoursButton = new System.Windows.Forms.Button();
            this.flipButton = new System.Windows.Forms.Button();
            this.interferenceButton = new System.Windows.Forms.Button();
            this.averageButton = new System.Windows.Forms.Button();
            this.pixelButton = new System.Windows.Forms.Button();
            this.valueUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Location = new System.Drawing.Point(12, 52);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(384, 288);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // loadButton
            // 
            this.loadButton.Image = ((System.Drawing.Image)(resources.GetObject("loadButton.Image")));
            this.loadButton.Location = new System.Drawing.Point(12, 11);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(35, 35);
            this.loadButton.TabIndex = 3;
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // invertButton
            // 
            this.invertButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.invertButton.Location = new System.Drawing.Point(404, 81);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(83, 23);
            this.invertButton.TabIndex = 5;
            this.invertButton.Text = "Invert";
            this.invertButton.UseVisualStyleBackColor = true;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // imageOpenFileDialog
            // 
            this.imageOpenFileDialog.DefaultExt = "png";
            this.imageOpenFileDialog.Filter = "Image Files|*.png;*.jpg;*.gif|PNG Files|*.png|JPEG Files|*.jpg|GIF Files|*.gif";
            this.imageOpenFileDialog.InitialDirectory = "Libraries\\Pictures";
            this.imageOpenFileDialog.RestoreDirectory = true;
            this.imageOpenFileDialog.Title = "Load an image...";
            // 
            // resetButton
            // 
            this.resetButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.resetButton.Location = new System.Drawing.Point(404, 52);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(83, 23);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // redFilterButton
            // 
            this.redFilterButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.redFilterButton.Location = new System.Drawing.Point(404, 110);
            this.redFilterButton.Name = "redFilterButton";
            this.redFilterButton.Size = new System.Drawing.Size(83, 23);
            this.redFilterButton.TabIndex = 14;
            this.redFilterButton.Text = "Red Filter";
            this.redFilterButton.UseVisualStyleBackColor = true;
            this.redFilterButton.Click += new System.EventHandler(this.redFilterButton_Click);
            // 
            // greenFilterButton
            // 
            this.greenFilterButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.greenFilterButton.Location = new System.Drawing.Point(404, 139);
            this.greenFilterButton.Name = "greenFilterButton";
            this.greenFilterButton.Size = new System.Drawing.Size(83, 23);
            this.greenFilterButton.TabIndex = 15;
            this.greenFilterButton.Text = "Green Filter";
            this.greenFilterButton.UseVisualStyleBackColor = true;
            this.greenFilterButton.Click += new System.EventHandler(this.greenFilterButton_Click);
            // 
            // blueFilterButton
            // 
            this.blueFilterButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.blueFilterButton.Location = new System.Drawing.Point(404, 168);
            this.blueFilterButton.Name = "blueFilterButton";
            this.blueFilterButton.Size = new System.Drawing.Size(83, 23);
            this.blueFilterButton.TabIndex = 16;
            this.blueFilterButton.Text = "Blue Filter";
            this.blueFilterButton.UseVisualStyleBackColor = true;
            this.blueFilterButton.Click += new System.EventHandler(this.blueFilterButton_Click);
            // 
            // cycleColoursButton
            // 
            this.cycleColoursButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cycleColoursButton.Location = new System.Drawing.Point(404, 197);
            this.cycleColoursButton.Name = "cycleColoursButton";
            this.cycleColoursButton.Size = new System.Drawing.Size(83, 23);
            this.cycleColoursButton.TabIndex = 17;
            this.cycleColoursButton.Text = "Cycle Colours";
            this.cycleColoursButton.UseVisualStyleBackColor = true;
            this.cycleColoursButton.Click += new System.EventHandler(this.cycleColoursButton_Click);
            // 
            // flipButton
            // 
            this.flipButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flipButton.Location = new System.Drawing.Point(404, 226);
            this.flipButton.Name = "flipButton";
            this.flipButton.Size = new System.Drawing.Size(83, 23);
            this.flipButton.TabIndex = 18;
            this.flipButton.Text = "Flip";
            this.flipButton.UseVisualStyleBackColor = true;
            this.flipButton.Click += new System.EventHandler(this.flipButton_Click);
            // 
            // interferenceButton
            // 
            this.interferenceButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.interferenceButton.Location = new System.Drawing.Point(404, 255);
            this.interferenceButton.Name = "interferenceButton";
            this.interferenceButton.Size = new System.Drawing.Size(83, 23);
            this.interferenceButton.TabIndex = 19;
            this.interferenceButton.Text = "Interference";
            this.interferenceButton.UseVisualStyleBackColor = true;
            this.interferenceButton.Click += new System.EventHandler(this.interferenceButton_Click);
            // 
            // averageButton
            // 
            this.averageButton.Location = new System.Drawing.Point(403, 285);
            this.averageButton.Name = "averageButton";
            this.averageButton.Size = new System.Drawing.Size(84, 23);
            this.averageButton.TabIndex = 20;
            this.averageButton.Text = "Blurry";
            this.averageButton.UseVisualStyleBackColor = true;
            this.averageButton.Click += new System.EventHandler(this.averageButton_Click);
            // 
            // pixelButton
            // 
            this.pixelButton.Location = new System.Drawing.Point(404, 315);
            this.pixelButton.Name = "pixelButton";
            this.pixelButton.Size = new System.Drawing.Size(83, 23);
            this.pixelButton.TabIndex = 21;
            this.pixelButton.Text = "Pixelate";
            this.pixelButton.UseVisualStyleBackColor = true;
            this.pixelButton.Click += new System.EventHandler(this.pixelButton_Click);
            // 
            // valueUpDown
            // 
            this.valueUpDown.Location = new System.Drawing.Point(403, 13);
            this.valueUpDown.Name = "valueUpDown";
            this.valueUpDown.Size = new System.Drawing.Size(84, 20);
            this.valueUpDown.TabIndex = 22;
            this.valueUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 353);
            this.Controls.Add(this.valueUpDown);
            this.Controls.Add(this.pixelButton);
            this.Controls.Add(this.averageButton);
            this.Controls.Add(this.interferenceButton);
            this.Controls.Add(this.flipButton);
            this.Controls.Add(this.cycleColoursButton);
            this.Controls.Add(this.blueFilterButton);
            this.Controls.Add(this.greenFilterButton);
            this.Controls.Add(this.redFilterButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.invertButton);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.loadButton);
            this.Name = "Form1";
            this.Text = "Picture Manipulation";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button redFilterButton;
        private System.Windows.Forms.Button greenFilterButton;
        private System.Windows.Forms.Button blueFilterButton;
        private System.Windows.Forms.Button cycleColoursButton;
        private System.Windows.Forms.Button flipButton;
        private System.Windows.Forms.Button interferenceButton;
        private System.Windows.Forms.Button averageButton;
        private System.Windows.Forms.Button pixelButton;
        private System.Windows.Forms.NumericUpDown valueUpDown;
    }
}

