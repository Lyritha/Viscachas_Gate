using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Combat
    {
        //creates a random object to generate random numbers
        Random random = new Random();

        Enemy enemy;
        Player player;
        DungeonGenerator dungeon;

        //keeps track of your ui position
        int[] uiPosition = { 0, 0 };

        float playerDamage = 0;
        float enemyDamage = 0;

        string playerActionText = "";
        string enemyActionText = "";

        public Combat(Player pPlayer)
        {
            //saves a reference to the player
            player = pPlayer;
        }



        public void UpdateDungeonVar(DungeonGenerator pDungeon) => dungeon = pDungeon;



        /// <summary>
        /// when a bossfight is required run this code
        /// </summary>
        /// <returns></returns>
        public bool StartBossfight()
        {

            enemy = new Boss();
            enemy.AssignLevelStats(dungeon.GetDungeonLevel());

            //clears the console before commencing combat
            Console.Clear();

            //loops until the fight is over
            BattleLoop();

            bool playerWon = enemy.GetHealth() < player.GetHealth();

            if (playerWon)
            {
                player.AddExperience(enemy.GetDroppedExperience());
                player.AddCoins(enemy.GetDroppedCoins());
            }

            //returns true if you won the fight, false if you lost
            return playerWon;
        }



        /// <summary>
        /// when a normal fight is needed run this code
        /// </summary>
        /// <param name="pPlayer"></param>
        /// <param name="pEnemy"></param>
        public bool StartEncounter()
        {
            //generates the enemy
            GenerateEnemy();

            //clears the console before comencing combat
            Console.Clear();

            //loops until the fight is over
            BattleLoop();


            bool playerWon = enemy.GetHealth() < player.GetHealth();

            if (playerWon)
            {
                Console.WriteLine("You won!");

                int droppedExperience = enemy.GetDroppedExperience();
                int droppedCoins = enemy.GetDroppedCoins();

                //display the winning screen
                DisplayWin(droppedExperience, droppedCoins);
                Console.ReadKey(true);

                //apply changes of experience and coins
                player.AddExperience(droppedExperience);
                player.AddCoins(droppedCoins);

                //if the player doesn't have a map and the enemy dropped one
                if (enemy.GetDroppedMap() && !player.GetInventory().ContainsByID(100 + player.GetDungeonProgress())) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The enemy dropped a map!");
                    Console.ForegroundColor = ConsoleColor.White;
                    player.GetInventory().AddItem(new Map(player.GetDungeonProgress())); 
                }
            }
            else
            {
                Console.WriteLine("You lost...");
                Console.ReadKey(true);
            }


            //returns true if you won the fight, false if you lost
            return playerWon;
        }
        void GenerateEnemy()
        {
            switch (random.Next(0, 5))
            {
                case 0: enemy = new Skeleton(); break;
                case 1: enemy = new Ghoul(); break;
                case 2: enemy = new Zombie(); break;
                case 3: enemy = new Spider(); break;
                case 4: enemy = new Wraith(); break;
            }

            int enemyLevel = GenerateEnemyLevel();

            enemy.AssignLevelStats(enemyLevel);
        }
        int GenerateEnemyLevel()
        {
            //saved variables
            int dungeonLevel = dungeon.GetDungeonLevel();
            int playerLevel = player.GetLevel();

            //value getting modified
            int enemyLevel = 0;

            //if the player is still under the dungeon's level
            if (playerLevel <= dungeonLevel)
            {
                //set enemy level based on player level
                enemyLevel = random.Next(playerLevel - 3, playerLevel);
            }
            //if player is above dungeon level
            else
            {
                //set enemy level based on player level
                enemyLevel = random.Next(dungeonLevel - 5, dungeonLevel + 2);
            }

            //makes sure the enemy level can't be below 0
            enemyLevel = (enemyLevel <= 0) ? 1 : enemyLevel;

            return enemyLevel;
        }



        void BattleLoop()
        {
            //keeps the battle going as long as no one is dead
            do
            {
                //keeps showing display until player has chosen a move
                do
                {
                    //clears the screen before showing the next frame
                    Console.Clear();

                    //shows the display
                    DisplayStatInfo();
                    DisplayMoves();

                } while (!PlayerInput());

                bool playerEvaded = PlayerTurn();
                EnemyTurn(playerEvaded);

                //shows the damage screen
                Console.Clear();
                DisplayStatInfoDamage();
                DisplayDamages();

                //applies the damage
                enemy.TakeDamage(playerDamage);
                player.TakeDamage(enemyDamage);

                //resets the health to 0 if health is below it
                if (player.GetHealth() < 0.99f) { player.SetHealth(0); }
                if (enemy.GetHealth() < 0.99f) { enemy.SetHealth(0); }

                Console.ReadKey(true);

            } while (player.GetHealth() != 0 && enemy.GetHealth() != 0);
        }


        /// <summary>
        /// handles the player damage logic
        /// </summary>
        /// <returns>returns if the player evaded or not</returns>
        bool PlayerTurn()
        {
            //converts ui position to int a set move
            int chosenMove = 0;
            if (uiPosition[0] == 0 && uiPosition[1] == 0) { chosenMove = 0; }
            else if (uiPosition[0] == 0 && uiPosition[1] == 1) { chosenMove = 1; }
            else if (uiPosition[0] == 1 && uiPosition[1] == 0) { chosenMove = 2; }
            else if (uiPosition[0] == 1 && uiPosition[1] == 1) { chosenMove = 3; }

            switch (chosenMove)
            {
                //attack, based on weapon
                case 0:
                case 1:
                    playerDamage = player.DealDamage(chosenMove, enemy);
                    playerActionText = $"You did {playerDamage:0} damage!";
                    break;

                //player healing
                case 2:
                    playerDamage = 0;
                    //if the player has a healing potion
                    if (player.GetInventory().GetHealingPotionAmount() > 0)
                    {
                        player.MaxHeal();
                        player.GetInventory().AddHealingPotionAmount(-1);
                        playerActionText = $"You healed, you now have {player.GetInventory().GetHealingPotionAmount()} left.";
                    }
                    else
                    {
                        playerActionText = "You have no healing potions, you wasted this turn!";
                    }
                    break;

                //player evade
                case 3:
                    //75% chance of player evading attacks
                    if (random.Next(0,4) > 0) 
                    { 
                        playerActionText = "You evaded the enemies attack!";
                        return true;
                    }
                    else
                    {
                        playerActionText = "You failed your evasion...";
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// enemy damages player
        /// </summary>
        void EnemyTurn(bool pPlayerEvaded)
        {
            enemyDamage = (!pPlayerEvaded) ? enemy.DealDamage(player) : 0;
            enemyActionText = $"The {enemy.GetName()} did {enemyDamage:0} damage!";
        }



        string[] GenerateMoveDisplay()
        {
            //creates the "grid" of items
            string[,] menuItems = new string[2, 2];

            //fill grid with elements depending on if it's selected or not
            menuItems[0, 0] = (uiPosition[0] == 0 && uiPosition[1] == 0) ? "> Sword" : "Sword";
            menuItems[0, 1] = (uiPosition[0] == 0 && uiPosition[1] == 1) ? "> Bow" : "Bow";
            menuItems[1, 0] = (uiPosition[0] == 1 && uiPosition[1] == 0) ? $"> Healing Potion: {player.GetInventory().GetHealingPotionAmount()}" : $"Healing Potion: {player.GetInventory().GetHealingPotionAmount()}";
            menuItems[1, 1] = (uiPosition[0] == 1 && uiPosition[1] == 1) ? "> Evade" : "Evade";



            int biggestLine = 0;

            string[] MoveDisplay = new string[]
            {
                "┌",
                $"| {menuItems[0,0]}     ",
                $"| {menuItems[1,0]}     ",
                "└"
            };

            //gets the longest line character-wise
            biggestLine = MoveDisplay.OrderByDescending(s => s.Length).First().Length;

            //adds spaces to align all items for the next step
            for (int index = 1; index < MoveDisplay.Length - 1; index++)
            { for (int i = MoveDisplay[index].Length; i < biggestLine; i++) { MoveDisplay[index] += " "; } }

            //adds the extra infromation, properly alligned
            MoveDisplay[1] += $"{menuItems[0,1]}    ";
            MoveDisplay[2] += $"{menuItems[1,1]}    ";

            //gets the longest line character-wise again with the updated info
            biggestLine = MoveDisplay.OrderByDescending(s => s.Length).First().Length;

            //makes top border
            for (int i = 1; i < biggestLine; i++) { MoveDisplay[0] += "-"; }
            MoveDisplay[0] += "┐";

            //creates the stat cards
            for (int index = 1; index < MoveDisplay.Length - 1; index++)
            {
                for (int lineLength = MoveDisplay[index].Length; lineLength < biggestLine; lineLength++)
                { MoveDisplay[index] += " "; }
                MoveDisplay[index] += "|";
            }

            //makes bottom border
            for (int i = 1; i < biggestLine; i++) { MoveDisplay[3] += "-"; }
            MoveDisplay[3] += "┘";

            return MoveDisplay;
        }
        void DisplayMoves()
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in GenerateMoveDisplay())
            {
                Console.WriteLine(line);
            }
        }



        string[] GenerateDamageDisplay()
        {
            int biggestLine = 0;
            string[] MoveDisplay = new string[]
            {
                "┌",
                $"| {playerActionText}     ",
                $"| {enemyActionText}     ",
                "└"
            };

            //gets the longest line character-wise
            biggestLine = MoveDisplay.OrderByDescending(s => s.Length).First().Length;

            //adds spaces to align all items for the next step
            for (int index = 1; index < MoveDisplay.Length - 1; index++)
            { for (int i = MoveDisplay[index].Length; i < biggestLine; i++) { MoveDisplay[index] += " "; } }

            //gets the longest line character-wise again with the updated info
            biggestLine = MoveDisplay.OrderByDescending(s => s.Length).First().Length;

            //makes top border
            for (int i = 1; i < biggestLine; i++) { MoveDisplay[0] += "-"; }
            MoveDisplay[0] += "┐";

            //creates the stat cards
            for (int index = 1; index < MoveDisplay.Length - 1; index++)
            {
                MoveDisplay[index] += "|";
            }

            //makes bottom border
            for (int i = 1; i < biggestLine; i++) { MoveDisplay[3] += "-"; }
            MoveDisplay[3] += "┘";

            return MoveDisplay;
        }
        void DisplayDamages()
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in GenerateDamageDisplay())
            {
                Console.WriteLine(line);
            }
        }

        string[] GenerateWinDisplay(int pDroppedExperience, int pDroppedCoins)
        {
            int biggestLine = 0;
            string[] MoveDisplay = new string[]
            {
                "┌",
                $"| Dropped Experience: +{pDroppedExperience}, {player.GetCurrentExperience() + pDroppedExperience}/{player.GetRequiredExperience()}     ",
                $"| Dropped Coins: +{pDroppedCoins}, you now have {player.GetInventory().GetCoins() + pDroppedCoins}     ",
                "└"
            };

            //gets the longest line character-wise
            biggestLine = MoveDisplay.OrderByDescending(s => s.Length).First().Length;

            //adds spaces to align all items for the next step
            for (int index = 1; index < MoveDisplay.Length - 1; index++)
            { for (int i = MoveDisplay[index].Length; i < biggestLine; i++) { MoveDisplay[index] += " "; } }

            //gets the longest line character-wise again with the updated info
            biggestLine = MoveDisplay.OrderByDescending(s => s.Length).First().Length;

            //makes top border
            for (int i = 1; i < biggestLine; i++) { MoveDisplay[0] += "-"; }
            MoveDisplay[0] += "┐";

            //creates the stat cards
            for (int index = 1; index < MoveDisplay.Length - 1; index++)
            {
                MoveDisplay[index] += "|";
            }

            //makes bottom border
            for (int i = 1; i < biggestLine; i++) { MoveDisplay[3] += "-"; }
            MoveDisplay[3] += "┘";

            return MoveDisplay;
        }
        void DisplayWin(int pDroppedExperience, int pDroppedCoins)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in GenerateWinDisplay(pDroppedExperience, pDroppedCoins))
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// gets the input used for the selection move menu, gets a bool back if a player selected something
        /// </summary>
        bool PlayerInput()
        {
            //gets user input
            string input = Console.ReadKey(true).Key.ToString().ToLower();
            int[] savedPosition = uiPosition;

            //allows for interaction
            switch(input)
            {
                case "w":
                case "uparrow":
                    savedPosition[0]--;
                    break;

                case "s":
                case "downarrow":
                    savedPosition[0]++;
                    break;

                case "a":
                case "leftarrow":
                    savedPosition[1]--;
                    break;

                case "d":
                case "rightarrow":
                    savedPosition[1]++;
                    break;

                case "enter":
                    return true;
            }


            //clamps values to grid
            uiPosition[0] = (savedPosition[0] < 0) ? 0 : savedPosition[0];
            uiPosition[0] = (savedPosition[0] > 1) ? 1 : savedPosition[0];
            uiPosition[1] = (savedPosition[1] < 0) ? 0 : savedPosition[1];
            uiPosition[1] = (savedPosition[1] > 1) ? 1 : savedPosition[1];

            return false;
        }

        void DisplayStatInfo()
        {
            player.PrintShowStats();
            enemy.PrintShowStats();
        }
        void DisplayStatInfoDamage()
        {
            player.PrintShowStatsDamage(enemyDamage);
            enemy.PrintShowStatsDamage(playerDamage);
        }

    }
}
