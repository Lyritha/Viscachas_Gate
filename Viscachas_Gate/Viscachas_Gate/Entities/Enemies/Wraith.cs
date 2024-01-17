using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Wraith : Enemy
    {
        public Wraith()
        {
            //change some base stats for the wraith sub-class
            SetName("Wraith");
            AddMaxHealth(-50);
            AddArmor(15 + (int)(1f * GetLevel()));
            AddStrength(0.1f * GetLevel());
            AddSpeed(2 + (int)(0.30 * GetLevel()));
            experienceModifier = 1.5f;
            droppedCoinsModifier = 1.4f;
        }
    }
}
