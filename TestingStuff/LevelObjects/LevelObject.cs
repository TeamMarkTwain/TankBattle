using System;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    public abstract class LevelObject: IPrintable
    {
        private int x, y;
        private ConsoleColor color;

        public LevelObject(int x, int y, ConsoleColor color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public int X
        {
            get { return this.x; }
        }

        public int Y
        {
            get { return this.y; }
        }

        public ConsoleColor Color
        {
            get { return this.color; }
        }

        public abstract void Print();
        public abstract void LoadVisual();
    }
}
