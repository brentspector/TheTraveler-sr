/*******************************************************************************
 *
 *  File Name: Ally.cs
 *
 *  Description: Wrapper for the allies
 *
 *******************************************************************************/
using GSP.Entities;
using GSP.Entities.Friendlies;
using UnityEngine;

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: Ally
     * 
     * Description: The base class for the wrapper to the ally entities.
     * 
     *******************************************************************************/
    // This class name won't have a number later
    public class Ally<TSubAlly> : MonoBehaviour where TSubAlly : Friendly
    {
        TSubAlly ally;   // Each Ally needs its own ally object

        // Use this for initialisation
        public virtual void Start()
        {
            //
        } // end Start

        // Get the ally's reference
        public void GetAlly(int ID)
        {
            ally = (TSubAlly)EntityManager.Instance.GetEntity(ID);
        } // end GetAlly

        // Destroy the game object this script is attached to
        public void DestroyGO()
        {
            Destroy(this.gameObject);
        } // end DestroyGO

        // Gets the ally's entity
        // This is used to get the entity to do things with the implemented interfaces; just cast
        // back to the proper type first
        public Entity Entity
        {
            get { return ally; }
        } // end Entity

        #region Wrapper for the ally class

        // Gets the ally's Name
        public string Name
        {
            get { return ally.Name; }
            set { ally.Name = value; }
        } // end Name

        #endregion
    } // end Ally
} // end GSP.Char
