/*******************************************************************************
 *
 *  File Name: IEquipment
 *
 *  Description: Describes a contract for both weapon and armour functionality
 *
 *******************************************************************************/

namespace GSP.Entities.Interfaces
{
    // Note: Use this if you want to use both Armour and Weapons.
    /*******************************************************************************
     *
     * Name: IEquipment
     * 
     * Description: Supplies the functionality of both IWeapon and IArmor. Use this
     *              if you want to use both armour and weapons.
     * 
     *******************************************************************************/
    interface IEquipment : IArmor, IWeapon
    {
        // This is left blank for now.
    } // end IEquipment
} // end GSP.Entities.Interfaces
