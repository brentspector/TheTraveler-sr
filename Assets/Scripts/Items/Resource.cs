using System;
using UnityEngine;

namespace GSP.Items
{
    public class Resource : Item
    {
        int weightValue;    // The weight value of the resource
        int sellValue;		// The sell value of the resource
        int sizeValue;		// The size value of the resource
        
        // Create a resource
        public Resource(string itemName, ResourceType itemType, int weight, int size, int worth) :
            base(itemName, itemType.ToString())
        {
            // Initialise the item to the given parameters
            weightValue = weight;
            sizeValue = size;
            sellValue = worth;
        } // end Resource

        // Gets and Sets the Resource's Weight
        public int Weight
        {
            get { return weightValue; }
            set { weightValue =  Utility.ZeroClampInt(value); }
        } // end Weight

        // Gets and Sets the Resource's Worth
        public int Worth
        {
            get { return sellValue; }
            set { sellValue = Utility.ZeroClampInt(value); }
        } // end Worth

        // Gets the Resource's Size
        public int Size
        {
            get { return  Utility.ZeroClampInt(sizeValue); }
        } // end Size

        // Gets the Resource's ResourceType
        public ResourceType ResourceType
        {
            get
            { 
                // Temp ReourceType
                ResourceType resourceType = ResourceType.None;

                // Try to parse the ResourceType from the Type
                try
                {
                    resourceType = (ResourceType)Enum.Parse(typeof(ResourceType), Type);

                    // Check if the result is defined
                    if (!Enum.IsDefined(typeof(ResourceType), resourceType))
                    {
                        // It's not defined so return None
                        Debug.LogErrorFormat("'{0}' is not defined within the ResourceType enum.", resourceType.ToString());
                        resourceType = ResourceType.None;
                    } // end if
                } // end try
                catch(ArgumentException)
                {
                    // We couldn't parse so return None
                    Debug.LogErrorFormat("Parsing the Type '{0}' as a ResourceType failed.", Type);
                    resourceType = ResourceType.None;
                } // end catch

                // Return the result
                return resourceType;
            } // end get
        } // end Type
    } // end Resource
} // end GSP.Items