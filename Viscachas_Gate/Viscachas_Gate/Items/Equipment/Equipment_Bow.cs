using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Equipment_Bow : Equipment
    {
        public Equipment_Bow() 
        {
            name = "Bow";
            itemDamage = new int[] {2,18 };
            speedBonus = 5f;

            flavorText = "I can use this to shoot very far, but i can get a bad shot too.";
        }
    }
}
