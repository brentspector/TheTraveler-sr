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

        // Max number of players.
        static readonly int m_maxPlayers = 4;
        
        // The variables here are through dictionaries. The key is the player number.
        Dictionary<int, string> m_playerNames;
        Dictionary<int, PlayerColours> m_PlayerColours;

        // Initialise the dictionaries.
        public override void Awake()
        {
            // Call the awake function of the base class.
            base.Awake();
            
            m_playerNames = new Dictionary<int, string>();
            m_PlayerColours = new Dictionary<int, PlayerColours>();

            // Set the save file information strings.
            m_playerFilePath = Application.persistentDataPath + "/player";
            m_highScoreFilePath = Application.persistentDataPath + "/highscores";
            m_saveFileExt = ".sav";
        }

        // Fill the dictionaries.
        void Start()
        {
            for (int i = 1; i < MaxPlayers + 1; i++)
            {
                m_playerNames.Add(i, "");
                m_PlayerColours.Add(i, PlayerColours.COL_BLACK);
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
