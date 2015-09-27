using GSP.Char.Allies;
/*******************************************************************************
 *
 *  File Name: AllySlot.cs
 *
 *  Description: Contains the logic of an ally slot. The ally's Inventory
 *               system is filled with these slots. Their functionality is
 *               fairly minimal.
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Friendlies;
using GSP.Entities.Neutrals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSP.Items.Inventories
{
    /*******************************************************************************
     *
     * Name: AllySlot
     * 
     * Description: The functionality of each slot in the ally's inventory.
     *              This is done through Unity's EventSystems interfaces.
     * 
     *******************************************************************************/
    public class AllySlot : Slot<PlayerInventory, Market, AllyInventory, RecycleBin>, 
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region IPointerEnterHandler Members

        // Event triggers when the mouse enters the slot's space
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (subInventoryTwo.GetItem(AllyNumber, SlotId).Name != string.Empty)
            {
                // Show the tooltip window while hovering over an item
                subInventoryTwo.ShowTooltip(subInventoryTwo.GetItem(AllyNumber, SlotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        // Event triggers when the mouse leaves the slot's space
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (subInventoryTwo.GetItem(AllyNumber, SlotId).Name != string.Empty)
            {
                // Show the tooltip window while not hovering over an item
                subInventoryTwo.ShowTooltip(null, false);
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
                    if (subInventoryTwo.GetItem(AllyNumber, SlotId).Name != string.Empty)
                    {
                        // Get the item that was right clicked
                        Item item = subInventoryTwo.GetItem(AllyNumber, SlotId);

                        // For now, just handle the transferring to the player's inventory
                        TradeToPlayer(item);
                    } // end if inventory.GetItem(PlayerNumber, SlotId).Name != string.Empty
                } // end mainInventory.IsOpen
            } // end if
        } // end OnPointerUp

        // Trades to the player
        void TradeToPlayer(Item item)
        {
            // Get the player's merchant
            Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(PlayerNumber).Entity;

            // Get the ally, this is hardcoded for the port ally
            Porter ally = (Porter)playerMerchant.GetAlly(0).GetComponent<PorterMB>().Entity;

            // Transfer the resource to the ally
            if(ally.TransferResource<Merchant>(playerMerchant, (Resource)item))
            {
                // Update the ally's stats
                subInventoryTwo.SetStats(playerMerchant);
            } // end if
        } // end TradeToPlayer

        #endregion
    } // end AllySlot
} // end GSP.Items.Inventories
