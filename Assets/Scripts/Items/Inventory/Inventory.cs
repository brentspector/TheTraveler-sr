/*******************************************************************************
 *
 *  File Name: Inventory.cs
 *
 *  Description: Contains the logic of the new inventory system. This is more
 *               functional that the old system, but it's still pretty minimal.
 *
 *******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GSP.Items.Inventory
{
    /*******************************************************************************
     *
     * Name: Inventory
     * 
     * Description: The logic for the new inventory system
     * 
     *******************************************************************************/
    public class Inventory : MonoBehaviour
    {

        List<GameObject> inventorySlots;    // The list of inventory slots for the inventory
        List<GameObject> equipmentSlots;    // The list of equipment slots for the inventory
        List<GameObject> bonusSlots;        // The list of bonus slots for the inventory
        List<Item> inventoryItems;          // The list of items for the inventory
        List<Item> equipmentItems;          // The list of items for the inventory's equipment
        List<Item> bonusItems;              // The list of items for the inventory's bonus items
        int numInventorySlotsCreate;        // The number of inventory slots to create
        int numEquipmentSlotsCreate;        // The number of equipment slots to create
        int numBonusSlotsCreate;            // The number of bonus slots to create
        int numInventorySlots;              // The number of inventory slots currently created
        int numEquipmentSlots;              // The number of equipment slots currently created
        int numBonusSlots;                  // The number of bonus slots currently created
        
        bool canShowTooltip;        // Whether the tooltip is show
        Transform bottomGrid;       // The Inentory's Bottom Panel
        Transform equipmentPanel;   // The Inventory's Top/Equipment Panel
        Transform bonusPanel;       // The Inventory's Top/Bonus Panel
        GameObject tooltip;         // The tooltip's GameObjectn
        RectTransform tooltipRect;  // The transform of the tooltip

        void Awake()
        {
            if (ItemDatabase.Instance == null) { }
        }

        IEnumerator Add()
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(3);
            Debug.Log("Done");

            // Get the items from the database
            List<Item> database = ItemDatabase.Instance.Items;

            for (int index = 0; index < database.Count; index++)
            {
                AddItem(ItemDatabase.Instance.Items[index].Id);
            }
        }

        // Use this for initialisation
        void Start()
        {
            // Get Inventory's Bottom panel
            Transform bottomGrid = GameObject.Find("Inventory/Bottom").transform;

            // Get the Inventory's Top/Equipment panel
            Transform equipmentPanel = GameObject.Find("Inventory/Top/Equipment").transform;

            // Get the Inventory's Top/Bonus panel
            Transform bonusPanel = GameObject.Find("Inventory/Top/Bonus").transform;

            // Get the tooltip GameObject
            tooltip = GameObject.Find("Tooltip");
            tooltip.SetActive(false);

            // Iniialise the tooltip as not shown
            canShowTooltip = false;

            // Get the reference to the tooltips RectTransform
            tooltipRect = tooltip.GetComponent<RectTransform>();

            // Initialise the lists
            inventorySlots = new List<GameObject>();
            equipmentSlots = new List<GameObject>();
            bonusSlots = new List<GameObject>();
            inventoryItems = new List<Item>();
            equipmentItems = new List<Item>();
            bonusItems = new List<Item>();

            // Initialise the number of slots to create
            numInventorySlotsCreate = 28;
            numEquipmentSlotsCreate = 2;
            numBonusSlotsCreate = 7;

            // Initalise the number of slots to zero
            numInventorySlots = 0;
            numEquipmentSlots = 0;
            numBonusSlots = 0;

            // Loop to create the inventory slots
            for (int index = 0; index < numInventorySlotsCreate; index++)
            {
                // Create the slot
                GameObject slot = Instantiate(PrefabReference.prefabSlot) as GameObject;

                // Parent the slot to the Inventory's Bottom panel
                slot.transform.SetParent(bottomGrid);

                // Name the slot in the editor for convienience
                slot.name = "InventorySlot " + (index + 1);

                // Change the slotId
                slot.GetComponent<InventorySlot>().SlotId = numInventorySlots;

                // Add the slot to the list
                inventorySlots.Add(slot);

                // Add an empty item to the list for the slot
                inventoryItems.Add(new EmptyItem());

                // Increment the number of slots
                numInventorySlots++;
            } // end for

            // Loop to create the equipment slots
            for (int index = 0; index < numEquipmentSlotsCreate; index++)
            {
                // Create the slot
                GameObject slot = Instantiate(PrefabReference.prefabSlot) as GameObject;

                // Parent the slot to the Inventory's Bottom panel
                slot.transform.SetParent(equipmentPanel);

                // Name the slot in the editor for convienience
                slot.name = "EquipmentSlot " + (index + 1);

                // Change the slotId
                slot.GetComponent<InventorySlot>().SlotId = numEquipmentSlots;

                // Add the slot to the list
                equipmentSlots.Add(slot);

                // Add an empty item to the list for the slot
                equipmentItems.Add(new EmptyItem());

                // Increment the number of slots
                numEquipmentSlots++;
            } // end for

            // Loop to create the bonus slots
            for (int index = 0; index < numBonusSlotsCreate; index++)
            {
                // Create the slot
                GameObject slot = Instantiate(PrefabReference.prefabSlot) as GameObject;

                // Parent the slot to the Inventory's Bottom panel
                slot.transform.SetParent(bonusPanel);

                // Name the slot in the editor for convienience
                slot.name = "BonusSlot " + (index + 1);

                // Change the slotId
                slot.GetComponent<InventorySlot>().SlotId = numBonusSlots;

                // Add the slot to the list
                bonusSlots.Add(slot);

                // Add an empty item to the list for the slot
                bonusItems.Add(new EmptyItem());

                // Increment the number of slots
                numEquipmentSlots++;
            } // end for

            StartCoroutine(Add());
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

        // Add an item to the inventory
        public bool AddItem(int itemId)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(item => item.Id == itemId))
            {
                int freeSlot;   // The first slot that is free
                
                // Check if there's space for the item
                if ((freeSlot = FindFreeInventorySlot()) >= 0)
                {
                    // Get the item from the database
                    Item tempItem = database[database.FindIndex(item => item.Id == itemId)];

                    // Place it in the free slot
                    inventoryItems[freeSlot] = tempItem;

                    // Return success
                    return true;
                } // end if

                // Otherwise, return failure as there isn't enough space
                Debug.LogFormat("No space for item of Id '{0}' in the inventory.", itemId);
                return false;
            } // end if
            else
            {
                // The item didn't exist in the database to return failure
                Debug.LogErrorFormat("The Id '{0}' does not exist in the ItemDatabase!", itemId);
                return false;
            } // end else
        } // end AddItem

        // Gets the first empty inventory slot
        public int FindFreeInventorySlot()
        {
            // Return the first empty slot.
            return inventoryItems.FindIndex(item => item.Name == string.Empty);
        } // end FindFreeInventorySlot

        // Gets the first empty inventory slot
        public int FindFreeEquipmentSlot()
        {
            // Return the first empty slot.
            return equipmentItems.FindIndex(item => item.Name == string.Empty);
        } // end FindFreeEquipmentSlot

        // Gets the first empty inventory slot
        public int FindFreeBonusSlot()
        {
            // Return the first empty slot.
            return bonusItems.FindIndex(item => item.Name == string.Empty);
        } // end FindFreeBonusSlot

        // Gets an item at the given index
        public Item GetItem(int itemId)
        {
            return inventoryItems[itemId];
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
                tooltip.SetActive(true);

                // Get the Title Text child
                Text tooltipTitleText = tooltip.transform.GetChild(0).GetComponent<Text>();

                // Get the Body Text Child
                Text tooltipBodyText = tooltip.transform.GetChild(1).GetComponent<Text>();

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
                // Otherwise, the item must be a resource
                else
                {
                    // Set the tooltip's body text
                    tooltipBodyText.text = "Weight: -" + ((Resource)item).Weight + "\nWorth: " + ((Resource)item).Worth;
                } // end else
            }
            else
            {
                // Otherwise, disable the tooltip window
                tooltip.SetActive(false);
            } // end else
        } // end ShowTooltip
    } // end GSP.Items.Inventory
} // end GSP.Items
