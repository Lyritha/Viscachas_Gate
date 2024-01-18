using System.Media;
using WMPLib;

namespace Viscachas_Gate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creates a new object
            SaveData saveData = new SaveData();

            StoryLibrary storyLibrary = new();
            PrintBehaviors printBehaviors = new();
            Menus menu = new(storyLibrary);
            AudioHandler audioHandler = new();
            MainGame mainGame;

            //sets the console text color to white
            Console.ForegroundColor = ConsoleColor.White;

            //write text to screen, and call method inside audio handler
            Console.WriteLine("Initializing AudioHandler...");
            audioHandler.PlayMainMenuMusic();

            //asks player to fullscreen application
            Console.WriteLine("Please fullscreen the application (f11) to continue, this is reccomended for the best experience.\nThis application might break on some devices, especially windows 10 or older.");
            Console.WriteLine("Press any key to continue");
            //waits for a key input, true will make it not write to screen
            Console.ReadKey(true);
            //clears the console
            Console.Clear();

            //creates a new bool with the value of false
            bool selected = false;
            //loops until selected is true
            while (!selected)
            {
                //calls method MainMenu of Menus class, and uses it's output to decide what gets loaded
                switch (menu.MainMenu())
                {
                    //continue game
                    case 0:
                        //tries an action, if any error occurs that would normally crash the game it will execute the code in catch
                        try
                        {
                            mainGame = saveData.LoadMainGame("mainGame");
                            mainGame.LoadGame(saveData, audioHandler);
                            //sets selected to true
                            selected = true;
                        }
                        catch
                        {
                            //uses a custom method to write to the screen
                            printBehaviors.WriteLineCharactersSlowly("You do not have a save file.", 1);
                            //clears any key presses to make sure readykey doesn't get triggered from keypresses during last line
                            printBehaviors.ClearBuffer();
                            Console.ReadKey(true);
                        }
                        break;

                    //new game
                    case 1:
                        //writes the main story of the game to the screen
                        storyLibrary.Intro();
                        //creates a new mainGame with given parameters
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