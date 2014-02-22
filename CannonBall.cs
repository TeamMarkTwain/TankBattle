using System;
using TankBattle.Interfaces;

namespace TankBattle
{
    public abstract class CannonBall : IPrintable, IDestroyable
    {
        private Directions direction;
        private int x, y;  //coords
        private int speed;
        private int shootPower;
        private char visual;

        public CannonBall(int x, int y, int speed, int shootPower, Directions direction)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.shootPower = shootPower;
            this.direction = direction;
            this.visual = 'O';
        }

        protected char Visual
        {
            get { return this.visual; }
            set { this.visual = value; }
        }

        public int X { get { return this.x; } }

        public int Y { get { return this.y; } }

        public int Speed { get { return this.speed; } }

        public int ShootPower { 
            get { return this.shootPower; }
            private set { this.shootPower = value; }
        }

        public bool IsHitted { set; get; }

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

        public virtual void LooseHealth(int amount)
        {
            this.ShootPower = this.shootPower - amount;
        }

        public int Health
        {
            get { return this.ShootPower; }
        }

        public void Move()
        {
            if (this.direction == Directions.Down)
            {
                this.x += this.Speed;
            }
            else if (this.direction == Directions.Up)
            {
                this.x -= this.Speed;
            }
            else if (this.direction == Directions.Right)
            {
                this.y += this.Speed;
            }
            else if (this.direction == Directions.Left)
            {
                this.y -= this.Speed;
            }
        }

        public void Print()
        {
            ConsoleAction.PrintCharOnPos(this.visual, this.x, this.y, ConsoleColor.White);
        }
    }
}
