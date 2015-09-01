/*******************************************************************************
 *
 *  File Name: Slot.cs
 *
 *  Description: Contains the base class logic for all slots that use the new
 *               Inventory system
 *
 *******************************************************************************/
using GSP.Core;
using UnityEngine;
using UnityEngine.UI;

namespace GSP.Items.Inventories
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
        // Variables are set to protected so the derived classes can access them while others can't
        protected Image itemIcon;               // The Image component reference of the slot; this is where the item's image goes
        protected Inventory inventory;          // The player inventory where the items are stored
        protected Market market;                // The market inventory where the items are sold
        protected AllyInventory allyInventory;  // The ally's inventory where the items are stored
        
        int slotId;         // The ID of the slot
        int playerNum;      // The current player's turn
        int allyNum;        // The ID of the current player's current ally.
        SlotType slotType;  // The type of the slot

        // Use this for initialization
        void Start()
        {
            // Set the references to a null state
            inventory = null;
            market = null;
            allyInventory = null;

            // For now there is only a single ally so this is defaulted to zero
            allyNum = 0;
            
            // Get the Inventory component reference
            if (GameObject.Find("Inventory") != null)
            {
                inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            } // end if

            // Get the Market component reference
            if (GameObject.Find("Market") != null)
            {
                market = GameObject.Find("Market").GetComponent<Market>();
            } // end if

            // Get the AllyInventory component refereence
            if (GameObject.Find("AllyInventory") != null)
            {
                allyInventory = GameObject.Find("AllyInventory").GetComponent<AllyInventory>();
            } // end if

            // Get the Image component reference
            itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        } // end Start

        // Update is called once per frame
        void Update()
        {
            // Get the player number if it has changed
            if (playerNum != GameMaster.Instance.Turn)
            {
                // Set the player number to the current turn
                playerNum = GameMaster.Instance.Turn;
            } // end if
            
            // Check if the inventory exists
            if (inventory != null)
            {
                // Check if the slot contains an item
                if (inventory.GetItem(playerNum, slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!itemIcon.enabled)
                    {
                        itemIcon.enabled = true;
                        itemIcon.sprite = inventory.GetItem(playerNum, slotId).Icon;
                    } // end if !itemIcon.enabled
                } // end if
                else
                {
                    if (itemIcon.enabled)
                    {
                        // Disable the component
                        itemIcon.enabled = false;
                    } // end if
                } // end else
            } // end if

            // Check if the market exists
            if (market != null)
            {
                // Check if the slot contains an item
                if (market.GetItem(slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!itemIcon.enabled)
                    {
                        itemIcon.enabled = true;
                        itemIcon.sprite = market.GetItem(slotId).Icon;
                    } // end if !itemIcon.enabled
                } // end if
                else
                {
                    if (itemIcon.enabled)
                    {
                        // Disable the component
                        itemIcon.enabled = false;
                    } // end if
                } // end else
            } // end if

            // Check if the ally's inventory exists
            if (allyInventory != null)
            {
                // Check if the slot contains an item
                if (allyInventory.GetItem(allyNum, slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!itemIcon.enabled)
                    {
                        itemIcon.enabled = true;
                        itemIcon.sprite = allyInventory.GetItem(allyNum, slotId).Icon;
                    } // end if !itemIcon.enabled
                } // end if
                else
                {
                    if (itemIcon.enabled)
                    {
                        // Disable the component
                        itemIcon.enabled = false;
                    } // end if
                } // end else
            } // end if
        } // end Update

        // Gets and Sets the ID of the slot
        public int SlotId
        {
            get { return slotId; }
            set { slotId = value; }
        } // end SlotId

        // Gets the Player's Number
        public int PlayerNumber
        {
            get { return playerNum; }
        } // end PlayerNumber

        // Gets the Ally's Number
        public int AllyNumber
        {
            get { return allyNum; }
        } // end AllyNumber

        // Gets and Sets the SlotType of the slot
        public SlotType SlotType
        {
            get { return slotType; }
            set { slotType = value; }
        } // end SlotType
    } // end Slot
} // end GSP.Items.Inventories
