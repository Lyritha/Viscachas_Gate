using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Media;
using System.Threading.Tasks.Dataflow;

namespace Viscachas_Gate
{
    internal class Menus
    {
        //saves all the different ascii art used for the main menu
        List<string[]> asciiMainMenu = new List<string[]>();

        //allows for fancy writing to the screen
        WritingStyles writingStyles = new WritingStyles();
        StoryLibrary storyLibrary = null;

        //stat distribution menu related variables
        readonly char[] statMulitplierType = { ' ', 'x', '%', 'x', 'x' };
        readonly string[] statsText = { "Health: ", "Strength: ", "Critical chance: ", "Critical multiplier: ", "Speed: " };
        float[] statMultipliers;

        public Menus(StoryLibrary pStoryLibrary)
        {
            storyLibrary = pStoryLibrary;

            //saves the main menu art
            asciiMainMenu.Add(AsciiArt("logo"));
            asciiMainMenu.Add(AsciiArt("continue_game"));
            asciiMainMenu.Add(AsciiArt("start_game"));
            asciiMainMenu.Add(AsciiArt("credits"));
        }

        /// <summary>
        /// this method runs the main menu selection screen
        /// </summary>
        public int MainMenu()
        {

            int uiPosition = 0;
            bool uiChosen = false;

            //say stuff to player to fullscreen application
            Console.WriteLine("Please fullscreen the application (f11) to continue, otherwise application might crash");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();

            //ui update loop with player input, for now selection doesn't actually matter, but planning for save files to be implemented in the future
            while (!uiChosen)
            {
                //shows the main logo
                Console.Write("\n\n\n\n\n");
                PrintTextCentered(asciiMainMenu[0]);
                Console.Write("\n\n\n\n\n\n\n\n");

                //changes how the ui is displayed depending on selected element
                switch (uiPosition)
                {
                    case 0:
                        PrintTextCentered(asciiMainMenu[1], true);
                        PrintTextCentered(asciiMainMenu[2]);
                        PrintTextCentered(asciiMainMenu[3]);
                        break;

                    case 1:
                        PrintTextCentered(asciiMainMenu[1]);
                        PrintTextCentered(asciiMainMenu[2], true);
                        PrintTextCentered(asciiMainMenu[3]);
                        break;

                    case 2:
                        PrintTextCentered(asciiMainMenu[1]);
                        PrintTextCentered(asciiMainMenu[2]);
                        PrintTextCentered(asciiMainMenu[3], true);
                        break;
                }

                //allow user input to control ui
                switch (Console.ReadKey(true).Key.ToString().ToLower())
                {
                    case "w":
                    case "uparrow":
                        if (uiPosition > 0) { uiPosition--; }
                        break;

                    case "s":
                    case "downarrow":
                        if (uiPosition < 2) { uiPosition++; }
                        break;

                    default:
                        uiChosen = true;
                        break;
                }

                Console.Clear();
            }
            return uiPosition;
        }
        /// <summary>
        /// the menu shown when you pause the game with some helpful information
        /// </summary>
        public void PauseMenu(Player pPlayer)
        {
            Console.Clear();

            Console.WriteLine("Possible commands:");

            Console.WriteLine("'exp' to view experience point progress.");
            Console.WriteLine("'inv' to open your inventory.");
            Console.WriteLine("'heal' to heal yourself with a potion.");
            Console.WriteLine("'compass' to get your own coordinates and the boss rooms coordinates.");

            Console.WriteLine();

            //purchaseable items
            if (pPlayer.GetInventory().ContainsByName("Map"))
            {
                Console.WriteLine("'map' to see the map, only works if you have the map.");
            }

            Console.WriteLine("\nHit the escape key to close this menu,\nor press 'e' to close the game (WARNING: this will wipe your progress.)");
            string userInput = "";
            do
            {
                userInput = Console.ReadKey(true).Key.ToString().ToLower();

                if (userInput == "e")
                {
                    Console.WriteLine("Are you sure that you wish to close the application? Yes/No");
                    if (storyLibrary.AskPlayer()) { Environment.Exit(0); }
                    else { writingStyles.OverwriteLines(1); }
                }
            } while (userInput != "escape" && userInput != "~");
        }



