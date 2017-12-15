using System;
using System.Numerics;

namespace Brown_Numbers
{
    public class Program
    {
        private static readonly int[] preCalc = {
            8, 7, 6, 6, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2,
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
        };

        public static int Log2(BigInteger bi)
        {
            byte[] buf = bi.ToByteArray();
            int len = buf.Length;
            return (len * 8) - preCalc[buf[len - 1]] - 1;
        }

        public static BigInteger Sqrt(BigInteger bi)
        {
            return new BigInteger(1) << (Log2(bi) / 2);
        }

        public static BigInteger Factorial(int num)
        {
            BigInteger total = 1;

            for (int i = 1; i <= num; i++)
            {
                total *= i;
            }

            return total;
        }

        private static void Main()
        {
            for (int n = 1; n <= 100; n++)
            {
                BigInteger factorial = Factorial(n);
                BigInteger limit = factorial / Sqrt(factorial);
                BigInteger m = 1;
                bool found = false;

                while (m <= limit)
                {
                    if ((m * m) == (factorial + 1))
                    {
                        found = true;
                        break;
                    }

                    m++;
                }

                if (found)
                {
                    Console.WriteLine(m + ", " + n);
                }
                else
                {
                    Console.WriteLine(n + " - No");
                }
            }

            Console.ReadLine();
        }
    }
}