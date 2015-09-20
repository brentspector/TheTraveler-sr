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
     *       TSubInventoryOne should be the market
     *       TSubInventoryTwo should be the ally inventory
     * 
     *******************************************************************************/
    public abstract class Slot<TMainInventory, TSubInventoryOne, TSubInventoryTwo, TSubInventoryThree> : MonoBehaviour, IBaseSlot
        where TMainInventory : class, IBaseInventory
        where TSubInventoryOne : class, IBaseInventory
        where TSubInventoryTwo : class, IBaseInventory
        where TSubInventoryThree : class, IBaseInventory
    {
        // Variables are set to protected so the derived classes can access them while others can't
        protected Image mainInventoryIcon;      // Image component of the main inventory's slot; where the item's icon goes
        protected Image subInventoryOneIcon;    // Image component of the sub inventory's slot; where the item's icon goes
        protected Image subInventoryTwoIcon;    // Image component of the sub inventory's slot; where the item's icon goes
        protected Image subInventoryThreeIcon;  // Image component of the sub inventory's slot; where the item's icon goes

        protected TMainInventory mainInventory;         // The main inventory system that's being interacted with
        protected TSubInventoryOne subInventoryOne;     // The sub inventory system that's being interacted with
        protected TSubInventoryTwo subInventoryTwo;     // The sub inventory system that's being interacted with
        protected TSubInventoryThree subInventoryThree; // The sub inventory system that's being interacted with
        
        int slotId;         // The ID of the slot
        int playerNum;      // The current player's turn
        int allyNum;        // The ID of the current player's current ally.
        SlotType slotType;  // The type of the slot

        // Use this for initialization
        void Start()
        {
            // Set the references to a null state
            mainInventoryIcon = null;
            subInventoryOneIcon = null;
            subInventoryTwoIcon = null;
            subInventoryThreeIcon = null;
            mainInventory = null;
            subInventoryOne = null;
            subInventoryTwo = null;
            subInventoryThree = null;

            // Set the player number to the current turn
            playerNum = GameMaster.Instance.Turn;

            // Set the player's ally number; hard coded for a single ally right now
            allyNum = playerNum + 7;

            // Get the main inventory component reference
            if (GameObjectExists(typeof(TMainInventory)))
            {
                mainInventory = GameObject.Find("Canvas").transform.Find(typeof(TMainInventory).Name).GetComponent<TMainInventory>();
            } // end if

            // Get the first sub inventory component reference
            if (GameObjectExists(typeof(TSubInventoryOne)))
            {
                subInventoryOne = GameObject.Find("Canvas").transform.Find(typeof(TSubInventoryOne).Name).GetComponent<TSubInventoryOne>();
            } // end if

            // Get the second sub inventory component reference
            if (GameObjectExists(typeof(TSubInventoryTwo)))
            {
                subInventoryTwo = GameObject.Find("Canvas").transform.Find(typeof(TSubInventoryTwo).Name).GetComponent<TSubInventoryTwo>();
            } // end if

            // Get the third sub inventory component reference
            if (GameObjectExists(typeof(TSubInventoryThree)))
            {
                subInventoryThree = GameObject.Find("Canvas").transform.Find(typeof(TSubInventoryThree).Name).GetComponent<TSubInventoryThree>();
            } // end if

            if (mainInventoryIcon == null && subInventoryOneIcon == null && subInventoryTwoIcon == null && subInventoryThreeIcon == null)
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

                // Set the player's ally number; hard coded for a single ally right now
                allyNum = playerNum + 7;
            } // end if

            // Check if the inventory exists
            if (mainInventory != null && mainInventoryIcon != null)
            {
               // Check if the item's list exists
                if (mainInventory.GetItemsListCount(playerNum) > 0)
                {
                    // Check if the slot contains an item
                    if (mainInventory.GetItem(PlayerNumber, slotId).Name != string.Empty)
                    {
                        // Enable the component
                        if (!mainInventoryIcon.enabled)
                        {
                            mainInventoryIcon.enabled = true;
                            mainInventoryIcon.sprite = mainInventory.GetItem(PlayerNumber, slotId).Icon;
                        } // end if
                        else
                        {
                            mainInventoryIcon.sprite = mainInventory.GetItem(PlayerNumber, slotId).Icon;
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
                } // end if mainInventory.GetItemsListCount(playerNum) > 0
            } // end if

            // Check if the market exists
            if (subInventoryOne != null && subInventoryOneIcon != null)
            {
                // Check if the item's list exists
                if (GetItemsListCount() > 0)
                {
                    // Check if the slot contains an item
                    if (GetItem(slotId).Name != string.Empty)
                    {
                        // Enable the component
                        if (!subInventoryOneIcon.enabled)
                        {
                            subInventoryOneIcon.enabled = true;
                            subInventoryOneIcon.sprite = GetItem(slotId).Icon;
                        } // end if
                        else
                        {
                            subInventoryOneIcon.sprite = GetItem(slotId).Icon;
                        } // end else
                    } // end if
                    else
                    {
                        if (subInventoryOneIcon.enabled)
                        {
                            // Disable the component
                            subInventoryOneIcon.enabled = false;
                        } // end if
                    } // end else
                } // end if GetItemsListCount() > 0
            } // end if

            // Check if the ally inventory exists
            if (subInventoryTwo != null && subInventoryTwoIcon != null)
            {
                // Check if the item's list exists
                if (subInventoryTwo.GetItemsListCount(allyNum) > 0)
                {
                    // Check if the slot contains an item
                    if (GetItem(slotId).Name != string.Empty)
                    {
                        // Enable the component
                        if (!subInventoryTwoIcon.enabled)
                        {
                            subInventoryTwoIcon.enabled = true;
                            subInventoryTwoIcon.sprite = GetItem(slotId).Icon;
                        } // end if
                        else
                        {
                            subInventoryTwoIcon.sprite = GetItem(slotId).Icon;
                        } // end else
                    } // end if
                    else
                    {
                        if (subInventoryTwoIcon.enabled)
                        {
                            // Disable the component
                            subInventoryTwoIcon.enabled = false;
                        } // end if
                    } // end else
                } // end if subInventoryTwo.GetItemsListCount(allyNum) > 0
            } // end if

            // Check if the recyle bin inventory exists
            if (subInventoryThree != null && subInventoryThreeIcon != null)
            {
                // Check if the item's list exists
                if (subInventoryThree.GetItemsListCount(6) > 0)
                {
                    // Check if the slot contains an item
                    if (subInventoryThree.GetItem(6, slotId).Name != string.Empty)
                    {
                        // Enable the component
                        if (!subInventoryThreeIcon.enabled)
                        {
                            subInventoryThreeIcon.enabled = true;
                            subInventoryThreeIcon.sprite = subInventoryThree.GetItem(6, slotId).Icon;
                        } // end if
                        else
                        {
                            subInventoryThreeIcon.sprite = subInventoryThree.GetItem(6, slotId).Icon;
                        } // end else
                    } // end if
                    else
                    {
                        if (subInventoryThreeIcon.enabled)
                        {
                            // Disable the component
                            subInventoryThreeIcon.enabled = false;
                        } // end if
                    } // end else
                } // end if subInventoryThree.GetItemsListCount(6) > 0
            } // end if
        } // end Update

        // Does the game object for the inventory exist?
        bool GameObjectExists(System.Type inventoryType)
        {
            // Check if the object exists
            if (GameObject.Find("Canvas").transform.Find(inventoryType.Name) != null)
            {
                return true;
            } // end if
            else
            {
                return false;
            } // end else
        } // end GameObjectExists

        // Gets an item based on sub inventory type
        Item GetItem(int slotId)
        {
            // The item to return
            Item item = null;
            
            if (slotType == SlotType.Market)
            {
                // Get the item from the market
                item = ((Market)(object)subInventoryOne).GetItem(slotId);
            }
            else if (slotType == SlotType.Ally)
            {
                // Get the item from the ally inventory
                item = subInventoryTwo.GetItem(AllyNumber, slotId);
            }

            // Return the item
            return item;
        } // end GetItem

        // Gets the items list count for the market
        int GetItemsListCount()
        {
            if (((Market)(object)subInventoryOne).Action == MarketAction.Buy)
            {
                return subInventoryOne.GetItemsListCount(5);
            } // end if
            else
            {
                return subInventoryOne.GetItemsListCount(4);
            } // end else
        } // end GetItemsListCount

        // Sets the component reference for the slot's icon
        void SetIconReference()
        {
            // Check if the slot's type is part of the inventory
            if (slotType == SlotType.Inventory || slotType == SlotType.Equipment || slotType == SlotType.Bonus)
            {
                // Get the Image component reference
                mainInventoryIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end if
            // Check if the slot's type is part of the market
            else if (slotType == SlotType.Market)
            {
                // Get the Image component reference
                subInventoryOneIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end else if
            // Check if the slot's type is ally
            else if (slotType == SlotType.Ally)
            {
                // Get the Image component reference
                subInventoryTwoIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            } // end else if
            // Check if the slot's type is recycle
            else if (slotType == SlotType.Recycle)
            {
                // Get the Image component reference
                subInventoryThreeIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
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
