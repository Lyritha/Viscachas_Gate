using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class OpenWorld
    {
        //holds the openworld itself
        int[,] openWorld;
        int openWorldLength = 0;

        //holds all the different possible tiles in the world
        List<BiomeTile> biomes = new List<BiomeTile>
        {
            //creates the different type of tiles
            new BiomeTile("grass", true, ConsoleColor.Green, 3),
            new BiomeTile("desert", true, ConsoleColor.Yellow, 1),
            new BiomeTile("water", true, ConsoleColor.Blue, 2),
            new BiomeTile("rock", false, ConsoleColor.DarkGray, 1),
            new DungeonEntrance("dungeon", true, ConsoleColor.Magenta, 0)
        };

        //constructor, runs when the object gets created
        public OpenWorld(int pOpenWorldSize = 50, int pOpenWorldDetail = 50, int pDungeonAmount = 5)
        {
            //sets the size of the open world
            openWorld = new int[pOpenWorldSize, pOpenWorldSize];
            openWorldLength = pOpenWorldSize;

            //fills the grid with values corresponding to biomes
            OpenWorldGenerator generator = new OpenWorldGenerator(openWorld, biomes, pOpenWorldDetail, pDungeonAmount );
        }

        /// <summary>
        /// shows the map, but zoomed in, also shows other parts of the map when next to the edges of the map
        /// </summary>
        /// <param name="openWorld"></param>
        /// <param name="pPlayer"></param>
        /// <param name="pCameraSize"></param>
        public void UpdateDisplay(Player pPlayer, int pCameraSize)
        {
            //saves the player position to save on calls to other class
            int[] playerPosition = pPlayer.GetPosition();

            //prints the grid, from the perspective of the player
            for (int vertical = playerPosition[0] - pCameraSize; vertical <= playerPosition[0] + pCameraSize; vertical++)
            {
                for (int horizontal = playerPosition[1] - pCameraSize; horizontal <= playerPosition[1] + pCameraSize; horizontal++)
                {
                    //create new ints to prevent overwriting the original vertical and horizontal
                    int adjustedVertical = ClampToGrid(vertical, openWorldLength);
                    int adjustedHorizontal = ClampToGrid(horizontal, openWorldLength);

                    //if current tile isn't the same as the players then print tile, otherwise print player
                    if (playerPosition[0] != vertical || playerPosition[1] != horizontal)
                    { biomes[openWorld[adjustedVertical, adjustedHorizontal] - 1].PrintTile(); }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            //reset console text color back to white after printing map
            Console.ResetColor();
        }

        //checks if value is within grid, modify to show other side of grid
        int ClampToGrid(int value, int pOpenWorldLength)
        {
            if (value < 0) { return value += pOpenWorldLength; }
            else if (value >= pOpenWorldLength) { return value -= pOpenWorldLength; }
            return value;
        }

        public int[,] GetOpenWorld() => openWorld;
        public List<BiomeTile> GetBiomes() => biomes;
    }
}
