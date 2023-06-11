using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class Form2 : Form
    {
        public Form2(string dialogText)
        {
            InitializeComponent();
            DialogLabel.Text = dialogText;
            RestartButton.Click += (object sender, EventArgs e) =>
            {
                DialogResult = DialogResult.OK;
            };
            MenuButton.Click += (object sender, EventArgs e) =>
            {
                DialogResult = DialogResult.Cancel;
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
