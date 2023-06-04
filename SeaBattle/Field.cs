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
    public class Field
    {
        public int Size;
        public string[,] map;
        public Button[,] buttons;
        public Ship[] ships;
        public bool isEmpty;
        public int placedShips = 0;
        public Field(int size)
        {
            Size = size;
            string alphabet = "ABCDEFGHIJ";

            buttons = new Button[size, size];

            ships = new Ship[10];

            isEmpty = false;

            map = new string[size, size];
            for (int i = 0; i < this.Size; i++)
            {
                for(int j = 0; j < this.Size; j++)
                {
                    this.map[i, j] = "Empty";

                    Button button = new Button();
                    button.Size = new Size(30, 30);
                    button.BackColor = Color.White;
                    if(j == 0 || i == 0)
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
                    buttons[i, j] = button;
                }
            }
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
        public bool CheckShip(Coords coords)
        {
            if(map[coords.X, coords.Y].Length > 4 && map[coords.X, coords.Y].Substring(0, 4) == "Ship")
            {
                return true;
            }
            return false;
        }
        public void PlaceShip(Coords coords, bool setVisible)
        {
            if (placedShips >= 10) return;
            ships[placedShips].Position = coords;
            if (CheckShipPosition(ships[placedShips].GetShipCoords()) && CheckShipConflicts(ships[placedShips].GetAroundCoords()))
            {
                this.DrawShip(ships[placedShips], setVisible);
            }
            else return;
        }
        public void ChangeShipDirection(Coords buttonCoords)
        {
            if(this.map[buttonCoords.X, buttonCoords.Y].Length > 4 
                && this.map[buttonCoords.X, buttonCoords.Y].Substring(0, 4) == "Ship")
            {
                int shipIndex = Convert.ToInt32(this.map[buttonCoords.X, buttonCoords.Y].Substring(4));
                if (ships[shipIndex].Size == 1) return;
                List<Coords> coordsBefore = ships[shipIndex].GetShipCoords();
                ships[shipIndex].isHorizontal = !ships[shipIndex].isHorizontal;
                foreach (Coords coord in coordsBefore)
                {
                    map[coord.X, coord.Y] = "Empty";
                    buttons[coord.X, coord.Y].BackColor = Color.White;
                }
                if(CheckShipPosition(ships[shipIndex].GetShipCoords()) && CheckShipConflicts(ships[shipIndex].GetAroundCoords()))
                {
                    foreach (Coords coord in coordsBefore)
                    {
                        map[coord.X, coord.Y] = "Empty";
                        buttons[coord.X, coord.Y].BackColor = Color.White;
                    }
                    foreach (Coords coord in ships[shipIndex].GetShipCoords())
                    {
                        map[coord.X, coord.Y] = "Ship" + shipIndex;
                        buttons[coord.X, coord.Y].BackColor = Color.Red;
                    }
                }
                else
                {
                    ships[shipIndex].isHorizontal = !ships[shipIndex].isHorizontal;
                    foreach (Coords coord in coordsBefore)
                    {
                        map[coord.X, coord.Y] = "Ship" + shipIndex;
                        buttons[coord.X, coord.Y].BackColor = Color.Red;
                    }
                }
            }
        }
        public bool CheckShipPosition(List<Coords> coords)
        {
            foreach(Coords coord in coords)
            {
                if (coord.X < 0 || coord.Y < 0 || coord.X >= this.Size || coord.Y >= this.Size) return false;
            }
            return true;
        }
        public bool CheckShipConflicts(Coords[,] coords)
        {
            bool isEmpty = true;
            bool[,] empty = new bool[coords.GetLength(0), coords.GetLength(1)];
            for (int i = 0; i < coords.GetLength(0); i++) 
            {
                for(int j = 0; j < coords.GetLength(1); j++) 
                {
                    if (coords[i, j].X < 0 || coords[i, j].Y < 0 || coords[i, j].Y >= this.Size 
                        || coords[i, j].X >= this.Size) empty[i, j] = true;
                    else if (this.map[coords[i, j].X, coords[i, j].Y].Length > 4 
                        && this.map[coords[i, j].X, coords[i, j].Y].Substring(0, 4) == "Ship")
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
        private void DrawShip(Ship ship, bool setVisible)
        {
            foreach(Coords coordsPair in ship.GetShipCoords())
            {
                this.map[coordsPair.X, coordsPair.Y] = "Ship" + placedShips;
                if(setVisible) this.buttons[coordsPair.X, coordsPair.Y].BackColor = Color.Red;
            }
            placedShips++;
        }
        public bool Hit(Coords coords)
        {
            bool isHit;
            if (map[coords.X, coords.Y].Length > 4 && map[coords.X, coords.Y].Substring(0, 4) == "Ship")
            {
                buttons[coords.X, coords.Y].BackColor = Color.Blue;
                buttons[coords.X, coords.Y].Text = "X";
                int shipNum = Convert.ToInt32(map[coords.X, coords.Y].Substring(4));
                if (ships[shipNum].CheckDestroyed(map)) DrawShipAround(ships[shipNum].GetAroundCoords());
                isHit = true;
                return isHit;
            }
            else if(map[coords.X, coords.Y] == "Empty")
            {
                isHit = false;
                map[coords.X, coords.Y] = "Miss";
                buttons[coords.X, coords.Y].BackColor = Color.Black;
                return isHit;
            }
            else
            {
                isHit = true;
                return isHit;
            }
        }
        private void DrawShipAround(Coords[,] coords)
        {

        }
        public bool CheckFieldEmpty()
        {
            foreach(Ship ship in ships)
            {
                if (!ship.isDestroyed) return false;
            }
            return true;
        }
    }
}
