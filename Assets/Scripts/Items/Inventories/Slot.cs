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
     * Note: TMainInventory should always be the player's inventory
     *       TSubInventory is the inventory they are interacting with such as the
     *       market or ally.
     * 
     *******************************************************************************/
    public abstract class Slot<TMainInventory, TSubInventory> : MonoBehaviour, IBaseSlot
        where TMainInventory : class, IBaseInventory
        where TSubInventory : class, IBaseInventory
    {
        // Variables are set to protected so the derived classes can access them while others can't
        protected Image mainInventoryIcon;  // Image component of the main inventory's slot; where the item's icon goes
        protected Image subInventoryIcon;   // Image component of the sub inventory's slot; where the item's icon goes

        protected TMainInventory mainInventory; // The main inventory system that's being interacted with
        protected TSubInventory subInventory;   // The sub inventory system that's being interacted with
        
        int slotId;         // The ID of the slot
        int playerNum;      // The current player's turn
        int allyNum;        // The ID of the current player's current ally.
        SlotType slotType;  // The type of the slot

        // Use this for initialization
        void Start()
        {
            // Set the references to a null state
            mainInventoryIcon = null;
            subInventoryIcon = null;
            mainInventory = null;
            subInventory = null;

            // For now there is only a single ally so this is defaulted to zero
            allyNum = 0;
            
            // Get the main inventory component reference
            if (GameObject.Find(typeof(TMainInventory).Name) != null)
            {
                mainInventory = GameObject.Find(typeof(TMainInventory).Name).GetComponent<TMainInventory>();
            } // end if

            // Get the sub inventory component reference
            if (GameObject.Find(typeof(TSubInventory).Name) != null)
            {
                subInventory = GameObject.Find(typeof(TSubInventory).Name).GetComponent<TSubInventory>();
            } // end if

            if (mainInventoryIcon == null && subInventoryIcon == null)
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
            if (mainInventory != null && mainInventoryIcon != null)
            {
                // Check if the slot contains an item
                if (mainInventory.GetItem(slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!mainInventoryIcon.enabled)
                    {
                        mainInventoryIcon.enabled = true;
                        mainInventoryIcon.sprite = mainInventory.GetItem(slotId).Icon;
                    } // end if
                    else
                    {
                        mainInventoryIcon.sprite = mainInventory.GetItem(slotId).Icon;
                    } // end else
                } // end if
                else
                {
                    if (mainInventoryIcon.enabled)
                    {
                        // Disable the component
                        mainInventoryIcon.enabled = false;
                    } // end if
                } // end else
            } // end if

            // Check if the market exists
            if (subInventory != null && subInventoryIcon != null)
            {
                // Check if the slot contains an item
                if (subInventory.GetItem(slotId).Name != string.Empty)
                {
                    // Enable the component
                    if (!subInventoryIcon.enabled)
                    {
                        subInventoryIcon.enabled = true;
                        subInventoryIcon.sprite = subInventory.GetItem(slotId).Icon;
                    } // end if
                    else
                    {
                        subInventoryIcon.sprite = subInventory.GetItem(slotId).Icon;
                    } // end else
                } // end if
                else
                {
                    if (subInventoryIcon.enabled)
                    {
                        // Disable the component
                        subInventoryIcon.enabled = false;
                    } // end if
                } // end else
            } // end if
        } // end Update

        // Shows an icon for the inventory
        public void ShowIcon(Image icon, bool canShow)
        {
            //
        } // end ShowIcon

        // Sets the component reference for the slot's icon
        void SetIconReference()
        {
            // Check if the slot's type is part of the inventory
            if (slotType == SlotType.Inventory || slotType == SlotType.Equipment || slotType == SlotType.Bonus)
            {
                // Get the Image component reference
                mainInventoryIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end if
            // Check if the slot's type is part of the market or ally
            else if (slotType == SlotType.Market || slotType == SlotType.Ally)
            {
                // Get the Image component reference
                subInventoryIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
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
