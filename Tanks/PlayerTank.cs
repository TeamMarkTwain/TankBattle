using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.Tanks
{
    public class PlayerTank: Tank
    {
        private const byte playerLives = 5;

        private byte lives;
        private bool isGameOver = false;
        private int tanksDestroyed = 0; 

        public PlayerTank()
            : base(25, 38, 100, 20, 50, ConsoleColor.DarkGray, Directions.Up)
        {
            this.lives = playerLives;
        }

        public PlayerTank(ConsoleColor color)
            : base(25, 38, 100, 20, 50, color, Directions.Up)
        {
            this.lives = playerLives;
        }

        public bool IsGameOver { get { return this.isGameOver; } }
        public byte Lives { get { return this.Lives; } }

        public int TanksDestroyed { get { return this.tanksDestroyed; } }

        public void LooseLive()
        {
            this.lives--;
            if (this.lives == 0)
            {
                isGameOver = true;
            }
        }

        public void DestroyTank()
        {
            this.tanksDestroyed++;
        }

        public override string[] LoadVisual()
        {
            return base.LoadVisual();
        }

        public override void Print()
        {
            base.Print();
        }
    }
}
