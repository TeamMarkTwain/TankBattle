using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.LevelObjects
{
    class River: LevelObject
    {
        private string[] visual;

        public River() : this(0, 0) { }
        public River(int x, int y) :base(x, y, ConsoleColor.DarkBlue) 
        {
            LoadVisual();
        }
        public override void LoadVisual()
        {
            string line = new string(Symbols.GetChar(126), 6);
            this.visual = new string[3] 
            {
                line,
                line,
                line
            };
        }
        public override void Print()
        {
            ConsoleAction.PrintOnPos(this.visual, this.X, this.Y, this.Color, ConsoleColor.Blue);
        }

    }
}
