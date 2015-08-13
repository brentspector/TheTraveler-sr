/*******************************************************************************
 *
 *  File Name: InventorySlot.cs
 *
 *  Description: Contains the logic of a slot. The Inventory system is filled
 *               with slots. Their functionality is fairly minimal.
 *
 *******************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSP.Items.Inventory
{
    /*******************************************************************************
     *
     * Name: InventorySlot
     * 
     * Description: The functionality of each slot in the inventory. This is done
     *              through Unity's EventSystems interfaces.
     * 
     *******************************************************************************/
    public class InventorySlot : Slot, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region IPointerEnterHandler Members

        // Event triggers when the mouse enters the slot's space
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (inventory.GetItem(playerNum, slotId).Name != string.Empty)
            {
                // Show the tooltip window while hovering over an item
                inventory.ShowTooltip(inventory.GetItem(playerNum, slotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        // Event triggers when the mouse leaves the slot's space
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (inventory.GetItem(playerNum, slotId).Name != string.Empty)
            {
                // Show the tooltip window while not hovering over an item
                inventory.ShowTooltip(null, false);
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerDownHandler Members

        // Event triggers when a mouse button is pressed in the slot's space
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            // Leave empty for now; used to detect mouse so it can detect mouse up
        } // end OnPointerDown

        #endregion

        #region IPointerUpHandler Members

        // Event triggers when a mouse button is released in the slot's space
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (inventory.GetItem(playerNum, slotId).Name != string.Empty)
            {
                // Check if the button was the right mouse
                if (pointerEventData.pointerId == -2)
                {
                    // Get the item that was right clicked
                    Item item = inventory.GetItem(playerNum, slotId);

                    // Check if the item is a piece of equipment
                    if (item is Equipment)
                    {
                        // The item is a piece of equipment so equip it
                        inventory.EquipItem(playerNum, (Equipment)item);

                        // Check if the item is a bonus item and that we're right clicking it from the bonus slot range.
                        if (item is Bonus && (slotId >= inventory.BonusSlotBegin && slotId < inventory.BonusSlotEnd))
                        {
                            int freeSlot;   // The first slot that is free
                            
                            // Check if there's space for the item
                            if ((freeSlot = inventory.FindFreeSlot(playerNum, SlotType.Inventory)) >= 0)
                            {
                                // Swap the bonus item with the item at the free slot
                                inventory.SwapItem(playerNum, item, inventory.GetItem(playerNum, freeSlot));

                                // Disable the tooltip
                                inventory.ShowTooltip(null, false);
                            }
                        } // end if item is Bonus && (slotId >= inventory.BonusSlotBegin && slotId < inventory.BonusSlotEnd)
                    } // end if item is Equipment
                } // end if pointerEventData.pointerId == -2
            } // end if
        } // end OnPointerUp

        #endregion
    } // end InventorySlot
} // end GSP.Items.Inventory
