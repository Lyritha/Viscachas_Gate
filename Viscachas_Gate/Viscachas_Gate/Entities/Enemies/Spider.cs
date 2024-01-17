using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Spider : Enemy
    {
        public Spider()
        {
            //change some base stats for the spider sub-class
            SetName("Spider");
            AddMaxHealth(10 * GetLevel());
            AddArmor(2 + (int)(0.2f * GetLevel()));
            AddStrength(0.15f * GetLevel());
            AddSpeed(3 + (int)(0.5 * GetLevel()));
            experienceModifier = 1.5f;
            droppedCoinsModifier = 1.3f;
        }
    }
}
