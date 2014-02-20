using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.LevelObjects
{
    class Grass: LevelObject
    {
        private string[] visual;
        public Grass(): this(0, 0) { }

        public Grass(int x, int y) :base(x, y, ConsoleColor.DarkGreen) 
        {
            LoadVisual();
        }
        public override void LoadVisual()
        {
            string line = new string(Symbols.GetChar(178), 6);
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
