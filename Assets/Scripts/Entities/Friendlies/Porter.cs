using UnityEngine;
using System.Collections;
using GSP.Entities.Interfaces;
using GSP.Char;

namespace GSP.Entities.Friendlies
{
	public class Porter : Friendly, IInventory
	{
        #region IInventory Variables

        // Variables will be defined in week 4.

        #endregion
        
        public Porter(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter.
			Type = EntityType.ENT_PORTER;

            #region IInventory Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion
		}

        // The below interfaces will be implemented in Week 4.

        #region IInventory Members

        public ResourceList Resources
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TotalWeight
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TotalSize
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int TotalValue
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int MaxWeight
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int MaxInventorySpace
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public int Currency
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void PickupResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void SellResource(Resource resource, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void SellResources()
        {
            throw new System.NotImplementedException();
        }

        public void TransferCurrency(GameObject other, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void TransferResource(GameObject other, Resource resource)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
