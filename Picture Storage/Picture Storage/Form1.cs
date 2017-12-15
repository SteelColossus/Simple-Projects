using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_Storage
{
    public partial class MainForm : Form
    {
        private static readonly SemaphoreSlim bitmapSemaphore = new SemaphoreSlim(1, 1);

        // Fancy line stuff
        private Color lineHighlightColor;

        private Bitmap inputBitmap;

        private Bitmap inputSurface;
        private Graphics gInput;

        private string imageEncodingMode;

        private string[] imageNumberedResult;

        public MainForm()
        {
            InitializeComponent();
        }

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

                if ((allFileNamesWithExtensions.Substring(allFileNamesWithExtensions.Length) != ";") &&
                    (allFileNamesWithExtensions.Length > 0))
                {
                    allFileNamesWithExtensions += "|";
                }

                allFileNamesWithExtensions += fileNameWithExtensions;

                if ((allFileExtensions.Substring(allFileExtensions.Length) != ";") && (allFileExtensions.Length > 0))
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

        private async void ConvertImageToText()
        {
            if (inputBitmap != null)
            {
                outputListBox.Items.Clear();
                outputPictureBox.Image = null;

                ChangeEnability(false);

                imageEncodingMode = textModeComboBox.SelectedItem.ToString();

                progressBar.Value = 0;
                progressBar.Maximum = inputBitmap.Height;

                Progress<int> progress = new Progress<int>();

                progress.ProgressChanged += (sender, i) =>
                {
                    bitmapSemaphore.Wait();

                    try
                    {
                        DrawLineHorizontal(gInput, inputBitmap, lineHighlightColor, i);
                        pictureBox.Image = inputSurface;
                    }
                    finally
                    {
                        bitmapSemaphore.Release();
                    }

                    progressBar.PerformStep();

                    float percent = (float)progressBar.Value / progressBar.Maximum;
                    percentLabel.Text = percent.ToString("p1");
                };

                switch (imageEncodingMode)
                {
                    case "Binary":
                        imageNumberedResult = await ConvertToNumberedBinary(inputBitmap, progress);
                        break;
                    case "Brightness":
                        imageNumberedResult = await ConvertToNumberedBrightness(inputBitmap, progress);
                        break;
                    case "RGB":
                        imageNumberedResult = await ConvertToNumberedRgb(inputBitmap, progress);
                        break;
                    case "ARGB":
                        imageNumberedResult = await ConvertToNumberedArgb(inputBitmap, progress);
                        break;
                    case "Hexadecimal":
                        imageNumberedResult = await ConvertToNumberedHexadecimal(inputBitmap, progress);
                        break;
                }

                MessageBox.Show(@"The picture has been fully scanned.", @"Scan Complete", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                outputListBox.Items.AddRange(imageNumberedResult.Cast<object>().ToArray());

                ChangeEnability(true);
            }
            else
            {
                MessageBox.Show(@"No picture has been loaded yet.", @"Conversion Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        private void imageToTextButton_Click(object sender, EventArgs e)
        {
            ConvertImageToText();
        }

        private void textToImageButton_Click(object sender, EventArgs e)
        {
            ConvertTextToImage();
        }

        private async void ConvertTextToImage()
        {
            if (outputListBox.Items.Count > 0)
            {
                outputPictureBox.Image = null;

                ChangeEnability(false);

                Bitmap bmp = new Bitmap(1, 1);

                progressBar.Value = 0;
                progressBar.Maximum = inputBitmap.Height;

                Progress<BitmapProgress> progress = new Progress<BitmapProgress>();

                progress.ProgressChanged += (sender, bp) =>
                {
                    bitmapSemaphore.Wait();

                    try
                    {
                        ManualResetEvent mre = new ManualResetEvent(false);

                        outputPictureBox.Paint += (o, args) => { mre.Set(); };

                        outputPictureBox.Image = bp.currentBitmap;

                        mre.WaitOne();
                    }
                    finally
                    {
                        bitmapSemaphore.Release();
                    }

                    progressBar.PerformStep();

                    float percent = (float)progressBar.Value / progressBar.Maximum;
                    percentLabel.Text = percent.ToString("p1");
                };

                switch (imageEncodingMode)
                {
                    case "Binary":
                        bmp = await ConvertNumberedBinaryToBitmap(imageNumberedResult, progress);
                        break;
                    case "Brightness":
                        bmp = await ConvertNumberedBrightnessToBitmap(imageNumberedResult, progress);
                        break;
                    case "RGB":
                        bmp = await ConvertNumberedRgbToBitmap(imageNumberedResult, progress);
                        break;
                    case "ARGB":
                        bmp = await ConvertNumberedArgbToBitmap(imageNumberedResult, progress);
                        break;
                    case "Hexadecimal":
                        bmp = await ConvertNumberedHexadecimalToBitmap(imageNumberedResult, progress);
                        break;
                }

                outputPictureBox.Image = bmp;

                MessageBox.Show(@"The picture has been fully recreated.", @"Recreation Complete", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                ChangeEnability(true);
            }
            else
            {
                MessageBox.Show(@"No images have been scanned yet.", @"Image Recreation Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
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
                    DialogResult overrideResult =
                        MessageBox
                            .Show(@"The current text output will be replaced with another." + Environment.NewLine + @"Do you want to continue?",
                                  @"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (overrideResult == DialogResult.Yes)
                    {
                        Reset();
                    }
                    else
                    {
                        return;
                    }
                }

                outputListBox.Items.AddRange(File.ReadAllLines(openFromTextFileDialog.FileName).Cast<object>().ToArray());
            }
        }

        private void exportToTextFileButton_Click(object sender, EventArgs e)
        {
            if (outputListBox.Items.Count > 0)
            {
                DialogResult saveResult = saveToTextFileDialog.ShowDialog();

                if (saveResult == DialogResult.OK)
                {
                    File.WriteAllLines(saveToTextFileDialog.FileName, outputListBox.Items.Cast<string>().ToArray());
                }
            }
            else
            {
                MessageBox.Show(@"There is nothing to export.", @"Exporting Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        private static void DrawLineHorizontal(Graphics g, Bitmap b, Color c, int y)
        {
            Pen pen = new Pen(c);

            g.Clear(Color.Empty);
            g.DrawImage(b, 0, 0);
            g.DrawLine(pen, 0, y, b.Width, y);
        }

        private struct BitmapProgress
        {
            private int imageLine;
            public readonly Bitmap currentBitmap;

            public BitmapProgress(int imageLine, Bitmap currentBitmap)
            {
                this.imageLine = imageLine;
                this.currentBitmap = currentBitmap;
            }
        }

        #region Converting images to text

        private string[] GetTextOfBitmap(Bitmap bmp, Func<Color, Color, bool> areSame, Func<Color, int, string> getColorString,
                                         IProgress<int> progress = null)
        {
            Color currentColor = Color.Empty;
            Color previousColor = Color.Empty;
            string[] stringArray = new string[bmp.Height];

            // Stops accessing of the bitmap during another thread
            int bitmapHeight = bmp.Height;
            int bitmapWidth = bmp.Width;

            for (int y = 0; y < bitmapHeight; y++)
            {
                int numContinousColors = 0;

                for (int x = 0; x < bitmapWidth; x++)
                {
                    bitmapSemaphore.Wait();

                    try
                    {
                        currentColor = bmp.GetPixel(x, y);
                    }
                    finally
                    {
                        bitmapSemaphore.Release();
                    }

                    if (x == 0)
                    {
                        previousColor = currentColor;
                    }

                    if (!areSame(currentColor, previousColor) || (x == (bitmapWidth - 1)))
                    {
                        stringArray[y] += getColorString(previousColor, numContinousColors);

                        if (x < (bitmapWidth - 1))
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

                progress?.Report(y);
            }

            return stringArray;
        }

        private async Task<string[]> ConvertToNumberedBinary(Bitmap bmp, IProgress<int> progress)
        {
            string[] bitmapText =
                await Task.Run(() =>
                                   GetTextOfBitmap(bmp,
                                                   (c1, c2) => ((c1.GetBrightness() <= 0.5) && (c2.GetBrightness() <= 0.5)) ||
                                                               ((c1.GetBrightness() > 0.5) && (c2.GetBrightness() > 0.5)),
                                                   GetBinaryColorString, progress));

            return bitmapText;
        }

        private static string GetBinaryColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return (col.GetBrightness() <= 0.5 ? "1" : "0") + ", " + numContinousColors;
            }
            return "";
        }

        private async Task<string[]> ConvertToNumberedBrightness(Bitmap bmp, IProgress<int> progress)
        {
            string[] bitmapText =
                await Task.Run(() =>
                                   GetTextOfBitmap(bmp,
                                                   (c1, c2) => (int)Math.Round(c1.GetBrightness() * 255) ==
                                                               (int)Math.Round(c2.GetBrightness() * 255),
                                                   GetBrightnessColorString, progress));

            return bitmapText;
        }

        private static string GetBrightnessColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return Math.Round(col.GetBrightness() * 255) + ", " + numContinousColors;
            }
            return "";
        }

        private async Task<string[]> ConvertToNumberedRgb(Bitmap bmp, IProgress<int> progress)
        {
            string[] bitmapText =
                await Task.Run(() => GetTextOfBitmap(bmp, (c1, c2) => (c1.R == c2.R) && (c1.G == c2.G) && (c1.B == c2.B),
                                                     GetRgbColorString, progress));

            return bitmapText;
        }

        private string GetRgbColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return "(" + col.R + ", " + col.G + ", " + col.B + ")" + ", " + numContinousColors;
            }
            return "";
        }

        private async Task<string[]> ConvertToNumberedArgb(Bitmap bmp, IProgress<int> progress)
        {
            string[] bitmapText = await Task.Run(() => GetTextOfBitmap(bmp, (c1, c2) => c1 == c2, GetArgbColorString, progress));

            return bitmapText;
        }

        private string GetArgbColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return "(" + col.A + ", " + col.R + ", " + col.G + ", " + col.B + ")" + ", " + numContinousColors;
            }
            return "";
        }

        private async Task<string[]> ConvertToNumberedHexadecimal(Bitmap bmp, IProgress<int> progress)
        {
            string[] bitmapText =
                await Task.Run(() => GetTextOfBitmap(bmp, (c1, c2) => c1 == c2, GetHexadecimalColorString, progress));

            return bitmapText;
        }

        private string GetHexadecimalColorString(Color col, int numContinousColors)
        {
            if (numContinousColors > 0)
            {
                return ColorTranslator.ToHtml(col) + ", " + numContinousColors;
            }
            return "";
        }

        #endregion

        #region Converting text to images

        private Bitmap GetBitmapFromText(string[] text, Func<string, Color> getColorFromString,
                                         IProgress<BitmapProgress> progress = null)
        {
            Bitmap bmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            int y = 0;

            foreach (string line in text)
            {
                int x = 0;

                foreach (string pixelString in line.Split(new[] {"; "}, StringSplitOptions.None))
                {
                    int lastCommaIndex = pixelString.LastIndexOf(", ", StringComparison.Ordinal);

                    int numOccurrences = int.Parse(pixelString.Substring(lastCommaIndex + ", ".Length));

                    Color color = getColorFromString(pixelString.Substring(0, lastCommaIndex));

                    for (int times = 0; times < numOccurrences; times++)
                    {
                        bitmapSemaphore.Wait();

                        try
                        {
                            bmp.SetPixel(x, y, color);
                        }
                        finally
                        {
                            bitmapSemaphore.Release();
                        }

                        x++;
                    }
                }

                progress?.Report(new BitmapProgress(y, bmp));

                y++;
            }

            return bmp;
        }

        private Color GetBinaryColorFromString(string colorString)
        {
            if (colorString == "0")
            {
                return Color.White;
            }
            return Color.Black;
        }

        private async Task<Bitmap> ConvertNumberedBinaryToBitmap(string[] imageText, IProgress<BitmapProgress> progress)
        {
            Bitmap bitmap = await Task.Run(() => GetBitmapFromText(imageText, GetBinaryColorFromString, progress));

            return bitmap;
        }

        private Color GetBrightnessColorFromString(string colorString)
        {
            int brightnessValue = int.Parse(colorString);

            return Color.FromArgb(brightnessValue, brightnessValue, brightnessValue);
        }

        private async Task<Bitmap> ConvertNumberedBrightnessToBitmap(string[] imageText, IProgress<BitmapProgress> progress)
        {
            Bitmap bitmap = await Task.Run(() => GetBitmapFromText(imageText, GetBrightnessColorFromString, progress));

            return bitmap;
        }

        private Color GetRgbColorFromString(string colorString)
        {
            int[] colorParts = colorString.Split(new[] {", ", "(", ")"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                                          .ToArray();

            return Color.FromArgb(colorParts[0], colorParts[1], colorParts[2]);
        }

        private async Task<Bitmap> ConvertNumberedRgbToBitmap(string[] imageText, IProgress<BitmapProgress> progress)
        {
            Bitmap bitmap = await Task.Run(() => GetBitmapFromText(imageText, GetRgbColorFromString, progress));

            return bitmap;
        }

        private Color GetArgbColorFromString(string colorString)
        {
            int[] colorParts = colorString.Split(new[] {", ", "(", ")"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                                          .ToArray();

            return Color.FromArgb(colorParts[0], colorParts[1], colorParts[2], colorParts[3]);
        }

        private async Task<Bitmap> ConvertNumberedArgbToBitmap(string[] imageText, IProgress<BitmapProgress> progress)
        {
            Bitmap bitmap = await Task.Run(() => GetBitmapFromText(imageText, GetArgbColorFromString, progress));

            return bitmap;
        }

        private Color GetHexadecimalColorFromString(string colorString)
        {
            return ColorTranslator.FromHtml(colorString);
        }

        private async Task<Bitmap> ConvertNumberedHexadecimalToBitmap(string[] imageText, IProgress<BitmapProgress> progress)
        {
            Bitmap bitmap = await Task.Run(() => GetBitmapFromText(imageText, GetHexadecimalColorFromString, progress));

            return bitmap;
        }

        #endregion
    }
}