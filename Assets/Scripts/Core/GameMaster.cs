using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GSP.Core
{
    public class GameMaster : BaseSingleton<GameMaster>
    {
        // Save file information.
        static string m_playerFilePath;
        static string m_highScoreFilePath;
        static string m_saveFileExt;

        // The first tile.
        static Vector3 startingPos = new Vector3(.32f, -(GSP.Tiles.TileManager.MaxHeightUnits / 2.0f), -1.6f);

        // Max number of players.
        static readonly int m_maxPlayers = 4;
        
        // The variables here are through dictionaries. The key is the player number.
        Dictionary<int, string> m_playerNames;
        Dictionary<int, PlayerColours> m_PlayerColours;
        Dictionary<int, GameObject> m_playerObjs;
        Dictionary<int, Char.Player> m_players;

        // Initialise the dictionaries.
        public override void Awake()
        {
            // Call the parent's awake first.
            base.Awake();
            
            // Create the dictionaries.
            m_playerNames = new Dictionary<int, string>();
            m_PlayerColours = new Dictionary<int, PlayerColours>();
            m_playerObjs = new Dictionary<int, GameObject>();
            m_players = new Dictionary<int, Char.Player>();

            // Set the save file information strings.
            m_playerFilePath = Application.persistentDataPath + "/player";
            m_highScoreFilePath = Application.persistentDataPath + "/highscores";
            m_saveFileExt = ".sav";
        }

        // Fill the dictionaries.
        void Start()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                int playerNum = i + 1;
                m_playerNames.Add(playerNum, "");
                m_PlayerColours.Add(playerNum, PlayerColours.COL_BLACK);
                m_playerObjs.Add(playerNum, null);
                m_players.Add(playerNum, null);
            }
        }

        // Gets the max number of players allowed.
        public int MaxPlayers
        {
            get
            {
                return m_maxPlayers;
            }
        }

        // Gets the player's name with the given key.
        public string GetPlayerName(int playerNum)
        {
            return m_playerNames[playerNum];
        }

        // Sets the player's name with the given key.
        public void SetPlayerName(int playerNum, string playerName)
        {
            m_playerNames[playerNum] = playerName;
        }

        // Gets the player's colour with the given key.
        public PlayerColours GetPlayerColour(int playerNum)
        {
            return m_PlayerColours[playerNum];
        }

        // Sets the player's colour with the given key.
        public void SetPlayerColour(int playerNum, PlayerColours playerColour)
        {
            m_PlayerColours[playerNum] = playerColour;
        }

        #region Create Characters

        #region Create Players

        // Create a new player.
        public void CreatePlayer(int playerNum)
        {
            Vector3 startPos = startingPos;
            // Get the starting position.
            startPos.y -= ((playerNum + 1) * .64f);
            // Create the player game object.
            GameObject obj = Instantiate(PrefabReference.prefabPlayer, startingPos, Quaternion.identity) as GameObject;
            obj.transform.localScale = new Vector3(1, 1, 1);

            // Holds the ID of the created entity.
            int entID = -1;

            // Create the merchant entity.
            Entities.EntityManager.Instance.GetEntityGenerator().CreateEntity(out entID, Entities.EntityType.ENT_MERCHANT, obj, playerNum);

            // Set the game obj for the given player.
            m_playerObjs[playerNum] = obj;

            // Set the player script for the given player.
            m_players[playerNum] = m_playerObjs[playerNum].GetComponent<Char.Player>();

            // Give the player script the ID for the merchant.
            m_players[playerNum].GetMerchant(entID);
        }

        // Create a new player and load their settings.
        public void CreateAndLoadPlayer(int playerNum)
        {
            //First load the player's settings.
            LoadPlayer(playerNum);

            // Then create the player.
            CreatePlayer(playerNum);
        }

        // Create new players.
        public void CreatePlayers()
        {
            // Loop over the dictionary to create each player. We use the player name dictionary here.
            foreach (var player in m_playerNames)
            {
                // create the current player.
                CreatePlayer(player.Key);
            }
        }

        // Create new players and load their settings.
        public void CreateAndLoadPlayers()
        {
            // Loop over the dictionary to create each player. We use the player name dictionary here.
            foreach (var player in m_playerNames)
            {
                // create the current player.
                CreateAndLoadPlayer(player.Key);
            }
        }

        #endregion

        #region Create Enemies

        // Create a new enemy.
        public void CreateEnemy(Entities.HostileType enemyType)
        {
            // Create the enemy game object.
            GameObject obj = Instantiate(PrefabReference.prefabEnemy) as GameObject;

            int entID = -1;  // Holds the ID of the created enemy.

            switch (enemyType)
            {
                case Entities.HostileType.HT_BANDIT:
                    {
                        // Make sure the ID is zero before starting.
                        entID = -1;

                        // Add the bandit enemy script.
                        var script = obj.AddComponent<Char.Enemies.BanditMB>();

                        // Create the enemy entity.
                        Entities.EntityManager.Instance.GetEntityGenerator().CreateEntity(out entID, Entities.EntityType.ENT_BANDIT, obj);

                        // Give the enemy script the ID for the enemy.
                        script.GetEnemy(entID);

                        break;
                    }
                case Entities.HostileType.HT_MIMIC:
                    {
                        // Make sure the ID is zero before starting.
                        entID = -1;

                        // Add the bandit enemy script.
                        var script = obj.AddComponent<Char.Enemies.BanditMB>();

                        // Create the enemy entity.
                        Entities.EntityManager.Instance.GetEntityGenerator().CreateEntity(out entID, Entities.EntityType.ENT_BANDIT, obj);

                        // Give the enemy script the ID for the enemy.
                        script.GetEnemy(entID);

                        break;
                    }
            }
        }

        #endregion

        #region Create Allies

        // Create a new enemy.
        public void CreateAlly(Entities.FriendlyType allyType)
        {
            // Create the enemy game object.
            GameObject obj = Instantiate(PrefabReference.prefabAlly) as GameObject;

            int entID = -1;  // Holds the ID of the created enemy.

            switch (allyType)
            {
                case Entities.FriendlyType.FT_PORTER:
                    {
                        // Make sure the ID is zero before starting.
                        entID = -1;

                        // Add the porter ally script.
                        var script = obj.AddComponent<Char.Allies.PorterMB>();

                        // Create the enemy entity.
                        Entities.EntityManager.Instance.GetEntityGenerator().CreateEntity(out entID, Entities.EntityType.ENT_PORTER, obj);

                        // Give the ally script the ID for the ally.
                        script.GetAlly(entID);

                        break;
                    }
                case Entities.FriendlyType.FT_MERCENARY:
                    {
                        // Make sure the ID is zero before starting.
                        entID = -1;

                        // Add the mercenary ally script.
                        var script = obj.AddComponent<Char.Allies.MercenaryMB>();

                        // Create the enemy entity.
                        Entities.EntityManager.Instance.GetEntityGenerator().CreateEntity(out entID, Entities.EntityType.ENT_MERCENARY, obj);

                        // Give the ally script the ID for the ally.
                        script.GetAlly(entID);

                        break;
                    }
            }
        }

        #endregion

        #endregion

        #region Save and Load

        // Saves a player with the given key.
        public void SavePlayer(int playerNum)
        {
            // The player's personal save path.
            string playerFilePath = m_playerFilePath + playerNum + m_saveFileExt;
            
            // Create a new binary formatter to save to a binary file.
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Declare the filestream for the file.
            FileStream fileStream;

            // Check if the player's save file exists.
            if (File.Exists(playerFilePath))
            {
                // The file exists so open the file.
                fileStream = File.Open(playerFilePath, FileMode.Open, FileAccess.ReadWrite);
            }
            else
            {
                // Otherwise the file doesn't exist so create it.
                fileStream = File.Create(playerFilePath);
            }

            // Create a new player data instance.
            PlayerData playerData = new PlayerData();

            // Set the player's name.
            playerData.PlayerName = GetPlayerName(playerNum);
            
            // Set the player's colour.
            playerData.PlayerColour = GetPlayerColour(playerNum);

            // Now write the data to the file.
            binaryFormatter.Serialize(fileStream, playerData);

            // Finally, close the file stream.
            fileStream.Close();
        }

        // Loads a player with the given key.
        public void LoadPlayer(int playerNum)
        {
            // The player's personal save path.
            string playerFilePath = m_playerFilePath + playerNum + m_saveFileExt;
            
            // Make sure the player's save file exists before trying to load it.
            if (File.Exists(playerFilePath))
            {
                // Create a new binary formatter to load the binary file.
                BinaryFormatter binaryFormater = new BinaryFormatter();

                // Open a file stream while opening the file.
                FileStream fileStream = File.Open(playerFilePath, FileMode.Open);

                // Create a player data instance from the file.
                PlayerData playerData = (PlayerData)binaryFormater.Deserialize(fileStream);

                // Now close the file stream.
                fileStream.Close();

                // Load the player's name.
                SetPlayerName(playerNum, playerData.PlayerName);

                // Load the player's colour.
                SetPlayerColour(playerNum, playerData.PlayerColour);
            }
        }

        // Saves all players.
        public void SavePlayers()
        {
            // Loop over the dictionary to save each player. We use the player name dictionary here.
            foreach (var player in m_playerNames)
            {
                // Save the current player.
                SavePlayer(player.Key);
            }
        }

        // Loads all players.
        public void LoadPlayers()
        {
            // Loop over the dictionary to load each player. We use the player name dictionary here.
            foreach (var player in m_playerNames)
            {
                // Save the current player.
                LoadPlayer(player.Key);
            }
        }

        // Save the highscore table.
        public void SaveHighScores()
        {
            // The full high scores save path.
            string highScoresFilePath = m_highScoreFilePath + m_saveFileExt;

            // Create a new binary formatter to save to a binary file.
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Declare the filestream for the file.
            FileStream fileStream;

            // Check if the player's save file exists.
            if (File.Exists(highScoresFilePath))
            {
                // The file exists so open the file.
                fileStream = File.Open(highScoresFilePath, FileMode.Open, FileAccess.ReadWrite);
            }
            else
            {
                // Otherwise the file doesn't exist so create it.
                fileStream = File.Create(highScoresFilePath);
            }

            // Create a new high scores instance.
            HighScores highScores = new HighScores();
            
            /*
             * Fill in the high scores stuff to save.
             */

            // Now write the data to the file.
            binaryFormatter.Serialize(fileStream, highScores);

            // Finally, close the file stream.
            fileStream.Close();
        }

        // Load the highscore table.
        public void LoadHighScores()
        {
            // The full high scores save path.
            string highScoresFilePath = m_highScoreFilePath + m_saveFileExt;
            
            // Make sure the high score's save file exists before trying to load it.
            if (File.Exists(highScoresFilePath))
            {
                // Create a new binary formatter to load the binary file.
                BinaryFormatter binaryFormater = new BinaryFormatter();

                // Open a file stream while opening the file.
                FileStream fileStream = File.Open(highScoresFilePath, FileMode.Open);

                // Create a high scores instance from the file.
                HighScores highScores = (HighScores)binaryFormater.Deserialize(fileStream);

                // Now close the file stream.
                fileStream.Close();

                /*
                 * Load high scores stuff here.
                 */
            }
        }

        #endregion
    }
}
