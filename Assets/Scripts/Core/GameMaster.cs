/*******************************************************************************
 *
 *  File Name: GameMaster.cs
 *
 *  Description: Manages the data transfers and general wrapper for dealing
 *               with the entities of the game
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Items;
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

        static readonly int maxPlayers = 4; // Max number of players
        static int turn;                    // Who's turn it is
        static int numPlayers;              // The current number of players
        
        // The variables here are through dictionaries. The key is the player number.
        Dictionary<int, string> playerNames;            // The list of the players' names
        Dictionary<int, InterfaceColors> playerColors;  // The list of the players' colours
        Dictionary<int, GameObject> playerObjs;         // The list of players' GameObject's
        Dictionary<int, Char.Player> players;           // The list of players' Player scripts references

        List<int> enemyIdentifiers;     // The list of enemy IDs
        List<int> tempAllyIdentifiers;  // A temporary list of identifiers for newly created allies

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

            // Create the lists
            enemyIdentifiers = new List<int>();
            tempAllyIdentifiers = new List<int>();

            // Set the save file information strings
            playerFilePath = Application.persistentDataPath + "/player";
            highScoreFilePath = Application.persistentDataPath + "/highscores";
            saveFileExt = ".sav";

            // Loop over the players to fill the dictionaries with deafult values
            for (int i = 0; i < MaxPlayers; i++)
            {
                // Dictionaries are zero-index based so add one to get the player's number
                int playerNum = i + 1;
                playerNames.Add(playerNum, string.Empty);
                playerColors.Add(playerNum, InterfaceColors.Black);
                playerObjs.Add(playerNum, null);
                players.Add(playerNum, null);
            } // end for
        } // end Awake

        // Fill the dictionaries
        void Start()
        {
            // Initialise the number of players to zero
            numPlayers = 0;

            // Initialise the current turn to one
            turn = 1;
        } // end Start

        // Called when a new level was loaded
        void OnLevelWasLoaded(int level)
        {
            // Reset the containers after a level was loaded to flushout old references
            ResetCollections();
        }

        // Resets the containers
        public void ResetCollections()
        {
            // Loop over the players to reset the dictionaries to deafult values
            for (int i = 0; i < MaxPlayers; i++)
            {
                // Dictionaries are zero-index based so add one to get the player's number
                int playerNum = i + 1;
                playerNames[playerNum] = string.Empty;
                playerColors[playerNum] = InterfaceColors.Black;
                playerObjs[playerNum] = null;
                players[playerNum] = null;
            } // end for

            // Clear the lists
            enemyIdentifiers.Clear();
            tempAllyIdentifiers.Clear();
        } // end ResetCollections

        // Removes an enemy ID from the enemyIdentifiers list
        public void RemoveEnemyIdentifier(int ID)
        {
            int position; // The position in the list the ID is

            // Get the position in the list the ID is
            position = enemyIdentifiers.IndexOf(ID);

            // Check if the ID is valid
            if (position == -1)
            {
                // Simply return
                return;
            } // end if

            // Remove the ID at the found index
            enemyIdentifiers.RemoveAt(position);
        } // end RemoveEnemyIdentifier

        // Clears the ally identifiers list
        public void ClearAllyIdentifiers()
        {
            tempAllyIdentifiers.Clear();
        } // end ClearAllyIdentifiers

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

        // Gets the player's GameObject
        public GameObject GetPlayerObject(int playerNum)
        {
            return playerObjs[playerNum];
        } // end GetPlayerObject

        // Gets the player's scripts
        public Player GetPlayerScript(int playerNum)
        {
            return players[playerNum];
        } // end GetPlayerScript

        #region Create Characters

        #region Create Players

        // Create a new player
        public void CreatePlayer(int playerNum, bool isDataOnly = false)
        {
            Vector3 startPos = startingPos; // The starting position
            int entID = -1;                 // The ID of the created entity

            // Get the starting position
            startPos.y -= ((playerNum + 1) * .64f);

            // Create the player GameObject
            GameObject obj = Instantiate(PrefabReference.prefabPlayer, startingPos, Quaternion.identity) as GameObject;
            obj.transform.localScale = new Vector3(1, 1, 1);

            // Name the player in the editor for convienience
            obj.name = GetPlayerName(playerNum);

            // Check if players should be created for data only; that is without a SpriteRenderer
            if (isDataOnly)
            {
                // Destroy the SpriteRenderer component
                Destroy(playerObjs[playerNum].GetComponent<SpriteRenderer>());
            }

            // Create the merchant entity
            Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Merchant, obj, playerNum);

            // Set the game obj for the given player
            playerObjs[playerNum] = obj;

            // Set the player script for the given player
            players[playerNum] = playerObjs[playerNum].GetComponent<Char.Player>();

            // Give the player script the ID for the merchant
            players[playerNum].GetMerchant(entID);
        } // end CreatePlayer

        // Create a new player and load their settings
        public void CreateAndLoadPlayer(int playerNum, bool isDataOnly = false)
        {
            // First load the player's settings
            LoadPlayer(playerNum);

            // Then create the player
            CreatePlayer(playerNum, isDataOnly);
        } // end CreateAndLoadPlayer

        // Create new players
        public void CreatePlayers(bool isDataOnly = false)
        {
            // Loop over the dictionary to create each player; We use the player name dictionary here
            foreach (var player in playerNames)
            {
                // Check if the player is playing
                if (player.Key < numPlayers)
                {
                    // Create the current player
                    CreatePlayer(player.Key);
                } // end if
            } // end foreach
        } // end CreatePlayers

        // Create new players and load their settings
        public void CreateAndLoadPlayers(bool isDataOnly = false)
        {
            // Loop over the dictionary to create each player; We use the player name dictionary here
            foreach (var player in playerNames)
            {
                // Check if the player is playing
                if (player.Key < numPlayers)
                {
                    // Create the current player
                    CreateAndLoadPlayer(player.Key, isDataOnly);
                } // end if
            } // end foreach
        } // end CreateAndLoadPlayers

        #endregion

        #region Create Enemies

        // Create a new enemy
        public void CreateEnemy(Entities.HostileType enemyType, string enemyName)
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

                        // Name the GameObject in the editor for convienience sake
                        obj.name = enemyName;

                        // Create the enemy entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Bandit, obj);

                        // Name the enemy entity
                        Entities.EntityManager.Instance.GetEntity(entID).Name = enemyName;

                        // Add the ID to the enemyIdentifiers list
                        enemyIdentifiers.Add(entID);

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

                        // Name the GameObject in the editor for convienience sake
                        obj.name = enemyName;

                        // Create the enemy entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Mimic, obj);

                        // Name the enemy entity
                        Entities.EntityManager.Instance.GetEntity(entID).Name = enemyName;
                        
                        // Add the ID to the enemyIdentifiers list
                        enemyIdentifiers.Add(entID);

                        // Give the enemy script the ID for the enemy
                        script.GetEnemy(entID);

                        break;
                    } // end case Mimic
            } // end switch enemyType
        } // end CreateEnemy

        #endregion

        #region Create Allies

        // Create a new ally
        public void CreateAlly(Entities.FriendlyType allyType, string allyName)
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

                        // Name the GameObject in the editor for convienience sake
                        obj.name = allyName;

                        // Add the ResourceList script
                        obj.AddComponent<Char.ResourceList>();

                        // Create the ally entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Porter, obj);

                        // Name the ally entity
                        Entities.EntityManager.Instance.GetEntity(entID).Name = allyName;

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

                        // Name the GameObject in the editor for convienience sake
                        obj.name = allyName;

                        // Create the ally entity
                        Entities.EntityManager.Instance.Generator.CreateEntity(out entID, Entities.EntityType.Mercenary, obj);

                        // Name the ally entity
                        Entities.EntityManager.Instance.GetEntity(entID).Name = allyName;

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
                // Check if the player is playing
                if (player.Key < numPlayers)
                {
                    // Save the current player
                    SavePlayer(player.Key);
                } // end if
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
                // Check if the player is playing
                if (player.Key < numPlayers)
                {
                    // Save the current player
                    LoadPlayer(player.Key);
                } // end if
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

        #region Create Items

        // Note: The cost of all items is default to 1. This can be changed later
        
        // Creates and returns a weapon object
        public Weapon CreateWeapon(WeaponType weaponType)
        {
            Weapon weapon = null;   // The created weapon
            
            // Switch over the weaponType for the correct weapon
            switch (weaponType)
            {
                case WeaponType.Broadsword:
                    {
                        weapon = new Weapon("Broadsword", WeaponType.Broadsword, 9, 1);
                        break;
                    } // end case Broadsword
                case WeaponType.Mace:
                    {
                        weapon = new Weapon("Mace", WeaponType.Mace, 7, 1);
                        break;
                    } // end case Mace
                case WeaponType.Spear:
                    {
                        weapon = new Weapon("Spear", WeaponType.Spear, 8, 1);
                        break;
                    } // end case Spear
                case WeaponType.Sword:
                    {
                        weapon = new Weapon("Sword", WeaponType.Sword, 5, 1);
                        break;
                    } // end case Sword
            } // end switch weaponType

            // Finally return the weapon
            return weapon;
        } // end CreateWeapon

        // Creates and returns an armour object
        public Armor CreateArmor(ArmorType armorType)
        {
            Armor armor = null;   // The created armor

            // Switch over the armorType for the correct armour
            switch (armorType)
            {
                case ArmorType.Chainlegs:
                    {
                        armor = new Armor("Chainlegs", ArmorType.Chainlegs, 2, 1);
                        break;
                    } // end case Chainlegs
                case ArmorType.Chainmail:
                    {
                        armor = new Armor("Chainmail", ArmorType.Chainmail, 5, 1);
                        break;
                    } // end case Chainmail
                case ArmorType.Fullsuit:
                    {
                        armor = new Armor("Fullsuit", ArmorType.Fullsuit, 11, 1);
                        break;
                    } // end case Fullsuit
                case ArmorType.Platebody:
                    {
                        armor = new Armor("Platebody", ArmorType.Platebody, 8, 1);
                        break;
                    } // end case Platebody
                case ArmorType.Platelegs:
                    {
                        armor = new Armor("Platelegs", ArmorType.Platelegs, 3, 1);
                        break;
                    } // end case Platelegs
            } // end switch armorType

            // Finally return the armour
            return armor;
        } // end CreateArmor

        // Creates and returns a bonus object
        public Bonus CreateBonus(BonusType bonusType)
        {
            Bonus bonus = null;   // The created bonus

            // Switch over the bonusType for the correct bonus
            switch (bonusType)
            {
                case BonusType.RubberBoots:
                    {
                        bonus = new Bonus("RubberBoots", BonusType.RubberBoots, 0, 10, 1);
                        break;
                    } // end case RubberBoots
                case BonusType.Sachel:
                    {
                        bonus = new Bonus("Sachel", BonusType.Sachel, 3, 0, 1);
                        break;
                    } // end case Sachel
            } // end switch bonusType

            // Finally return the bonus
            return bonus;
        } // end CreateBonus

        #endregion

        // Switches turns through the number of players and returns the current turn
        public int NextTurn()
        {
            // Its now the next players turn
            turn++;

            // Loop back to player 1 if we reached the end of the players turns
            if (turn > numPlayers)
            {
                // The player's list is zero-index based so setting to zero is player 1.
                turn = 1;
            }

            // Return the turn
            return turn;
        } // end NextTurn
        
        // Gets who's turn it is
        public int Turn
        {
            get { return turn; }
        } // end Turn

        // Gets the max number of players allowed
        public int MaxPlayers
        {
            get { return maxPlayers; }
        } // end MaxPlayers

        // Gets the current number of players
        public int NumPlayers
        {
            get { return numPlayers; }
            set { numPlayers = Utility.ClampInt(value, 1, MaxPlayers); }
        } // end NumPlayers

        // Gets a copy of the enemyIdentifiers list
        public List<int> EnemyIdentifiers
        {
            get
            {
                // Create a copy of the list
                List<int> tmp = new List<int>(enemyIdentifiers);

                // Return the copy
                return tmp;
            } // end get
        } // end EnemyIdentifiers

        // Gets a copy of the tempAllyIdentifiers list
        public List<int> TempAllyIdentifiers
        {
            get
            {
                // Create a copy of the list
                List<int> tmp = new List<int>(tempAllyIdentifiers);

                // Return the copy
                return tmp;
            } // end get
        } // end AllyIdentifiers
    } // end GameMaster
} // end GSP.Core
