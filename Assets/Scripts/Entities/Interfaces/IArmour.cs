using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Entities.Interfaces
{
    interface IArmour
    {
        #region Properties

        // The defense power given from the armour.
        int DefencePower { get; set; }

        // The armour object equipped.
        Char.EquippedArmor EquippedArmour { get; set; }

        // The list of bonuses granted by the armour.
        List<GameObject> Bonuses { get; set; }

        #endregion

        #region Functions

        // Equip a piece of armour.
        void EquipArmour(string item);

        // Unequip a piece of armour.
        void UnequipArmour(string item);

        #endregion
    }
}
