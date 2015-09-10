﻿/*******************************************************************************
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
    public abstract class Slot<TMainInventory, TSubInventoryOne, TSubInventoryTwo> : MonoBehaviour, IBaseSlot
        where TMainInventory : class, IBaseInventory
        where TSubInventoryOne : class, IBaseInventory
        where TSubInventoryTwo : class, IBaseInventory
    {
        // Variables are set to protected so the derived classes can access them while others can't
        protected Image mainInventoryIcon;      // Image component of the main inventory's slot; where the item's icon goes
        protected Image subInventoryOneIcon;    // Image component of the sub inventory's slot; where the item's icon goes
        protected Image subInventoryTwoIcon;    // Image component of the sub inventory's slot; where the item's icon goes

        protected TMainInventory mainInventory; // The main inventory system that's being interacted with
        protected TSubInventoryOne subInventoryOne; // The sub inventory system that's being interacted with
        protected TSubInventoryTwo subInventoryTwo; // The sub inventory system that's being interacted with
        
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
            mainInventory = null;
            subInventoryOne = null;
            subInventoryTwo = null;

            // Set the player number to the current turn
            playerNum = GameMaster.Instance.Turn;

            // Set the player's ally number; hard coded for a single ally right now
            allyNum = playerNum + 6;

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

            if (mainInventoryIcon == null && subInventoryOneIcon == null && subInventoryTwoIcon == null)
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
                allyNum = playerNum + 6;
            } // end if

            if (mainInventory == null)
            {
                Debug.Log("Update: Main Inventory is null");
            }
            
            // Check if the inventory exists
            if (mainInventory != null && mainInventoryIcon != null)
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
            } // end if

            // Check if the market exists
            if (subInventoryOne != null && subInventoryOneIcon != null)
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
            } // end if

            // Check if the ally inventory exists
            if (subInventoryTwo != null && subInventoryTwoIcon != null)
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
            
            // Check if the sub inventory is the market
            if (typeof(TSubInventoryOne) == typeof(Market) && subInventoryOne != null)
            {
                // Get the item from the market
                item = ((Market)(object)subInventoryOne).GetItem(slotId);
            } // end if
            
            // Check if the sub inventory is the ally inventory
            if (typeof(TSubInventoryTwo) == typeof(AllyInventory) && subInventoryTwo != null)
            {
                // Get the item from the ally inventory
                item = subInventoryTwo.GetItem(AllyNumber, slotId);
            } // end if

            // Return the item
            return item;
        } // end GetItem

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
