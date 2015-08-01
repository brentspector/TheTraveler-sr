/*******************************************************************************
 *
 *  File Name: Item.cs
 *
 *  Description: Logic of a single item
 *
 *******************************************************************************/
//TODO: Damien: Replace with something for a better inventory later.
using UnityEngine;

namespace GSP.Char
{
	//Items enums
	// NOTE!!
	// SIZE must be the last item in the enum so that anything based
	// on the length of the enum can be used as normal. It is best to
	// add items to the left of SIZE but after the current 2nd to last
	// item in the enum. For instance if the list was {SWORD, MACE, SIZE}
	// you should enter the new item between MACE and SIZE. Create name
	// here and then define it under "SetItem" function
	enum Weapons {Sword, Broadsword, Mace, Spear, Size};
	enum Armors {Platebody, Chainmail, Platelegs, Chainlegs, Fullsuit, Size};
	enum Inventory {Sachel, Size};
	enum Weight {Rubberboots, Size};

    /*******************************************************************************
     *
     * Name: Items
     * 
     * Description: Manages a single item.
     * 
     *******************************************************************************/
    public class Item : MonoBehaviour 
	{
        string itemName;	// Name of this item; Due to MonoBevaviour, this is an exception to the standards: repeat class name
        string type;		// Type of item (dont forget to make enum of new types)
        int attackValue;	// Attack value this item adds/removes
        int defenceValue;	// Defence value this item adds/removes
        int inventoryValue; // Inventory value this item adds/removes
        int weightValue;	// Weight value this item adds/removes
        int costValue;		// Cost value of this item

		// Use this for initialization
		void Start () 
		{
			itemName = "NAN";
			type = "NAN";
			attackValue = 0;
			defenceValue = 0;
			inventoryValue = 0;
			weightValue = 0;
			costValue = 0;
		} // end Start
		
		// Update is called once per frame
		void Update () 
		{
			//
		} // end Update

		// Sets item to predetermined types
		public string SetItem(string item)
		{
			// Switch over the items
            switch (item.ToUpper())
            {
                // Weapons
                case "SWORD":
                    {
                        itemName = "Sword";
                        type = "Weapon";
                        attackValue = 5;
                        return "attack";
                    } // end case SWORD
                case "BROADSWORD":
                    {
				        itemName = "Broadsword";
				        type = "Weapon";
				        attackValue = 9;
				        return "attack";
                    } // end case BROADSWORD
                case "MACE":
                    {
				        itemName = "Mace";
				        type = "Weapon";
				        attackValue = 7;
                        return "attack";
                    } // end case MACE
                case "SPEAR":
                    {
                        itemName = "Spear";
                        type = "Weapon";
                        attackValue = 8;
                        return "attack";
                    } // end case SPEAR

                // Armours
                case "PLATEBODY":
                    {
				        itemName = "Platebody";
				        type = "Armors";
				        defenceValue = 8;
				        return "defence";
                    } // end case PLATEBODY
                case "CHAINMAIL":
                    {
				        itemName = "Chainmail";
				        type = "Armors";
				        defenceValue = 5;
				        return "defence";
                    } // end case CHAINMAIL
                case "PLATELEGS":
                    {
				        itemName = "Platelegs";
				        type = "Armors";
				        defenceValue = 3;
				        return "defence";
                    }  // end case PLATELEGS
                case "CHAINLEGS":
                    {
				        itemName = "Chainlegs";
				        type = "Armors";
				        defenceValue = 2;
				        return "defence";
                    } // end case CHAINLEGS
                case "FULLSUIT":
                    {
				        itemName = "Full Suit";
				        type = "Armors";
				        defenceValue = 11;
				        return "defence";
                    } // end case FULLSUIT

                // Inventory modifiers
                case "SACHEL":
                    {
				        itemName = "Sachel";
				        type = "Inventory";
				        inventoryValue = 3;
				        return "inventory";
                    } // end case SACHEL
                
                // Weight modifiers
                case "RUBBERBOOTS":
                    {
				        itemName = "Rubber Boots";
				        type = "Weight";
				        weightValue = 10;
				        return "weight";
                    } // end case RUBBERBOOTS

                // Invalid item
                default:
                    {
                        return "NAN";
                    }
            } // end switch item
		} // end SetItem

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
	} // end Items
} // end GSP.Char