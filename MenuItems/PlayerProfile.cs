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
        private Dictionary<byte, ulong> bestScores;  // Lvl - best score
        private PlayerTank personalTank;
        private sbyte profileNumber;
        private ulong currentScore;

        public PlayerProfile(string name, Dictionary<byte, ulong> bestScores, PlayerTank personalTank, sbyte profileNumber)
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

        public Dictionary<byte, ulong> BestScores { get { return this.bestScores; } }

        public ulong CurrentScore 
        { 
            get { return this.currentScore; }
        }

        public void AddScore(ulong scoreToAdd)
        {
            this.currentScore += scoreToAdd;
        }

        public ulong GetLevelScore(byte levelNumber)
        {
            return this.bestScores[(byte)(levelNumber)];
        }

        public void SetScore(ulong score, byte level)
        {
            if (this.bestScores.ContainsKey(level))
            {

                if (this.GetLevelScore(level) < score)
                {
                    this.bestScores[level] = score;
                }
            }
            else
            {
                this.bestScores.Add(level, score);
            }
        }

    }
}
