using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GSP.Tiles;
using GSP.Char;

namespace GSP.Entities.Neutrals
{
	public class Merchant : Entity
	{
        string          m_name;     // Holds the name of the merchant.
        PlayerColours   m_colour;   // Holds the colour of the merchant
        
        public Merchant(int ID, GameObject gameObject, PlayerColours playerCoulours, string playerName) :
            base(ID, gameObject)
        {
            // Set the entity's type to merchant.
            Type = EntityType.ENT_MERCHANT;

            // Set merchant's name.
            m_name = playerName;

            // Set the merchant's colour.
            m_colour = playerCoulours;
        }

        // Gets the merchant's name.
        public string Name
        {
            get
            {
                return m_name;
            }
        }

        // Gets the merchant's colour.
        public PlayerColours Colour
        {
            get
            {
                return m_colour;
            }
        }
	}
}
