using System;
using System.Collections.Generic;
using System.Linq;
using TankBattle.Tanks;
using System.Threading;
using TankBattle.LevelObjects;
using TankBattle.Interfaces;
using TankBattle.MenuItems;

namespace TankBattle
{
    public static class Engine
    {
        private const int gamerLives = 5;
        public static void StartGame(int level, PlayerProfile player)
        {
            DateTime timeGameStart = DateTime.Now;
            //To use it for add score for every play time minute
            int passedMinutes = 1;
            int waves = 1 + level;
            int lives = gamerLives;

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
            enemyTankPosition = 1;

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
                        cannonBalls.Add(new SimpleCannonBall(playerTank.Y + barrelCoords[0], playerTank.X + barrelCoords[1], 1, 100, playerTank.Direction, true));
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
                            lives--;
                            currentPlayerTank.LooseLive();

                            //clear position
                            if (currentPlayerTank.Direction == Directions.Up || currentPlayerTank.Direction == Directions.Down)
                            {
                                ConsoleAction.Clear(playerTank.X, playerTank.Y, 6, 2);
                            }
                            else if (currentPlayerTank.Direction == Directions.Left || currentPlayerTank.Direction == Directions.Right)
                            {
                                ConsoleAction.Clear(playerTank.X, playerTank.Y, 3, 3);
                            }
                            currentPlayerTank.SetDefaultPosition();
                            currentPlayerTank.Print();
                            

                            if (currentPlayerTank.IsGameOver)
                            {
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
                            cannonBalls.Add(new SimpleCannonBall(allLevelObjects[i].Y + barrelCoords[0], allLevelObjects[i].X + barrelCoords[1], 1, 100, currentEnemyTank.Direction, false));
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
                HitManager.ManageShotsAndLevelObject(cannonBalls, allLevelObjects, player);

                // Remove destroyed cannonballs
                for (int i = 0; i < cannonBalls.Count; i++)
                {
                    if (cannonBalls[i].IsDestroyed)
                    {
                        ConsoleAction.Clear(cannonBalls[i].Y, cannonBalls[i].X, 1, 1);
                        cannonBalls.RemoveAt(i);
                    }
                }
                //Destroy enemies
                if (!allLevelObjects.Any(x => x is EnemyTank))
                {
                    waves--;
                    if (waves == 0)
                    {
                        player.AddScore(500);
                        WinGame(player, currentLevel.LevelNumber);
                    }
                    else
                    {
                        for (int i = 0; i < enemyTanksCount; i++)
                        {
                            allLevelObjects.Add(new EnemySmartTank(playerTank, allLevelObjects, enemyTankPosition, 1, Directions.Down));
                            enemyTankPosition += 15;
                        }
                        for (int i = 0; i < allLevelObjects.Count; i++)
                        {
                            if (allLevelObjects[i] is EnemySmartTank)
                            {
                                allLevelObjects[i].Print();
                            }
                        }
                        enemyTankPosition = 1;
                    }
                }

                //Add score point for playing minute
                if (DateTime.Now.Minute == timeGameStart.AddMinutes(passedMinutes).Minute)
                {
                    passedMinutes++;
                    player.AddScore(10);

                }
                RePrintLevelObjects(allLevelObjects);
                PrintStats(player, lives);
            }
        }
        //To keep Rivers and grass
        private static void RePrintLevelObjects(List<LevelObject> levelObjects)
        {
            foreach (var item in levelObjects)
            {
                if (item is IReprintable)
                {
                    item.Print();
                }
            }
        }

        private static void PrintStats(PlayerProfile player, int lives)  //stack overflow problem !!!!
        {
            ConsoleAction.PrintOnPos(string.Format("Name: {0}", player.Name), 10, 42, ConsoleColor.Cyan);
            ConsoleAction.PrintOnPos(string.Format("Lives: {0}", lives), 19 + player.Name.Length, 42, ConsoleColor.Cyan);
            ConsoleAction.PrintOnPos(string.Format("Score: {0}", player.CurrentScore), 29 + player.Name.Length, 42, ConsoleColor.Cyan);
        }

        private static void EndGame(PlayerProfile player, int levelNumber) 
        {
            Console.Clear();
            SoundEngine.EndGameSound();

            ConsoleAction.PrintOnPos("GAME OVER !!!", 35, 20, ConsoleColor.Red);
            ConsoleAction.PrintOnPos(string.Format("Your Score: {0} points",player.CurrentScore ), 35, 24, ConsoleColor.Green);
            ConsoleAction.PrintOnPos("Press Esc or Enter key to continue", 35, 28, ConsoleColor.White);

            player.SetScore(player.CurrentScore, (byte)levelNumber);
            ProfileManager.WriteToFile(player);
            SetDefaults(player);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.Escape || pressedKey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Menu.LoadMainMenu(player);
                    }
                }
            }
            
        }

        private static void WinGame(PlayerProfile player, int levelNumber)
        {
            Console.Clear();
            SoundEngine.StartGameSound();
            player.SetScore(player.CurrentScore, (byte)levelNumber);
            ProfileManager.WriteToFile(player);

            ConsoleAction.PrintOnPos("YOU WIN !!!", 35, 20, ConsoleColor.Green);
            ConsoleAction.PrintOnPos(string.Format("Your Score: {0} points", player.CurrentScore), 35, 24, ConsoleColor.Green);

            SetDefaults(player);

            ConsoleAction.PrintOnPos("Press Esc to go back to the main menu", 35, 28, ConsoleColor.White);

            if (levelNumber < Level.NumberOfLevels())
            {
                ConsoleAction.PrintOnPos("Press Enter for the next level", 35, 32, ConsoleColor.White);
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        Menu.LoadMainMenu(player);
                    }
                    else if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        StartGame(levelNumber + 1, player);
                    }
                }
            }

        }

        public static void SetDefaults(PlayerProfile profile)
        {
            profile.PersonalTank.SetDefaultValues();
            profile.ResetCurrentScore();
        }
    }
}
