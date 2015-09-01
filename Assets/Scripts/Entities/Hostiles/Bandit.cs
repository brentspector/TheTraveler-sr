/*******************************************************************************
 *
 *  File Name: Bandit.cs
 *
 *  Description: An enemy that is a human bandit
 *
 *******************************************************************************/
using GSP.Char.Enemies;
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
        BanditMB script;    // THe script reference for the Bandit enemy.
        
        // Constructor used to create a Bandit entity
        public Bandit(int ID, GameObject gameObject) : base(ID, gameObject)
        {
            // Set the entity's type to Bandit
            Type = EntityType.Bandit;

            // Set the entity's script reference
            script = GameObj.GetComponent<BanditMB>();
        } // end Bandit Constructor

        // Gets the entity's script reference
        public BanditMB Script
        {
            get { return script; }
        } // end Script
    } // end Bandit
} // end GSP.Entities.Hostiles
