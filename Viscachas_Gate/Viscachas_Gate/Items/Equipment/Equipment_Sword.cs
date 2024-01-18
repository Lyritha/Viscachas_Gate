using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Equipment_Sword : Equipment
    {
        public Equipment_Sword()
        {
            name = "Sword";
            itemDamage = new int[] { 6, 12 };
            speedBonus = 0f;

            flavorText = "This item is very sharp, i should be careful with it.";
        }
    }
}
