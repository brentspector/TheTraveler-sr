/*******************************************************************************
 *
 *  File Name: Enemy.cs
 *
 *  Description: Wrapper for the enemies
 *
 *******************************************************************************/
using GSP.Entities;
using GSP.Entities.Hostiles;
using UnityEngine;

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: Enemy
     * 
     * Description: The base class for the wrapper to the enemy entities.
     * 
     *******************************************************************************/
    public class Enemy<TSubEnemy> : MonoBehaviour where TSubEnemy : Hostile
    {
        TSubEnemy enemy;   // Each Enemy needs its own enemy object


        // Use this for initialisation
        public virtual void Start()
        {
            //
        } // end Start

        // Get the Enemy's reference
        public void GetEnemy(int ID)
        {
            enemy = (TSubEnemy)EntityManager.Instance.GetEntity(ID);
        } // end Start

        // Destroy the game object this script is attached to
        public void DestroyGO()
        {
            Destroy(this.gameObject);
        } // end DestroyGO

        // Gets the enemy's entity
        // This is used to get the entity to do things with the implemented interfaces; just cast
        // back to the proper type first
        public Entity Entity
        {
            get { return enemy; }
        } // end Entity

        #region Wrapper for the enemy class

        // Gets the Enemy's Name
        public string Name
        {
            get { return enemy.Name; }
            set { enemy.Name = value; }
        } // end Name

        // Gets the Enemy's number of allies
        public int NumAllies
        {
            get { return enemy.NumAllies; }
        } // end NumAllies

        #endregion
    } // end Enemy
} // end GSP.Char
