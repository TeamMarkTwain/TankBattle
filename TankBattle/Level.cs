using System;
using System.Collections.Generic;
using System.IO;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle
{
    public class Level : IPrintable
    {
        private List<IPrintable> levelObjects;
        private int levelNumber;
        private int levelHeight = 14;
        // private int levelWidth = 13;
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

            levelObjects = new List<IPrintable>();

            using (StreamReader reader = new StreamReader(levelPath))
            {
                FieldCoords objectsPosition = new FieldCoords(1, 1);

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
                            levelObjects.Add(new BrickWall(objectsPosition));

                        }
                        else if (objects[j] == "S") // steel
                        {
                            levelObjects.Add(new SteelWall(objectsPosition));
                        }
                        else if (objects[j] == "R") // river
                        {
                            levelObjects.Add(new River(objectsPosition));
                        }
                        else if (objects[j] == "E") // empty
                        {
                            //levelObjects[i, j] = null;
                        }
                        else if (objects[j] == "G") //grass
                        {
                            levelObjects.Add(new Grass(objectsPosition));
                        }

                        objectsPosition.X += 6;
                    }

                    objectsPosition.X = 1;
                    objectsPosition.Y += 3;
                }
            }
        }

        public List<IPrintable> GetLevelObjects()
        {
            return this.levelObjects;
        }

        public char[,] GetImage()
        {
            return new char[0, 0];
        }

        public FieldCoords GetTopLeft()
        {
            return new FieldCoords();
        }
    }
}
