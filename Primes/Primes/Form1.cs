using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Primes
{
    public partial class Form1 : Form
    {
        private List<int> primes;

        public Form1()
        {
            InitializeComponent();
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;

            primeOutputBox.Items.Clear();

            primes = new List<int>((int)(limitNumericUpDown.Value / 2) + 1);

            for (int num = 2; num <= limitNumericUpDown.Value; num++)
            {
                if (IsPrime(num))
                {
                    primes.Add(num);
                    primeOutputBox.Items.Add(num);
                }
            }

            totalBox.Text = primes.Count.ToString();
            percentBox.Text = (primes.Count / limitNumericUpDown.Value).ToString("P2");

            DateTime end = DateTime.Now;
            TimeSpan time = end - start;

            string message = "Took a total time of ";

            if (time.TotalMinutes >= 1)
            {
                message += $"{time.TotalMinutes:N2} minutes.";
            }
            else if (time.TotalSeconds >= 1)
            {
                message += $"{time.TotalSeconds:N2} seconds.";
            }
            else
            {
                message += "less than 1 second.";
            }

            MessageBox.Show(message, @"Time taken", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult ans = MessageBox.Show(@"Do you want to save the result?", @"Save result", MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (ans == DialogResult.Yes)
            {
                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, string.Join(",", primes));
                }
            }
        }

        private bool IsPrime(int num)
        {
            foreach (int divisor in primes)
            {
                if ((num % divisor) == 0)
                {
                    return false;
                }
                if (num < (divisor * divisor))
                {
                    break;
                }
            }

            return true;
        }
    }
}