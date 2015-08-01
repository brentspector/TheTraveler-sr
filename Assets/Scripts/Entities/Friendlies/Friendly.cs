/*******************************************************************************
 *
 *  File Name: Friendly.cs
 *
 *  Description: The base for all allies
 *
 *******************************************************************************/
using GSP.Entities.Interfaces;
using UnityEngine;

namespace GSP.Entities.Friendlies
{
    /*******************************************************************************
     *
     * Name: Friendly
     * 
     * Description: The base class for all friendlies a.k.a allies.
     * 
     *******************************************************************************/
    public abstract class Friendly : Entity, IDamageable
	{
        #region IDamageable Variables

        // Variables will be defined in week 4

        #endregion
        
        // Constructor; Derived classes create an entity object
        public Friendly(int ID, GameObject gameObject) : base(ID, gameObject)
		{
            #region IDamageable Variable Initialisation

            // Variable initialisation will be done in week 4

            #endregion
		} // end Friendly

        // The below interfaces will be implemented in Week 4

        #region IDamageable Members

        // Causes the entity to take damage; this is call by others
        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        } // end TakeDamage

        // Resets the health of the entity
        public void ResetHealth()
        {
            throw new System.NotImplementedException();
        } // end ResetHealth
        
        // Gets and Sets the current health of the entity
        public int Health
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Health

        // Gets and Sets the maximum health of the entity
        public int MaxHealth
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end MaxHealth

        #endregion
	} // end Friendly
} // end GSP.Entities.Friendlies
