using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle
{
    public static class HitManager
    {
        public static void ManageHits(List<CannonBall> shots, List<LevelObject> targets)
        {
            ManageShotsAndTargets(shots, targets);
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

            char[,] targetImage = target.GetImage();

            int shotX = shot.Position.X;
            int shotY = shot.Position.Y;

            int targetMinX = target.Position.X;
            int targetMinY = target.Position.Y;
            int targetMaxX = target.Position.X + target.GetImage().GetLength(1);
            int targetMaxY = target.Position.Y + target.GetImage().GetLength(0);

            //if (shot.Direction == Directions.Up || shot.Direction == Directions.Down)
            //{
                if (shotX >= targetMinX && shotX <= targetMaxX &&
                    shotY >= targetMinY && shotY <= targetMaxY)
                {
                    isHitted = true;
                }
            //}

            //if (shot.Direction == Directions.Left || shot.Direction == Directions.Right)
            //{
            //    if (shotY >= targetMinY && shotY <= targetMaxY)
            //    {
            //        isHitted = true;
            //    }
            //}

            return isHitted;
        }
    }
}
