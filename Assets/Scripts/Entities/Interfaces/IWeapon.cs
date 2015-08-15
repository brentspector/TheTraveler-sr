/*******************************************************************************
 *
 *  File Name: IWeapon
 *
 *  Description: Describes a contract for weapon functionality
 *
 *******************************************************************************/
using GSP.Items;

namespace GSP.Entities.Interfaces
{
    /*******************************************************************************
     *
     * Name: IWeapon
     * 
     * Description: Supplies the functionality for equipping weapons. This includes
     *              adding attack power.
     * 
     *******************************************************************************/
    interface IWeapon
    {
        #region Functions

        // Equip a weapon
        void EquipWeapon(Weapon weapon);

        // Unequip a weapon
        void UnequipWeapon(Weapon weapon);

        #endregion

        #region Properties

        // The attack power given from the weapon
        int AttackPower { get; set; }

        // The weapon object equipped
        Weapon EquippedWeapon { get; }

        #endregion
    } // end IWeapon
} //end GSP.Entities.Interfaces
