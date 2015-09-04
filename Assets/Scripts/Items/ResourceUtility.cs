/*******************************************************************************
 *
 *  File Name: ResourceUtility.cs
 *
 *  Description: Contains utility functions for getting and removing resources
 *               from the new inventory system
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Items.Inventories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    
namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: ResourceUtility
     * 
     * Description: Utility script for getting and removing resources from the new
     *              inventory system.
     * 
     *******************************************************************************/
    public class ResourceUtility
    {
        // Get all the resources
        public List<Resource> GetResources(int playerNum)
        {
            // The list to return; returns empty list if the inventory doesn't exist
            List<Resource> resources = new List<Resource>();

            // Get the inventory script
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<PlayerInventory>();

            // Make sure the inventory exists
            if (inventory != null)
            {
                // Get all the items in the inventory
                List<Item> inventoryItems = inventory.GetItems(playerNum);

                // Find all the resources
                resources = inventoryItems.FindAll(tempItem => tempItem is Resource).Select(item => (Resource)item).ToList();
            } // end if

            // Return the list of resources
            return resources;
        } // end GetResources

        // Removes all the resources
        public void RemoveResources(int playerNum)
        {
            // Get the inventory script
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<PlayerInventory>();

            // Make sure the inventory exists
            if (inventory != null)
            {
                // Get all the resources
                List<Resource> resources = new List<Resource>();
                resources = GetResources(playerNum);

                // Loop over each resource and remove it
                foreach (var resource in resources)
                {
                    inventory.Remove(resource);
                } // end foreach
            } // end if
        } // end RemoveResources

        // Get all the resources of a given type
        public List<Resource> GetResourcesByType(ResourceType resourceType, int playerNum)
        {
            // The list to return; returns empty list if the inventory doesn't exist
            List<Resource> resources = new List<Resource>();
            
            // Get the inventory script
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<PlayerInventory>();

            // Make sure the inventory exists
            if (inventory != null)
            {
                // Get all the items in the inventory
                List<Item> inventoryItems = inventory.GetItems(playerNum);

                // Find all the resources of the given type
                resources = inventoryItems.FindAll(tempItem => tempItem.Type == resourceType.ToString()).Select(item => (Resource)item).ToList();
            } // end if

            // Return the list of resources
            return resources;
        } // end RemoveResources

        // Remove all the resources of a given type
        public void RemoveResourcesByType(ResourceType resourceType, int playerNum)
        {
            // Get the inventory script
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<PlayerInventory>();

            // Make sure the inventory exists
            if (inventory != null)
            {
                // Get all the resources
                List<Resource> resources = GetResourcesByType(resourceType, playerNum);

                // Loop over each resource and remove it
                foreach (var resource in resources)
                {
                    inventory.Remove(resource);
                } // end foreach
            } // end if
        } // end RemoveResources

        // Remove a single resource
        public void RemoveResource(Resource resource, int playerNum)
        {
            // Get the inventory script
            PlayerInventory inventory = GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<PlayerInventory>();

            // Make sure the inventory exists
            if (inventory != null)
            {
                // Remove the resource
                inventory.Remove(resource);
            } // end if
        } // end RemoveResource
    } // end ResourceUtility
} // end GSP.Items
