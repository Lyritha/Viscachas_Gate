using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Skeleton : Enemy
    {
        public Skeleton()
        {
            //change some base stats for the skeleton sub-class
            SetName("Skeleton");
            AddMaxHealth(-50);
            AddArmor(5);
            experienceModifier = 0.8f;
            droppedCoinsModifier = 0.9f;
        }


    }
}
