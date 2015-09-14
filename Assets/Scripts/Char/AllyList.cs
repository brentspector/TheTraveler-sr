/*******************************************************************************
 *
 *  File Name: AllyList.cs
 *
 *  Description: Old list of allies for the character
 *
 *******************************************************************************/
using GSP.Char.Allies;
using GSP.Entities.Friendlies;
using GSP.Entities.Neutrals;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: AllyList
     * 
     * Description: Manages an ally list for the characters.
     * 
     *******************************************************************************/
    public class AllyList : MonoBehaviour
	{
		List<GameObject> allies;    // The list of allies
		int maxAllies;				// The maximum number of allies allowed

		void Awake()
        {
            // Initialise our list here
            allies = new List<GameObject>();

            // A maximum of a single ally for now
            maxAllies = 1;
        } // end Awake
		
		// Return and ally GameObject reference
		public GameObject GetObject(int index)
		{
			return allies[index];
		} // end GetObject

		// Get the index of the ally
		public int GetIndex(GameObject ally)
		{
            return allies.IndexOf(ally);
		} // end GetIndex

		// Adds an ally to the list
        // Note: This is hard-coded for a single ally for now; it'll work for now :P
		public bool AddAlly(GameObject ally)
		{
            // Ensure the character isn't at their max number of allies
            if(NumAllies != maxAllies)
			{
                // Add the ally to the list
                allies.Add(ally);

                // Return sucess
                return true;
			} // end if
			else
			{
                // Return failure
                Debug.Log("Ally limit reached. Add denied.");
                return false;
			} // end else
		} // end AddAlly

		// Removes an ally from the list by its GameObject
		public void RemoveAlly(GameObject ally)
		{
            // Get the merchant script of the player
            var playerEntity = (Merchant)GetComponent<Player>().Entity;
            // Get the porter script of the ally
            var allyEntity = (Porter)ally.GetComponent<PorterMB>().Entity;

            // Remove the ally's values from the player directly
            // Note: this will be done differently later
            playerEntity.MaxWeight -= allyEntity.MaxWeight;

			// Remove the ally from the list
            allies.Remove(ally);
		} // end RemoveAlly

		// Removes an ally from the list by its index
		// ISSUE: Doesn't seem to work all that well
		public void RemoveAlly(int index)
		{
            // Get the merchant script of the player
            var playerEntity = (Merchant)GetComponent<Player>().Entity;
            // Get the porter script of the ally
            var allyEntity = (Porter)this[index].GetComponent<PorterMB>().Entity;

            // Remove the ally's values from the player directly
            // Note: this will be done differently later
            playerEntity.MaxWeight -= allyEntity.MaxWeight;

			// Get the number of allies for later
			int temp = NumAllies;
			// Remove the ally at the given index
            allies.RemoveAt(index);
            Debug.LogFormat("Old count {0} New count {1}", temp, NumAllies);
		} // end RemoveAlly

		// Clear the ally list of the character
		public void ClearAllyList()
		{
			// Loop through the ally list
            foreach (var ally in allies)
			{
				// Remove the current ally
                RemoveAlly(ally);
			} // end foreach
		} // end ClearAllyList

        // Get the ally by its index; This should stay readonly (get only)
        public GameObject this[int index]
        {
            get { return allies[index]; }
        } // end indexer

        // Gets the number of allies the character has
        // Also sets the max number they can have
        public int NumAllies
        {
            get { return allies.Count; }
            set { maxAllies = Utility.ZeroClampInt(value); }
        } // end NumAllies
	} // end AllyList
} // end GSP.Char
