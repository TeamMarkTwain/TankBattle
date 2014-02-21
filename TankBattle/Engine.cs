using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle
{
    class Engine
    {
        private IPrinter printer;
        private Level level;
        List<IPrintable> objs;

        public Engine(IPrinter printer, int level)
        {
            this.printer = printer;
            this.level = new Level(level);
            InitializeFieldObject(this.level, this.printer);
        }

        private void InitializeFieldObject(Level level, IPrinter printer)
        {
            this.objs = level.GetLevelObjects();

            foreach (var obj in objs)
            {
                printer.EnqueueForPrinting(obj);
            }
        }

        public void Run()
        {
            // For now the loop is not needed

            //while (true)
            //{
                printer.PrintAll();
                printer.ClearQueue();
                // ...
            //}
        }
    }
}
