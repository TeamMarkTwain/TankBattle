using System;
using System.Media;

namespace TankBattle
{
    //IMPLEMENT
    public static class SoundEngine
    {
        private static SoundPlayer musicPlayer;
        private static SoundPlayer inGamePlayer;
        public static void MoveSound() 
        {
            inGamePlayer = new SoundPlayer("../../Sounds/background.wav");
            inGamePlayer.Play();
        }

        public static void EndGameSound() 
        {
            musicPlayer = new SoundPlayer("../../Sounds/gameover.wav");
            musicPlayer.Play();
        }

        public static void StartGameSound() 
        {
            musicPlayer = new SoundPlayer("../../Sounds/gamestart.wav");
            musicPlayer.Play();
        }

        public static void HitSound() 
        {
            inGamePlayer = new SoundPlayer("../../Sounds/explosion.wav");
            inGamePlayer.Play();
        }

        public static void FireSound() 
        {
            inGamePlayer = new SoundPlayer("../../Sounds/fire.wav");
            inGamePlayer.Play();
        }
    }
}