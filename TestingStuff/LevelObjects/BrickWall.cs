using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.LevelObjects
{
    public class BrickWall: LevelObject
    {
        private string[] visual;
        private int health;
        public BrickWall() :this(0, 0) 
        { }
        public BrickWall(int x, int y)
            : base(x, y, ConsoleColor.Red)
        {
            this.health = 100;
            LoadVisual();
        }

        public int Health
        {
            get { return this.health; }
        }
        public string[] Visual
        {
            get { return this.visual; }
        }

        public override void LoadVisual()
        {
            string line = new string(Symbols.GetChar(177), 6);
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
