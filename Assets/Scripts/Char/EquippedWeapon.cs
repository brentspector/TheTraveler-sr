/*******************************************************************************
 *
 *  File Name: EquippedWeapon.cs
 *
 *  Description: Old construct for managing the the equipped weapon of the 
 *               player
 *
 *******************************************************************************/
//TODO: Damien: Replace with something for a better inventory later.
using UnityEngine;

namespace GSP.Char
{
    /*******************************************************************************
     *
     * Name: EquippedWeapon
     * 
     * Description: Manages an equipped weapon item.
     * 
     *******************************************************************************/
    public class EquippedWeapon : MonoBehaviour
	{
        string itemName;	// Name of this item; Due to MonoBevaviour, this is an exception to the standards: repeat class name
        string type;		// Type of item (dont forget to make enum of new types)
        int attackValue;	// Attack value this item adds/removes
        int defenceValue;	// Defence value this item adds/removes
        int inventoryValue; // Inventory value this item adds/removes
        int weightValue;	// Weight value this item adds/removes
        int costValue;		// Cost value of this item

		// Use this for initialisation
		void Start()
		{
			// Initialise the variables; instead of listing them twice, just call reset
            ResetWeapon();
		} // end Start
		
		// Reset the variables to their initial state
		// This is used when unequipping it
		public void ResetWeapon()
		{
			// Reset the variables.
			itemName = "NAN";
			type = "NAN";
			attackValue = 0;
			defenceValue = 0;
			inventoryValue = 0;
			weightValue = 0;
			costValue = 0;
		} // end ResetWeapon

        // Gets and Sets the item's Name
        public string Name
        {
            get { return itemName; }
            set { itemName = value; }
        } //end Name

        //Gets and Sets the item's Type
        public string Type
        {
            get { return type; }
            set { type = value; }
        } // end Type

        // Gets and Sets the item's AttackValue
        public int AttackValue
        {
            get { return attackValue; }
            set { attackValue = value; }
        } // end AttackValue

        // Gets and Sets the item's DefenceValue
        public int DefenceValue
        {
            get { return defenceValue; }
            set { defenceValue = value; }
        } // end DefenceValue

        // Gets and Sets the item's WeightValue
        public int WeightValue
        {
            get { return weightValue; }
            set { weightValue = value; }
        } // end WeightValue

        // Gets and Sets the item's CostValue
        public int CostValue
        {
            get { return costValue; }
            set
            {
                // Set to the value
                costValue = value;

                // Check if the CostValue is less than zero
                if (costValue < 0)
                {
                    // Clamp to zero
                    costValue = 0;
                } // end if
            } // end set
        } // end CostValue

        // Gets and Sets the item's InventoryValue
        public int InventoryValue
        {
            get { return inventoryValue; }
            set { inventoryValue = value; }
        } // end InventoryValue
	} // end EquippedWeapon
} // end GSP.Char
