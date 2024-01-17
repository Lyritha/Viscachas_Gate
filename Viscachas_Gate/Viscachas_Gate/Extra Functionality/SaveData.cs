using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Viscachas_Gate
{
    [Serializable]
    internal class SaveData
    {
        string saveFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Save");

        /// <summary>
        /// Save open world data to a specific file
        /// </summary>
        /// <param name="pOpenWorld"></param>
        /// <param name="fileName"></param>
        public void SaveOpenWorld(OpenWorld pOpenWorld, string fileName)
        {
            // Check if the folder exists; if not, create it
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, pOpenWorld);
            }
        }

        /// <summary>
        /// Load open world data from a specific file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public OpenWorld LoadOpenWorld(string fileName)
        {
            OpenWorld openWorld;

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                openWorld = (OpenWorld)binaryFormatter.Deserialize(fileStream);
            }

            return openWorld;
        }



        /// <summary>
        /// Save player data to a specific file
        /// </summary>
        /// <param name="pOpenWorld"></param>
        /// <param name="fileName"></param>
        public void SavePlayer(Player pPlayer, string fileName)
        {
            // Check if the folder exists; if not, create it
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, pPlayer);
            }
        }

        /// <summary>
        /// Load player data from a specific file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Player LoadPlayer(string fileName)
        {   
            Player player;

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                player = (Player)binaryFormatter.Deserialize(fileStream);
            }

            return player;
        }



        /// <summary>
        /// Save dungeon data to a specific file
        /// </summary>
        /// <param name="pOpenWorld"></param>
        /// <param name="fileName"></param>
        public void SaveDungeon(Dungeon pDungeon, string fileName)
        {
            // Check if the folder exists; if not, create it
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, pDungeon);
            }
        }

        /// <summary>
        /// Load dungeon data from a specific file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Dungeon LoadDungeon(string fileName)
        {
            Dungeon dungeon; 

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                dungeon = (Dungeon)binaryFormatter.Deserialize(fileStream);
            }

            return dungeon;
        }



        /// <summary>
        /// Save main game data to a specific file
        /// </summary>
        /// <param name="pOpenWorld"></param>
        /// <param name="fileName"></param>
        public void SaveMainGame(MainGame pMainGame, string fileName)
        {
            // Check if the folder exists; if not, create it
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, pMainGame);
            }
        }

        /// <summary>
        /// Load main game data from a specific file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public MainGame LoadMainGame(string fileName)
        {
            MainGame mainGame;

            string filePath = Path.Combine(saveFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                mainGame = (MainGame)binaryFormatter.Deserialize(fileStream);
            }

            return mainGame;
        }
    }
}