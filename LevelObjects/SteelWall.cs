using System;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    public class SteelWall : LevelObject, IHitable, IDestroyable
    {

        private string[] visual;
        private int health;
        public SteelWall():this(0,0){ }

        public SteelWall(int x, int y) :base(x, y, ConsoleColor.Gray)
        {
            this.health = 1000;
            this.visual = LoadVisual();
        }

        public int Health
        {
            get { return this.health; }
        }

        public string[] Visual
        {
            get { return this.visual; }
        }

        public bool IsHitted { set; get; } // If it is hitted, must be turned back to false later!

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

        public void LooseHealth(int amount)
        {
            this.health -= amount;
        }

        public override string[] LoadVisual()
        {
            string line = new string(Symbols.GetChar(219), 6);
            string[] visual = new string[3]
            {
                line,
                line,
                line
            };

            return visual;
        }

        public override void Print()
        {
            ConsoleAction.PrintOnPos(this.Visual, this.X, this.Y, this.Color);
        }
    }
}
