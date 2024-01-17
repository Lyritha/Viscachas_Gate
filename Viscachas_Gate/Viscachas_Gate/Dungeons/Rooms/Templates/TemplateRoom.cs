using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class TemplateRoom
    {
        [NonSerialized]
        Random random = new Random();

        //all possible room layours
        List<bool[]> possibleHallwayLayouts = new List<bool[]>
        {
            new bool[]{true, true, true, true },// full room
            new bool[]{true, true, false, true },// 3-way split
            new bool[]{true, true, true, false },
            new bool[]{true, false, true, true },
            new bool[]{false, true, true, true },
            new bool[]{false, false, true, true },// 2-way split
            new bool[]{true, false, false, true },
            new bool[]{false, true, true, false },
            new bool[]{true, true, false, false },
            new bool[]{false, true, false, true },
            new bool[]{true, false, true, false },
            new bool[]{false, false, true, false },// 1-way room
            new bool[]{false, false, false, true },
            new bool[]{false, true, false, false },
            new bool[]{true, false, false, false },
        };

        //keeps track of all the hallways this room has, clockwise starting at 12 am
        int roomIndex = 0;
        bool[] hallwayLayout = new bool[4];

        //stores the artwork of this room
        string roomArt = "Placeholder";

        /// <summary>
        /// creates a random room layout
        /// </summary>
        /// <param name="pAsciiArt"></param>
        public void RandomHallwayLayout(string[] pAsciiArt)
        {
            //decides current room hallways
            ChooseRandomIndex();
            //asign correct hallway layout
            AssignLayout();
            //asign the correct art to this room
            AssignRoomArt(pAsciiArt);
        }

        /// <summary>
        /// allows you to set your own room layout
        /// </summary>
        /// <param name="pAsciiArt"></param>
        /// <param name="pHallwaysLayout"></param>
        public void OverwriteHallwayLayout(string[] pAsciiArt, bool[] pHallwaysLayout)
        {
            //decides current room hallways
            ChooseCustomIndex(pHallwaysLayout);
            //asign correct hallway layout
            AssignLayout();
            //asign the correct art to this room
            AssignRoomArt(pAsciiArt);
        }

        /// <summary>
        /// generates a random number to use as the room index
        /// </summary>
        void ChooseRandomIndex() => roomIndex = random.Next(0, 15);
        /// <summary>
        /// allows you to set your own index based on a bool array
        /// </summary>
        /// <param name="pHallwaysLayout"></param>
        void ChooseCustomIndex(bool[] pHallwaysLayout)
        {
            //goes through the whole list
            for (int listPosition = 0; listPosition < possibleHallwayLayouts.Count; listPosition++)
            {
                //creates new int
                int arraySimilarity = 0;

                //goes through an array
                for (int boolArrayPosition = 0; boolArrayPosition < possibleHallwayLayouts[listPosition].Length; boolArrayPosition++)
                {
                    //if the current position is equals to eachother then add to similarity
                    if (pHallwaysLayout[boolArrayPosition] == possibleHallwayLayouts[listPosition][boolArrayPosition])
                    { arraySimilarity++; }

                    //if similarity is 4 then return the position
                    if (arraySimilarity == 4)
                    { roomIndex = listPosition; break; }
                }
            }
        }

        /// <summary>
        /// assigns the correct hallway layout for the current room based on its index
        /// </summary>
        void AssignLayout() => hallwayLayout = possibleHallwayLayouts[roomIndex];

        /// <summary>
        /// gives the current room the correct ascii art based on the current rooms hallways (represtented as a 4 long 0-1 sequence)
        /// </summary>
        /// <param name="pAsciiArt"></param>
        void AssignRoomArt(string[] pAsciiArt)
        {
            StringBuilder stringBuilder = new StringBuilder();


            foreach (char c in pAsciiArt[roomIndex])
            {
                if (c == '=')
                {
                   if (random.Next(0, 20) >= 18)
                    {
                        stringBuilder.Append('+');
                    }
                   else
                    {
                        stringBuilder.Append('=');
                    }
                } 
                
                else
                {
                    stringBuilder.Append(c);
                }
            }

            roomArt = stringBuilder.ToString();
        }

        public void SetCustomRoomArt(string pArt) => roomArt = pArt;

        /// <summary>
        /// gets the bool array representing the hallways of this room
        /// </summary>
        /// <returns></returns>
        public bool[] GetHallways() => hallwayLayout;
        /// <summary>
        /// gets the ascii art for the current room layout
        /// </summary>
        /// <returns></returns>
        public string GetRoomArt() => roomArt;
    }
}
