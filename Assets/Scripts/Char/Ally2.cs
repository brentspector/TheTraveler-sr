/*******************************************************************************
 *
 *  File Name: Ally2.cs
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
     * Name: Ally2
     * 
     * Description: The base class for the wrapper to the ally entities.
     * 
     *******************************************************************************/
    // This class name won't have a number later
    public class Ally2<TSubAlly> : MonoBehaviour where TSubAlly : Friendly
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

        #region Wrapper for the ally class

        // Gets the ally's Name.
        public string Name
        {
            get { return ally.Name; }
            set { ally.Name = value; }
        } // end Name

        #endregion
    } // end Ally2
} // end GSP.Char
