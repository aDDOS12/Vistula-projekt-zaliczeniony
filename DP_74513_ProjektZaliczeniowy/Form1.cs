using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace DP_74513_ProjektZaliczeniowy
{
    // Klasa - główne okno i funkcjonalność interfejsu
    public partial class Form1 : Form
    {
        // instancja konstruktora kontrolek
        private Controlls crl;
        // panel roboczy
        public static Panel workPanel;
        // konstruktor, wywołanie metody konstruktora kontrolek, zadeklarowanie panelu roboczego i instacji konstruktora
        public Form1()
        {
            InitializeComponent();

            crl = new Controlls();

            workPanel = this.WorkPanel;

            LoadControlls();
        }
        // metoda budująca szkielet intefesju
        private void LoadControlls()
        {
            // panel roboczy z UI do obsługi algorytmów
            GroupBox Gb_WorkPanel = crl.Create_GroupBox(15, 10, 900, 600, "Work Panel", "GbWorkPanel");
            workPanel.Controls.Add(Gb_WorkPanel);

            // panel boczny z przyciskami do wyboru algorytmu
            GroupBox Gb_Algorithms = crl.Create_GroupBox(925, 10, 160, 600, "Algorithms", "GbAlgorithms");
            workPanel.Controls.Add(Gb_Algorithms);

            // panel dolny z kontrolkami, przycisk zamknięcia programu i wyczyszczenia panelu roboczego
            GroupBox Gb_Controls = crl.Create_GroupBox(15, 625, 1070, 100, "Controls", "GbControls");
            workPanel.Controls.Add(Gb_Controls);

            // deklarujemy zmienne, czcionka i kolor dla kontrolek
            Font font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            Color foreColor = Color.Black;
            Color backColor = Color.FromKnownColor(KnownColor.Control);

            // tworzenie przycisku z wyborem algorytmu kompresji, tworzenie handlerów obsługujących funkcjonalność
            Button buttonCompression = crl.CreateButton("BtCompression", 10, 30, 140, 30, font, foreColor, backColor, "KOMPRESJA");
            buttonCompression.MouseClick += BtCompGenerate_MouseClick;
            buttonCompression.MouseHover += BtCommon_MouseHover;
            buttonCompression.MouseLeave += BtCommon_MouseLeave;
            Gb_Algorithms.Controls.Add(buttonCompression); // ładujemy przycisk do interfejsu

            // tworzenie przycisku z wyborem algorytmu sortującego, tworzenie handlerów obsługujących funkcjonalność
            Button buttonSorting = crl.CreateButton("BtSorting", 10, 80, 140, 30, font, foreColor, backColor, "SORTOWANIE");
            buttonSorting.MouseClick += BtSortGenerate_MouseClick;
            buttonSorting.MouseHover += BtCommon_MouseHover;
            buttonSorting.MouseLeave += BtCommon_MouseLeave;
            Gb_Algorithms.Controls.Add(buttonSorting); // ładujemy przycisk do interfejsu

            // tworzenie przycisku z wyborem algorytmu matematycznego, tworzenie handlerów obsługujących funkcjonalność
            Button buttonMathematical = crl.CreateButton("BtMath", 10, 130, 140, 30, font, foreColor, backColor, "MATEMATYCZNY");
            buttonMathematical.MouseClick += BtMathGenerate_MouseClick;
            buttonMathematical.MouseHover += BtCommon_MouseHover;
            buttonMathematical.MouseLeave += BtCommon_MouseLeave;
            Gb_Algorithms.Controls.Add(buttonMathematical); // ładujemy przycisk do interfejsu

            // tworzenie przycisku czyszczenia panelu roboczego, tworzenie handlerów obsługujących funkcjonalność
            Button buttonClear = crl.CreateButton("BtClear", 25, 30, 120, 50, font, foreColor, backColor, "WYCZYŚĆ");
            buttonClear.MouseClick += BtClear_MouseClick;
            buttonClear.MouseHover += BtCommon_MouseHover;
            buttonClear.MouseLeave += BtCommon_MouseLeave;
            Gb_Controls.Controls.Add(buttonClear); // ładujemy przycisk do interfejsu

            // tworzenie przycisku zamknięcia programu, tworzenie handlerów obsługujących funkcjonalność
            Button buttonExit = crl.CreateButton("BtExit", 925, 30, 120, 50, font, foreColor, backColor, "ZAMKNIJ");
            buttonExit.MouseClick += BtExit_MouseClick;
            buttonExit.MouseHover += BtCommon_MouseHover;
            buttonExit.MouseLeave += BtCommon_MouseLeave;
            Gb_Controls.Controls.Add(buttonExit); // ładujemy przycisk do interfejsu
        }

        // metoda wspólna dla przycisków, obsługująca "podświetlenie" po najechaniu kursorem myszki na przycisk
        private void BtCommon_MouseHover(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.BackColor = Color.DeepSkyBlue;
            }
        }

        // metoda wspólna dla przycisków, obsługująca wyłączenie "podświetlenia" po zabraniu kursosa myszki znad przycisku
        private void BtCommon_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }

        // metoda wywołująca zamknięcie programu po wciśnięciu przycisku zamknięcia
        private void BtExit_MouseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        // metoda czyszczenia panelu roboczego po kliknięciu przycisku
        private void BtClear_MouseClick(object sender, MouseEventArgs e)
        {
            // deklarowanie zmiennej roboczej
            GroupBox groupBox = null;

            // szukanie panelu roboczego
            Control[] found = this.Controls.Find("GbWorkPanel", true);
            // sprawdzamy czy w panelu roboczym znajdują się jakieś elementy
            if (found != null && found.Length > 0)
            {
                groupBox = found[0] as GroupBox;
            }

            if (groupBox != null && groupBox.Controls.Count != 0)
            {
                groupBox.Controls.Clear();
            }
        }

        // metoda generująca interfejs obsługujący algorytm kompresji i dekompresji
        private void BtCompGenerate_MouseClick(Object sender, EventArgs e)
        {
            // znajdź panel roboczy w programie
            GroupBox groupBoxWork = workPanel.Controls["GbWorkPanel"] as GroupBox;
            if (groupBoxWork == null) return;

            // wyczyszczenie panelu roboczego, uniknięcie nałożenia na siebie interfejsów
            foreach (Control control in groupBoxWork.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }
            groupBoxWork.Controls.Clear();

            // deklarowanie zmiennych dla elementów graficznych interfejsu
            Font font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            Font titleFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            Color foreColor = Color.Black;
            Color backColor = Color.FromKnownColor(KnownColor.Control);

            int x = 20;
            int y = 80;
            int rowGap = 55;

            int labelWidth = 180;
            int labelHeight = 25;

            int buttonWidth = 120;
            int buttonHeight = 25;
            int textBoxHeight = 25;

            int textBoxX = x + labelWidth + 15;
            int rightMargin = 20;
            int buttonGap = 15;

            // wyliczanie szerokości textboxa tak żeby zmieścić przyciski po prawej stronie
            int textBoxWidth = groupBoxWork.ClientSize.Width - textBoxX - buttonGap - buttonWidth - rightMargin;

            if (textBoxWidth < 250) textBoxWidth = 250;
            int buttonX = textBoxX + textBoxWidth + buttonGap;

            // tworzenie labela z nazwą algorytmu na górze
            Label title = crl.Create_Label(
                "LabelCompTitle",
                new Point(0, 20),
                titleFont,
                backColor,
                foreColor,
                30,
                groupBoxWork.ClientSize.Width,
                "Kompresja LZW");

            title.TextAlign = ContentAlignment.MiddleCenter;

            // tworzenie labela z opisem po lewej stronie textboxa
            Label labelInput = crl.Create_Label(
                "LabelCompInput",
                new Point(x, y),
                font,
                backColor,
                foreColor,
                labelHeight,
                labelWidth,
                "Łańcuch znaków do kompresji");

            // tworzenie textboxa inputu
            TextBox textBoxInput = crl.Create_TextBox(
                "TextBoxCompInput",
                new Point(textBoxX, y),
                textBoxWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor);

            // wywołanie metody blokującej wpisywanie cyfer
            textBoxInput.KeyPress += IntBlock;

            // tworzenie przycisku do kompresji
            Button buttonCompress = crl.CreateButton(
                "ButtonCompress",
                buttonX,
                y,
                buttonWidth,
                buttonHeight,
                font,
                foreColor,
                backColor,
                "KOMPRESUJ");

            // handlery funkcjonalności przycisku
            buttonCompress.MouseClick += BtCompress_MouseClick;
            buttonCompress.MouseHover += BtCommon_MouseHover;
            buttonCompress.MouseLeave += BtCommon_MouseLeave;

            // tworzenie labela z opisem dla kolejnego pola
            Label labelCompressed = crl.Create_Label(
                "LabelCompCompressed",
                new Point(x, y + rowGap),
                font,
                backColor,
                foreColor,
                labelHeight,
                labelWidth,
                "Skompresowany łańcuch");

            // tworzenie textboxa z outputem skompresowanego ciągu
            TextBox textBoxCompressed = crl.Create_TextBox(
                "TextBoxCompCompressed",
                new Point(x + labelWidth + 15, y + rowGap),
                textBoxWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor);
            textBoxCompressed.ReadOnly = true; // textbox tylko do odczytu, brak możliwości ręcznej edycji przez użytkownika
            textBoxCompressed.TabStop = true;

            // tworzenie przycisku do dekompresji
            Button buttonDecompress = crl.CreateButton(
                "ButtonDecompress",
                buttonX,
                y + rowGap,
                buttonWidth,
                buttonHeight,
                font,
                foreColor,
                backColor,
                "ZDEKOMPRESUJ");

            // handlery funkcjonalności przycisku
            buttonDecompress.MouseClick += BtDecompress_MouseClick;
            buttonDecompress.MouseHover += BtCommon_MouseHover;
            buttonDecompress.MouseLeave += BtCommon_MouseLeave;

            // tworzenie labela opisowego dla ostatniego textboxa
            Label labelDecompressed = crl.Create_Label(
                "LabelCompDecompressed",
                new Point(x, y + 2 * rowGap),
                font,
                backColor,
                foreColor,
                labelHeight,
                labelWidth,
                "Zdekompresowany łańcuch");

            // textbox z outputem zdekompresowanego ciągu
            TextBox textBoxDecompressed = crl.Create_TextBox(
                "TextBoxCompDecompressed",
                new Point(textBoxX, y + 2 * rowGap),
                textBoxWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor);
            textBoxDecompressed.ReadOnly = true; // textbox tylko do odczytu, brak możliwości ręcznej edycji przez użytkownika
            textBoxDecompressed.TabStop = true;

            // załadowanie wszystkich elementów
            groupBoxWork.Controls.Add(title);

            groupBoxWork.Controls.Add(labelInput);
            groupBoxWork.Controls.Add(textBoxInput);
            groupBoxWork.Controls.Add(buttonCompress);

            groupBoxWork.Controls.Add(labelCompressed);
            groupBoxWork.Controls.Add(textBoxCompressed);
            groupBoxWork.Controls.Add(buttonDecompress);

            groupBoxWork.Controls.Add(labelDecompressed);
            groupBoxWork.Controls.Add(textBoxDecompressed);
        }

        // metoda wywołująca algorytm kompresji i przekazująca dane do algorytmu
        private void BtCompress_MouseClick(object sender, MouseEventArgs e)
        {
            // wyszukanie textboxa z danymi do kompresji
            TextBox textBoxInput = FindTextBox("TextBoxCompInput");
            TextBox textBoxCompressed = FindTextBox("TextBoxCompCompressed");

            // sprawdzenie czy textboxy nie są puste
            if (textBoxInput == null || textBoxCompressed == null) return;

            string input = textBoxInput.Text;
            
            // w przypadku pustych lub niepoprawnych danych wyświetlamy okno dialogowe z informacją dla użytkownika
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show(
                    "Wpisz ciąg znaków do kompresji.",
                    "Brak danych wejściowych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (input.Any(ch => ch > 255))
            {
                MessageBox.Show(
                    "Wprowadź znaki bez polskich znaków",
                    "Nieobsługiwane znaki",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // wywołanie metody kompresji
            List<int> codes = Compression.CompressionLZW(input);
            textBoxCompressed.Text = FormatLzwCodes(codes); // formatowanie kodu do danych typu string i przekazanie do metody formatującej output
        }

        // metoda wywołująca algorytm dekompresji i przekazywanie danych
        private void BtDecompress_MouseClick(object sender, MouseEventArgs e)
        {
            // wyszukanie odpowiedniego textboxa
            TextBox textBoxCompressed = FindTextBox("TextBoxCompCompressed");
            TextBox textBoxDecompressed = FindTextBox("TextBoxCompDecompressed");

            // sprawdzenie czy textbox jest pusty
            if (textBoxCompressed == null || textBoxDecompressed == null) return;

            // jest textbox jest pusty lub doszło do błędu programu i wypisane zostały błędne dane, informujemy o tym użytkownika oknem dialogowym z informacją
            if (string.IsNullOrEmpty(textBoxCompressed.Text))
            {
                MessageBox.Show(
                    "Brak danych do dekompresji (pole 'Skompresowany łańcuch' jest puste).",
                    "Brak danych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!TryParseLzwCodes(textBoxCompressed.Text, out List<int> codes))
            {
                MessageBox.Show(
                    "Niepoprawny format skompresowanych danych. \n",
                    "Błąd danych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (codes.Count == 0)
            {
                MessageBox.Show(
                    "Nie można zdekompresować puste listy kodów.",
                    "Błąd",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // wywołanie metody dekompresji i wypisanie
            string decompressed = Compression.DecompressionLZW(codes);
            textBoxDecompressed.Text = decompressed;
        }

        // metoda wyszukująca odpowiedni textbox
        private TextBox FindTextBox(string name)
        {
            // wyszukujemy danego typu kontrolki po odpowiedniej nazwie
            Control[] found = this.Controls.Find(name, true);
            if (found != null && found.Length > 0)
                return found[0] as TextBox;
            return null;
        }
        
        // metoda formatująca dane wyjściowe do wypisania
        private string FormatLzwCodes(List<int> codes)
        {
            return string.Join(" ", codes);
        }

        // metoda formatująca tekst z outputem kompresji na listę gotową do użycia przez resztę programu
        private bool TryParseLzwCodes(string text, out List<int> codes)
        {
            // utworzenie listy i przekazanie skompresowaneg outputu
            codes = new List<int>();
            
            // rozdzielenie outputu przecinkami
            string[] parts = text
                .Split(new char[] { ' ', ',', ';', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // zamiana danych na int
            foreach (string p in parts)
            {
                // sprawdzenie czy operacja udała się
                if (!int.TryParse(p, out int value))
                    return false;

                codes.Add(value);
            }
            return true;
        }
        // metoda generująca interfejs graficzny do obsługi algorytmu sortującego
        private void BtSortGenerate_MouseClick(object sender, MouseEventArgs e)
        {
            // znajdowanie panelu roboczego
            GroupBox groupBoxWork = workPanel.Controls["GbWorkPanel"] as GroupBox;
            if (groupBoxWork == null) return;

            // czyszczenie panelu roboczego
            foreach (Control control in groupBoxWork.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }
            groupBoxWork.Controls.Clear();

            // deklaracja zmiennych dla elementów graficznych i kontrolek
            Font font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            Font titleFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            Color foreColor = Color.Black;
            Color backColor = Color.FromKnownColor(KnownColor.Control);

            int startX = 20;
            int startY = 110;
            int x = startX;
            int y = startY;
            int columnGap = 10;
            int rowGap = 25;
            int labelHeight = 18;
            int textBoxWidth = 70;
            int textBoxHeight = 25;
            int maxX = groupBoxWork.ClientSize.Width - 20;

            // tworzenie labela z nazwą tytułową na górze
            Label title = crl.Create_Label(
                "LabelSortTitle",
                new Point(0, 20),
                titleFont,
                backColor,
                foreColor,
                30,
                groupBoxWork.ClientSize.Width,
                "Bubble Sort");

            title.TextAlign = ContentAlignment.MiddleCenter;

            // zmienne dla lokalizacji przycisku pod nazwą tytułową
            int sortButtonWidth = 120;
            int sortButtonHeight = 30;
            int sortButtonX = (groupBoxWork.ClientSize.Width - sortButtonWidth) / 2;
            int sortButtonY = 60;

            // tworzenie przycisku do sortowania
            Button buttonSort = crl.CreateButton(
                "ButtonSort",
                sortButtonX,
                sortButtonY,
                sortButtonWidth,
                sortButtonHeight,
                font,
                foreColor,
                backColor,
                "SORTUJ");

            // handlery funkcjonalności
            buttonSort.MouseClick += BtSort_MouseClick;
            buttonSort.MouseHover += BtCommon_MouseHover;
            buttonSort.MouseLeave += BtCommon_MouseLeave;

            // pętla generująca 10 pól tekstowych z podpisami do których użytkownik wpisuje swojego liczby
            // pętla sprawdza czy pola tekstowe nie wychodzą poza ramkę panelu roboczego
            // w takim przypadku kolejne pola tekstowe są generowane linijke niżej
            for (int i = 1; i <= 10; i++)
            {
                if (x + textBoxWidth > maxX)
                {
                    y += labelHeight + textBoxHeight + rowGap;
                }

                // tworzenie labeli z podpisem według wzoru
                Label label = crl.Create_Label(
                    $"LabelSortNum{i}",
                    new Point(x, y),
                    font,
                    backColor,
                    foreColor,
                    labelHeight,
                    textBoxWidth,
                    $"Liczba {i}");

                // tworzenie pól tekstowych według wzoru
                TextBox textBox = crl.Create_TextBox(
                    $"TextBoxSortNum{i}",
                    new Point(x, y + labelHeight + 2),
                    textBoxWidth,
                    textBoxHeight,
                    font,
                    Color.White,
                    foreColor);

                // wywołanie metody blokującej wpisywanie znaków innych niż cyfry
                textBox.KeyPress += StringBlock;

                // ładujemy elementy do panelu roboczego
                groupBoxWork.Controls.Add(label);
                groupBoxWork.Controls.Add(textBox);

                x += textBoxWidth + columnGap;
            }

            // deklarowanie zmiennych dla pola tekstowego z outputem
            int inputBottom = y + labelHeight + 2 + textBoxHeight;
            int outY = inputBottom + 50;
            int outLabelWidth = 160;
            int outTextBoxWidth = groupBoxWork.ClientSize.Width - startX * 2 - outLabelWidth - 15;
            if (outTextBoxWidth < 200) outTextBoxWidth = 200;

            // tworzenie labela z podpisem
            Label labelOut = crl.Create_Label(
                "LabelSortOutput",
                new Point(startX, outY),
                font,
                backColor,
                foreColor,
                textBoxHeight,
                outLabelWidth,
                "Posortowane liczby");

            // tworzenie pola tekstowego z outputem
            TextBox textBoxOut = crl.Create_TextBox(
                "TextBoxOutput",
                new Point(startX + outLabelWidth + 15, outY),
                outTextBoxWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor);

            // pole tekstowe tylko do odczytu, brak możliwości edytowania przez użytkownika
            textBoxOut.ReadOnly = true;
            textBoxOut.TabStop = false;

            // załadowanie elementów do pola roboczego
            groupBoxWork.Controls.Add(labelOut);
            groupBoxWork.Controls.Add(textBoxOut);

            groupBoxWork.Controls.Add(title);
            groupBoxWork.Controls.Add(buttonSort);
        }

        // metoda wywołująca algorytm sortujący i przekazująca dane
        private void BtSort_MouseClick(object sender, MouseEventArgs e)
        {
            // tworzenie tablicy z 10 liczbami które podał użytkownika
            int[] numbers = new int[10];

            // sprawdzenie wszystkich pól tekstowych, jeśli któreś jest puste lub ma niepoprawne dane to generujemy okno dialogowe z odpowiednia informacją dla użytkownika
            for (int i = 1; i <= 10; i++)
            {
                TextBox textBox = FindTextBox($"TextBoxSortNum{i}");
                if (textBox == null) return;

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show(
                        "Wpisz wszystkie 10 liczb przed sortowaniem",
                        "Brak danych",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    textBox.Focus();
                    return;
                }

                if (!int.TryParse(textBox.Text, out int value))
                {
                    MessageBox.Show(
                        $"NIepoprawna wartość w polu Liczba {i}. Wpisz liczbę całkowitą",
                        "Błąd danych",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    textBox.Focus();
                    textBox.SelectAll();
                    return;
                }

                numbers[i - 1] = value;
            }

            // wywołanie algorytmu sortującego
            Sorting.BubbleSort(numbers, numbers.Length);

            // odszukujemy pole tekstowe z outputem, sprawdzamy czy jest puste i wypisujemy posortowane liczby
            TextBox textBoxOutput = FindTextBox("TextBoxOutput");
            if (textBoxOutput == null) return;

            textBoxOutput.Text = string.Join(", ", numbers);
        }

        // metoda generująca interfejs do obsługi algorytmu matematycznego
        private void BtMathGenerate_MouseClick(object sender, MouseEventArgs e)
        {
            // wyszukujemy panel roboczy
            GroupBox groupBoxWork = workPanel.Controls["GbWorkPanel"] as GroupBox;
            if (groupBoxWork == null) return;

            // czyścimy panel roboczy
            foreach (Control control in groupBoxWork.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }
            groupBoxWork.Controls.Clear();

            // zmienne dla elementów interfejsu graficznego
            Font font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            Font titleFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            Color foreColor = Color.Black;
            Color backColor = Color.FromKnownColor(KnownColor.Control);

            int x1 = 20;
            int y = 90;
            int labelHeight = 18;
            int textBoxHeight = 25;
            int gapX = 40;
            int inputWidth = 260;

            // tworzenie labela z napisem tytułowym na górze
            Label title = crl.Create_Label(
                "LabelMathTitle",
                new Point(0, 20),
                titleFont,
                backColor,
                foreColor,
                30,
                groupBoxWork.ClientSize.Width,
                "Rozszerzony Algorytm Euklidesa");

            title.TextAlign = ContentAlignment.MiddleCenter;

            // tworzenie labela z opisem dla pierwszego pola tekstowego
            Label labelA = crl.Create_Label(
                "LabelMathA",
                new Point(x1, y),
                font,
                backColor,
                foreColor,
                labelHeight,
                inputWidth,
                "Pierwsza liczba całkowita dodatnia");

            // tworzenie pierwszego pola tekstowego
            TextBox textBoxA = crl.Create_TextBox(
                "TextBoxMathA",
                new Point(x1, y + labelHeight + 2),
                inputWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor
            );

            // wywołanie metody blokującej wpisywanie znaków innych niż liczby dodatnie
            textBoxA.KeyPress += PositiveStringBlock;

            // zmienna licząca przerwe między pozycją pierwszego i drugiego pola
            int x2 = x1 + inputWidth + gapX;

            // tworzenie labela z opisem dla drugiego pola tekstowego
            Label labelB = crl.Create_Label(
                "LebolMathB",
                new Point(x2, y),
                font,
                backColor,
                foreColor,
                labelHeight,
                inputWidth,
                "Druga liczba całkowita dodatnia");

            // tworzenie drugiego pola tekstowego
            TextBox textBoxB = crl.Create_TextBox(
                "TextBoxMathB",
                new Point(x2, y + labelHeight + 2),
                inputWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor);

            // wywołanie metody blokującej wpisywanie znaków innych niż liczby dodatnie
            textBoxB.KeyPress += PositiveStringBlock;

            // zmienne z pozycją dla przycisku do obliczenia
            int buttonWidth = 120;
            int buttonHeight = 25;
            int buttonX = x2 + inputWidth + 20;
            int buttonY = y + labelHeight;

            // tworzenie przycisku
            Button buttonCalculate = crl.CreateButton(
                "ButtonCalculate",
                buttonX,
                buttonY,
                buttonWidth,
                buttonHeight,
                font,
                foreColor,
                backColor,
                "OBLICZ");

            // handlery funkcjonalności
            buttonCalculate.MouseClick += BtCalculate_MouseClick;
            buttonCalculate.MouseHover += BtCommon_MouseHover;
            buttonCalculate.MouseLeave += BtCommon_MouseLeave;

            // zmienne dla pozycji okna tekstowego z outputem
            int outY = y + labelHeight + textBoxHeight + 2 + 50;
            int outLabelWidth = 250;
            int outTextBoxWidth = groupBoxWork.ClientSize.Width - x1 * 2 - outLabelWidth - 15;
            if (outTextBoxWidth < 200) outTextBoxWidth = 200;

            // tworzenie labela z opisem dla pola tekstowego
            Label labelOut = crl.Create_Label(
                "LabelMathOut",
                new Point(x1, outY),
                font,
                backColor,
                foreColor,
                textBoxHeight,
                outLabelWidth,
                "Największy wspólny dzielnik dwóch liczb");

            // tworzenie pola tekstowego dla outputu
            TextBox textBoxOut = crl.Create_TextBox(
                "TextBoxMathOut",
                new Point(x1 + outLabelWidth + 15, outY),
                outTextBoxWidth,
                textBoxHeight,
                font,
                Color.White,
                foreColor);

            // pole tekstowe tylko do odczytu, brak możliwości edycji przez użytkownika
            textBoxOut.ReadOnly = true;
            textBoxOut.TabStop = false;

            // załadowanie wszystkich elementów do panelu roboczego
            groupBoxWork.Controls.Add(title);
            groupBoxWork.Controls.Add(buttonCalculate);

            groupBoxWork.Controls.Add(labelOut);
            groupBoxWork.Controls.Add(textBoxOut);

            groupBoxWork.Controls.Add(labelA);
            groupBoxWork.Controls.Add(textBoxA);
            groupBoxWork.Controls.Add(labelB);
            groupBoxWork.Controls.Add(textBoxB);
        }

        // metoda wywołująca algorytm matematyczny
        private void BtCalculate_MouseClick(object sender, MouseEventArgs e)
        {
            // wyszukanie textboxów
            TextBox textBoxA = FindTextBox("TextBoxMathA");
            TextBox textBoxB = FindTextBox("TextBoxMathB");
            TextBox textBoxOut = FindTextBox("TextBoxMathOut");

            // sprawdzenie czy textboxy są puste i mają odpowiedni typ danych, w przypadku błędu generujemy odpowiednie okno dialogowe z informacją dla użytkownika
            if (textBoxA == null || textBoxB == null || textBoxOut == null) return;

            if (string.IsNullOrWhiteSpace(textBoxA.Text) || string.IsNullOrWhiteSpace(textBoxB.Text))
            {
                MessageBox.Show(
                    "Wpisz obie liczby przez obliczeniem.",
                    "Brak danych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxA.Text, out int a) || !int.TryParse(textBoxB.Text, out int b))
            {
                MessageBox.Show(
                    "Wpisz poprawne liczby całkowite dodatnie.",
                    "Błąd danych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (a <= 0 || b <= 0)
            {
                MessageBox.Show(
                    "Liczby muszą być dodatnie (większe od 0).",
                    "Błąd danych.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            // zmienne dla rozszerzonego algorytmu
            int x = 0;
            int y = 0;

            // wywołanie metody elgorytmu i odebranie zmiennych do wypisania outputu
            int gcd = Mathematical.EuclideanExtended(a, b, ref x, ref y);

            textBoxOut.Text = $"{gcd} (x={x}, y={y})";
        }

        // metoda z blokadą wpisywania cyfer
        private void IntBlock(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // metoda z blokadą wpisywania liter
        private void StringBlock(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            if (char.IsDigit(e.KeyChar))
                return;
            // zezwolenie na wpisanie minusa i znaków typu backspace, delete itp
            if (e.KeyChar == '-')
            {
                bool cursorAtStart = textBox.SelectionStart == 0;
                bool minusAlreadyExists = textBox.Text.Contains("-");
                bool replacingAllText = textBox.SelectionLength == textBox.Text.Length;

                if (cursorAtStart && (!minusAlreadyExists || replacingAllText))
                    return;
            }

            e.Handled = true;
        }

        // metoda z blokadą wpisywania liter i liczb ujemnych
        private void PositiveStringBlock(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
