using System;
using System.Numerics;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Player : Entity
    {
        int[] distributedPoints = { 0, 0, 0, 0, 0 };

        Menus menu;

        //holds the position of the player
        int[] position = { 0, 0 };

        //checks if the player is in a dungeon
        bool isInDungeon = false;
        int dungeonProgress = 1;

        //player leveling system
        int experience = 0;
        int requiredExperience = 700;

        Inventory inventory = new Inventory();

        public Player(Menus pMenu)
        {
            menu = pMenu;
            AddMaxHealth(100);
            MaxHeal();
        }



        //movement logic
        /// <summary>
        /// handles all the player controls for both the dungeon and the open world, this does all the collision checks and rules for movements
        /// </summary>
        /// <param name="pDungeon"></param>
        /// <param name="pOpenWorld"></param>
        public void PlayerInput(OpenWorld pOpenWorld, Dungeon pDungeon = null)
        {
            //tells the user how to control the player
            Console.Write("WASD to walk around, Q to give text input.");

            //gets the user input
            string input = Console.ReadKey(true).Key.ToString().ToLower();

            //handles all the controls
            switch (input)
            {
                case "w":
                case "uparrow":
                    if (isInDungeon) { DungeonMove(pDungeon, 0); } else { OpenWorldMove(pOpenWorld, 0); }
                    break;

                case "s":
                case "downarrow":
                    if (isInDungeon) { DungeonMove(pDungeon, 2); } else { OpenWorldMove(pOpenWorld, 2); }
                    break;

                case "a":
                case "leftarrow":
                    if (isInDungeon) { DungeonMove(pDungeon, 3); } else { OpenWorldMove(pOpenWorld, 3); }
                    break;

                case "d":
                case "rightarrow":
                    if (isInDungeon) { DungeonMove(pDungeon, 1); } else { OpenWorldMove(pOpenWorld, 1); }
                    break;

                case "q":
                    Console.WriteLine();
                    Console.WriteLine("Text input mode: ");
                    Console.Write(">");
                    TextInput(pDungeon);
                    break;

                case "escape":
                    menu.PauseMenu(this, pOpenWorld, pDungeon);
                    break;

                default:
                    Console.WriteLine("Not a valid input");
                    Thread.Sleep(500);
                    break;
            }
        }
        void TextInput(Dungeon pDungeon)
        {
            string playerInput = Console.ReadLine().ToLower();

            switch (playerInput)
            {

                case "experience":
                case "exp":
                case "check experience":
                case "check exp":
                    Console.Write($"You have: {experience}/{requiredExperience}EXP");
                    break;

                case "inv":
                case "inventory":
                case "check inventory":
                    inventory.PrintInventory(this);
                    break;

                //allows the player to use the map item
                case "map":
                case "show map":
                case "open map":

                    if (inventory.ContainsByID(100 + dungeonProgress))
                    {
                        if (isInDungeon) { menu.MapMenu(pDungeon, position); }
                        else { Console.WriteLine("You need to be inside a dungeon to use this item"); };
                    } else { Console.WriteLine("You do not have this item!"); }

                    break;

                case "compass":
                case "show compass":
                case "open compass":
                    if (inventory.ContainsByName("Compass"))
                    {
                        if (isInDungeon)
                        {
                            //displays some info
                            Console.WriteLine($"The compass starts to spin, then stopping suddenly, pointing two way:");
                            Console.WriteLine($"Boss Room: Vertical:{pDungeon.GetBossRoomCoordinates()[0]}, Horizontal:{pDungeon.GetBossRoomCoordinates()[1]}");
                            Console.WriteLine($"Store room (there are 2 more): Vertical:{pDungeon.GetStoreRoomCoordinates()[0]}, Horizontal:{pDungeon.GetStoreRoomCoordinates()[1]}");
                            Console.WriteLine($"You: Vertical:{position[0]}, Horizontal:{position[1]}");
                        }
                        else { Console.WriteLine("You need to be inside a dungeon to use this item"); };
                    }
                    else { Console.WriteLine("You do not have this item!"); }


                    break;

                case "heal":
                case "use heal":
                case "potion":
                case "use potion":
                case "healing potion":
                case "use healing potion":
                    //if the player has a healing potion
                    if (GetInventory().GetHealingPotionAmount() > 0)
                    {
                        PotionHeal();
                        GetInventory().AddHealingPotionAmount(-1);
                        Console.WriteLine($"You healed, you now have {GetInventory().GetHealingPotionAmount()} left.");
                    }
                    else
                    {
                        Console.WriteLine("You have no healing potions");
                    }
                    break;

                default:
                    Console.Write("Check the escape/pause menu for some possible commands.");
                    break;
            }

            Console.ReadKey(true);
        }



        //dungeon movement
        /// <summary>
        /// if the player wants to move in a dungeon checks if you are allowed to move a direction, if you can then move the player
        /// </summary>
        /// <param name="pDungeon"></param>
        /// <param name="pDirection"></param>
        /// <returns></returns>
        void DungeonMove(Dungeon pDungeon, int pDirection)
        {

            if (pDungeon.GetRoomObject(position).GetHallways()[pDirection])
            {
                switch (pDirection)
                {
                    case 0: position[0]--; break;
                    case 1: position[1]++; break;
                    case 2: position[0]++; break;
                    case 3: position[1]--; break;
                }
            }
        }



        //open world movement
        /// <summary>
        /// handles the logic to walk around in the open world, does bound teleportation and collission checks
        /// </summary>
        /// <param name="pOpenWorld"></param>
        /// <param name="pPositionX"></param>
        /// <param name="pPositionY"></param>
        void OpenWorldMove(OpenWorld pOpenWorld, int pDirection)
        {
            int positionX = position[0];
            int positionY = position[1];

            switch (pDirection)
            {
                case 0: positionX--; break;
                case 1: positionY++; break;
                case 2: positionX++; break;
                case 3: positionY--; break;
            }

            //check if player position is out of bounds
            if (positionX < 0 || positionY < 0 || positionX >= pOpenWorld.GetOpenWorld().GetLength(0) || positionY >= pOpenWorld.GetOpenWorld().GetLength(1))
            {
                //teleport player if player tries going out of bounds
                OpenWorldBoundsTeleport(pOpenWorld, positionX, positionY);
            }

            //if no bounds teleport, then check the collission
            else if (pOpenWorld.GetBiomes()[pOpenWorld.GetOpenWorld()[positionX, positionY] - 1].canWalkOn)
            {
                position[0] = positionX;
                position[1] = positionY;
            }
        }
        /// <summary>
        /// handles bound teleportation, if the player is outside 
        /// </summary>
        /// <param name="pOpenWorld"></param>
        /// <param name="pPositionX"></param>
        /// <param name="pPositionY"></param>
        void OpenWorldBoundsTeleport(OpenWorld pOpenWorld, int pPositionX, int pPositionY)
        {
            //if player is out of bounds, move player coords to other side of map
            pPositionX = (pPositionX + pOpenWorld.GetOpenWorld().GetLength(0)) % pOpenWorld.GetOpenWorld().GetLength(0);
            pPositionY = (pPositionY + pOpenWorld.GetOpenWorld().GetLength(1)) % pOpenWorld.GetOpenWorld().GetLength(1);

            //if player can walk on the other side then teleport player
            if (pOpenWorld.GetBiomes()[pOpenWorld.GetOpenWorld()[pPositionX, pPositionY] - 1].canWalkOn)
            {
                position[0] = pPositionX;
                position[1] = pPositionY;
            }
        }
        /// <summary>
        /// sets the spawn point of the player randomly in the open world
        /// </summary>
        /// <param name="pOpenWorld"></param>
        public void SpawnPlayerOpenWorld(OpenWorld pOpenWorld)
        {
            //create object of class random called random
            Random random = new Random();

            //create 2 ints with base value of 0
            int[] randomCoords = { 0, 0 };

            //keeps running until valid spawn point has been found
            do
            {
                //create random coords, and check if the chose tile can be walked on, if true break loop and return the random coords
                randomCoords[0] = random.Next(0, pOpenWorld.GetOpenWorld().GetLength(0));
                randomCoords[1] = random.Next(0, pOpenWorld.GetOpenWorld().GetLength(1));
            } while (!pOpenWorld.GetBiomes()[pOpenWorld.GetOpenWorld()[randomCoords[0], randomCoords[1]] - 1].canWalkOn);

            //randomly sets the players position
            position = randomCoords;
        }



        public float DealDamage(int pChosenWeapon, Enemy pEnemy, Random pRandom)
        {
            //gets the weapon stats
            int baseDamage = pRandom.Next(inventory.GetEquippedItems()[pChosenWeapon].GetItemDamage()[0], inventory.GetEquippedItems()[pChosenWeapon].GetItemDamage()[1]);

            //calculates the crit chance and damage
            float critical = pRandom.Next(0, 101) <= criticalChance ? criticalMultiplier : 1f;

            float damageDone = ((baseDamage * strength) * critical) / 100f * (100f - pEnemy.GetArmor());

            //creates the final amount of damage
            return damageDone;
        }


        public void AddExperience(int pExperience)
        {
            //update experience
            experience += pExperience;

            //if a level up happened, increase the level, deduct the xp, and set a new required experience level
            if (experience >= requiredExperience) 
            { 
                AddLevel(1);
                experience -= requiredExperience;

                requiredExperience += 500;

                Console.Clear();
                PrintShowStats();
                menu.StatDistributionMenu(this);
            }
        }
        public void AddCoins(int pCoins) => inventory.AddCoins(pCoins);
        public void AddDungeonProgress(int pDungeonProgress) => dungeonProgress += pDungeonProgress;



        public void SetPlayerPosition(int[] pPosition) => position = pPosition;
        public void SetDungeonProgress(int pDungeonProgress) => dungeonProgress = pDungeonProgress;
        public void SetIsInDungeon(bool pIsInDungeon) => isInDungeon = pIsInDungeon;

        public int GetRequiredExperience() => requiredExperience;
        public int GetCurrentExperience() => experience;
        public int[] GetPosition() => position;
        public int GetDungeonProgress() => dungeonProgress;
        public bool GetIsInDungeon() => isInDungeon;
        public int[] GetDistributedPoints() => distributedPoints;
        public Inventory GetInventory() => inventory;


        public void MaxHeal() => SetHealth(maxHealth);
        public void PotionHeal() => health = maxHealth;
    }
}
