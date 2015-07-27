using UnityEngine;
using System.Collections;
using GSP.Char;
using System.Collections.Generic;
using GSP.Entities.Interfaces;

namespace GSP.Entities.Friendlies
{
	public class Mercenary : Friendly, IEquipment
	{
        #region IEquipment Variables

        // Variables will be defined in week 4.

        #endregion
        
        public Mercenary(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter.
			Type = EntityType.ENT_MERCENARY;

            #region IEquipment Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion
		}

        #region IEquipment Members

        public int DefencePower
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

        public EquippedArmor EquippedArmour
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

        public List<GameObject> Bonuses
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

        public void EquipArmour(string item)
        {
            throw new System.NotImplementedException();
        }

        public void UnequipArmour(string item)
        {
            throw new System.NotImplementedException();
        }

        public int AttackPower
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

        public EquippedWeapon EquippedWeapon
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

        public void EquipWeapon(string item)
        {
            throw new System.NotImplementedException();
        }

        public void UnequipWeapon(string item)
        {
            throw new System.NotImplementedException();
        }

        #endregion
	}
}
