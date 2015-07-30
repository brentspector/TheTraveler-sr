/*******************************************************************************
 *
 *  File Name: MenuData.cs
 *
 *  Description: Middle man between the menu and the game scenes for
 *               transferring data
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    /*******************************************************************************
     *
     * Name: MenuData
     * 
     * Description: Saves the number of players from the menu to the game world.
     * 
     *******************************************************************************/
    public class MenuData : MonoBehaviour
	{
		// The number of players for the game.
		int numberPlayers;

        // Use this for initialisation.
        void Start()
        {
            // Initialise the variables to zero.
            numberPlayers = 0;
        } // end Start
		
		// Gets and Sets the number of players
        public int NumberPlayers
		{
			get
            { 
                // Return the number of players
                return numberPlayers;
            } // end get
			set
			{
				// Set the number of players
                numberPlayers = value;
				
				// Check if the number of players is less than zero.
                if (numberPlayers < 0)
				{
					// Clamp the number of players to zero.
                    numberPlayers = 0;
				} // end if
			} // end set
		} // end NumberPlayers
	} // end MenuData
} // end GSP
