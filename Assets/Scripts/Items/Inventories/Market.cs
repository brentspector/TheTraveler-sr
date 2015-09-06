/*******************************************************************************
 *
 *  File Name: Market.cs
 *
 *  Description: Contains the logic of the market system. This is am
 *               inventory backend for the market system.
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Neutrals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GSP.Items.Inventories
{
    /*******************************************************************************
     *
     * Name: Market
     * 
     * Description: The logic for the new market inventory system
     * 
     *******************************************************************************/
    public class Market : Inventory<MarketSlot>
    {
        int numInventorySlotsCreate;    // The number of inventory slots to create

        Transform bottomGrid;       // The Inentory's Bottom Panel

        GameObject buySellButton;   // Reference for the Market's buy/sell button
        GameObject acceptButton;    // Reference for the Market's accept button

        MarketAction action;    // The action the market is doing

        // Use this for initialisation
        protected override void Awake()
        {
            // Call the parent's Awake() first
            base.Awake();
            
            // Get Inventory's Bottom panel
            bottomGrid = GameObject.Find("Market/Body").transform;

            // Get the button objects
            buySellButton = GameObject.Find("Market/Buttons/BuySellButton");
            acceptButton = GameObject.Find("Market/Buttons/AcceptButton");

            // Disable the accept button by default
            acceptButton.SetActive(false);

            // Initialise the number of slots to create
            numInventorySlotsCreate = 30;

            // Add the sell and buy items lists
            CreateItemList(4, numInventorySlotsCreate);
            CreateItemList(5, numInventorySlotsCreate);

            // Get all the equipment items
            List<Item> equipment = ItemDatabase.Instance.Items.FindAll(item => item is Equipment);

            // Make sure the number of items will fit.
            int itemsToAdd = 0;
            if (equipment.Count <= numInventorySlotsCreate)
            {
                // The items will fit
                itemsToAdd = equipment.Count;
            } // end if
            else
            {
                // Otherwise the items won't fit so only fill with a portion of them
                itemsToAdd = numInventorySlotsCreate;
            } // end else
            
            // Loop to set the proper items
            for (int index = 0; index < itemsToAdd; index++)
            {
                // Add the current item to the market inventory
                AddItemFromSave(4, equipment[index].Id, index);
            } // end for

            // The default action for the market is selling
            action = MarketAction.Sell;
        } // end Awake
        
        // Use this for initialisation
        protected override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Create the market slots
            CreateSlots(numInventorySlotsCreate, SlotType.Market, bottomGrid, "MarketSlot ");
        } // end Start

        // Runs each frame; used to update the tooltip's position
        protected override void Update()
        {
            // Call the parent's Update() first
            base.Update();
        } // end Update

        // Gets the first empty slot of the given SlotType
        // Note: Only usuable in buy mode; returns -1 otherwise
        public override int FindFreeSlot(int key, SlotType slotType)
        {
            // Find the next free slot using the parent's calculations
            int freeSlot = base.FindFreeSlot(key, slotType);

            // Check if we found a free slot
            if (freeSlot < 0)
            {
                // No free slots available so return negative one
                return -1;
            } // end if
            
            // Return the first empty slot clamped to the market range
            return Utility.ClampInt(freeSlot, 0, numInventorySlotsCreate);
        } // end FindFreeSlot

        // Gets an item from the market's inventories based upon the current action
        public Item GetItem(int slotNum)
        {
            // Declare the item we'll use
            Item item = null;

            // Check we are in sell mode
            if (action == MarketAction.Sell)
            {
                item = GetItem(4, slotNum);
            } // end if
            else
            {
                item = GetItem(5, slotNum);
            } // end else

            // Return the item
            return item;
        } // end GetItem

        // Set the stats for the status panels
        public override void SetStats(Merchant player)
        {
            // No stats to set, but have to implement this
        } // end SetStats

        public void ToggleBuySell()
        {
            // Toggle the action
            ToggleAction();

            // Check if we're in sell mode
            if (action == MarketAction.Sell)
            {
                // The market is selling; the player is buying
                
                // Update the button text
                buySellButton.transform.GetChild(0).GetComponent<Text>().text = "Sell";

                // Disable the accept button
                if (acceptButton.activeInHierarchy)
                {
                    acceptButton.SetActive(false);
                } // end if

                // The sale was cancelled
                SellItems(false);
            } // end if
            else
            {
                // Otherwise, the market is buying; the player is selling
                
                // Update the button text
                buySellButton.transform.GetChild(0).GetComponent<Text>().text = "Buy";

                 // Enable the accept button
                if (!acceptButton.activeInHierarchy)
                {
                    acceptButton.SetActive(true);
                } // end if
            } // end else
        } // end ToggleBuySell

        void ToggleAction()
        {
            if (action == MarketAction.Sell)
            {
                // Toggle to buy action
                action = MarketAction.Buy;
            } // end if
            else
            {
                // Toggle to sell action
                action = MarketAction.Sell;
            } // end else
        } // end ToggleAction

        public void SellItems(bool isSelling = true)
        {
            // Get all the items in the buy items window
            List<Item> tempItems = GetItems(5).FindAll(item => item.Name != string.Empty);

            // Get the inventory for the player
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("PlayerInventory").GetComponent<PlayerInventory>();

            // Get the player's merchant
            Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(GameMaster.Instance.Turn).Entity;
            
            // Check if the player is actually selling the items
            if (isSelling)
            {
                // The player is actually selling their items

                // Loop through and sell the items for the player
                foreach (Item item in tempItems)
                {
                    // Check if the item is a piece of equipment
                    if (item is Equipment)
                    {
                        playerMerchant.Currency += ((Equipment)item).CostValue;
                    } // end if
                    // Check if the item is a resource
                    else if (item is Resource)
                    {
                        playerMerchant.Currency += ((Resource)item).Worth;
                    } // end else if
                } // end foreach

                // Update the inventory's stats after the sale
                inventory.SetStats(playerMerchant);
            } // end if
            else
            {
                // Otherwise, the player cancelled their sale so return the items

                // Make sure there are items to return
                if (tempItems.Count > 0)
                {
                    // Loop through all the buy items and add them back to the player's inventory
                    foreach (Item item in tempItems)
                    {
                        // Add the current item to the player's inventory
                        inventory.AddItem(GameMaster.Instance.Turn, item.Id, SlotType.Inventory);
                    } // end foreach
                } // end if
            }

            // Clear the buy items list
            ClearItemList(5);

            // Recreate the buy items list
            CreateItemList(5, numInventorySlotsCreate);
        }

        // Gets the action the market is doing
        public MarketAction Action
        {
            get { return action; }
        } // end Action
    } // end Market
} // end GSP.Itens.Inventories
