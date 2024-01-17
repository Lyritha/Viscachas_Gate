using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class OpenWorldGenerator
    {
        //saves lengths for slight optimization to reduce excesive calls, also saves the array
        int[] openWorldSize;
        int[,] openWorld;
        List<BiomeTile> biomes;

        //saves all possible coordinate to grow biomes from
        List<int[]> growCoordinates = new List<int[]>();

        public OpenWorldGenerator(int[,] pOpenWorld, List<BiomeTile> pBiomes, int pRandomPointsAmount, int pDungeonAmount)
        {
            //create random object called random
            Random random = new Random();

            //saves information that will be user repeatedly in this class
            openWorldSize = new int[]{ pOpenWorld.GetLength(0), pOpenWorld.GetLength(1)};
            openWorld = pOpenWorld;
            biomes = pBiomes;

            //spreads random "seeds" to spread biomes from
            SpreadRandomPoints(random, pRandomPointsAmount);

            //spread from filled tiles and fill map until there are no more tiles to spread from
            while (growCoordinates.Count > 0) { FillBiomes(random); }

            //cleans the map up a bit
            CleanUp(random);

            //spawns the dungeons
            SpawnDungeons(random, pBiomes, pDungeonAmount);
        }

        //spawn random "seeds" to grow biomes/tiles from
        void SpreadRandomPoints(Random pRandom, int pointsAmount = 10)
        {
            //combines all the spawn biases to use to create random numb
            int totalBias = biomes.Sum(biome => biome.spawnBias);

            //randomly spread points on map
            for (int point = 0; point < pointsAmount; point++)
            {
                //get random coordinate within the map bounds
                int[] randomCoordinates = { pRandom.Next(0, openWorldSize[0]), pRandom.Next(0, openWorldSize[1]) };

                //keeps count of the current position of bias
                int biasCounter = 0;
                //creates a random value with the range of all the biases combined
                int randomValue = pRandom.Next(0, totalBias + 1);
                //creates a value to modify and finally assign to the open world
                int finalValue = 1;

                //loop through all the biomes in the list
                foreach(BiomeTile biome in biomes)
                {
                    //check if the random value is within the range of bias (for example: 0,1 if the first biome has a bias of one, 1,3 if the second biome has a bias of 2)
                    if (randomValue >= biasCounter && randomValue <= biasCounter + biome.spawnBias)
                    { break; }
                    biasCounter += biome.spawnBias;
                    finalValue++;
                }

                //sets the value of the tile
                openWorld[randomCoordinates[0], randomCoordinates[1]] = finalValue;
                //saves the tile coords in a list
                growCoordinates.Add(randomCoordinates);
            }

        }

        //spreads the biomes until the map is full
        void FillBiomes(Random pRandom)
        {
            //save how many coordinate there are saved in the list
            int locationsAmount = growCoordinates.Count;

            //check if the amount of coordinate saved is above 0, to avoid out of index errors at the random number generator (0,0 not possible)
            if (locationsAmount > 0)
            {
                //choose random index to make the spreading is a bit more chaotic
                int randomIndex = pRandom.Next(0, locationsAmount);
                int[] coordinate = growCoordinates[randomIndex];

                //goes around the current point
                for (int addVertical = -1; addVertical <= 1; addVertical++)
                {
                    for (int addHorizontal = -1; addHorizontal <= 1; addHorizontal++)
                    {
                        int totalVertical = AdjustBorders(coordinate[0] + addVertical, openWorldSize[0]);
                        int totalHorizontal = AdjustBorders(coordinate[1] + addHorizontal, openWorldSize[1]);

                        //if the found tile is 0, then overwrite that tile with the middle tile's color
                        if (openWorld[totalVertical, totalHorizontal] == 0) 
                        { 
                            openWorld[totalVertical, totalHorizontal] = openWorld[coordinate[0], coordinate[1]];
                            int[] totalCoords = { totalVertical, totalHorizontal };
                            growCoordinates.Add(totalCoords);
                        }
                    }
                }

                //removes the current coord from the list to avoid repetition
                growCoordinates.RemoveAt(randomIndex);
            }

        }

        void CleanUp(Random pRandom)
        {
            for(int vertical = 0; vertical < openWorldSize[0]; vertical++)
            {
                for( int horizontal = 0; horizontal < openWorldSize[1]; horizontal++)
                {
                    int differentBiome = 0;

                    //goes around the current point
                    for (int addVertical = -1; addVertical <= 1; addVertical++)
                    {
                        for (int addHorizontal = -1; addHorizontal <= 1; addHorizontal++)
                        {
                            int totalVertical = AdjustBorders(vertical + addVertical, openWorldSize[0]);
                            int totalHorizontal = AdjustBorders(horizontal + addHorizontal, openWorldSize[1]);

                            if (openWorld[totalVertical, totalHorizontal] != openWorld[vertical, horizontal] && totalVertical != vertical && totalHorizontal != horizontal)
                            {
                                differentBiome++;
                            }
                        }
                    }

                    if (differentBiome > 5)
                    {
                        bool validCoords = false;
                        while (!validCoords)
                        {
                            //try to replace pixel with coord around it, if it fails replace with other 
                            try { openWorld[vertical, horizontal] = openWorld[vertical + pRandom.Next(-1, 2), horizontal + pRandom.Next(-1, 2)]; validCoords = true; } catch { validCoords = false; }
                        }
                    }

                }
            }
        }

        void SpawnDungeons(Random pRandom, List<BiomeTile> pBiomes, int pDungeonAmount)
        {
            //create multiple dungeons
            for (int currentAmount = 0; currentAmount < pDungeonAmount; currentAmount++)
            {
                //create 2 ints with base value of 0
                int[] randomCoords = { 0, 0 };

                //keeps running until valid spawn point has been found
                do
                {
                    randomCoords[0] = pRandom.Next(0, openWorld.GetLength(0));
                    randomCoords[1] = pRandom.Next(0, openWorld.GetLength(1));
                } while (!pBiomes[openWorld[randomCoords[0], randomCoords[1]] - 1].canWalkOn);

                //will output a value with a bigger index then the biomes and set the openworld value to it
                openWorld[randomCoords[0], randomCoords[1]] = biomes.Count;
            }
        }

        int AdjustBorders(int value, int pOpenWorldLength)
        {
            if (value < 0) { return value += pOpenWorldLength; }
            else if (value >= pOpenWorldLength) { return value -= pOpenWorldLength; }
            return value;
        }
    }
}
