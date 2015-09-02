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
    public class Market : MonoBehaviour
    {
        List<Item> sellItems;           // The list of items for the inventory for selling
        List<Item> buyItems;            // The list of items for the inventory for buying
        List<GameObject> slots;         // The list of inventory slots for the inventory
        int numInventorySlotsCreate;    // The number of inventory slots to create

        bool canShowTooltip;        // Whether the tooltip is show
        Transform bottomGrid;       // The Inentory's Bottom Panel
        GameObject tooltip;         // The tooltip's GameObjectn
        RectTransform tooltipRect;  // The transform of the tooltip

        GameObject buySellButton;   // Reference for the Market's buy/sell button
        GameObject acceptButton;    // Reference for the Market's accept button

        MarketAction action;    // The action the market is doing

        // Use this for initialisation
        void Awake()
        {
            // Get Inventory's Bottom panel
            bottomGrid = GameObject.Find("Market/Body").transform;

            // Initialise the lists
            slots = new List<GameObject>();
            sellItems = new List<Item>();
            buyItems = new List<Item>();

            // Get the tooltip GameObject
            tooltip = GameObject.Find("Canvas").transform.Find("Tooltip").gameObject;

            // Get the button objects
            buySellButton = GameObject.Find("Market/Buttons/BuySellButton");
            acceptButton = GameObject.Find("Market/Buttons/AcceptButton");

            // Disable the accept button by default
            acceptButton.SetActive(false);

            // Initialise the number of slots to create
            numInventorySlotsCreate = 30;

            // Get the empty item to add later
            Item emptyItem = ItemDatabase.Instance.Items.Find(item => item.Type == "Empty");

            // Add the empty items to the inventory
            for (int index = 0; index < numInventorySlotsCreate; index++)
            {
                // Add an empty item to the lists
                sellItems.Add(emptyItem);
                buyItems.Add(emptyItem);
            } // end for
        } // end Awake
        
        // Use this for initialisation
        void Start()
        {
            // Iniialise the tooltip as not shown
            canShowTooltip = false;

            // Get the reference to the tooltips RectTransform
            tooltipRect = tooltip.GetComponent<RectTransform>();

            // Loop to create the market slots
            for (int index = 0; index < numInventorySlotsCreate; index++)
            {
                // Create the slots
                GameObject slot = Instantiate(PrefabReference.prefabMarketSlot) as GameObject;

                // Market slots are parented to the Market's Body panel
                slot.transform.SetParent(bottomGrid);

                // Name the slot in the editor for convienience
                slot.name = "MarketSlotSell " + (index + 1) + " (" + index + ")";

                // Set the slot's type to market
                slot.GetComponent<MarketSlot>().SlotType = SlotType.Market;

                // Change the slotId
                slot.GetComponent<MarketSlot>().SlotId = index;

                // Add the slots to the lists
                slots.Add(slot);
            } // end for

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
                sellItems[index] = equipment[index];
            } // end for

            // The default action for the market is selling
            action = MarketAction.Sell;
        } // end Start

        // Runs each frame; used to update the tooltip's position
        void Update()
        {
            // Only proceed if the tooltip exists
            if (tooltip != null)
            {
                // Check if the tooltip is shown
                if (canShowTooltip)
                {
                    tooltipRect.position = new Vector3((Input.mousePosition.x + 15.0f), (Input.mousePosition.y - 10.0f), 0.0f);
                } // end if canShowTooltip
            } // end if
        } // end Update

        // Gets an item at the given index
        public Item GetItem(int slotNum)
        {
            // Return the item based upon the current action
            if (action == MarketAction.Sell)
            {
                // Return the selling items
                return sellItems[slotNum];
            } // end if
            else
            {
                // Otherwise return the buying items
                return buyItems[slotNum];
            } // end else
        } // end GetItem

        // Shows the tooltip window for item information
        public void ShowTooltip(Item item, bool canShow = true)
        {
            // Store the canShow bool for updating the tooltip
            canShowTooltip = canShow;

            // Check if we're showing the tooltip
            if (canShow)
            {
                // Enable the tooltip window
                if (!tooltip.activeInHierarchy)
                {
                    tooltip.SetActive(true);
                } // end if

                // Get the Title Text child
                Text tooltipTitleText = tooltip.transform.GetChild(0).GetChild(0).GetComponent<Text>();

                // Get the Body Text Child
                Text tooltipBodyText = tooltip.transform.GetChild(0).GetChild(1).GetComponent<Text>();

                // Set the tooltip's title text
                tooltipTitleText.text = item.Name;

                // Check if the item is a piece of armour
                if (item is Armor)
                {
                    // Set the tooltip's body text
                    tooltipBodyText.text = "Defence: +" + ((Armor)item).DefenceValue + "\nCost: " + ((Armor)item).CostValue;
                } // end if
                // Check if the item is a weapon
                else if (item is Weapon)
                {
                    // Set the tooltip's body text
                    tooltipBodyText.text = "Attack: +" + ((Weapon)item).AttackValue + "\nCost: " + ((Weapon)item).CostValue;
                } // end else if
                // Check if the item is a bonus
                else if (item is Bonus)
                {
                    string text = "";   // Holds the compiled string

                    // Check if the weight is set
                    if (((Bonus)item).WeightValue > 0)
                    {
                        // Append the weight text
                        text += "Weight: +" + ((Bonus)item).WeightValue + "\n";
                    }

                    // Check if the inventory space variable is set
                    if (((Bonus)item).InventoryValue > 0)
                    {
                        // Append the space text
                        text += "Space: +" + ((Bonus)item).InventoryValue + "\n";
                    }

                    // Append the cost text
                    text += "Cost: " + ((Bonus)item).CostValue;

                    // Set the tooltip's body text
                    tooltipBodyText.text = text;
                } // end else if
            }
            else
            {
                // Otherwise, disable the tooltip window
                if (tooltip.activeInHierarchy)
                {
                    tooltip.SetActive(false);
                } // end if
            } // end else
        } // end ShowTooltip

        // Sets the inventory up for the current player
        public void SetPlayer(int playerNum)
        {
            // Set the inventory's colour
            SetInventoryColor(GameMaster.Instance.GetPlayerColor(playerNum));
        } // end SetPlayer

        // Sets the player's inventory colour to their interface colour
        void SetInventoryColor(InterfaceColors interfaceColor)
        {
            // Get the colour for the player's interface colour
            Color color = Utility.InterfaceColorToColor(interfaceColor);

            // Get the Image component of the inventory and set its colour
            GetComponent<Image>().color = color;
        } // end SetInventoryColor

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
            List<Item> tempItems = buyItems.FindAll(item => item.Name != string.Empty);

            // Get the inventory for the player
            Inventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<Inventory>();

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
                        inventory.AddItem(GameMaster.Instance.Turn, item.Id);
                    } // end foreach
                } // end if
            }

            // Clear the buy items list
            // Empty item for later
            Item emptyItem = ItemDatabase.Instance.Items.Find(item => item.Type == "Empty");

            // Loop through the buy items list to return it to an empty state
            for (int index = 0; index < numInventorySlotsCreate; index++)
            {
                buyItems[index] = emptyItem;
            } // end for
        }

        // Gets the action the market is doing
        public MarketAction Action
        {
            get { return action; }
        } // end Action
    } // end Market
} // end GSP.Itens.Inventories
