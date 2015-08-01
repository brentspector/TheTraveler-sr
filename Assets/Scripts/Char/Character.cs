/*******************************************************************************
 *
 *  File Name: Character.cs
 *
 *  Description: Old construct used for enemies/allies/players
 *
 *******************************************************************************/
using GSP.Tiles;
using System.Collections.Generic;
using UnityEngine;

//TODO: Damien: Replace wit new constructs later

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: Character
     * 
     * Description: Manages all the characters.
     * 
     *******************************************************************************/
    public class Character : MonoBehaviour
	{
		ResourceList resources;		    // The ResourceList script reference
		Ally allyScript;				// The Ally script reference
		int maxWeight;					// The maximum weight the character can hold
		int maxInventory;				// The maximum inventory (max number player can hold)
		int currency; 					// The amount of currency the character is holding
		int attackPower;				// The attack of the character (from weapons)
		int defencePower;				// The defence of the character (from armor)
		EquippedWeapon weapon;			// The script component reference to the weapon being wielded
		EquippedArmor armor;			// The script component reference to the armor being worn
		List<GameObject> bonuses;		// The bonuses picked up (Inventory and Weight mods)
		List<Sprite> charSprites;		// The Sprite's for the Character
		SpriteRenderer spriteRenderer;  // The SpriteRenderer component reference of the Character

		// Called before Start(); Fixes the starter item issues
		void Awake()
		{
			// Initialise the weapons and armour
			weapon = GetComponent<EquippedWeapon>();
			armor = GetComponent<EquippedArmor>();
			charSprites = new List<Sprite>();
			spriteRenderer = GetComponent<SpriteRenderer>();
		} // end Awake

		// Use this for initialisation
		void Start()
		{
			// Initialise the variables
			resources = GetComponent<ResourceList>();
			allyScript = GetComponent<Ally>();
			maxWeight = 300;
			maxInventory = 20;
			currency = 0;
			attackPower = 0;
			defencePower = 0;
			bonuses = new List<GameObject>();
		} // end Start

		// Attempts to pick up a resource for the character
		public void PickupResource(Resource resource, int amount)
		{
			// Check if picking up this resource will put the Character overweight
			if ((ResourceWeight + resource.WeightValue) * amount <= MaxWeight)
			{
				// Check if there is enough room for this resource
				if(resources.TotalSize + resource.SizeValue <= MaxInventory)
				{
					// Add the resource
                    resources.AddResource(resource, amount);

					// Get the resource's position
					Vector3 tmp = transform.localPosition;
					// Change the z to make tiles work
					tmp.z = -0.01f;
                    // Remove the resource from the map
                    TileDictionary.RemoveResource(TileManager.ToPixels(tmp));
				} // end if
				else
				{
                    Debug.Log("Pickup failed. Max inventory capacity reached.");
				} // end else
			} // end if
			else
			{
				print( "Pickup failed. Max inventory weight reached." );
			} // end else
		} // end PickupResource

		// Sells the resources the Character is currently holding
		public void SellResource()
		{
			// Credit the Character for the resources they are holding
			Currency += resources.TotalValue;

			// Clear the resources now.
			resources.ClearResources();
		} // end SellResource

		// Transfers currency to another Character
		public void TransferCurrency(GameObject other, int amount)
		{
			// Get the ammount for clamping later
			int amt = amount;

			// Get the Character script attached the the other Character GameObject
			Character charScript = other.GetComponent<Character>();

			// Check if the script exists
            if (charScript == null)
			{
				// Simply return
				return;
			} // end if

			// Only proceed if the amount is greater than zero
            if (amt > 0)
			{
				// Check if the ammount is greater than the Character's currency
                if (amt > this.Currency)
				{
					// Clamp it to the character's currency if so
					amt = this.Currency;

				} // end if

				// Add the amount of currency to the other Character
				charScript.Currency += amt;

				// Subtract the amount of currency from the Character this is attached to
				this.Currency -= amt;
			} // end if
		} // end TransferCurrency

		// Transfers a resource to another character
		public void TransferResource(GameObject other, Resource resource)
		{
			// Check if the resource object exists
            if (resource == null)
			{
				// Simply return
				return;
			} // end if

			// Get the ResourceList script attached the other Character GameObject
			ResourceList otherResourceScript = other.GetComponent<ResourceList>();
			// Get the Character script attached the other Character GameObject
			Character otherCharacterScript = other.GetComponent<Character>();
			// Get the ResourceList script attached the Character this is attached to
			ResourceList charResourceScript = this.gameObject.GetComponent<ResourceList>();
			
			// Check if the script objects exists
            if (otherResourceScript == null || charResourceScript == null || otherCharacterScript == null)
			{
				// Simply return
                Debug.LogWarning("The script references in TransferResource() are null!");
				return;
			} // end if

			// Check if the other Character recieving this resource will put the Character overweight
			if (otherResourceScript.TotalWeight + resource.WeightValue <= otherCharacterScript.MaxWeight)
			{
				// Check if there is enough room for this resource
				if(otherResourceScript.TotalSize + resource.SizeValue <= otherCharacterScript.MaxInventory)
				{
					// Add the resource to the other Character
					otherResourceScript.AddResource(resource, 1);

					// Remove the resource from the Character this is attached to
					charResourceScript.RemoveResource(resource);
				} // end if
				else
				{
                    Debug.Log("Transfer failed. Their max inventory capacity reached.");
				} // end else
			} // end if
			else
			{
                Debug.LogFormat("WEIGHT: {0}", otherResourceScript.TotalWeight);
                Debug.LogFormat("MAX WEIGHT: {0}", otherCharacterScript.MaxWeight);
                Debug.LogFormat("THIS MAX WEIGHT: {0}", this.MaxWeight);
                Debug.LogFormat("ALLY MAX WEIGHT: {0}", allyScript[0].gameObject.GetComponent<Character>().MaxWeight);
                Debug.Log("Transfer failed. Their max inventory weight reached.");
			} // end else weight
		} // end TransferResource

		//TODO: Damien: Doesn't seem to work right
        //Equips custom or predefined items
		//NOTE!!
		//For custom items, the "item" is the stat you want to change
		//and the "value" is what you are changing it by, which can be
		//a positive or negative value
        public void EquipItem(string item, int value = 0)
		{
			// The Item GameObject
            GameObject itemObject = Instantiate(PrefabReference.prefabItem, this.transform.position, new Quaternion()) as GameObject;
			
			// Get its script
            Item itemScript = itemObject.GetComponent<Item>();

			// Assign values to a custom item
			if(value != 0)
			{
                Debug.Log("Custom item detected.");
				itemScript.Name = "CustomItem-" + item;

                // Switch over the items
                switch (item.ToLower())
                {
                    case "attack":
                        {
                            itemScript.Type = "Weapon";
                            itemScript.AttackValue += value;
                            break;
                        } // end case attack
                    case "defence":
                        {
                            itemScript.Type = "Armor";
                            itemScript.DefenceValue += value;
                            break;
                        } // end case defence
                    case "inventory":
                        {
                            itemScript.Type = "Inventory";
                            itemScript.InventoryValue += value;
                            break;
                        } // end case inventory
                    case "weight":
                        {
                            itemScript.Type = "Weight";
                            itemScript.WeightValue += value;
                            break;
                        } // end case weight
                    default:
                        {
                            Destroy(itemObject);
                            Debug.LogWarningFormat("No stat of {0} type found. Valid types are attack, defence, inventory, and weight.", item);
                            break;
                        } // end case default
                } // end switch item
			} // end if
			// Assign values to a predefined item
			else
			{
                Debug.Log("Predefined item detected.");
				// Check if the item was found
                if(itemScript.SetItem(item) != "NAN")
				{
					// The item was found so set the item
                    item = itemScript.SetItem(item);
				} // end if
				else
				{
					// No item found
                    Debug.LogWarningFormat("No item of {0} name found.", item);
					Destroy(itemObject);
				} // end else SetItem != NAN
			} //end else value != 0

            // Switch over the items for predefined
            switch (item.ToLower())
            {
                case "attack":
                    {
                        Debug.Log("Attack item detected.");
                        //If a weapon already exists, let player decide to accept or refuse
                        // Instead of a null check, check if the attack value of the EquippedWeapon has been set
                        if (weapon.AttackValue > 0)
                        {
                            Debug.Log("Do you want this weapon? Hit y for yes and n for no.");
                            if (Input.GetKeyDown(KeyCode.Y))
                            {
                                //Clean up old weapon, then apply new
                                AttackPower -= weapon.AttackValue;
                                AttackPower += itemScript.AttackValue;
                                // Copy the values of the item object into the equipped weapon script
                                CopyItemToWeapon(itemObject);
                            } // end if
                            else if (Input.GetKeyDown(KeyCode.N))
                            {
                                // Destroy itemObject
                                Destroy(itemObject);
                            } // end else if
                        } // end if
                        else
                        {
                            print("Attack power before equip: " + AttackPower);
                            AttackPower += itemScript.AttackValue;
                            // Copy the values of the item object into the equipped weapon script
                            CopyItemToWeapon(itemObject);
                            print("Item is " + itemObject.GetComponent<Item>().Type);
                            print("Held weapon is " + weapon.Name);
                            print("Attack power after equip: " + AttackPower);
                        } //end else
                        break;
                    } // end case attack
                case "defence":
                    {
                        print("Defence item detected.");
                        //If armour already exsist, allow player to accept or refuse
                        // Instead of a null check, check if the defence value of the equipped armor has been set
                        if (armor.DefenceValue > 0)
                        {
                            print("Do you want this armor? Hit y for yes and n for no.");
                            if (Input.GetKeyDown(KeyCode.Y))
                            {
                                //Clean up old armor, then apply new
                                DefencePower -= armor.DefenceValue;
                                DefencePower += itemScript.DefenceValue;
                                // Copy the values of the item object into the equipped armor script
                                CopyItemToArmor(itemObject);
                            }
                            else if (Input.GetKeyDown(KeyCode.N))
                            {
                                // Destroy itemScript
                                Destroy(itemObject);
                            } //end else if
                        } //end if
                        else
                        {
                            DefencePower += itemScript.DefenceValue;
                            // Copy the values of the item object into the equipped armor script
                            CopyItemToArmor(itemObject);
                        } //end else
                        break;
                    } // end case defence
                case "inventory":
                    {
                        Debug.Log("Inventory item detected.");
                        // Add the value to bonuses
                        bonuses.Add(itemObject);
                        MaxInventory += itemScript.InventoryValue;
                        break;
                    } // end case inventory
                case "weight":
                    {
                        Debug.Log("Weight item detected.");
                        // Add the value to bonuses
                        bonuses.Add(itemObject);
                        MaxWeight += itemScript.WeightValue;
                        break;
                    } // end case weight
                default:
                    {
                        //Explain error, destroy itemScript, then return
                        Debug.LogWarningFormat("Stat value {0} does not exist.", item);
                        Destroy(itemObject);
                        return;
                    } // end case default
            } // end switch item
		} // end EquipItem

		// Unequips an item from the Character
        public void RemoveItem(string item)
		{
            // Switch over the items
            switch (item.ToLower())
            {
                case "attack":
                    {
                        Debug.Log("Attempting to remove weapon.");
                        // Verify weapon is equipped
                        // This means instead of a null check, use the attack value
                        if (weapon.AttackValue > 0)
                        {
                            Debug.LogFormat("Attack power before unequip: {0}", AttackPower);
                            Debug.LogFormat("{0} removed.", weapon.Name);
                            AttackPower -= weapon.AttackValue;
                            // Unequipping the weapon effective resets the values of the script
                            weapon.ResetWeapon();

                            Debug.LogFormat("Attack power after unequip: {0}", AttackPower);
                        } // end if
                        else
                        {
                            Debug.Log("No weapon equipped.");
                        } // end else
                        break;
                    } // end case attack
                case "defence":
                    {
                        // Verify armor is equipped
                        // This means instead of a null check, use the defence value
                        if (armor.DefenceValue > 0)
                        {
                            Debug.LogFormat("{0} removed.", armor.Name);
                            DefencePower -= armor.DefenceValue;
                            // Unequipping the weapon effective resets the values of the script
                            armor.ResetArmor();
                        } // end if
                        else
                        {
                            Debug.Log("No armor equipped.");
                        } // end else
                        break;
                    } // end case defence
                case "inventory":
                    {
                        GameObject m_item = bonuses.Find(x => x.GetComponent<Item>().Type == "Inventory");
                        MaxInventory -= m_item.GetComponent<Item>().InventoryValue;
                        bonuses.Remove(m_item);
                        break;
                    } // end case inventory
                case "weight":
                    {
                        GameObject m_item = bonuses.Find(x => x.GetComponent<Item>().Type == "Weight");
                        MaxWeight -= m_item.GetComponent<Item>().WeightValue;
                        bonuses.Remove(m_item);
                        break;
                    } // end case weight
                default:
                    {
                        Debug.LogWarningFormat("No item of {0} stat found. Valid types are weapon, armor, inventory, and weight.", item);
                        break;
                    } // end case default
            } // end switch item
		} // end RemoveItem

		// Copies information from the item game object to the weapon script
		// NOTE: The source is the item game object
		void CopyItemToWeapon(GameObject source)
		{
			// Get the item's script
			var sourceScript = source.GetComponent<Item>();

			// Copy the values from the source GameObject to the weapon script
			weapon.Name = sourceScript.Name;
			weapon.Type = sourceScript.Type;
			weapon.AttackValue = sourceScript.AttackValue;
			weapon.DefenceValue = sourceScript.DefenceValue;
			weapon.InventoryValue = sourceScript.InventoryValue;
			weapon.WeightValue = sourceScript.WeightValue;
			weapon.CostValue = sourceScript.CostValue;
		} // end CopyItemToWeapon

		// Copies information from the item game object to the armor script
		// NOTE: The source is the item game object
		void CopyItemToArmor(GameObject source)
		{
			// Get the Item's script
			var sourceScript = source.GetComponent<Item>();
			
			// Copy the values from the object to the weapon script
			armor.Name = sourceScript.Name;
			armor.Type = sourceScript.Type;
			armor.AttackValue = sourceScript.AttackValue;
			armor.DefenceValue = sourceScript.DefenceValue;
			armor.InventoryValue = sourceScript.InventoryValue;
			armor.WeightValue = sourceScript.WeightValue;
			armor.CostValue = sourceScript.CostValue;
		} // end CopyItemToArmor

		// Allows for collision on the market place to end the game
		void OnCollisionEnter2D(Collision2D coll)
		{
			// Layer 8 is "Market"
            if (coll.gameObject.layer == 8)
			{
				// Get the GameObject with the GameStateMachineTag tag
                GameObject obj = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag");

				// Now get its script.
				var stateMachineScript = obj.GetComponent<GameplayStateMachine>();

				// Finally end the game by calling EndGame()
				stateMachineScript.EndGame();
			} // end if
		} // end OnCollisionEnter2D

		// Setup the Character's Sprite set. This is an array of Sprites that will be used for the Character
		public void SetCharacterSprites(int playerNumber)
		{
			// A temporary Sprite array
			Sprite[] tmp = Resources.LoadAll<Sprite>("player" + playerNumber);

			// Add the idle Sprites for each direction
			charSprites.Add(tmp[1]);
			charSprites.Add(tmp[4]);
			charSprites.Add(tmp[7]);
			charSprites.Add(tmp[10]);
		} // end SetCharacterSprites

		// Sets the Character's Sprite to the given index
		void SetSprite(int index)
		{
			spriteRenderer.sprite = charSprites[index];
		} // end SetSprite

		// Faces a character in a given direction. This changes the character's sprite to match this
		public void Face(FacingDirection facingDirection)
		{
			// Get the BoxCollider2D component of the Character
			var boxCollider = GetComponent<BoxCollider2D>();

			// Set its size to fix for the scaling fix
            boxCollider.size = new Vector2(0.18f, 0.2f);

			// Switch over the selection
			switch (facingDirection)
			{
				case FacingDirection.North:
                    {
                        // Change the Character's Sprite to face the north direction
                        SetSprite(0);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        transform.localScale = new Vector3(1.66f, 1.56f, 1.0f);
                        break;
                    } // end case North
				case FacingDirection.East:
                    {
                        // Change the Character's Sprite to face the east direction
                        SetSprite(1);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        transform.localScale = new Vector3(1.41f, 1.56f, 1.0f);
                        break;
                    } // end case East
				case FacingDirection.South:
                    {
                        // Change the Character's Sprite to face the south direction
                        SetSprite(2);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        transform.localScale = new Vector3(1.66f, 1.56f, 1.0f);
                        break;
                    } // end case South
				case FacingDirection.West:
                    {
                        // Change the Character's Sprite to face the west direction
                        SetSprite(3);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        transform.localScale = new Vector3(1.40f, 1.56f, 1.0f);
                        break;
                    } // end case West
			} // end Switch
		} // end Face

        // Gets the current value of resources the Character is holding
        public int ResourceValue
        {
            get { return resources.TotalValue; }
        } // end ResourceValue

        // Gets the current weight of resources the Character is holding
        public int ResourceWeight
        {
            get { return resources.TotalWeight; }
        } // end ResourceWeight

        // Gets the current size of resources the cCaracter is holding
        public int ResourceSize
        {
            get { return resources.TotalSize; }
        } // end ResourceSize

        // Gets the number of allies the Character has
        public int NumAllies
        {
            get { return allyScript.NumAllies; }
        } // end NumAllies

        // Gets and Sets the maximum weight the Character can hold
        public int MaxWeight
        {
            get { return maxWeight; }
            set
            {
                // Set to value
                maxWeight = value;

                // Check if the MaxWeight is less than zero
                if (value < 0)
                {
                    // Clamp to zero
                    maxWeight = 0;
                } // end if
            } // end set
        } // end MaxWeight

        // Gets and Sets the maximum inventory slots the Character has
        public int MaxInventory
        {
            get { return maxInventory; }
            set
            {
                // Set to value
                maxInventory = value;

                // Check if the MaxInventory is less than zero
                if (value < 0)
                {
                    // Clamp to zero
                    maxInventory = 0;
                } // end if
            } // end set
        } // end MaxInventory

        // Gets and Sets the currency a Character is holding
        public int Currency
        {
            get { return currency; }
            set
            {
                // Set to value
                currency = value;

                // Check if the Currency is less than zero
                if (currency < 0)
                {
                    // Clamp to zero
                    currency = 0;
                } // end if
            } // end set
        } // end Currency

        // Gets and Sets the attack power of Character
        public int AttackPower
        {
            get { return attackPower; }
            set
            {
                // Set to value
                attackPower = value;

                // Check if the AttackPower is less than zero
                if (attackPower < 0)
                {
                    // Clamp to zero
                    attackPower = 0;
                } // end if
            } // end set
        } // end AttackPower

        //Gets and Sets the defence power of the Character.
        public int DefencePower
        {
            get { return defencePower; }
            set
            {
                // Set to value
                defencePower = value;

                // Check if the DefensePower is less than zero
                if (defencePower < 0)
                {
                    // Clamp to zero
                    defencePower = 0;
                } // end if
            } // end set
        } // end DefencePower
	} // end Character
} // end GSP.Char
