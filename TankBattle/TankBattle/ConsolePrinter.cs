using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Interfaces;

namespace TankBattle
{
    class ConsolePrinter : IPrinter
    {
        int consoleMatrixRows;
        int consoleMatrixCols;
        char[,] consoleMatrix;

        public ConsolePrinter(int visibleConsoleRows, int visibleConsoleCols)
        {
            consoleMatrix = new char[visibleConsoleRows, visibleConsoleCols];

            this.consoleMatrixRows = visibleConsoleRows;
            this.consoleMatrixCols = visibleConsoleCols;
            // this.ClearQueue();
        }

        public void EnqueueForPrinting(IPrintable obj)
        {
            if (obj != null)
            {
                char[,] objImage = obj.GetImage();

                int imageRows = objImage.GetLength(0);
                int imageCols = objImage.GetLength(1);

                FieldCoords objTopLeft = obj.GetTopLeft();

                int lastRow = Math.Min(objTopLeft.Y + imageRows, this.consoleMatrixRows);
                int lastCol = Math.Min(objTopLeft.X + imageCols, this.consoleMatrixCols);

                for (int row = objTopLeft.Y; row < lastRow; row++)
                {
                    for (int col = objTopLeft.X; col < lastCol; col++)
                    {
                        if (row >= 0 && row < consoleMatrixRows &&
                            col >= 0 && col < consoleMatrixCols)
                        {
                            consoleMatrix[row, col] = objImage[row - obj.GetTopLeft().Y, col - obj.GetTopLeft().X];
                        }
                    }
                }
            }
        }

        public void PrintAll()
        {
            Console.SetCursorPosition(0, 0);

            StringBuilder scene = new StringBuilder();

            for (int row = 0; row < this.consoleMatrixRows; row++)
            {
                for (int col = 0; col < this.consoleMatrixCols; col++)
                {
                    scene.Append(this.consoleMatrix[row, col]);
                }

                scene.Append(Environment.NewLine);
            }

            Console.WriteLine(scene.ToString());
        }

        public void ClearQueue()
        {
            for (int row = 0; row < this.consoleMatrixRows; row++)
            {
                for (int col = 0; col < this.consoleMatrixCols; col++)
                {
                    this.consoleMatrix[row, col] = ' ';
                }
            }
        }
    }
}
