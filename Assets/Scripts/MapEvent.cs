/*******************************************************************************
 *
 *  File Name: MapEvent.cs
 *
 *  Description: The logic for the events that happen on the maps
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GSP
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    /*******************************************************************************
     *
     * Name: MapEvent
     * 
     * Description: Deals with the logic behind the different map's events.
     * 
     *******************************************************************************/
    public class MapEvent : MonoBehaviour
	{
        Die die; // Die reference
		
		GameObject player;          // The player's GameObject reference
		Character playerCharScript; // The player's Character script component reference

        GameObject audioSrc;    // The AudioSource GameObject reference
		
		//NOTE!!
		//SIZE must be the last item in the enum so that anything based
		//on the length of the enum can be used as normal. It is best to
		//add items to the left of SIZE but after the current 2nd to last
		//item in the enum. For instance if the list was {Enemy, Ally, Size}
		//you should enter the new item between Ally and Size
		
		// The enumeration for normal tile events
		enum NormalTile {Enemy, Ally, Item, Nothing, Size};
		
		// The enumeration for the resource tile events
		enum ResourceTile {Wool, Wood, Fish, Ore, Size};
		
		//Event chances
		//NOTE: These should add up to less than 100 so that 
		//there is a chance nothing occurs. Minimum chance of
		//one or else there will be problems
		int enemyChance = 25;   // The minimum chance for the MapEvent to an enemy
        int allyChance = 15;    // The minimum chance for the MapEvent to an ally
        int itemChance = 15;    // The minimum chance for the MapEvent to an item
		
		string guiResult; // The MapEvent summary

		// Used for initialisation
		void Start()
		{
			// Initialise the die here.
			die = new Die();
			
			// Reseed the die.
			die.Reseed(Environment.TickCount);

            // Get the AudioSource
            audioSrc = GameObject.FindGameObjectWithTag("AudioSourceTag");
		}

        //TODO: Damien: Replace with the GameMaster functionality later.
        //Calls map event and returns string
		public string DetermineEvent(GameObject playerEntity)
		{
			// Set the player GameObject and its Character script
			player = playerEntity;
			playerCharScript = player.GetComponent<Character>();
			
			// Get the tile at the player's position
			Vector3 tmp = player.transform.localPosition;
			// Fix the z-axis; change by Damien to get the tiles to work again.
			tmp.z = -0.01f;
            Tile currentTile = TileDictionary.GetTile(TileManager.ToPixels(tmp));
			
			// Was a tile found?
			if(currentTile == null)
			{
				// No tile found so return "This is not a valid \ntile. No event occured.";
				guiResult = "This is not a valid \ntile. No event occured.";
				return "Nothing";
			} // end if
			// Otherwise, was the tile a non-resource?
			else if (currentTile.ResourceType == ResourceType.None) 
			{
				// Roll a die to get a number from 1-100
                int dieResult = die.Roll (1, 100);

                // Check for an enemy
				if(dieResult < enemyChance)
				{
					return "Enemy";
				} // end if
				// Check for an ally
                else if (dieResult < allyChance + enemyChance && dieResult >= enemyChance)
				{
					return "Ally";
				} // end else if
				// Check for an item
                else if(dieResult < itemChance + allyChance + enemyChance
				        && dieResult >= allyChance + enemyChance)
				{
					return "Item";
				} // end else if
				else
				{
					// The MapEvent was nothing so return "Die roll was " + dieResult + ".\nNo map event occured.";
					guiResult = "No map event occured.";
					return "Nothing";
				} // end else
			} //end if
			// Otherwise, the tile is a resource
			else
			{
				// Create a temp resource
				Resource temp = new Resource();
				
				// Turn temp into a type of resource
				temp.SetResource(currentTile.ResourceType.ToString());
				
				// Pick up the resource
                playerCharScript.PickupResource(temp, 1);
				
				// Declare what was landed on
				guiResult = "You got a resource:\n" + temp.Name;

                //TODO: Brent: Replace with AudioManager later
                //// Play found for what was landed on
                //if(temp.ResourceName == "Fish")
                //{
                //    // Play fish sound
                //    audioSrc.GetComponent<AudioSource>().PlayOneShot(GSP.AudioReference.sfxFishing);
                //} // end if
                //else if(temp.ResourceName == "Wood")
                //{
                //    // Play wood sound
                //    audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxWoodcutting );
                //} // end else if
                //else if(temp.ResourceName == "Wool")
                //{
                //    // Play wool sound
                //    audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxShearing );
                //} // end else if
                //else
                //{
                //    // Play ore sound
                //    audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxMining );
                //} // end else
				return "Resource";
			} // end else
		} // end DetermineEvent

        //TODO: Damien: Replace with the GameMaster functionality later.
        // Resolves a fight when the enemy MapEvent spawns
        public string ResolveFight(GameObject player)
		{
			// Create the enemy
            GameObject enemy = Instantiate(PrefabReference.prefabCharacter, 
                new Vector3(0.7f, 0.5f, 0.0f), new Quaternion()) as GameObject;

			// Remove the SpriteRenderer component. This makes the enemy not shown in the scene.
            Destroy(enemy.GetComponent<SpriteRenderer>());
			
			// Get the character script attached to the enemy
			Character enemyScript = enemy.GetComponent<Character>();
			
			// Set the stats of the enemy
			enemyScript.AttackPower = die.Roll(1, 9);
			enemyScript.DefencePower = die.Roll(1, 9);
			
			//TODO: Brent: Add Battle Scene
			
			//Battle characters
			Fight fighter = new Fight();
			string result = fighter.CharacterFight(enemy, player);
			
			// Check if the player lost the fight
			if(result.Contains("Enemy wins"))
			{
                // The player lost the fight, remove its resources or its weapon
                // The player has no resources so remove its weapon weapon
				if(playerCharScript.ResourceWeight <= 0)
				{
					playerCharScript.RemoveItem("attack");
				} // end if
				// Otherwise, the player has resources so remove a random resource
				else
				{
					// Get the player's resource list script
					ResourceList tempList = player.GetComponent<ResourceList>();
					
					// Choose a resource
                    int resourceNumber = die.Roll(1, (int)ResourceType.Size) - 1;
                    Debug.LogFormat("Resource number is {0}", resourceNumber);

					// Get the list of resources of that type
                    List<Resource> resList = tempList.GetResourcesByType(
                        Enum.GetName(typeof(ResourceType), resourceNumber));
					
					// Remove the resources by list
                    tempList.RemoveResources(resList);
					result += " \nAs a result, you lost all your " + Enum.GetName(typeof(ResourceType), resourceNumber);
				} // end else
			} // end if

			// Destroy the enemy GameObject
			Destroy(enemy);

			// Set the summary and return it
			guiResult = result;
			return guiResult;
		} // end ResolveFight

        //TODO: Damien: Replace with the GameMaster functionality later.
        // Resolves an ally when the ally MapEvent spawns
        public string ResolveAlly(GameObject player, string guiAcceptResult)
		{
			// Create the ally
            GameObject ally = Instantiate(PrefabReference.prefabCharacter,
                player.transform.position, new Quaternion()) as GameObject;

			// Remove the SpriteRenderer component. This makes the ally not shown in the scene.
            Destroy(ally.GetComponent<SpriteRenderer>());

            // Get the character script attached to the ally
			Character allyScript = ally.GetComponent<Character>();
			
			// Set the ally's max weight
            allyScript.MaxWeight = die.Roll(1, 20) * 6;

			// Check if the player accepts the ally
            if (guiAcceptResult == "YES")
			{
				// Add the ally to the player's ally list
				Ally playerAllyScript = player.GetComponent<Ally>();
				playerAllyScript.AddAlly(ally);

				// Set and return accepted
				guiResult = "Ally was added.";
				return guiResult;
			}

			//Check if the player declines the ally
            else if (guiAcceptResult == "NO")
			{
				// Destroy the ally GameObject
				Destroy(ally);

				// Set and return declined
				guiResult = "Ally was declined.";
				return "No ally was added.";
			} // end else if

			// Otherwise wait for answer
			return "No choice was made.";
		} // end ResolveAlly

        //TODO: Damien: Replace with the GameMaster functionality later.
        // Resolves an item when the item MapEvent spawns
        public string ResolveItem(GameObject player)
		{
			// String to return for display
			string result;
			
			// Determine what item was found
			int itemType = die.Roll(1, 4);

            // Switch ove the itemType
            switch (itemType)
            {
                // A weapon item
                case 1:
                    {
                        // Pick an item from the weapons enumeration
                        int itemNumber = die.Roll(1, (int)Weapons.Size) - 1;

                        // Assign the chosen number as the item
                        result = Enum.GetName(typeof(Weapons), itemNumber);

                        // Equip the item on player
                        playerCharScript.EquipItem(result);
                        break;
                    } // end case 1
                // Am armour item
                case 2:
                    {
                        // Pick an item from the armor enumeration
                        int itemNumber = die.Roll(1, (int)Armor.Size) - 1;

                        // Assign the chosen number as the item
                        result = Enum.GetName(typeof(Armor), itemNumber);

                        // Equip the item on player
                        playerCharScript.EquipItem(result);
                        break;
                    } // end case 2
                // An inventory item
                case 3:
                    {
                        // Pick an item from the inventory enumeration
                        int itemNumber = die.Roll(1, (int)Inventory.Size) - 1;

                        // Assign the chosen number as the item
                        result = Enum.GetName(typeof(Inventory), itemNumber);

                        // Equip the item on player
                        playerCharScript.EquipItem(result);
                        break;
                    } // end case 3
                // A weight item
                case 4:
                    {
                        // Pick an item from the weight enumeration
                        int itemNumber = die.Roll(1, (int)Weight.Size) - 1;

                        // Assign the chosen number as the item
                        result = Enum.GetName(typeof(Weight), itemNumber);

                        // Equip the item on player
                        playerCharScript.EquipItem(result);
                        break;
                    } // end case 4
                // An invalid item
                default:
                    {
                        result = "non-existant item. Nothing given.";
                        break;
                    } // end case default
            } // end switch itemType
			
			// Set and return the result
            guiResult = "You got \n" + result;
            return guiResult;
		} // end ResolveItem

		// Gets the result string
        public string GetResultString()
		{
			return guiResult;
		} // end GetResultString
	} // end MapEvent
} // end GSP
