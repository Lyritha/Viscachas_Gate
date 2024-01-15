using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Equipment_Bow : Equipment
    {
        public Equipment_Bow() 
        {
            name = "Bow";
            itemDamage = new int[] {1,10 };
            speedBonus = 5f;
        }
    }
}
