using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class StoreRoom : TemplateRoom
    {
        public StoreRoom(string[] pAsciiArt, bool[] pHallwaysLayout)
        {
            OverwriteHallwayLayout(pAsciiArt, pHallwaysLayout);
        }


    }
}
