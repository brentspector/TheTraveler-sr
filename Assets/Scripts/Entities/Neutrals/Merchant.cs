/*******************************************************************************
 *
 *  File Name: Merchant.cs
 *
 *  Description: This is entity the player will be playing as
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Entities.Interfaces;
using GSP.Items;
using System.Collections.Generic;
using UnityEngine;
using GSP.Tiles;

namespace GSP.Entities.Neutrals
{
    /*******************************************************************************
     *
     * Name: Merchant
     * 
     * Description: The Merchant class is the playable class.
     * 
     *******************************************************************************/
    public class Merchant : Entity, IInventory, IEquipment, IDamageable
    {
        #region IInventory Variables

        int maxWeight;		    // The maximum weight the entity can hold
        int maxInventory;       // The maximum inventory spaces (max number of spaces an entity can hold)
        int currency; 		    // The amount of currency the entity is holding
        ResourceList resources; // The ResourceList script reference

        #endregion

        #region IEquipment Variables

        int defencePower;       // The defence of the entity (from armor)
        int attackPower;	    // The attack of the entity (from weapons)

        List<Bonus> bonuses;    // The bonuses picked up (Inventory and Weight mods)
        Armor equippedArmor;    // The piece of armor that is being worn.
        Weapon equippedWeapon;  // The weapon that is being wielded.

        #endregion

        #region IDamageable Variables

        int health;     // The current health the entity has
        int maxHealth;  // THe maximum health the entity has
        bool isDead;    // Whether the entity is dead

        #endregion

        InterfaceColors color;          // The colour of the Merchant
        List<Sprite> charSprites;		// The Sprite's for the Character
        SpriteRenderer spriteRenderer;  // SpriteRenderer component of the Character

        Ally allyScript;				// The ally script object

        // Creates a Merchant entity
        public Merchant(int ID, GameObject gameObject, InterfaceColors playerCoulours, string playerName) :
            base(ID, gameObject)
        {
            // Set the entity's type to Merchant
            Type = EntityType.Merchant;

            // Set Merchant's name
            Name = playerName;

            // Set the Merchant's colour
            color = playerCoulours;

            // Create a new list of Sprite's
            charSprites = new List<Sprite>();
            // Get the GameObject's SpriteRenderer
            spriteRenderer = GameObj.GetComponent<SpriteRenderer>();

            // Get the GameObject's ally script
            allyScript = GameObj.GetComponent<Ally>();

            #region IInventory Variable Initialisation

            // The default for now - hard coded values
            maxWeight = 300;
            maxInventory = 20;
            
            // The entity starts with no currency
            currency = 0;

            // Get the ResourceList component reference
            resources = GameObj.GetComponent<ResourceList>();

            #endregion

            #region IEquipment Variable Initialisation

            // The entity isn't wearing any armour, wielding any weapon, or has any bonuses
            equippedArmor = null;
            equippedWeapon = null;
            bonuses = new List<Bonus>();
            attackPower = 0;
            defencePower = 0;

            #endregion

            #region IDamageable Variable Initialisation

            // The default hard coded values for now.
            health = 50;
            maxHealth = 50;
            isDead = false;

            #endregion

        }

        // Setup the Character's Sprite set. This is an array of Sprites that will be used for the Character
        public void SetCharacterSprites(int playerNumber)
        {
            // A temporary Sprite array
            Sprite[] tmp = UnityEngine.Resources.LoadAll<Sprite>("player" + playerNumber);

            // Add the idle sprites for each direction
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

        // Faces the Merchant in a given direction; This changes the Merchant's Sprite to match this
        public void Face(FacingDirection facingDirection)
        {
            // Get the box collider of the character
            var boxCollider = GameObj.GetComponent<BoxCollider2D>();

            // Set the BoxCollider2D component size smaller to fix for the scaling fix
            boxCollider.size = new Vector2(0.18f, 0.2f);

            // Switch over the selection
            switch (facingDirection)
            {
                case FacingDirection.North:
                    {
                        // Change the Character's Sprite to face the north direction
                        SetSprite(0);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        GameObj.transform.localScale = new Vector3(1.66f, 1.56f, 1.0f);
                        break;
                    } // end case North
                case FacingDirection.East:
                    {
                        // Change the Character's Sprite to face the east direction
                        SetSprite(1);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        GameObj.transform.localScale = new Vector3(1.41f, 1.56f, 1.0f);
                        break;
                    } // end case East
                case FacingDirection.South:
                    {
                        // Change the Character's Sprite to face the south direction
                        SetSprite(2);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        GameObj.transform.localScale = new Vector3(1.66f, 1.56f, 1.0f);
                        break;
                    } // end case South
                case FacingDirection.West:
                    {
                        // Change the Character's Sprite to face the west direction
                        SetSprite(3);

                        // Using the GameObject's transform, scale to fix the small Sprite issue
                        GameObj.transform.localScale = new Vector3(1.40f, 1.56f, 1.0f);
                        break;
                    } // end case West
            } // end switch facingDirection
        } // end Face

        // Gets the Merchant's colour
        public InterfaceColors Color
        {
            get { return color; }
        } // end Color

        // Gets the number of allies the Character has
        public int NumAllies
        {
            get { return allyScript.NumAllies; }
        } // end NumAllies

        #region IInventory Members

        // Picks up a resource for an entity adding it to their ResourceList
        public bool PickupResource(Resource resource, int amount, bool isFromMap = true)
        {
            // Check if picking up this resource will put the entity overweight
            if ((TotalWeight + resource.WeightValue) * amount <= MaxWeight)
            {
                // Check if there is enough room for this resource
                if (resources.TotalSize + resource.SizeValue <= MaxInventorySpace)
                {
                    // Add the resource
                    resources.AddResource(resource, amount);

                    // Check if the resource is from the map
                    if (isFromMap)
                    {
                        // Get the resource's position
                        Vector3 tmp = GameObj.transform.localPosition;
                        // Change the z to make tiles work
                        tmp.z = -0.01f;
                        // Remove the resource from the map
                        TileDictionary.RemoveResource(TileManager.ToPixels(tmp));
                    } // end if

                    // Return success
                    return true;
                } // end if
                else
                {
                    Debug.Log("Pickup failed. Max inventory capacity reached.");

                    // Return failure
                    return false;
                } // end else
            } // end if
            else
            {
                Debug.Log("Pickup failed. Max inventory weight reached.");

                // Return failure
                return false;
            } // end else
        } // end PickupResource

        // Sells a resource for an entity removing it from their ResourceList
        public void SellResource(Resource resource, int amount)
        {
            // A temporary list to hold the resources
            List<Resource> tmpResources = new List<Resource>();

            // The counter for the for loop below
            int count = 0;

            // Get all the resources of the given resource's type
            tmpResources = resources.GetResourcesByType(resource.Type.ToString());

            // Check if the returned number of resources is fewer than amount
            if (tmpResources.Count < amount)
            {
                // Set the counter to the number of resources found
                count = tmpResources.Count;
            } // end if
            else
            {
                // Set the counter to amount
                count = amount;
            } // end else

            // Loop over the list until we reach count
            for (int index = 0; index < count; index++)
            {
                // Credit the entity for the resource
                currency += tmpResources[index].SellValue;

                // Remove the resource from the list
                resources.RemoveResource(tmpResources[index]);
            } // end for
        } // end SellResource

        // Sells all resources for an entity clearing their ResourceList
        public void SellResources()
        {
            // Credit the entity for the resources they are holding
            currency += TotalValue;

            // Clear the ResourceList now
            resources.ClearResources();
        } // end SellResources

        // Transfers currency from the entity to another entity
        public void TransferCurrency(GameObject other, int amount)
        {
            // The clamped amount between zero and the entity's currency amount
            int transferAmount = Utility.ClampInt(amount, 0, currency);

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

        // Transfers a resource from the entity to another entity
        public void TransferResource(GameObject other, Resource resource)
        {
            throw new System.NotImplementedException();
        } // end TransferResource

        // Gets and Sets the list of resources of the entity
        public ResourceList Resources
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Resources

        // Gets and Sets the TotalWeight of the entity
        public int TotalWeight
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end TotalWeight

        // Gets and Sets the TotalSize of the entity
        public int TotalSize
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end TotalSize

        // Gets and Sets the TotalValue of the entity
        public int TotalValue
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end TotalValue

        // Gets and Sets the MaxWeight of the entity
        public int MaxWeight
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end MaxWeight

        // Gets and Sets the MaxInventorySpace of the entity
        public int MaxInventorySpace
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end MaxInventorySpace

        // Gets and Sets the Currency of the entity
        public int Currency
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Currency

        #endregion

        #region IEquipment Members

        // Equips a piece of armour for an entity
        public void EquipArmor(Armor armor)
        {
            // Check if the merchant is wearing armour
            if (equippedArmor == null)
            {
                // The merchant isn't wearing armor so just equip the given armour
                defencePower += armor.DefenceValue;
                equippedArmor = armor;
            } // end if
            else
            {
                // The merchant is already wearing armour so unequip it first
                UnequipArmor(equippedArmor);

                // Now equip the given armour
                defencePower += armor.DefenceValue;
                equippedArmor = armor;
            } // end else
        } // end EquipArmor

        // Unequips a piece of armour for an entity
        public void UnequipArmor(Armor armor)
        {
            // Only unequip the armour if the merchant is wearing any
            if (equippedArmor != null)
            {
                // Unequip the given armour
                defencePower -= armor.DefenceValue;
                equippedArmor = null;
            } // end if
        } // end UnequipArmor

        // Equips a weapon for an entity
        public void EquipWeapon(Weapon weapon)
        {
            // Check if the merchant is wielding a weapon
            if (equippedArmor == null)
            {
                // The merchant isn't wielding a weapon so just equip the given weapon
                attackPower += weapon.AttackValue;
                equippedWeapon = weapon;
            } // end if
            else
            {
                // The merchant is already wielding a weapon so unequip it first
                UnequipWeapon(equippedWeapon);

                // Now wield the given weapon
                attackPower += weapon.AttackValue;
                equippedWeapon = weapon;
            } // end else
        } // end EquipWeapon

        // Unequips a weapon for the entity
        public void UnequipWeapon(Weapon weapon)
        {
            // Only unequip the weapon if the merchant is wielding any
            if (equippedWeapon != null)
            {
                // Unequip the given weapon
                attackPower -= weapon.AttackValue;
                equippedWeapon = null;
            } // end if
        } // end UnequipWeapon

        // Gets and Sets the amount of defense the entity has
        public int DefencePower
        {
            get { return defencePower;}
            set { defencePower = Utility.ZeroClampInt(value);}
        } // end DefencePower

        // Gets the EquippedArmor of the entity
        public Armor EquippedArmor
        {
            get { return equippedArmor;}
        } // end EquippedArmor

        // Gets the bonuses the entity has
        public List<Bonus> Bonuses
        {
            get { return bonuses; }
        } // end Bonuses

        // Gets and Sets the how hard the the entity hits
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = Utility.ZeroClampInt(value); }
        } // end AttackPower

        // Gets the EquippedWeapon of the entity
        public Weapon EquippedWeapon
        {
            get { return equippedWeapon; }
        } // end EquippedWeapon

        #endregion

        #region IDamageable Members

        // Causes the entity to take damage; this is call by others
        public void TakeDamage(int damage)
        {
            // Only allow damage if the entity isn't dead
            if (!IsDead)
            {
                // Dish out the damage
                health -= damage;

                // Check if the entity is dead
                if (health == 0)
                {
                    // The entity is dead
                    isDead = true;
                } // end if health == 0
            } // end if
        } // end TakeDamage

        // Resets the health of the entity
        public void ResetHealth()
        {
            health = maxHealth;
            isDead = false;
        } // end ResetHealth

        // Gets the current health of the entity
        public int Health
        {
            get { return health; }
        } // end Health

        // Gets and Sets the maximum health of the entity
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = Utility.ZeroClampInt(value); }
        } // end MaxHealth

        // Gets whether the eneity is dead
        public bool IsDead
        {
            get { return isDead; }
        } // end IsDead

        #endregion
    } // end Merchant
} // end GSP.Entities.Neutrals
