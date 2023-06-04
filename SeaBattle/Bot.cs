using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    public class Bot
    {
        public Field myField;

        public Field enemyField;

        public string[,] myMap = new string[Form1.mapSize, Form1.mapSize];
        public string[,] enemyMap = new string[Form1.mapSize, Form1.mapSize];

        static public Ship[] ships = new Ship[10];

        public Button[,] myButtons = new Button[Form1.mapSize, Form1.mapSize];
        public Button[,] enemyButtons = new Button[Form1.mapSize, Form1.mapSize];

        public Bot(Field myField, Field enemyField)
        {
            this.myField = myField;
            this.enemyField = enemyField;
        } 
        public bool IsInsideMap(int i, int j)
        {
            if (i < 0 || j < 0 || i >= Form1.mapSize || j >= Form1.mapSize)
            {
                return false;
            }
            return true;
        }
        public bool IsEmpty(int i, int j, int length)
        {
            bool isEmpty = true;
            for(int k = j; k < j + length; k++)
            {
                if(myMap[i, k] != "Empty")
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }
        /*
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
        */
        public void CreateShips()
        {
            Random r = new Random();

            int posX;
            int posY;

            while(myField.placedShips < 10)
            {
                posX = r.Next(1, myField.Size);
                posY = r.Next(1, myField.Size);
                myField.PlaceShip(new Coords(posX, posY), false);
            }
        }
        public string[,] PlaceShips()
        {
            int placedShips = 0;

            Random r = new Random();

            int posX = 0;
            int posY = 0;

            while(placedShips < 10)
            {
                posX = r.Next(1, Form1.mapSize);
                posY = r.Next(1, Form1.mapSize);

                ships[placedShips].Position = new Coords(posX, posY);

                if (Form1.IsInsideMap(ships[placedShips].GetShipCoords()) && IsFieldsEmpty(ships[placedShips].GetAroundCoords())) {
                    DrawShip(ships[placedShips], placedShips);
                    placedShips++;
                } 
            }
            return myMap;
        }
        /*
        int lengthShip = 4;
        int cycleValue = 4;
        int shipsCount = 10;


        while (shipsCount > 0)
        { 
            for(int i = 0; i < cycleValue / 4; i++)
            {
                posX = r.Next(1, Form1.mapSize);
                posY = r.Next(1, Form1.mapSize);

                while(!IsInsideMap(posX, posY + lengthShip - 1) || !IsEmpty(posX, posY, lengthShip))
                {
                    posX = r.Next(1, Form1.mapSize);
                    posY = r.Next(1, Form1.mapSize);
                }
                for(int k = posY; k < posY + lengthShip; k++)
                {
                    myMap[posX, k] = "Ship";
                }
                shipsCount--;
                if(shipsCount <= 0)
                {
                    break;
                }
                cycleValue += 4;
                lengthShip--;
            }
        }
        */
        public void CheckIfDestroyed()
        {
            foreach(Ship ship in Form1.ships)
            {
                if(!ship.isDestroyed && ship.CheckDestroyed(enemyMap))
                {
                    DrawHit(ship.GetAroundCoords(), enemyMap, enemyButtons);
                }
            }
        }
        public void DrawHit(Coords[,] coords, string [,] map, Button[,] buttons)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < coords.GetLength(1); j++)
                {
                    if(coords[i, j].X > 0 && coords[i, j].Y > 0 && coords[i, j].X < Form1.mapSize 
                        && coords[i, j].Y < Form1.mapSize && map[coords[i, j].X, coords[i, j].Y] == "Empty")
                    {
                        map[coords[i, j].X, coords[i, j].Y] = "Around-Ship";
                        buttons[coords[i, j].X, coords[i, j].Y].Text = "X";
                        buttons[coords[i, j].X, coords[i, j].Y].ForeColor = Color.Blue;
                    }
                }
            }
        }
        public bool Shoot()
        {
            //if (!Form1.isStarted) return false;
            Random r = new Random();

            int posX = r.Next(1, myField.Size);
            int posY = r.Next(1, myField.Size);
            while (enemyField.map[posX, posY] == "Miss" || enemyField.map[posX, posY] == "Hit" 
                || enemyField.map[posX, posY] == "Around-Ship")
            {
                posX = r.Next(1, myField.Size);
                posY = r.Next(1, myField.Size);
            }
            return enemyField.Hit(new Coords(posX, posY));
        }
        public bool IsFieldsEmpty(Coords[,] coords)
        {
            bool isEmpty = true;
            bool[,] empty = new bool[3, coords.GetLength(1)];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < coords.GetLength(1); j++)
                {
                    if (coords[i, j].X < 0 || coords[i, j].Y < 0 || coords[i, j].Y >= Form1.mapSize || coords[i, j].X >= Form1.mapSize) empty[i, j] = true;
                    else if (myMap[coords[i, j].X, coords[i, j].Y].Substring(0, 4) == "Ship")
                    {
                        empty[i, j] = false;
                    }
                    else empty[i, j] = true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < coords.GetLength(1); j++)
                {
                    if (!empty[i, j]) isEmpty = false;
                }
            }
            return isEmpty;
        }
        public void DrawShip(Ship ship, int placedShips)
        {
            List<Coords> shipCoords = ship.GetShipCoords();
            foreach (Coords coordsPair in shipCoords)
            {
                myMap[coordsPair.X, coordsPair.Y] = "Ship" + placedShips;
                //myButtons[coordsPair.X, coordsPair.Y].BackColor = Color.Red;
            }
        }
    }

    
}
