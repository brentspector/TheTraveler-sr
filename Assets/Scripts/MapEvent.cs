using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GSP.Char;
using GSP.Tiles;

namespace GSP
{
	public class MapEvent : MonoBehaviour
	{
		//Holds object for refrencing die functions
		private Die m_die;
		
		//Holds the objects for referencing the player and its script functions.
		GameObject m_player;
		Character m_playerCharScript;

		//Audio
		GameObject audioSrc;
		
		//NOTE!!
		//SIZE must be the last item in the enum so that anything based
		//on the length of the enum can be used as normal. It is best to
		//add items to the left of SIZE but after the current 2nd to last
		//item in the enum. For instance if the list was {ENEMY, ALLY, SIZE}
		//you should enter the new item between ALLY and SIZE.
		
		//Holds list of normal tile events
		enum normalTile {ENEMY, ALLY, ITEM, WEATHER, NOTHING, SIZE};
		
		//Holds list of resource tile events
		enum resourceTile {WOOL, WOOD, FISH, ORE, SIZE};
		
		//Event chances
		//NOTE: These should add up to less than 100 so that 
		//there is a chance nothing occurs. Minimum chance of
		//one or else there will be problems
		int m_enemyChance = 25;
		int m_allyChance = 15;
		int m_itemChance = 15;
		
		//Event Summary
		string m_GUIResult;

		//Initialize die
		void Start()
		{
			// Initialise the die here.
			m_die = new Die();
			
			// Reseed the die.
			m_die.Reseed(Environment.TickCount);

			//Audio
			audioSrc = GameObject.FindGameObjectWithTag( "AudioSourceTag" );
		}

