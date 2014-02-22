using System;
using System.Text;

namespace TankBattle
{
    class Symbols
    {
        private static char[] chars;
        public static void LoadChars() 
        {
            chars = new char[255];
            for (byte i = 0; i < 255; i++)
            {
                chars[i] = Encoding.GetEncoding(437).GetChars(new byte[] { i })[0];
            }
        }
        public static char GetChar(byte charNum) 
        {
            return chars[charNum];
        }
    }
}
