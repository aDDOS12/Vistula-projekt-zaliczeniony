using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_74513_ProjektZaliczeniowy
{
    // klasa z metodą algorytmu sortującego
    internal class Sorting
    {
        // metoda algorytmu sortującego
        public static void BubbleSort(int[] arr, int n)
        {
            // pętla licząca "przebiegi", ilość porównań
            int i, j, temp;
            bool swapped;
            for (i = 0; i < n - 1; i++)
            {
                // pętla porównująca
                swapped = false;
                for (j = 0; j < n - i - 1; j++)
                {
                    // zamieniamy miejscami porównywane wartości jeśli druga liczba jest większa od pierwszej i zwracamy informacje do programu
                    if (arr[j] > arr[j + 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                // jeśli wartości są już prawidłowo posortowane, tzn. są już zamienione miejscami to zwracamy tą informacje do programu i przerywamy pętle porównującą
                if (swapped == false)
                    break;
            }
        }
    }
}
