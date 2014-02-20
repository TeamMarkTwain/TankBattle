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

                    playerTank.Move(pressedKey);
                }
            }
        }
    }
}
