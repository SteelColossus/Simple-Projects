using System;
using System.Drawing;
using System.Windows.Forms;

namespace PictureManipulation
{
    public partial class Form1 : Form
    {
        private Bitmap inputBitmap;
        private Bitmap currentBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetNewImage(Bitmap newBitmap)
        {
            currentBitmap = newBitmap;
            pictureBox.Image = currentBitmap;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            DialogResult loadResult = imageOpenFileDialog.ShowDialog();

            if (loadResult == DialogResult.OK)
            {
                inputBitmap = BitmapHelper.Load(imageOpenFileDialog.FileName, 1920, 1440);

                currentBitmap = inputBitmap;
                pictureBox.Image = currentBitmap;
            }
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            SetNewImage(inputBitmap);
        }

        private void EditImage(Func<Color, Color> colorFunc)
        {
            EditImage((x, y, bmp) => colorFunc(bmp.GetPixel(x, y)));
        }

        private void EditImage(Func<int, int, Bitmap, Color> colorFunc)
        {
            Bitmap newBitmap = new Bitmap(currentBitmap);

            for (int y = 0; y < currentBitmap.Height; y++)
            {
                for (int x = 0; x < currentBitmap.Width; x++)
                {
                    newBitmap.SetPixel(x, y, colorFunc(x, y, newBitmap));
                }
            }

            SetNewImage(newBitmap);
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            EditImage(c => Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
        }

        private void redFilterButton_Click(object sender, EventArgs e)
        {
            EditImage(c => Color.FromArgb(255, c.G, c.B));
        }

        private void greenFilterButton_Click(object sender, EventArgs e)
        {
            EditImage(c => Color.FromArgb(c.R, 255, c.B));
        }

        private void blueFilterButton_Click(object sender, EventArgs e)
        {
            EditImage(c => Color.FromArgb(c.R, c.G, 255));
        }

        private void cycleColoursButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            Color ColorCycle(Color c)
            {
                int randNum = rand.Next(4);

                switch (randNum)
                {
                    case 0:
                        return Color.FromArgb(c.G, c.B, c.R);
                    case 1:
                        return Color.FromArgb(c.B, c.R, c.G);
                    case 2:
                        return Color.FromArgb(c.G, c.R, c.G);
                    case 3:
                        return Color.FromArgb(c.B, c.G, c.R);
                    default:
                        return Color.Empty;
                }
            }

            EditImage(ColorCycle);
        }

        private void flipButton_Click(object sender, EventArgs e)
        {
            Color FlipColor(int x, int y, Bitmap bmp)
            {
                Color c = currentBitmap.GetPixel(currentBitmap.Width - x - 1, currentBitmap.Height - y - 1);

                return Color.FromArgb(c.R, c.G, c.B);
            }

            EditImage(FlipColor);
        }

        private void interferenceButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            Color InterfereColor(Color c)
            {
                int randNum = rand.Next(4);

                switch (randNum)
                {
                    case 0:
                        return Color.Gray;
                    case 1:
                        return Color.LightGray;
                    default:
                        return c;
                }
            }

            EditImage(InterfereColor);
        }

        private void averageButton_Click(object sender, EventArgs e)
        {
            int avgFactor = (int)(valueUpDown.Value * ((decimal)currentBitmap.Width / 1920));

            Color AverageColor(int x, int y, Bitmap bmp)
            {
                float totR = 0;
                float totG = 0;
                float totB = 0;

                int numPixelsAvgd = 0;

                for (int ya = y - avgFactor; ya <= (y + avgFactor); ya++)
                {
                    if (ya < 0 || ya >= currentBitmap.Height) continue;

                    for (int xa = (x - avgFactor); xa <= (x + avgFactor); xa++)
                    {
                        if (xa < 0 || xa >= currentBitmap.Width)
                        {
                            continue;
                        }

                        numPixelsAvgd++;

                        Color c = currentBitmap.GetPixel(xa, ya);

                        totR += c.R;
                        totG += c.G;
                        totB += c.B;
                    }
                }

                Color avgColor = Color.FromArgb((int)Math.Round(totR / numPixelsAvgd), (int)Math.Round(totG / numPixelsAvgd),
                                                (int)Math.Round(totB / numPixelsAvgd));

                return avgColor;
            }

            EditImage(AverageColor);
        }

        private void pixelButton_Click(object sender, EventArgs e)
        {
            Bitmap newBitmap = new Bitmap(currentBitmap);
            int pixelFactor = (int)(valueUpDown.Value * (decimal)(currentBitmap.Width / 1920.0));

            if (pixelFactor < 1) pixelFactor = 1;

            for (int y = 0; y < currentBitmap.Height; y += pixelFactor)
            {
                for (int x = 0; x < currentBitmap.Width; x += pixelFactor)
                {
                    Color mainC = currentBitmap.GetPixel(x, y);

                    for (int yp = y; yp <= (y + pixelFactor); yp++)
                    {
                        if (yp < 0 || yp >= currentBitmap.Height)
                            continue;

                        for (int xp = x; xp <= (x + pixelFactor); xp++)
                        {
                            if (xp < 0 || xp >= currentBitmap.Width)
                            {
                                continue;
                            }

                            newBitmap.SetPixel(xp, yp, mainC);
                        }
                    }
                }
            }

            SetNewImage(newBitmap);
        }
    }
}
