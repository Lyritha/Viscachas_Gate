using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Boss : Enemy
    {
        public Boss(Player pPlayer)
        {
            //change some base stats for the Guardian sub-class
            SetName("Guardian");

            statPointsAmount += 40 * pPlayer.GetDungeonProgress();
            strength += 0.2f * pPlayer.GetDungeonProgress();
            maxHealth += 250 * pPlayer.GetDungeonProgress();
            health += 250 * pPlayer.GetDungeonProgress();
            armor = 15;
            baseDamageStats = new int[] { 5, 16 };

            experienceModifier = 2.5f;
            droppedCoinsModifier = 3;
        }
    }
}
