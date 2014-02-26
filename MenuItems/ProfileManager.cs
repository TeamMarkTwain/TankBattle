using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TankBattle.Tanks;

namespace TankBattle.MenuItems
{
    public static class ProfileManager
    {
        private static int numberOfProfiles = 10;
        private static string profilesPath = @"..\..\MenuItems\Profiles.txt";

        //Write created profile
        public static void WriteToFile(PlayerProfile profile)
        {
            string[] profiles = new string[numberOfProfiles];

            using (StreamReader reader = new StreamReader(profilesPath))
            {

                for (int i = 0; i < numberOfProfiles; i++)
                {
                    profiles[i] = reader.ReadLine();
                }

                string scores = ScoresAsString(profile);
                profiles[profile.ProfileNumber] = string.Format("{0}.Name:{1},Color:{2},Scores:{3}",
                    profile.ProfileNumber + 1, profile.Name, profile.PersonalTank.Color, scores);
            }
            using (StreamWriter writer = new StreamWriter(profilesPath))
            {
                for (int i = 0; i < numberOfProfiles; i++)
                {
                    writer.WriteLine(profiles[i]);
                }
            }
        }

        //Loads profile from Profile.txt
        public static PlayerProfile LoadProfile(int number)
        {
            using (StreamReader reader = new StreamReader(profilesPath))
            {
                string line = "";
                for (int i = 0; i < number; i++)
                {
                    line = reader.ReadLine();
                }

                string profileLine = reader.ReadLine();
                sbyte profNum = (sbyte)number;

                string[] components = profileLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string name = components[0].Substring(components[0].IndexOf(':') + 1);
                string color = components[1].Substring(components[1].IndexOf(':') + 1);
                string scores = components[2].Substring(components[2].IndexOf(':') + 1);

                return new PlayerProfile(name, ScoresToDictionary(scores), new PlayerTank(GetColor(color)), profNum);
            }
        }

        //Get epmy sloth for creating new profile. If there is no empy, returns -1
        public static sbyte GetEmptySloth()
        {
            string line = "";
            try
            {
                using (StreamReader reader = new StreamReader(profilesPath))
                {
                    for (sbyte i = 0; i < numberOfProfiles; i++)
                    {
                        line = reader.ReadLine();
                        string subLine = line.Substring(line.IndexOf('.') + 1);
                        if (subLine == "E")
                        {
                            return i;
                        }
                    }

                    return (sbyte)-1;
                }
            }
            catch (FileNotFoundException f)
            {
                return 0;
            }
        }

        public static List<MenuItem> GetProfilesAsMenu()
        {
            List<MenuItem> profilesAsMenu = new List<MenuItem>(numberOfProfiles);

            using (StreamReader reader = new StreamReader(profilesPath))
            {
                string line = "";

                for (int i = 0; i < numberOfProfiles; i++)
                {
                    line = reader.ReadLine();
                    string subLine = line.Substring(line.IndexOf('.') + 1);

                    if (subLine == "E")
                    {
                        continue;
                    }
                    else
                    {
                        int lenght = subLine.IndexOf(',') - (subLine.IndexOf("Name:") + 5);
                        string name = subLine.Substring(subLine.IndexOf("Name:") + 5, lenght);
                        profilesAsMenu.Add(new MenuItem(40, 20 + (i * 2), name, false));
                    }
                }

                profilesAsMenu[0].Selected = true;
            }

            return profilesAsMenu;
        }

        //Converts score from PlayerProfile to string dor Profile.txt
        private static string ScoresAsString(PlayerProfile profile)
        {
            StringBuilder builder = new StringBuilder();
            if (profile.BestScores.Count == 0)
            {                   //number of levels 
                for (int i = 0; i < Level.NumberOfLevels(); i++)
                {
                    builder.AppendFormat("{0}-0;", i + 1);
                }
            }
            foreach (var item in profile.BestScores)
            {
                builder.AppendFormat("{0}-{1};", item.Key, item.Value);
            }

            return builder.ToString();
        }

        //Gets color from string
        private static ConsoleColor GetColor(string color)
        {
            switch (color)
            {
                case "DarkGray": return ConsoleColor.DarkGray;
                case "Green": return ConsoleColor.Green;
                case "Blue": return ConsoleColor.Blue;
                case "White": return ConsoleColor.White;
                case "Yellow": return ConsoleColor.Yellow;
                case "Magenta": return ConsoleColor.Magenta;
                case "Cyan": return ConsoleColor.Cyan;
                default: throw new ArgumentException("Not a collor from list!");
            }
        }

        //Converts scores from string (from Profile.txt)
        private static Dictionary<byte, ulong> ScoresToDictionary(string scoreLine)
        {
            Dictionary<byte, ulong> profileScores = new Dictionary<byte, ulong>();
            string[] levelScores = scoreLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //level count
            for (int i = 0; i < levelScores.Length; i++)
            {
                string[] keyValue = levelScores[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                byte key = byte.Parse(keyValue[0]);
                ulong value = ulong.Parse(keyValue[1]);

                profileScores.Add(key, value);
            }

            return profileScores;
        }
    }
}
