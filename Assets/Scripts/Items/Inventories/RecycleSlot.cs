/*******************************************************************************
 *
 *  File Name: RecycleSlot.cs
 *
 *  Description: Contains the logic of a slot. The Recycle Bin Inventory system
 *                is filled with slots. Their functionality is fairly minimal.
 *
 *******************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using GSP.Entities.Neutrals;
using GSP.Core;

namespace GSP.Items.Inventories
{
    /*******************************************************************************
     *
     * Name: RecycleSlot
     * 
     * Description: The functionality of each slot in the Recyle Bin's inventory.
     *              This is done through Unity's EventSystems interfaces.
     * 
     *******************************************************************************/
    public class RecycleSlot : Slot<PlayerInventory, Market, AllyInventory, RecycleBin>,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region IPointerEnterHandler Members

        // Event triggers when the mouse enters the slot's space
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (subInventoryThree.GetItem(6, SlotId).Name != string.Empty)
            {
                // Show the tooltip window while hovering over an item
                subInventoryThree.ShowTooltip(subInventoryThree.GetItem(6, SlotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        // Event triggers when the mouse leaves the slot's space
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (subInventoryThree.GetItem(6, SlotId).Name != string.Empty)
            {
                // Show the tooltip window while not hovering over an item
                subInventoryThree.ShowTooltip(null, false);
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
            // Check if the button was the right mouse
            if (pointerEventData.pointerId == -2)
            {
                // Check if the inventory window is open
                if (mainInventory.IsOpen)
                {
                    // Check if there is an item in the slot
                    if (subInventoryThree.GetItem(6, SlotId).Name != string.Empty)
                    {
                        // Get the item that was right clicked
                        Item item = subInventoryThree.GetItem(6, SlotId);

                        // Get the player's merchant
                        Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(PlayerNumber).Entity;

                        // Handle the retrieving of items to recycle
                        HandleRecycling(item, playerMerchant);
                    } // end if subInventoryThree.GetItem(6, SlotId).Name != string.Empty
                } // end mainInventory.IsOpen
            } // end if
        } // end OnPointerUp

        // Handles the market buying items from the player
        void HandleRecycling(Item item, Merchant merchant)
        {
            // Simply add the item back to the player's inventory
            mainInventory.AddItem(0, PlayerNumber, item.Id, SlotType.Inventory);

            // Now remove it from the market's buy inventory
            subInventoryThree.Remove(6, item);
        } // end HandleRecycling

        #endregion
    } // end RecycleSlot
}
