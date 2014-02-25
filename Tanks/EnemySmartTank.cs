using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle.Tanks
{
    public class EnemySmartTank : Tank, IPrintable, IHitable, ICanShoot, IDestroyable
    {
        private Random randomizer = new Random();
        private PlayerTank playerTank;
        private List<LevelObject> levelObjects;

        public EnemySmartTank(PlayerTank playerTank, List<LevelObject> levelObjects, int x, int y, Directions direction, int health = 100, int speed = 20, int shootPower = 50, ConsoleColor color = ConsoleColor.Cyan)
            : base(x, y, health, speed, shootPower, color, direction)
        {
            this.levelObjects = levelObjects;
            this.playerTank = playerTank;
        }

        public void Update()
        {
            this.Move(this.Think());
        }

        // Look for the player tank, if in the same row or col return true.
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

            bool isLeftDirectionAvailable = HitManager.ManageTankAndWallHit(this, this.levelObjects, new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false));
            bool isRightDirectionAvailable = HitManager.ManageTankAndWallHit(this, this.levelObjects, new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false));
            bool isUpDirectionAvailable = HitManager.ManageTankAndWallHit(this, this.levelObjects, new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false));
            bool isDownDirectionAvailable = HitManager.ManageTankAndWallHit(this, this.levelObjects, new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false));

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

        private int CalcDistanceBetweenTanks(Directions direction)
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
    }
}
