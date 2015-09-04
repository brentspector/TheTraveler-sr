using UnityEngine;

namespace GSP.Items.Inventories
{
    public interface IBaseInventory
    {
        void CreateSlots(int numSlots, SlotType slotType, Transform parent, string slotName);

        bool AddItem(int itemId);
        bool AddItemFromSave(int itemId, int slotNum);
        void Remove(int slotNum);
        void Remove(Item item);

        Item GetItem(int slotNum);

        void SwapItem(Item a, Item b);
        void SwapItem(int slotNumA, int slotNumB);

        int FindFreeSlot(SlotType slotType);

        void ShowTooltip(Item item, bool canShow = true);
    }
} // end GSP.Items.Inventories
