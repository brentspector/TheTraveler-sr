/*******************************************************************************
 *
 *  File Name: EndSceneCharData.cs
 *
 *  Description: Old construct for transferring character data from the game to
 *               the end scene
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Char
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    /*******************************************************************************
     *
     * Name: EndSceneCharData
     * 
     * Description: Stores the character data for use in the end scene.
     * 
     *******************************************************************************/
    public class EndSceneCharData
	{
		int playerNumber;	// The player's number
		string playerName;  // The player's name based of its number
		int playerCurrency; // The player's currency amount
		
		// Constructor for creating the EndSceneCharData
		public EndSceneCharData(int playerNum, GameObject player)
		{
			// Set the player's number
			playerNumber = playerNum;
			
			// Set the player's name based on the above number
			playerName = "Player " + playerNumber.ToString();
			
			// Get the character script of the player
			Character charScript = player.GetComponent<Character>();

			// Set the player's currency
			playerCurrency = charScript.Currency;
		} // end EndSceneCharData

		// Gets the player's number
		public int PlayerNumber
		{
			get { return playerNumber; }
		} // end PlayerNumber

		// Gets the player's name
		public string PlayerName
		{
			get { return playerName; }
		} // end PlayerName

		// Gets the player's curreny
		public int PlayerCurrency
		{
			get { return playerCurrency; }
		} // end PlayerCurrency
	} // end EndSceneCharData
} // end GSP.Char
