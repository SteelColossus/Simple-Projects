using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Drawing.Imaging;

namespace Picture_Storage
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Fancy line stuff
        private Color lineHighlightColor;
        private Pen lineHighlightPen;

        private Bitmap inputBitmap;

        private Bitmap inputSurface;
        private Graphics gInput;

        private string imageEncodingMode;

        private string[] imageNumberedResult;

        private void MainForm_Load(object sender, EventArgs e)
        {
            lineHighlightColor = Color.FromArgb(196, Color.LightGreen);
            lineHighlightPen = new Pen(lineHighlightColor);

            textModeComboBox.SelectedItem = "RGB";

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string allFileExtensions = "";
            string allFileNamesWithExtensions = "";

            foreach (ImageCodecInfo c in codecs)
            {
                string fileNameWithExtensions = c.FormatDescription + " Files" + "|" + c.FilenameExtension.ToLower();

                if (allFileNamesWithExtensions.Substring(allFileNamesWithExtensions.Length) != ";" && allFileNamesWithExtensions.Length > 0)
                {
                    allFileNamesWithExtensions += "|";
                }

                allFileNamesWithExtensions += fileNameWithExtensions;

                if (allFileExtensions.Substring(allFileExtensions.Length) != ";" && allFileExtensions.Length > 0)
                {
                    allFileExtensions += ";";
                }

                allFileExtensions += c.FilenameExtension.ToLower();
            }

            imageOpenFileDialog.Filter = @"Image Files" + @"|" + allFileExtensions + @"|" + allFileNamesWithExtensions;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            DialogResult loadResult = imageOpenFileDialog.ShowDialog();

            if (loadResult == DialogResult.OK)
            {
                Reset();

                inputBitmap = BitmapHelper.Load(imageOpenFileDialog.FileName);

                inputSurface = new Bitmap(inputBitmap);

                gInput = Graphics.FromImage(inputSurface);

                pictureBox.Image = inputSurface;
            }
        }

        private void imageToTextButton_Click(object sender, EventArgs e)
        {
            if (inputBitmap != null)
            {
                outputListBox.Items.Clear();
                outputPictureBox.Image = null;

                ChangeEnability(false);

                imageEncodingMode = textModeComboBox.SelectedItem.ToString();

                Func<Bitmap, string[]> conversionFunction = null;

                switch (imageEncodingMode)
                {
                    case "Binary":
                        conversionFunction = ConvertToNumberedBinary;
                        break;
                    case "Brightness":
                        conversionFunction = ConvertToNumberedBrightness;
                        break;
                    case "RGB":
                        conversionFunction = ConvertToNumberedRgb;
                        break;
                    case "ARGB":
                        conversionFunction = ConvertToNumberedArgb;
                        break;
                    case "Hexadecimal":
                        conversionFunction = ConvertToNumberedHexadecimal;
                        break;
                }

                if (conversionFunction != null)
                {
                    imageNumberedResult = conversionFunction(inputBitmap);
                }

                foreach (string line in imageNumberedResult)
                {
                    outputListBox.Items.Add(line);
                }

                MessageBox.Show(@"The picture has been fully scanned.", @"Scan Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ChangeEnability(true);
            }
            else
            {
                MessageBox.Show(@"No picture has been loaded yet.", @"Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textToImageButton_Click(object sender, EventArgs e)
        {
            if (outputListBox.Items.Count > 0)
            {
                outputPictureBox.Image = null;

                ChangeEnability(false);

                Bitmap bmp = new Bitmap(1, 1);

                Func<Bitmap> conversionFunction = null;

                switch (imageEncodingMode)
                {
                    case "Binary":
                        conversionFunction = ConvertNumberedBinaryToBitmap;
                        break;
                    case "Brightness":
                        conversionFunction = ConvertNumberedBrightnessToBitmap;
                        break;
                    case "RGB":
                        conversionFunction = ConvertNumberedRgbToBitmap;
                        break;
                    case "ARGB":
                        conversionFunction = ConvertNumberedArgbToBitmap;
                        break;
                    case "Hexadecimal":
                        conversionFunction = ConvertNumberedHexadecimalToBitmap;
                        break;
                }

                if (conversionFunction != null)
                {
                    bmp = conversionFunction();
                }

                outputPictureBox.Image = bmp;

                MessageBox.Show(@"The picture has been fully recreated.", @"Recreation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ChangeEnability(true);
            }
            else
            {
                MessageBox.Show(@"No images have been scanned yet.", @"Image Recreation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            pictureBox.Image = null;
            percentLabel.Text = "";
            progressBar.Value = 0;
            outputListBox.Items.Clear();
            outputPictureBox.Image = null;
        }

        private void ChangeEnability(bool enabled)
        {
            foreach (Control c in Controls)
            {
                if (c is Button button)
                {
                    button.Enabled = enabled;
                }
            }

            textModeComboBox.Enabled = enabled;
        }

        private void outputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DrawLineHorizontal(gInput, inputBitmap, lineHighlightColor, outputListBox.SelectedIndex);

                pictureBox.Image = inputSurface;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void importFromTextFileButton_Click(object sender, EventArgs e)
        {
            DialogResult openResult = openFromTextFileDialog.ShowDialog();

            if (openResult == DialogResult.OK)
            {
                if (outputListBox.Items.Count > 0)
                {
                    DialogResult overrideResult = MessageBox.Show(@"The current text output will be replaced with another." + Environment.NewLine + @"Do you want to continue?", @"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (overrideResult == DialogResult.Yes)
                    {
                        Reset();
                    }
                    else
                    {
                        return;
                    }
                }

                outputListBox.Items.AddRange(System.IO.File.ReadAllLines(openFromTextFileDialog.FileName).Cast<object>().ToArray());
            }
        }

        private void exportToTextFileButton_Click(object sender, EventArgs e)
        {
            if (outputListBox.Items.Count > 0)
            {
                DialogResult saveResult = saveToTextFileDialog.ShowDialog();

                if (saveResult == DialogResult.OK)
                {
                    System.IO.File.WriteAllLines(saveToTextFileDialog.FileName, outputListBox.Items.Cast<string>().ToArray());
                }
            }
            else
            {
                MessageBox.Show(@"There is nothing to export.", @"Exporting Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #region Converting images to text
        private string[] ConvertToNumberedBinary(Bitmap bmp)
        {
            Color currentColor = Color.Empty;
            Color previousColor = Color.Empty;
            string[] stringArray = new string[bmp.Height];

            // Fancy progress bar stuff
            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;

            for (int y = 0; y < bmp.Height; y++)
            {
                int numContinousColors = 0;

                for (int x = 0; x < bmp.Width; x++)
                {
                    currentColor = bmp.GetPixel(x, y);

                    if (x == 0)
                    {
                        previousColor = Color.White;
                    }

                    if ((currentColor.GetBrightness() > 0.5 && previousColor.GetBrightness() <= 0.5) || currentColor.GetBrightness() <= 0.5 && previousColor.GetBrightness() > 0.5)
                    {
                        stringArray[y] += numContinousColors + ", ";
                        
                        previousColor = currentColor;

                        numContinousColors = 1;
                    }
                    else
                    {
                        numContinousColors++;
                    }
                }

                stringArray[y] += numContinousColors.ToString();

                DrawLineHorizontal(gInput, bmp, lineHighlightColor, y);

                pictureBox.Image = inputSurface;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                Application.DoEvents();
            }

            return stringArray;
        }

        private string[] ConvertToNumberedBrightness(Bitmap bmp)
        {
            Color currentColor = Color.Empty;
            Color previousColor = Color.Empty;
            string[] stringArray = new string[bmp.Height];

            // Fancy progress bar stuff
            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;
            
            for (int y = 0; y < bmp.Height; y++)
            {
                int numContinousColors = 0;

                for (int x = 0; x < bmp.Width; x++)
                {
                    currentColor = bmp.GetPixel(x, y);

                    if (x == 0)
                    {
                        previousColor = currentColor;
                    }

                    if (currentColor != previousColor || x == 0)
                    {
                        stringArray[y] += GetBrightnessColorString(previousColor, numContinousColors);

                        if (x != 0)
                        {
                            stringArray[y] += ", ";
                            previousColor = currentColor;
                        }

                        numContinousColors = 1;
                    }
                    else
                    {
                        numContinousColors++;
                    }
                }

                stringArray[y] += GetBrightnessColorString(previousColor, numContinousColors);

                DrawLineHorizontal(gInput, bmp, lineHighlightColor, y);

                pictureBox.Image = inputSurface;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                Application.DoEvents();
            }

            return stringArray;
        }

        private static string GetBrightnessColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return Math.Round(col.GetBrightness() * 255) + ", " + numContinousColors;
            }
            else
            {
                return "";
            }
        }

        private string[] ConvertToNumberedRgb(Bitmap bmp)
        {
            Color currentColor = Color.Empty;
            Color previousColor = Color.Empty;
            string[] stringArray = new string[bmp.Height];

            // Fancy progress bar stuff
            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;
            
            for (int y = 0; y < bmp.Height; y++)
            {
                int numContinousColors = 0;

                for (int x = 0; x < bmp.Width; x++)
                {
                    currentColor = bmp.GetPixel(x, y);

                    if (x == 0)
                    {
                        previousColor = currentColor;
                    }

                    if (currentColor != previousColor || x == 0)
                    {
                        stringArray[y] += GetRgbColorString(previousColor, numContinousColors);

                        if (x != 0)
                        {
                            stringArray[y] += ", ";
                            previousColor = currentColor;
                        }

                        numContinousColors = 1;
                    }
                    else
                    {
                        numContinousColors++;
                    }
                }

                stringArray[y] += GetRgbColorString(previousColor, numContinousColors);

                DrawLineHorizontal(gInput, bmp, lineHighlightColor, y);

                pictureBox.Image = inputSurface;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                Application.DoEvents();
            }

            return stringArray;
        }

        private string GetRgbColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return "(" + col.R + ", " + col.G + ", " + col.B + ")" + ", " + numContinousColors;
            }
            else
            {
                return "";
            }
        }

        private string[] ConvertToNumberedArgb(Bitmap bmp)
        {
            Color currentColor = Color.Empty;
            Color previousColor = Color.Empty;
            string[] stringArray = new string[bmp.Height];

            // Fancy progress bar stuff
            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;

            for (int y = 0; y < bmp.Height; y++)
            {
                int numContinousColors = 0;

                for (int x = 0; x < bmp.Width; x++)
                {
                    currentColor = bmp.GetPixel(x, y);

                    if (x == 0)
                    {
                        previousColor = currentColor;
                    }

                    if (currentColor != previousColor || x == 0)
                    {
                        stringArray[y] += GetArgbColorString(previousColor, numContinousColors);

                        if (x != 0)
                        {
                            stringArray[y] += ", ";
                            previousColor = currentColor;
                        }

                        numContinousColors = 1;
                    }
                    else
                    {
                        numContinousColors++;
                    }
                }

                stringArray[y] += GetArgbColorString(previousColor, numContinousColors);

                DrawLineHorizontal(gInput, bmp, lineHighlightColor, y);

                pictureBox.Image = inputSurface;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                Application.DoEvents();
            }

            return stringArray;
        }

        private string GetArgbColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return "(" + col.A + ", " + col.R + ", " + col.G + ", " + col.B + ")" + ", " + numContinousColors;
            }
            else
            {
                return "";
            }
        }

        private string[] ConvertToNumberedHexadecimal(Bitmap bmp)
        {
            Color currentColor = Color.Empty;
            Color previousColor = Color.Empty;
            string[] stringArray = new string[bmp.Height];

            // Fancy progress bar stuff
            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;

            for (int y = 0; y < bmp.Height; y++)
            {
                int numContinousColors = 0;

                for (int x = 0; x < bmp.Width; x++)
                {
                    currentColor = bmp.GetPixel(x, y);

                    if (x == 0)
                    {
                        previousColor = currentColor;
                    }

                    if (currentColor != previousColor || x == 0)
                    {
                        stringArray[y] += GetHexadecimalColorString(previousColor, numContinousColors);

                        if (x != 0)
                        {
                            stringArray[y] += ", ";
                            previousColor = currentColor;
                        }

                        numContinousColors = 1;
                    }
                    else
                    {
                        numContinousColors++;
                    }
                }

                stringArray[y] += GetHexadecimalColorString(previousColor, numContinousColors);

                DrawLineHorizontal(gInput, bmp, lineHighlightColor, y);

                pictureBox.Image = inputSurface;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                Application.DoEvents();
            }

            return stringArray;
        }

        private string GetHexadecimalColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return ColorTranslator.ToHtml(col) + ", " + numContinousColors;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region Converting text to images
        private Bitmap ConvertNumberedBinaryToBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;

            int y = 0;

            foreach (string line in imageNumberedResult)
            {
                int x = 0;

                string trim = line.Trim();

                Color col = Color.Empty;

                foreach (string pixelValue in trim.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (col == Color.White)
                    {
                        col = Color.Black;
                    }
                    else
                    {
                        col = Color.White;
                    }

                    for (int times = 0; times < Int32.Parse(pixelValue); times++)
                    {
                        bmp.SetPixel(x, y, col);
                        x++;
                    }
                }

                outputPictureBox.Image = bmp;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                y++;

                Application.DoEvents();
            }

            return bmp;
        }

        private Bitmap ConvertNumberedBrightnessToBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;
            
            int y = 0;

            foreach (string line in imageNumberedResult)
            {
                int x = 0;

                string trim = line.Trim();

                string[] pixelString = trim.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int v = 0; v < pixelString.Length; v += 2)
                {
                    for (int times = 0; times < Int32.Parse(pixelString[v + 1]); times++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(Int32.Parse(pixelString[v]), Int32.Parse(pixelString[v]), Int32.Parse(pixelString[v])));
                        x++;
                    }
                }
                
                outputPictureBox.Image = bmp;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                y++;

                Application.DoEvents();
            }

            return bmp;
        }

        private Bitmap ConvertNumberedRgbToBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;
            
            int y = 0;

            foreach (string line in imageNumberedResult)
            {
                int x = 0;

                string trim = line.Trim();

                foreach (string pixelString in trim.Split(new[] { ", (" }, StringSplitOptions.None))
                {
                    string[] pixelValues = pixelString.Split(new[] { ',', '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int times = 0; times < Int32.Parse(pixelValues[3]); times++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(Int32.Parse(pixelValues[0]), Int32.Parse(pixelValues[1]), Int32.Parse(pixelValues[2])));
                        x++;
                    }
                }

                outputPictureBox.Image = bmp;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                y++;

                Application.DoEvents();
            }

            return bmp;
        }

        private Bitmap ConvertNumberedArgbToBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;
            
            int y = 0;

            foreach (string line in imageNumberedResult)
            {
                int x = 0;

                string trim = line.Trim();

                foreach (string pixelString in trim.Split(new[] { ", (" }, StringSplitOptions.None))
                {
                    string[] pixelValues = pixelString.Split(new[] { ',', '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int times = 0; times < Int32.Parse(pixelValues[4]); times++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(Int32.Parse(pixelValues[0]), Int32.Parse(pixelValues[1]), Int32.Parse(pixelValues[2]), Int32.Parse(pixelValues[3])));
                        x++;
                    }
                }

                outputPictureBox.Image = bmp;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                y++;

                Application.DoEvents();
            }

            return bmp;
        }

        private Bitmap ConvertNumberedHexadecimalToBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;
            
            int y = 0;

            foreach (string line in imageNumberedResult)
            {
                int x = 0;

                string trim = line.Trim();

                string[] pixelString = trim.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int v = 0; v < pixelString.Length; v += 2)
                {
                    for (int times = 0; times < Int32.Parse(pixelString[v + 1]); times++)
                    {
                        bmp.SetPixel(x, y, ColorTranslator.FromHtml(pixelString[v]));
                        x++;
                    }
                }

                outputPictureBox.Image = bmp;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)(y + 1) / bmp.Height;
                percentLabel.Text = percent.ToString("p1");

                y++;

                Application.DoEvents();
            }

            return bmp;
        }
        #endregion

        private static void DrawLineHorizontal(Graphics g, Bitmap b, Color c, int y)
        {
            Pen pen = new Pen(c);

            g.Clear(Color.Empty);
            g.DrawImage(b, 0, 0);
            g.DrawLine(pen, 0, y, b.Width, y);
        }
    }
}
