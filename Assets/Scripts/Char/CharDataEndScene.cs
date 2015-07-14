using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	public class CharDataEndScene : MonoBehaviour
	{
		// Use this for initialisation.
		void Start()
		{
			// Get the end scene data object.
			GameObject endSceneData = GameObject.FindGameObjectWithTag( "EndSceneCharDataTag" );

			// Get the end scene data object's script.
			EndSceneData endSceneDataScript = endSceneData.GetComponent<EndSceneData>();

			// Get the script of this game object's end scene data script.
			EndSceneData endSceneScript = GetComponent<EndSceneData>();

			// Get the number of players.
			int numPlayers = endSceneDataScript.Count;

			// Copy the data in the end scene data object to the object in the end scene.
			for (int index = 0; index < numPlayers; index++)
			{
				// Get the player number.
				int playerNum = index + 1;

				// Get the current player. Remember to add 1 to the index.
				EndSceneCharData endSceneCharData = endSceneDataScript.GetData( playerNum );

				// Add it to this game object's script.
				endSceneScript.AddData( endSceneCharData );
			} // end for loop

			// Once we are done with the object, destroy it.
			Destroy( endSceneData );
		} // end Start function
	} // end CharDataEndScene class
} // end namespace
