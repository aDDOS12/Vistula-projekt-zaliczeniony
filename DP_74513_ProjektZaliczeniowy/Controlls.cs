using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DP_74513_ProjektZaliczeniowy
{
    // klasa - konstruktor kontrolek, elementów graficznych interfejsu
    internal class Controlls
    {
        // metoda budowy kontrolki typu button
        public Button CreateButton(string name, int x, int y, int width, int height, Font czcionka, Color foreColor, Color backColor, string text)
        {
            Button button = new Button()
            {
                Name = name,
                Location = new Point(x, y),
                Width = width,
                Height = height,
                AutoSize = false,
                Font = czcionka,
                BackColor = backColor,
                ForeColor = foreColor,
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter
            };
            return button;
        }

        // metoda budowy ramki typu groupbox
        public GroupBox Create_GroupBox(int x, int y, int width, int height, string text, string name)
        {
            GroupBox groupBox = new GroupBox()
            {
                Location = new Point(x, y),
                Width = width,
                Height = height,
                Text = text,
                Name = name
            };
            return groupBox;
        }

        // metoda budowy "plakietki" z nazwą typu label
        public Label Create_Label(string name, Point location, Font czcionka, Color backColor, Color foreColor, int height, int width, string text)
        {
            Label label = new Label()
            {
                Location = location,
                AutoSize = false,
                Width = width,
                Height = height,
                Font = czcionka,
                BackColor = backColor,
                ForeColor = foreColor,
                Name = name,
                Text = text
            };
            return label;
        }

        // metoda budowy pola tekstowego typu textbox
        public TextBox Create_TextBox(string name, Point location, int width, int height, Font czcionka, Color backColor, Color foreColor)
        {
            TextBox textBox = new TextBox()
            {
                Location = location,
                Width = width,
                Height = height,
                Name = name,
                Font = czcionka,
                BackColor = backColor,
                ForeColor = foreColor,
                BorderStyle = BorderStyle.FixedSingle,
            };
            return textBox;
        }
    }
}
