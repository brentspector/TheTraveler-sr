﻿using UnityEngine;
using System.Collections;

namespace GSP.Core
{
    // Container class for a clean version of the serialisable player data.
    [System.Serializable]
    public class PlayerData
    {
        string m_name;              // The player's name.
        PlayerColours m_colour;     // The player's colour.

        // Default constructor.
        public PlayerData()
        {
            // Reset the container. This prevents the initialisation being in 2 places.
            Reset();
        }

        // Resets the contents of the container.
        public void Reset()
        {
            // Set the name to an empty string.
            m_name = "";

            // Set the colour to black.
            m_colour = PlayerColours.COL_BLACK;
        }

        // Gets and Sets the player's name.
        public string PlayerName
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        // Gets and Sets the player's colour.
        public PlayerColours PlayerColour
        {
            get
            {
                return m_colour;
            }
            set
            {
                m_colour = value;
            }
        }
    }
}
