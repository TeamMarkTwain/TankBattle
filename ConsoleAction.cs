using System;

namespace TestingStuff
{
    public static class ConsoleAction
    {
        public static void Clear(int x, int y, int width, int height)
        {
            int curTop = Console.CursorTop;
            int curLeft = Console.CursorLeft;
            for (; height > 0; )
            {
                Console.SetCursorPosition(x, y + --height);
                Console.Write(new string(' ', width));
            }
            Console.SetCursorPosition(curLeft, curTop);
        }

        public static void PrintOnPos(string[] str, int x, int y, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            foreach (var item in str)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(item);
                y++;
            }
        }

        public static void PrintOnPos(string[] str, int x, int y, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;

            foreach (var item in str)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(item);
                y++;
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void PrintCharOnPos(char ch, int y, int x, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.SetCursorPosition(x, y);
            Console.Write(ch);
        }
    }
}
