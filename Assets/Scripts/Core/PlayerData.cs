/*******************************************************************************
 *
 *  File Name: PlayerData.cs
 *
 *  Description: A clean serialisable class for saving and loading player data
 *
 *******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Core
{
    /*******************************************************************************
     *
     * Name: PlayerData
     * 
     * Description: Container class for a clean version of the serialisable player
     *              data.
     * 
     *******************************************************************************/
    [Serializable]
    public class PlayerData
    {
        string name;                    // The player's name
        InterfaceColors color;          // The player's colour
        SerializableVector3 position;   // The player's position
        int merchantId;                 // The player's merchant entity ID
        int allyId;                     // The player's ally entity ID
        List<int> itemIds;              // The player's list of item IDs in their inventory
        AllyData allyData;              // The player's ally's data

        // Default constructor; Creates an empty object
        public PlayerData()
        {
            // Initialise the list
            itemIds = new List<int>();

            // Reset the container; This prevents the initialisation being in 2 places
            Reset();
        } // end PlayerData

        // Resets the contents of the container
        public void Reset()
        {
            // Set the name to an empty string
            name = string.Empty;

            // Set the colour to black
            color = InterfaceColors.Black;

            // Set the position to be zero.
            position = Vector3.zero;

            // Set the entity ID's to negative one
            merchantId = -1;
            allyId = -1;

            // Clear the list
            itemIds.Clear();

            // Set the ally data to a null state
            allyData = null;
        } // end Reset

        // Gets an itemId at the given index
        public int GetItemId(int index)
        {
            return itemIds[index];
        } // end GetItemId

        // Adds an itemId to the list
        public void AddItemId(int itemId)
        {
            itemIds.Add(itemId);
        } // end AddItemId

        // Gets and Sets the player's Name
        public string Name
        {
            get { return name; }
            set { name = value; }
        } // end Name

        // Gets and Sets the player's Color
        public InterfaceColors Color
        {
            get { return color;}
            set { color = value;}
        } // end Color

        // Gets and Sets the player's Position
        public Vector3 Position
        {
            get { return position;}
            set { position = value; }
        } // end Position

        // Gets and Sets the player's Merchant Entity's ID
        public int MerchantId
        {
            get { return merchantId; }
            set { merchantId = value; }
        } // end MerchantId

        // Gets and Sets the player's Ally Entity's ID
        public int AllyId
        {
            get { return allyId; }
            set { allyId = value; }
        } // end AllyId

        // Gets and Sets the player's Ally's data
        public AllyData AllyData
        {
            get { return allyData; }
            set { allyData = value; }
        } // end AllyData
    } // end PlayerData
} // end GSP.Core
