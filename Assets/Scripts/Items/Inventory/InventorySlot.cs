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
     * Description: The logic of each slot in the inventory.
     * 
     *******************************************************************************/
    public class InventorySlot : Slot, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
    {
        #region IPointerDownHandler Members

        // Handler for mouse clicks
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            Debug.LogFormat("The slot '{0}' was clicked!", transform.name);
        } // end OnPointerDown

        #endregion

        #region IPointerEnterHandler Members

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (inventory.GetItem(slotId).Name != string.Empty)
            {
                Debug.LogFormat("The mouse has entered slot '{0}'!", transform.name);

                // Show the tooltip window while hovering over an item
                inventory.ShowTooltip(inventory.GetItem(slotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (inventory.GetItem(slotId).Name != string.Empty)
            {
                Debug.LogFormat("The mouse has left slot '{0}'!", transform.name);

                // Show the tooltip window while not hovering over an item
                inventory.ShowTooltip(null, false);
            } // end if
        } // end OnPointerEnter

        #endregion
    } // end InventorySlot
} // end GSP.Items.Inventory
