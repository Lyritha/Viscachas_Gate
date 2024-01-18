using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Compass : Item
    {
        public Compass() 
        {
            name = "Compass";
            flavorText = "Use this item to find 1 store and the location of the boss in coordinates (0,0 is top left).";
        }
    }
}
