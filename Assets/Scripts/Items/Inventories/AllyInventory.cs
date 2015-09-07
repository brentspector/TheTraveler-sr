/*******************************************************************************
 *
 *  File Name: AllyInventory.cs
 *
 *  Description: Contains the logic of the new inventory system for allies.
 *               This is like the player's inventory system.
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Neutrals;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GSP.Items.Inventories
{
    //TODO: Turn this from player to ally inventory
    /*******************************************************************************
     *
     * Name: AllyInventory
     * 
     * Description: The logic for the new inventory system for allies.
     * 
     *******************************************************************************/
    public class AllyInventory : Inventory<AllySlot>
    {
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

        // Use this for initialisation
        protected override void Awake()
        {
            // Call the parent's Awake() first
            base.Awake();

            // Get Inventory's Bottom panel
            bottomGrid = GameObject.Find("PlayerInventory/Bottom").transform;

            // Get the Inventory's Top/Equipment panel
            equipmentPanel = GameObject.Find("PlayerInventory/Top/Equipment").transform;

            // Get the Inventory's Top/Bonus panel
            bonusPanel = GameObject.Find("PlayerInventory/Top/Bonus").transform;

            // Get the Inventory's Top/StatusLeft panel
            statusLeft = GameObject.Find("PlayerInventory/Top/StatusLeft").transform;

            // Get the Inventory's Top/StatusRight panel
            statusRight = GameObject.Find("PlayerInventory/Top/StatusRight").transform;

            // Initialise the number of slots to create
            numInventorySlotsCreate = 28;
            numEquipmentSlotsCreate = 2;
            numBonusSlotsCreate = 7;

            // Calculate the equipment and bonus slot ends
            equipmentSlots = numInventorySlotsCreate + numEquipmentSlotsCreate;
            bonusSlots = equipmentSlots + numBonusSlotsCreate;

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
            CreateSlots(numBonusSlotsCreate, SlotType.Bonus, bonusPanel, "BonusSlot "); ;
        } // end Start

        // Runs each frame; used to update the tooltip's position
        protected override void Update()
        {
            // Call the parent's Update() first
            base.Update();
        } // end Update

        // Creates the list of items for the ally
        public void CreateAllyItemList(int allyNum)
        {
            // Check if the list needs to be created
            if (GetItems(allyNum).Count == 0)
            {
                // Create the player's item list
                CreateItemList(allyNum, bonusSlots);
            } // end if
        } // end CreateAllyItemList

        // Equips an Equipment item
        public bool EquipItem(int playerNum, Equipment item)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;

            // Only proceed if the ID exists in the database
            if (database.Exists(tempItem => tempItem.Id == item.Id))
            {
                // The item's index in the inventory
                int itemIndex = GetItems(playerNum).FindIndex(aItem => aItem.Id == item.Id);
                // Check if the item is a piece of armour
                if (item is Armor)
                {
                    // Get the item in the armour slot
                    Item armor = GetItem(playerNum, armorSlot);

                    // Get the player's merchant
                    Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;

                    // The item is armour so check if there's already armour equipped
                    if (armor.Name == string.Empty)
                    {
                        // There is no armour already equipped; Swap slot places with the item
                        SwapItem(playerNum, armorSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal the the equipping
                        playerMerchant.EquipArmor((Armor)item);
                    } // end if
                    else
                    {
                        // Otherwise there is already armour equipped; Swap slot places with the item
                        SwapItem(playerNum, armorSlot, itemIndex);

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
                    Item weapon = GetItem(playerNum, weaponSlot);

                    // Get the player's merchant
                    Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerNum).Entity;

                    // The item is a weapon so check if there's already a weapon equipped
                    if (weapon.Name == string.Empty)
                    {
                        // There is no weapon already equipped; Swap slot places with the item
                        SwapItem(playerNum, weaponSlot, itemIndex);

                        // Disable the tooltip
                        ShowTooltip(null, false);

                        // Then deal with the unequipping and equipping
                        playerMerchant.EquipWeapon((Weapon)item);
                    } // end if
                    else
                    {
                        // Otherwise there is already a weapon equipped; Swap slot places with the item
                        SwapItem(playerNum, weaponSlot, itemIndex);

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
                    if ((freeSlot = FindFreeSlot(playerNum, SlotType.Bonus)) >= 0)
                    {
                        // Swap slot places with the item
                        SwapItem(playerNum, freeSlot, itemIndex);

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

            // Check if the slot type is bonus
            if (slotType == SlotType.Bonus)
            {
                // Clamp the slot to bonus range
                freeSlot = Utility.ClampInt(freeSlot, equipmentSlots, BonusSlotEnd);
            } // end if
            else
            {
                // Clamp the slot to inventory range
                freeSlot = Utility.ClampInt(freeSlot, 0, (numInventorySlotsCreate - 1));
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
    } // end AllyInventory
} // end GSP.Items.Inventories
