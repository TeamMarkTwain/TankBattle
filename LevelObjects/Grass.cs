using System;
using TankBattle.Interfaces;
namespace TankBattle.LevelObjects
{
    class Grass : LevelObject, IReprintable
    {

        private string[] visual;

        public Grass(int x, int y)
            : base(x, y, ConsoleColor.DarkGreen) 
        {
            this.visual = LoadVisual();
        }

        public string[] Visual
        {
            get { return this.visual; }
        }
        public override string[] LoadVisual()
        {
            string line = new string(Symbols.GetChar(178), 6);
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

        public void RePrint()
        {
            this.Print();
        }
    }
}
