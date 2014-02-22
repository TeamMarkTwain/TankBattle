using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle
{
    class TankBattleMain
    {
        static void Main(string[] args)
        {
            Level level = new Level(1);

            ConsolePrinter printer = new ConsolePrinter(30, 50);

            Engine engine = new Engine(printer, 1);

            engine.Run();
        }
    }
}
