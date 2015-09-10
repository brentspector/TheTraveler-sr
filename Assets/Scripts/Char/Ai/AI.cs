/*******************************************************************************
 *
 *  File Name: AI.cs
 *
 *  Description: Script fo AI behavior
 *
 *******************************************************************************/
using UnityEngine;
using GSP.Core	;
using GSP.Entities.Neutrals;
using GSP.Tiles;
using GSP.Items;
using GSP.Items.Inventories;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char.AI
{
	/*******************************************************************************
     *
     * Name: AI
     * 
     * Description: The logic for controlling AI behavior in-game
     * 
     *******************************************************************************/
	public class AI : Player
	{
		GameplayStateMachine GPSM;	// Active GPSM of the game
		GUIMovement GUIM;			// Active movement of the game
		Merchant merchant;			// Merchant object of the AI
		int turnPhase;				// Moves AI along through turn
		bool isProcessing;			// Whether AI is already doing something

		// Used for initialization
		void Start()
		{
			// Initialize GPSM reference
			GPSM = GameObject.Find ("Canvas").GetComponent<GameplayStateMachine> ();

			// Initialize GUIM reference
			GUIM = GameObject.Find ("Canvas").GetComponent<GUIMovement> ();

			// Initialize merchant reference
			merchant = (Merchant)Entity;

			// Initialize turn phase to 0
			turnPhase = 0;

			// Initailize isProcessing to false
			isProcessing = false;
		} //end Start

		// Reset variables when AI is awoken
		void Awake()
		{
			turnPhase = 0;
			isProcessing = false;
		} //end Awake

		// Used to direct AI's actions
		void Update()
		{
			if(turnPhase == 0)
			{
				// Only run once per turn
				if(!isProcessing)
				{
					// Set isProcessing to true
					isProcessing = true;
					
					// Only begin turn once
					PressAction ();
				} //end if !isProcessing
			} //end if
			// Roll Dice phase
			else if (turnPhase == 1) 
			{
				// Only run once per turn
				if(!isProcessing)
				{
					// Set isProcessing to true
					isProcessing = true;

					// Only roll die once
					PressAction ();
				} //end if !isProcessing
			} //end else if

			// Select Path to Take phase
			else if (turnPhase == 2) 
			{
				// Only run once per turn
				if(!isProcessing)
				{
					// Set isProcessing to true
					isProcessing = true;

					// Only set target once
					SetTarget ();
				} //end if

				// Keep calling Move until Move ends itself
				Move ();
			} //end else if

			// Do Action phase
			else if(turnPhase == 3)
			{
				// Determine what scene we are in
				if(Application.loadedLevelName == "BattleScene")
				{
					// If in battle scene, keep battling until BattleScene ends itself
					Battle();
				} //end if
				else
				{
					// Only run once per turn
					if(!isProcessing)
					{
						// Accept or Decline ally if prompted
						if(GameObject.Find("Accept").activeInHierarchy)
						{
							Ally();
						} //end if Accept.activeInHierarchy
						else
						{
							Item();
						} //end else
					} //end if !isProcessing
				} //end else scene != "BattleScene"
			} //end else if
		} //end Update

		// Rolls the die or starts the turn
		void PressAction()
		{
			GPSM.ActionButton ();
			turnPhase++;
			isProcessing = false;
		} //end RollDie

		// Sets the target to move towards
		void SetTarget()
		{
			// If a target does not exist, create one
			if(merchant.Target == Vector3.zero)
			{
				// Value of the target resource
				int resourceValue = 0; 

				// Get all objects within move distance sphere on layer 9  (resource layer)
				float radius = GPSM.GetDist() * TileManager.PlayerMoveDistance - 0.31f;
				Collider[] hitColliders = Physics.OverlapSphere(merchant.Position, radius, 9);

				// Determine highest value if possible
				for (int index = 0; index < hitColliders.Length; index++)
				{
					// Get ResourceType of resource hit
					ResourceType resourceType = hitColliders[index].gameObject.GetComponent<ResourceTile>().Type;

					// Get worth/value of the resource
					Resource resource = (Resource)ItemDatabase.Instance.Items.Find(tempItem => 
						((Resource)tempItem).ResourceType == resourceType); 

					// If the value is higher than current target, replace it as the new target
					if(resourceValue < resource.Worth)
					{
						merchant.Target = hitColliders[index].transform.position;
						resourceValue = resource.Worth;
					};
				} //end for

				// Verify there is a target, otherwise set it to a village
				if(merchant.Target == Vector3.zero)
				{
					// Set target depending on the map
					if(GameMaster.Instance.BattleMap == BattleMap.area01)
					{
						merchant.Target = new Vector3(12.48f, -3.52f, -0.01f);
					} //end if
					else if(GameMaster.Instance.BattleMap == BattleMap.area02)
					{
						merchant.Target = Vector3.zero;
					} //end else if
					else if(GameMaster.Instance.BattleMap == BattleMap.area03)
					{
						merchant.Target = Vector3.zero;
					} //end else if
					else 
					{
						merchant.Target = Vector3.zero;
					} //end else
				} //end if
			} //end if
			else
			{
				// Make sure target still exists
				if (TileDictionary.ResourcePositions.Contains(merchant.Target))
				{
					return;
				} //end if
				// If not, pick a new target
				else
				{
					// Value of the target resource
					int resourceValue = 0; 
					
					// Get all objects within move distance sphere on layer 9  (resource layer)
					float radius = GPSM.GetDist() * TileManager.PlayerMoveDistance - 0.31f;
					Collider[] hitColliders = Physics.OverlapSphere(merchant.Position, radius, 9);
					
					// Determine highest value if possible
					for (int index = 0; index < hitColliders.Length; index++)
					{
						// Get ResourceType of resource hit
						ResourceType resourceType = hitColliders[index].gameObject.GetComponent<ResourceTile>().Type;
						
						// Get worth/value of the resource
						Resource resource = (Resource)ItemDatabase.Instance.Items.Find(tempItem => 
							((Resource)tempItem).ResourceType == resourceType); 
						
						// If the value is higher than current target, replace it as the new target
						if(resourceValue < resource.Worth)
						{
							merchant.Target = hitColliders[index].transform.position;
							resourceValue = resource.Worth;
						};
					} //end for
					
					// Verify there is a target, otherwise set it to a village
					if(merchant.Target == Vector3.zero)
					{
						// Set target depending on the map
						if(GameMaster.Instance.BattleMap == BattleMap.area01)
						{
							merchant.Target = new Vector3(12.48f, -3.52f, -0.01f);
						} //end if
						else if(GameMaster.Instance.BattleMap == BattleMap.area02)
						{
							merchant.Target = new Vector3(12.48f, -3.52f, -0.01f);
						} //end else if
						else if(GameMaster.Instance.BattleMap == BattleMap.area03)
						{
							merchant.Target = new Vector3(9.28f, -0.32f, -0.01f);
						} //end else if
						else 
						{
							merchant.Target = new Vector3(12.48f, -3.52f, -0.01f);
						} //end else
					} //end if
				} //end else
			} //end else
		} //end SetTarget

		// Moves the AI towards the target
		void Move()
		{
			// Move while there are still moves to make
			if(GPSM.GetDist() > 0)
			{
				float x = merchant.Position.x - merchant.Target.x;
				float y = merchant.Position.y - merchant.Target.y;
				// Move horizontally if not aligned with target
				if(x != 0)
				{
					if(x > 0)
					{
						GUIM.MoveLeft();
					} //end if
					else
					{
						GUIM.MoveRight();
					} //end else
				} //end if
				// Move vertically if not aligned with target 
				else if(y != 0)
				{
					if(y > 0)
					{
						GUIM.MoveDown();
					} //end if
					else
					{
						GUIM.MoveUp();
					} //end else
				} //end else if
				// End turn if aligned with target
				else
				{
					merchant.Target = Vector3.zero;
					GPSM.ActionButton();
					turnPhase++;
					isProcessing = false;
				}
			} //end if
			// End turn when out of moves
			else
			{
				GPSM.ActionButton();
				turnPhase++;
				isProcessing = false;
			} //end else
		} //end Move

		// Determines what attack AI will use to defeat enemy
		void Battle()
		{
			// Get active battle component
			Battle battle = GameObject.Find ("Canvas").GetComponent<Battle> ();

			// Determine what attack to use
			if(merchant.Health > 40)
			{
				battle.Head();
			} //end if
			else if((merchant.Health < 40) && (merchant.Health > 15))
			{
				battle.Torso();
			} //end else if
			else
			{
				battle.Feint();
			} //end else
		} //end Battle

		// Determines whether AI will accept ally or not
		void Ally()
		{
			// If AI has an ally, decline ally
			if(merchant.NumAllies != 0)
			{
				GPSM.No();
			} //end if
			// If AI has no allies, accept ally
			else
			{
				GPSM.Yes();
			} //end else
		} //end Ally

		// Determines if a better item is in inventory
		void Item()
		{
			// Get AI's inventory and all items in it
			PlayerInventory inventory = GameObject.Find ("PlayerInventory").GetComponent<PlayerInventory>();
			List<Item> equipment = inventory.GetItems(GameMaster.Instance.Turn).
				FindAll(tempItem => tempItem is Equipment);
				
			// Loop through item list
			foreach (Item item in equipment)
			{
				if (item is Weapon)
				{
					// Cast the item to a weapon
					Weapon weapon = (Weapon)item;
					
					// Compare with equipped
					if(weapon.AttackValue > merchant.AttackPower)
					{
						inventory.EquipItem(GameMaster.Instance.Turn, (Equipment)weapon);
					} //end if weapon.AttackValue > merchant.AttackPower
				} //end if item is Weapon
				else if (item is Armor)
				{
					// Cast the item to an armor
					Armor armor = (Armor)item;
					
					// Compare with equipped
					if(armor.DefenceValue > merchant.DefencePower)
					{
						inventory.EquipItem(GameMaster.Instance.Turn, (Equipment)armor);
					} //end if
				} //end else if
			} //end foreach
		} //end Item
	} // end class AI
} // end namespace GSP.Char.AI