using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Char;

using System;
using System.IO;

namespace GSP.Tiles
{
	public static class TileDictionary
	{
		// Declare our private static dictionary variable here.
		// Vector3 is the key and Tile is the value.
		static Dictionary<Vector3, Tile> m_tileDictionary  = new Dictionary<Vector3, Tile>();
		
		// Returns if the key exists in the dictionary.
		public static bool EntryExists( Vector3 key )
		{
			// Check if the key exists returning the result.
			return m_tileDictionary.ContainsKey( key );
		} // end EntryExixts function

		// Gets the tile associated with the given key
		public static Tile GetTile( Vector3 key )
		{
			// Just as a caution in case the caller doesn't check for the key's existence first.
			// Or if it's easier to just call this. We'll check for the keys existence first.
			if ( !EntryExists( key ) )
			{
				// The key doesn't exist so return null.
				Debug.LogError("Key at " + key.ToString("F2") + " doesn't exist!");
				return null;
			} // end if statement

			// Otherwise the key exists so return the value according to the key.
			return m_tileDictionary[key];
		} // end GetTile function

		// Update a tile's resource type and object.
		public static void UpdateTile( Vector3 key, ResourceType resourceType, GameObject resource )
		{
			// Get the tile at key.
			Tile tile = GetTile( key );

			// Update the tile.
			tile.UpdateTile( resourceType, resource );
		} // end UpdateTile

		// Add an entry to the dictionary.
		public static void AddEntry( Vector3 key, Tile tile )
		{
			// Add the entry to the dictionary.
			m_tileDictionary.Add( key, tile );
		} // end AddEntry function

		// Remove an entry from the dictionary.
		// NOTE: This removes the entire tile from the dictionary.
		public static void RemoveEntry( Vector3 key )
		{
			// As a precautionary measure, check if the key exists.
			if ( !EntryExists( key ) )
			{
				// The key doesn't exist so just return.
				return;
			} // end if statement

			// First get the resource game object in the tile.
			GameObject obj = m_tileDictionary[key].Resource;

			// Now destroy the game object.
			MonoBehaviour.Destroy( obj );

			// Finally, remove the entry from the dictionary.
			m_tileDictionary.Remove( key );
		} // end RemoveEntry function

		// Remove the resource from the tile. This leaves the tile intact.
		public static void RemoveResource( Vector3 key )
		{
			// As a precautionary measure, check if the key exists.
			if ( !EntryExists( key ) )
			{
				// The key doesn't exist so just return.
				return;
			} // end if statement

			// First get the resource game object in the tile.
			GameObject obj = m_tileDictionary[key].Resource;

			// Now Destroy the game object.
			MonoBehaviour.Destroy( obj );

			// Finally, update the tile to be a normal tile.
			UpdateTile( key, ResourceType.NONE, null );
		} // end RemoveResource

		// Cleans/Empties the tile dictionary.
		public static void Clean()
		{
			// Holds the all the keys in the dictionary.
			var dictKeys = m_tileDictionary.Keys;

			// Holds the resource game object for each key in the loop.
			GameObject obj;

			// Loop through the keys and destroy the objects.
			foreach ( var key in dictKeys )
			{
				// First get the resource game object in the tile.
				obj = m_tileDictionary[key].Resource;
				
				// Now destroy the game object.
				MonoBehaviour.Destroy( obj );
			}

			// Finally, empty the dictionary.
			m_tileDictionary.Clear();
		} // end Clean function
	} // end TileDictionary class
} // end namespace
