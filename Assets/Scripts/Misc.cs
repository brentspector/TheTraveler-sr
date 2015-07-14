using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Char;
using System.Linq;

namespace GSP
{
	public class Misc : MonoBehaviour
	{
		// Holds the player's currency dictionary.
		Dictionary<int, int> m_currencyDict;

		// Holds the sorted list of players. It's sorted by place aka 1st place etc.
		List<KeyValuePair<int, int>> currencyList;

		// Use this for initialisation.
		void Start()
		{
			// Initialises the player currency dictionary to a new dictionary.
			m_currencyDict = new Dictionary<int, int>();
		} // end Start function

		// This is called every frame.
		void Update()
		{
			// Check for the 'c' key being pressed.
			if ( Input.GetKeyDown( KeyCode.C ) )
			{
				// Change to the cake scene!
				Application.LoadLevel( "cake" );
			} // end if statement
		} // end Update function

		// Determine who is the winner and fill in the sorted list.
		public int DetermineWinner()
		{
			// Get the game object with the end scene data tag.
			GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
			
			// Get its script.
			EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();
			
			// Get the number of players.
			int numPlayers = endSceneScript.Count;
			
			// Loop over the data in the end scene data script.
			for ( int index = 0; index < numPlayers; index++ )
			{
				// Increase index by one to get the player number.
				int playerNum = index + 1;
				
				// Only proceed if the player's currency hasn't been added.
				if ( !m_currencyDict.ContainsKey( playerNum ) )
				{
					// Get the player's char data.
					EndSceneCharData endScenechardata = endSceneScript.GetData( playerNum );
					
					// Add the player's number as the key and its currency as the value to the dictionary.
					m_currencyDict.Add( playerNum, endScenechardata.PlayerCurrency );
				} // end if statement
			} // end for loop
			
			// Now we sort the currency dictionary.
			IEnumerable<KeyValuePair<int, int>> sortedCurrency = from entry in m_currencyDict orderby entry.Value descending select entry;
			
			// Create a list from this ordering.
			currencyList = sortedCurrency.ToList();
			
			// Now get and return the player at the front of the list as the winner.
			// This only happens because it was sorted to have the highest at the top.
			return currencyList[0].Key;
		} // end DetermineWinner function

		// Gets a copy of the sorted currency list.
		public List<KeyValuePair<int, int>> GetList()
		{
			// Get a copy of the sorted currency list.
			List<KeyValuePair<int, int>> tmpList = currencyList;

			// Return the list.
			return tmpList;
		} // end GetList function
	} // end Misc class
} // end namespace
