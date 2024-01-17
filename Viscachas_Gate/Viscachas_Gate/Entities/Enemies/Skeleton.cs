using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Skeleton : Enemy
    {
        public Skeleton()
        {
            //change some base stats for the skeleton sub-class
            SetName("Skeleton");
            AddMaxHealth(-50);
            AddMaxHealth(5 * GetLevel());
            AddArmor(5 + (int)(0.5f * GetLevel()));
            AddStrength(0.1f * GetLevel());
            AddSpeed((int)(0.2 * GetLevel()));
            experienceModifier = 0.8f;
            droppedCoinsModifier = 0.9f;
        }


    }
}
