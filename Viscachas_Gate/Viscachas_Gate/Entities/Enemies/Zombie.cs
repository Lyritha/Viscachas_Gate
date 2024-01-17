using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Zombie : Enemy
    {
        public Zombie()
        {
            //change some base stats for the zombie sub-class
            SetName("Zombie");
            AddMaxHealth(25 * GetLevel());
            AddArmor(1 + (int)(0.1f * GetLevel()));
            AddStrength(0.2f * GetLevel());
            AddSpeed(1 + (int)(0.05 * GetLevel()));
            experienceModifier = 1.05f;
            droppedCoinsModifier = 1.1f;
        }
    }
}
