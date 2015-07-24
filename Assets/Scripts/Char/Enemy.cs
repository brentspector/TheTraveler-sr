using UnityEngine;
using System.Collections;
using GSP.Entities.Hostiles;
using GSP.Entities;

namespace GSP.Char
{
    public class Enemy<T> : MonoBehaviour where T : Hostile
    {
        T m_enemy;   // Each enemy needs its own enemy object.

        // Use this for initialisation
        public virtual void Start()
        {
            //
        }

        public void GetEnemy(int ID)
        {
            // Get the enemy's reference.
            m_enemy = (T)EntityManager.Instance.GetEntity(ID);
        }

        // Destroy the game object this script is attached to.
        public void DestroyGO()
        {
            Destroy(this.gameObject);
        }

        #region Wrapper for the enemy class

        // Gets the enemy's name.
        public string Name
        {
            get
            {
                return m_enemy.Name;
            }
            set
            {
                m_enemy.Name = value;
            }
        }

        // Gets the enemy's number of allies.
        public int NumAllies
        {
            get
            {
                return 0;//m_enemy.NumAllies;
            }
        }

        #endregion
    }
}
