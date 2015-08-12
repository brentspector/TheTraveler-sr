using UnityEngine;
using System.Collections;

namespace GSP.Items
{
    public class EmptyItem : Item
    {
        // Creates an empty Item
        public EmptyItem() : base(string.Empty, "Empty", null)
        {
            // Leave empty
        } // end EmptyItem Constructor
    } // end EmptyItem
}  // end GSP.Items
