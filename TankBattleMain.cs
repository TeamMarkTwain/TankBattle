using System;
using TankBattle.LevelObjects;
using System.Linq;
using TankBattle.Tanks;

namespace TankBattle
{
    public class TankBattleMain
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 45;
            Console.WindowWidth = 90;

            Console.CursorVisible = false;
            Console.Title = "Tank Battle";

            Symbols.LoadChars();
            PlayerTank playerTank = new PlayerTank();

            Engine.StartGame(1, playerTank);
        }
    }
}
