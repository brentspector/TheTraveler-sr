/*******************************************************************************
 *
 *  File Name: Item.cs
 *
 *  Description: The base for all item types
 *
 *******************************************************************************/

namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: Item
     * 
     * Description: The base class and base logic for all item types.
     * 
     *******************************************************************************/
    public abstract class Item
    {
        static int nextId; // The next ID used for giving each entity a unique ID upon their creation
        
        int id;             // The ID of the item
        string name;    	// Name of this item
        string type;		// Type of item (don't forget to make enum of new types)

        // Dervived classes use this to create an item
        public Item(string itemName, string itemType)
        {
            // Initialise the item to the given parameters
            id = nextId;
            name = itemName;
            type = itemType;

            // Increment the ID for the next item
            nextId++;
        } // end Item

        // Gets the Item's Id
        public int Id
        {
            get { return id; }
        }

        // Gets and Sets the Item's Name
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        } //end Name

        //Gets the Item's Type
        public string Type
        {
            get { return type; }
        } // end Type
    }
}
