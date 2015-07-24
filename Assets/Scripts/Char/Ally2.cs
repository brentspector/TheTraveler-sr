using UnityEngine;
using System.Collections;
using GSP.Entities.Friendlies;
using GSP.Entities;

namespace GSP.Char
{
    public class Ally2<T> : MonoBehaviour where T : Friendly
    {
        T m_ally;   // Each ally needs its own ally object.

        // Use this for initialisation
        public virtual void Start()
        {
            //
        }

        public void GetAlly(int ID)
        {
            // Get the enemy's reference.
            m_ally = (T)EntityManager.Instance.GetEntity(ID);
        }

        // Destroy the game object this script is attached to.
        public void DestroyGO()
        {
            Destroy(this.gameObject);
        }

        #region Wrapper for the ally class

        // Gets the ally's name.
        public string Name
        {
            get
            {
                return m_ally.Name;
            }
            set
            {
                m_ally.Name = value;
            }
        }

        #endregion
    }
}
