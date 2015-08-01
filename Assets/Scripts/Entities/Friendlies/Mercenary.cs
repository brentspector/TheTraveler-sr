/*******************************************************************************
 *
 *  File Name: Mercenary.cs
 *
 *  Description: An ally capable of helping fight
 *
 *******************************************************************************/
using GSP.Entities.Interfaces;
using GSP.Items;
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

        int defencePower;       // The defence of the entity (from armor)
        int attackPower;	    // The attack of the entity (from weapons)

        List<Bonus> bonuses;    // The bonuses picked up (Inventory and Weight mods)
        Armor equippedArmor;    // The piece of armor that is being worn.
        Weapon equippedWeapon;  // The weapon that is being wielded.

        #endregion
        
        // Constructor used to create a Mercenary entity
        public Mercenary(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to Mercenary
			Type = EntityType.Mercenary;

            #region IEquipment Variable Initialisation

            // The entity isn't wearing any armour, wielding any weapon, or has any bonuses
            equippedArmor = null;
            equippedWeapon = null;
            bonuses = new List<Bonus>();
            attackPower = 0;
            defencePower = 0;

            #endregion
		} // end Mercenary

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
            get { return bonuses; }
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
	} // end Mercenary
} // end GSP.Entities.Friendlies
