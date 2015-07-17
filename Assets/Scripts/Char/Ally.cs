using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char
{
	public class Ally : MonoBehaviour
	{
		List<GameObject> m_allyList;		//Holds list of allies
		int maxAllies;						//Limit on number of allies

		// Use this for initialisation
		void Start()
		{
			// Initialise our list here.
			m_allyList = new List<GameObject>();
			// NOTE: Keep this at 1 when creating game for realistic play
			maxAllies = 1;
		}
		
		// Update is called once per frame
		void Update()
		{
			// Nothing to do here at the moment.
		}

		// Destroy the game object this script is attached to.
		public void DestroyGO()
		{
			Destroy(this.gameObject);
		}

		// Get the ally by its index. This should stay readonly (get only).
		public GameObject this[int index]
		{
			get { return m_allyList [index]; }
		}

		// Return ally object
		public GameObject GetObject( int index )
		{
			return m_allyList[index];
		}

		// Get the index of the ally.
		public int GetIndex( GameObject ally )
		{
			return m_allyList.IndexOf( ally );
		}

		// Gets the number of allies the character has.
		// Also sets the max number they can have.
		public int NumAllies
		{
			get { return m_allyList.Count; }
			set 
			{ 
				//Apply value
				maxAllies = value;

				//Clamp allyLimit to zero
				if(maxAllies < 0)
				{
					maxAllies = 0;
				} //end if
			} //end Set
		} // end NumAllies function

		// Adds an ally to the list.
		public void AddAlly( GameObject ally )
		{
			if(NumAllies != maxAllies)
			{
				// Add the ally to the list.
				m_allyList.Add( ally );

				// Get the character script of the game object this is attached to.
				var playerCharScript = GetComponent<Character>();
				// Also get the character script for the ally.
				var allyCharScript = ally.GetComponent<Character>();

				// Add the ally's values to the player directly.
				playerCharScript.MaxWeight += allyCharScript.MaxWeight;
				playerCharScript.MaxInventory += allyCharScript.MaxInventory;
			} //end if
			else
			{
				print ("Ally limit reached. Add denied.");
			} //end else
		} // end AddAlly function

		// Removes an ally from the list by its object.
		public void RemoveAlly( GameObject ally )
		{
			// Get the character script of the game object this is attached to.
			var playerCharScript = GetComponent<Character>();
			// Also get the character script for the ally.
			var allyCharScript = ally.GetComponent<Character>();
			
			// Add the ally's values to the player directly.
			playerCharScript.MaxWeight -= allyCharScript.MaxWeight;
			playerCharScript.MaxInventory -= allyCharScript.MaxInventory;

			// Remove the ally from the list.
			m_allyList.Remove( ally );
		} // end RemoveAlly function

		// Removes an ally from the list by its index.
		// ISSUE: Doesn't seem to work all that well.
		public void RemoveAlly( int index )
		{
			// Get the character script of the game object this is attached to.
			var playerCharScript = GetComponent<Character>();
			// Also get the character script for the ally.
			var allyCharScript = this[index].GetComponent<Character>();
			
			// Add the ally's values to the player directly.
			playerCharScript.MaxWeight -= allyCharScript.MaxWeight;
			playerCharScript.MaxInventory -= allyCharScript.MaxInventory;

			// Get the number of allies for later.
			int temp = NumAllies;
			// Remove the ally at the given index.
			m_allyList.RemoveAt( index );
			print ("Old count " + temp + " New count " + NumAllies);
		} // end RemoveAlly function

		// Clear the ally list of the character.
		public void ClearAllyList()
		{
			// Loop through the ally list.
			foreach ( var ally in m_allyList )
			{
				// Remove the current ally.
				RemoveAlly( ally );
			} // end foreach loop
		} // end ClearAllyList function.
	}
}
