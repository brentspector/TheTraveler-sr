/*******************************************************************************
 *
 *  File Name: Porter.cs
 *
 *  Description: An ally capable of helping carrying resources
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Entities.Interfaces;
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

        int maxWeight;		    // The maximum weight the entity can hold
        int maxInventory;       // The maximum inventory spaces (max number of spaces an entity can hold)
        int currency; 		    // The amount of currency the entity is holding
        ResourceList resources; // The ResourceList script reference

        #endregion

        DieInput die;   // The reference to the die

        // Constructor used to create a Porter entity
        public Porter(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter
			Type = EntityType.Porter;

            // Get the reference to the die script
            die = GameObject.Find("Die").GetComponent<DieInput>();

            #region IInventory Variable Initialisation

            // The entity's max weight is a random number between 6 and 120
            maxWeight = die.Dice.Roll(1, 20) * 6;
            // The entity's max inventory space is left at the hard-coded default
            maxInventory = 20;

            // The entity starts with no currency
            currency = 0;

            // Get the ResourceList component reference
            resources = GameObj.GetComponent<ResourceList>();

            #endregion
		}

        #region IInventory Members

        // Picks up a resource for an entity adding it to their ResourceList
        public void PickupResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        } // end PickupResource

        // Sells a resource for an entity removing it from their ResourceList
        public void SellResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        } // end SellResource

        // Sells all resources for an entity clearing their ResourceList
        public void SellResources()
        {
            throw new System.NotImplementedException();
        } // end SellResources

        // Transfers currency from the entity to another entity
        public void TransferCurrency(GameObject other, int amount)
        {
            throw new System.NotImplementedException();
        } // end TransferCurrency

        // Transfers a resource from the entity to another entity
        public void TransferResource(GameObject other, Resource resource)
        {
            throw new System.NotImplementedException();
        } // end TransferResource

        // Gets and Sets the list of resources of the entity
        public ResourceList Resources
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Resources

        // Gets and Sets the TotalWeight of the entity
        public int TotalWeight
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end TotalWeight

        // Gets and Sets the TotalSize of the entity
        public int TotalSize
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end TotalSize

        // Gets and Sets the TotalValue of the entity
        public int TotalValue
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end TotalValue

        // Gets and Sets the MaxWeight of the entity
        public int MaxWeight
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end MaxWeight

        // Gets and Sets the MaxInventorySpace of the entity
        public int MaxInventorySpace
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end MaxInventorySpace

        // Gets and Sets the Currency of the entity
        public int Currency
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Currency

        #endregion
    } // end Porter
} // end GSP.Entities.Friendlies
