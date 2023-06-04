using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class Ship
    {
        public int Size;
        public Coords Position;
        public bool isDestroyed = false;
        public bool isHorizontal = true;
        public Ship(int size)
        {

            Size = size;
        }
        public List<Coords> GetShipCoords()
        {
            List<Coords> Coords = new List<Coords>();
            if (this.isHorizontal)
            {
                for (int i = Position.Y; i < Position.Y + Size; i++)
                {
                    Coords.Add(new Coords(Position.X, i));
                    Console.WriteLine(new Coords(Position.X, i).ToString());
                }
            }
            else
            {
                for (int i = Position.X; i < Position.X + Size; i++)
                {
                    Coords.Add(new Coords(i, Position.Y));
                }
            }
            return Coords;
        }
        public Coords[,] GetAroundCoords()
        {
            if(this.isHorizontal)
            {
                Coords[,] coords = new Coords[3, Size + 2];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < Size + 2; j++)
                    {
                        coords[i, j] = new Coords(Position.X + i - 1, Position.Y + j - 1);
                        Console.Write(coords[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }
                return coords;
            }
            else
            {
                Coords[,] coords = new Coords[Size + 2, 3];
                for (int i = 0; i < Size + 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        coords[i, j] = new Coords(Position.X + i - 1, Position.Y + j - 1);
                        Console.Write(coords[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }
                return coords;
            }
        }
        public bool CheckDestroyed(string[,] map)
        {
            List<Coords> shipCoords = GetShipCoords();
            bool isHit = true;
            foreach(Coords coord in shipCoords)
            {
                if (map[coord.X, coord.Y].Length >= 4 && map[coord.X, coord.Y].Substring(0, 4) == "Ship") isHit = false;
            }
            if(isHit) this.isDestroyed = true;
            return isHit;
        }
    } 
}
