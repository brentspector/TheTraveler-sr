using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	//Items enums
	//NOTE!!
	//SIZE must be the last item in the enum so that anything based
	//on the length of the enum can be used as normal. It is best to
	//add items to the left of SIZE but after the current 2nd to last
	//item in the enum. For instance if the list was {SWORD, MACE, SIZE}
	//you should enter the new item between MACE and SIZE. Create name
	//here and then define it under "SetItem" function
	enum Weapons {SWORD, BROADSWORD, MACE, SPEAR, SIZE};
	enum Armor {PLATEBODY, CHAINMAIL, PLATELEGS, CHAINLEGS, FULLSUIT, SIZE};
	enum Inventory {SACHEL, SIZE};
	enum Weight {RUBBERBOOTS, SIZE};

	public class Items : MonoBehaviour 
	{
		string m_itemName;				//Name of this item
		string m_itemType;				//Type of item (dont forget to make enum of new types
		int m_attackValue;				//Attack value this item adds/removes
		int m_defenceValue;				//Defence value this item adds/removes
		int m_inventoryValue;			//Inventory value this item adds/removes
		int m_weightValue;				//Weight value this item adds/removes
		int m_costValue;				//Cost value of this item

		//Get/Set ItemName
		public string ItemName
		{
			get { return m_itemName; }
			set
			{
				m_itemName = value;
			} //end Set
		} //end ItemName

		//Get/Set ItemType
		public string ItemType
		{
			get { return m_itemType; }
			set
			{
				m_itemType = value;
			} //end Set
		} //end ItemName

		//Get/Set AttackValue
		public int AttackValue
		{
			get{ return m_attackValue; }
			set{ m_attackValue = value; }
		} //end AttackValue

		//Get/Set DefenceValue
		public int DefenceValue
		{
			get{ return m_defenceValue; }
			set{ m_defenceValue = value; }
		} //end DefenceValue

		//Get/Set InventoryValue
		public int InventoryValue
		{
			get{ return m_inventoryValue; }
			set{ m_inventoryValue = value; }
		} //end InventoryValue

		//Get/Set WeightValue
		public int WeightValue
		{
			get{ return m_weightValue; }
			set{ m_weightValue = value; }
		} //end WeightValue

		//Get/Set Cost
		public int CostValue
		{
			get{ return m_costValue; }
			set
			{ 
				m_costValue = value; 
				if(m_costValue < 0)
				{
					m_costValue = 0;
				} //end if
			} //end Set
		}//end CostValue

		// Use this for initialization
		void Start () 
		{
			m_itemName = "NAN";
			m_itemType = "NAN";
			m_attackValue = 0;
			m_defenceValue = 0;
			m_inventoryValue = 0;
			m_weightValue = 0;
			m_costValue = 0;
		} //end Start()
		
		// Update is called once per frame
		void Update () 
		{
			
		} //end Update()

		//Sets item to predetermined types
		public string SetItem(string Item)
		{
			//Weapons
			if (Item == "SWORD") 
			{
				m_itemName = "Sword";
				m_itemType = "Weapon";
				m_attackValue = 5;
				return "attack";
			} //end if
			else if (Item == "BROADSWORD")
			{
				m_itemName = "Broadsword";
				m_itemType = "Weapon";
				m_attackValue = 9;
				return "attack";
			} //end else if
			else if (Item == "MACE")
			{
				m_itemName = "Mace";
				m_itemType = "Weapon";
				m_attackValue = 7;
				return "attack";
			} //end else if
			else if (Item == "SPEAR")
			{
				m_itemName = "Spear";
				m_itemType = "Weapon";
				m_attackValue = 8;
				return "attack";
			} //end else if

			//Armors
			else if (Item == "PLATEBODY") 
			{
				m_itemName = "Platebody";
				m_itemType = "Armor";
				m_defenceValue = 8;
				return "defence";
			} //end else if
			else if (Item == "CHAINMAIL")
			{
				m_itemName = "Chainmail";
				m_itemType = "Armor";
				m_defenceValue = 5;
				return "defence";
			} //end else if
			else if (Item == "PLATELEGS")
			{
				m_itemName = "Platelegs";
				m_itemType = "Armor";
				m_defenceValue = 3;
				return "defence";
			} //end else if
			else if (Item == "CHAINLEGS")
			{
				m_itemName = "Chainlegs";
				m_itemType = "Armor";
				m_defenceValue = 2;
				return "defence";
			} //end else if
			else if (Item == "FULLSUIT")
			{
				m_itemName = "Full Suit";
				m_itemType = "Armor";
				m_defenceValue = 11;
				return "defence";
			} //end else if

			//Inventory modifiers
			else if (Item == "SACHEL")
			{
				m_itemName = "Sachel";
				m_itemType = "Inventory";
				m_inventoryValue = 3;
				return "inventory";
			} //end else if

			//Weight modifiers
			else if (Item == "RUBBERBOOTS")
			{
				m_itemName = "Rubber Boots";
				m_itemType = "Weight";
				m_weightValue = 10;
				return "weight";
			} //end else if

			//Default, string does not match
			else
			{
				return "NAN";
			} //end else DEFAULT
		} //end SetItem(string Item)
	} //end Items class
} //end namespace GSP.Char