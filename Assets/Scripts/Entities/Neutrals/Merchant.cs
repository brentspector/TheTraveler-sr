using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Tiles;
using GSP.Char;
using GSP.Entities.Interfaces;

namespace GSP.Entities.Neutrals
{
    // The enumeration of the directions a character can be facing.
    // This is used for displaying the correct sprite.
    public enum FacingDirection { NORTH, EAST, SOUTH, WEST };
    
    public class Merchant : Entity, IInventory, IEquipment, IDamageable
    {
        #region IInventory Variables

        // Variables will be defined in week 4.

        #endregion

        #region IEquipment Variables

        // Variables will be defined in week 4.

        #endregion

        #region IDamageable Variables

        // Variables will be defined in week 4.

        #endregion

        PlayerColours   m_colour;           // Holds the colour of the merchant
        List<Sprite> m_CharSprites;			// Holds the sprites for the character.
        SpriteRenderer m_spriteRenderer;    // SpriteRenderer component of the character.

        Ally m_allyScript;					// This is the ally script object.

        public Merchant(int ID, GameObject gameObject, PlayerColours playerCoulours, string playerName) :
            base(ID, gameObject)
        {
            // Set the entity's type to merchant.
            Type = EntityType.ENT_MERCHANT;

            // Set merchant's name.
            Name = playerName;

            // Set the merchant's colour.
            m_colour = playerCoulours;

            // Create a new list of sprites.
            m_CharSprites = new List<Sprite>();
            // Get the game object's sprite renderer.
            m_spriteRenderer = GameObj.GetComponent<SpriteRenderer>();

            // Get the game object's ally script.
            m_allyScript = GameObj.GetComponent<Ally>();

            #region IInventory Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion

            #region IEquipment Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion

            #region IDamageable Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion

        }

        // Gets the merchant's colour.
        public PlayerColours Colour
        {
            get
            {
                return m_colour;
            }
        }

        // Setup the character's sprite set. This is an array of sprites that will be used for the character.
        public void SetCharacterSprites(int playerNumber)
        {
            // A temporary sprite array.
            Sprite[] tmp = UnityEngine.Resources.LoadAll<Sprite>("player" + playerNumber);

            // Add the idle sprites for each direction.
            m_CharSprites.Add(tmp[1]);
            m_CharSprites.Add(tmp[4]);
            m_CharSprites.Add(tmp[7]);
            m_CharSprites.Add(tmp[10]);
        }

        // Sets the character's sprite to the given index.
        void SetSprite(int index)
        {
            m_spriteRenderer.sprite = m_CharSprites[index];
        } // end SetSprite function

        // Faces the merchant in a given direction. This changes the merchant's sprite to match this.
        public void Face(FacingDirection facingDirection)
        {
            // Get the box collider of the character.
            var boxCollider = GameObj.GetComponent<BoxCollider2D>();

            // Set the box collider smaller to fix for the scaling fix.
            boxCollider.size = new Vector2(0.18f, 0.2f);

            // Switch over the selection.
            switch (facingDirection)
            {
                case FacingDirection.NORTH:
                    // Change the character's sprite to face the north direction.
                    SetSprite(0);

                    // Using the object's transform, scale to fix the small sprite issue.
                    GameObj.transform.localScale = new Vector3(1.66f, 1.56f, 1.0f);
                    break;
                case FacingDirection.EAST:
                    // Change the character's sprite to face the east direction.
                    SetSprite(1);

                    // Using the object's transform, scale to fix the small sprite issue.
                    GameObj.transform.localScale = new Vector3(1.41f, 1.56f, 1.0f);
                    break;
                case FacingDirection.SOUTH:
                    // Change the character's sprite to face the south direction.
                    SetSprite(2);

                    // Using the object's transform, scale to fix the small sprite issue.
                    GameObj.transform.localScale = new Vector3(1.66f, 1.56f, 1.0f);
                    break;
                case FacingDirection.WEST:
                    // Change the character's sprite to face the west direction.
                    SetSprite(3);

                    // Using the object's transform, scale to fix the small sprite issue.
                    GameObj.transform.localScale = new Vector3(1.40f, 1.56f, 1.0f);
                    break;
            }
        }

        #region Ally
        // Gets the number of allies the character has.
        public int NumAllies
        {
            get { return m_allyScript.NumAllies; }
        } // end NumAllies function
        #endregion

        // The below interfaces will be implemented in Week 4.

        #region IInventory Members

        public ResourceList Resources
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TotalWeight
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TotalSize
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TotalValue
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int MaxWeight
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int MaxInventorySpace
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int Currency
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void PickupResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void SellResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void SellResources()
        {
            throw new System.NotImplementedException();
        }

        public void TransferCurrency(GameObject other, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void TransferResource(GameObject other, Resource resource)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IEquipment Members

        public int DefencePower
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public EquippedArmor EquippedArmour
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public List<GameObject> Bonuses
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void EquipArmour(string item)
        {
            throw new System.NotImplementedException();
        }

        public void UnequipArmour(string item)
        {
            throw new System.NotImplementedException();
        }

        public int AttackPower
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public EquippedWeapon EquippedWeapon
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void EquipWeapon(string item)
        {
            throw new System.NotImplementedException();
        }

        public void UnequipWeapon(string item)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IDamageable Members

        public int Health
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int MaxHealth
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void ResetHealth()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
