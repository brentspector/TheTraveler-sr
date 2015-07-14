using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char
{
	// Very simple resource script. Handles the resource value and weight collectively.
	// Could be expanded to be more complex with individual resources later.
	public class ResourceList : MonoBehaviour
	{
		// Declare out private variables. The default scope is private.
		int m_totalValue;				// This is the value of all the resources combined.
		int m_totalWeight;				// This is the weight of all the resources combined.
		int m_totalSize;				// This is the total size of all the resources combined.
		List<Resource> m_resourceList;	// This is the list of resources the character is holding.
		
		// Gets the total value of all the resources being carried.
		public int TotalValue
		{
			get { return m_totalValue; }
		} // end TotalValue property
		
		// Gets the total weight of all the resources being carried.
		public int TotalWeight
		{
			get { return m_totalWeight; }
		} // end TotalWeight property

		public int TotalSize
		{
			get { return m_totalSize; }
		} // end TotalSize property

		// Gets the number of resources the character is holding.
		public int NumResources
		{
			get { if (m_resourceList == null) Debug.LogError("NULL"); return m_resourceList.Count; }
		} // end NumResources property
		
		// Use this for initialisation.
		void Start()
		{
			// Initialise the values.
			m_totalValue = 0;
			m_totalWeight = 0;
			m_totalSize = 0;
			m_resourceList = new List<Resource>();
		} // end Start function
		
		// Update is called once per frame.
		void Update()
		{
			// Nothing to do here.
		} // end Update function
		
		// Adds a resource.
		// This is called upon picking up a resource.
		public void AddResource( Resource resource, int amount )
		{
			// Check if the ammount is zero or less.
			if ( amount <= 0)
			{
				// Simply return.
				return;
			} // end if statement

			// Add the number of resources.
			for ( int index = 0; index < amount; index++ )
			{
				// Add the resource to the list.
				m_resourceList.Add( resource );
				
				// Add the resource's values.
				m_totalValue += resource.SellValue;
				m_totalWeight += resource.WeightValue;
				m_totalSize += resource.SizeValue;
			} // emd for loop
		} // end AddResource function
		
		// Removes a resource.
		// This is called upon transferring a resource to another character.
		// NOTE: Don't use the below code.
//		public void RemoveResource( Resource resource, int amount )
//		{
//			// Check if the ammount is zero or less.
//			if ( amount <= 0)
//			{
//				// Simply return.
//				return;
//			} // end if statement
//
//			// Remove the number of resources.
//			for (int index = 0; index < amount; index++)
//			{
//				// Check if the operation will bring the total value to zero or less.
//				if ((m_totalValue - resource.SellValue) <= 0)
//				{
//					// Clamp to zero.
//					m_totalValue = 0;
//				} // end if statement
//				else
//				{
//					// Otherwise subtract the given value.
//					m_totalValue -= resource.SellValue;
//				} // end else statement
//				
//				// Check if the operation will bring the weight value to zero or less.
//				if ((m_totalValue - resource.WeightValue) <= 0)
//				{
//					// Clamp to zero.
//					m_totalWeight = 0;
//				} // end if statement
//				else
//				{
//					// Otherwise subtract the given value.
//					m_totalWeight -= resource.WeightValue;
//				} // end else statement
//
//				// Check if the operation will bring the weight value to zero or less.
//				if ((m_totalSize - resource.SizeValue) <= 0)
//				{
//					// Clamp to zero.
//					m_totalSize = 0;
//				} // end if statement
//				else
//				{
//					// Otherwise subtract the given value.
//					m_totalSize -= resource.SizeValue;
//				} // end else statement
//
//				// Remove the resource from the list.
//				m_resourceList.Remove( resource );
//			} // end for loop
//		} // end RemoveResource function

		// Removes a resource.
		// This removes a single resource.
		public void RemoveResource( Resource resource )
		{
			// Check if the operation will bring the total value to zero or less.
			if ( ( m_totalValue - resource.SellValue ) <= 0 )
			{
				// Clamp to zero.
				m_totalValue = 0;
			} // end if statement
			else
			{
				// Otherwise subtract the given value.
				m_totalValue -= resource.SellValue;
			} // end else statement
			
			// Check if the operation will bring the weight value to zero or less.
			if ( ( m_totalWeight - resource.WeightValue ) <= 0 )
			{
				// Clamp to zero.
				m_totalWeight = 0;
			} // end if statement
			else
			{
				// Otherwise subtract the given value.
				m_totalWeight -= resource.WeightValue;
			} // end else statement
			
			// Check if the operation will bring the weight value to zero or less.
			if ( ( m_totalSize - resource.SizeValue ) <= 0 )
			{
				// Clamp to zero.
				m_totalSize = 0;
			} // end if statement
			else
			{
				// Otherwise subtract the given value.
				m_totalSize -= resource.SizeValue;
			} // end else statement
			
			// Remove the resource from the list.
			m_resourceList.Remove( resource );
		} // end RemoveResource function

		// Removes all resources in the given list.
		public void RemoveResources( List<Resource> resourceList )
		{
			// Loop over the resource list.
			foreach ( var item in resourceList )
			{
				// Check if the operation will bring the total value to zero or less.
				if ( ( m_totalValue - item.SellValue ) <= 0 )
				{
					// Clamp to zero.
					m_totalValue = 0;
				} // end if statement
				else
				{
					// Otherwise subtract the given value.
					m_totalValue -= item.SellValue;
				} // end else statement
				
				// Check if the operation will bring the weight value to zero or less.
				if ( ( m_totalWeight - item.WeightValue ) <= 0 )
				{
					// Clamp to zero.
					m_totalWeight = 0;
				} // end if statement
				else
				{
					// Otherwise subtract the given value.
					m_totalWeight -= item.WeightValue;
				} // end else statement
				
				// Check if the operation will bring the weight value to zero or less.
				if ( ( m_totalSize - item.SizeValue ) <= 0 )
				{
					// Clamp to zero.
					m_totalSize = 0;
				} // end if statement
				else
				{
					// Otherwise subtract the given value.
					m_totalSize -= item.SizeValue;
				} // end else statement

				// Remove the resource from the list.
				m_resourceList.Remove( item );
			} // end foreach loop
		} // end RemoveResources function
		
		// Clear the resources. This sets the values back to zero.
		// This is called after selling.
		public void ClearResources()
		{
			// Zero out the values.
			m_totalValue = 0;
			m_totalWeight = 0;
			m_totalSize = 0;

			// Empty the list.
			m_resourceList.Clear();
		} // end ClearResources function

		// Gets the first resource found in the list of the given type.
		public List<Resource> GetResourcesByType( string resourceType )
		{
			// Holds the results of parsing.
			ResourceType tmp;

			// Holds the resource list.
			List<Resource> resourceList = new List<Resource>();

			try
			{
				// Attempt to parse the string into the enum value.
				tmp = (ResourceType)Enum.Parse( typeof( ResourceType ), resourceType );
				
				// Search the list for the resource type.
				resourceList = m_resourceList.FindAll( res => res.ResType == tmp );
			} // end try statement
			catch (Exception)
			{
				// The parsing failed.
				print( "Requested resource type '" + resourceType + "' was not found." );
			} // end catch statement

			// Otherwise, return the object.
			return resourceList;
		} // end GetResourceByType function

		// Gets the resource by its index
		public Resource GetResourceByIndex( int index )
		{
			// Search the list for the resource type.
			Resource resource = m_resourceList[index];
			
			// Check if the resource was found.
			if ( resource == null )
			{
				// The resource wasn't found so return null;
				return null;
			}
			
			// Otherwise, return the object.
			return resource;
		} // end GetResourceByIndex function
	} // end ResourceList class
} // end namespace
