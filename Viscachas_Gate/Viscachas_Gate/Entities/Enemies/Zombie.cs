using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Zombie : Enemy
    {
        public Zombie()
        {
            //change some base stats for the zombie sub-class
            SetName("Zombie");
            AddMaxHealth(10 * GetLevel() );
            experienceModifier = 1.05f;
            droppedCoinsModifier = 1.1f;
        }
    }
}
