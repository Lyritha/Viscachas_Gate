using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Viscachas_Gate
{

    internal class StoryLibrary
    {
        WritingStyles writingStyles = new WritingStyles();

        public void Intro()
        {
            //clears the console to remove anything currently on the screen
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            writingStyles.WriteLineCharactersSlowlyClamped("In a world once teeming with an ancient civilization of wise and inquisitive Viscachas, their society thrived on intellectual prowess and insatiable curiosity. These rodent-like creatures cultivated sprawling cities blending seamlessly with nature, adorned with intricately designed structures.", 1);
            Console.WriteLine();
            writingStyles.WriteLineCharactersSlowlyClamped("Their libraries stood as repositories of boundless knowledge, chronicling discoveries, innovations, and philosophical musings, reflecting their unwavering pursuit of wisdom. Yet, the echoes of their once vibrant civilization now whispered tales of a bygone era.", 1);
            Console.WriteLine();
            writingStyles.WriteLineCharactersSlowlyClamped("The Viscacha Gate remained, a silent witness to the demise of a once-great culture, guarding a grand library that holds the culmination of their wisdom, echoing through the ages despite the civilization's eventual decline and disappearance.", 1);
            Console.WriteLine();
            writingStyles.WriteLineCharactersSlowlyClamped("Your task is to find and conquer the dungeons keeping the Viscacha's Gate hidden, they are filled with the story of the bygone erra which you must learn about and conquer. Beat the dungeons, reveal and enter the Vischacha's Gate, to aquire the unlimited knowledge within!", 1);

            writingStyles.ClearBuffer();

            Continue();
        }


        /// <summary>
        /// shows story of the dungeon you are currently progressing to
        /// </summary>
        /// <param name="pPlayer"></param>
        public bool EnterDungeon(Player pPlayer, bool pHasReadDungeonStory)
        {
            //clears the console to remove anything currently on the screen
            Console.Clear();
            Console.ResetColor();

            bool hasReadStory = pHasReadDungeonStory;

            switch (pPlayer.GetDungeonProgress())
            {
                //story for the first dungeon
                case 1:

                    writingStyles.WriteLineCharactersSlowlyClamped("Now Entering: Explorer's Maze", 10);
                    Console.WriteLine();

                    if (pHasReadDungeonStory) { break; }
                    writingStyles.WriteLineCharactersSlowly("Do you wish to hear the story? yes/no: ", 1);
                    if (AskPlayer())
                    {
                        writingStyles.WriteLineCharactersSlowlyClamped("The echoes of the Explorer's Maze recount the bold adventures of a Viscacha explorers through its winding paths, the brave explorers of the past met an untimely end within these very passages, one of them being the bravest of their kind.", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("Immortalized in their true bravery to challenge the unknown, they transformed into the guardian protecting this maze. A testament to the their courage, their spirit poses a challenging adversary for those who dare to tread the same path.", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("Legends whisper of a way to liberate the trapped soul, but you will have to find them first to truly know.", 1);
                        
                        hasReadStory = true;
                    }
                    else { writingStyles.OverwriteLines(2); }

                    break;

                //story for the second dungeon
                case 2:

                    writingStyles.WriteLineCharactersSlowlyClamped("Now Entering: Emperor's Treasure: ", 10);
                    Console.WriteLine();

                    if (pHasReadDungeonStory) { break; }
                    writingStyles.WriteLineCharactersSlowly("Do you wish to hear the story? yes/no: ", 1);
                    if (AskPlayer())
                    {
                        writingStyles.WriteLineCharactersSlowlyClamped("Deep within these chambers, relics spoke of a once-mighty ruler consumed by unending greed, his ambitions leading to an empire's fall.", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("Guarding these echoes is the guardian of this dungeon, a lingering reminder of the emperor's unchecked desires. To claim the treasure and heed its cautionary tales,you must challenge the daunting task of overcoming this relentless guardian, a necessary sacrifice to break the cycle of unchecked ambition.", 1);

                        hasReadStory = true;
                    }
                    else { writingStyles.OverwriteLines(2); }

                    break;

                //story for the third dungeon
                case 3:

                    writingStyles.WriteLineCharactersSlowlyClamped("Now Entering: The Sand Chambers", 10);
                    Console.WriteLine();

                    if (pHasReadDungeonStory) { break; }
                    writingStyles.WriteLineCharactersSlowly("Do you wish to hear the story? yes/no: ", 1);
                    if (AskPlayer())
                    {
                        writingStyles.WriteLineCharactersSlowlyClamped("Amid the shifting dunes of the Sand Chambers, the guardian stands as protector of obscured truths. Deceptive whispers, woven into the very fabric of the desert, presented illusions challenging the resolve of explorers.", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("At the heart of this enigmatic expanse stands a figure embodying resilience forged in relentless sands. Escaping the Chambers demands not just physical strength, but a recognition of the resilience required to withstand the allure of the desert's deceptive trials.", 1);

                        hasReadStory = true;
                    }
                    else { writingStyles.OverwriteLines(2); }

                    break;

                //story for the fourth dungeon
                case 4:

                    writingStyles.WriteLineCharactersSlowlyClamped("Now Entering: The Dark Wet Caves", 10);
                    Console.WriteLine();

                    if (pHasReadDungeonStory) { break; }
                    writingStyles.WriteLineCharactersSlowly("Do you wish to hear the story? yes/no: ", 1);
                    if (AskPlayer())
                    {
                        writingStyles.WriteLineCharactersSlowlyClamped("Within the profound depths of the Dark Wet Caves, the shadowy guardian keeps watching over forgotten truths. Whispers hint at untold knowledge concealed within, awaiting discovery by the intrepid few who dare to challenge this spectral keeper.", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("To emerge enlightened from the profound darkness, you have to summon courage and confront the daunting challenge presented by the guardian, a crucial step toward unearthing the long-buried wisdom concealed within the caverns' depths.", 1);

                        hasReadStory = true;
                    }
                    else { writingStyles.OverwriteLines(2); }

                    break;

                //story for the fith dungeon
                case 5:

                    writingStyles.WriteLineCharactersSlowlyClamped("Now Entering: Resonant Vaults, The last frontier.", 10);
                    Console.WriteLine();

                    if (pHasReadDungeonStory) { break; }
                    writingStyles.WriteLineCharactersSlowly("Do you wish to hear the story? yes/no: ", 1);
                    if (AskPlayer())
                    {
                        writingStyles.WriteLineCharactersSlowlyClamped("In the depths of the Resonant Vaults, emotional narratives of the Viscacha civilization's resilience echoe through every passage. These corridors aren't mere pathways, but stories etched with a spectrum of emotions—tales of courage, sacrifice, love, and endurance.", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("Embedded within these emotional tales is a guardian, embodying the depth of experiences held within the tunnels. To grasp the profound insights woven into the Viscachas' narratives, you must embark on a journey not just through stone and earth but through the intricate emotions that shaped their civilization. ", 1);
                        Console.WriteLine();
                        writingStyles.WriteLineCharactersSlowlyClamped("Confronting the guardian isn't solely about physicality; it is a reverent acknowledgment of the emotions, sacrifices, and unwavering strength interwoven into the poignant tapestry of the Viscacha's tales of hope and perseverance.", 1);

                        hasReadStory = true;
                    }
                    else { writingStyles.OverwriteLines(2); }

                    break;
            }

            writingStyles.ClearBuffer();

            Continue();
            return hasReadStory;
        }

        /// <summary>
        /// outputs a bool based on if the player has said yes or no (yes = true, no = false)
        /// </summary>
        /// <returns></returns>
        public bool AskPlayer()
        {
            bool questionAnswered = false;
            while (!questionAnswered)
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "yes":
                    case "ja":
                    case "y":
                        writingStyles.OverwriteLines(1);
                        return true;

                    case "no":
                    case "nee":
                    case "n":
                        writingStyles.OverwriteLines(1);
                        return false;

                    default:
                        questionAnswered = false;
                        writingStyles.OverwriteLines(1);
                        break;
                }
            }

            return false;

        }

        void Continue()
        {
            Console.WriteLine();
            writingStyles.WriteCharactersSlowly("Press any key to continue.");
            Console.ReadKey(true);
            Console.Clear();
        }

    }
}
