using System;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    public class BrickWall : LevelObject, IHitable, IDestroyable
    {
        private int health;

        private string[] visual;

        public BrickWall()
            : this(0, 0)
        { }

        public BrickWall(int x, int y)
            : base(x, y, ConsoleColor.Red)
        {
            this.health = 100;
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

        public bool IsHitted { get; set; }

        public bool IsDestroyed
        {
            get
            {
                if (this.health <= 0)
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
            string line = new string(Symbols.GetChar(177), 6);
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
