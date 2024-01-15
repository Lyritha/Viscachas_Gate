using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Wraith : Enemy
    {
        public Wraith()
        {
            //change some base stats for the wraith sub-class
            SetName("Wraith");
            AddMaxHealth(-50);
            AddArmor(15);
            AddSpeed(0.5f * GetLevel());
            experienceModifier = 1.5f;
            droppedCoinsModifier = 1.4f;
        }
    }
}
