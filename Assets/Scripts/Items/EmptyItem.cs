/*******************************************************************************
 *
 *  File Name: EmptyItem.cs
 *
 *  Description: Logic for an empty item. Used in conjunction with the Inventory
 *               system.
 *
 *******************************************************************************/

namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: EmptyItem
     * 
     * Description: An empty item for use with the empty slots of the Inventory
     *              system.
     * 
     *******************************************************************************/
    public class EmptyItem : Item
    {
        // Creates an empty Item
        public EmptyItem() : base(string.Empty, "Empty", null)
        {
            // Leave empty
        } // end EmptyItem Constructor
    } // end EmptyItem
}  // end GSP.Items
