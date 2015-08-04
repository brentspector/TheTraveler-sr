/*******************************************************************************
 *
 *  File Name: PlayerData.cs
 *
 *  Description: A clean serialisable class for saving and loading player data
 *
 *******************************************************************************/
using System;
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
        string name;            // The player's name
        InterfaceColors color;  // The player's colour
        Vector3 position;       // The player's position

        // Default constructor; Creates an empty object
        public PlayerData()
        {
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
        } // end Reset

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
    } // end PlayerData
} // end GSP.Core
