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
        public const int mapSize = 10;
        public int cellSize = 30;
        public string alphabet = "ABCDEFGHIJ";

        public int PlacedShips = 0;

        public Button[,] myButtons = new Button[mapSize, mapSize];
        public Button[,] enemyButtons = new Button[mapSize, mapSize];

        public string[,] myMap = new string[mapSize, mapSize];
        public string[,] enemyMap = new string[mapSize, mapSize];

        static public bool isStarted = false;

        public Bot bot;

        public Field playerField;

        public Field botField;

        static public Ship[] ships = new Ship[10];
        public Form1()
        {

            InitializeComponent();
            Init();
        }
        public void Init()
        {
            isStarted = false;
            playerField = new Field(10);
            for(int i = 0; i < 10; i++) 
            {
                for(int j = 0; j < 10; j++)
                {
                    playerField.buttons[i, j].Location = new Point(j * 30, i * 30);
                    if (j != 0 && i != 0)
                    {
                        playerField.buttons[i, j].Click += PlayerFieldClick;
                    }
                    this.Controls.Add(playerField.buttons[i, j]);
                }

            }
            Label map1 = new Label();
            map1.Text = "Player's map";
            map1.Location = new Point(10 * 30 / 2, 10 * 30 + 10);
            this.Controls.Add(map1);
            botField = new Field(10);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    botField.buttons[i, j].Location = new Point(320 + (j * 30), i * 30);
                    if (j != 0 && i != 0)
                    {
                        botField.buttons[i, j].Click += PlayerOnShoot;
                    }
                    this.Controls.Add(botField.buttons[i, j]);
                }

            }
            Label map2 = new Label();
            map2.Text = "Bot's map";
            map2.Location = new Point(320 + 10 * 30 / 2, 10 * 30 + 10);
            this.Controls.Add(map2);
            bot = new Bot(botField, playerField);
            bot.CreateShips();
            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += StartGame;
            startButton.Location = new Point(0, mapSize * cellSize + 20);
            this.Controls.Add(startButton);
            //DrawMap();
            //CreateShips();
            //PlacedShips = 0;
            //bot = new Bot(enemyMap, myMap, enemyButtons, myButtons);
            //enemyMap = bot.PlaceShips();
        }
        public void StartGame(object sender, EventArgs e)
        {
            if (PlacedShips == 10)
            {
                isStarted = true;
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        Console.Write(myMap[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        public void DrawMap()
        {
            for(int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = "Empty";

                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    else
                    {
                        button.Click += FieldOnClick;
                    }
                    myButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = "Empty";
                    enemyMap[i, j] = "Empty";

                    Button button = new Button();
                    button.Location = new Point(320 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    else
                    {
                        button.Click += PlayerShoot;
                    }
                    enemyButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.Text = "Player's map";
            map1.Location = new Point(mapSize * cellSize / 2, mapSize * cellSize + 10);
            this.Controls.Add(map1);

            Label map2 = new Label();
            map2.Text = "Enemy's map";
            map2.Location = new Point(350 + mapSize * cellSize / 2, mapSize * cellSize + 10);
            this.Controls.Add(map2);

            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += StartGame;
            startButton.Location = new Point(0, mapSize * cellSize + 20);
            this.Controls.Add(startButton);
        }
        public void PlayerFieldClick(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if(!isStarted && !playerField.CheckShip(new Coords(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize)))
            {
                playerField.PlaceShip(new Coords(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize), true);
            }
            else if (!isStarted && playerField.CheckShip(new Coords(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize)))
            {
                playerField.ChangeShipDirection(new Coords(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize));
            }
        } 
        public void FieldOnClick(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (!isStarted && PlacedShips < 10
                && myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize].Length > 4
                && myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize].Substring(0, 4) != "Ship")
            {
                ships[PlacedShips].Position = new Coords(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);
                if (IsInsideMap(ships[PlacedShips].GetShipCoords()) && IsFieldsEmpty(ships[PlacedShips].GetAroundCoords())) PlaceShip(sender, e);
            }
            else if (!isStarted && PlacedShips < 10
                && myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize].Length > 4
                && myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize].Substring(0, 4) == "Ship")
            {
                ShipChangeDirection(sender);
            }
            else return;
        }
        static public bool IsInsideMap(List<Coords> coords)
        {
            bool isInside = true;
            foreach(Coords coord in coords)
            {
                if (coord.X < 0 || coord.Y < 0 || coord.X >= mapSize || coord.Y >= mapSize) isInside = false;
            }
            return isInside;
        }
        public bool IsFieldsEmpty(Coords[,] coords)
        {
            bool isEmpty = true;
            bool[,] empty = new bool[coords.GetLength(0), coords.GetLength(1)];
            for(int i = 0; i < coords.GetLength(0); i++)
            {
                for(int j = 0; j < coords.GetLength(1); j++)
                {
                        if (coords[i, j].X < 0 || coords[i, j].Y < 0 || coords[i, j].Y >= mapSize || coords[i, j].X >= mapSize) empty[i, j] = true;
                        else if (myMap[coords[i, j].X, coords[i, j].Y].Substring(0, 4) == "Ship")
                        {
                            empty[i, j] = false;
                        }
                        else empty[i, j] = true;
                }
            }
            for (int i = 0; i < coords.GetLength(0); i++)
            {
                for (int j = 0; j < coords.GetLength(1); j++)
                {
                    if (!empty[i, j]) isEmpty = false;
                }
            }
            return isEmpty;
        }
        private void ShipChangeDirection(object sender)
        {
            Console.WriteLine("handle");
            if (isStarted) return;
            Button button = sender as Button;
            Coords coords = new Coords(button.Location.Y / cellSize, button.Location.X / cellSize);
            if (myMap[coords.X, coords.Y].Substring(0, 4) == "Ship")
            {
                int shipIndex = Convert.ToInt32(myMap[coords.X, coords.Y].Substring(4));
                if (ships[shipIndex].Size == 1) return;
                List<Coords> coordsBefore = ships[shipIndex].GetShipCoords();
                ships[shipIndex].isHorizontal = !ships[shipIndex].isHorizontal;
                foreach (Coords coord in coordsBefore)
                {
                    myMap[coord.X, coord.Y] = "Empty";
                    myButtons[coord.X, coord.Y].BackColor = Color.White;
                }
                if (IsInsideMap(ships[shipIndex].GetShipCoords()) && IsFieldsEmpty(ships[shipIndex].GetAroundCoords()))
                {
                    foreach (Coords coord in coordsBefore)
                    {
                        myMap[coord.X, coord.Y] = "Empty";
                        myButtons[coord.X, coord.Y].BackColor = Color.White;
                    }
                    foreach (Coords coord in ships[shipIndex].GetShipCoords())
                    {
                        myMap[coord.X, coord.Y] = "Ship" + shipIndex;
                        myButtons[coord.X, coord.Y].BackColor = Color.Red;
                    }
                }
                else
                {
                    ships[shipIndex].isHorizontal = !ships[shipIndex].isHorizontal;
                    foreach (Coords coord in coordsBefore)
                    {
                        myMap[coord.X, coord.Y] = "Ship" + shipIndex;
                        myButtons[coord.X, coord.Y].BackColor = Color.Red;
                    }
                }
               }

            }
        public void CreateShips()
        {
            ships[0] = new Ship(4);
            ships[1] = new Ship(3);
            ships[2] = new Ship(3);
            ships[3] = new Ship(2);
            ships[4] = new Ship(2);
            ships[5] = new Ship(2);
            ships[6] = new Ship(1);
            ships[7] = new Ship(1);
            ships[8] = new Ship(1);
            ships[9] = new Ship(1);
        }
        public void PlaceShip(object sender, EventArgs e) 
        {
            if(PlacedShips < 10)
            {
                DrawShip(ships[PlacedShips]);
                PlacedShips++;
            }
        }
        public void DrawShip(Ship ship)
        {
            List<Coords> shipCoords = ship.GetShipCoords();
            foreach(Coords coordsPair in shipCoords)
            {
                myMap[coordsPair.X, coordsPair.Y] = "Ship" + PlacedShips;
                myButtons[coordsPair.X, coordsPair.Y].BackColor = Color.Red;
            }
        }
        public bool CheckMapEmpty()
        {
            bool isEmpty1 = true;
            bool isEmpty2 = true;
            for(int i = 0; i < ships.Length; i++)
            {
                if (!ships[i].isDestroyed) isEmpty1 = false;
                Console.WriteLine(ships[i].isDestroyed);
                if (!Bot.ships[i].isDestroyed) isEmpty2 = false;
            }
            Console.WriteLine(isEmpty1);
            if(isEmpty2)
            {
                Console.WriteLine("You Won!!!!!!");
                return false;
            }
            else if(isEmpty1)
            {
                Console.WriteLine("Bot Won.");
                return false;
            }
            return true;
        }
        public void PlayerOnShoot(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            bool playerTurn = botField.Hit(new Coords(pressedButton.Location.Y / 30, (pressedButton.Location.X - 320) / 30));
            if(!playerTurn)
            {
                bool botTurn = bot.Shoot();
                while (botTurn)
                {
                    botTurn = bot.Shoot();
                }
                if (playerField.CheckFieldEmpty() || botField.CheckFieldEmpty())
                {
                    this.Controls.Clear();
                    isStarted = false;
                    Init();
                }
            }
        } 
        public void PlayerShoot(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            bool playerTurn = Shoot(enemyMap, pressedButton);
            if(!playerTurn)
            {
                bool botTurn = bot.Shoot();
                while(botTurn)
                {
                    botTurn = bot.Shoot();
                }
            }
            if(!CheckMapEmpty()) {
                this.Controls.Clear();
                isStarted = false;
                Init();
            }
        }
        public void CheckIfDestroyed()
        {
            foreach(Ship ship in Bot.ships)
            {
                if (!ship.isDestroyed && ship.CheckDestroyed(enemyMap))
                {
                    DrawHit(ship.GetAroundCoords(), enemyMap, enemyButtons);
                }
                Console.WriteLine(ship.isDestroyed);

            }
        } 
        public void DrawHit(Coords[,] coords, string[,] map, Button[,] buttons)
        {
            for(int i = 0; i < coords.GetLength(0); i++)
            {
                for(int j = 0; j < coords.GetLength(1); j++)
                {
                    if(coords[i, j].X > 0 && coords[i, j].Y > 0 && coords[i, j].X < mapSize && coords[i, j].Y < mapSize && map[coords[i, j].X, coords[i,j].Y] == "Empty")
                    {
                        map[coords[i, j].X, coords[i, j].Y] = "Around-Ship";
                        buttons[coords[i, j].X, coords[i, j].Y].Text = "X";
                        buttons[coords[i, j].X, coords[i, j].Y].ForeColor = Color.Blue;
                    }
                }
            }
        }
        public bool Shoot(string[,] map, Button pressedButton)
        {
            bool hit = false;
            if(isStarted)
            {
                int delta = 0;
                if(pressedButton.Location.X > 320)
                {
                    delta = 320;
                }
                if(map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize].Length >= 4 
                    && map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize].Substring(0, 4) == "Ship")
                {
                    hit = true;
                    map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = "Hit";
                    CheckIfDestroyed();
                    pressedButton.BackColor = Color.Blue;
                    pressedButton.Text = "X";
                }
                else if(map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] == "Miss")
                {
                    hit = true;
                }
                else if(map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] == "Hit")
                {
                    hit = true;
                }
                else if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] == "Around-Ship")
                {
                    hit = true;
                }
                else
                {
                    hit = false;
                    map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = "Miss";
                    pressedButton.BackColor = Color.Black; 
                }
            }
            return hit;
        }
    }
}
