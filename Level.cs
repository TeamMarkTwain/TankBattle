using System;
using System.Collections.Generic;
using System.IO;
using TankBattle.LevelObjects;

namespace TankBattle
{
    public class Level
    {
        private List<LevelObject> level;
        private int levelNumber;
        private int levelHeight = 14;
        private int levelWidth = 13;
        private static int playgroundHeight = 42;
        private static int playgroundWidth = 80;

        public Level(int levelNumber)
        {
            this.levelNumber = levelNumber;
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

        public List<LevelObject> LoadLevel()
        {
            string levelPath = string.Format("../../Levels/{0}.txt", this.levelNumber);
            level = new List<LevelObject>();

            using (StreamReader reader = new StreamReader(levelPath))
            {
                int indexX = 1, indexY = 1;
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
                            level.Add(new BrickWall(indexX, indexY));

                        }
                        else if (objects[j] == "S") // steel
                        {
                            level.Add(new SteelWall(indexX, indexY));
                        }
                        else if (objects[j] == "R") // river
                        {
                            level.Add(new River(indexX, indexY));
                        }
                        else if (objects[j] == "E") // empty
                        {
                            
                        }
                        else if (objects[j] == "G") //grass
                        {
                            level.Add(new Grass(indexX, indexY));
                        }
                        else if (objects[j] == "H")
                        {
                            level.Add(new Base(indexX, indexY));
                        }

                        indexX += 6;
                    }

                    indexX = 1;
                    indexY += 3;
                }
            }

            return level;
        }

        public void PrintLevel()
        {
            for (int i = 0; i < level.Count; i++)
            {
                    level[i].Print();
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
