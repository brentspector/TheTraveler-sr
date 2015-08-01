/*******************************************************************************
 *
 *  File Name: Mimic.cs
 *
 *  Description: An enemy that mimics being a chest
 *
 *******************************************************************************/
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
        // Constructor used to create a Mimic entity
        public Mimic(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to Mimic
			Type = EntityType.Mimic;
		} // end Mimic Constructor
    } // end Mimic
} // end GSP.Entities.Hostiles
