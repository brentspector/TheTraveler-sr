/*******************************************************************************
 *
 *  File Name: AllyData.cs
 *
 *  Description: A clean serialisable class for saving and loading ally data
 *
 *******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Core
{
    /*******************************************************************************
     *
     * Name: AllyData
     * 
     * Description: Container class for a clean version of the serialisable ally
     *              data.
     * 
     *******************************************************************************/
    [Serializable]
    public class AllyData
    {
        List<int> itemIds;              // The player's list of item IDs in their inventory

        // Default constructor; Creates an empty object
        public AllyData()
        {
            // Initialise the list
            itemIds = new List<int>();

            // Reset the container; This prevents the initialisation being in 2 places
            Reset();
        } // end PlayerData

        // Resets the contents of the container
        public void Reset()
        {
            // Clear the list
            itemIds.Clear();
        } // end Reset

        // Gets an itemId at the given index
        public int GetItemId(int index)
        {
            return itemIds[index];
        } // end GetItemId

        // Adds an itemId to the list
        public void AddItemId(int itemId)
        {
            itemIds.Add(itemId);
        } // end AddItemId

        public int IdsCount
        {
            get { return itemIds.Count; }
        } // end IdsCount
    } // end PlayerData
} // end GSP.Core
