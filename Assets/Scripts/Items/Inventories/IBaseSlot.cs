/*******************************************************************************
 *
 *  File Name: IBaseSlot
 *
 *  Description: Describes a contract for a slot system functionality with
 *               generics.
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Items.Inventories
{
    /*******************************************************************************
     *
     * Name: IBaseSlot
     * 
     * Description: Supplies the functionality for the slot system usimg generics.
     * 
     *******************************************************************************/
    public interface IBaseSlot
    {
        // Gets and Sets the ID of the slot
        int SlotId { get; set; }

        // Gets the Player's Number
        int PlayerNumber { get; }
        // Gets the Ally's Number
        int AllyNumber { get; }

        // Gets and Sets the SlotType of the slot
        SlotType SlotType { get; set; }
    } // end IBaseSlot
} // end GSP.Items.Inventories
