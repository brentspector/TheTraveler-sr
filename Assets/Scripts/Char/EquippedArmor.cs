using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	public class EquippedArmor : MonoBehaviour
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
		
		
		// Use this for initialisation
		void Start()
		{
			// Initialise the variables.
			m_itemName = "NAN";
			m_itemType = "NAN";
			m_attackValue = 0;
			m_defenceValue = 0;
			m_inventoryValue = 0;
			m_weightValue = 0;
			m_costValue = 0;
		} // end Start function
		
		// Update is called once per frame
		void Update()
		{
			// Nothing to do here atm.
		} // end Update function
		
		// Reset the variable to their initial state.
		// This is used when unequipping it.
		public void ResetArmor()
		{
			// Reset the variables.
			m_itemName = "NAN";
			m_itemType = "NAN";
			m_attackValue = 0;
			m_defenceValue = 0;
			m_inventoryValue = 0;
			m_weightValue = 0;
			m_costValue = 0;
		} // end ResetWeapon function
	} // end EquippedWeapon class
} // end namespace
