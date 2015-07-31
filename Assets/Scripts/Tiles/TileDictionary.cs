/*******************************************************************************
 *
 *  File Name: TileDictionary.cs
 *
 *  Description: A collection for tiles
 *
 *******************************************************************************/
using GSP.Char;
using System.Collections.Generic;
using UnityEngine;

namespace GSP.Tiles
{
    /*******************************************************************************
     *
     * Name: TileDictionary
     * 
     * Description: A collection that holds and handles all the tiles.
     * 
     *******************************************************************************/
    public static class TileDictionary
	{
		// The static dictionary; Vector3 is the key and Tile is the value
		static Dictionary<Vector3, Tile> tiles  = new Dictionary<Vector3, Tile>();
		
		// Returns if the key exists in the dictionary
		public static bool EntryExists(Vector3 key)
		{
			// Check if the key exists returning the result
            return tiles.ContainsKey(key);
		} // end EntryExixts

		// Gets the Tile associated with the given key
		public static Tile GetTile(Vector3 key)
		{
			// Just as a caution in case the caller doesn't check for the key's existence first
			// Or if it's easier to just call this; We'll check for the keys existence first
            if (!EntryExists(key))
			{
				// The key doesn't exist so return null
                Debug.LogErrorFormat("Key at {0} doesn't exist!", key.ToString("F2"));
				return null;
			} // end if

			// Otherwise the key exists so return the value according to the key
			return tiles[key];
		} // end GetTile

		// Update a Tile's ResourceType and GameObject
		public static void UpdateTile(Vector3 key, ResourceType resourceType, GameObject resource)
		{
			// Get the Tile at key
            Tile tile = GetTile(key);

			// Update the Tile
            tile.UpdateTile(resourceType, resource);
		} // end UpdateTile

		// Add an entry to the dictionary
		public static void AddEntry(Vector3 key, Tile tile)
		{
			// Add the entry to the dictionary
            tiles.Add(key, tile);
		} // end AddEntry

		// Remove an entry from the dictionary
		// NOTE: This removes the entire Tile from the dictionary
		public static void RemoveEntry(Vector3 key)
		{
			// As a precautionary measure, check if the key exists
            if (!EntryExists(key))
			{
				// The key doesn't exist so just return
				return;
			} // end if

			// First get the resource GameObject in the Tile
			GameObject obj = tiles[key].Resource;

			// Now destroy the GameObject
            MonoBehaviour.Destroy(obj);

			// Finally, remove the entry from the dictionary
            tiles.Remove(key);
		} // end RemoveEntry

		// Remove the resource from the Tile; This leaves the Tile intact
		public static void RemoveResource(Vector3 key)
		{
			// As a precautionary measure, check if the key exists
            if (!EntryExists(key))
			{
				// The key doesn't exist so just return
				return;
			} // end if

			// First get the resource GameObject in the Tile
            GameObject obj = tiles[key].Resource;

			// Now Destroy the GameObject
            MonoBehaviour.Destroy(obj);

			// Finally, update the Tile to be normal
            UpdateTile(key, ResourceType.None, null);
		} // end RemoveResource

		// Cleans/Empties the TileDictionary
		public static void Clean()
		{
			// All the keys in the dictionary
			var dictKeys = tiles.Keys;

			// The resource GameObject for each key in the loop
			GameObject obj;

			// Loop through the keys and destroy the GameObjects
            foreach (var key in dictKeys)
			{
				// First get the resource GameObject in the Tile
				obj = tiles[key].Resource;
				
				// Now destroy the GameObject
                MonoBehaviour.Destroy(obj);
			} // end foreach

			// Finally, empty the dictionary
			tiles.Clear();
		} // end Clean
	} // end TileDictionary
} // end GSP.Tiles
