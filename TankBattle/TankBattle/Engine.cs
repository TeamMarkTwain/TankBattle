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
        private List<LevelObject> objs;
        private CannonBall cb = new SimpleCannonBall(new FieldCoords(19, 1), Directions.Down, new FieldCoords(0, 1));


        public Engine(IPrinter printer, int level)
        {
            this.printer = printer;
            this.level = new Level(level);
            InitializeFieldObject(this.level, this.printer);
        }

        private void InitializeFieldObject(Level level, IPrinter printer)
        {
            this.objs = level.GetLevelObjects();

            this.printer.EnqueueForPrinting(cb);

            foreach (var obj in objs)
            {
                printer.EnqueueForPrinting(obj);
            }
        }



        public void Run()
        {
            // For now the loop is not needed
            objs.Add(cb);

            while (true)
            {
                System.Threading.Thread.Sleep(500);

                Console.Clear();
                Console.SetCursorPosition(0, 0);
                printer.PrintAll();
                printer.ClearQueue();



                for (int i = 0; i < objs.Count; i++)
                {
                    if (objs[i] is IMoveable)
                    {
                        (objs[i] as IMoveable).Move();
                    }


                }

                HitManager.ManageHits(new List<CannonBall> { cb }, objs);
                for (int i = 0; i < objs.Count; i++)
                {
                    if (objs[i] is IDestroyable)
                    {
                        if ((objs[i] as IDestroyable).IsDestroyed)
                        {
                            objs.RemoveAt(i);
                        }
                    }
                }

                for (int i = 0; i < objs.Count; i++)
                {
                    printer.EnqueueForPrinting(objs[i]);
                }
            }
        }
    }
}
