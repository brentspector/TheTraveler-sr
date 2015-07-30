/*******************************************************************************
 *
 *  File Name: IDamageable.cs
 *
 *  Description: Describes a contract for damageable entity behaviour
 *
 *******************************************************************************/

namespace GSP.Entities.Interfaces
{
    /*******************************************************************************
     *
     * Name: IDamageable
     * 
     * Description: Supplies the functionality for damageable entities. This
     *              includes health and a way to deal damage.
     * 
     *******************************************************************************/
    interface IDamageable
    {
        #region Functions

        // Allows an entity to take damage
        void TakeDamage(int damage);

        // Resets the health of the entity
        void ResetHealth();

        #endregion

        #region Properties

        // The health of the entity
        int Health { get; set; }

        // The maximum health of the entity
        int MaxHealth { get; set; }

        #endregion
    } // end IDamageable
} // end GSP.Entities.Interfaces