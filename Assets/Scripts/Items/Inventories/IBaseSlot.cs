using UnityEngine;

namespace GSP.Items.Inventories
{
    public interface IBaseSlot
    {
        int SlotId { get; set; }

        int PlayerNumber { get; }
        int AllyNumber { get; }

        SlotType SlotType { get; set; }
    }
} // end GSP.Items.Inventories
