using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GSP.Char;

namespace GSP
{
	public class Test : MonoBehaviour
	{
		// Update is called once per frame.
		void Update()
		{
			// Tests getting the player's info
			if ( Input.GetKeyDown( KeyCode.A ) )
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

					// Get the player's char data.
					EndSceneCharData endScenechardata = endSceneScript.GetData( playerNum );

					// Debug what's in the char data object.
					Debug.Log( "Player Number: " + endScenechardata.PlayerNumber );
					Debug.Log( "Player Name: " + endScenechardata.PlayerName );
					Debug.Log( "Player Currency: " + endScenechardata.PlayerCurrency );
				} // end for loop
			} // end if statement

			// Tests checking to player's currency for a winner.
			if ( Input.GetKeyDown( KeyCode.B ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
				
				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();

				// Get its misc script.
				Misc endSceneMisc = endSceneDataObject.GetComponent<Misc>();

				// Get the winning player.
				int playerWinner = endSceneMisc.DetermineWinner();

				// Show it on the console.
				Debug.Log( "Player #" + playerWinner + " is the winner!" );

				// Get the winning players data.
				EndSceneCharData playerWinnerData = endSceneScript.GetData( playerWinner );

				// Show it on the console.
				Debug.Log( playerWinnerData.PlayerName + " has won with " + playerWinnerData.PlayerCurrency + " currency!" );
			} // end if statement

			// Tests checks for a winner and grabs the sorted list. Then loops over it.
			if ( Input.GetKeyDown( KeyCode.C ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
				
				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();
				
				// Get its misc script.
				Misc endSceneMisc = endSceneDataObject.GetComponent<Misc>();
				
				// Since we're getting the list, it doesn't matter if we assign the result to a variable here.
				endSceneMisc.DetermineWinner();

				// Get the list.
				List<KeyValuePair<int, int>> playerList = endSceneMisc.GetList();

				// Loop over the list.
				foreach ( var pair in playerList )
				{
					// Get the player's data from the key in the pair.
					EndSceneCharData charData = endSceneScript.GetData( pair.Key );

					// Do stuff with it.
					Debug.Log( "Player #" + charData.PlayerNumber + " has " + charData.PlayerCurrency + " currency!" );
				} // end foreach
			} // end if statement

			// Tests checks for a winner and grabs the sorted list. Uses it by index.
			if ( Input.GetKeyDown( KeyCode.D ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
				
				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();
				
				// Get its misc script.
				Misc endSceneMisc = endSceneDataObject.GetComponent<Misc>();
				
				// Since we're getting the list, it doesn't matter if we assign the result to a variable here.
				endSceneMisc.DetermineWinner();
				
				// Get the list.
				List<KeyValuePair<int, int>> playerList = endSceneMisc.GetList();

				// Do stuff with the list by index.

				// Get the char data for the 2nd place.
				EndSceneCharData charData = endSceneScript.GetData( playerList[1].Key );

				// Do something with it.
				Debug.Log("Player #" + charData.PlayerNumber + " has placed second with " + charData.PlayerCurrency + " currency!");

			} // end if statement

			// Tests checks for a winner and grabs the sorted list. Uses it to grab the last player.
			if ( Input.GetKeyDown( KeyCode.E ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
				
				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();
				
				// Get its misc script.
				Misc endSceneMisc = endSceneDataObject.GetComponent<Misc>();
				
				// Since we're getting the list, it doesn't matter if we assign the result to a variable here.
				endSceneMisc.DetermineWinner();
				
				// Get the list.
				List<KeyValuePair<int, int>> playerList = endSceneMisc.GetList();

				// Get the number of players.
				int numPlayers = endSceneScript.Count;

				// Get the last player. Its index is the number of players minus one.
				int lastPlayerIndex = numPlayers - 1;
				
				// Get the char data for the last player.
				EndSceneCharData charData = endSceneScript.GetData( playerList[lastPlayerIndex].Key );
				
				// Do something with it.
				Debug.Log("Player #" + charData.PlayerNumber + " has placed last with " + charData.PlayerCurrency + " currency!");
				
			} // end if statement
		} // end Update function
	} // end Test class
} // end namespace
