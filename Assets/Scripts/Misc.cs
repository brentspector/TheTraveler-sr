/*******************************************************************************
 *
 *  File Name: Misc.cs
 *
 *  Description: Determines a winner and sorts players by place
 *
 *******************************************************************************/
using GSP.Char;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSP
{
    //TODO: Damien: Replace with the GameMaster functionality later
    /*******************************************************************************
     *
     * Name: Misc
     * 
     * Description: Determines the winner at the end scene.
     * 
     *******************************************************************************/
    public class Misc : MonoBehaviour
	{
		// The player's currency dictionary
		Dictionary<int, int> playerCurrencies;

		// The sorted list of players; It's sorted by place aka 1st place etc
		List<KeyValuePair<int, int>> currencies;

		// Use this for initialisation
		void Start()
		{
			// Initialises the player currency dictionary
			playerCurrencies = new Dictionary<int, int>();
		} // end Start

		// This is called every frame.
		void Update()
		{
			// Check for the 'c' key being pressed.
			if (Input.GetKeyDown(KeyCode.C))
			{
				// Change to the cake scene!
                Application.LoadLevel("cake");
			} // end if
		} // end Update

		// Determine who the winner is and fill in the sorted list.
		public int DetermineWinner()
		{
			// Get the game object with the EndSceneDataTag tag.
            GameObject endSceneDataObject = GameObject.FindGameObjectWithTag("EndSceneDataTag");
			
			// Get its script.
            EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();
			
			// Get the number of players.
			int numPlayers = endSceneScript.Count;
			
			// Loop over the data in the EndSceneData script.
			for (int index = 0; index < numPlayers; index++)
			{
				// Increase index by one to get the player number; this is because the the dictionaries are zero-index based
				int playerNum = index + 1;
				
				// Only proceed if the player's currency hasn't been added.
				if (!playerCurrencies.ContainsKey(playerNum))
				{
					// Get the player's EndSceneCharData.
                    EndSceneCharData endScenechardata = endSceneScript.GetData(playerNum);
					
					// Add the player's number as the key and its currency as the value to the dictionary.
                    playerCurrencies.Add(playerNum, endScenechardata.PlayerCurrency);
				} // end if
			} // end for
			
			// Now we sort the currency dictionary.
			IEnumerable<KeyValuePair<int, int>> sortedCurrencies = from entry in playerCurrencies orderby entry.Value descending select entry;
			
			// Create a list from this ordering.
			currencies = sortedCurrencies.ToList();
			
			// Now get and return the player at the front of the list as the winner
			// This only happens because it was sorted to have the highest at the top
			return currencies[0].Key;
		} // end DetermineWinner

		// Gets a copy of the sorted currency list.
		public List<KeyValuePair<int, int>> GetList()
		{
			// Get a copy of the sorted currency list.
			List<KeyValuePair<int, int>> tmpCurrencies = currencies;

			// Return the list.
			return tmpCurrencies;
		} // end GetList
	} // end Misc
} // end GSP
