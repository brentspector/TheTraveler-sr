using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Tiles;
using System;

namespace GSP
{
	public static class Highlight
	{
		static List<GameObject> m_highlightList = new List<GameObject>();

		public static void GenerateHighlight( Vector3 position, int travelDistance )
		{
			// Initialise the temporary position to a single tile above the player's position.
			Vector3 tmpPos = new Vector3( position.x, position.y + TileManager.PlayerMoveDistance, position.z );

			// Truncate the y value now.
			tmpPos.y = Convert.ToInt32(tmpPos.y * 100) / 100.0f;

			// Stretch to the north first.
			for ( int northIndex = 0; northIndex < travelDistance; northIndex++ )
			{
				// Check if we have hit the min height.
				if ( tmpPos.y > TileManager.MinHeightUnits )
				{
					// Reached min height so break out of the loop.
					break;
				} // end if statement
				else
				{
					// Otherwise add a highlight tile to the position.
					AddHighlight( tmpPos );
					
					// Advance the position to the next tile.
					tmpPos.y += TileManager.PlayerMoveDistance;
				} // end else statement
			} // end for loop

			// Reset the temporary vector to be a single tile to the right of the player's position.
			tmpPos = new Vector3( position.x + TileManager.PlayerMoveDistance, position.y, position.z );

			// Truncate the x value now.
			tmpPos.x = Convert.ToInt32(tmpPos.x * 100) / 100.0f;

			// Stretch to the east next.
			for ( int eastIndex = 0; eastIndex < travelDistance; eastIndex++ )
			{
				// Check if we have hit the max width.
				if ( tmpPos.x > TileManager.MaxWidthUnits )
				{
					// Reached min height so break out of the loop.
					break;
				} // end if statement
				else
				{
					// Otherwise add a highlight tile to the position.
					AddHighlight( tmpPos );
					
					// Advance the position to the next tile.
					tmpPos.x += TileManager.PlayerMoveDistance;
				} // end else statement
			} // end for loop

			// Reset the temporary vector to be a single tile below the player's position.
			tmpPos = new Vector3( position.x, position.y - TileManager.PlayerMoveDistance, position.z );
			
			// Truncate the y value now.
			tmpPos.y = Convert.ToInt32(tmpPos.y * 100) / 100.0f;
			
			// Stretch to the south next.
			for ( int southIndex = 0; southIndex < travelDistance; southIndex++ )
			{
				// Check if we have hit the min height.
				if ( tmpPos.y < TileManager.MaxHeightUnits )
				{
					// Reached min height so break out of the loop.
					break;
				} // end if statement
				else
				{
					// Otherwise add a highlight tile to the position.
					AddHighlight( tmpPos );
					
					// Advance the position to the next tile.
					tmpPos.y -= TileManager.PlayerMoveDistance;
				} // end else statement
			} // end for loop

			// Reset the temporary vector to be a single tile to the left of the player's position.
			tmpPos = new Vector3( position.x - TileManager.PlayerMoveDistance, position.y, position.z );
			
			// Truncate the x value now.
			tmpPos.x = Convert.ToInt32(tmpPos.x * 100) / 100.0f;
			
			// Finally, stretch to the west.
			for ( int westIndex = 0; westIndex < travelDistance; westIndex++ )
			{
				// Check if we have hit the max width.
				if ( tmpPos.x < TileManager.MinWidthUnits )
				{
					// Reached min height so break out of the loop.
					break;
				} // end if statement
				else
				{
					// Otherwise add a highlight tile to the position.
					AddHighlight( tmpPos );
					
					// Advance the position to the next tile.
					tmpPos.x -= TileManager.PlayerMoveDistance;
				} // end else statement
			} // end for loop

			// Create the highlight tile centered at the player's location.
			//highlight = GameObject.Instantiate( PrefabReference.prefabHighlight, position, Quaternion.identity ) as GameObject;

			// Add the highlight tile to the list.
			//m_highlightList.Add( highlight );
		} // end GenerateHighlight function

		// Destroys the highlight objects.
		public static void ClearHightlight()
		{
			// Loop over the list and destroy each object.
			foreach ( var highlight in m_highlightList )
			{
				GameObject.Destroy( highlight );
			} // end foreach loop

			// Clear the list just to be sure.
			m_highlightList.Clear();
		} // end ClearHighlight function

		// Adds a highlight object to scene and to the list.
		static void AddHighlight( Vector3 position )
		{
			// Otherwise add a highlight tile to the position.
			GameObject highlight = GameObject.Instantiate( PrefabReference.prefabHighlight, position, Quaternion.identity ) as GameObject;
			
			// Add the highlight tile to the list.
			m_highlightList.Add( highlight );
		} // end AddHighlight function
	} // end Highlight class
} // end namespace
