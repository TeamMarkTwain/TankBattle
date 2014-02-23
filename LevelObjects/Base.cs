using System;
using System.Text;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    class Base: LevelObject, IHitable, IDestroyable
    {
        private int health;
        private string[] visual;

        public Base(int x, int y)
            :base(x, y, ConsoleColor.DarkGray)
        {
            this.health = 500;
            this.visual = LoadVisual();
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

        public int Health { get { return this.health; } }

        public bool IsHitted { get; set; }

        public void LooseHealth(int amount)
        {
            this.health -= amount;
        }

        public override void Print()
        {
            ConsoleAction.PrintOnPos(this.visual, this.X, this.Y, this.Color);
        }

        //Edit base visual!
        public override string[] LoadVisual()
        {
            string[] visualBase = new string[3];

            visualBase[0] = " /^^\\ ";
            visualBase[1] = "<---->";
            visualBase[2] = " \\__/ ";

            return visualBase;
        }
    }
}
