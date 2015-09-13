/*******************************************************************************
 *
 *  File Name: RecycleBins.cs
 *
 *  Description: Contains the logic of the recycle bin system. This is an
 *               inventory backend for the recycle bin system.
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
     * Name: RecycleBin
     * 
     * Description: The logic for the new recycle bin inventory system
     * 
     *******************************************************************************/
    public class RecycleBin : Inventory<RecycleSlot>
    {
        int numInventorySlotsCreate;    // The number of inventory slots to create

        Transform bottomGrid;   // The Inventory's Bottom Panel

        // Use this for initialisation
        protected override void Awake()
        {
            // Call the parent's Awake() first
            base.Awake();

            // Get Inventory's Bottom panel
            bottomGrid = GameObject.Find("RecycleBin/Body").transform;

            // Initialise the number of slots to create
            numInventorySlotsCreate = 30;

            // Add the sell and buy items lists
            CreateItemList(6, numInventorySlotsCreate);
        } // end Awake

        // Use this for initialisation
        protected override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Create the recycle slots
            CreateSlots(3, numInventorySlotsCreate, SlotType.Recycle, bottomGrid, "RecycleSlot ");
        } // end Start

        // Runs each frame; used to update the tooltip's position
        protected override void Update()
        {
            // Call the parent's Update() first
            base.Update();
        } // end Update

        // Gets the first empty slot of the given SlotType
        public override int FindFreeSlot(int slotKey, int key, SlotType slotType)
        {
            // Find the next free slot using the parent's calculations
            int freeSlot = base.FindFreeSlot(slotKey, key, slotType);

            // Check if we found a free slot
            if (freeSlot < 0)
            {
                // No free slots available so return negative one
                return -1;
            } // end if

            // Return the first empty slot clamped to the recycle range
            return Utility.ClampInt(freeSlot, 0, numInventorySlotsCreate);
        } // end FindFreeSlot

        // Set the stats for the status panels
        public override void SetStats(Merchant player)
        {
            // No stats to set, but have to implement this
        } // end SetStats

        // Sells or returns the player's items
        public void RecycleItems(bool isRecycling = true)
        {
            // Get all the items in the recycle bin window
            List<Item> tempItems = GetItems(5).FindAll(item => item.Name != string.Empty);

            // Get the inventory for the player
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("PlayerInventory").GetComponent<PlayerInventory>();

            // Check if the player is actually recycling the items
            if (!isRecycling)
            {
                // The player cancelled the recycling of their items so return them

                // Make sure there are items to return
                if (tempItems.Count > 0)
                {
                    // Loop through all the recycle bin items and add them back to the player's inventory
                    foreach (Item item in tempItems)
                    {
                        // Add the current item to the player's inventory
                        inventory.AddItem(0, GameMaster.Instance.Turn, item.Id, SlotType.Inventory);
                    } // end foreach
                } // end if
            } // end if

            // Clear the buy items list
            ClearItemList(6);

            // Recreate the buy items list
            CreateItemList(6, numInventorySlotsCreate);
        } // end RecycleItems

        // Gets the max space for the inventory
        public int MaxSpace
        {
            get { return numInventorySlotsCreate; }
        } // end MaxSpace
    } // end RecycleBin
}
