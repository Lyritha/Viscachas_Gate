using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Ghoul : Enemy
    {
        public Ghoul()
        {
            //change some base stats for the ghoul sub-class
            SetName("Ghoul");
            AddMaxHealth(15 * GetLevel() );
            AddStrength(0.1f * GetLevel());
            AddArmor(2 + (int)(0.5f * GetLevel()) );
            AddSpeed(2 + (int)(0.2 * GetLevel()) );
            experienceModifier = 1.1f;
            droppedCoinsModifier = 1.1f;
        }
    }
}
