/*******************************************************************************
 *
 *  File Name: Inventory.cs
 *
 *  Description: Contains the logic of the new inventory system. This is more
 *               functional that the old system, but it's still pretty minimal.
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Neutrals;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GSP.Items.Inventories
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
        Dictionary<int, List<Item>> items;  // The list of items for the inventory

        List<GameObject> slots;         // The list of inventory slots for the inventory
        int numInventorySlotsCreate;    // The number of inventory slots to create
        int numEquipmentSlotsCreate;    // The number of equipment slots to create
        int numBonusSlotsCreate;        // The number of bonus slots to create
        int equipmentSlots;             // The slot number after where the equipment slots end
        int bonusSlots;                 // The slot number after where the bonus slots end
        int weaponSlot;                 // The slot number of the equipped weapon
        int armorSlot;                  // The slot number of the equipped armor
        
        bool canShowTooltip;        // Whether the tooltip is show
        Transform bottomGrid;       // The Inentory's Bottom Panel
        Transform equipmentPanel;   // The Inventory's Top/Equipment Panel
        Transform bonusPanel;       // The Inventory's Top/Bonus Panel
        Transform statusLeft;       // The Inventory's Top/StatusLeft Panel
        Transform statusRight;       // The Inventory's Top/StatusRight Panel
        GameObject tooltip;         // The tooltip's GameObjectn
        RectTransform tooltipRect;  // The transform of the tooltip

        // Use this for initialisation
        void Awake()
        {
            // Get Inventory's Bottom panel
            bottomGrid = GameObject.Find("Inventory/Bottom").transform;

            // Get the Inventory's Top/Equipment panel
            equipmentPanel = GameObject.Find("Inventory/Top/Equipment").transform;

            // Get the Inventory's Top/Bonus panel
            bonusPanel = GameObject.Find("Inventory/Top/Bonus").transform;

            // Get the Inventory's Top/StatusLeft panel
            statusLeft = GameObject.Find("Inventory/Top/StatusLeft").transform;

            // Get the Inventory's Top/StatusRight panel
            statusRight = GameObject.Find("Inventory/Top/StatusRight").transform;
            
            // Initialise the list
            slots = new List<GameObject>();

            // Initialise the dictionary
            items = new Dictionary<int, List<Item>>();

            // Get the tooltip GameObject
            tooltip = GameObject.Find("Tooltip");
            tooltip.SetActive(false);

            // Initialise the number of slots to create
            numInventorySlotsCreate = 28;
            numEquipmentSlotsCreate = 2;
            numBonusSlotsCreate = 7;

            // Calculate the equipment and bonus slot ends
            equipmentSlots = numInventorySlotsCreate + numEquipmentSlotsCreate;
            bonusSlots = equipmentSlots + numBonusSlotsCreate;

            // Add all the EmptyItem's for all the players to match the slots
            for (int key = 0; key < 4; key++)
            {
                items.Add(key, new List<Item>());

                for (int index = 0; index < bonusSlots; index++)
                {
                    items[key].Add(ItemDatabase.Instance.Items.Find(item => item.Type == "Empty"));
                } // end for index
            } // end for key

            // Get the weapon and armor slots
            // Note: Since there's only two slots and this is a minimal inventory, the first slot starts after the inventory
            // slots and the second slot starts one slot before the bonus slots begin
            weaponSlot = numInventorySlotsCreate;
            armorSlot = equipmentSlots - 1;
        } // end Awake
        
        // Use this for initialisation
        void Start()
        {
            // Iniialise the tooltip as not shown
            canShowTooltip = false;

            // Get the reference to the tooltips RectTransform
            tooltipRect = tooltip.GetComponent<RectTransform>();

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

                    // Set the slot's type to inventory
                    slot.GetComponent<InventorySlot>().SlotType = SlotType.Inventory;
                } // end if
                // Check if the index is in the range of the equipment slots
                else if (index >= numInventorySlotsCreate && index < equipmentSlots)
                {
                    // Equipment slots are parented to the Inventory's Top/Equipment panel
                    slot.transform.SetParent(equipmentPanel);

                    // Name the slot in the editor for convienience
                    slot.name = "EquipmentSlot " + ((index - numInventorySlotsCreate) + 1) + " (" + index + ")";

                    // Set the slot's type to equipment
                    slot.GetComponent<InventorySlot>().SlotType = SlotType.Equipment;
                } // end else if
                // Check if the index is in the range of the bonus slots
                else if (index >= equipmentSlots && index < bonusSlots)
                {
                    // Bonus slots are parented to the Inventory's Top/Bonus panel
                    slot.transform.SetParent(bonusPanel);

                    // Name the slot in the editor for convienience
                    slot.name = "BonusSlot " + ((index - equipmentSlots) + 1) + " (" + index + ")";

                    // Set the slot's type to bonus
                    slot.GetComponent<InventorySlot>().SlotType = SlotType.Bonus;
                } // end else if
                
                // Change the slotId
                slot.GetComponent<InventorySlot>().SlotId = index;

                // Add the slot to the list
                slots.Add(slot);
            } // end for
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

        // Add an item to the inventory for the given player
        public bool AddItem(int playerNum, int itemId)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(item => item.Id == itemId))
            {
                int freeSlot;   // The first slot that is free
                
                // Check if there's space for the item
                if ((freeSlot = FindFreeSlot(playerNum, SlotType.Inventory)) >= 0)
                {
                    // Get the item from the database
                    Item tempItem = database[database.FindIndex(item => item.Id == itemId)];

                    // Place it in the free slot
                    items[playerNum][freeSlot] = tempItem;

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

        // Add an item to the inventory for a given player from a save file
        public bool AddItemFromSave(int playerNum, int itemId, int slotNum)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(item => item.Id == itemId))
            {
                // Get the item from the database
                Item tempItem = database[database.FindIndex(item => item.Id == itemId)];

                // Place it in the given slot
                items[playerNum][slotNum] = tempItem;

                // Return success
                return true;
            } // end if
            else
            {
                // The item didn't exist in the database to return failure
                Debug.LogErrorFormat("The Id '{0}' does not exist in the ItemDatabase!", itemId);
                return false;
            } // end else
        } // end AddItemFromSave

        // Equips an Equipment item
        public bool EquipItem(int playerNum, Equipment item)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(tempItem => tempItem.Id == item.Id))
            {
                // The item's index in the inventory
                int itemIndex = items[playerNum].FindIndex(aItem => aItem.Id == item.Id);
                // Check if the item is a piece of armour
                if (item is Armor)
                {
                    // Get the item in the armour slot
                    Item armor = items[playerNum][armorSlot];

                    // The item is armour so check if there's already armour equipped
                    if (armor.Name == string.Empty)
                    {
                        // There is no armour already equipped; Swap slot places with the item
                        SwapItem(playerNum, armorSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal the the equipping
                        Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;
                        playerMerchant.EquipArmor((Armor)item);
                    } // end if
                    else
                    {
                        // Otherwise there is already armour equipped; Swap slot places with the item
                        SwapItem(playerNum, armorSlot, itemIndex);

                        // Update the tooltip
                        ShowTooltip(armor);

                        // Then deal with the unequipping and equipping
                        Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;
                        playerMerchant.UnequipArmor((Armor)armor);
                        playerMerchant.EquipArmor((Armor)item);
                    } // end else

                    // Return success
                    return true;
                } // end if
                // Check if the item is a weapon
                else if (item is Weapon)
                {
                    // Get the item in the weapon slot
                    Item weapon = items[playerNum][weaponSlot];

                    // The item is a weapon so check if there's already a weapon equipped
                    if (weapon.Name == string.Empty)
                    {
                        // There is no weapon already equipped; Swap slot places with the item
                        SwapItem(playerNum, weaponSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal with the unequipping and equipping
                        Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;
                        playerMerchant.EquipWeapon((Weapon)item);
                    } // end if
                    else
                    {
                        // Otherwise there is already a weapon equipped; Swap slot places with the item
                        SwapItem(playerNum, weaponSlot, itemIndex);

                        // Update the tooltip
                        ShowTooltip(weapon);

                        // Then deal with the unequipping and equipping
                        Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;
                        playerMerchant.UnequipWeapon((Weapon)weapon);
                        playerMerchant.EquipWeapon((Weapon)item);
                    } // end else

                    // Return success
                    return true;
                } // end if
                // Check if the item is a bonus item
                else if (item is Bonus)
                {
                    int freeSlot;   // The first slot that is free in the bonus inventory

                    // Make sure there's enough enough space
                    if ((freeSlot = FindFreeSlot(playerNum, SlotType.Bonus)) >= 0)
                    {
                        // Swap slot places with the item
                        SwapItem(playerNum, freeSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal with the equipping
                        Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;
                        playerMerchant.EquipBonus((Bonus)item);

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
            } // end else
        } // end EquipItem

        // Removes an item from the inventory
        public void Remove(int playerNum, int slotNum)
        {
            // Remove the item at the given slot
            items[playerNum][slotNum] = ItemDatabase.Instance.Items.Find(item => item.Type == "Empty");

            // Disable the tooltip
            ShowTooltip(null, false);
        } // end Remove

        // Removes an item from the inventory
        public void Remove(int playerNum, Item item)
        {
            // Find the index of the item
            int index = items[playerNum].FindIndex(tempItem => tempItem.Id == item.Id);

            // Remove the item
            Remove(playerNum, index);
        } // end Remove

        // Swaps an item's place in the inventory with another slot
        public void SwapItem(int playerNum, Item a, Item b)
        {
            int aSlot;  // The slot item a resides in
            int bSlot;  // The slot item b resides in

            // Get the item's indices
            aSlot = items[playerNum].FindIndex(aItem => aItem.Id == a.Id);
            bSlot = items[playerNum].FindIndex(bItem => bItem.Id == b.Id);

            // Now swap the items
            items[playerNum][aSlot] = b;
            items[playerNum][bSlot] = a;
        } // end SwapItem

        // Swaps an item's place in the inventory with another slot
        public void SwapItem(int playerNum, int slotNumA, int slotNumB)
        {
            // Get the items in the slots
            Item aItem = items[playerNum][slotNumA];
            Item bItem = items[playerNum][slotNumB];

            // Swap the slots
            items[playerNum][slotNumA] = bItem;
            items[playerNum][slotNumB] = aItem;
        } // end SwapItem

        // Gets the first empty slot of the given SlotType
        public int FindFreeSlot(int playerNum, SlotType slotType)
        {
            int freeSlot = -1;  // The next free slot of the given type
            int totalFreeSlot;  // The slot free between the inventory and bonus inventory

            // Find the next free slot
            totalFreeSlot = FindAvailableSlot(playerNum, slotType);

            // Check if we found a free slot
            if (totalFreeSlot < 0)
            {
                // No free slots available so return negative one
                return -1;
            } // end if

            // Check if the slot type is bonus
            if (slotType == SlotType.Bonus)
            {
                // Clamp the slot to bonus range
                freeSlot = Utility.ClampInt(totalFreeSlot, equipmentSlots, bonusSlots);
            } // end if
            else
            {
                // Clamp the slot to inventory range
                freeSlot = Utility.ClampInt(totalFreeSlot, 0, numInventorySlotsCreate);
            } // end else

            // Return the first empty slot
            return freeSlot;
        } // end FindFreeSlot

        // Gets the first empty slot of the given SlotType
        int FindAvailableSlot(int playerNum, SlotType slotType)
        {
            // The next free slot of the given type
            int freeSlot = -1;

            // Loop over the items and slots to determine the next free slot
            for (int index = 0; index < slots.Count; index++)
            {
                // Get the current slot's script reference
                InventorySlot inventorySlot = slots[index].GetComponent<InventorySlot>();
                
                // Check if the slot type matches
                if (inventorySlot.SlotType == slotType)
                {
                    // We have a matching slot type so check if the slot is empty
                    if (items[playerNum][index].Name == string.Empty)
                    {
                        // We have a matching free slot so set the slot to the current index
                        freeSlot = index;

                        // Now break out of the loop
                        break;
                    } // end if items[playerNum][index].Name == string.Empty
                } // end if
            } // end for

            // Return the first empty slot, if any
            return freeSlot;
        } // end FindAvailableSlot

        // Gets an item at the given index
        public Item GetItem(int playerNum, int slotNum)
        {
            return items[playerNum][slotNum];
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
                if (tooltip.activeInHierarchy)
                {
                    tooltip.SetActive(false);
                } // end if
            } // end else
        } // end ShowTooltip

        // Sets the player's inventory colour to their interface colour
        void SetInventoryColor(InterfaceColors interfaceColor)
        {
            // Get the colour for the player's interface colour
            Color color = Utility.InterfaceColorToColor(interfaceColor);
            
            // Get the Image component of the inventory and set its colour
            GetComponent<Image>().color = color;

            // Get the Image component of the tooltip and set its colour
            tooltip.GetComponent<Image>().color = color;
        } // end SetInventoryColor

        // Sets the player's stats for the status panels
        public void SetStats(Merchant player)
        {
            // Set the title's value
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = player.Name + "'s Inventory";
            
            // Set the Health status line's value
            statusLeft.GetChild(0).GetChild(1).GetComponent<Text>().text = player.Health.ToString() + "/" + player.MaxHealth.ToString();

            // Set the Attack status line's value
            statusLeft.GetChild(1).GetChild(1).GetComponent<Text>().text = player.AttackPower.ToString();

            // Set the Defence status line's value
            statusLeft.GetChild(2).GetChild(1).GetComponent<Text>().text = player.DefencePower.ToString();

            // Set the Weight status line's value
            statusLeft.GetChild(3).GetChild(1).GetComponent<Text>().text = player.TotalWeight.ToString() + "/" + player.MaxWeight.ToString();

            // Set the Gold status line's value
            statusRight.GetChild(0).GetChild(1).GetComponent<Text>().text = player.Currency.ToString();

            // Set the Profit status line's value
            statusRight.GetChild(1).GetChild(1).GetComponent<Text>().text = player.TotalWorth.ToString();
        } // end SetStats

        // Sets the inventory up for the current player
        public void SetPlayer(int playerNum)
        {
            // Set the inventory's colour
            SetInventoryColor(GameMaster.Instance.GetPlayerColor(playerNum));

            // Set the player's stats
            SetStats((Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity);
        } // end SetPlayer

        // Gets the items from the inventory
        public List<Item> GetItems(int playerNum)
        {
            // Get a temporary list from the items list
            List<Item> tempItems = items[playerNum];

            // Return the temp list
            return tempItems;
        } // end GetItems

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

        // Gets the weapon's slot
        public int WeaponSlot
        {
            get { return weaponSlot; }
        } // end WeaponSlot

        // Gets the armour's slot
        public int ArmorSlot
        {
            get { return armorSlot; }
        } // end ArmorSlot

        // Gets the items from the inventory
        public List<Item> Items
        {
            get
            { 
                // Get a temporary list from the items list
                List<Item> tempItems = items[GameMaster.Instance.Turn];
                
                // Return the temp list
                return tempItems;
            } // end get
        } // end Items
    } // end Inventory
} // end GSP.Items.Inventories
