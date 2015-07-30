/*******************************************************************************
 *
 *  File Name: Mercenary.cs
 *
 *  Description: An ally capable of helping fight
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Entities.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Entities.Friendlies
{
    /*******************************************************************************
     *
     * Name: Mercenary
     * 
     * Description: The Mercenary ally class. Capable of helping the player fight.
     * 
     *******************************************************************************/
    public class Mercenary : Friendly, IEquipment
	{
        #region IEquipment Variables

        // Variables will be defined in week 4

        #endregion
        
        // Constructor used to create a Mercenary entity
        public Mercenary(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to Mercenary
			Type = EntityType.Mercenary;

            #region IEquipment Variable Initialisation

            // Variable initialisation will be done in week 4

            #endregion
		} // end Mercenary

        // The below interfaces will be implemented in Week 4

        #region IEquipment Members

        // Equips a piece of armour for an entity
        public void EquipArmor(string item)
        {
            throw new System.NotImplementedException();
        } // end EquipArmor

        // Unequips a piece of armour for an entity
        public void UnequipArmor(string item)
        {
            throw new System.NotImplementedException();
        } // end UnequipArmor

        // Equips a weapon for an entity
        public void EquipWeapon(string item)
        {
            throw new System.NotImplementedException();
        } // end EquipWeapon

        // Unequips a weapon for the entity
        public void UnequipWeapon(string item)
        {
            throw new System.NotImplementedException();
        } // end UnequipWeapon
        
        // Gets and Sets the amount of defense the entity has
        public int DefencePower
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end DefencePower

        // Gets and Sets the EquippedArmor of the entity
        public EquippedArmor EquippedArmor
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end EquippedArmor

        // Gets and Sets the bonuses the entity has
        public List<GameObject> Bonuses
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end Bonuses
        
        // Gets and Sets the how hard the the entity hits
        public int AttackPower
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end AttackPower

        // Gets and Sets the EquppedWeapon of the entity
        public EquippedWeapon EquippedWeapon
        {
            get
            {
                throw new System.NotImplementedException();
            } // end get
            set
            {
                throw new System.NotImplementedException();
            } // end set
        } // end EquippedWeapon

        #endregion
	} // end Mercenary
} // end GSP.Entities.Friendlies
