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

        List<GameObject> slots;         // The list of inventory slots for the inventory
        List<Item> items;               // The list of items for the inventory
        int numInventorySlotsCreate;    // The number of inventory slots to create
        int numEquipmentSlotsCreate;    // The number of equipment slots to create
        int numBonusSlotsCreate;        // The number of bonus slots to create
        int equipmentSlots;             // The slot number after where the equipment slots end
        int bonusSlots;                 // The slot number after where the bonus slots end
        
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
            slots = new List<GameObject>();
            items = new List<Item>();

            // Initialise the number of slots to create
            numInventorySlotsCreate = 28;
            numEquipmentSlotsCreate = 2;
            numBonusSlotsCreate = 7;

            // Calculate the equipment and bonus slot ends
            equipmentSlots = numInventorySlotsCreate + numEquipmentSlotsCreate;
            bonusSlots = equipmentSlots + numBonusSlotsCreate;

            // Loop to create the inventory slots
            for (int index = 0; index < bonusSlots; index++)
            {
                // Create the slot
                GameObject slot = Instantiate(PrefabReference.prefabInventorySlot) as GameObject;

                // Check if the index is in the range of the inventory slots
                if (index >= 0 && index < numInventorySlotsCreate)
                {
                    // Inventory slots are parented to the Inventory's Bottom panel
                    slot.transform.SetParent(bottomGrid);

                    // Name the slot in the editor for convienience
                    slot.name = "InventorySlot " + (index + 1) + " (" + index + ")";
                } // end if
                // Check if the index is in the range of the equipment slots
                else if (index >= numInventorySlotsCreate && index < equipmentSlots)
                {
                    // Equipment slots are parented to the Inventory's Top/Equipment panel
                    slot.transform.SetParent(equipmentPanel);

                    // Name the slot in the editor for convienience
                    slot.name = "EquipmentSlot " + ((index - numInventorySlotsCreate) + 1) + " (" + index + ")";
                } // end else if
                // Check if the index is in the range of the bonus slots
                else if (index >= equipmentSlots && index < bonusSlots)
                {
                    // Bonus slots are parented to the Inventory's Top/Bonus panel
                    slot.transform.SetParent(bonusPanel);

                    // Name the slot in the editor for convienience
                    slot.name = "BonusSlot " + ((index - equipmentSlots) + 1) + " (" + index + ")";
                } // end else if

                // Change the slotId
                slot.GetComponent<InventorySlot>().SlotId = index;

                // Add the slot to the list
                slots.Add(slot);

                // Add an empty item to the list for the slot
                items.Add(new EmptyItem());
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

        // Add an item to the inventory for the given SlotType
        public bool AddItem(int itemId)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(item => item.Id == itemId))
            {
                int freeSlot;   // The first slot that is free
                
                // Check if there's space for the item
                if ((freeSlot = FindFreeSlot(SlotType.Inventory)) >= 0)
                {
                    // Get the item from the database
                    Item tempItem = database[database.FindIndex(item => item.Id == itemId)];

                    // Place it in the free slot
                    items[freeSlot] = tempItem;

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

        // Equips an Equipment item
        public bool EquipItem(Equipment item)
        {
            // Get the weapon and armor slots
            // Note: Since there's only two slots and this is a minimal inventory, the first slot starts after the inventory
            // slots and the second slot starts one slot before the bonus slots begin
            int weaponSlot = numInventorySlotsCreate;
            int armorSlot = equipmentSlots - 1;

            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(tempItem => tempItem.Id == item.Id))
            {
                // Check if the item is a piece of armour
                if (item is Armor)
                {
                    // Get the item in the armour slot
                    Item armor = items[armorSlot];

                    // The item is armour so check if there's already armour equipped
                    if (armor.Name == string.Empty)
                    {
                        // There is no armour already equipped; Swap slot places with the item
                        SwapItem(armor, item);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal the the equipping
                        //TODO: Damien: Get the merchant and call Equip on the item later
                    } // end if
                    else
                    {
                        // Otherwise there is already armour equipped; Swap slot places with the item
                        SwapItem(items[armorSlot], item);

                        // Update the tooltip
                        ShowTooltip(armor);

                        // Then deal with the unequipping and equipping
                        //TODO: Damien: Get the merchant and call Unequip/Equip on the item later
                    } // end else

                    // Return success
                    return true;
                } // end if
                // Check if the item is a weapon
                else if (item is Weapon)
                {
                    // Get the item in the weapon slot
                    Item weapon = items[weaponSlot];

                    // The item is a weapon so check if there's already a weapon equipped
                    if (weapon.Name == string.Empty)
                    {
                        // There is no a weapon already equipped; Swap slot places with the item
                        SwapItem(weapon, item);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal the the equipping
                        //TODO: Damien: Get the merchant and call Equip on the item later
                    } // end if
                    else
                    {
                        // Otherwise there is already a weapon equipped; Swap slot places with the item
                        SwapItem(items[weaponSlot], item);

                        // Update the tooltip
                        ShowTooltip(weapon);

                        // Then deal with the unequipping and equipping
                        //TODO: Damien: Get the merchant and call Unequip/Equip on the item later
                    } // end else

                    // Return success
                    return true;
                } // end if
                // Check if the item is a bonus item
                else if (item is Bonus)
                {
                    int freeSlot;   // The first slot that is free in the bonus inventory

                    // Make sure there's enough enough space
                    if ((freeSlot = FindFreeSlot(SlotType.Bonus)) >= 0)
                    {
                        // Swap slot places with the item
                        SwapItem(items[freeSlot], item);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal with the equipping
                        //TODO: Damien: Get the merchant and call Equip on the item later

                        // Return success
                        return true;
                    }
                } // end if
                
                // Otherwise, return failure as there isn't enough space
                Debug.LogFormat("No space for item of Id '{0}' in the bonus inventory.", item.Id);
                return false;
            } // end if
            else
            {
                // The item didn't exist in the database to return failure
                Debug.LogErrorFormat("The Id '{0}' does not exist in the ItemDatabase!", item.Id);
                return false;
            } // end if
        } // end EquipItem

        // Swaps an item's place in the inventory with another slot
        public void SwapItem(Item a, Item b)
        {
            int aSlot;  // The slot item a resides in
            int bSlot;  // The slot item b resides in

            // Get the item's indices
            aSlot = items.FindIndex(aItem => aItem.Id == a.Id);
            bSlot = items.FindIndex(bItem => bItem.Id == b.Id);

            // Now swap the items
            items[aSlot] = b;
            items[bSlot] = a;
        } // end SwapItem

        // Gets the first empty slot of the given SlotType
        public int FindFreeSlot(SlotType slotType)
        {
            int freeSlot = -1;  // The next free slot of the given type
            int totalFreeSlot;  // The slot free between the inventory and bonus inventory
            
            // Check if the slot type is bonus
            if (slotType == SlotType.Bonus)
            {
                // Start the free slot search at the bonus inventory's start index
                totalFreeSlot = items.FindIndex(equipmentSlots, item => item.Name == string.Empty);
            } // end if
            else
            {
                // Otherwise start the free slot search at the beginning
                totalFreeSlot = items.FindIndex(item => item.Name == string.Empty);
            } // end else

            // Check if the free slot is in the range of the inventory slots
            if (slotType == SlotType.Inventory && (totalFreeSlot >= 0 && totalFreeSlot < numInventorySlotsCreate))
            {
                // The free slot is in the correct range so return it
                freeSlot = totalFreeSlot;
            } // end if
            // Check if the free slot is in the range of the bonus slots
            else if (slotType == SlotType.Bonus && (totalFreeSlot >= equipmentSlots && totalFreeSlot < bonusSlots))
            {
                // The free slot is in the correct range so return it
                freeSlot = totalFreeSlot;
            } // end if

            // Return the first empty slot
            return freeSlot;
        } // end FindFreeSlot

        // Gets an item at the given index
        public Item GetItem(int itemId)
        {
            return items[itemId];
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

        // Gets the beginning of the bonus slots
        public int BonusSlotBegin
        {
            get { return equipmentSlots; }
        } // end BonusSlotBegin

        // Gets the end of the bonus slots
        public int BonusSlotEnd
        {
            get { return bonusSlots - 1; }
        } // end BonusSlotEnd
    } // end GSP.Items.Inventory
} // end GSP.Items
