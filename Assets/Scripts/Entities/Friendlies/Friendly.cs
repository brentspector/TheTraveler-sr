/*******************************************************************************
 *
 *  File Name: Friendly.cs
 *
 *  Description: The base for all allies
 *
 *******************************************************************************/
using GSP.Entities.Interfaces;
using UnityEngine;

namespace GSP.Entities.Friendlies
{
    /*******************************************************************************
     *
     * Name: Friendly
     * 
     * Description: The base class for all friendlies a.k.a allies.
     * 
     *******************************************************************************/
    public abstract class Friendly : Entity, IDamageable
	{
        #region IDamageable Variables

        int health;     // The current health the entity has
        int maxHealth;  // THe maximum health the entity has
        bool isDead;    // Whether the entity is dead

        #endregion

        FriendlyType friendlyType;  // Holds the type value from the FriendlyType enumeration
        
        // Constructor; Derived classes create an entity object
        public Friendly(int ID, GameObject gameObject) : base(ID, gameObject)
		{
            #region IDamageable Variable Initialisation

            // The default hard coded values for now.
            health = 50;
            maxHealth = 50;
            isDead = false;

            #endregion
		} // end Friendly

        // Gets and protected sets the entity's FriendlyType
        public FriendlyType FriendlyType
        {
            get { return friendlyType; }
            protected set { friendlyType = value; }
        } // end FriendlyType

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
	} // end Friendly
} // end GSP.Entities.Friendlies
