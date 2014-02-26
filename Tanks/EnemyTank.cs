using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.Tanks
{
    public abstract class EnemyTank : Tank
    {

        protected PlayerTank playerTank;
        public EnemyTank(int x, int y, PlayerTank playerTank, Directions direction, int health = 100, int speed = 20, int shootPower = 50, ConsoleColor color = ConsoleColor.Cyan)
            : base(x, y, health, speed, shootPower, color, direction)
        {
            this.playerTank = playerTank;
        }

        protected int CalcDistanceBetweenTanks(Directions direction)
        {
            int distance = 0;

            if (direction == Directions.Down)
            {
                distance = (this.X - playerTank.X) * (this.X - playerTank.X) +
                           (this.Y + this.Speed - playerTank.Y) * (this.Y + this.Speed - playerTank.Y);
            }
            else if (direction == Directions.Up)
            {
                distance = (this.X - playerTank.X) * (this.X - playerTank.X) +
                           (this.Y - this.Speed - playerTank.Y) * (this.Y - this.Speed - playerTank.Y);
            }
            else if (direction == Directions.Left)
            {
                distance = (this.X - this.Speed - playerTank.X) * (this.X - this.Speed - playerTank.X) +
                           (this.Y - playerTank.Y) * (this.Y - playerTank.Y);
            }
            else if (direction == Directions.Right)
            {
                distance = (this.X + this.Speed - playerTank.X) * (this.X + this.Speed - playerTank.X) +
                           (this.Y - playerTank.Y) * (this.Y - playerTank.Y);
            }

            return distance;
        }

        public abstract void Update();
    }
}
