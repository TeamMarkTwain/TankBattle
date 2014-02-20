using System;
using System.IO;
using TankBattle.LevelObjects;

namespace TankBattle
{
    public class Level
    {
        private LevelObject[,] level;
        private int levelNumber;
        private int levelHeight = 14;
        private int levelWidth = 13;
        private static int playgroundHeight = 42;
        private static int playgroundWidth = 80;

        public Level(int levelNumber)
        {
            this.levelNumber = levelNumber;
            LoadLevel();
        }

        public int LevelNumber
        {
            get { return this.levelNumber; }
        }

        public static int PlaygroundWidth()
        {
            return playgroundWidth;
        }

        public static int PlaygroundHeight()
        {
            return playgroundHeight; 
        }

        private void LoadLevel()
        {
            string levelPath = string.Format("../../Levels/{0}.txt", this.levelNumber);
            level = new LevelObject[levelHeight, levelWidth];
            using (StreamReader reader = new StreamReader(levelPath))
            {
                int indexX = 1, indexY= 1;
                for (int i = 0; i < levelHeight; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }
                    string[] objects = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < objects.Length; j++)
                    {
                       
                        if (objects[j] == "B") //bricks
                        {
                            level[i, j] = new BrickWall(indexX , indexY);
          
                        }
                        else if (objects[j] == "S") // steel
                        {
                            level[i, j] = new SteelWall(indexX, indexY);
                        }
                        else if (objects[j] == "R") // river
                        {
                            level[i, j] = new River(indexX, indexY);
                        }
                        else if (objects[j] == "E") // empty
                        {
                            level[i, j] = null;
                        }
                        else if (objects[j] == "G") //grass
                        {
                            level[i, j] = new Grass(indexX, indexY);
                        }
                        indexX += 6;
                    }
                    indexX = 1;
                    indexY += 3;
                }

                
            }
        }

        public void PrintLevel()
        {
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    if (level[i, j] == null) continue;
                    
                    level[i, j].Print();
                }

            }
            for (int y = 0; y < playgroundWidth; y++)
           {
               ConsoleAction.PrintCharOnPos(Symbols.GetChar(219), 0, y, ConsoleColor.Gray);
               ConsoleAction.PrintCharOnPos(Symbols.GetChar(219), playgroundHeight - 2, y, ConsoleColor.Gray);
           }
           for (int x = 0; x < playgroundHeight - 1; x++)
           {
               ConsoleAction.PrintCharOnPos(Symbols.GetChar(219), x, 0, ConsoleColor.Gray);
               ConsoleAction.PrintCharOnPos(Symbols.GetChar(219), x, playgroundWidth - 1, ConsoleColor.Gray);
           }
        }
    }
}
