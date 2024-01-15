using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Spider : Enemy
    {
        public Spider()
        {
            //change some base stats for the spider sub-class
            SetName("Spider");
            AddMaxHealth(5 * GetLevel() );
            AddArmor(5);
            AddSpeed(0.2f * GetLevel() );
            experienceModifier = 1.5f;
            droppedCoinsModifier = 1.3f;
        }
    }
}
