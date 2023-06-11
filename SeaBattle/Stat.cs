using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    class Stat
    {
        public Button backButton = new Button();
        public Stat(Form1 form1)
        {
            backButton.Text = "Back";
            backButton.Location = new Point(50, 5);
            backButton.Size = new Size(130, 40);
            backButton.Font = new Font("Arial", 12);
            backButton.BackColor = Color.PowderBlue;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderColor = Color.AliceBlue;
            Label StatLabel = new Label
            {
                Text = "Statistics",
                Font = new Font("Arial", 24),
                Location = new Point(50, 80),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Label TotalWins = new Label {
                Text = $"Total Wins: {form1.winsTotal}",
                Font = new Font("Arial", 18),
                Location = new Point(50, 147),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Label TotalLosses = new Label
            {
                Text = $"Total Losses: {form1.lossesTotal}",
                Font = new Font("Arial", 18),
                Location = new Point(50, 207),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Label ShipsDestroyed = new Label
            {
                Text = $"Destroyed Ships: {form1.destroyedTotal}",
                Font = new Font("Arial", 18),
                Location = new Point(50, 263),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Label ShipsLost = new Label
            {
                Text = $"Lost Ships: {form1.lostTotal}",
                Font = new Font("Arial", 18),
                Location = new Point(50, 323),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            form1.Controls.Add(StatLabel);
            form1.Controls.Add(TotalWins);
            form1.Controls.Add(TotalLosses);
            form1.Controls.Add(ShipsDestroyed);
            form1.Controls.Add(ShipsLost); 
            form1.Controls.Add(backButton);
        }
    }
}
