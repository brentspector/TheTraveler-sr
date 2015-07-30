using UnityEngine;
using System.Collections;

namespace GSP.Entities.Interfaces
{
    interface IDamageable
    {
        #region Properties

        // The health of the entity.
        int Health { get; set; }
        
        // The maximum health of the entity.
        int MaxHealth { get; set; }

        #endregion

        #region Functions

        // Allows an entity to take damage.
        void TakeDamage(int damage);

        // Resets the health of the entity.
        void ResetHealth();

        #endregion
    }
}