using UnityEngine;
using System.Collections;

namespace GSP.Entities.Interfaces
{
    interface IInventory
    {
        #region Properties

        // The resource list.
        Char.ResourceList Resources { get; set; }

        // The total weight of the resources.
        int TotalWeight { get; set; }

        // The total size of the resources.
        int TotalSize { get; set; }

        // The total value of the resources.
        int TotalValue { get; set; }

        // The maximum weight that is able to be carried.
        int MaxWeight { get; set; }

        // The maximum inventory space available.
        int MaxInventorySpace { get; set; }

        // The currency currently being held.
        int Currency { get; set; }
        
        #endregion

        #region Functions

        // Attempt to pickup a resource.
        void PickupResource(Char.Resource resource, int amount);

        // Sells a given resource and amount.
        void SellResource(Char.Resource resource, int amount);

        // Sells all the resources being carried.
        void SellResources();

        // Transfer currency from one to another.
        void TransferCurrency(GameObject other, int amount);

        // Transfer a resource from one to another.
        void TransferResource(GameObject other, Char.Resource resource);

        #endregion
    }
}
