using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Equipment : Item
    {
        protected int[] itemDamage = {0, 1};
        protected float speedBonus = 0;

        public int[] GetItemDamage() => itemDamage;
        public float GetSpeedBonus() => speedBonus;
    }


}