		//Calls map event and returns string
		public string DetermineEvent(GameObject player)
		{
			//Set up player script
			m_player = player;
			m_playerCharScript = m_player.GetComponent<Character>();
			
			//Gather tile
			Vector3 tmp = m_player.transform.localPosition;
			// Change by Damien to get tiles to work again.
			tmp.z = -0.01f;
			// Original: tmp.z = 0.0f;
			Tile currentTile = TileDictionary.GetTile (TileManager.ToPixels (tmp));
			
			//If no tile found
			if(currentTile == null)
			{
				//return "This is not a valid \ntile. No event occured.";
				m_GUIResult = "This is not a valid \ntile. No event occured.";
				return "NOTHING";
			} //end if
			//If no resource at tile
			else if (currentTile.ResourceType == ResourceType.NONE) 
			{
				int dieResult = m_die.Roll (1, 100);
				if(dieResult < m_enemyChance)
				{
					return "ENEMY";
				} //end if ENEMY
				else if (dieResult < m_allyChance + m_enemyChance && dieResult >= m_enemyChance)
				{
					return "ALLY";
				} //end else if ALLY
				else if(dieResult < m_itemChance + m_allyChance + m_enemyChance
				        && dieResult >= m_allyChance + m_enemyChance)
				{
					return "ITEM";
				} //end else if ITEM
				else
				{
					//return "Die roll was " + dieResult + ".\nNo map event occured.";
					m_GUIResult = "No map event occured.";
					return "NOTHING";
				} //end else if NOTHING
			} //end if NORMAL TILE
			//If resource at tile
			else
			{
				//Create temp resource
				Resource temp = new Resource();
				
				// Turn temp into type of resource
				temp.SetResource(currentTile.ResourceType.ToString());
				
				//Pick up resource
				m_playerCharScript.PickupResource( temp, 1 );
				
				//Declare what was landed on
				m_GUIResult = "You got a resource:\n" + temp.ResourceName;

				//Play found for what was landed on
				if(temp.ResourceName == "Fish")
				{
					audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxFishing ); //Play fish sound
				} //end if
				else if(temp.ResourceName == "Wood")
				{
					audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxWoodcutting ); //Play wood sound
				} //end else if
				else if(temp.ResourceName == "Wool")
				{
					audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxShearing ); //Play wool sound
				} //end else if
				else
				{
					audioSrc.GetComponent<AudioSource>().PlayOneShot( GSP.AudioReference.sfxMining ); //Play ore sound
				} //end else
				return "RESOURCE";
			} //end else if RESOURCE TILE
		} //end DetermineEvent(GameObject player)
		
		public string ResolveFight(GameObject player)
		{
			///Create the enemy
			//TODO Replace prefabCharacter with prefabEnemy
			GameObject m_enemy = Instantiate( PrefabReference.prefabCharacter,
				new Vector3( 0.7f, 0.5f, 0.0f ), new Quaternion() ) as GameObject;

			// Remove the sprite renderer component. This makes the ally not shown on the map.
			Destroy( m_enemy.GetComponent<SpriteRenderer>() );
			
			//Get the character script attached to the enemy
			Character m_enemyScript = m_enemy.GetComponent<Character>();
			
			//Set stats
			m_enemyScript.AttackPower = m_die.Roll(1, 9);
			m_enemyScript.DefencePower = m_die.Roll(1, 9);
			
			//TODO Add Battle Scene
			
			//Battle characters
			Fight fighter = new Fight();
			///////////////////////////////////////
			//string result = "Die roll was " + dieResult + ".\nMap event was enemy, \nand " + 
			//	fighter.CharacterFight(m_enemy, m_player);
			//////////////////////////////////////
			string result = fighter.CharacterFight(m_enemy, m_player);
			
			//Player lost fight, remove resources or weapon
			if(result.Contains("Enemy wins"))
			{
				//Player has no resources, remove weapon
				if(m_playerCharScript.ResourceWeight <= 0)
				{
					m_playerCharScript.RemoveItem("weapon");
				} //end if
				//Player has resources, remove random resource
				else
				{
					// Get the player's resource list script.
					ResourceList tempList = m_player.GetComponent<ResourceList>();
					
					// Choose a resource.
					int resourceNumber = m_die.Roll( 1, (int)ResourceType.SIZE ) - 1;
					print ("Resource number is " + resourceNumber);

					// Get the list of resources of that type.
					List<Resource> resList = tempList.GetResourcesByType( 
						Enum.GetName( typeof( ResourceType ), resourceNumber ) );
					
					// Remove the resources by list.
					tempList.RemoveResources( resList );
					result += " \nAs a result, you lost all your " + Enum.GetName(typeof(ResourceType), 
					    resourceNumber);
				} //end else
			} //end if

			//Clear enemy
			Destroy(m_enemy);

			//Set Summary
			m_GUIResult = result;
			return m_GUIResult;
		} //end ResolveFight(GameObject player)
		
		public string ResolveAlly(GameObject player, string p_GUIresult)
		{
			//Create ally
			GameObject m_ally = Instantiate( PrefabReference.prefabCharacter,
				m_player.transform.position, new Quaternion() ) as GameObject;

			// Remove the sprite renderer component. This makes the ally not shown on the map.
			Destroy( m_ally.GetComponent<SpriteRenderer>() );
			
			//Generate script
			Character m_allyScript = m_ally.GetComponent<Character>();
			
			//Set max weight
			m_allyScript.MaxWeight = m_die.Roll(1, 20) * 6;

			//Player accepts ally
			if(p_GUIresult == "YES")
			{
				//Add to ally list
				Ally m_playerAllyScript = m_player.GetComponent<Ally>();
				m_playerAllyScript.AddAlly(m_ally);

				//Return accepted
				m_GUIResult = "Ally was added.";
				return "Ally Was Added.";
			}

			//Player declines ally
			else if(p_GUIresult == "NO")
			{
				//Destroy newAlly
				Destroy(m_ally);

				//Return decline
				m_GUIResult = "Ally was declined.";
				return "No ally was added.";
			} //end else if

			//Otherwise wait for answer
			return "No choice was made.";
		} //end ResolveAlly(GameObject player, string p_GUIresult)
		
		public string ResolveItem(GameObject player)
		{
			//String to return for display
			string result;
			
			//Determine what item was found
			int itemType = m_die.Roll(1, 4);
			
			if(itemType == 1)
			{
				//Pick an item from the weapons enum
				int itemNumber = m_die.Roll(1, (int)Weapons.SIZE) - 1;
				
				//Assign chosen number as the item
				result = Enum.GetName(typeof(Weapons), itemNumber);
				
				//Equip to player
				m_playerCharScript.EquipItem(result);
				
			} //end if Weapon
			else if(itemType == 2)
			{
				//Pick an item from the armor enum
				int itemNumber = m_die.Roll(1, (int)Armor.SIZE) - 1;
				
				//Assign chosen number as the item
				result = Enum.GetName(typeof(Armor), itemNumber);
				
				//Equip to player
				m_playerCharScript.EquipItem(result);
			} //end else if Armor
			else if(itemType == 3)
			{
				//Pick an item from the inventory enum
				int itemNumber = m_die.Roll(1, (int)Inventory.SIZE) - 1;
				
				//Assign chosen number as the item
				result = Enum.GetName(typeof(Inventory), itemNumber);
				
				//Equip to player
				m_playerCharScript.EquipItem(result);
			} //end else if Inventory
			else if(itemType == 4)
			{
				//Pick an item from the weight enum
				int itemNumber = m_die.Roll(1, (int)Weight.SIZE) - 1;
				
				//Assign chosen number as the item
				result = Enum.GetName(typeof(Weight), itemNumber);
				
				//Equip to player
				m_playerCharScript.EquipItem(result);
			} //end else if Weight
			else
			{
				result = "non-existant item. Nothing given.";
			} //end else

			m_GUIResult = "You got \n" + result;
			
			return "You got \n" + result;
		} //end ResolveItem(GameObject player)

		public string GetResultString()
		{
			return m_GUIResult;
		}

	} //end MapEvent class
} //end namespace GSP
