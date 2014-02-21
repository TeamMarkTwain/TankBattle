using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    class SteelWall : LevelObject, IPrintable, IHitable, IDestroyable
    {
        private readonly char[,] SteelWallImage = { { 'S' } };
        private int health;

        public SteelWall(FieldCoords position, ConsoleColor color = ConsoleColor.Gray, int health = 1000)
            : base(position, color)
        {
            this.health = health;
        }

        public int Health
        {
            get { return this.health; }
            private set
            {
                this.health = value;
            }
        }

        public override char[,] GetImage()
        {
            return this.SteelWallImage;
        }

        public bool IsHitted { set; get; } // If it is hitted, must be turned back to false later!

        public void LooseHealth(int amount)
        {
            if (this.IsHitted) // Do we need this validation?
            {
                this.Health -= amount;
            }
        }

        public bool IsDestroyed
        {
            get 
            {
                if (this.Health <= 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
