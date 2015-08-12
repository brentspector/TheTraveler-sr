/*******************************************************************************
 *
 *  File Name: Equipment.cs
 *
 *  Description: The base for all equipement types; derives from Item.
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: Equipment
     * 
     * Description: The base class/base logic for all equipment. Derives from Item.
     * 
     *******************************************************************************/
    public abstract class Equipment : Item
    {
        int attackValue;	// Attack value this item adds/removes
        int defenceValue;	// Defence value this item adds/removes
        int inventoryValue; // Inventory value this item adds/removes
        int weightValue;	// Weight value this item adds/removes
        int costValue;		// Cost value of this item

        // Create a piece of equipment
        public Equipment(string itemName, string itemType, Sprite itemIcon, int attack, int defence, int space, int weight, int cost) : 
            base(itemName, itemType.ToString(), itemIcon)
        {
            // Initialise the item to the given parameters
            attackValue = attack;
            defenceValue = defence;
            inventoryValue = space;
            weightValue = weight;
            costValue = cost;
        } // end Equipment

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
    } // end Equipment
} // end GSP.Items
