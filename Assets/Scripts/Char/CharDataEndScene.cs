/*******************************************************************************
 *
 *  File Name: CharDataEndScene.cs
 *
 *  Description: Old construct for getting the character data in the end scene
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Char
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    /*******************************************************************************
     *
     * Name: CharDataEndScene
     * 
     * Description: Gets the data from the EndSceneCharData instance in the end
     *              scene.
     * 
     *******************************************************************************/
    public class CharDataEndScene : MonoBehaviour
	{
		// Use this for initialisation
		void Start()
		{
			// Get the EndSceneData GameObject
            GameObject endSceneData = GameObject.FindGameObjectWithTag("EndSceneCharDataTag");

			// Get its script
			EndSceneData endSceneDataScript = endSceneData.GetComponent<EndSceneData>();

			// Get the script of this GameObject's EndSceneData script
			EndSceneData endSceneScript = GetComponent<EndSceneData>();

			// Get the number of players
			int numPlayers = endSceneDataScript.Count;

			// Copy the data in the EndSceneData GameObject to the GameObject in the end scene
			for (int index = 0; index < numPlayers; index++)
			{
				// Get the player number. We add one because the list is zero-index based
				int playerNum = index + 1;

				// Get the current player
                EndSceneCharData endSceneCharData = endSceneDataScript.GetData(playerNum);

				// Add it to this GameObject's script
                endSceneScript.AddData(endSceneCharData);
			} // end for

			// Once we are done with the GameObject, destroy it
            Destroy(endSceneData);
		} // end Start
	} // end CharDataEndScene
} // end GSP.Char
