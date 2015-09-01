/*******************************************************************************
 *
 *  File Name: MarketSlot.cs
 *
 *  Description: Contains the logic of a market slot. The market system is
 *               filled with these slots. Their functionality is fairly
 *               minimal.
 *
 *******************************************************************************/
using UnityEngine.EventSystems;

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
    public class MarketSlot : Slot, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region IPointerEnterHandler Members

        // Event triggers when the mouse enters the slot's space
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (market.GetItem(SlotId).Name != string.Empty)
            {
                // Show the tooltip window while hovering over an item
                market.ShowTooltip(market.GetItem(SlotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        // Event triggers when the mouse leaves the slot's space
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (market.GetItem(SlotId).Name != string.Empty)
            {
                // Show the tooltip window while not hovering over an item
                market.ShowTooltip(null, false);
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
                if (market.GetItem(SlotId).Name != string.Empty)
                {
                    // Get the item that was right clicked
                    Item item = market.GetItem(SlotId);

                    // TODO: Add selling mechanic
                } // end if market.GetItem(SlotId).Name != string.Empty
            } // end if
        } // end OnPointerUp

        #endregion
    } // end MarketSlot
} // end GSP.Items.Inventories
