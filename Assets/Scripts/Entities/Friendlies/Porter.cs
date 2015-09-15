/*******************************************************************************
 *
 *  File Name: Porter.cs
 *
 *  Description: An ally capable of helping carrying resources
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Char.Allies;
using GSP.Core;
using GSP.Entities.Interfaces;
using GSP.Entities.Neutrals;
using GSP.Items;
using GSP.Items.Inventories;
using GSP.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Entities.Friendlies
{
    /*******************************************************************************
     *
     * Name: Porter
     * 
     * Description: The Porter ally class. Capable of helping the player carrying
     *              their resources.
     * 
     *******************************************************************************/
    public class Porter : Friendly, IInventory
	{
        #region IInventory Variables

        int maxWeight;		                // The maximum weight the entity can hold
        int currency; 		                // The amount of currency the entity is holding
        List<Resource> resources;           // The list of resources
        AllyInventory inventory;            // The inventory of the ally
        ResourceUtility resourceUtility;    // The resource utility functions

        #endregion

        PorterMB script;    // The script reference for the Mimic enemy.
        Die die;      		// The die object

        // Constructor used to create a Porter entity
        public Porter(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter
			Type = EntityType.Porter;

            // Also set the entity's friendly type
            FriendlyType = Entities.FriendlyType.Porter;

            // Get the reference to the die script
            die = new Die();

            // Set the entity's script reference
            script = GameObj.GetComponent<PorterMB>();

            #region IInventory Variable Initialisation

            // The entity's max weight is a random number between 25 and 120
            maxWeight = Utility.ClampInt(die.Roll(1, 20) * 6 + 25, 25, 150);

            // The entity starts with no currency
            currency = 0;

            // Get the ResourceList component reference
            resources = new List<Resource>();

            // Get the inventory script
            inventory = GameObject.Find("Canvas").transform.Find("AllyInventory").GetComponent<AllyInventory>();

            // Create a new ResourceUltity object
            resourceUtility = new ResourceUtility();

            #endregion
		} // end Porter

        // Gets the entity's script reference
        public PorterMB Script
        {
            get { return script; }
        } // end Script

        #region IInventory Members

        // Picks up a resource for an entity adding it to their ResourceList
        public bool PickupResource(Items.Resource resource, int amount, bool isFromMap = true)
        {
            // Check if picking up this resource will put the entity overweight
            if ((TotalWeight + resource.Weight) * amount <= MaxWeight)
            {
                // Attempt to add the resource to the inventory
                if (inventory.AddItem(2, AllyNumber, resource.Id, SlotType.Ally))
                {
                    // Update the inventory's stats; this is hard coded for a single ally
                    inventory.SetStats((Merchant)GameMaster.Instance.GetPlayerScript(AllyNumber - 7).Entity);

                    // Return success
                    return true;
                } // end if
                else
                {
                    Debug.Log("Pickup failed. Max inventory capacity reached.");

                    // Return failure
                    return false;
                } // end else
            } // end if
            else
            {
                Debug.Log("Pickup failed. Max inventory weight reached.");

                // Return failure
                return false;
            } // end else
        } // end PickupResource

        // Sells a resource for an entity removing it from their ResourceList
        public void SellResource(Resource resource, int amount)
        {
            // A temporary list to hold the resources
            List<Resource> tmpResources = new List<Resource>();

            // The counter for the for loop below
            int count = 0;

            // Get all the resources of the given resource's type
            tmpResources = ResourceUtility.GetResourcesByType(resource.ResourceType, AllyNumber, false);

            // Check if the returned number of resources is fewer than amount
            if (tmpResources.Count < amount)
            {
                // Set the counter to the number of resources found
                count = tmpResources.Count;
            } // end if
            else
            {
                // Set the counter to amount
                count = amount;
            } // end else

            // Loop over the list until we reach count
            for (int index = 0; index < count; index++)
            {
                // Credit the entity for the resource
                currency += tmpResources[index].Worth;

                // Remove the resource from the inventory
                ResourceUtility.RemoveResource(tmpResources[index], AllyNumber, false);
            } // end for
        } // end SellResource

        // Sells all resources for an entity clearing their ResourceList
        public void SellResources()
        {
            // Credit the entity for the resources they are holding
            currency += TotalWorth;

            // Remove all the resources now
            ResourceUtility.RemoveResources(AllyNumber, false);
        } // end SellResources

        // Transfers currency from the entity to another entity
        public void TransferCurrency<TInventoryEntity>(TInventoryEntity other, int amount) where TInventoryEntity : IInventory
        {
            // The clamped amount between zero and the entity's currency amount
            int transferAmount = Utility.ClampInt(amount, 0, currency);

            // Add the amount of currency to the other Character
            other.Currency += transferAmount;

            // Subtract the amount of currency from the Character this is attached to
            currency -= transferAmount;
        } // end TransferCurrency

        // Transfers a resource from the entity to another entity
        public bool TransferResource<TInventoryEntity>(TInventoryEntity other, Items.Resource resource) where TInventoryEntity : IInventory
        {
            // Check if the resource object exists
            if (resource == null)
            {
                // The resource object is invalid so return failure
                return false;
            } // end if

            // Have the other entity pickup the resource and test if it's a success
            if (other.PickupResource(resource, 1, false))
            {
                // The pickup succeeded so remove the resource from the entity's inventory
                ResourceUtility.RemoveResource(resource, AllyNumber, false);

                // Return success
                return true;
            } // end if
            else
            {
                // The pickup failed for the other entity so return failure
                Debug.Log("Transfer failed.");
                return false;
            }
        } // end TransferResource

        // Gets the ResourceUtility object
        public ResourceUtility ResourceUtility
        {
            get { return resourceUtility; }
        } // end ResourceUtility

        // Gets the list of resources of the entity
        public List<Resource> Resources
        {
            get
            {
                // Get the list of resources in the player's inventory
                resources = ResourceUtility.GetResources(AllyNumber, false);

                // Create a temporary list based on the list of resources
                List<Resource> tempList = resources;

                // Return the temporary list
                return tempList;
            } // end get
        } // end Resources

        // Gets the TotalWeight of the entity's resources
        public int TotalWeight
        {
            get
            {
                // The total weight of all the resources in the player's inventory
                int totalWeight = 0;

                // Get all the resources
                List<Resource> allResources = Resources;

                // Make sure there are resources
                if (allResources.Count > 0)
                {
                    // Get the total weight
                    foreach (Resource resource in allResources)
                    {
                        totalWeight += resource.Weight;
                    } // end foreach
                } // end if
                // Return the total weight
                return totalWeight;
            } // end get
        } // end TotalWeight

        // Gets the TotalValue of the entity's resources
        public int TotalWorth
        {
            get
            {
                // The total weight of all the resources in the player's inventory
                int totalWorth = 0;

                // Get all the resources
                List<Resource> allResources = Resources;

                // Make sure there are resources
                if (allResources.Count > 0)
                {
                    // Get the total worth
                    foreach (Resource resource in allResources)
                    {
                        totalWorth += resource.Worth;
                    } // end foreach
                } // end if

                // Return the total worth
                return totalWorth;
            } // end get
        } // end TotalWorth

        // Gets and Sets the MaxWeight of the entity
        public int MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = Utility.ZeroClampInt(value); }
        } // end MaxWeight

        // Gets the MaxInventorySpace of the entity
        public int MaxInventorySpace
        {
            get { return inventory.MaxSpace; }
        } // end MaxInventorySpace

        // Gets and Sets the Currency of the entity
        public int Currency
        {
            get { return currency; }
            set { currency = Utility.ZeroClampInt(value); }
        } // end Currency

        #endregion
    } // end Porter
} // end GSP.Entities.Friendlies