        /// <summary>
        /// creates the map and displays it to the screen
        /// </summary>
        /// <param name="pDungeon"></param>
        /// <param name="pPlayerPosition"></param>
        public void MapMenu(DungeonGenerator pDungeon, int[] pPlayerPosition)
        {


            TemplateRoom[,] dungeonRooms = pDungeon.GetDungeon();
            char[,] mapGrid = new char[dungeonRooms.GetLength(0) * 3, dungeonRooms.GetLength(1) * 3];

            int adjustVertical = 0;
            int adjustHorizontal = 0;

            //generates the map itself
            for (int vertical = 0; vertical < dungeonRooms.GetLength(0); vertical++)
            {
                for (int horizontal = 0; horizontal < dungeonRooms.GetLength(1); horizontal++)
                {
                    if (dungeonRooms[vertical, horizontal] != null)
                    {
                        bool[] hallways = dungeonRooms[vertical, horizontal].GetHallways();

                        //first line
                        mapGrid[0 + adjustVertical, 0 + adjustHorizontal] = ' ';
                        mapGrid[0 + adjustVertical, 1 + adjustHorizontal] = (hallways[0]) ? '║' : ' ';
                        mapGrid[0 + adjustVertical, 2 + adjustHorizontal] = ' ';

                        //second line
                        mapGrid[1 + adjustVertical, 0 + adjustHorizontal] = (hallways[3]) ? '═' : ' ';
                        mapGrid[1 + adjustVertical, 1 + adjustHorizontal] = '■';
                        mapGrid[1 + adjustVertical, 2 + adjustHorizontal] = (hallways[1]) ? '═' : ' ';

                        //third line
                        mapGrid[2 + adjustVertical, 0 + adjustHorizontal] = ' ';
                        mapGrid[2 + adjustVertical, 1 + adjustHorizontal] = (hallways[2]) ? '║' : ' ';
                        mapGrid[2 + adjustVertical, 2 + adjustHorizontal] = ' ';
                    }
                    else
                    {
                        //first line
                        mapGrid[0 + adjustVertical, 0 + adjustHorizontal] = ' ';
                        mapGrid[0 + adjustVertical, 1 + adjustHorizontal] = ' ';
                        mapGrid[0 + adjustVertical, 2 + adjustHorizontal] = ' ';

                        //second line
                        mapGrid[1 + adjustVertical, 0 + adjustHorizontal] = ' ';
                        mapGrid[1 + adjustVertical, 1 + adjustHorizontal] = ' ';
                        mapGrid[1 + adjustVertical, 2 + adjustHorizontal] = ' ';

                        //third line
                        mapGrid[2 + adjustVertical, 0 + adjustHorizontal] = ' ';
                        mapGrid[2 + adjustVertical, 1 + adjustHorizontal] = ' ';
                        mapGrid[2 + adjustVertical, 2 + adjustHorizontal] = ' ';
                    }

                    adjustHorizontal += 3;
                }

                adjustVertical += 3;
                adjustHorizontal = 0;
            }

            //saves adjusted coords
            int[] adjustedPlayerPosition = { (pPlayerPosition[0] * 3) + 1, (pPlayerPosition[1] * 3) + 1 };
            Console.Clear();

            //prints the map to the screen
            for (int vertical = 0; vertical < mapGrid.GetLength(0); vertical++)
            {
                for (int horizontal = 0; horizontal < mapGrid.GetLength(1); horizontal++)
                {

                    //player
                    if (vertical == adjustedPlayerPosition[0] && horizontal == adjustedPlayerPosition[1])
                    { Console.ForegroundColor = ConsoleColor.Green; }

                    //if it alligns to grid (the rooms)
                    else if ((vertical - 1) % 3 == 0 && (horizontal - 1) % 3 == 0)
                    {
                        //if boss room
                        if (dungeonRooms[(vertical - 1) / 3, (horizontal - 1) / 3] is BossRoom)
                        { Console.ForegroundColor = ConsoleColor.Red; }

                        //if store
                        else if (dungeonRooms[(vertical - 1) / 3, (horizontal - 1) / 3] is StoreRoom)
                        { Console.ForegroundColor = ConsoleColor.DarkYellow; }

                        //if starting room
                        else if (dungeonRooms[(vertical - 1) / 3, (horizontal - 1) / 3] is EntranceRoom)
                        { Console.ForegroundColor = ConsoleColor.Blue; }

                        //if normal rooms
                        else
                        { Console.ForegroundColor = ConsoleColor.White; }
                    }

                    //hallways/empty space
                    else
                    { Console.ForegroundColor = ConsoleColor.White; }

                    Console.Write($"{mapGrid[vertical, horizontal]} ");
                }
                Console.WriteLine();
            }

            //prints legend
            Console.WriteLine();

            Console.Write("Player: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("■");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Boss Room: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Entrance: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("■");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Store: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("■");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// displays the store on the screen
        /// </summary>
        /// <param name="pPlayer"></param>
        public void StoreMenu(int pDungeonLevel, Player pPlayer)
        {
            Console.Clear();

            Console.WriteLine($"Coins: {pPlayer.GetInventory().GetCoins()}");
            Console.WriteLine();

            //gets the cost of all the items
            int mapCost = 250 * pDungeonLevel;
            int potionCost = 1200 * pPlayer.GetInventory().GetMaxHealingPotionAmount();
            int armorCost = 500 + 500 * (int)pPlayer.GetArmor();

            //show items you can purchase
            Console.WriteLine($"Map: {mapCost} coins");
            Console.WriteLine($"Max Potions: {potionCost} coins");
            Console.WriteLine($"Armor +2: {armorCost} coins");
            Console.WriteLine();

            //ask player what item they wish to buy
            Console.WriteLine("Would you like to purchase an item?");
            if (storyLibrary.AskPlayer())
            {
                writingStyles.OverwriteLines(1);
                Console.WriteLine("Which item would you like to buy?\nType 'stop' to stop shopping");

                bool done = false;
                string userInput = "";
                while (!done)
                {
                    userInput = Console.ReadLine().ToLower();

                    switch (userInput)
                    {
                        //if player wants to purchase map
                        case "map":
                            //check if player has a map for this dungeon
                            if (!pPlayer.GetInventory().ContainsByID(100 + pPlayer.GetDungeonProgress()))
                            {
                                //check if player has enough money
                                if (pPlayer.GetInventory().GetCoins() >= mapCost)
                                {
                                    //removes the paid money
                                    pPlayer.GetInventory().AddCoins(-mapCost);

                                    //update inventory to reflect purchase
                                    pPlayer.GetInventory().AddItem(new Map(pPlayer.GetDungeonProgress()));

                                    Console.WriteLine($"You purchased a map!");
                                }
                                else { Console.WriteLine("You do not have enough Coins!"); }
                            } else { Console.WriteLine("You already have a map for this dungeon!"); }
                            break;

                        //if player wants to purchase a healing potion
                        case "healing potion":
                        case "potion":
                        case "healing":
                        case "max potions":
                        case "max potion":
                        case "max healing potions":
                        case "max healing potion":
                        case "max healing":
                            //check if player has enough money
                            if (pPlayer.GetInventory().GetCoins() >= potionCost)
                            {
                                //removes the paid money
                                pPlayer.GetInventory().AddCoins(-potionCost);

                                //update inventory to reflect purchase
                                pPlayer.GetInventory().AddMaxHealingPotionAmount(1);

                                Console.WriteLine("You purchased another potion slot!");
                            }
                            else { Console.WriteLine("You do not have enough Coins!"); }
                            break;

                        case "armor":
                            //check if player has enough money
                            if (pPlayer.GetInventory().GetCoins() >= armorCost)
                            {
                                //removes the paid money
                                pPlayer.GetInventory().AddCoins(-armorCost);

                                //update stats to reflect purchase
                                pPlayer.AddArmor(2  );

                                Console.WriteLine("You purchased +2 armor");
                            }
                            else { Console.WriteLine("You do not have enough Coins!"); }
                            break;

                        //if the player no longer wants to buy something
                        case "stop":
                            done = true;
                            break;

                        //if player gave an incorrect input
                        default:
                            Console.WriteLine($"{userInput} is not sold here");
                            break;
                    }
                }

            }
            else
            {
                writingStyles.OverwriteLines(2);
                Console.WriteLine("Press any key to close the store");
                Console.ReadKey(true);
            }


        }



        /// <summary>
        /// displays the character creator on the screen
        /// </summary>
        /// <param name="pPlayer"></param>
        /// <summary>
        /// holds everything that has to do with the character creator, for now it's only setting it's name
        /// </summary>
        public void CharacterCreator(Player pPlayer, StoryLibrary pStoryLibrary)
        {
            ChooseName(pPlayer, pStoryLibrary);
            pPlayer.PrintShowStats();
            StatDistributionMenu(pPlayer);
            pPlayer.MaxHeal();
        }
        void ChooseName(Player pPlayer, StoryLibrary pStoryLibrary)
        {
            //stores if the user wants this username or not
            bool nameChose = false;
            //loops until user likes their name
            writingStyles.WriteLineCharactersSlowly("Character Creation\n\n");
            while (!nameChose)
            {
                //clears current line and places the cursor at the beginning of said line
                writingStyles.OverwriteLines();

                writingStyles.WriteCharactersSlowly("Choose your name: ");
                pPlayer.SetName(Console.ReadLine());

                //clears current line and places the cursor at the beginning of said line
                writingStyles.OverwriteLines();

                //write information to give user choice
                writingStyles.WriteLineCharactersSlowly($"Do you want {pPlayer.GetName()} to be your name? yes/no");
                nameChose = pStoryLibrary.AskPlayer();
            }

            writingStyles.OverwriteLines(3);
        }
        public void StatDistributionMenu(Player pPlayer)
        {
            //allows you to read the changes
            int[] distributedPointsChanges = { 0, 0, 0, 0, 0 };

            //gets the needed values from the player object
            int[] distributedPoints = pPlayer.GetDistributedPoints();
            statMultipliers = pPlayer.GetStatMultiplier();

            //stores the current position in the ui
            int uiPosition = 0;

            Console.WriteLine("\nW/S to move up and down the menu, A/D to decrease/increase a stat.\n");

            //loop until the player is happy with their stats
            bool done = false;
            while (!done)
            {
                Console.WriteLine($"|| Current Points: {pPlayer.GetStatPointsAmount()} ||");
                for (int i = 0; i < distributedPoints.Length; i++)
                {
                    bool selected = i == uiPosition;

                    if (statMulitplierType[i] == ' ')
                    {
                        if (selected && distributedPoints[i] != 0)
                        { Console.WriteLine($"> {statsText[i]}{distributedPoints[i]} + {distributedPoints[i] * statMultipliers[i]:0}{statMulitplierType[i]}"); }

                        else if (!selected && distributedPoints[i] != 0)
                        { Console.WriteLine($"{statsText[i]} {distributedPoints[i]} + {distributedPoints[i] * statMultipliers[i]:0}{statMulitplierType[i]}"); }

                        else if (selected) { Console.WriteLine($"> {statsText[i]}{distributedPoints[i]}"); }

                        else { Console.WriteLine($"{statsText[i]}{distributedPoints[i]}"); };
                    }

                    else if (statMulitplierType[i] == 'x')
                    {
                        if (selected && distributedPoints[i] != 0)
                        { Console.WriteLine($"> {statsText[i]}{distributedPoints[i]} + {distributedPoints[i] * statMultipliers[i]:0.00}{statMulitplierType[i]}"); }

                        else if (!selected && distributedPoints[i] != 0)
                        { Console.WriteLine($"{statsText[i]} {distributedPoints[i]} + {distributedPoints[i] * statMultipliers[i]:0.00}{statMulitplierType[i]}"); }

                        else if (selected) { Console.WriteLine($"> {statsText[i]}{distributedPoints[i]}"); }

                        else { Console.WriteLine($"{statsText[i]}{distributedPoints[i]}"); };
                    }

                    else if (statMulitplierType[i] == '%')
                    {
                        if (selected && distributedPoints[i] != 0)
                        { Console.WriteLine($"> {statsText[i]}{distributedPoints[i]} + {distributedPoints[i] * statMultipliers[i]:0.0}{statMulitplierType[i]}"); }

                        else if (!selected && distributedPoints[i] != 0)
                        { Console.WriteLine($"{statsText[i]} {distributedPoints[i]} + {distributedPoints[i] * statMultipliers[i]:0.0}{statMulitplierType[i]}"); }

                        else if (selected) { Console.WriteLine($"> {statsText[i]}{distributedPoints[i]}"); }

                        else { Console.WriteLine($"{statsText[i]}{distributedPoints[i]}"); };
                    }


                }

                Console.WriteLine();
                Console.WriteLine("Press 'Enter' to confirm your stat distribution.");

                //allow user input to control ui
                switch (Console.ReadKey(true).Key.ToString().ToLower())
                {
                    case "w":
                    case "uparrow":
                        if (uiPosition > 0) { uiPosition--; }
                        break;

                    case "s":
                    case "downarrow":
                        if (uiPosition < distributedPoints.Length - 1) { uiPosition++; }
                        break;

                    case "d":
                    case "rightarrow":
                        if (pPlayer.GetStatPointsAmount() > 0)
                        {
                            distributedPoints[uiPosition]++;
                            distributedPointsChanges[uiPosition]++;
                            pPlayer.AddStatPointsAmount(-1);
                        }
                        break;

                    case "a":
                    case "leftarrow":
                        if (distributedPoints[uiPosition] != 0)
                        {
                            distributedPoints[uiPosition]--;
                            distributedPointsChanges[uiPosition]--;
                            pPlayer.AddStatPointsAmount(1);
                        }
                        break;

                    case "enter":
                        Console.Write("Are you sure? yes/no: ");
                        done = storyLibrary.AskPlayer();
                        break;
                }

                writingStyles.OverwriteLines(8);
            }

            //update all the stats by adding the values
            pPlayer.UpdateStats(distributedPointsChanges);
        }

        public void PlayGateAnimation()
        {
            string[] gate = File.ReadAllText($"ascii_art/Door.txt").Split('■');

            new SoundPlayer($"Audio/Door_Open.WAV").Play();

            foreach (string frame in gate)
            {
                //loop through all the lines of the ascii art
                foreach (string line in frame.Split('\n'))
                {
                    try
                    {
                        // Calculate padding to center the user input text
                        int leftPaddingText = (Console.WindowWidth - line.Length) / 2;
                        // Output the centered text
                        Console.SetCursorPosition(leftPaddingText, Console.CursorTop);
                        Console.WriteLine(line);
                    }
                    catch { Console.WriteLine(line); }

                }
                Thread.Sleep(175);
                Console.Clear();
            }
        }

        /// <summary>
        /// prints text/ascii art centered on the screen, you can also tell the function if the text is selected
        /// </summary>
        /// <param name="asciiArt"></param>
        /// <param name="selected"></param>
        void PrintTextCentered(string[] asciiArt, bool selected = false)
        {

            //loop through all the lines of the ascii art
            foreach (string line in asciiArt)
            {
                //changes the string depending if the current input is selected or not
                string modifiedLine = (selected) ? $"||  {line}  ||" : line;

                try
                {
                    // Calculate padding to center the user input text
                    int leftPaddingText = (Console.WindowWidth - modifiedLine.Length) / 2;
                    // Output the centered text
                    Console.SetCursorPosition(leftPaddingText, Console.CursorTop);
                    Console.WriteLine(modifiedLine);
                }
                catch { Console.WriteLine(line); }

            }
            //adds spacing between the ascii art
            Console.Write("\n\n\n\n\n");
        }
        /// <summary>
        /// loads ascii art from a text file into a variable
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        string[] AsciiArt(string pFileName) => File.ReadAllLines($"ascii_art/MainMenu/{pFileName}.txt");
    }
}
