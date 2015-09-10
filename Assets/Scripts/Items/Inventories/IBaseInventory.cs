using UnityEngine;

namespace GSP.Items.Inventories
{
    public interface IBaseInventory
    {
        void CreateSlots(int numSlots, SlotType slotType, Transform parent, string slotName);

        bool AddItem(int key, int itemId, SlotType slotType);
        bool AddItemFromSave(int key, int itemId, int slotNum);
        void Remove(int key, int slotNum);
        void Remove(int key, Item item);

        Item GetItem(int key, int slotNum);

        void SwapItem(int key, Item a, Item b);
        void SwapItem(int key, int slotNumA, int slotNumB);

        int FindFreeSlot(int key, SlotType slotType);

        void ShowTooltip(Item item, bool canShow = true);
    }
} // end GSP.Items.Inventories
