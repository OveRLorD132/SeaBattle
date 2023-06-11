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

        public Bot(Field myField, Field enemyField)
        {
            this.myField = myField;
            this.enemyField = enemyField;
        } 
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
        public bool Shoot()
        {
            if (!Form1.isStarted) return false;
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
    }

    
}
