using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Equipment_Sword : Equipment
    {
        public Equipment_Sword()
        {
            name = "Sword";
            itemDamage = new int[] { 4, 15 };
            speedBonus = 0f;
        }
    }
}
