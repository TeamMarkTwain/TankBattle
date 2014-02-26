using System;
using System.Collections.Generic;
using System.Linq;
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
            DateTime timeGameStart = DateTime.Now;

            Level currentLevel = new Level(level);
            List<CannonBall> cannonBalls = new List<CannonBall>();
            List<Tank> enemyTanks = new List<Tank>();
            SoundEngine.StartGameSound();

            PlayerTank playerTank = player.PersonalTank;

            enemyTanks.Add(player.PersonalTank);

            List<LevelObject> allLevelObjects = new List<LevelObject>(currentLevel.LoadLevel());
            allLevelObjects.Add(player.PersonalTank);

            int enemyTankPosition = 1;
            int enemyTanksCount = 5;

            for (int i = 0; i < enemyTanksCount; i++)
            {
                allLevelObjects.Add(new EnemySmartTank(playerTank, allLevelObjects, enemyTankPosition, 1, Directions.Down));
                enemyTankPosition += 15;
            }

            currentLevel.PrintLevel();
            playerTank.Print();

            for (int i = 0; i < allLevelObjects.Count; i++)
            {
                if (allLevelObjects[i] is EnemySmartTank)
                {
                    allLevelObjects[i].Print();
                }
            }

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow || pressedKey.Key == ConsoleKey.DownArrow ||
                        pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (HitManager.ManageTankAndWallHit(playerTank, allLevelObjects, pressedKey)) // changed from LevelObjects
                        {
                            playerTank.Move(pressedKey);
                            if (DateTime.Now > timeGameStart.AddSeconds(5))
                            {
                                SoundEngine.MoveSound();
                            }
                            
                        }

                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        //implement fire
                        SoundEngine.FireSound();
                        int[] barrelCoords = playerTank.GetTankBarrel();
                        cannonBalls.Add(new SimpleCannonBall(playerTank.Y + barrelCoords[0], playerTank.X + barrelCoords[1], 1, 100, playerTank.Direction));
                    }
                    else if (pressedKey.Key == ConsoleKey.Escape)
                    {
                        Menu.LoadPauseMenu(player);
                    }
                }

                Thread.Sleep(50);

                // If players tank is hitted, lose life
                // Shoot if possible and update direction and position
                for (int i = 0; i < allLevelObjects.Count; i++)
                {
                    if (allLevelObjects[i] is PlayerTank)
                    {
                        PlayerTank currentPlayerTank = allLevelObjects[i] as PlayerTank;

                        if (currentPlayerTank.IsDestroyed)
                        {
                            currentPlayerTank.LooseLive();
                            currentPlayerTank.SetDefaultValues();
                            currentPlayerTank.Print();
                            

                            if (currentPlayerTank.IsGameOver)
                            {
                                // What to do if player tank is dead ?
                                EndGame(player, currentLevel.LevelNumber);
                            }
                            continue;
                        }
                    }

                    if (allLevelObjects[i] is EnemySmartTank)
                    {
                        EnemySmartTank currentEnemyTank = allLevelObjects[i] as EnemySmartTank;
                        if (currentEnemyTank.CanShootToPlayertank())
                        {
                            int[] barrelCoords = (allLevelObjects[i] as EnemySmartTank).GetTankBarrel();
                            cannonBalls.Add(new SimpleCannonBall(allLevelObjects[i].Y + barrelCoords[0], allLevelObjects[i].X + barrelCoords[1], 1, 100, currentEnemyTank.Direction));
                        }

                        currentEnemyTank.Update();
                    }
                }

                // Remove destroyed objects
                for (int i = 0; i < allLevelObjects.Count; i++)
                {
                    if ((allLevelObjects[i] is IDestroyable) && !(allLevelObjects[i] is PlayerTank))
                    {
                        if ((allLevelObjects[i] as IDestroyable).IsDestroyed)
                        {
                            // Check coordinate to clear for different type of objects(tank, cannonball, brick)
                            ConsoleAction.Clear(allLevelObjects[i].X, allLevelObjects[i].Y, allLevelObjects[i].LoadVisual()[0].Length, allLevelObjects[i].LoadVisual().Length);
                            allLevelObjects.RemoveAt(i);
                        }
                    }
                }
                //check if base is dstroyed
                if (HitManager.IsBaseDestroyed(allLevelObjects)) 
                {
                    EndGame(player, currentLevel.LevelNumber);
                }

                // Move all cannonballs
                for (int i = 0; i < cannonBalls.Count; i++)
                {
                    ConsoleAction.Clear(cannonBalls[i].Y, cannonBalls[i].X, 1, 1);

                    cannonBalls[i].Move();
                    cannonBalls[i].Print();
                }

                // Check if some cannonball hit an object
                HitManager.ManageShotsAndLevelObject(cannonBalls, allLevelObjects);

                // Remove destroyed cannonballs
                for (int i = 0; i < cannonBalls.Count; i++)
                {
                    if (cannonBalls[i].IsDestroyed)
                    {
                        ConsoleAction.Clear(cannonBalls[i].Y, cannonBalls[i].X, 1, 1);
                        cannonBalls.RemoveAt(i);
                    }
                }

                if (!allLevelObjects.Any(x => x is EnemyTank))
                {
                    return;
                }

                player.AddScore(2); //for surviving time 
                PrintStats(player, playerTank);
            }
        }

        private static void PrintStats(PlayerProfile player, PlayerTank tank)  //stack overflow problem !!!!
        {
            ConsoleAction.PrintOnPos(string.Format("Name: {0}", player.Name), 10, 42, ConsoleColor.Cyan);
            //ConsoleAction.PrintOnPos(string.Format("Lives: {0}", tank.Lives), 19 + player.Name.Length, 42, ConsoleColor.Cyan);
            ConsoleAction.PrintOnPos(string.Format("Score: {0}", player.CurrentScore), 25 + player.Name.Length, 42, ConsoleColor.Cyan);
        }

        private static void EndGame(PlayerProfile player, int levelNumber) 
        {
            Console.Clear();
            SoundEngine.EndGameSound();

            ConsoleAction.PrintOnPos("GAME OVER !!!", 35, 20, ConsoleColor.Red);
            ConsoleAction.PrintOnPos(string.Format("Your Score: {0} points",player.CurrentScore ), 35, 24, ConsoleColor.Green);
            ConsoleAction.PrintOnPos("Press any key to continue", 35, 28, ConsoleColor.White);
            Console.ReadKey();

            player.SetScore(player.CurrentScore, (byte)levelNumber);
            ProfileManager.WriteToFile(player);

            Menu.LoadMainMenu(player);
        }
    }
}
