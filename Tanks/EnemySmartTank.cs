using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle.Tanks
{
    public class EnemySmartTank : EnemyTank, IPrintable, IHitable, ICanShoot, IDestroyable
    {
        private Random randomizer = new Random();
        private List<LevelObject> levelObjects;

        public EnemySmartTank(PlayerTank playerTank, List<LevelObject> levelObjects, int x, int y, Directions direction, int health = 100, int speed = 20, int shootPower = 50, ConsoleColor color = ConsoleColor.Cyan)
            : base(x, y, playerTank, direction, health, speed, shootPower, color)
        {
            this.levelObjects = levelObjects;
        }

        public override void Update()
        {
            if (randomizer.Next(0, 101) > 70)
            {
                this.Move(this.Think());
            }

        }

        // Look for the player tank, if in the same row or col return true(shoot).
        public bool CanShootToPlayertank()
        {
            bool canShoot = false;

            if (playerTank.X == this.X)
            {
                if (playerTank.Y < this.Y && this.Direction == Directions.Up)
                {
                    canShoot = true;
                }
                else if (playerTank.Y > this.Y && this.Direction == Directions.Down)
                {
                    canShoot = true;
                }
            }
            else if (playerTank.Y == this.Y)
            {
                if (playerTank.X < this.X && this.Direction == Directions.Left)
                {
                    canShoot = true;
                }
                else if (playerTank.X > this.X && this.Direction == Directions.Right)
                {
                    canShoot = true;
                }
            }

            return canShoot;
        }

        private ConsoleKeyInfo Think()
        {
            ConsoleKeyInfo newDirection = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);

            int minDistance = int.MaxValue;
            int distance = 0;

            bool isLeftDirectionAvailable = HitManager.IsDirectionLeftAvailable(this, this.levelObjects);
            bool isRightDirectionAvailable = HitManager.IsDirectionRightAvailable(this, this.levelObjects);
            bool isUpDirectionAvailable = HitManager.IsDirectionUpAvailable(this, this.levelObjects);
            bool isDownDirectionAvailable = HitManager.IsDirectionDownAvailable(this, this.levelObjects);

            if (this.Direction == Directions.Down)
            {
                if (isLeftDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Left);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false);
                    }
                }

                if (isDownDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Down);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);
                    }
                }

                if (isRightDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Right);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);
                    }
                }
            }
            else if (this.Direction == Directions.Up)
            {
                if (isLeftDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Left);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false);
                    }
                }

                if (isRightDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Right);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);
                    }
                }

                if (isUpDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Up);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false);
                    }
                }
            }
            else if (this.Direction == Directions.Left)
            {
                if (isLeftDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Left);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false);
                    }
                }

                if (isDownDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Down);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);
                    }
                }

                if (isUpDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Up);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false);
                    }
                }
            }
            else if (this.Direction == Directions.Right)
            {
                if (isUpDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Up);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false);
                    }
                }

                if (isDownDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Down);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);
                    }
                }

                if (isRightDirectionAvailable)
                {
                    distance = CalcDistanceBetweenTanks(Directions.Right);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newDirection = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);
                    }
                }
            }

            return newDirection;
        }


    }
}
