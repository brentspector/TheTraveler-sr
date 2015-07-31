/*******************************************************************************
 *
 *  File Name: EndSceneData.cs
 *
 *  Description: Middle man between the game scenes and the end scene for some
 *               data transfer
 *
 *******************************************************************************/
//TODO: Damien: Replace with the GameMaster functionality later.
using GSP.Char;
using System.Collections.Generic;
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: EndSceneData
     * 
     * Description: Allows the end scene to access some of the player's data.
     * 
     *******************************************************************************/
    public class EndSceneData : MonoBehaviour
	{
		Dictionary<int, EndSceneCharData> dataDictionary; // The dictionary containing the character's data


		// Use this for ininitialisation
		public void Start()
		{
			// Initialises the EndSceneData dictionary
			dataDictionary = new Dictionary<int, EndSceneCharData>();
		} // end Start

		// Adds data to the EndSceneData dictionary with a player GameObject
		public void AddData(int playerNum, GameObject player)
		{
			// Create a new EndSceneCharData object using the given player GameObject
			EndSceneCharData charData = new EndSceneCharData(playerNum, player);

			// Add that object to the dictionary using the player number as the key
			dataDictionary.Add(playerNum, charData);
		} // end AddData

		// Adds data to the EndSceneData dictionary with the EnsSceneCharData object
		public void AddData(EndSceneCharData endSceneCharData)
		{
			// Add the given EndSceneCharData object to the dictionary
			dataDictionary.Add(endSceneCharData.PlayerNumber, endSceneCharData);
		} // end AddData

		// Retrieves the EndSceneData from the dictionary based on the key supplied
		public EndSceneCharData GetData(int playerNum)
		{
			// Check if the dictionary contains the key
			if (!dataDictionary.ContainsKey(playerNum))
			{
				// No key found so log it and return null
				Debug.LogWarningFormat("The EndSceneData Dictionary contains no such key: '{0}'", playerNum);
				return null;
			} // end if

			// Otherwise get the value and return it
			return dataDictionary[playerNum];
		} // end GetData

		// Checks if the key exists in the dictionary
		public bool KeyExists(int playerNum)
		{
			return dataDictionary.ContainsKey(playerNum);
		} // end KeyExists

		// Gets the number of entries in the dictionary. This is essentially the number of players.
		public int Count
		{
			get { return dataDictionary.Count; }
		} // end Count
	}
} // end GSP
