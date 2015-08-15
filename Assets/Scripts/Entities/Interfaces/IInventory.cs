/*******************************************************************************
 *
 *  File Name: IInventory.cs
 *
 *  Description: Describes a contract for inventory functionlaity
 *
 *******************************************************************************/
using GSP.Items;
using UnityEngine;

namespace GSP.Entities.Interfaces
{
    /*******************************************************************************
     *
     * Name: IInventory
     * 
     * Description: Supplies the functionality for inventory operations. This
     *              includes resources and currency.
     * 
     *******************************************************************************/
    public interface IInventory
    {
        #region Functions

        // Attempt to pickup a resource
        bool PickupResource(Resource resource, int amount, bool isFromMap = true);

        // Sells a given resource and amount
        void SellResource(Resource resource, int amount);

        // Sells all the resources being carried
        void SellResources();

        // Transfer currency from one to another
        void TransferCurrency<TInventoryEntity>(TInventoryEntity other, int amount) where TInventoryEntity : IInventory;

        // Transfer a resource from one to another
        bool TransferResource<TInventoryEntity>(TInventoryEntity other, Resource resource) where TInventoryEntity : IInventory;

        #endregion

        #region Properties

        // The resource list
        Char.ResourceList Resources { get; }

        // The total weight of the resources
        int TotalWeight { get; }

        // The total size of the resources
        int TotalSize { get; }

        // The total value of the resources
        int TotalValue { get; }

        // The maximum weight that is able to be carried
        int MaxWeight { get; set; }

        // The maximum inventory space available
        int MaxInventorySpace { get; set; }

        // The currency currently being held
        int Currency { get; set; }

        #endregion
    } // end IInventory
} // end GSP.Entities.Interfaces
