using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using TankBattle.Tanks;

namespace TankBattle.MenuItems
{
    public static class Menu
    {
        public static PlayerProfile LoadProfileMenu() 
        {
            List<MenuItem> menuItems = new List<MenuItem>()
            {
                new MenuItem(35, 18, "Create Profile", true),
                new MenuItem(35, 23, "Load Profile", false)
            };

            string command = LoadCommand(menuItems);

            if (command == "Create Profile")
            {
                Console.Clear();
                PrintLogo();
                sbyte profileNumber = PlayerProfile.GetEmptySloth();

                if (profileNumber < 0)
                {
                    ConsoleAction.PrintOnPos("No empty sloth! Choose a profile to replace it!", 30, 18, ConsoleColor.Red);
                    List<MenuItem> profileItems = PlayerProfile.GetProfilesAsMenu();

                    string deleteCommand = LoadCommand(profileItems);
                    sbyte position = 0;

                    foreach (var item in profileItems)
                    {
                        if (deleteCommand == item.Name)
                        {
                            break;
                        }
                        position++;
                    }

                    //PlayerProfile.DeleteProfile(position);
                    profileNumber = position;

                }

                ConsoleAction.PrintOnPos("Profile name: ", 35, 17, ConsoleColor.Green);
                string name = Console.ReadLine();

                ConsoleAction.PrintOnPos("Choose tank color: ", 35, 19, ConsoleColor.Green);
                //TO DO : Check for same name

                ConsoleColor color = LoadColorsMenu();

                PlayerProfile player = new PlayerProfile(name, new Dictionary<byte, int>(), new PlayerTank(color), profileNumber);

                PlayerProfile.WriteToFile(player);

                LoadMainMenu(player);
            }
            else if (command == "Load Profile")
            {
                Console.Clear();
                PrintLogo();
                List<MenuItem> profileItems = PlayerProfile.GetProfilesAsMenu();

                string selectedCommand = LoadCommand(profileItems);
                sbyte position = 0;

                foreach (var item in profileItems)
                {
                    if (selectedCommand == item.Name)
                    {
                        LoadMainMenu(PlayerProfile.LoadProfile(position));
                    }
                    position++;
                }

                
            }

            return new PlayerProfile("guest", new Dictionary<byte,int>(), new PlayerTank(), 9);
        }

        //TO DO: select level, credits, hightscores
        public static void LoadMainMenu(PlayerProfile currProfile) 
        {
            Console.Clear();
            PrintLogo();

            List<MenuItem> menuItems = new List<MenuItem>()
            {
                new MenuItem(35, 20, "New Game", true),
                new MenuItem(35, 25, "Selec Level", false),
                new MenuItem(35, 30, "Hight scores",false),
                new MenuItem(35, 35, "Credits",false),
                new MenuItem(35, 40, "Exit game",false)
            };

            string command = LoadCommand(menuItems);

            switch (command)
            {
                case "New Game":
                    Console.Clear();
                    Engine.StartGame(1, currProfile.PersonalTank); break;
                case "Select Level": break;
                case "Hight scores": break;
                case "Credits": break;
                case "Exit": Exit(); break;
                default:
                    break;
            }
        }

        //TO DO
        public static void LoadPauseMenu() 
        {

        }

        public static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                using (StreamReader reader = new StreamReader(@"..\..\MenuItems\GameLogo.txt"))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                Console.SetCursorPosition(20, 10);
                Console.WriteLine("Tank Battle");
            }
        }

        private static string LoadCommand(List<MenuItem> menuItems)
        {
            ConsoleAction.Clear(20, 20, 50, 25);
            //PrintLogo();
            PrintMenu(menuItems);

            int selectedItem = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        if (selectedItem > 0)
                        {
                            menuItems[selectedItem - 1].Selected = true;
                            menuItems[selectedItem].Selected = false;
                            selectedItem--;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        if (selectedItem < menuItems.Count - 1)
                        {
                            menuItems[selectedItem + 1].Selected = true;
                            menuItems[selectedItem].Selected = false;
                            selectedItem++;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        return menuItems[selectedItem].Name;
                    }
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(45, 19);
                    ConsoleAction.Clear(20, 20, 50, 25);
                    PrintMenu(menuItems);
                }
            }
        }

        private static void PrintMenu(List<MenuItem> items)
        {
            foreach (var item in items)
            {
                if (item.Selected) Console.ForegroundColor = ConsoleColor.Green;
                else Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition(item.X, item.Y);
                Console.WriteLine(item.Name);
            }
        }

        private static ConsoleColor LoadColorsMenu()
        {
            List<MenuItem> items = new List<MenuItem>()
            {
                new MenuItem(35, 22, "Dark Gray(default)", true),
                new MenuItem(35, 24, "Green", false),
                new MenuItem(35, 26, "Blue", false),
                new MenuItem(35, 28, "White", false),
                new MenuItem(35, 30, "Yellow", false),
                new MenuItem(35, 32, "Magenta", false),
                new MenuItem(35, 34, "Cyan", false)
            };

            string command = LoadCommand(items);

            switch (command)
            {
                case "Dark Gray(default)": return ConsoleColor.DarkGray;
                case "Green": return ConsoleColor.Green;
                case "Blue": return ConsoleColor.Blue;
                case "White": return ConsoleColor.White;
                case "Yellow": return ConsoleColor.Yellow;
                case "Magenta": return ConsoleColor.Magenta;
                case "Cyan": return ConsoleColor.Cyan;
                default: throw new ArgumentException("Not a collor from list!");
            }

        }

        private static void Exit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("Developed by Team Mark Twain");
            Console.SetCursorPosition(20, 10);
            Console.WriteLine("Telerik Academy 2013/2014");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }
}
