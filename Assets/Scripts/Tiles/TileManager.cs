using UnityEngine;
using System.Collections;
using System;
using GSP.Char;

namespace GSP.Tiles
{
	public static class TileManager
	{
		// Declare the tile values that will be used here. This will be the same that is used in Tiled when creating the map.
		static int m_tileSize = 0;		// The size of each tile.
		static int m_numTilesWide = 0;	// The number of tiles in width.
		static int m_numTilesHigh = 0;	// The number of tile in height.

		// Gets the tile size.
		public static int TileSize
		{
			get { return m_tileSize; }
		} // end TileSize property.

		// Gets the number of tiles in width
		public static int NumTilesWide
		{
			get { return m_numTilesWide; }
		} // end NumTilesWide property

		// Gets the number of tiles in width
		public static int NumTilesHigh
		{
			get { return m_numTilesHigh; }
		} // end NumTilesWide property

		// Gets the max height you can place a tile.
		public static int MaxHeight
		{
			get
			{
				// Calculate the height of the map. Then subtract half the tile size. This is the highest coord the tile can be placed at.
				// This is because we start at 32.
				int temp = NumTilesHigh * TileSize - ( TileSize / 2 );

				// Since we're in the fourth quadrant, the y value is negative.
				return ( temp * -1 );
			} // end get accessor
		} // end MaxHeight property

		// Gets the max width you can place a tile.
		public static int MaxWidth
		{
			get
			{
				// Calculate the width of the map. Then subtract half the tile size. This is the highest coord the tile can be placed at.
				// This is because we start at 32.
				return ( NumTilesWide * TileSize - ( TileSize / 2 ) );
			} // end get accessor
		} // end MaxHeight property

		// Gets the min height you can place a tile.
		public static int MinHeight
		{
			get { return ( TileSize / 2 ) * -1; }
		} // end MinWidth property

		// Gets the min width you can place a tile.
		public static int MinWidth
		{
			get { return TileSize / 2; }
		} // end MaxWidth property

		// Gets the min height you can place a tile in units.
		public static float MinHeightUnits
		{
			get { return ToUnits( new Vector3(MinHeight, 0, 0 ) ).x; }
		} // end MinHeightUnits property

		// Gets the min width you can place a tile in units.
		public static float MinWidthUnits
		{
			get { return ToUnits( new Vector3(MinWidth, 0, 0 ) ).x; }
		} // end MinWidthUnits property

		// Gets the max height you can place a tile in units.
		public static float MaxHeightUnits
		{
			get { return ToUnits( new Vector3(MaxHeight, 0, 0 ) ).x; }
		} // end MaxHeightUnits property

		// Gets the max width you can place a tile in units.
		public static float MaxWidthUnits
		{
			get { return ToUnits( new Vector3(MaxWidth, 0, 0 ) ).x; }
		} // end MaxWidthUnits property

		// Gets the map size.
		public static Vector2 MapSize
		{
			get
			{
				return new Vector2( NumTilesWide * TileSize, NumTilesHigh * TileSize );
			} // end get accessor
		} // end MapSize property

		// Gets the standard move distance for a player.
		public static float PlayerMoveDistance
		{
			get
			{
				// Originally ToUnits is a Vector3 operation. So we pass in zeroes for y and z here.
				// All we care about is the x component.
				return ToUnits( new Vector3(TileSize, 0, 0) ).x;
			}
		}

		// Sets the dimensions of the Tiled map. This should only be called once when initialising the map.
		// You shouldn't change these values unless you redo the map in Tiled.
		// NOTE: Setting these incorrectly will screw things up.
		public static void SetDimensions( int tileSize, int numTilesWide, int numTilesHigh )
		{
			m_tileSize = tileSize;
			m_numTilesWide = numTilesWide;
			m_numTilesHigh = numTilesHigh;
		} // end SetDimensions function

		// Generates tiles and adds them to the dictionary.
		public static void GenerateAndAddTiles()
		{
			// Get all the game objects tagged as resources.
			GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag( "Resource" );

			// Loop over the map. Width first.
			for ( int width = 32; width < (int)MapSize.x; width += 64 )
			{
				// Then height.
				for ( int height = 32; height < (int)MapSize.y; height += 64 )
				{
					// We are in the fourth quadrant so the y is negative.
					Vector3 key = new Vector3( width, height * -1, -1.0f );

					// Create an empty tile at the given position.
					Tile newTile = new Tile( key, ResourceType.NONE, null );

					// Add the tile to the dictionary.
					TileDictionary.AddEntry( key, newTile );
				} // end inner height for loop
			} // end outer width for loop

			// Now loop over the resource array and set the tiles to resources.
			for (int index = 0; index < resourceObjects.Length; index++)
			{
				Vector3 key = ToPixels( resourceObjects[index].transform.position );

				// Get the ResourceTile component.
				ResourceTile resTile = resourceObjects[index].GetComponent<ResourceTile>();

				// Holds the tile's resource type.
				ResourceType resourceType = resTile.resourceType;

				// Update the tile at the given key.
				TileDictionary.UpdateTile( key, resourceType, resourceObjects[index] );
			} // end for loop
		} // end GenerateAndAddTiles function.

		// Converts unity units to pixels for use on the map.
		public static Vector3 ToPixels( Vector3 param )
		{
			// To convert the parameter to pixels that the resource positions use, multiply by 100.
			Vector3 tmp = new Vector3(param.x * 100, param.y * 100, param.z * 100);

			// Check if the width (x) is within the valid map positions and Clamp if not.
			if ( tmp.x > MaxWidth)
			{
				tmp.x = MaxWidth;
			} // end if statement

			// Check if the height (y) is within the valid map positions and Clamp if not.
			if ( tmp.y < MaxHeight)
			{
				tmp.y = MaxHeight;
			} // end if statement

			// We need integers for the keys to work. So convert the temp vector3's params to integers.
			// NOTE: Trying to use "(int)tmp.x" results in the wrong number. So use "Convert.ToInt32(tmp.x)"
			int resX = Convert.ToInt32(tmp.x);
			int resY = Convert.ToInt32(tmp.y);
			int resZ = Convert.ToInt32(tmp.z);

			// Everything should be fine now so return the result.
			return new Vector3( resX, resY, resZ );
		} // end ToPixels function

		// Converts pixels to unity units for use on the map.
		public static Vector3 ToUnits( Vector3 param )
		{
			// To convert the parameter to units that unity positions use, divide by 100.
			Vector3 tmp = new Vector3( param.x / 100.0f, param.y / 100.0f, param.z / 100.0f );

			// Gets the max width and height in units.
			float maxWidth = MaxWidth / 100.0f;
			float maxHeight = MaxHeight / 100.0f;
			
			// Check if the width (x) is within the valid map positions and Clamp if not.
			if ( tmp.x > maxWidth )
			{
				tmp.x = maxWidth;
			} // end if statement
			
			// Check if the height (y) is within the valid map positions and Clamp if not.
			if ( tmp.y < maxHeight )
			{
				tmp.y = maxHeight;
			} // end if statement
			
			// Everything should be fine now sp return the result.
			return tmp;
		} // end ToPixels function

	} // end TileManager class
} // end namespace
