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
        protected Image inventoryIcon;  // Image component reference of the inventorySlot; this is where the item's image goes
        protected Image marketIcon;     // Image component reference of the marketSlot; this is where the item's image goes
        protected Image allyIcon;       // Image component reference of the allySlot; this is where the item's image goes

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
            inventoryIcon = null;
            marketIcon = null;
            allyIcon = null;

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

            if (inventoryIcon == null && marketIcon == null && allyIcon == null)
            {
                // Set the icon's image reference for one of them
                SetIconReference();
            }
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
            if (inventory != null && inventoryIcon != null)
            {
                // Check if the slot contains an item
                if (inventory.GetItem(playerNum, slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!inventoryIcon.enabled)
                    {
                        inventoryIcon.enabled = true;
                        inventoryIcon.sprite = inventory.GetItem(playerNum, slotId).Icon;
                    } // end if !itemIcon.enabled
                } // end if
                else
                {
                    if (inventoryIcon.enabled)
                    {
                        // Disable the component
                        inventoryIcon.enabled = false;
                    } // end if
                } // end else
            } // end if

            // Check if the market exists
            if (market != null && marketIcon != null)
            {
                // Check if the slot contains an item
                if (market.GetItem(slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!marketIcon.enabled)
                    {
                        marketIcon.enabled = true;
                        marketIcon.sprite = market.GetItem(slotId).Icon;
                    } // end if !itemIcon.enabled
                } // end if
                else
                {
                    if (marketIcon.enabled)
                    {
                        // Disable the component
                        marketIcon.enabled = false;
                    } // end if
                } // end else
            } // end if

            // Check if the ally's inventory exists
            if (allyInventory != null && allyIcon != null)
            {
                // Check if the slot contains an item
                if (allyInventory.GetItem(allyNum, slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!allyIcon.enabled)
                    {
                        allyIcon.enabled = true;
                        allyIcon.sprite = allyInventory.GetItem(allyNum, slotId).Icon;
                    } // end if !itemIcon.enabled
                } // end if
                else
                {
                    if (allyIcon.enabled)
                    {
                        // Disable the component
                        allyIcon.enabled = false;
                    } // end if
                } // end else
            } // end if
        } // end Update

        // Sets the component reference for the slot's icon
        void SetIconReference()
        {
            // Check if the slot's type is part of the inventory
            if (slotType == SlotType.Inventory || slotType == SlotType.Equipment || slotType == SlotType.Bonus)
            {
                // Get the Image component reference
                inventoryIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end if
            else if (slotType == SlotType.Market)
            {
                // Get the Image component reference
                marketIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end else if
            else if (slotType == SlotType.Ally)
            {
                // Get the Image component reference
                allyIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end else if
        } // end SetIconReference

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
