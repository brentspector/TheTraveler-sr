/*******************************************************************************
 *
 *  File Name: Bandit.cs
 *
 *  Description: An enemy that is a human bandit
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Entities.Hostiles
{
    /*******************************************************************************
     *
     * Name: Bandit
     * 
     * Description: The Bandit enemy class.
     * 
     *******************************************************************************/
    public class Bandit : Hostile
    {
        // Constructor used to create a Bandit entity
        public Bandit(int ID, GameObject gameObject) : base(ID, gameObject)
        {
            // Set the entity's type to Bandit
            Type = EntityType.Bandit;
        } // end Mimic Constructor
    } // end Bandit
} // end GSP.Entities.Hostiles
