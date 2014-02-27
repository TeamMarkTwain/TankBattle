using System;
using TankBattle.Interfaces;


namespace TankBattle.LevelObjects
{
    class River : LevelObject, IReprintable
    {

        private string[] visual;
        public River() : this(0, 0) { }
        public River(int x, int y) :base(x, y, ConsoleColor.DarkBlue) 
        {
            this.visual = LoadVisual();
        }

        public string[] Visual
        {
            get { return this.visual; }
        }
        public override string[] LoadVisual()
        {
            string line = new string(Symbols.GetChar(126), 6);
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
            ConsoleAction.PrintOnPos(this.Visual, this.X, this.Y, this.Color, ConsoleColor.Blue);
        }

        public void RePrint()
        {
            this.Print();
        }
    }
}
