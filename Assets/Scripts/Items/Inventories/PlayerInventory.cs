/*******************************************************************************
 *
 *  File Name: PlayerInventory.cs
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
     * Name: PlayerInventory
     * 
     * Description: The logic for the new inventory system
     * 
     *******************************************************************************/
    public class PlayerInventory : Inventory<InventorySlot>
    {
        Dictionary<int, List<Item>> items;  // The list of items for the inventory

        int numInventorySlotsCreate;    // The number of inventory slots to create
        int numEquipmentSlotsCreate;    // The number of equipment slots to create
        int numBonusSlotsCreate;        // The number of bonus slots to create
        int equipmentSlots;             // The slot number after where the equipment slots end
        int bonusSlots;                 // The slot number after where the bonus slots end
        int weaponSlot;                 // The slot number of the equipped weapon
        int armorSlot;                  // The slot number of the equipped armor

        Transform bottomGrid;       // The Inentory's Bottom Panel
        Transform equipmentPanel;   // The Inventory's Top/Equipment Panel
        Transform bonusPanel;       // The Inventory's Top/Bonus Panel
        Transform statusLeft;       // The Inventory's Top/StatusLeft Panel
        Transform statusRight;       // The Inventory's Top/StatusRight Panel

        int playerNum;  // The current player's turn

        // Use this for initialisation
        protected override void Awake()
        {
            // Call the parent's Awake() first
            base.Awake();
            
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

            // Initialise the dictionary
            items = new Dictionary<int, List<Item>>();

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
        protected override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Create the inventory slots
            CreateSlots(numInventorySlotsCreate, SlotType.Inventory, bottomGrid, "InventorySlot ");

            // Create the equipment slots
            CreateSlots(numEquipmentSlotsCreate, SlotType.Equipment, equipmentPanel, "EquipmentSlot ");

            // Create the bonus slots
            CreateSlots(numBonusSlotsCreate, SlotType.Bonus, bonusPanel, "BonusSlot ");

            // Initialise player number to zero
            playerNum = 0;

            // Set the inventory to the current player's items
            SetList(GetItems(playerNum));
        } // end Start

        // Runs each frame; used to update the tooltip's position
        protected override void Update()
        {
            // Call the parent's Update() first
            base.Update();

            // Get the player number if it has changed
            if (playerNum != GameMaster.Instance.Turn)
            {
                // Get a temporary list of items
                List<Item> tempItems = Items;

                // Set the previous players items
                SetItems(playerNum, tempItems);

                // Set the player number to the current turn
                playerNum = GameMaster.Instance.Turn;

                // Set the inventory to the current player's items
                SetList(GetItems(playerNum));
            } // end if
        } // end Update

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

                    // Get the player's merchant
                    Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;

                    // The item is armour so check if there's already armour equipped
                    if (armor.Name == string.Empty)
                    {
                        // There is no armour already equipped; Swap slot places with the item
                        SwapItem(armorSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal the the equipping
                        playerMerchant.EquipArmor((Armor)item);
                    } // end if
                    else
                    {
                        // Otherwise there is already armour equipped; Swap slot places with the item
                        SwapItem(armorSlot, itemIndex);

                        // Update the tooltip
                        ShowTooltip(armor);

                        // Then deal with the unequipping and equipping
                        playerMerchant.UnequipArmor((Armor)armor);
                        playerMerchant.EquipArmor((Armor)item);
                    } // end else

                    // Update the inventory's stats
                    SetStats(playerMerchant);

                    // Return success
                    return true;
                } // end if
                // Check if the item is a weapon
                else if (item is Weapon)
                {
                    // Get the item in the weapon slot
                    Item weapon = items[playerNum][weaponSlot];

                    // Get the player's merchant
                    Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;

                    // The item is a weapon so check if there's already a weapon equipped
                    if (weapon.Name == string.Empty)
                    {
                        // There is no weapon already equipped; Swap slot places with the item
                        SwapItem(weaponSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal with the unequipping and equipping
                        playerMerchant.EquipWeapon((Weapon)item);
                    } // end if
                    else
                    {
                        // Otherwise there is already a weapon equipped; Swap slot places with the item
                        SwapItem(weaponSlot, itemIndex);

                        // Update the tooltip
                        ShowTooltip(weapon);

                        // Then deal with the unequipping and equipping
                        playerMerchant.UnequipWeapon((Weapon)weapon);
                        playerMerchant.EquipWeapon((Weapon)item);
                    } // end else

                    // Update the inventory's stats
                    SetStats(playerMerchant);

                    // Return success
                    return true;
                } // end if
                // Check if the item is a bonus item
                else if (item is Bonus)
                {
                    int freeSlot;   // The first slot that is free in the bonus inventory

                    // Get the player's merchant
                    Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;

                    // Make sure there's enough enough space
                    if ((freeSlot = FindFreeSlot(SlotType.Bonus)) >= 0)
                    {
                        // Swap slot places with the item
                        SwapItem(freeSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal with the equipping
                        playerMerchant.EquipBonus((Bonus)item);

                        // Update the inventory's stats
                        SetStats(playerMerchant);

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

        // Gets the first empty slot of the given SlotType
        public override int FindFreeSlot(SlotType slotType)
        {
            // Find the next free slot using the parent's calculations
            int freeSlot = base.FindFreeSlot(slotType);

            // Check if we found a free slot
            if (freeSlot < 0)
            {
                // No free slots available so return negative one
                return -1;
            } // end if

            // Check if the slot type is bonus
            if (slotType == SlotType.Bonus)
            {
                // Clamp the slot to bonus range
                freeSlot = Utility.ClampInt(freeSlot, equipmentSlots, bonusSlots);
            } // end if
            else
            {
                // Clamp the slot to inventory range
                freeSlot = Utility.ClampInt(freeSlot, 0, numInventorySlotsCreate);
            } // end else

            // Return the first empty slot
            return freeSlot;
        } // end FindFreeSlot

        // Sets the player's stats for the status panels
        public override void SetStats(Merchant player)
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
        public override void SetPlayer(int playerNum)
        {
            // Call the parent's SetPlayer() first
            base.SetPlayer(playerNum);
            
            // Set the player's stats
            SetStats((Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity);
        } // end SetPlayer

        // Gets the items from the inventory
        public override List<Item> GetItems(int playerNum)
        {
            // Get a temporary list from the items list
            List<Item> tempItems = items[playerNum];

            // Return the temp list
            return tempItems;
        } // end GetItems

        // Sets the list of items to another list
        protected override void SetItems(int playerNum, List<Item> newItems)
        {
            // Clear the old list and set it to the new list
            items[playerNum].Clear();
            items[playerNum] = newItems;
        } // end SetItems

        // Changes the player's list in the inventory
        public void ChangePlayer(int player)
        {
            // Set the previous player's items if possible
            if (player > 0 && player < (GameMaster.Instance.NumPlayers - 1))
            {
                // Get a temporary list of items
                List<Item> tempItems = Items;

                // Set the previous players items
                SetItems((player - 1), tempItems);
            } // end if
            else if (player == (GameMaster.Instance.NumPlayers - 1))
            {
                // Get a temporary list of items
                List<Item> tempItems = Items;

                // Set the previous players items
                SetItems((GameMaster.Instance.NumPlayers - 1), tempItems);
            } // end else if

            // Set the inventory to the given player's items
            SetList(GetItems(player));
        } // end ChangePlayer

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
    } // end PlayerInventory
} // end GSP.Items.Inventories
