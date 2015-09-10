/*******************************************************************************
 *
 *  File Name: MarketSlot.cs
 *
 *  Description: Contains the logic of a market slot. The market system is
 *               filled with these slots. Their functionality is fairly
 *               minimal.
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
     * Name: MarketSlot
     * 
     * Description: The functionality of each slot in the Market's inventory. This
     *              is done through Unity's EventSystems interfaces.
     * 
     *******************************************************************************/
    public class MarketSlot : Slot<PlayerInventory, Market, AllyInventory>, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region IPointerEnterHandler Members

        // Event triggers when the mouse enters the slot's space
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (subInventoryOne.GetItem(SlotId).Name != string.Empty)
            {
                // Show the tooltip window while hovering over an item
                subInventoryOne.ShowTooltip(subInventoryOne.GetItem(SlotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        // Event triggers when the mouse leaves the slot's space
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (subInventoryOne.GetItem(SlotId).Name != string.Empty)
            {
                // Show the tooltip window while not hovering over an item
                subInventoryOne.ShowTooltip(null, false);
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
                // Check if there is an item in the slot
                if (subInventoryOne.GetItem(SlotId).Name != string.Empty)
                {
                    // Get the item that was right clicked
                    Item item = subInventoryOne.GetItem(SlotId);

                    // Get the player's merchant
                    Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(PlayerNumber).Entity;

                    // Check if we're buying to selling
                    if (subInventoryOne.Action == MarketAction.Sell)
                    {
                        // The market is selling so handle the selling
                        HandleSelling(item, playerMerchant);
                    } // end if
                    else
                    {
                        // Otherwise, the market is buying so handle the buying
                        HandleBuying(item, playerMerchant);
                    } // end else
                } // end if market.GetItem(SlotId).Name != string.Empty
            } // end if
        } // end OnPointerUp

        // Handles the market buying items from the player
        void HandleBuying(Item item, Merchant merchant)
        {
            // Simply add the item back to the player's inventory
            mainInventory.AddItem(PlayerNumber, item.Id, SlotType.Inventory);

            // Now remove it from the market's buy inventory
            subInventoryOne.Remove(5, item);
        } // end HandleBuying

        // Handles the market selling items to the player
        void HandleSelling(Item item, Merchant merchant)
        {
            // Check if the merchant has enough space in the inventory
            if (mainInventory.FindFreeSlot(PlayerNumber, SlotType.Inventory) >= 0)
            {
                // Check if the merchant has enough currency to purchase the item
                if (merchant.Currency >= ((Equipment)item).CostValue)
                {
                    // The merchant has enough currency so deduct the cost
                    merchant.Currency -= ((Equipment)item).CostValue;

                    // Add the item to the player's inventory
                    mainInventory.AddItem(PlayerNumber, item.Id, SlotType.Inventory);
                } // end if merchant.Currency >= ((Equipment)item).CostValue
            } // end if
        } // end HandleSelling

        #endregion
    } // end MarketSlot
} // end GSP.Items.Inventories
