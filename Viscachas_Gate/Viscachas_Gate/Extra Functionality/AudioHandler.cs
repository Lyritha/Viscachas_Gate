using System;
using System.Threading;
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
            //import all the music and stops the music to make sure it doesn't all suddenly play
            mainMenuMusic = CreateAudioChannel("Audio/Music/scott-buckley-permafrost.mp3", true);
            openWorldMusic = CreateAudioChannel("Audio/Music/scott-buckley-phaseshift.mp3", true);
            dungeonMusic = CreateAudioChannel("Audio/Music/Ghostrifter-Official-Resurgence.mp3", true);
            bossMusic = CreateAudioChannel("Audio/Music/Damiano-Baldoni-Charlotte.mp3", true);
        }


        /// <summary>
        /// plays the main menu music, blends from and to other songs if needed
        /// </summary>
        public async void PlayMainMenuMusic() => await Task.WhenAll(FadeInAudioAsync(mainMenuMusic), FadeOutAudioAsync(openWorldMusic), FadeOutAudioAsync(dungeonMusic), FadeOutAudioAsync(bossMusic));
        /// <summary>
        /// plays the open world music, blends from and to other songs if needed
        /// </summary>
        public async void PlayOpenWorldMusic() => await Task.WhenAll(FadeInAudioAsync(openWorldMusic), FadeOutAudioAsync(mainMenuMusic), FadeOutAudioAsync(dungeonMusic), FadeOutAudioAsync(bossMusic));
        /// <summary>
        /// plays the dungeon music, blends from and to other songs if needed
        /// </summary>
        public async void PlayDungeonMusic() => await Task.WhenAll(FadeInAudioAsync(dungeonMusic), FadeOutAudioAsync(mainMenuMusic), FadeOutAudioAsync(openWorldMusic), FadeOutAudioAsync(bossMusic));
        /// <summary>
        /// plays the boss music, blends from and to other songs if needed
        /// </summary>
        public async void PlayBossMusic() => await Task.WhenAll(FadeInAudioAsync(bossMusic), FadeOutAudioAsync(mainMenuMusic), FadeOutAudioAsync(openWorldMusic), FadeOutAudioAsync(dungeonMusic));
        

        
        /// <summary>
        /// handles increasing the volume of a media player smoothly
        /// </summary>
        /// <param name="pMedia"></param>
        /// <returns></returns>
        async Task FadeInAudioAsync(WindowsMediaPlayer pMedia)
        {
            pMedia.controls.play();
            pMedia.settings.volume = 0;

            await Task.Run(() =>
            {
                pMedia.controls.play();
                pMedia.settings.volume = 0;

                bool free = false;
                do
                {
                    try
                    {
                        for (int step = 0; step < 100; step += 2)
                        {
                            pMedia.settings.volume = step;
                            Thread.Sleep(1);
                        }

                        free = true;
                    }
                    catch
                    {
                        Thread.Sleep(10);
                    }
                } while (!free);

                pMedia.settings.volume = 100;
            });

            pMedia.settings.volume = 100;
        }
        /// <summary>
        /// handles decreasing the volume of a media player smoothly
        /// </summary>
        /// <param name="pMedia"></param>
        /// <returns></returns>
        async Task FadeOutAudioAsync(WindowsMediaPlayer pMedia)
        {
            if (pMedia.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                await Task.Run(() =>
                {
                    bool free = false;
                    do
                    {
                        try
                        {
                            for (int step = 100; step > 0; step -= 2)
                            {
                                pMedia.settings.volume = step;
                                Thread.Sleep(1);
                            }

                            free = true;
                        }
                        catch
                        {
                            Thread.Sleep(10);
                        }
                    } while (!free);


                });

                pMedia.controls.stop();
            }
        }



        WindowsMediaPlayer CreateAudioChannel(string filePath, bool isLooping)
        {
            WindowsMediaPlayer audioChannel = new WindowsMediaPlayer();
            audioChannel.URL = filePath;
            audioChannel.settings.setMode("loop", isLooping);
            audioChannel.controls.stop();
            return audioChannel;
        }
    }
}
