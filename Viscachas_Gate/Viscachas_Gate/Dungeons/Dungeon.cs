using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    internal class Dungeon
    {
        Random random = new Random();

        //creates a 2d grid holding the dungeon in it
        TemplateRoom[,] dungeon;

        //holds the ascii art for all the possible rooms
        string[] asciiArtHallRooms;

        int[] startingRoomCoordinates = new int[2];
        int[] bossRoomCoordinates = new int[2];
        int[] storeRoomCoordinates = new int[2];
        bool isDungeonCleared = false;

        int dungeonLevel = 5;

        //stores all the NewRoomsCoordinates to generate from
        List<int[]> newRoomsCoordinates = new List<int[]>();
        int generatedRoomsCount = 0;

        /// <summary>
        /// creates a new object that holds a randomly generated dungeon with connected rooms
        /// </summary>
        /// <param name="pDungeonDimensions"></param>
        public Dungeon(int pDungeonDimensions = 10, int pDungeonLevel = 5)
        {
            //sets the level of this dungeon
            dungeonLevel = pDungeonLevel;

            //loads all the ascii art of the rooms
            LoadAsciiArt();

            //loops until a currently sized dungeon gets created
            do
            {
                generatedRoomsCount = 0;

                //creates the dungeon grid
                dungeon = new TemplateRoom[pDungeonDimensions, pDungeonDimensions];

                //generates the actual rooms
                GenerateDungeon(pDungeonDimensions);

            } while (generatedRoomsCount < pDungeonDimensions * pDungeonDimensions / 2);
        }


        void LoadAsciiArt()
        {
            asciiArtHallRooms = File.ReadAllText("ascii_art/Hallways.txt").Split(',');
        }
        /// <summary>
        /// randomly generates a dungeon within a grid
        /// </summary>
        /// <param name="pDungeonDimensions"></param>
        void GenerateDungeon(int pDungeonDimensions)
        {
            CreateStartingRoom(pDungeonDimensions);

            //runs until no further hallways to grow from exist (rooms with currentRoomHallways)
            while (newRoomsCoordinates.Count > 0) { GenerateRooms(); }

            //removes hallways that go to nothing
            RemoveStrayHallways();

            //generate stores
            GenerateStores();

            //spawns the boss room as far from the start as possible
            GenerateBossRoom();
        }


        /// <summary>
        /// generates new rooms based on a central room (acquired from the first index (0) of the NewRoomsCoordinates list), it checks both the currentRoomHallways of the current room and checks if the new rooms actually have connecting currentRoomHallways to the current room.
        /// </summary>
        void GenerateRooms()
        {
            //gets the first room in the to/do list of rooms and saves its coordinates
            int[] currentRoomCoordinates = newRoomsCoordinates[0];
            bool[] currentRoomHallways = dungeon[currentRoomCoordinates[0], currentRoomCoordinates[1]].GetHallways();

            for (int hallwayDirection = 0; hallwayDirection < 4; hallwayDirection++)
            {
                int invertedHallwaydirection = (hallwayDirection + 2) % 4;

                //checks if the current hallwayDirection actually has a hallway, if not then fully skip this all
                if (currentRoomHallways[hallwayDirection] == true)
                {
                    //sets newRoomCoordinates based on in what hallwayDirection the current room it's checking is
                    int[] newRoomCoords = GetNewRoomCoordinates(currentRoomCoordinates, hallwayDirection);

                    //check if coord is within bounds range, if within bounds then run this && check if the current location already has a room or not, if there is no room then generate one
                    if (CheckDungeonBounds(newRoomCoords) && dungeon[newRoomCoords[0], newRoomCoords[1]] == null)
                    {
                        //generate a new room, checking if it is properly connecting to new room
                        do { dungeon[newRoomCoords[0], newRoomCoords[1]] = new Room(asciiArtHallRooms); }
                        while (dungeon[newRoomCoords[0], newRoomCoords[1]].GetHallways()[invertedHallwaydirection] == false);

                        //adds the room to the list of new rooms to itterate through
                        newRoomsCoordinates.Add(newRoomCoords);
                        generatedRoomsCount++;
                    }

                }

                //removes the current room from the list
                newRoomsCoordinates.Remove(currentRoomCoordinates);
            }
        }
        /// <summary>
        /// generates a new coordinate based on the direction you are going from the current room
        /// </summary>
        /// <param name="pCurrentRoomCoordinates"></param>
        /// <param name="pHallwayDirection"></param>
        /// <returns></returns>
        int[] GetNewRoomCoordinates(int[] pCurrentRoomCoordinates, int pHallwayDirection)
        {
            switch (pHallwayDirection)
            {
                //top
                case 0:
                    return new int[2] { pCurrentRoomCoordinates[0] - 1, pCurrentRoomCoordinates[1] };
                //right
                case 1:
                    return new int[2] { pCurrentRoomCoordinates[0], pCurrentRoomCoordinates[1] + 1 };
                //bottom
                case 2:
                    return new int[2] { pCurrentRoomCoordinates[0] + 1, pCurrentRoomCoordinates[1] };
                //right
                case 3:
                    return new int[2] { pCurrentRoomCoordinates[0], pCurrentRoomCoordinates[1] - 1 };
                //should never happen, but here to stop method from being annoyed
                default:
                    return new int[2] { 0, 0 };
            }
        }



        //generate unique rooms
        void CreateStartingRoom(int pDungeonDimensions)
        {
            //creates the starting room to grow the dungeon from at the center of the grid
            int[] dungeonCenter = { pDungeonDimensions / 2, pDungeonDimensions / 2 };

            bool[] roomLayout = { true, true, true, true };
            dungeon[dungeonCenter[0], dungeonCenter[1]] = new EntranceRoom(asciiArtHallRooms, roomLayout);

            //saves the starting location of this dungeon
            startingRoomCoordinates = dungeonCenter;
            newRoomsCoordinates.Add(startingRoomCoordinates);
            generatedRoomsCount++;
        }
        void GenerateBossRoom()
        {
            bool isBossRoomGenerated = false;

            //get random pattern to find spot to generate boss room
            switch (random.Next(0, 4))
            {
                case 0:
                    for (int vertical = 0; vertical < dungeon.GetLength(0); vertical++)
                    {
                        for (int horizontal = 0; horizontal < dungeon.GetLength(1); horizontal++)
                        {
                            if (!isBossRoomGenerated && dungeon[vertical, horizontal] != null)
                            {

                                bool[] hallways = dungeon[vertical, horizontal].GetHallways();

                                bossRoomCoordinates = new int[] { vertical, horizontal };
                                dungeon[vertical, horizontal] = new BossRoom(asciiArtHallRooms, hallways);
                                isBossRoomGenerated = true;
                            }
                        }
                    }
                    break;

                case 1:
                    for (int vertical = dungeon.GetLength(0) - 1; vertical >= 0; vertical--)
                    {
                        for (int horizontal = dungeon.GetLength(1) - 1; horizontal >= 0; horizontal--)
                        {
                            if (!isBossRoomGenerated && dungeon[vertical, horizontal] != null)
                            {

                                bool[] hallways = dungeon[vertical, horizontal].GetHallways();

                                bossRoomCoordinates = new int[] { vertical, horizontal };
                                dungeon[vertical, horizontal] = new BossRoom(asciiArtHallRooms, hallways);
                                isBossRoomGenerated = true;
                            }
                        }
                    }
                    break;

                case 2:
                    for (int vertical = dungeon.GetLength(0) - 1; vertical >= 0; vertical--)
                    {
                        for (int horizontal = 0; horizontal < dungeon.GetLength(1); horizontal++)
                        {
                            if (!isBossRoomGenerated && dungeon[vertical, horizontal] != null)
                            {

                                bool[] hallways = dungeon[vertical, horizontal].GetHallways();

                                bossRoomCoordinates = new int[] { vertical, horizontal };
                                dungeon[vertical, horizontal] = new BossRoom(asciiArtHallRooms, hallways);
                                isBossRoomGenerated = true;
                            }
                        }
                    }
                    break;

                case 3:
                    for (int vertical = 0; vertical < dungeon.GetLength(0); vertical++)
                    {
                        for (int horizontal = dungeon.GetLength(1) - 1; horizontal >= 0; horizontal--)
                        {
                            if (!isBossRoomGenerated && dungeon[vertical, horizontal] != null)
                            {

                                bool[] hallways = dungeon[vertical, horizontal].GetHallways();

                                bossRoomCoordinates = new int[] { vertical, horizontal };
                                dungeon[vertical, horizontal] = new BossRoom(asciiArtHallRooms, hallways);
                                isBossRoomGenerated = true;
                            }
                        }
                    }
                    break;
            }
        }
        void GenerateStores()
        {
            //amount of wanted stores
            int storeAmount = 3;

            //generate stores until 3 get generated
            do
            {
                //create random position
                int[] pos = { random.Next(0, dungeon.GetLength(0)), random.Next(0, dungeon.GetLength(1)) };

                //check if room is a room
                if (dungeon[pos[0], pos[1]] != null)
                {
                    //copy the hallways pattern from the original room occupying this space
                    bool[] hallways = dungeon[pos[0], pos[1]].GetHallways();

                    //create store replacing original room with correct hallways
                    dungeon[pos[0], pos[1]] = new StoreRoom(asciiArtHallRooms, hallways);

                    //stores one of the store room coordinates
                    storeRoomCoordinates = new int[] { pos[0], pos[1] };

                    storeAmount--;
                }

            } while (storeAmount != 0);
        }


        void RemoveStrayHallways()
        {
            //goes through the entire grid
            for (int vertical = 0; vertical < dungeon.GetLength(0); vertical++)
            {
                for (int horizontal = 0; horizontal < dungeon.GetLength(1); horizontal++)
                {
                    //checks if the current room is actually a room
                    if (dungeon[vertical, horizontal] != null)
                    {
                        //get the current rooms hallways
                        bool[] currentRoomHallways = dungeon[vertical, horizontal].GetHallways();

                        //goes through all the hallways in this room
                        for (int hallwayDirection = 0; hallwayDirection < 4; hallwayDirection++)
                        {
                            //save the inversion of the hallway (to check connected rooms hallways)
                            int invertedHallwaydirection = (hallwayDirection + 2) % 4;

                            //checks if the current hallwayDirection actually has a hallway, if not then fully skip this
                            if (currentRoomHallways[hallwayDirection] == true)
                            {
                                //create coordinates based on the side it's currently checking
                                int[] newRoomCoords = GetNewRoomCoordinates(new int[] { vertical, horizontal }, hallwayDirection);
                                if (!CheckDungeonBounds(newRoomCoords))
                                {
                                    currentRoomHallways[hallwayDirection] = false;
                                }
                                else if (dungeon[newRoomCoords[0], newRoomCoords[1]].GetHallways()[invertedHallwaydirection] == false)
                                {
                                    currentRoomHallways[hallwayDirection] = false;
                                }
                            }
                        }

                        dungeon[vertical, horizontal].OverwriteHallwayLayout(asciiArtHallRooms, currentRoomHallways);
                    }
                }
            }
        }

        bool CheckDungeonBounds(int[] coords) => coords[0] >= 0 && coords[0] < dungeon.GetLength(0) && coords[1] >= 0 && coords[1] < dungeon.GetLength(1);




        public int[] GetStartingRoomCoordinates() => startingRoomCoordinates;
        public int[] GetBossRoomCoordinates() => bossRoomCoordinates;
        public int[] GetStoreRoomCoordinates() => storeRoomCoordinates;

        public bool GetIsDungeonCleared() => isDungeonCleared;
        public int GetDungeonLevel() => dungeonLevel;

        public TemplateRoom GetRoomObject(int[] pPosition) => dungeon[pPosition[0], pPosition[1]];
        public TemplateRoom[,] GetDungeon() => dungeon;




        public void SetIsDungeonCleared(bool pIsDungeonCleared) => isDungeonCleared = pIsDungeonCleared;

    }
}
