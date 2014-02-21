using System;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    public abstract class LevelObject : IPrintable
    {
        // This position gives us top left corner!
        public LevelObject(FieldCoords position, ConsoleColor color)
        {
            this.Position = position;
            this.Color = color;
        }

        public FieldCoords Position { get; protected set; }

        public ConsoleColor Color { get; private set; }

        public abstract char[,] GetImage();

        public FieldCoords GetTopLeft()
        {
            return this.Position;
        }
    }
}
