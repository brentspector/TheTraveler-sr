/*******************************************************************************
 *
 *  File Name: Resource.cs
 *
 *  Description: Logic of a single resource
 *
 *******************************************************************************/
using System;
using UnityEngine;

namespace GSP.Char
{
    //TODO: Damien: Replace with something for a better inventory later.
    /*******************************************************************************
     *
     * Name: Resource
     * 
     * Description: Manages a single resource object.
     * 
     *******************************************************************************/
    public class Resource
	{
		string name;	            // The name of the resource
		int weightValue;		    // The weight value of the resource
		int sellValue;		        // The sell value of the resource
		int sizeValue;		        // The size value of the resource
		ResourceType type;  // The type of the resource

		// The constructor; initialises the values for the resource
		public Resource()
		{
			// Initialise the values
			name = "NAN";
			weightValue = 0;
			sellValue = 0;
			sizeValue = 0;
			
			// Set Type to None
			type = ResourceType.None;
		} // end Resource

		// Sets resource to predetermined types
		// NOTE: These values can be changed
		public string SetResource(string resourceType)
		{
			// Holds the results of parsing
			ResourceType tmp;
			
			try
			{
				// Attempt to parse the string into the enum value
                tmp = (ResourceType)Enum.Parse(typeof(ResourceType), resourceType);
				
				// Switch over the possible values. ToUpper() is used as a caution
				switch (tmp)
				{
					case ResourceType.Wool:
                        {
                            // Set the values for the resource
                            name = "Wool";
                            sellValue = 15;
                            weightValue = 10;
                            sizeValue = 5;
                            this.type = ResourceType.Wool;
                            return "wool";
                        }
					case ResourceType.Wood:
                        {
                            // Set the values for the resource
                            name = "Wood";
                            sellValue = 20;
                            weightValue = 15;
                            sizeValue = 5;
                            this.type = ResourceType.Wood;
                            return "wood";
                        }
					case ResourceType.Fish:
                        {
                            // Set the values for the resource
                            name = "Fish";
                            sellValue = 15;
                            weightValue = 25;
                            sizeValue = 5;
                            this.type = ResourceType.Fish;
                            return "fish";
                        }
					case ResourceType.Ore:
                        {
                            // Set the values for the resource
                            name = "Ore";
                            sellValue = 10;
                            weightValue = 20;
                            sizeValue = 5;
                            this.type = ResourceType.Ore;
                            return "ore";
                        }
                    default:
                        {
                            // Invalid resource so return NAN
                            return "NAN";
                        }
                } // end switch tmp
			} // end try
			catch (Exception)
			{
				// The parsing failed so return NAN.
				Debug.LogWarningFormat("Requested resource type '{0}' was not found!", resourceType);
				return "NAN";
			} // end catch
		} // end SetResource

        // Gets and Sets the resource's Name
        public string Name
        {
            get { return name; }
            set { name = value; }
        } // end Name

        // Gets and Sets the resource's WeightValue
        public int WeightValue
        {
            get { return weightValue; }
            set { weightValue = value; }
        } // end WeightValue

        // Gets and Sets the resource's SellValue
        public int SellValue
        {
            get { return sellValue; }
            set
            {
                // Set to the value
                sellValue = value;

                // Check if the sell value is less than zero
                if (sellValue < 0)
                {
                    // Clamp the sell value to zero
                    sellValue = 0;
                } // end if
            } // end set
        } // end SellValue

        // Gets the resource's SizeValue
        public int SizeValue
        {
            get { return sizeValue; }
        } // end SizeValue

        // Gets the resource's Type
        public ResourceType Type
        {
            get { return type; }
        } // end Type
	} // end Resource
} // end GSP.Char