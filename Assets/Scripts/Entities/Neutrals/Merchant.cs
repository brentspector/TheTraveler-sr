/*******************************************************************************
 *
 *  File Name: Merchant.cs
 *
 *  Description: This is entity the player will be playing as
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Entities.Interfaces;
using System.Collections.Generic;
using UnityEngine;

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

        // Variables will be defined in week 4

        #endregion

        #region IEquipment Variables

        // Variables will be defined in week 4

        #endregion

        #region IDamageable Variables

        // Variables will be defined in week 4

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

            // Variable initialisation will be done in week 4

            #endregion

            #region IEquipment Variable Initialisation

            // Variable initialisation will be done in week 4

            #endregion

            #region IDamageable Variable Initialisation

            // Variable initialisation will be done in week 4

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

        // The below interfaces will be implemented in Week 4

        #region IInventory Members

        // Picks up a resource for an entity adding it to their ResourceList
        public void PickupResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        } // end PickupResource

        // Sells a resource for an entity removing it from their ResourceList
        public void SellResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        } // end SellResource

        // Sells all resources for an entity clearing their ResourceList
        public void SellResources()
        {
            throw new System.NotImplementedException();
        } // end SellResources

        // Transfers currency from the entity to another entity
        public void TransferCurrency(GameObject other, int amount)
        {
            throw new System.NotImplementedException();
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
        public void EquipArmor(string item)
        {
            throw new System.NotImplementedException();
        } // end EquipArmor

        // Unequips a piece of armour for an entity
        public void UnequipArmor(string item)
        {
            throw new System.NotImplementedException();
        } // end UnequipArmor

        // Equips a weapon for an entity
        public void EquipWeapon(string item)
        {
            throw new System.NotImplementedException();
        } // end EquipWeapon

        // Unequips a weapon for the entity
        public void UnequipWeapon(string item)
        {
            throw new System.NotImplementedException();
        } // end UnequipWeapon

        // Gets and Sets the amount of defense the entity has
        public int DefencePower
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end DefencePower

        // Gets and Sets the EquippedArmor of the entity
        public EquippedArmor EquippedArmor
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end EquippedArmor

        // Gets and Sets the bonuses the entity has
        public List<GameObject> Bonuses
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Bonuses

        // Gets and Sets the how hard the the entity hits
        public int AttackPower
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end AttackPower

        // Gets and Sets the EquppedWeapon of the entity
        public EquippedWeapon EquippedWeapon
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end EquippedWeapon

        #endregion

        #region IDamageable Members

        // Causes the entity to take damage; this is call by others
        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        } // end TakeDamage

        // Resets the health of the entity
        public void ResetHealth()
        {
            throw new System.NotImplementedException();
        } // end ResetHealth

        // Gets and Sets the current health of the entity
        public int Health
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Health

        // Gets and Sets the maximum health of the entity
        public int MaxHealth
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end MaxHealth

        #endregion
    }
}
