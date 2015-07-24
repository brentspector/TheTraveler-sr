using UnityEngine;
using System.Collections;

namespace GSP.Entities.Hostiles
{
	public class Hostile : Entity
	{
        #region IEquipment Variables

        // Variables will be defined in week 4.

        #endregion

        #region IDamageable Variables

        // Variables will be defined in week 4.

        #endregion
        
        public Hostile(int ID, GameObject gameObject) : base(ID, gameObject)
		{
            #region IEquipment Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion

            #region IDamageable Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion
		}

        // The below interfaces will be implemented in Week 4.

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

        public Char.EquippedArmor EquippedArmour
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

        public System.Collections.Generic.List<GameObject> Bonuses
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

        public Char.EquippedWeapon EquippedWeapon
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

        #region IDamageable Members

        public int Health
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

        public int MaxHealth
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

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void ResetHealth()
        {
            throw new System.NotImplementedException();
        }

        #endregion
	}
}
