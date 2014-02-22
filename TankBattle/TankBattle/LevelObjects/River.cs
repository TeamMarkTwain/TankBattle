using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    class River : LevelObject, IPrintable
    {
        private readonly char[,] RiverImage = { { 'R' } };

        public River(FieldCoords position, ConsoleColor color = ConsoleColor.DarkBlue)
            : base(position, color)
        {
        }

        public override char[,] GetImage()
        {
            return this.RiverImage;
        }
    }
}
