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
        public static void StartGame(int level, PlayerProfile player)
        {
            List<CannonBall> cannonBalls = new List<CannonBall>();
            Level currentLevel = new Level(level);
            List<LevelObject> levelObjects = currentLevel.LoadLevel();
            List<EnemySmartTank> enemyTanks = new List<EnemySmartTank>();
            PlayerTank playerTank = player.PersonalTank;

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
                        Menu.LoadPauseMenu();
                    }
                }

                Thread.Sleep(50);

                for (int i = 0; i < cannonBalls.Count; i++)
                {
                    ConsoleAction.Clear(cannonBalls[i].Y, cannonBalls[i].X, 1, 1);

                    cannonBalls[i].Move();
                    cannonBalls[i].Print();
                }

                // enemy tank part
                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    if (enemyTanks[i].CanShootToPlayertank())
                    {
                        int[] barrelCoords = enemyTanks[i].GetTankBarrel();
                        cannonBalls.Add(new SimpleCannonBall(enemyTanks[i].Y + barrelCoords[0], enemyTanks[i].X + barrelCoords[1], 1, 100, enemyTanks[i].Direction));
                    }

                    enemyTanks[i].Update();
                }

                HitManager.ManageHits(cannonBalls, levelObjects);

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
