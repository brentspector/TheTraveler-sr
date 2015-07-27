using UnityEngine;
using System.Collections;
using GSP.Entities.Interfaces;

namespace GSP.Entities.Friendlies
{
	public class Friendly : Entity, IDamageable
	{
        #region IDamageable Variables

        // Variables will be defined in week 4.

        #endregion
        
        public Friendly(int ID, GameObject gameObject) : base(ID, gameObject)
		{
            #region IDamageable Variable Initialisation

            // Variable initialisation will be done in week 4.

            #endregion
		}

        // The below interfaces will be implemented in Week 4.

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
