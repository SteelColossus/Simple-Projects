using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Base_Calculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static string TransformToBase(string numStr, int baseValue)
        {
            if (!long.TryParse(numStr, out long num))
            {
                return null;
            }
            else if (num == long.MinValue)
            {
                return null;
            }

            int power = 0;
            StringBuilder output = new StringBuilder();

            while (Math.Pow(baseValue, power + 1) <= num)
            {
                power++;
            }

            for (int lPower = power; lPower >= 0; lPower--)
            {
                int numDigit = 0;

                while (num >= Math.Pow(baseValue, lPower))
                {
                    num -= (long)Math.Pow(baseValue, lPower);
                    numDigit++;
                }

                string digitValue = numDigit.ToString();
                string[] charDigitValues = {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K"};

                if (numDigit >= 10)
                {
                    digitValue = charDigitValues[numDigit - 10];
                }

                output.Append(digitValue);
            }

            return output.ToString();
        }

        private static string TransformFromBase(string numStr, int baseValue)
        {
            long total = 0;
            List<string> charDigitValues = new List<string> {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K"};

            if (numStr.Length == 0)
            {
                return null;
            }

            for (int digit = 0; digit < numStr.Length; digit++)
            {
                string digitStr = numStr[digit].ToString();

                if (!int.TryParse(digitStr, out int digitNum))
                {
                    digitNum = 10 + charDigitValues.IndexOf(digitStr);

                    if (digitNum == 9)
                    {
                        return null;
                    }
                }

                if (digitNum >= baseValue)
                {
                    return null;
                }

                long addition = (long)Math.Pow(baseValue, numStr.Length - 1 - digit) * digitNum;

                if (addition == long.MinValue)
                {
                    return null;
                }

                total += addition;
            }

            return total.ToString();
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            RefreshResult();
        }

        private void baseNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RefreshResult();
        }

        private void oppositeBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshResult();
        }

        private void RefreshResult()
        {
            string num = inputTextBox.Text;
            int baseNum = (int)baseNumericUpDown.Value;
            outputTextBox.Text = oppositeBox.Checked ? TransformFromBase(num, baseNum) : TransformToBase(num, baseNum);
        }
    }
}