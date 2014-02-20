using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class CannonBall
    {
        private int x, y;
        private byte direction;

        public CannonBall(int x, int y, byte direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
    }
}
