using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace GSP.Items.Inventory
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
    {
        Image itemIcon;         // The Image component reference of the slot; this is where the item's image goes
        int slotId;             // The ID of the slot
        Inventory inventory;    // The inventory where the items are stored

        // Use this for initialization
        void Start()
        {
            // Get the Inventory component reference
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            
            // Get the Image component reference
            itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        } // end Start

        // Update is called once per frame
        void Update()
        {
            // Check if the slot contains an item
            if (inventory.GetItem(slotId).Name != string.Empty)
            {
                // Enable the component
                itemIcon.enabled = true;
                itemIcon.sprite = inventory.GetItem(slotId).Icon;
            } // end if
            else
            {
                // Disable the component
                itemIcon.enabled = false;
            } // end else
        } // end Update

        // Gets and Sets the ID of the slot
        public int SlotId
        {
            get { return slotId; }
            set { slotId = value; }
        } // end SlotId

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
            if (inventory.GetItem(slotId).Name != null)
            {
                Debug.LogFormat("The mouse has entered slot '{0}'!", transform.name);

                // Show the tooltip window while hovering over an item
                inventory.ShowTooltip(transform.localPosition, inventory.GetItem(slotId));
            } // end if
        } // end OnPointerEnter

        #endregion

        #region IPointerExitHandler Members

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            // Check if there is an item in the slot
            if (inventory.GetItem(slotId).Name != null)
            {
                Debug.LogFormat("The mouse has left slot '{0}'!", transform.name);

                // Show the tooltip window while not hovering over an item
                inventory.ShowTooltip(Vector3.zero, null, false);
            } // end if
        } // end OnPointerEnter

        #endregion
    } // end Slot
} // end GSP.Items.Inventory
