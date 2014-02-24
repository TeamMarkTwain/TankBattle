using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;

namespace TankBattle.Tanks
{
    class EnemySmartTank : Tank, IPrintable, IHitable, ICanShoot, IDestroyable
    {
        public EnemySmartTank(int x, int y, Directions direction, int health = 100, int speed = 20, int shootPower = 50, ConsoleColor color = ConsoleColor.Cyan)
            : base (x, y, health, speed, shootPower, color, direction)
        {
        }

        // Overriden to do nothing. Enemy tank will move by itself
        public override void Move(ConsoleKeyInfo pressedKey)
        {
        }
    }
}
