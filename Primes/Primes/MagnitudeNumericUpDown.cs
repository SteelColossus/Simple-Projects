using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Primes
{
    class MagnitudeNumericUpDown : NumericUpDown
    {
        public override void UpButton()
        {
            if (Value * Increment <= Maximum)
            {
                Value = Value * Increment;
            }
        }

        public override void DownButton()
        {
            if (Value / Increment >= Minimum)
            {
                Value = Value / Increment;
            }
        }
    }
}
