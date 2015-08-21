/*******************************************************************************
 *
 *  File Name: IInventory.cs
 *
 *  Description: Describes a contract for inventory functionlaity
 *
 *******************************************************************************/
using GSP.Items;
using UnityEngine;
using System.Collections.Generic;

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

        // The list of resources
        List<Resource> Resources { get; }

        // The total weight of the resources
        int TotalWeight { get; }

        // The total worth of the resources
        int TotalWorth { get; }

        // The maximum weight that is able to be carried
        int MaxWeight { get; set; }

        // The maximum inventory space available
        int MaxInventorySpace { get; }

        // The currency currently being held
        int Currency { get; set; }

        #endregion
    } // end IInventory
} // end GSP.Entities.Interfaces
