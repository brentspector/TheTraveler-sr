/*******************************************************************************
 *
 *  File Name: IArmor
 *
 *  Description: Describes a contract for armour functionality
 *
 *******************************************************************************/
using System.Collections.Generic;
using UnityEngine;

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

        // Equip a piece of armour.
        void EquipArmor(string item);

        // Unequip a piece of armour.
        void UnequipArmor(string item);

        #endregion

        #region Properties

        // The defense power given from the armour
        int DefencePower { get; set; }

        // The armour object equipped.
        Char.EquippedArmor EquippedArmor { get; set; }

        // The list of bonuses granted by the armour
        List<GameObject> Bonuses { get; set; }

        #endregion
    } // end IArmor
} // end GSP.Entities.Interfaces
