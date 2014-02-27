using System;
using System.Linq;
using TankBattle.Profile;
using TankBattle.MenuItems;

namespace TankBattle
{
    public class TankBattleMain
    {
      
        static void Main(string[] args)
        {
            AditionalSettings();

            Menu.PrintLogo();

            PlayerProfile currentProfile = Menu.LoadProfileMenu();

            Menu.LoadMainMenu(currentProfile);
        }

        private static void AditionalSettings()
        {

            Console.WindowHeight = 45;
            Console.WindowWidth = 80;

            Console.CursorVisible = false;
            Console.Title = "Tank Battle";

            Symbols.LoadChars();
        }
    }
}
