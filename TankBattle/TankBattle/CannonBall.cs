using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle
{
    public abstract class CannonBall : LevelObject, IShootable, IPrintable
    {
        private readonly char[,] CannonImage = { { 'X' } };

        public CannonBall(FieldCoords position, Directions direction, FieldCoords speed, ConsoleColor color, int shootPower)
            : base(position, color)
        {
            this.Direction = direction;
            this.ShootPower = shootPower;
            this.Speed = speed;
        }

        public Directions Direction { get; private set; }

        public override char[,] GetImage()
        {
            return this.CannonImage;
        }

        public FieldCoords Speed { get; private set; }

        public int ShootPower { get; private set; }

        public int Health
        {
            get { return this.ShootPower; }
        }

        public void Move()
        {
            base.Position += this.Speed;
        }

        public bool IsHitted { set; get; }

        public void LooseHealth(int amount)
        {
            this.ShootPower -= amount;
        }

        public bool IsDestroyed
        {
            get
            {
                if (this.Health <= 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
