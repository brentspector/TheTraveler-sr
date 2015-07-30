/*******************************************************************************
 *
 *  File Name: MimicMB.cs
 *
 *  Description: The Mimic enemy; this can be placed on the scene
 *
 *******************************************************************************/
using GSP.Entities.Hostiles;

namespace GSP.Char.Enemies
{
    /*******************************************************************************
     *
     * Name: MimicMB
     * 
     * Description: The wrapper for the Mimic Enemy.
     * 
     *******************************************************************************/
    public class MimicMB : Enemy<Mimic>
    {
        // Used for initialisation
        public override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Set the name of the enemy
            Name = "Mimic";
        } // end Start
    } // end MimicMB
} // end GSP.Char.Enemies