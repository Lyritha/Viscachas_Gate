using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class BiomeTile
    {
        //public variables of tile manager
        public string tileIdentifier;
        public bool canWalkOn;
        public ConsoleColor tileColor;
        public int spawnBias;

        //change public variables depending on what was given when creating the new object
        public BiomeTile(string pTileIdentifier = "Empty", bool pCanWalkOn = true, ConsoleColor pTileColor = ConsoleColor.White, int pSpawnBias = 1)
        {
            tileIdentifier = pTileIdentifier;
            canWalkOn = pCanWalkOn;
            tileColor = pTileColor;
            spawnBias = pSpawnBias;
        }

        //method that prints the current tile
        public void PrintTile()
        {
            Console.BackgroundColor = tileColor;
            Console.Write("  ");
        }
    }
}
