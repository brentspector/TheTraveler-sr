/*******************************************************************************
 *
 *  File Name: IArmor
 *
 *  Description: Describes a contract for armour functionality
 *
 *******************************************************************************/
using GSP.Items;
using System.Collections.Generic;

namespace GSP.Entities.Interfaces
{
    /*******************************************************************************
     *
     * Name: IArmor
     * 
     * Description: Supplies the functionality for equipping armour. This
     *              includes adding defence power.
     * 
     *******************************************************************************/
    interface IArmor
    {
        #region Functions

        // Equip a piece of armour
        void EquipArmor(Armor armor);

        // Unequip a piece of armour
        void UnequipArmor(Armor armor);

        #endregion

        #region Properties

        // The defense power given from the armour
        int DefencePower { get; set; }

        // The armour object equipped
        Armor EquippedArmor { get; }

        // The list of bonuses granted by the armour
        List<Bonus> Bonuses { get; }

        #endregion
    } // end IArmor
} // end GSP.Entities.Interfaces
