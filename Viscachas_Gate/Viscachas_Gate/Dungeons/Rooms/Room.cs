using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viscachas_Gate
{
    [Serializable]
    internal class Room : TemplateRoom
    {
        bool EnemyEncounter = false;
        [NonSerialized]
        Random random = new Random();

        /// <summary>
        /// generates a room with a random amount of hallways, it also gives this room the ascii art that fits the amount and position of these hallways
        /// </summary>
        /// <param name="pAsciiArt"></param>
        public Room(string[] pAsciiArt)
        {
            RandomHallwayLayout(pAsciiArt);
            EnemyEncounter = (random.Next(0, 2) == 0) ? true : false;
        }

        public bool GetEnemyEncounter() => EnemyEncounter;

        public void SetEnemyEncounter(bool pEnemyEncounter) => EnemyEncounter = pEnemyEncounter; 
    }
}
