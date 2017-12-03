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

        private Bitmap inputBitmap;

        private Bitmap inputSurface;
        private Graphics gInput;

        private string imageEncodingMode;

        private string[] imageNumberedResult;

        private void MainForm_Load(object sender, EventArgs e)
        {
            lineHighlightColor = Color.FromArgb(196, Color.LightGreen);

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
        private string[] GetTextOfBitmap(Bitmap bmp, Func<Color, Color, bool> areSame, Func<Color, int, string> getColorString)
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

                DrawLineHorizontal(gInput, bmp, lineHighlightColor, y);

                for (int x = 0; x < bmp.Width; x++)
                {
                    currentColor = bmp.GetPixel(x, y);

                    if (x == 0)
                    {
                        previousColor = currentColor;
                    }

                    if (!areSame(currentColor, previousColor) || x == bmp.Width - 1)
                    {
                        stringArray[y] += getColorString(previousColor, numContinousColors);

                        if (x < bmp.Width - 1)
                        {
                            stringArray[y] += "; ";
                            previousColor = currentColor;
                            numContinousColors = 1;
                        }
                    }
                    else
                    {
                        numContinousColors++;
                    }
                }

                pictureBox.Image = inputSurface;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)progressBar.Value / progressBar.Maximum;
                percentLabel.Text = percent.ToString("p1");

                Application.DoEvents();
            }

            return stringArray;
        }

        private string[] ConvertToNumberedBinary(Bitmap bmp)
        {
            return GetTextOfBitmap(bmp,
                                   (c1, c2) => (c1.GetBrightness() <= 0.5 && c2.GetBrightness() <= 0.5) ||
                                               c1.GetBrightness() > 0.5 && c2.GetBrightness() > 0.5, GetBinaryColorString);
        }

        private static string GetBinaryColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return ((col.GetBrightness() <= 0.5) ? "1" : "0") + ", " + numContinousColors;
            }
            else
            {
                return "";
            }
        }

        private string[] ConvertToNumberedBrightness(Bitmap bmp)
        {
            return GetTextOfBitmap(bmp, (c1, c2) => (int)Math.Round(c1.GetBrightness() * 255) == (int)Math.Round(c2.GetBrightness() * 255), GetBrightnessColorString);
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
            return GetTextOfBitmap(bmp, (c1, c2) => c1.R == c2.R && c1.G == c2.G && c1.B == c2.B, GetRgbColorString);
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
            return GetTextOfBitmap(bmp, (c1, c2) => c1 == c2, GetArgbColorString);
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
            return GetTextOfBitmap(bmp, (c1, c2) => c1 == c2, GetHexadecimalColorString);
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
        private Bitmap GetBitmapFromText(string[] text, Func<string, Color> getColorFromString)
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            progressBar.Value = 0;
            progressBar.Maximum = bmp.Height;

            int y = 0;

            foreach (string line in text)
            {
                int x = 0;

                foreach (string pixelString in line.Split(new[] {"; "}, StringSplitOptions.None))
                {
                    int lastCommaIndex = pixelString.LastIndexOf(", ", StringComparison.Ordinal);

                    int numOccurrences = Int32.Parse(pixelString.Substring(lastCommaIndex + ", ".Length));

                    Color color = getColorFromString(pixelString.Substring(0, lastCommaIndex));

                    for (int times = 0; times < numOccurrences; times++)
                    {
                        bmp.SetPixel(x, y, color);
                        x++;
                    }
                }

                outputPictureBox.Image = bmp;

                // Fancy progress bar stuff
                progressBar.PerformStep();

                // Fancy percentage stuff
                float percent = (float)progressBar.Value / progressBar.Maximum;
                percentLabel.Text = percent.ToString("p1");

                y++;

                Application.DoEvents();
            }

            return bmp;
        }

        private Color GetBinaryColorFromString(string colorString)
        {
            if (colorString == "0")
            {
                return Color.White;
            }
            else
            {
                return Color.Black;
            }
        }

        private Bitmap ConvertNumberedBinaryToBitmap()
        {
            return GetBitmapFromText(imageNumberedResult, GetBinaryColorFromString);
        }

        private Color GetBrightnessColorFromString(string colorString)
        {
            int brightnessValue = Int32.Parse(colorString);

            return Color.FromArgb(brightnessValue, brightnessValue, brightnessValue);
        }

        private Bitmap ConvertNumberedBrightnessToBitmap()
        {
            return GetBitmapFromText(imageNumberedResult, GetBrightnessColorFromString);
        }

        private Color GetRgbColorFromString(string colorString)
        {
            int[] colorParts = colorString.Split(new[] {", ", "(", ")"}, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(Int32.Parse).ToArray();

            return Color.FromArgb(colorParts[0], colorParts[1], colorParts[2]);
        }

        private Bitmap ConvertNumberedRgbToBitmap()
        {
            return GetBitmapFromText(imageNumberedResult, GetRgbColorFromString);
        }

        private Color GetArgbColorFromString(string colorString)
        {
            int[] colorParts = colorString.Split(new[] {", ", "(", ")"}, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();

            return Color.FromArgb(colorParts[0], colorParts[1], colorParts[2], colorParts[3]);
        }

        private Bitmap ConvertNumberedArgbToBitmap()
        {
            return GetBitmapFromText(imageNumberedResult, GetArgbColorFromString);
        }

        private Color GetHexadecimalColorFromString(string colorString)
        {
            return ColorTranslator.FromHtml(colorString);
        }

        private Bitmap ConvertNumberedHexadecimalToBitmap()
        {
            return GetBitmapFromText(imageNumberedResult, GetHexadecimalColorFromString);
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
