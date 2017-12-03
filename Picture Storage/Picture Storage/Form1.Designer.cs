namespace Picture_Storage
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
            this.components = new System.ComponentModel.Container();
            this.loadButton = new System.Windows.Forms.Button();
            this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.percentLabel = new System.Windows.Forms.Label();
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.imageToTextButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.textToImageButton = new System.Windows.Forms.Button();
            this.outputPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.importFromTextFileButton = new System.Windows.Forms.Button();
            this.importTextFilePictureBox = new System.Windows.Forms.PictureBox();
            this.exportToTextFileButton = new System.Windows.Forms.Button();
            this.exportTextFilePictureBox = new System.Windows.Forms.PictureBox();
            this.saveToTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFromTextFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textModeComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.importTextFilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exportTextFilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Image = global::Picture_Storage.Properties.Resources.Open_icon;
            this.loadButton.Location = new System.Drawing.Point(12, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(35, 35);
            this.loadButton.TabIndex = 1;
            this.toolTip.SetToolTip(this.loadButton, "Load an image into the program.");
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // imageOpenFileDialog
            // 
            this.imageOpenFileDialog.DefaultExt = "png";
            this.imageOpenFileDialog.Filter = "Image Files|*.png;*.jpg;*.gif|PNG Files|*.png|JPEG Files|*.jpg|GIF Files|*.gif";
            this.imageOpenFileDialog.InitialDirectory = "Libraries\\Pictures";
            this.imageOpenFileDialog.RestoreDirectory = true;
            this.imageOpenFileDialog.Title = "Load an image...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(53, 17);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(809, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Location = new System.Drawing.Point(868, 23);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(0, 13);
            this.percentLabel.TabIndex = 5;
            // 
            // outputListBox
            // 
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.HorizontalScrollbar = true;
            this.outputListBox.Location = new System.Drawing.Point(319, 53);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(276, 251);
            this.outputListBox.TabIndex = 8;
            this.outputListBox.TabStop = false;
            this.outputListBox.SelectedIndexChanged += new System.EventHandler(this.outputListBox_SelectedIndexChanged);
            // 
            // imageToTextButton
            // 
            this.imageToTextButton.Image = global::Picture_Storage.Properties.Resources.arrow_right;
            this.imageToTextButton.Location = new System.Drawing.Point(278, 159);
            this.imageToTextButton.Name = "imageToTextButton";
            this.imageToTextButton.Size = new System.Drawing.Size(35, 35);
            this.imageToTextButton.TabIndex = 2;
            this.toolTip.SetToolTip(this.imageToTextButton, "Converts the current image into a numbered format.");
            this.imageToTextButton.UseVisualStyleBackColor = true;
            this.imageToTextButton.Click += new System.EventHandler(this.imageToTextButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Image = global::Picture_Storage.Properties.Resources.cross;
            this.clearButton.Location = new System.Drawing.Point(440, 310);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(35, 35);
            this.clearButton.TabIndex = 4;
            this.toolTip.SetToolTip(this.clearButton, "Clears everything.");
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // textToImageButton
            // 
            this.textToImageButton.Image = global::Picture_Storage.Properties.Resources.arrow_right;
            this.textToImageButton.Location = new System.Drawing.Point(601, 159);
            this.textToImageButton.Name = "textToImageButton";
            this.textToImageButton.Size = new System.Drawing.Size(35, 35);
            this.textToImageButton.TabIndex = 3;
            this.toolTip.SetToolTip(this.textToImageButton, "Converts the numbered text back into an image.");
            this.textToImageButton.UseVisualStyleBackColor = true;
            this.textToImageButton.Click += new System.EventHandler(this.textToImageButton_Click);
            // 
            // outputPictureBox
            // 
            this.outputPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.outputPictureBox.Location = new System.Drawing.Point(642, 56);
            this.outputPictureBox.Name = "outputPictureBox";
            this.outputPictureBox.Size = new System.Drawing.Size(260, 248);
            this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.outputPictureBox.TabIndex = 2;
            this.outputPictureBox.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Location = new System.Drawing.Point(12, 53);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(260, 248);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // importFromTextFileButton
            // 
            this.importFromTextFileButton.Image = global::Picture_Storage.Properties.Resources.arrow_up;
            this.importFromTextFileButton.Location = new System.Drawing.Point(372, 310);
            this.importFromTextFileButton.Name = "importFromTextFileButton";
            this.importFromTextFileButton.Size = new System.Drawing.Size(35, 35);
            this.importFromTextFileButton.TabIndex = 9;
            this.toolTip.SetToolTip(this.importFromTextFileButton, "Imports a pre-existing text file into the program.");
            this.importFromTextFileButton.UseVisualStyleBackColor = true;
            this.importFromTextFileButton.Click += new System.EventHandler(this.importFromTextFileButton_Click);
            // 
            // importTextFilePictureBox
            // 
            this.importTextFilePictureBox.Image = global::Picture_Storage.Properties.Resources.textFileIcon;
            this.importTextFilePictureBox.Location = new System.Drawing.Point(372, 352);
            this.importTextFilePictureBox.Name = "importTextFilePictureBox";
            this.importTextFilePictureBox.Size = new System.Drawing.Size(35, 35);
            this.importTextFilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.importTextFilePictureBox.TabIndex = 10;
            this.importTextFilePictureBox.TabStop = false;
            // 
            // exportToTextFileButton
            // 
            this.exportToTextFileButton.Image = global::Picture_Storage.Properties.Resources.arrow_down;
            this.exportToTextFileButton.Location = new System.Drawing.Point(506, 311);
            this.exportToTextFileButton.Name = "exportToTextFileButton";
            this.exportToTextFileButton.Size = new System.Drawing.Size(35, 35);
            this.exportToTextFileButton.TabIndex = 11;
            this.toolTip.SetToolTip(this.exportToTextFileButton, "Exports the current text to a text file.");
            this.exportToTextFileButton.UseVisualStyleBackColor = true;
            this.exportToTextFileButton.Click += new System.EventHandler(this.exportToTextFileButton_Click);
            // 
            // exportTextFilePictureBox
            // 
            this.exportTextFilePictureBox.Image = global::Picture_Storage.Properties.Resources.textFileIcon;
            this.exportTextFilePictureBox.Location = new System.Drawing.Point(506, 352);
            this.exportTextFilePictureBox.Name = "exportTextFilePictureBox";
            this.exportTextFilePictureBox.Size = new System.Drawing.Size(35, 35);
            this.exportTextFilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.exportTextFilePictureBox.TabIndex = 10;
            this.exportTextFilePictureBox.TabStop = false;
            // 
            // saveToTextFileDialog
            // 
            this.saveToTextFileDialog.DefaultExt = "txt";
            this.saveToTextFileDialog.Filter = "All files|*.*|Text files|*.txt";
            this.saveToTextFileDialog.RestoreDirectory = true;
            this.saveToTextFileDialog.Title = "Save text file...";
            // 
            // openFromTextFileDialog
            // 
            this.openFromTextFileDialog.DefaultExt = "txt";
            this.openFromTextFileDialog.Filter = "Text files|*.txt";
            this.openFromTextFileDialog.RestoreDirectory = true;
            this.openFromTextFileDialog.Title = "Open a text file...";
            // 
            // textModeComboBox
            // 
            this.textModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textModeComboBox.FormattingEnabled = true;
            this.textModeComboBox.Items.AddRange(new object[] {
            "Binary",
            "Brightness",
            "RGB",
            "ARGB",
            "Hexadecimal"});
            this.textModeComboBox.Location = new System.Drawing.Point(79, 324);
            this.textModeComboBox.Name = "textModeComboBox";
            this.textModeComboBox.Size = new System.Drawing.Size(121, 21);
            this.textModeComboBox.TabIndex = 12;
            this.toolTip.SetToolTip(this.textModeComboBox, "The mode that the program will use to create the text output.");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 399);
            this.Controls.Add(this.textModeComboBox);
            this.Controls.Add(this.exportToTextFileButton);
            this.Controls.Add(this.exportTextFilePictureBox);
            this.Controls.Add(this.importTextFilePictureBox);
            this.Controls.Add(this.importFromTextFileButton);
            this.Controls.Add(this.imageToTextButton);
            this.Controls.Add(this.outputListBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.textToImageButton);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.outputPictureBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.loadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Picture Storage";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.importTextFilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exportTextFilePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox outputPictureBox;
        private System.Windows.Forms.Button textToImageButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ListBox outputListBox;
        private System.Windows.Forms.Button imageToTextButton;
        private System.Windows.Forms.Button importFromTextFileButton;
        private System.Windows.Forms.PictureBox importTextFilePictureBox;
        private System.Windows.Forms.Button exportToTextFileButton;
        private System.Windows.Forms.PictureBox exportTextFilePictureBox;
        private System.Windows.Forms.SaveFileDialog saveToTextFileDialog;
        private System.Windows.Forms.OpenFileDialog openFromTextFileDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox textModeComboBox;
    }
}

