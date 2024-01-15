using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class BossRoom : TemplateRoom
    {
        /// <summary>
        /// generates a room with a random amount of hallways, it also gives this room the ascii art that fits the amount and position of these hallways
        /// </summary>
        /// <param name="pAsciiArt"></param>
        public BossRoom(string[] pAsciiArt, bool[] pHallwaysLayout)
        {
            OverwriteHallwayLayout(pAsciiArt, pHallwaysLayout);
        }
    }
}
