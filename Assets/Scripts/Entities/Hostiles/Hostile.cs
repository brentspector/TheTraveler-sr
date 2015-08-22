/*******************************************************************************
 *
 *  File Name: Hostile.cs
 *
 *  Description: The base for all enemies
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Items;
using GSP.Entities.Interfaces;
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
    public abstract class Hostile : Entity, IDamageable, IEquipment
	{
        #region IEquipment Variables

        int defencePower;       // The defence of the entity (from armor)
        int attackPower;	    // The attack of the entity (from weapons)

        List<Bonus> bonuses;    // The bonuses picked up (Inventory and Weight mods)
        Armor equippedArmor;    // The piece of armor that is being worn.
        Weapon equippedWeapon;  // The weapon that is being wielded.

        #endregion

        #region IDamageable Variables

        int health;     // The current health the entity has
        int maxHealth;  // THe maximum health the entity has
        bool isDead;    // Whether the entity is dead

        #endregion

        AllyList allyScript;    // The ally script object

        // Constructor; Derived classes create an entity object
        public Hostile(int ID, GameObject gameObject) : base(ID, gameObject)
		{
            // Get the GameObject's ally script
            allyScript = GameObj.GetComponent<AllyList>();
            
            #region IEquipment Variable Initialisation

            // The entity isn't wearing any armour, wielding any weapon, or has any bonuses
            equippedArmor = null;
            equippedWeapon = null;
            bonuses = new List<Bonus>();
            attackPower = 0;
            defencePower = 0;

            #endregion

            #region IDamageable Variable Initialisation

            // The default hard coded values for now.
            health = 50;
            maxHealth = 50;
            isDead = false;

            #endregion
		} // end Hostile

        // Gets the number of allies the Hostile has
        public int NumAllies
        {
            get { return allyScript.NumAllies; }
        } // end NumAllies

        #region IEquipment Members

        // Equips a piece of armour for an entity
        public void EquipArmor(Armor armor)
        {
            // Check if the merchant is wearing armour
            if (equippedArmor == null)
            {
                // The merchant isn't wearing armor so just equip the given armour
                defencePower += armor.DefenceValue;
                equippedArmor = armor;
            } // end if
            else
            {
                // The merchant is already wearing armour so unequip it first
                UnequipArmor(equippedArmor);

                // Now equip the given armour
                defencePower += armor.DefenceValue;
                equippedArmor = armor;
            } // end else
        } // end EquipArmor

        // Unequips a piece of armour for an entity
        public void UnequipArmor(Armor armor)
        {
            // Only unequip the armour if the merchant is wearing any
            if (equippedArmor != null)
            {
                // Unequip the given armour
                defencePower -= armor.DefenceValue;
                equippedArmor = null;
            } // end if
        } // end UnequipArmor

        // Note: The Hostile class doesn't implement IInventory so the bonuses are only added to the list
        // Equips a bonus item
        public void EquipBonus(Bonus bonus)
        {
            // Add the bonus to the list
            bonuses.Add(bonus);
        } // end EquipBonus

        // Note: The Hostile class doesn't implement IInventory so the bonuses are only added to the list
        // Unequips a bonus item
        public void UnequipBonus(Bonus bonus)
        {
            // Remove the bonus from the list
            bonuses.Remove(bonus);
        } // end UnequipBonus

        // Equips a weapon for an entity
        public void EquipWeapon(Weapon weapon)
        {
            // Check if the merchant is wielding a weapon
            if (equippedArmor == null)
            {
                // The merchant isn't wielding a weapon so just equip the given weapon
                attackPower += weapon.AttackValue;
                equippedWeapon = weapon;
            } // end if
            else
            {
                // The merchant is already wielding a weapon so unequip it first
                UnequipWeapon(equippedWeapon);

                // Now wield the given weapon
                attackPower += weapon.AttackValue;
                equippedWeapon = weapon;
            } // end else
        } // end EquipWeapon

        // Unequips a weapon for the entity
        public void UnequipWeapon(Weapon weapon)
        {
            // Only unequip the weapon if the merchant is wielding any
            if (equippedWeapon != null)
            {
                // Unequip the given weapon
                attackPower -= weapon.AttackValue;
                equippedWeapon = null;
            } // end if
        } // end UnequipWeapon

        // Gets and Sets the amount of defense the entity has
        public int DefencePower
        {
            get { return defencePower; }
            set { defencePower = Utility.ZeroClampInt(value); }
        } // end DefencePower

        // Gets the EquippedArmor of the entity
        public Armor EquippedArmor
        {
            get { return equippedArmor; }
        } // end EquippedArmor

        // Gets the bonuses the entity has
        public List<Bonus> Bonuses
        {
            get
            {
                // Get a temp list
                var tmp = new List<Bonus>(bonuses);

                // Return the temp list
                return tmp;
            } // end get
        } // end Bonuses

        // Gets and Sets the how hard the the entity hits
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = Utility.ZeroClampInt(value); }
        } // end AttackPower

        // Gets the EquippedWeapon of the entity
        public Weapon EquippedWeapon
        {
            get { return equippedWeapon; }
        } // end EquippedWeapon

        #endregion

        #region IDamageable Members

        // Causes the entity to take damage; this is call by others
        public void TakeDamage(int damage)
        {
            // Only allow damage if the entity isn't dead
            if (!IsDead)
            {
                // Dish out the damage
                health -= damage;

                // Check if the entity is dead
                if (health == 0)
                {
                    // The entity is dead
                    isDead = true;
                } // end if health == 0
            } // end if
        } // end TakeDamage

        // Resets the health of the entity
        public void ResetHealth()
        {
            health = maxHealth;
            isDead = false;
        } // end ResetHealth

        // Gets the current health of the entity
        public int Health
        {
            get { return Utility.ZeroClampInt(health); }
        } // end Health

        // Gets and Sets the maximum health of the entity
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = Utility.ZeroClampInt(value); }
        } // end MaxHealth

        // Gets whether the eneity is dead
        public bool IsDead
        {
            get { return isDead; }
        } // end IsDead

        #endregion
	} // end Hostile
} // end GSP.Entities.Hostiles
