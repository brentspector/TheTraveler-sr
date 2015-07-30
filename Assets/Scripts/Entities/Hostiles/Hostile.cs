/*******************************************************************************
 *
 *  File Name: Hostile.cs
 *
 *  Description: The base for all enemies
 *
 *******************************************************************************/
using GSP.Char;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Entities.Hostiles
{
    /*******************************************************************************
     *
     * Name: Hostile
     * 
     * Description: The base class for all hostiles a.k.a enemies.
     * 
     *******************************************************************************/
    public abstract class Hostile : Entity
	{
        #region IEquipment Variables

        // Variables will be defined in week 4

        #endregion

        #region IDamageable Variables

        // Variables will be defined in week 4

        #endregion

        // Constructor; Derived classes create an entity object
        public Hostile(int ID, GameObject gameObject) : base(ID, gameObject)
		{
            #region IEquipment Variable Initialisation

            // Variable initialisation will be done in week 4

            #endregion

            #region IDamageable Variable Initialisation

            // Variable initialisation will be done in week 4

            #endregion
		} // end Hostile

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
	} // end Hostile
} // end GSP.Entities.Hostiles
