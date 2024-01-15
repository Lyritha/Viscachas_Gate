using System.Media;
using System.Numerics;

namespace Viscachas_Gate
{
    internal class ViscachaGate
    {
        Player player = null;
        OpenWorld openWorld = null;
        DungeonGenerator dungeon = null;
        StoryLibrary storyLibrary = null;
        WritingStyles writingStyles = new WritingStyles();
        Combat combat = null;
        Menus menu = null;

        //base dungeon information
        int dungeonLevel = 5;
        bool hasReadDungeonStory = false;

        public void Game(StoryLibrary pStoryLibrary, Menus pMenu) 
        {
            GameSetup(pStoryLibrary, pMenu);
            pMenu.PlayGateAnimation();
            EnterOpenWorld();
        }

        void GameSetup(StoryLibrary pStoryLibrary, Menus pMenu)
        {
            //gets reference to the story library and menu
            storyLibrary = pStoryLibrary;
            menu = pMenu;

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
            //start playing open world music
            new SoundPlayer($"Audio/Music/scott-buckley-phaseshift.wav").PlayLooping();

            //spawns the player in the open world
            player.SpawnPlayerOpenWorld(openWorld);

            //runs while the player is in the open world
            while (player.GetDungeonProgress() <= 5)
            {
                writingStyles.ClearBuffer();

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
                    new SoundPlayer($"Audio/Music/scott-buckley-phaseshift.wav").PlayLooping();
                }
            }

            //once the game is cleared
        }

        void EnterDungeon()
        {
            //start playing dungeon music
            new SoundPlayer($"Audio/Music/Ghostrifter-Official-Resurgence.wav").PlayLooping();

            //creates a new dungeon, overwriting any previously generated dungeon
            dungeon = new DungeonGenerator(15, dungeonLevel);
            combat.UpdateDungeonVar(dungeon);

            //spawns the player in the dungeon, and tells the player it is in the dungeon
            player.SetPlayerPosition(dungeon.GetStartingRoomCoordinates());
            player.SetIsInDungeon(true);

            //run story for the current dungeon
            hasReadDungeonStory = storyLibrary.EnterDungeon(player, hasReadDungeonStory);

            //dungeon loop, keeps running until either the dungeon is cleared or the player died
            while (player.GetHealth() != 0 && !dungeon.GetIsDungeonCleared())
            {
                writingStyles.ClearBuffer();

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
                new SoundPlayer($"Audio/Music/Damiano-Baldoni-Charlotte.wav").PlayLooping();

                if (combat.StartBossfight())
                {
                    //if the player clears this dungeon
                    dungeon.SetIsDungeonCleared(true);
                }
            }

            //check if room is store
            else if(currentRoom is StoreRoom)
            {
                menu.StoreMenu(dungeonLevel,player);
            }
            
            //check if room is a normal room
            else if(currentRoom is Room)
            {
                Room room = currentRoom as Room;

                if (room.GetEnemyEncounter())
                {
                    if (combat.StartEncounter())
                    { 
                        room.SetEnemyEncounter(false);

                    }
                }
            }
        }
    }
}