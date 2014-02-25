using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TankBattle.Tanks;

namespace TankBattle.MenuItems
{
    public class PlayerProfile
    {
        private string name;
        private Dictionary<byte, int> bestScores;  // Lvl - best score
        private PlayerTank personalTank;
        private sbyte profileNumber;
        private ulong currentScore;

        public PlayerProfile(string name, Dictionary<byte, int> bestScores, PlayerTank personalTank, sbyte profileNumber)
        {
            this.name = name;
            this.bestScores = bestScores;
            this.personalTank = personalTank;
            this.profileNumber = profileNumber;
            this.currentScore = 0;
        }

        public string Name { get { return this.name; } }

        public sbyte ProfileNumber{ get { return this.profileNumber; } }

        public PlayerTank PersonalTank { get { return this.personalTank; } }

        public Dictionary<byte, int> BestScores { get { return this.bestScores; } }

        public ulong Score { get { return this.Score; } }

        public int GetLevelScore(byte levelNumber)
        {
            return this.bestScores[(byte)(levelNumber)];
        }

        

    }
}
