using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.Tanks
{
    public class PlayerTank: Tank
    {
        public PlayerTank()
            : base(25, 38, 100, 20, 50, ConsoleColor.DarkGray, Directions.Up)
        {
        }

        public PlayerTank(ConsoleColor color)
            : base(25, 38, 100, 20, 50, color, Directions.Up)
        {

        }
    }
}
