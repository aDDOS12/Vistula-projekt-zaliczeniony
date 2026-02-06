using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_74513_ProjektZaliczeniowy
{
    // klasa z metodą algorytmu matematycznego roszerzonego Euklidesa
    internal class Mathematical
    {
        // metoda z algorytmem rozszerzonym Euklidesa
        public static int EuclideanExtended(int a, int b, ref int x, ref int y)
        {
            // wersja rozszerzona algorytmu, wylicza wartość x i y ze wzoru a * x + b * y = GCD(a , b)
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int x1 = 0, y1 = 0;
            int gcd = EuclideanExtended(b % a, a, ref x1, ref y1);

            x = y1 - (b / a) * x1;
            y = x1;
            return gcd;
        }
    }
}
