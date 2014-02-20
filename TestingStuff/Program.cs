using System;
using TankBattle.LevelObjects;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TankBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 45;
            Console.WindowWidth = 90;

            Console.CursorVisible = false;
            Console.Title = "Tank Battles";

            Symbols.LoadChars();

            Engine.StartGame();
        }
    }
}
