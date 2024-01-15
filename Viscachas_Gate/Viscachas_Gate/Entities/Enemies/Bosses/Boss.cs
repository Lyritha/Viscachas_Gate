using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Boss : Enemy
    {
        public Boss()
        {
            //change some base stats for the Guardian sub-class
            SetName("Guardian");
            statPointsAmount += 15;
            experienceModifier = 2.5f;
            droppedCoinsModifier = 3;
        }
    }
}
