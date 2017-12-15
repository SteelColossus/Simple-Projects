using System.Windows.Forms;

namespace Primes
{
    internal class MagnitudeNumericUpDown : NumericUpDown
    {
        public override void UpButton()
        {
            if ((Value * Increment) <= Maximum)
            {
                Value = Value * Increment;
            }
        }

        public override void DownButton()
        {
            if ((Value / Increment) >= Minimum)
            {
                Value = Value / Increment;
            }
        }
    }
}