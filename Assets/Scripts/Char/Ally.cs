/*******************************************************************************
 *
 *  File Name: Ally.cs
 *
 *  Description: Old list of allies for the character
 *
 *******************************************************************************/
//TODO: Damien: Replace this with Ally2 later
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: Ally
     * 
     * Description: Manages an ally list for the characters.
     * 
     *******************************************************************************/
    public class Ally : MonoBehaviour
	{
		List<GameObject> allies;    // The list of allies
		int maxAllies;				// The maximum number of allies allowed

		// Use this for initialisation
		void Start()
		{
			// Initialise our list here
			allies = new List<GameObject>();

			// A maximum of a single ally for now
			maxAllies = 1;
		} // end Start
		
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
		public void AddAlly(GameObject ally)
		{
			// Ensure the character isn't at their max number of allies
            if(NumAllies != maxAllies)
			{
				// Add the ally to the list
                allies.Add(ally);

				// Get the Character script of the GameObject this is attached to
				var playerCharScript = GetComponent<Character>();
				// Also get the Character script for the ally
				var allyCharScript = ally.GetComponent<Character>();

				// Add the ally's values to the player directly
				playerCharScript.MaxWeight += allyCharScript.MaxWeight;
				playerCharScript.MaxInventory += allyCharScript.MaxInventory;
			} // end if
			else
			{
                Debug.Log("Ally limit reached. Add denied.");
			} // end else
		} // end AddAlly

		// Removes an ally from the list by its GameObject
		public void RemoveAlly(GameObject ally)
		{
			// Get the Character script of the GameObject this is attached to
			var playerCharScript = GetComponent<Character>();
			// Also get the Character script for the ally
			var allyCharScript = ally.GetComponent<Character>();
			
			// Remove the ally's values from the player directly
			playerCharScript.MaxWeight -= allyCharScript.MaxWeight;
			playerCharScript.MaxInventory -= allyCharScript.MaxInventory;

			// Remove the ally from the list
            allies.Remove(ally);
		} // end RemoveAlly

		// Removes an ally from the list by its index
		// ISSUE: Doesn't seem to work all that well
		public void RemoveAlly(int index)
		{
            // Get the Character script of the GameObject this is attached to
			var playerCharScript = GetComponent<Character>();
            // Also get the Character script for the ally
			var allyCharScript = this[index].GetComponent<Character>();

            // Remove the ally's values from the player directly
			playerCharScript.MaxWeight -= allyCharScript.MaxWeight;
			playerCharScript.MaxInventory -= allyCharScript.MaxInventory;

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
            set
            {
                // Apply value
                maxAllies = value;

                // Check if maxAllies is less than zero
                if (maxAllies < 0)
                {
                    // Clmap to zero
                    maxAllies = 0;
                } // end if
            } // end set
        } // end NumAllies
	} // end Ally
} // end GSP.Char
