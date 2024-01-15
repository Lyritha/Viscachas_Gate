using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Ghoul : Enemy
    {
        public Ghoul()
        {
            //change some base stats for the ghoul sub-class
            SetName("Ghoul");
            AddMaxHealth(5 * GetLevel() );
            AddSpeed(0.1f * GetLevel() );
            AddArmor(5 + (0.5f * GetLevel()) );
            experienceModifier = 1.1f;
            droppedCoinsModifier = 1.1f;
        }
    }
}
