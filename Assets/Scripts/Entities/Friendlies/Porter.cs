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

        // Variables will be defined in week 4

        #endregion

        // Constructor used to create a Porter entity
        public Porter(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter
			Type = EntityType.Porter;

            #region IInventory Variable Initialisation

            // Variable initialisation will be done in week 4

            #endregion
		}

        // The below interfaces will be implemented in Week 4

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
