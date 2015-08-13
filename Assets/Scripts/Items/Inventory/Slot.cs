/*******************************************************************************
 *
 *  File Name: Slot.cs
 *
 *  Description: Contains the base class logic for all slots that use the new
 *               Inventory system
 *
 *******************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace GSP.Items.Inventory
{
    /*******************************************************************************
     *
     * Name: Slot
     * 
     * Description: The base class for all slots of the new Inventory system.
     * 
     *******************************************************************************/
    public abstract class Slot : MonoBehaviour
    {
        // Variables are set to protected so the derived classes can access them while other can't
        protected Image itemIcon;         // The Image component reference of the slot; this is where the item's image goes
        protected int slotId;             // The ID of the slot
        protected Inventory inventory;    // The inventory where the items are stored

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
    } // end InventorySlot
} // end GSP.Items.Inventory
