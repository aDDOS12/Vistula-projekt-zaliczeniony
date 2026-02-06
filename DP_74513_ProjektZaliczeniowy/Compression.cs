using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_74513_ProjektZaliczeniowy
{
    // klasa z metodami kompresji i dekompresji algorytmu LZW
    internal class Compression
    {
        // metoda algorytmu kompresującego
        public static List<int> CompressionLZW(string uncompressed)
        {
            // budowanie słownika opartego na ASCII
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(((char)i).ToString(), i);

            string w = string.Empty;
            List<int> compressed = new List<int>();
            // sprawdzamy czy słowo istnieje w słowniku
            foreach (char c in uncompressed)
            {
                // jeśli istnieje to przechodzimy dalej
                string wc = w + c;
                if (dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                // jeśli nie isntieje to tworzymy słowo kodowe
                else
                {
                    // dodaj w do outputu
                    compressed.Add(dictionary[w]);
                    // wc - nowe słowo kodowe, dodajemy do słownika
                    dictionary.Add(wc, dictionary.Count);
                    w = c.ToString();
                }
            }
            // sprawdzamy czy pozostały jeszcze znaki w ciągu
            if (!string.IsNullOrEmpty(w))
               compressed.Add(dictionary[w]);
            // zwracamy skompresowany ciąg
            return compressed;
        }
        // metoda algorytmu dekompresującego
        public static string DecompressionLZW(List<int> compressed)
        {
            // budowanie słownika do dekompresji opartego na ASCII
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(i, ((char)i).ToString());
            // pobieramy słowa robocze
            string w = dictionary[compressed[0]];
            compressed.RemoveAt(0);
            StringBuilder decompressed = new StringBuilder(w);
            // sprawdzamy czy słowo istnieje w słowniku
            foreach (int k in compressed)
            {
                // jeśli istnieje to przechodzimy do dekompresji
                string entry = null;
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                // jeśli nie istnieje to tworzymy nowe słowo kodowe
                else if (k == dictionary.Count)
                    entry = w + w[0];

                decompressed.Append(entry);

                dictionary.Add(dictionary.Count, w + entry[0]);

                w = entry;
            }
            // zwracamy zdekompresowany ciąg
            return decompressed.ToString();
        }
    }
}
