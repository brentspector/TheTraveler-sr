using UnityEngine;
using System;
using System.Collections;

namespace GSP.Char
{
	// Resource enums
	// NOTE!!
	// SIZE must be the last item in the enum so that anything based
	// on the length of the enum can be used as normal. It is best to
	// add items to the left of NONE but after the current 3rd to last
	// item in the enum. For instance if the list was {SWORD, MACE, NONE, SIZE}
	// you should enter the new item between MACE and NONE. Create name
	// here and then define it under "SetResource" function.
	public enum ResourceType {WOOL, WOOD, FISH, ORE, NONE, SIZE};
	
	public class Resource
	{
		string m_resourceName;	// This is the name of the resource.
		int m_weightValue;		// This is the weight value of the resource.
		int m_sellValue;		// This is the sell value of the resource.
		int m_sizeValue;		// This is the size value of the resource.
		ResourceType m_resType;	// This is the type of the resource.

		// The constructor.
		public Resource()
		{
			// Initialise the values.
			m_resourceName = "NAN";
			m_weightValue = 0;
			m_sellValue = 0;
			m_sizeValue = 0;
			
			// Set resource type to none.
			m_resType = ResourceType.NONE;
		} // end constructor
		
		// Gets and Sets the resource's name.
		public string ResourceName
		{
			get { return m_resourceName; }
			set { m_resourceName = value; }
		} // end ResourceName property
		
		// Gets and Sets the resource's weight value.
		public int WeightValue
		{
			get{ return m_weightValue; }
			set{ m_weightValue = value; }
		} // end WeightValue property
		
		// Gets and Sets the resource's sell value.
		public int SellValue
		{
			get{ return m_sellValue; }
			set
			{ 
				m_sellValue = value; 

				// Check if the sell value is less than zero.
				if(m_sellValue < 0)
				{
					// Clamp the sell value to zero.
					m_sellValue = 0;
				} // end if statement
			} // end Set accessor
		}// end SellValue property

		public int SizeValue
		{
			get { return m_sizeValue; }
		} // end SizeValue property

		// Gets the resource's type.
		public ResourceType ResType
		{
			get { return m_resType; }
		} // end ResType property

		// Sets resource to predetermined types.
		// NOTE: These values can be changed.
		public string SetResource(string resourceType)
		{
			// Holds the results of parsing.
			ResourceType tmp;
			
			try
			{
				// Attempt to parse the string into the enum value.
				tmp = (ResourceType)Enum.Parse( typeof( ResourceType ), resourceType );
				
				// Switch over the possible values. ToUpper() is used as a caution.
				switch ( tmp )
				{
					case ResourceType.WOOL:
						// Set the values for the resource.
						m_resourceName = "Wool";
						m_sellValue = 15;
						m_weightValue = 10;
						m_sizeValue = 5;
						m_resType = ResourceType.WOOL;
						return "wool";
					case ResourceType.WOOD:
						// Set the values for the resource.
						m_resourceName = "Wood";
						m_sellValue = 20;
						m_weightValue = 15;
						m_sizeValue = 5;
						m_resType = ResourceType.WOOD;
						return "wood";
					case ResourceType.FISH:
						// Set the values for the resource.
						m_resourceName = "Fish";
						m_sellValue = 15;
						m_weightValue = 25;
						m_sizeValue = 5;
						m_resType = ResourceType.FISH;
						return "fish";
					case ResourceType.ORE:
						// Set the values for the resource.
						m_resourceName = "Ore";
						m_sellValue = 10;
						m_weightValue = 20;
						m_sizeValue = 5;
						m_resType = ResourceType.ORE;
						return "ore";
					default:
						return "NAN";
				} // end switch statment
			} // end try statement
			catch (Exception)
			{
				// The parsing failed so return NAN.
				Debug.Log( "Requested resource type '" + resourceType + "' was not found." );
				return "NAN";
			} // end catch statement
		} // end SetResource function
	} // end Resource class
} // end namespace