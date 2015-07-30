/*******************************************************************************
 *
 *  File Name: IWeapon
 *
 *  Description: Describes a contract for weapon functionality
 *
 *******************************************************************************/

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
        void EquipWeapon(string item);

        // Unequip a weapon
        void UnequipWeapon(string item);

        #endregion

        #region Properties

        // The attack power given from the weapon
        int AttackPower { get; set; }

        // The weapon object equipped
        Char.EquippedWeapon EquippedWeapon { get; set; }

        #endregion
    } // end IWeapon
} //end GSP.Entities.Interfaces
