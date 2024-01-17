using System.Media;
using System.Numerics;

namespace Viscachas_Gate
{
    internal class MainGame
    {
        Menus menu = null;
        AudioHandler audioHandler = null;
        
        Player player = null;
        OpenWorld openWorld = null;
        Dungeon dungeon = null;
        
        Combat combat = null;

        StoryLibrary storyLibrary = null;
        PrintBehaviors printBehaviors = new();


        //base dungeon information
        int dungeonLevel = 5;
        bool hasReadDungeonStory = false;

        public void Game(StoryLibrary pStoryLibrary, Menus pMenu, AudioHandler pAudioHandler) 
        {
            GameSetup(pStoryLibrary, pMenu, pAudioHandler);
            pMenu.PlayGateAnimation();
            EnterOpenWorld();
        }

        void GameSetup(StoryLibrary pStoryLibrary, Menus pMenu, AudioHandler pAudioHandler)
        {
            //gets reference to the story library and menu
            storyLibrary = pStoryLibrary;
            menu = pMenu;
            audioHandler = pAudioHandler;

            //creates ther player and displays the character creator
            player = new Player(menu);
            menu.CharacterCreator(player, storyLibrary);

            //create the combat class, while giving the player as a reference
            combat = new Combat(player);

            //generate open world
            openWorld = new OpenWorld();
        }

        void EnterOpenWorld()
        {
            audioHandler.PlayOpenWorldMusic();

            //spawns the player in the open world
            player.SpawnPlayerOpenWorld(openWorld);

            //runs while the player hasn't finished all the dungeons yet
            while (player.GetDungeonProgress() <= 5)
            {
                printBehaviors.ClearBuffer();
                Console.Clear();

                //updates the screen to show the open world
                openWorld.UpdateDisplay(player, 8);
                player.PrintShowStats();

                //allows the player to move in the open world, also allows for dungeon movement when needed
                player.PlayerInput(openWorld);

                //if the player overlaps with a dungeon tile, create and enter dungeon
                if (openWorld.GetOpenWorld()[player.GetPosition()[0], player.GetPosition()[1]] == openWorld.GetBiomes().Count)
                { 
                    EnterDungeon();
                    //start playing open world music again once the player leaves the dungeon
                    audioHandler.PlayOpenWorldMusic();
                }
            }

            //once the game is cleared
            menu.GameWinScreen();
        }

        void EnterDungeon()
        {
            //start playing dungeon music
            audioHandler.PlayDungeonMusic();

            //creates a new dungeon, overwriting any previously generated dungeon
            dungeon = new Dungeon(15, dungeonLevel);
            combat.UpdateDungeonVar(dungeon);

            //spawns the player in the dungeon, and tells the player it is in the dungeon
            player.SetPlayerPosition(dungeon.GetStartingRoomCoordinates());
            player.SetIsInDungeon(true);

            //run story for the current dungeon
            hasReadDungeonStory = storyLibrary.EnterDungeon(player, hasReadDungeonStory);

            //dungeon loop, keeps running until either the dungeon is cleared or the player died
            while (player.GetHealth() != 0 && !dungeon.GetIsDungeonCleared())
            {
                printBehaviors.ClearBuffer();
                Console.Clear();


                //shows current room
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(dungeon.GetRoomObject(player.GetPosition()).GetRoomArt());
                player.PrintShowStats();

                //allows player to move
                player.PlayerInput(openWorld, dungeon);

                EnterRoom();
            }

            //assigns values if dungeon is cleared
            if (dungeon.GetIsDungeonCleared())
            {
                player.AddDungeonProgress(1);
                dungeonLevel += 5;
                hasReadDungeonStory = false;
            }

            //spawns the player in the open world
            player.SpawnPlayerOpenWorld(openWorld);

            //transports player back to open world, and heals them to their max health
            player.SetIsInDungeon(false);
            player.MaxHeal();
            player.GetInventory().RechargeHealingPotion();
        }

        void EnterRoom()
        {
            // check if room is of specific type
            TemplateRoom currentRoom = dungeon.GetRoomObject(player.GetPosition());

            //check if room is boss room
            if (currentRoom is BossRoom)
            {
                //start playing boss music
                audioHandler.PlayBossMusic();

                if (combat.StartBossfight(audioHandler))
                {
                    //if the player clears this dungeon
                    dungeon.SetIsDungeonCleared(true);
                }
            }

            //check if room is store
            else if(currentRoom is StoreRoom)
            {
                Console.WriteLine();
                printBehaviors.OverwriteLines(1);
                Console.WriteLine("Would you like to enter the store?");
                if (storyLibrary.AskPlayer())
                {
                    menu.StoreMenu(dungeonLevel, player);
                }
            }
            
            //check if room is a normal room
            else if(currentRoom is Room)
            {
                Room room = currentRoom as Room;

                if (room.GetEnemyEncounter())
                {
                    if (combat.StartEncounter(audioHandler))
                    { 
                        room.SetEnemyEncounter(false);

                    }
                }
            }
        }
    }
}