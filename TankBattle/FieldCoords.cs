using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public struct FieldCoords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public FieldCoords(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static FieldCoords operator +(FieldCoords a, FieldCoords b)
        {
            return new FieldCoords(a.X + b.X, a.Y + b.Y);
        }

        public static FieldCoords operator -(FieldCoords a, FieldCoords b)
        {
            return new FieldCoords(a.X - b.X, a.Y - b.Y);
        }

        public override bool Equals(object obj)
        {
            FieldCoords objAsFieldCoords = (FieldCoords)obj;

            return objAsFieldCoords.X == this.X && objAsFieldCoords.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() * 7 + this.Y;
        }
    }
}
