using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public static class Engine
    {
        public static void StartGame()
        {
            Level firstLevel = new Level(1);
            firstLevel.PrintLevel();

            Tank playerTank = new Tank();

            playerTank.Print();

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow || pressedKey.Key == ConsoleKey.DownArrow ||
                        pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        playerTank.Move(pressedKey);
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        //implement fire
                    }
                    else if (pressedKey.Key == ConsoleKey.Escape)
                    {
                        //implement pause  and  pause menu (exit, continue ... )
                    }
                }
            }
        }
    }
}
