using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class DungeonEntrance : BiomeTile
    {
        public DungeonEntrance(string pTileIdentifier = "Empty", bool pCanWalkOn = true, ConsoleColor pTileColor = ConsoleColor.White, int pSpawnBias = 1)
        {
            tileIdentifier = pTileIdentifier;
            canWalkOn = pCanWalkOn;
            tileColor = pTileColor;
            spawnBias = pSpawnBias;
        }
    }
}
