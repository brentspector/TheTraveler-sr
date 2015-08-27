/*******************************************************************************
 *
 *  File Name: Porter.cs
 *
 *  Description: An ally capable of helping carrying resources
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Char.Allies;
using GSP.Entities.Interfaces;
using GSP.Items;
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

        int maxWeight;		        // The maximum weight the entity can hold
        int currency; 		        // The amount of currency the entity is holding
        List<Resource> resources;   // The list of resources

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

            // The entity's max weight is a random number between 6 and 120
            maxWeight = die.Roll(1, 20) * 6;

            // The entity starts with no currency
            currency = 0;

            // Get the ResourceList component reference
            resources = new List<Resource>();

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
            //TODO: Damien: Not implemented for allies yet
            return false;
        } // end PickupResource

        // Sells a resource for an entity removing it from their ResourceList
        public void SellResource(Resource resource, int amount)
        {
            //TODO: Damien: Not implemented for allies yet
        } // end SellResource

        // Sells all resources for an entity clearing their ResourceList
        public void SellResources()
        {
            //TODO: Damien: Not implemented for allies yet
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
            //TODO: Damien: Not implemented for allies yet
            return false;
        } // end TransferResource

        // Gets the list of resources of the entity
        public List<Resource> Resources
        {
            //TODO: Damien: Not implemented for allies yet
            get { return resources; }
        } // end Resources

        // Gets the TotalWeight of the entity's resources
        public int TotalWeight
        {
            //TODO: Damien: Not implemented for allies yet
            get { return 0; }
        } // end TotalWeight

        // Gets the TotalWorth of the entity's resources
        public int TotalWorth
        {
            //TODO: Damien: Not implemented for allies yet
            get { return 0; }
        } // end TotalWorth

        // Gets and Sets the MaxWeight of the entity
        public int MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = Utility.ZeroClampInt(value); }
        } // end MaxWeight

        // Gets and Sets the MaxInventorySpace of the entity
        public int MaxInventorySpace
        {
            //TODO: Damien: Not implemented for allies yet
            get { return 0; }
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
