using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SeaBattle
{
    class Rules
    {
        public Button backButton = new Button();
        private Panel panel = new Panel
        {
            Location = new Point(50, 50),
            BackColor = Color.PowderBlue,
            AutoSize = true
        };
        private Label RulesLabel = new Label
        {
            Text = "Rules",
            Location = new Point(50, 0),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 20),
            AutoSize = true
        };
        private Label FirstParagraph = new Label
        {
            Text = "1. The game is played on a 10x10 field.",
            Location = new Point(50, 40),
            Font = new Font("Arial", 12),
            BackColor = Color.Transparent,
            AutoSize = true
        };
        private Label SecondParagraph = new Label
        {
            Text = "2. Each player has their own fleet of ships, consisting of the following types:\r\n\r\nSingle-deck ship(1 unit)\r\nDouble-deck ship(2 units)\r\nTriple-deck ship(1 unit)\r\nQuadruple-deck ship(1 unit)\r\n",
            Location = new Point(50, 70),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 12),
            AutoSize = true
        };
        private Label ThirdParagraph = new Label
        {
            Text = "3. After the ships have been placed, the game enters the battle phase.",
            Location = new Point(50, 190),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 12),
            AutoSize = true
        };
        private Label FourthParagraph = new Label
        {
            Text = "4. Players take turns selecting coordinates on the opponent's grid to make a shot.",
            Location = new Point(50, 220),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 12),
            AutoSize = true
        };
        private Label FifthParagraph = new Label
        {
            Text = "5. If the shot hits an opponent's ship, the cell is considered \"hit.\" \nThe player gets an additional shot.",
            Location = new Point(50, 250),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 12),
            AutoSize = true
        };
        private Label SixthParagraph = new Label
        {
            Text = "6. If the shot misses, the turn switches to the opponent.",
            Location = new Point(50, 290),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 12),
            AutoSize = true
        };
        private Label SeventhParagraph = new Label
        {
            Text = "7. The game continues until all the ships of one player have been sunk.\n The player who sinks all the opponent's ships is considered the winner.",
            Location = new Point(50, 310),
            BackColor = Color.Transparent,
            Font = new Font("Arial", 12),
            AutoSize = true
        };
        public Rules(Form1 form1)
        {
            form1.Controls.Clear();
            panel.Controls.Add(RulesLabel);
            panel.Controls.Add(FirstParagraph);
            panel.Controls.Add(SecondParagraph);
            panel.Controls.Add(ThirdParagraph);
            panel.Controls.Add(FourthParagraph);
            panel.Controls.Add(FifthParagraph);
            panel.Controls.Add(SixthParagraph);
            panel.Controls.Add(SeventhParagraph);
            form1.Controls.Add(panel);
            form1.Controls.Add(backButton);
            backButton.Text = "Back";
            backButton.Location = new Point(50, 5);
            backButton.Size = new Size(130, 40);
            backButton.Font = new Font("Arial", 12);
            backButton.BackColor = Color.PowderBlue;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderColor = Color.AliceBlue;
        }
    }
}

