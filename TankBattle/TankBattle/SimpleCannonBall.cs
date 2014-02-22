using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle
{
    public class SimpleCannonBall : CannonBall, IShootable, IPrintable
    {
        public SimpleCannonBall(FieldCoords position, Directions direction, FieldCoords speed, ConsoleColor color = ConsoleColor.Yellow, int shootPower = 100)
            : base(position, direction, speed, color, shootPower)
        {
        }

    }
}
