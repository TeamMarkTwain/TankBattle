using System;
using System.Collections.Generic;
using TankBattle.Tanks;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TankBattle.LevelObjects;
using TankBattle.Interfaces;
using TankBattle.MenuItems;

namespace TankBattle
{
    public static class Engine
    {
        private static bool[,] fieldObjects = new bool[Level.PlaygroundHeight(), Level.PlaygroundWidth()];
        private static Random randomGenerator = new Random();
        public static void StartGame(int level, PlayerProfile player)
        {
            List<CannonBall> cannonBalls = new List<CannonBall>();
            Level currentLevel = new Level(level);
            List<LevelObject> levelObjects = currentLevel.LoadLevel();
            List<Tank> enemyTanks = new List<Tank>();
            PlayerTank playerTank = player.PersonalTank;

            enemyTanks.Add(player.PersonalTank);
            //List<Tank> allTanks = new List<Tank>(enemyTanks);
            //allTanks.Add(playerTank);

            int enemyTankPosition = 1;
            int enemyTanksCount = 3;

            for (int i = 0; i < enemyTanksCount; i++)
            {
                enemyTanks.Add(new EnemySmartTank(playerTank, levelObjects, enemyTankPosition, 1, Directions.Down));
                enemyTankPosition += 15;
            }

            currentLevel.PrintLevel();
            playerTank.Print();

            for (int i = 0; i < enemyTanks.Count; i++)
            {
                enemyTanks[i].Print();
            }

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow || pressedKey.Key == ConsoleKey.DownArrow ||
                        pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (HitManager.ManageTankAndWallHit(playerTank, levelObjects, pressedKey))
                        {
                            playerTank.Move(pressedKey);
                        }
                        
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        //implement fire
                        int[] barrelCoords = playerTank.GetTankBarrel();
                        cannonBalls.Add(new SimpleCannonBall(playerTank.Y + barrelCoords[0], playerTank.X + barrelCoords[1], 1, 100, playerTank.Direction));
                    }
                    else if (pressedKey.Key == ConsoleKey.Escape)
                    {
                        Menu.LoadPauseMenu(player);
                    }
                }

                Thread.Sleep(50);

                // Shoot if possible and update direction and position
                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    if (enemyTanks[i] is EnemySmartTank)
                    {
                        if ((enemyTanks[i] as EnemySmartTank).CanShootToPlayertank())
                        {
                            int[] barrelCoords = enemyTanks[i].GetTankBarrel();
                            cannonBalls.Add(new SimpleCannonBall(enemyTanks[i].Y + barrelCoords[0], enemyTanks[i].X + barrelCoords[1], 1, 100, enemyTanks[i].Direction));
                        }

                        (enemyTanks[i] as EnemySmartTank).Update();
                    }
                }

                // Move all cannonballs
                for (int i = 0; i < cannonBalls.Count; i++)
                {
                    ConsoleAction.Clear(cannonBalls[i].Y, cannonBalls[i].X, 1, 1);

                    cannonBalls[i].Move();
                    cannonBalls[i].Print();
                }

                // Check if some cannonball hit a tank
                HitManager.ManageShotsAndTanks(cannonBalls, enemyTanks);

                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    if (enemyTanks[i].IsDestroyed)
                    {
                        // Check if current tank is player tank
                        if (enemyTanks[i] is PlayerTank)
                        {
                            (enemyTanks[i] as PlayerTank).LooseLive();

                            if ((enemyTanks[i] as PlayerTank).IsGameOver)
                            {
                                // What to do if player tank is dead ?
                                return;
                            }
                        }
                        else
                        {
                            ConsoleAction.Clear(enemyTanks[i].X, enemyTanks[i].Y, enemyTanks[i].GetVisual()[0].Length, enemyTanks[i].GetVisual().Length);
                            enemyTanks.RemoveAt(i);
                        }
                    }
                }

                // Check if some cannonball hit a wall(brick)
                HitManager.ManageShotsAndLevelObject(cannonBalls, levelObjects);

                // If some of the bricks is destroyed, remove it
                for (int i = 0; i < levelObjects.Count; i++)
                {
                    IDestroyable obj = levelObjects[i] as IDestroyable;
                    if (levelObjects[i] is IDestroyable)
                    {
                        if ((levelObjects[i] as IDestroyable).IsDestroyed)
                        {
                            ConsoleAction.Clear(levelObjects[i].X, levelObjects[i].Y, levelObjects[i].LoadVisual()[0].Length, levelObjects[i].LoadVisual().Length);
                            levelObjects.RemoveAt(i);
                        }
                    }
                }

                // If cannonball is destroyed, remove it
                for (int i = 0; i < cannonBalls.Count; i++)
                {
                    if (cannonBalls[i].IsDestroyed)
                    {
                        ConsoleAction.Clear(cannonBalls[i].Y, cannonBalls[i].X, 1, 1);
                        cannonBalls.RemoveAt(i);
                    }
                }
            }
        }

        private static void PrintStats(PlayerProfile player)  //stack overflow problem !!!!
        {
            ConsoleAction.PrintOnPos(string.Format("Name: {0}", player.Name), 10, 42, ConsoleColor.Cyan);
            //ConsoleAction.PrintOnPos(string.Format("Lives: {0}", playerTank.Lives), 19 + player.Name.Length, 42, ConsoleColor.Cyan);
            //ConsoleAction.PrintOnPos(string.Format("Destroyed: {0}", tankDestroyed), 25 + name.Length, 43, ConsoleColor.Cyan);
        }

        private static void EndGame(PlayerProfile player) { }
    }
}
