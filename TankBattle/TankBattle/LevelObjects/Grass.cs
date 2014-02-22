using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    class Grass : LevelObject, IPrintable
    {
        private readonly char[,] GrassImage = { { 'G' } };

        public Grass(FieldCoords position, ConsoleColor color = ConsoleColor.DarkGreen)
            : base(position, color)
        {
        }

        public override char[,] GetImage()
        {
            return this.GrassImage;
        }
    }
}
