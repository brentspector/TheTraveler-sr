/*******************************************************************************
 *
 *  File Name: BanditMB.cs
 *
 *  Description: The Bandit enemy; this can be placed on the scene
 *
 *******************************************************************************/
using GSP.Entities.Hostiles;

namespace GSP.Char.Enemies
{
    /*******************************************************************************
     *
     * Name: BanditMB
     * 
     * Description: The wrapper for the Bandit Enemy.
     * 
     *******************************************************************************/
    public class BanditMB : Enemy<Bandit>
    {
        // Used for initialisation
        public override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Set the name of the enemy
            Name = "Bandit";
        } // end Start
    } // end BanditMB
} // end GSP.Char.Enemies