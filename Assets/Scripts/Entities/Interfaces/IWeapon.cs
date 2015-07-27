using UnityEngine;
using System.Collections;

namespace GSP.Entities.Interfaces
{
    interface IWeapon
    {
        #region Properties

        // The attack power given from the weapon.
        int AttackPower { get; set; }

        // The weapon object equipped.
        Char.EquippedWeapon EquippedWeapon { get; set; }

        #endregion

        #region Functions

        // Equip a weapon.
        void EquipWeapon(string item);

        // Unequip a weapon.
        void UnequipWeapon(string item);

        #endregion
    }
}
