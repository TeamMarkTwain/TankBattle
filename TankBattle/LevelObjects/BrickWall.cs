using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;

namespace TankBattle.LevelObjects
{
    public class BrickWall : LevelObject, IPrintable, IHitable, IDestroyable
    {
        private readonly char[,] BrickImage = { { 'B' } };
        private int health;

        public BrickWall(FieldCoords position, ConsoleColor color = ConsoleColor.Red, int health = 100)
            : base(position, color)
        {
            this.Health = health;
        }

        public int Health
        {
            get { return this.health; }
            private set
            {
                this.health = value;
            }
        }

        public override char[,] GetImage()
        {
            return this.BrickImage;
        }

        public bool IsHitted { get; set; }

        public void LooseHealth(int amount)
        {
            if (this.IsHitted)
            {
                this.Health -= amount;
            }
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
