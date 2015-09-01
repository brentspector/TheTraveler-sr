/*******************************************************************************
 *
 *  File Name: Mimic.cs
 *
 *  Description: An enemy that mimics being a chest
 *
 *******************************************************************************/
using GSP.Char.Enemies;
using UnityEngine;

namespace GSP.Entities.Hostiles
{
    /*******************************************************************************
     *
     * Name: Mimic
     * 
     * Description: The Mimic enemy class.
     * 
     *******************************************************************************/
    public class Mimic : Hostile
	{
        MimicMB script;    // THe script reference for the Mimic enemy.
        
        // Constructor used to create a Mimic entity
        public Mimic(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to Mimic
			Type = EntityType.Mimic;

            // Set the entity's script reference
            script = GameObj.GetComponent<MimicMB>();
		} // end Mimic Constructor

        // Gets the entity's script reference
        public MimicMB Script
        {
            get { return script; }
        } // end Script
    } // end Mimic
} // end GSP.Entities.Hostiles
