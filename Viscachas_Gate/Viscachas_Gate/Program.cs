using System.Media;

namespace Viscachas_Gate
{
    internal class Program
    {


        static void Main(string[] args)
        {
            StoryLibrary storyLibrary = new StoryLibrary();
            WritingStyles writingStyles = new WritingStyles();
            ViscachaGate viscachaGate = new ViscachaGate();
            Menus menu = new Menus(storyLibrary);

            Console.ForegroundColor = ConsoleColor.White;

            //start playing menu music
            new SoundPlayer($"Audio/Music/scott-buckley-permafrost.wav").PlayLooping();

            switch (menu.MainMenu())
            {
                //continue game
                case 0:
                    Console.WriteLine("This feature does not work sadly.");
                    break;

                //new game
                case 1:
                    //storyLibrary.Intro();
                    viscachaGate.Game(storyLibrary, menu);
                    break;

                //credits
                case 2:

                    writingStyles.WriteLineCharactersSlowly("Credits: ", 0);
                    Console.WriteLine();
                    writingStyles.WriteLineCharactersSlowly("Developed by: Collin Kloppenburg", 0);
                    writingStyles.WriteLineCharactersSlowly("Helped implementing audio: Jeroen Verboom", 0);

                    Console.WriteLine();

                    writingStyles.WriteLineCharactersSlowly("Used Audio: ", 0);
                    Console.WriteLine();
                    writingStyles.WriteLineCharactersSlowly("Gate_Open: Pixabay (modified by: Collin Kloppenburg)", 0);
                    Console.WriteLine();
                    writingStyles.WriteLineCharactersSlowly("Permafrost by Scott Buckley | www.scottbuckley.com.au\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n", 0);
                    Console.WriteLine();
                    writingStyles.WriteLineCharactersSlowly("Phase Shift by Scott Buckley | www.scottbuckley.com.au\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n", 0);
                    Console.WriteLine();
                    writingStyles.WriteLineCharactersSlowly("Resurgence by Ghostrifter Official | https://soundcloud.com/ghostrifter-official\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY-SA 3.0\r\nhttps://creativecommons.org/licenses/by-sa/3.0/\r\n ", 0);
                    Console.WriteLine();
                    writingStyles.WriteLineCharactersSlowly("Charlotte by Damiano Baldoni | https://soundcloud.com/damiano_baldoni\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n ", 0);

                    Console.ReadKey(true);
                    break;
            }
        }
    }
}