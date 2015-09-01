/*******************************************************************************
 *
 *  File Name: MercenaryMB.cs
 *
 *  Description: The Mercenary ally; this can be placed on the scene
 *
 *******************************************************************************/
using GSP.Entities.Friendlies;

namespace GSP.Char.Allies
{
    /*******************************************************************************
     *
     * Name: MercenaryMB
     * 
     * Description: Wrapper for the Mercenary Ally.
     * 
     *******************************************************************************/
    public class MercenaryMB : Ally<Mercenary>
    {
        // Used for initialisation
        public override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Set the name of the ally
            Name = "Mercenary";
        } // end Start
    } // end MercenaryMB
} // end GSP.Char.Allies