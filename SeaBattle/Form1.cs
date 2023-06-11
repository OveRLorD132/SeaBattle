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
    public partial class Form1 : Form
    {
        public int cellSize = 30;
        //public string alphabet = "ABCDEFGHIJ";

        static public bool isStarted = false;

        public Bot bot;

        public Field playerField;

        public Field botField;

        Button ClearButt;

        public int winsTotal = 0;

        public int lostTotal = 0;

        public int destroyedTotal = 0;

        public int lossesTotal = 0;

        public int chosenShipNum = 10;

        public Form1()
        {

            InitializeComponent();
            ShowMenu();
            //Init();
        }
        public void ShowMenu()
        {
            this.Controls.Clear();
            Menu mainMenu = new Menu(this);
            this.Text = "Main menu";
            mainMenu.PlayButton.Click += ShowFields;
            mainMenu.RulesButton.Click += ShowRules;
            mainMenu.StatButton.Click += ShowStat;
        }
        public void ShowFields(object sender, EventArgs e)
        {
            this.Controls.Clear();
            playerField = null;
            botField = null;
            Init();
        }
        public void ShowRules(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Rules rules = new Rules(this);
            this.Text = "Rules";
            rules.backButton.Click += BackToMenu;
        }
        public void ShowStat(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Stat stat = new Stat(this);
            this.Text = "Statistics";
            stat.backButton.Click += BackToMenu;
        }
        public void Init()
        {
            this.chosenShipNum = 10;
            this.Text = "Sea Battle";
            isStarted = false;

            Panel panel = new Panel();
            panel.Width = 700;
            panel.Height = 350;

            panel.BackColor = Color.White;
            panel.Location = new Point(50, 50);
            playerField = new Field(10);
            for(int i = 0; i < 10; i++) 
            {
                for(int j = 0; j < 10; j++)
                {
                    playerField.buttons[i, j].Location = new Point(30 + j * 30, i * 30);
                    if (j != 0 && i != 0)
                    {
                        playerField.buttons[i, j].Click += PlayerFieldClick;
                        //playerField.buttons[i, j].MouseDown += DragShip;
                    }
                    panel.Controls.Add(playerField.buttons[i, j]);
                }

            }
            Label map1 = new Label();
            map1.Text = "Player's map";
            map1.Location = new Point(10 * 30 / 2, 10 * 30 + 10);
            panel.Controls.Add(map1);
            botField = new Field(10);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    botField.buttons[i, j].Location = new Point(370 + (j * 30), i * 30);
                    if (j != 0 && i != 0)
                    {
                        botField.buttons[i, j].Click += PlayerOnShoot;
                    }
                    panel.Controls.Add(botField.buttons[i, j]);
                }

            }
            Label map2 = new Label();
            map2.Text = "Bot's map";
            map2.Location = new Point(360 + 10 * 30 / 2, 10 * 30 + 10);
            panel.Controls.Add(map2);
            bot = new Bot(botField, playerField);
            bot.CreateShips();
            Button backButton = new Button();
            backButton.Text = "Back";
            backButton.Click += BackToMenu;
            backButton.Location = new Point(50, 5);
            backButton.Size = new Size(130, 40);
            backButton.Font = new Font("Arial", 12);
            backButton.BackColor = Color.PowderBlue;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderColor = Color.AliceBlue;
            Button ClearButton = new Button();
            ClearButt = ClearButton;
            ClearButton.Text = "Clear";
            ClearButton.Click += ClearField;
            ClearButton.Location = new Point(50, 400);
            ClearButton.Size = new Size(130, 40);
            ClearButton.Font = new Font("Arial", 12);
            ClearButton.BackColor = Color.PowderBlue;
            ClearButton.FlatStyle = FlatStyle.Flat;
            ClearButton.FlatAppearance.BorderColor = Color.AliceBlue;
            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += StartGame;
            startButton.Location = new Point(200, 400);
            startButton.Size = new Size(130, 40);
            startButton.Font = new Font("Arial", 12);
            startButton.BackColor = Color.LightBlue;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.FlatAppearance.BorderColor = Color.AliceBlue;
            playerField.OnFieldEmpty += ShowBotWin;
            botField.OnFieldEmpty += ShowPlayerWin;
            this.Controls.Add(panel);
            this.Controls.Add(ClearButton);
            this.Controls.Add(backButton);
            this.Controls.Add(startButton);
        }
        public void StartGame(object sender, EventArgs e)
        {
            if (playerField.placedShips == 10 && botField.placedShips == 10)
            {
                isStarted = true;
                ClearButt.Text = "Give Up";
                Button pressedButton = sender as Button;
                pressedButton.Visible = false;
            }
        }
        public void BackToMenu(object sender, EventArgs e)
        {
            ShowMenu();
        }
        public void ClearField(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (pressedButton.Text == "Clear")
            {
                this.Controls.Clear();
                playerField = null;
                botField = null;
                Init();
            }
            else if(pressedButton.Text == "Give Up")
            {
                ShowBotWin(sender, e);
            }
        }
        public void PlayerFieldClick(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if(!isStarted && !playerField.CheckShip(new Coords(pressedButton.Location.Y / cellSize, (pressedButton.Location.X - 30) / cellSize)))
            {
                if (chosenShipNum.Equals(10))
                {
                    playerField.PlaceShip(new Coords(pressedButton.Location.Y / cellSize, (pressedButton.Location.X - 30) / cellSize), true);
                }
                else
                {
                    playerField.MoveShip(chosenShipNum, new Coords(pressedButton.Location.Y / cellSize, (pressedButton.Location.X - 30) / cellSize));
                    chosenShipNum = 10;
                }
            }
            else if (!isStarted && playerField.CheckShip(new Coords(pressedButton.Location.Y / cellSize, (pressedButton.Location.X - 30) / cellSize)))
            {
                int shipNum = Convert.ToInt32(playerField.map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - 30) / cellSize].Substring(4));
                if (chosenShipNum != shipNum) chosenShipNum = shipNum;
                else playerField.ChangeShipDirection(new Coords(pressedButton.Location.Y / cellSize, (pressedButton.Location.X - 30) / cellSize));
            }
        } 
        public void ShowBotWin(object sender, EventArgs e)
        {
            isStarted = false;
            Form2 dialog = new Form2("Bot Won!");
            dialog.Text = "Bot Win";
            lossesTotal++;
            destroyedTotal += botField.GetShipsDestroyed();
            lostTotal += playerField.GetShipsDestroyed();
            DialogResult dialogResult = dialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                dialog.Close();
                this.Controls.Clear();
                playerField = null;
                botField = null;
                Init();
            }
            else if(dialogResult == DialogResult.Cancel)
            {
                dialog.Close();
                ShowMenu();
            }
        }
        public void ShowPlayerWin(object sender, EventArgs e)
        {
            isStarted = false;
            Form2 dialog = new Form2("You Won!!!");
            dialog.Text = "Player win";
            winsTotal++;
            destroyedTotal += botField.GetShipsDestroyed();
            lostTotal += playerField.GetShipsDestroyed();
            DialogResult dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                dialog.Close();
                this.Controls.Clear();
                playerField = null;
                botField = null;
                Init();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                dialog.Close();
                ShowMenu();
            }
        }
        public void PlayerOnShoot(object sender, EventArgs e)
        {
            if (!isStarted) return;
            Button pressedButton = sender as Button;
            bool playerTurn = botField.Hit(new Coords(pressedButton.Location.Y / 30, (pressedButton.Location.X - 370) / 30));
            if(!playerTurn)
            {
                bool botTurn = bot.Shoot();
                while (botTurn)
                {
                    botTurn = bot.Shoot();
                }
            }
        } 
    }
}
