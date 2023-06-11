using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    class Menu
    {
        private Label NameLabel = new Label
        {
            Text = "Sea Battle",
            Font = new Font("Arial", 24),
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(200, 50),
            BackColor = Color.Transparent
        };
        private Label MenuLabel = new Label { 
            Text = "Main Menu", 
            Font = new Font("Arial", 16), 
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(200, 50),
            BackColor = Color.Transparent
        };
        public Button PlayButton = new Button {
            Text = "Play", 
            Size = new Size(200, 50),
            Font = new Font("Arial", 12),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.PowderBlue

        };
        public Button RulesButton = new Button { 
            Text = "Rules",
            BackColor = Color.PowderBlue,
            Font = new Font("Arial", 12),
            FlatStyle = FlatStyle.Flat,
            Size = new Size(200, 50),
        };
        public Button StatButton = new Button {
            Text = "Statistics",
            BackColor = Color.PowderBlue,
            Font = new Font("Arial", 12),
            FlatStyle = FlatStyle.Flat,
            Size = new Size(200, 50),
        };

        public Menu(Form1 form1)
        {
            form1.Controls.Clear();
            NameLabel.Location = new Point(300, 50);
            MenuLabel.Location = new Point(300, 120);
            PlayButton.Location = new Point(300, 200);
            RulesButton.Location = new Point(300, 280);
            StatButton.Location = new Point(300, 360);

            PlayButton.FlatAppearance.BorderSize = 0;
            RulesButton.FlatAppearance.BorderSize = 0;
            StatButton.FlatAppearance.BorderSize = 0;

            form1.Controls.Add(MenuLabel);
            form1.Controls.Add(PlayButton);
            form1.Controls.Add(RulesButton);
            form1.Controls.Add(StatButton);
            form1.Controls.Add(NameLabel);
        }
    }
}
