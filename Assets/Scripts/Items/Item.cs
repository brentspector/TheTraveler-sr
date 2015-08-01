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
        string name;    	// Name of this item
        string type;		// Type of item (don't forget to make enum of new types)
        int attackValue;	// Attack value this item adds/removes
        int defenceValue;	// Defence value this item adds/removes
        int inventoryValue; // Inventory value this item adds/removes
        int weightValue;	// Weight value this item adds/removes
        int costValue;		// Cost value of this item

        // Dervived classes use this to create an item
        public Item(string itemName, string itemType, int attack, int defence, int space, int weight, int cost)
        {
            // Initialise the item to the given parameters
            name = itemName;
            type = itemType;
            attackValue = attack;
            defenceValue = defence;
            inventoryValue = space;
            weightValue = weight;
            costValue = cost;
        } // end Item

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

        // Gets and Protected Sets the Item's AttackValue
        public int AttackValue
        {
            get { return attackValue; }
            protected set { attackValue = value; }
        } // end AttackValue

        // Gets and Protected Sets the Item's DefenceValue
        public int DefenceValue
        {
            get { return defenceValue; }
            protected set { defenceValue = value; }
        } // end DefenceValue

        // Gets and Protected Sets the Item's WeightValue
        public int WeightValue
        {
            get { return weightValue; }
            protected set { weightValue = value; }
        } // end WeightValue

        // Gets and Protected Sets the Item's CostValue
        public int CostValue
        {
            get { return costValue; }
            protected set { costValue = Utility.ZeroClampInt(value); }
        } // end CostValue

        // Gets and Protected Sets the item's InventoryValue
        public int InventoryValue
        {
            get { return inventoryValue; }
            protected set { inventoryValue = Utility.ZeroClampInt(value); }
        } // end InventoryValue
    }
}
