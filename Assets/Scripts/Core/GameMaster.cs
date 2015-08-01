/*******************************************************************************
 *
 *  File Name: GameMaster.cs
 *
 *  Description: Manages the data transfers and general wrapper for dealing
 *               with the entities of the game
 *
 *******************************************************************************/
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GSP.Core
{
    /*******************************************************************************
     *
     * Name: GameMaster
     * 
     * Description: Oversees the data and such between scene loads and overall.
     * 
     *******************************************************************************/
    public class GameMaster : BaseSingleton<GameMaster>
    {
        static string playerFilePath;       // The player's save file prefix
        static string highScoreFilePath;    // The highscores save file
        static string saveFileExt;          // The save file's extension

        // The first tile
        static Vector3 startingPos = new Vector3(.32f, -(GSP.Tiles.TileManager.MaxHeightUnits / 2.0f), -1.6f);

        static readonly int m_maxPlayers = 4;   // Max number of players
        
        // The variables here are through dictionaries. The key is the player number.
        Dictionary<int, string> playerNames;            // The list of the players' names
        Dictionary<int, InterfaceColors> playerColors;  // The list of the players' colours
        Dictionary<int, GameObject> playerObjs;         // The list of players' GameObject's
        Dictionary<int, Char.Player> players;           // The list of players' Player scripts references

        // Initialise the dictionaries
        public override void Awake()
        {
            // Call the parent's Awake() first
            base.Awake();
            
            // Create the dictionaries
            playerNames = new Dictionary<int, string>();
            playerColors = new Dictionary<int, InterfaceColors>();
            playerObjs = new Dictionary<int, GameObject>();
            players = new Dictionary<int, Char.Player>();

            // Set the save file information strings
            playerFilePath = Application.persistentDataPath + "/player";
            highScoreFilePath = Application.persistentDataPath + "/highscores";
            saveFileExt = ".sav";
        } // end Awake

        // Fill the dictionaries
        void Start()
        {
            // Loop over the players to fill the dictionaries with deafult values
            for (int i = 0; i < MaxPlayers; i++)
            {
                // Dictionaries are zero-index based so add one to get the player's number
                int playerNum = i + 1;
                playerNames.Add(playerNum, "");
                playerColors.Add(playerNum, InterfaceColors.Black);
                playerObjs.Add(playerNum, null);
                players.Add(playerNum, null);
            } // end for
        } // end Start

        // Gets the player's name with the given key
        public string GetPlayerName(int playerNum)
        {
            return playerNames[playerNum];
        } // end GetPlayerName

        // Sets the player's name with the given key
        public void SetPlayerName(int playerNum, string playerName)
        {
            playerNames[playerNum] = playerName;
        } //end SetPlayerName

        // Gets the player's colour with the given key
        public InterfaceColors GetPlayerColor(int playerNum)
        {
            return playerColors[playerNum];
        } //end GetPlayerColor

        // Sets the player's colour with the given key
        public void SetPlayerColor(int playerNum, InterfaceColors playerColor)
        {
            playerColors[playerNum] = playerColor;
        } // end SetPlayerColor

        #region Create Characters

        #region Create Players

        // Create a new player
        public void CreatePlayer(int playerNum)
        {
            Vector3 startPos = startingPos; // The starting position
            int entID = -1;                 // The ID of the created entity.

            // Get the starting position
            startPos.y -= ((playerNum + 1) * .64f);

            // Create the player GameObject
            GameObject obj = Instantiate(PrefabReference.prefabPlayer, startingPos, Quaternion.identity) as GameObject;
            obj.transform.localScale = new Vector3(1, 1, 1);

            // Create the merchant entity
            Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Merchant, obj, playerNum);

            // Set the game obj for the given player
            playerObjs[playerNum] = obj;

            // Set the player script for the given player
            players[playerNum] = playerObjs[playerNum].GetComponent<Char.Player>();

            // Give the player script the ID for the merchant
            players[playerNum].GetMerchant(entID);
        } // end CreatePlayer

        // Create a new player and load their settings.
        public void CreateAndLoadPlayer(int playerNum)
        {
            // First load the player's settings
            LoadPlayer(playerNum);

            // Then create the player
            CreatePlayer(playerNum);
        } // end CreateAndLoadPlayer

        // Create new players.
        public void CreatePlayers()
        {
            // Loop over the dictionary to create each player; We use the player name dictionary here
            foreach (var player in playerNames)
            {
                // Create the current player
                CreatePlayer(player.Key);
            } // end foreach
        } // end CreatePlayers

        // Create new players and load their settings
        public void CreateAndLoadPlayers()
        {
            // Loop over the dictionary to create each player; We use the player name dictionary here
            foreach (var player in playerNames)
            {
                // Create the current player
                CreateAndLoadPlayer(player.Key);
            } // end foreach
        } // end CreateAndLoadPlayers

        #endregion

        #region Create Enemies

        // Create a new enemy
        public void CreateEnemy(Entities.HostileType enemyType)
        {
            // Create the enemy GameObject
            GameObject obj = Instantiate(PrefabReference.prefabEnemy) as GameObject;

            int entID = -1;  // The ID of the created enemy

            // Switch over the enemy types
            switch (enemyType)
            {
                case Entities.HostileType.Bandit:
                    {
                        // Make sure the ID is -1 before starting
                        entID = -1;

                        // Add the bandit enemy script
                        var script = obj.AddComponent<Char.Enemies.BanditMB>();

                        // Create the enemy entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Bandit, obj);

                        // Give the enemy script the ID for the enemy
                        script.GetEnemy(entID);

                        break;
                    } // end case Bandit
                case Entities.HostileType.Mimic:
                    {
                        // Make sure the ID is -1 before starting
                        entID = -1;

                        // Add the mimic enemy script
                        var script = obj.AddComponent<Char.Enemies.BanditMB>();

                        // Create the enemy entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Mimic, obj);

                        // Give the enemy script the ID for the enemy
                        script.GetEnemy(entID);

                        break;
                    } // end case Mimic
            } // end switch enemyType
        } // end CreateAlly

        #endregion

        #region Create Allies

        // Create a new ally
        public void CreateAlly(Entities.FriendlyType allyType)
        {
            // Create the ally GameObject
            GameObject obj = Instantiate(PrefabReference.prefabAlly) as GameObject;

            int entID = -1;  // The ID of the created ally

            // Switch over the ally types
            switch (allyType)
            {
                case Entities.FriendlyType.Porter:
                    {
                        // Make sure the ID is -1 before starting
                        entID = -1;

                        // Add the porter ally script
                        var script = obj.AddComponent<Char.Allies.PorterMB>();

                        // Create the ally entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Porter, obj);

                        // Give the ally script the ID for the ally
                        script.GetAlly(entID);

                        break;
                    } // end case Porter
                case Entities.FriendlyType.Mercenary:
                    {
                        // Make sure the ID is -1 before starting
                        entID = -1;

                        // Add the mercenary ally script
                        var script = obj.AddComponent<Char.Allies.MercenaryMB>();

                        // Create the ally entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Mercenary, obj);

                        // Give the ally script the ID for the ally
                        script.GetAlly(entID);

                        break;
                    } // end case Mercenary
            } // end allyType
        } // end CreateAlly

        #endregion

        #endregion

        #region Save and Load

        // Saves a player with the given key
        public void SavePlayer(int playerNum)
        {
            // The player's personal save path
            string playerSavePath = playerFilePath + playerNum + saveFileExt;
            
            // Create a new binary formatter to save to a binary file
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Declare the filestream for the file
            FileStream fileStream;

            // Check if the player's save file exists
            if (File.Exists(playerFilePath))
            {
                // The file exists so open the file
                fileStream = File.Open(playerSavePath, FileMode.Open, FileAccess.ReadWrite);
            } // end if
            else
            {
                // Otherwise the file doesn't exist so create it
                fileStream = File.Create(playerSavePath);
            } // end else

            // Create a new player data instance
            PlayerData playerData = new PlayerData();

            // Set the player's name
            playerData.Name = GetPlayerName(playerNum);
            
            // Set the player's colour
            playerData.Color = GetPlayerColor(playerNum);

            // Now write the data to the file
            binaryFormatter.Serialize(fileStream, playerData);

            // Finally, close the file stream
            fileStream.Close();
        } // end Save Player

        // Saves all players
        public void SavePlayers()
        {
            // Loop over the dictionary to save each player; We use the player name dictionary here
            foreach (var player in playerNames)
            {
                // Save the current player
                SavePlayer(player.Key);
            } // end foreach
        } // end SavePlayers

        // Loads a player with the given key
        public void LoadPlayer(int playerNum)
        {
            // The player's personal save path
            string playerSavePath = playerFilePath + playerNum + saveFileExt;
            
            // Make sure the player's save file exists before trying to load it
            if (File.Exists(playerSavePath))
            {
                // Create a new binary formatter to load the binary file
                BinaryFormatter binaryFormater = new BinaryFormatter();

                // Open a file stream while opening the file
                FileStream fileStream = File.Open(playerSavePath, FileMode.Open);

                // Create a player data instance from the file
                PlayerData playerData = (PlayerData)binaryFormater.Deserialize(fileStream);

                // Now close the file stream
                fileStream.Close();

                // Load the player's name
                SetPlayerName(playerNum, playerData.Name);

                // Load the player's colour
                SetPlayerColor(playerNum, playerData.Color);
            } // end if
        } // end LoadPlayer

        // Loads all players
        public void LoadPlayers()
        {
            // Loop over the dictionary to load each player; We use the player name dictionary here
            foreach (var player in playerNames)
            {
                // Save the current player
                LoadPlayer(player.Key);
            } // end foreach
        } // end LoadPlayers

        // Save the highscore table
        public void SaveHighScores()
        {
            // The full high scores save path
            string highScoresSavePath = highScoreFilePath + saveFileExt;

            // Create a new binary formatter to save to a binary file
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Declare the filestream for the file
            FileStream fileStream;

            // Check if the player's save file exists
            if (File.Exists(highScoresSavePath))
            {
                // The file exists so open the file
                fileStream = File.Open(highScoresSavePath, FileMode.Open, FileAccess.ReadWrite);
            } // end if
            else
            {
                // Otherwise the file doesn't exist so create it
                fileStream = File.Create(highScoresSavePath);
            } // end else

            // Create a new high scores instance
            HighScores highScores = new HighScores();
            
            /*
             * Fill in the high scores stuff to save
             */

            // Now write the data to the file
            binaryFormatter.Serialize(fileStream, highScores);

            // Finally, close the file stream
            fileStream.Close();
        } // end SaveHighScores

        // Load the highscore table
        public void LoadHighScores()
        {
            // The full high scores save path
            string highScoresSavePath = highScoreFilePath + saveFileExt;
            
            // Make sure the high score's save file exists before trying to load it
            if (File.Exists(highScoresSavePath))
            {
                // Create a new binary formatter to load the binary file
                BinaryFormatter binaryFormater = new BinaryFormatter();

                // Open a file stream while opening the file
                FileStream fileStream = File.Open(highScoresSavePath, FileMode.Open);

                // Create a high scores instance from the file
                HighScores highScores = (HighScores)binaryFormater.Deserialize(fileStream);

                // Now close the file stream
                fileStream.Close();

                /*
                 * Load high scores stuff here
                 */
            } // end if
        } // end LoadHighScores

        #endregion

        // Gets the max number of players allowed
        public int MaxPlayers
        {
            get { return m_maxPlayers; }
        } // end MaxPlayers
    } // end GameMaster
} // end GSP.Core
