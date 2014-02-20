using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.LevelObjects
{
    class SteelWall: LevelObject
    {

        private string[] visual;
        private int health;
        public SteelWall():this(0,0){ }

        public SteelWall(int x, int y) :base(x, y, ConsoleColor.Gray)
        {
            this.health = 1000;
            LoadVisual();
        }

        public int Health
        {
            get { return this.health; }
        }
        public override void LoadVisual()
        {
            string line = new string(Symbols.GetChar(219), 6);
            this.visual = new string[3]
            {
                line,
                line,
                line
            };
        }

        public override void Print()
        {
            ConsoleAction.PrintOnPos(this.visual, this.X, this.Y, this.Color);
        }
    }
}
