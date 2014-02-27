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
                sbyte profileNumber = ProfileManager.GetEmptySloth();

                if (profileNumber < 0)
                {
                    ConsoleAction.PrintOnPos("No empty sloth! Choose a profile to replace it!", 30, 18, ConsoleColor.Red);
                    List<MenuItem> profileItems = ProfileManager.GetProfilesAsMenu();

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

                while (name.Length > 10 || name.Length < 3)
                {
                    ConsoleAction.Clear(35, 17, 30, 2);
                    ConsoleAction.PrintOnPos("Name must has 3-10 symbols: ", 35, 17, ConsoleColor.Red);
                    ConsoleAction.PrintOnPos("Profile name: ", 35, 18, ConsoleColor.Green);
                    name = Console.ReadLine();
                }
                

                ConsoleAction.PrintOnPos("Choose tank color: ", 35, 19, ConsoleColor.Green);
                //TO DO : Check for same name

                ConsoleColor color = LoadColorsMenu();

                PlayerProfile player = new PlayerProfile(name, new Dictionary<byte, ulong>(), new PlayerTank(color), profileNumber);

                ProfileManager.WriteToFile(player);

                LoadMainMenu(player);
            }
            else if (command == "Load Profile")
            {
                Console.Clear();
                PrintLogo();
                List<MenuItem> profileItems = ProfileManager.GetProfilesAsMenu();

                string selectedCommand = LoadCommand(profileItems);
                if (selectedCommand == "return")
                {
                    LoadProfileMenu();
                }
                sbyte position = 0;

                foreach (var item in profileItems)
                {
                    if (selectedCommand == item.Name)
                    {
                        LoadMainMenu(ProfileManager.LoadProfile(position));
                    }
                    position++;
                }

                
            }

            return new PlayerProfile("guest", new Dictionary<byte, ulong>(), new PlayerTank(), 9);
        }

        public static void LoadMainMenu(PlayerProfile currProfile) 
        {
            Console.Clear();
            PrintLogo();

            List<MenuItem> menuItems = new List<MenuItem>()
            {
                new MenuItem(35, 20, "New Game", true),
                new MenuItem(35, 25, "Select Level", false),
                new MenuItem(35, 30, "Hight scores",false),
                new MenuItem(35, 35, "Credits",false),
                new MenuItem(35, 40, "Exit game",false)
            };

            string command = LoadCommand(menuItems);

            switch (command)
            {
                case "New Game":
                    Console.Clear();
                    Engine.StartGame(1, currProfile); break;
                case "Select Level":
                    ConsoleAction.Clear(30, 20, 20, 25);
                    LoadLevelsMenu(currProfile);
                    break;
                case "Hight scores": 
                    ConsoleAction.Clear(30, 20, 20, 25);
                    VisualiseHightScores(currProfile);
                    while (true)
                    {
                        if (Console.KeyAvailable) LoadMainMenu(currProfile);
                    }
                case "Credits":
                    ConsoleAction.Clear(30, 20, 20, 25);
                    ConsoleAction.PrintOnPos("Comming soon", 35, 20 , ConsoleColor.Red);
                    ConsoleAction.PrintOnPos("Enter any key to return", 35, 22, ConsoleColor.Red);
                    while (true)
                    {
                        if (Console.KeyAvailable) LoadMainMenu(currProfile);
                    }
                case "Exit game": Exit(); break;
                case "return": LoadProfileMenu(); break;
                default:
                    break;
            }
        }

        

        //TO DO
        public static void LoadPauseMenu(PlayerProfile currProfile) 
        {
            List<MenuItem> pausedMenuItems = new List<MenuItem>()
            {
                new MenuItem(10, 43,"Resume game", true ),
                new MenuItem(25, 43, "Exit game", false)
            };

            PrintMenu(pausedMenuItems);

            int selectedItem = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        if (selectedItem > 0)
                        {
                            pausedMenuItems[selectedItem - 1].Selected = true;
                            pausedMenuItems[selectedItem].Selected = false;
                            selectedItem--;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (selectedItem < pausedMenuItems.Count - 1)
                        {
                            pausedMenuItems[selectedItem + 1].Selected = true;
                            pausedMenuItems[selectedItem].Selected = false;
                            selectedItem++;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        if (pausedMenuItems[0].Selected)
                        {
                            ConsoleAction.Clear(10, 43, 30,  1);
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            Engine.SetDefaults(currProfile);
                            LoadMainMenu(currProfile);
                        }
                    }
                    PrintMenu(pausedMenuItems);
                }
            }
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

        private static void VisualiseHightScores(PlayerProfile currProfile)
        {
            string line = "";
            for (byte i = 0; i < Level.NumberOfLevels(); i++)
            {
                if (i < currProfile.BestScores.Count)
	            {
		            line = string.Format("Level {0} : {1} points", i+1, currProfile.GetLevelScore((byte)(i+1)));
	            }
                else
	            {
                    line = string.Format("Level {0} : Not played", i+1);
	            }
                ConsoleAction.PrintOnPos(line, 35, 20+(i*2), ConsoleColor.Green);
            }
            ConsoleAction.PrintOnPos("Enter any key to return", 35, 20 + (Level.NumberOfLevels() * 2), ConsoleColor.Red);
        }

        private static void LoadLevelsMenu(PlayerProfile profile) 
        {
            List<MenuItem> levelMenuItems = new List<MenuItem>();

            for (int i = 0; i < Level.NumberOfLevels(); i++)
            {
                levelMenuItems.Add(new MenuItem(35, 20 + (i * 2), string.Format("Level {0}", i + 1), false));   
            }

            levelMenuItems[0].Selected = true;

            string selectedLevelCommand = LoadCommand(levelMenuItems);

            int selectedLevel = 1;

            foreach (var item in levelMenuItems)
            {
                if (item.Name == selectedLevelCommand)
                {
                    Console.Clear();
                    Engine.StartGame(selectedLevel, profile);
                }
                selectedLevel++;
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
                    else if (pressedKey.Key == ConsoleKey.Escape)
                    {
                        return "return";
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
            Console.SetCursorPosition(38, 8);
            Console.WriteLine("Developed by Team Mark Twain");
            Console.SetCursorPosition(38, 10);
            Console.WriteLine("Telerik Academy 2013/2014");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }
}
