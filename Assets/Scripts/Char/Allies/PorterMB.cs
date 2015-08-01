/*******************************************************************************
 *
 *  File Name: PorterMB.cs
 *
 *  Description: The Porter ally; this can be placed on the scene
 *
 *******************************************************************************/
using GSP.Entities.Friendlies;

namespace GSP.Char.Allies
{
    /*******************************************************************************
     *
     * Name: PorterMB
     * 
     * Description: Wrapper for the Porter Ally.
     * 
     *******************************************************************************/
    public class PorterMB : Ally2<Porter>
    {
        // Used for initialisation
        public override void Start()
        {
            // Call the parent's Start() first
            base.Start();

            // Set the name of the ally
            Name = "Porter";
        } // end Start
    } // end PorterMB
} // end GSP.Char.Allies