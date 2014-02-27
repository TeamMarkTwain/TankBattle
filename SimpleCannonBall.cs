using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class SimpleCannonBall : CannonBall
    {
        public SimpleCannonBall (int x, int y, int speed, int shootPower, Directions direction, bool firedFromPlayer, char visual = 'O')
            : base(x, y, speed, shootPower, direction, firedFromPlayer)
        {
            this.Visual = visual;
        }
    }
}
