using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Enemy : Entity
    {
        Random random = new Random();

        protected float experienceModifier = 1;
        protected float droppedCoinsModifier = 1;

        //stores the base damage the entity does, allows for adjusting
        protected int[] baseDamageStats = { 1, 11 };

        protected int mapDropChance = 5;

        protected void RandomStats()
        {
            float[] statMultipliers = GetStatMultiplier();

            while (GetStatPointsAmount() >= 0)
            {
                switch (random.Next(0, 5))
                {
                    case 0:
                        AddMaxHealth((int)statMultipliers[0]);
                        AddStatPointsAmount(-1);
                        break;

                    case 1:
                        AddStrength(statMultipliers[1]);
                        AddStatPointsAmount(-1);
                        break;

                    case 2:
                        AddCriticalChance(statMultipliers[2]);
                        AddStatPointsAmount(-1);
                        break;

                    case 3:
                        AddCriticalMultiplier(statMultipliers[3]);
                        AddStatPointsAmount(-1);
                        break;

                    case 4:
                        AddSpeed(statMultipliers[4]);
                        AddStatPointsAmount(-1);
                        break;
                }
            }
        }

        public int GetDroppedExperience() => (int)(100 + ( 50 * GetLevel() * experienceModifier));

        public int GetDroppedCoins() => (int)(100 + (100 * GetLevel() * droppedCoinsModifier));

        public bool GetDroppedMap() => (random.Next(1, 101) <= mapDropChance) ? true : false;

        public void AssignLevelStats(int pLevel)
        {

            //sets the level of the enemy, also updates the stat points
            SetLevel(pLevel);

            //generate random values
            RandomStats();
        }

        public float DealDamage(Player pPlayer)
        {
            //generates a random base damage amount, amount will be adjusted for balancing in the future
            int baseDamage = random.Next(baseDamageStats[0], baseDamageStats[1]);

            //calculates the crit chance and damage
            float critical = random.Next(0, 101) <= criticalChance ? criticalMultiplier : 1f;

            //calculates the final damage output
            float damageDone = ((baseDamage * strength) * critical) / 100f * (100f - pPlayer.GetArmor());

            return damageDone;
        }
    }
}
