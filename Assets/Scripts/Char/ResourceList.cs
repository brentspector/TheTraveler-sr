/*******************************************************************************
 *
 *  File Name: ResourceList.cs
 *
 *  Description: A collection for resources
 *
 *******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Char
{
    //TODO: Damien: Replace with something for a better inventory later.
    /*******************************************************************************
     *
     * Name: ResourceList
     * 
     * Description: Very simple resource script. Handles the resource value and
     *              weight collectively. Could be expanded to be more complex with
     *              individual resources later.
     * 
     *******************************************************************************/
    public class ResourceList : MonoBehaviour
	{
		int totalValue;			    // Value of all the resources combined
		int totalWeight;			// Weight of all the resources combined
		int totalSize;			    // Total size of all the resources combined
		List<Resource> resources;   // List of resources the character is holding
		
		// Use this for initialisation
		void Start()
		{
			// Initialise the values
			totalValue = 0;
			totalWeight = 0;
			totalSize = 0;
			resources = new List<Resource>();
		} // end Start
		
		// Adds a resource
		// This is called upon picking up a resource
		public void AddResource(Resource resource, int amount)
		{
			// Check if the ammount is zero or less
            if (amount <= 0)
			{
				// Simply return
				return;
			} // end if

			// Add the number of resources
            for (int index = 0; index < amount; index++)
			{
				// Add the resource to the list.
                resources.Add(resource);
				
				// Add the resource's values.
				totalValue += resource.SellValue;
				totalWeight += resource.WeightValue;
				totalSize += resource.SizeValue;
			} // end for
		} // end AddResource
		
		// Removes a resource
		// This removes a single resource
		public void RemoveResource(Resource resource)
		{
			// Check if the operation will bring the total value to zero or less
			if ((totalValue - resource.SellValue) <= 0)
			{
				// Clamp to zero
				totalValue = 0;
			} // end if
			else
			{
				// Otherwise subtract the given value
				totalValue -= resource.SellValue;
			} // end else
			
			// Check if the operation will bring the weight value to zero or less
			if ((totalWeight - resource.WeightValue) <= 0)
			{
				// Clamp to zero
				totalWeight = 0;
			} // end if
			else
			{
				// Otherwise subtract the given value
				totalWeight -= resource.WeightValue;
			} // end else
			
			// Check if the operation will bring the size value to zero or less
			if ((totalSize - resource.SizeValue) <= 0)
			{
				// Clamp to zero
				totalSize = 0;
			} // end if
			else
			{
				// Otherwise subtract the given value
				totalSize -= resource.SizeValue;
			} // end else
			
			// Remove the resource from the list
            resources.Remove(resource);
		} // end RemoveResource

		// Removes all resources in the given list
		public void RemoveResources(List<Resource> resourceList)
		{
			// Loop over the resource list.
			foreach (var item in resourceList)
			{
                // Check if the operation will bring the total value to zero or less
                if ((totalValue - item.SellValue) <= 0)
                {
                    // Clamp to zero
                    totalValue = 0;
                } // end if
                else
                {
                    // Otherwise subtract the given value
                    totalValue -= item.SellValue;
                } // end else

                // Check if the operation will bring the weight value to zero or less
                if ((totalWeight - item.WeightValue) <= 0)
                {
                    // Clamp to zero
                    totalWeight = 0;
                } // end if
                else
                {
                    // Otherwise subtract the given value
                    totalWeight -= item.WeightValue;
                } // end else

                // Check if the operation will bring the size value to zero or less
                if ((totalSize - item.SizeValue) <= 0)
                {
                    // Clamp to zero
                    totalSize = 0;
                } // end if
                else
                {
                    // Otherwise subtract the given value
                    totalSize -= item.SizeValue;
                } // end else

				// Remove the resource from the list
                resources.Remove(item);
			} // end foreach
		} // end RemoveResources
		
		// Clear the resources. This sets the values back to zero
		// This is called after selling
		public void ClearResources()
		{
			// Zero out the values
			totalValue = 0;
			totalWeight = 0;
			totalSize = 0;

			// Empty the list
			resources.Clear();
		} // end ClearResources

		// Gets all resources found in the list of the given type
		public List<Resource> GetResourcesByType(string resourceType)
		{
            ResourceType tmp;   // Holds the results of parsing

            List<Resource> resultResources = new List<Resource>(); // Holds the resource list

			try
			{
				// Attempt to parse the string into the enum value
                tmp = (ResourceType)Enum.Parse(typeof(ResourceType), resourceType);
				
				// Search the list for the resource type
                resultResources = resources.FindAll(res => res.Type == tmp);
			} // end try
			catch (Exception)
			{
				// The parsing failed
                Debug.LogWarningFormat("Requested resource type '{0}'", resourceType);
			} // end catch

			// Otherwise, return the object
			return resultResources;
		} // end GetResourceByType

		// Gets the resource by its index
		public Resource GetResourceByIndex(int index)
		{
			// Search the list for the resource type
            Resource resource = resources[index];
			
			// Check if the resource was found
            if (resource == null)
			{
				// The resource wasn't found so return null;
				return null;
			}
			
			// Otherwise, return the object
			return resource;
		} // end GetResourceByIndex

        // Gets the total value of all the resources being carried
        public int TotalValue
        {
            get { return totalValue; }
        } // end TotalValue

        // Gets the total weight of all the resources being carried
        public int TotalWeight
        {
            get { return totalWeight; }
        } // end TotalWeight

        public int TotalSize
        {
            get { return totalSize; }
        } // end TotalSize

        // Gets the number of resources the character is holding
        public int NumResources
        {
            get
            {
                return resources.Count;
            } // end get
        } // end NumResources
	} // end ResourceList
} // end GSP.Char
