using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Map : Item
    {

        public Map(int pDungeonID = 1)
        {
            //100 is any sort of map, the single digit defines which map it is

            UniqueID = 100 + pDungeonID;
            //set the name depending on the ID
            switch (pDungeonID)
            {
                case 1:
                    name = "Explorer's Maze Map";
                    break;

                case 2:
                    name = "Emperor's Treasure Map";
                    break;

                case 3:
                    name = "Sand Chambers Map";
                    break;

                case 4:
                    name = "Dark Wet Caves Map";
                    break;

                case 5:
                    name = "Resonant Vaults";
                    break;
            }

        }
    }
}
