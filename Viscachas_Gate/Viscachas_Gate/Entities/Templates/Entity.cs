using Microsoft.VisualBasic;
using System.Text;

namespace Viscachas_Gate
{
    internal class Entity
    {

        protected string name = "Nan";

        //will apply modifiers to the health, strength and speed later
        protected int level = 1;
        protected int statPointsAmount = 12;
        protected int statPointsPerLevelAmount = 5;
        float[] statMultipliers = { 20, 0.05f, .2f, 0.05f, 0.2f };

        //used to store information about the armor and health this entity has
        protected float maxHealth = 100;
        protected float health = 100;
        protected float armor = 0;

        protected float strength = 1f;

        //crit chance in %
        protected float criticalChance = 5;
        protected float criticalMultiplier = 1.2f;

        ///determins the chance you will hit first, the higher the stat, the higher the chance you will be able to attack first
        protected float speed = 1f;



        /// <summary>
        /// returns an array representing the stat card of this entity
        /// </summary>
        /// <returns></returns>
        string[] ShowStats()
        {
            int biggestLine = 0;
            string[] statMenu = new string[]
            {
                "┌",
                $"| Name: {name}     ",
                $"| Health: {health:0}/{maxHealth:0}     ",
                $"| Strength: {strength:0.00}x     ",
                $"| Crit Chance: {criticalChance:0.00}%     ",
                "└"
            };

            //gets the longest line character-wise
            biggestLine = statMenu.OrderByDescending(s => s.Length).First().Length;

            //adds spaces to align all items for the next step
            for (int index = 1; index < statMenu.Length - 1; index++)
            { for (int i = statMenu[index].Length; i < biggestLine; i++) { statMenu[index] += " "; } }

            //adds the extra infromation, properly alligned
            statMenu[1] += $"Lvl: {level}    ";
            statMenu[2] += $"Armor: {armor:0}    ";
            statMenu[3] += $"Speed: {speed:0.00}x    ";
            statMenu[4] += $"Crit Multiplier: {criticalMultiplier:0.00}x    ";

            //gets the longest line character-wise again with the updated info
            biggestLine = statMenu.OrderByDescending(s => s.Length).First().Length;

            //creates the line at the top of the menu
            for (int i = 1; i < biggestLine; i++) { statMenu[0] += "-"; }
            statMenu[0] += "┐";

            //creates the stat cards
            for (int index = 1; index < statMenu.Length - 1; index++)
            {
                for (int lineLength = statMenu[index].Length; lineLength < biggestLine; lineLength++)
                { statMenu[index] += " "; }
                statMenu[index] += "|";
            }

            for (int i = 1; i < biggestLine; i++) { statMenu[5] += "-"; }
            statMenu[5] += "┘";

            return statMenu;
        }
        public void PrintShowStats()
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in ShowStats())
            {
                Console.WriteLine(line);
            }
        }



        string[] ShowStatsDamage(float pDamageDone)
        {
            int biggestLine = 0;
            string[] statMenu = new string[]
            {
                "┌",
                $"| Name: {name}     ",
                $"| Health: {health:0}/{maxHealth:0} -{pDamageDone:0}     ",
                $"| Strength: {strength:0.00}x     ",
                $"| Crit Chance: {criticalChance:0.00}%     ",
                "└"
            };

            //gets the longest line character-wise
            biggestLine = statMenu.OrderByDescending(s => s.Length).First().Length;

            //adds spaces to align all items for the next step
            for (int index = 1; index < statMenu.Length - 1; index++)
            { for (int i = statMenu[index].Length; i < biggestLine; i++) { statMenu[index] += " "; } }

            //adds the extra infromation, properly alligned
            statMenu[1] += $"Lvl: {level}    ";
            statMenu[2] += $"Armor: {armor:0}    ";
            statMenu[3] += $"Speed: {speed:0.00}x    ";
            statMenu[4] += $"Crit Multiplier: {criticalMultiplier:0.00}x    ";

            //gets the longest line character-wise again with the updated info
            biggestLine = statMenu.OrderByDescending(s => s.Length).First().Length;

            //creates the line at the top of the menu
            for (int i = 1; i < biggestLine; i++) { statMenu[0] += "-"; }
            statMenu[0] += "┐";

            //creates the stat cards
            for (int index = 1; index < statMenu.Length - 1; index++)
            {
                for (int lineLength = statMenu[index].Length; lineLength < biggestLine; lineLength++)
                { statMenu[index] += " "; }
                statMenu[index] += "|";
            }

            for (int i = 1; i < biggestLine; i++) { statMenu[5] += "-"; }
            statMenu[5] += "┘";

            return statMenu;
        }
        public void PrintShowStatsDamage(float pDamageDone)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in ShowStatsDamage(pDamageDone))
            {
                Console.WriteLine(line);
            }
        }



        /// <summary>
        /// subtracts the damage done from the current health
        /// </summary>
        /// <param name="pDamage"></param>
        public void TakeDamage(float pDamage) => health -= pDamage;



        public void SetLevel(int pLevel)
        {
            //gets the difference to calculate how many stat points get added for their levels
            int difference = level - pLevel;
            statPointsAmount += difference * statPointsPerLevelAmount;
            //saves the level
            level = pLevel;
        }
        public void SetName(string pName = "Nan") => name = pName;
        public void SetHealth(float pHealth) => health = pHealth;



        public float GetHealth() => health;
        public string GetName() => name;
        public float[] GetStatMultiplier() => statMultipliers;
        public int GetStatPointsAmount() => statPointsAmount;
        public int GetLevel() => level;
        public float GetArmor() => armor;



        public void AddStatPointsAmount(int pStatPointsAmount) => statPointsAmount += pStatPointsAmount;
        public void AddLevel(int pLevel)
        {
            level += pLevel;
            statPointsAmount += statPointsPerLevelAmount;
        }
        public void AddMaxHealth(float pHealth)
        {
            maxHealth += pHealth;
            health = maxHealth;
        }
        public void AddStrength(float pStrength) => strength += pStrength;
        public void AddCriticalChance(float pCriticalChance) => criticalChance += pCriticalChance;
        public void AddCriticalMultiplier(float pCriticalMultiplier) => criticalMultiplier += pCriticalMultiplier;
        public void AddSpeed(float pSpeed) => speed += pSpeed;
        public void AddArmor(float pArmor) => armor = pArmor;
        
        
        /// <summary>
        /// updates all the adjustable stats in one go
        /// </summary>
        /// <param name="pStats"></param>
        public void UpdateStats(int[] pStats)
        {
            maxHealth += pStats[0] * statMultipliers[0];
            strength += pStats[1] * statMultipliers[1];
            criticalChance += pStats[2] * statMultipliers[2];
            criticalMultiplier += pStats[3] * statMultipliers[3];
            speed += pStats[3] * statMultipliers[3];
        }
    }

}
