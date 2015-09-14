/*******************************************************************************
 *
 *  File Name: IBaseInventory
 *
 *  Description: Describes a contract for an inventory system functionality
 *               with generics.
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Items.Inventories
{
    /*******************************************************************************
     *
     * Name: IBaseInventory
     * 
     * Description: Supplies the functionality for the inventory system usimg
     *              generics
     * 
     *******************************************************************************/
    public interface IBaseInventory
    {
        // Create the slots for the inventory
        void CreateSlots(int slottKey, int numSlots, SlotType slotType, Transform parent, string slotName);

        // Add an item to the inventory for the given player
        bool AddItem(int slottKey, int key, int itemId, SlotType slotType);
        // Add an item to the inventory for a given player from a save file
        bool AddItemFromSave(int key, int itemId, int slotNum);
        // Removes an item from the inventory
        void Remove(int key, int slotNum);
        // Removes an item from the inventory
        void Remove(int key, Item item);

        // Gets an item at the given index
        Item GetItem(int key, int slotNum);

        // Swaps an item's place in the inventory with another slot
        void SwapItem(int key, Item a, Item b);
        // Swaps an item's place in the inventory with another slot
        void SwapItem(int key, int slotNumA, int slotNumB);

        // Gets the first empty slot of the given SlotType
        int FindFreeSlot(int slottKey, int key, SlotType slotType);

        // Shows the tooltip window for item information
        void ShowTooltip(Item item, bool canShow = true);

        // Gets the items list count for the specified key
        int GetItemsListCount(int key);
    } // end IBaseInventory
} // end GSP.Items.Inventories
