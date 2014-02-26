using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle.Tanks
{
    public class EnemySimpleTank : EnemyTank
    {
        private Random randomGen = new Random();
        private bool isShooting = false;
        public EnemySimpleTank(int x, int y, Directions direction, PlayerTank playerTank)
            : base(x, y, playerTank, direction)
        {

        }

        public bool IsShooting { get { return this.isShooting; } }
        public override void Update()
        {
            this.Move(this.Think());
        }

        private ConsoleKeyInfo Think()
        {
            int generateCommandType = randomGen.Next(0, 101);

            if (generateCommandType > 30) //Return movecommand
            {

                if (randomGen.Next(0, 101) > 60)
                {
                    ConsoleKey command = DirectionToKey(this.Direction);
                    return new ConsoleKeyInfo(' ', command, false, false, false);
                }
                else
                {
                    int randomDirection = randomGen.Next(1, 5);
                    if (randomDirection == 1)
                    {
                        return new ConsoleKeyInfo(' ', DirectionToKey(Directions.Up), false, false, false);
                    }
                    else if (randomDirection == 2)
                    {
                        return new ConsoleKeyInfo(' ', DirectionToKey(Directions.Down), false, false, false);
                    }
                    else if (randomDirection == 3)
                    {
                        return new ConsoleKeyInfo(' ', DirectionToKey(Directions.Left), false, false, false);
                    }
                    else if (randomDirection == 4)
                    {
                        return new ConsoleKeyInfo(' ', DirectionToKey(Directions.Right), false, false, false);
                    }
                }
            }
            else// (generateCommandType > 10 && generateCommandType < 31) //Return fire command
            {
                this.isShooting = true;
                return new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false);
            }
            return new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false); //To get ridd off not all code return a vlue
        }

        private ConsoleKey DirectionToKey(Directions direction)
        {
            if (direction == Directions.Left)
            {
                return ConsoleKey.LeftArrow;
            }
            else if (direction == Directions.Right)
            {
                return ConsoleKey.RightArrow;
            }
            else if (direction == Directions.Up)
            {
                return ConsoleKey.UpArrow;
            }
            else
            {
                return ConsoleKey.DownArrow;
            }
        }

    }
}
