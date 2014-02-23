﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;
using TankBattle.Tanks;

namespace TankBattle
{
    public static class HitManager
    {
        public static void ManageHits(List<CannonBall> shots, List<LevelObject> targets)
        {
            ManageShotsAndTargets(shots, targets);
        }

        public static bool ManagetTankAndWallHit(Tank playerTank, List<LevelObject> levelObjects, ConsoleKeyInfo pressedKey)
        {
            if (playerTank.Direction == Directions.Up && pressedKey.Key == ConsoleKey.UpArrow)
            {
                foreach (LevelObject obj in levelObjects)
                {
                    //if (obj.Y+2 != playerTank.Y - 1) continue;

                    if (obj.Y+2 == playerTank.Y - 1 && (obj.X - playerTank.X <= 5 && obj.X - playerTank.X >= -5))
                    {
                        return false;
                    }
                }
            }
            else if (playerTank.Direction == Directions.Down && pressedKey.Key == ConsoleKey.DownArrow)
            {
                foreach (LevelObject obj in levelObjects)
                {
                    //if (obj.Y != playerTank.Y + 2) continue;

                    if (obj.Y == playerTank.Y + 2  && (obj.X - playerTank.X <= 5 && obj.X - playerTank.X >= -5))
                    {
                        return false;
                    }
                }
            }
            else if (playerTank.Direction == Directions.Left && pressedKey.Key == ConsoleKey.LeftArrow)
            {
                foreach (LevelObject obj in levelObjects)
                {
                    //if (obj.X+5 != playerTank.Y - 1) continue;

                    if (obj.X+5 == playerTank.X - 1 && (obj.Y - playerTank.Y <= 2 && obj.Y - playerTank.Y >= -2))
                    {
                        return false;
                    }
                }
            }
            else if (playerTank.Direction == Directions.Right && pressedKey.Key == ConsoleKey.RightArrow)
            {
                foreach (LevelObject obj in levelObjects)
                {
                    //if (obj.X+5 != playerTank.Y - 1) continue;

                    if (obj.X == playerTank.X + 3 && (obj.Y - playerTank.Y <= 2 && obj.Y - playerTank.Y >= -2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void ManageShotsAndTargets(List<CannonBall> shots, List<LevelObject> targets)
        {
            foreach (var shot in shots)
            {
                foreach (var target in targets)
                {
                    if (IsPositionEqual(shot, target))
                    {
                        if (target is IDestroyable)
                        {
                            int shotPower = shot.ShootPower;
                            int targetHealth = (target as IDestroyable).Health;

                            shot.LooseHealth(targetHealth);
                            (target as IDestroyable).LooseHealth(shotPower);
                        }
                        else if (target is IHitable)
                        {
                            shot.LooseHealth(shot.ShootPower);
                        }
                    }
                }
            }
        }

        private static bool IsPositionEqual(CannonBall shot, LevelObject target)
        {
            bool isHitted = false;

            string[] targetImage = target.LoadVisual();

            int shotX = shot.X;
            int shotY = shot.Y;

            int targetMinX = target.Y;
            int targetMaxX = target.Y + target.LoadVisual().Length - 1;

            int targetMinY = target.X;
            int targetMaxY = target.X + target.LoadVisual()[0].Length - 1;

            if (shotX >= targetMinX && shotX <= targetMaxX &&
                shotY >= targetMinY && shotY <= targetMaxY)
            {
                isHitted = true;
            }

            return isHitted;
        }
    }
}
