using System.Media;
using WMPLib;

namespace Viscachas_Gate
{
    internal class Program
    {


        static void Main(string[] args)
        {
            SaveData saveData = new SaveData();

            StoryLibrary storyLibrary = new();
            PrintBehaviors printBehaviors = new();
            Menus menu = new(storyLibrary);
            AudioHandler audioHandler = new();
            MainGame mainGame;

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
                        try
                        {
                            mainGame = saveData.LoadMainGame("mainGame");
                            mainGame.LoadGame(saveData, audioHandler);
                            selected = true;
                        }
                        catch
                        {
                            printBehaviors.WriteLineCharactersSlowly("You do not have a save file.", 1);
                            printBehaviors.ClearBuffer();
                            Console.ReadKey(true);
                        }
                        break;

                    //new game
                    case 1:
                        //storyLibrary.Intro();
                        mainGame = new(storyLibrary, menu);
                        mainGame.NewGame(saveData, audioHandler);
                        selected = true;
                        break;

                    //credits
                    case 2:

                        printBehaviors.WriteLineCharactersSlowly("Credits: ", 1);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Developed by: Collin Kloppenburg", 1);
                        printBehaviors.WriteLineCharactersSlowly("Gave me the idea of implementing audio: Jeroen Verboom", 1);
                        printBehaviors.WriteLineCharactersSlowly("Explained to me how async Tasks work and how to implement them: ChatGPT", 1);

                        Console.WriteLine();

                        printBehaviors.WriteLineCharactersSlowly("Used Audio: ", 1);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Gate_Open: Pixabay (modified by: Collin Kloppenburg)", 1);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Permafrost by Scott Buckley | www.scottbuckley.com.au\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n", 1);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Phase Shift by Scott Buckley | www.scottbuckley.com.au\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n", 1);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Resurgence by Ghostrifter Official | https://soundcloud.com/ghostrifter-official\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY-SA 3.0\r\nhttps://creativecommons.org/licenses/by-sa/3.0/\r\n ", 1);
                        Console.WriteLine();
                        printBehaviors.WriteLineCharactersSlowly("Charlotte by Damiano Baldoni | https://soundcloud.com/damiano_baldoni\r\nMusic promoted by https://www.chosic.com/free-music/all/\r\nCreative Commons CC BY 4.0\r\nhttps://creativecommons.org/licenses/by/4.0/\r\n ", 1);

                        printBehaviors.ClearBuffer();
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}