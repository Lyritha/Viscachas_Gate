using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Viscachas_Gate
{
    internal class AudioHandler
    {
        WindowsMediaPlayer mainMenuMusic = null;
        WindowsMediaPlayer openWorldMusic = null;
        WindowsMediaPlayer dungeonMusic = null;
        WindowsMediaPlayer bossMusic = null;

        public AudioHandler()
        {
            mainMenuMusic = CreateAudioChannel("Audio/Music/scott-buckley-permafrost.mp3", true);
            openWorldMusic = CreateAudioChannel("Audio/Music/scott-buckley-phaseshift.mp3", true);
            dungeonMusic = CreateAudioChannel("Audio/Music/Ghostrifter-Official-Resurgence.mp3", true);
            bossMusic = CreateAudioChannel("Audio/Music/Damiano-Baldoni-Charlotte.mp3", true);

            mainMenuMusic.controls.stop();
            openWorldMusic.controls.stop();
            dungeonMusic.controls.stop();
            bossMusic.controls.stop();
        }


        /// <summary>
        /// handles playing the music for the main menu
        /// </summary>
        public void PlayMainMenuMusic()
        {
            //plays the menu music
            mainMenuMusic.controls.play();
            //required delay for when starting the game
            Thread.Sleep(500);
            FadeInAudio(mainMenuMusic);

            //stop all other forms of music
            FadeOutAudio(openWorldMusic);
            FadeOutAudio(dungeonMusic);
            FadeOutAudio(bossMusic);
        }

        public void PlayOpenWorldMusic()
        {
            //plays the open world music
            openWorldMusic.controls.play();
            Thread.Sleep(500);
            FadeInAudio(openWorldMusic);

            //stop all other forms of music
            FadeOutAudio(mainMenuMusic);
            FadeOutAudio(dungeonMusic);
            FadeOutAudio(bossMusic);
        }

        public void PlayDungeonMusic()
        {
            //plays the dungeon music
            dungeonMusic.controls.play();
            Thread.Sleep(500);
            FadeInAudio(dungeonMusic);

            //stop all other forms of music
            FadeOutAudio(mainMenuMusic);
            FadeOutAudio(openWorldMusic);
            FadeOutAudio(bossMusic);
        }

        public void PlayBossMusic()
        {
            //plays the boss music
            bossMusic.controls.play();
            Thread.Sleep(500);
            FadeInAudio(bossMusic);

            //stop all other forms of music
            FadeOutAudio(mainMenuMusic);
            FadeOutAudio(openWorldMusic);
            FadeOutAudio(dungeonMusic);

        }

         void FadeInAudio(WindowsMediaPlayer pMedia)
        {
            pMedia.settings.volume = 0;

            for (int step = 0; step < 100; step += 2)
            {
                pMedia.settings.volume = step;
                Thread.Sleep(1);
            }

            pMedia.settings.volume = 100;
        }

         void FadeOutAudio(WindowsMediaPlayer pMedia)
        {
            if (pMedia.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                for (int step = 100; step > 0; step -= 2)
                {
                    pMedia.settings.volume = step;
                    Thread.Sleep(1);
                }

                pMedia.controls.stop();
            }
        }

        WindowsMediaPlayer CreateAudioChannel(string filePath, bool isLooping)
        {
            WindowsMediaPlayer audioChannel = new WindowsMediaPlayer();
            audioChannel.URL = filePath;
            audioChannel.settings.setMode("loop", isLooping);
            return audioChannel;
        }
    }
}
