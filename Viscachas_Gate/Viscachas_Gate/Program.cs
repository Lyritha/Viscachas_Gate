using System.Media;
using WMPLib;

namespace Viscachas_Gate
{
    internal class Program
    {


        static void Main(string[] args)
        {
            StoryLibrary storyLibrary = new();
            PrintBehaviors printBehaviors = new();
            MainGame viscachaGate = new();
            Menus menu = new(storyLibrary);
            AudioHandler audioHandler = new();

            Console.ForegroundColor = ConsoleColor.White;

            //play main menu music
            Console.WriteLine("Initializing AudioHandler...");
            audioHandler.PlayMainMenuMusic();

            //say stuff to player to fullscreen application
            Console.WriteLine("Please fullscreen the application (f11) to continue, otherwise application might crash\nThis application might break on some devices, especially windows 10 or older.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();

            bool selected = false;
            while (!selected)
            {
                switch (menu.MainMenu())
                {
                    //continue game
                    case 0:
                        Console.WriteLine("This feature does not work sadly.");
                        Console.ReadKey(true);
                        break;

                    //new game
                    case 1:
                        selected = true;
                        //storyLibrary.Intro();
                        viscachaGate.Game(storyLibrary, menu, audioHandler);
                        break;

                    //credits
                    case 2:

                        printBehaviors.WriteLineCharactersSlowly("Credits: ", 0);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Developed by: Collin Kloppenburg", 0);
                        printBehaviors.WriteLineCharactersSlowly("Helped implementing audio: Jeroen Verboom", 0);

                        Console.WriteLine();

                        printBehaviors.WriteLineCharactersSlowly("Used Audio: ", 0);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Gate_Open: Pixabay (modified by: Collin Kloppenburg)", 0);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Permafrost by Scott Buckley | www.scottbuckley.com.au\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n", 0);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Phase Shift by Scott Buckley | www.scottbuckley.com.au\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n", 0);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Resurgence by Ghostrifter Official | https://soundcloud.com/ghostrifter-official\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY-SA 3.0\r\nhttps://creativecommons.org/licenses/by-sa/3.0/\r\n ", 0);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Charlotte by Damiano Baldoni | https://soundcloud.com/damiano_baldoni\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n ", 0);

                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}