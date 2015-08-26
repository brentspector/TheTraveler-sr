/*******************************************************************************
 *
 *  File Name: TileManager.cs
 *
 *  Description: Deals with the logic of the tiles
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Items;
using System;
using UnityEngine;

namespace GSP.Tiles
{
    /*******************************************************************************
     *
     * Name: TileManager
     * 
     * Description: Manages all the tiles in the game.
     * 
     *******************************************************************************/
    public static class TileManager
	{
		// These will be the same that is used in Tiled when creating the map
		static int tileSize = 0;		// The size of each tile
		static int numTilesWide = 0;	// The number of tiles in width
		static int numTilesHigh = 0;	// The number of tile in height

    	// Sets the dimensions of the Tiled map; This should only be called once when initialising the map
		// You shouldn't change these values unless you redo the map in Tiled
		// NOTE: Setting these incorrectly will screw things up
		public static void SetDimensions(int size, int tilesWide, int tilesHigh)
		{
			tileSize = size;
			numTilesWide = tilesWide;
			numTilesHigh = tilesHigh;
		} // end SetDimensions

		// Generates tiles and adds them to the dictionary
		public static void GenerateAndAddTiles()
		{
			// Get all the game objects tagged as resources
            GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag("Resource");

            // Loop over the map; Width first
            for (int width = 32; width < (int)MapSize.x; width += 64)
            {
                // Then height
                for (int height = 32; height < (int)MapSize.y; height += 64)
                {
                    // We are in the fourth quadrant so the y is negative
                    Vector3 key = new Vector3(width, height * -1, -1.0f);

                    // Create an empty Tile at the given position
                    Tile newTile = new Tile(key, ResourceType.None, null);

                    // Add the Tile to the dictionary
                    TileDictionary.AddEntry(key, newTile);
                } // end inner for
            } // end outer for

            // Check if the game is new
            if (GameMaster.Instance.IsNew)
            {
                // Now loop over the resourceObjects array and set the Tiles to resources
                for (int index = 0; index < resourceObjects.Length; index++)
                {
                    // Get the position of the resource
                    Vector3 key = ToPixels(resourceObjects[index].transform.position);

                    // Add the position to the list
                    TileDictionary.ResourcePositions.Add(key);

                    // Holds the Tile's ResourceType
                    ResourceType resourceType = resourceObjects[index].GetComponent<ResourceTile>().Type;

                    // Update the tile at the given key
                    TileDictionary.UpdateTile(key, resourceType, resourceObjects[index]);
                } // end for
            } // end if
            else
            {
                // Otherwise the game isn't new so load the resource positions
                GameMaster.Instance.LoadResources();

                // Get the list's length
                int length = resourceObjects.Length;

                // Now loop over the resourceObjects array and set the Tiles to resources
                for (int index = 0; index < length; index++)
                {
                    // Get the position of the resource
                    Vector3 key = ToPixels(resourceObjects[index].transform.position);

                    // Check if the key is in the list
                    if (TileDictionary.ResourcePositions.Contains(key))
                    {
                        // Holds the Tile's ResourceType
                        ResourceType resourceType = resourceObjects[index].GetComponent<ResourceTile>().Type;

                        // Update the tile at the given key
                        TileDictionary.UpdateTile(key, resourceType, resourceObjects[index]);
                    } // end if
                    else
                    {
                        // Otherwise, we want to get rid of the resource object
                        GameObject.Destroy(resourceObjects[index]);
                    } // end else
                } // end for
            } // end else
		} // end GenerateAndAddTiles

		// Converts unity units to pixels for use on the map
		public static Vector3 ToPixels(Vector3 param)
		{
			// To convert the parameter to pixels that the resource positions use, multiply by 100
            Vector3 tmp = new Vector3(param.x * 100, param.y * 100, param.z * 100);

			// Check if the width (x) is within the valid map positions and Clamp if not
            if (tmp.x > MaxWidth)
			{
				tmp.x = MaxWidth;
			} // end if

			// Check if the height (y) is within the valid map positions and Clamp if not
            if (tmp.y < MaxHeight)
			{
				tmp.y = MaxHeight;
			} // end if

			// We need integers for the keys to work. So convert the temp vector3's params to integers
			// NOTE: Trying to use "(int)tmp.x" results in the wrong number. So use "Convert.ToInt32(tmp.x)"
            int resX = Convert.ToInt32(tmp.x);
            int resY = Convert.ToInt32(tmp.y);
            int resZ = Convert.ToInt32(tmp.z);

			// Everything should be fine now so return the result
            return new Vector3(resX, resY, resZ);
		} // end ToPixels

		// Converts pixels to unity units for use on the map
		public static Vector3 ToUnits(Vector3 param)
		{
			// To convert the parameter to units that unity positions use, divide by 100
            Vector3 tmp = new Vector3(param.x / 100.0f, param.y / 100.0f, param.z / 100.0f);

			// Gets the max width and height in units
            float maxWidth = MaxWidth / 100.0f;
            float maxHeight = MaxHeight / 100.0f;
			
			// Check if the width (x) is within the valid map positions and Clamp if not
            if (tmp.x > maxWidth)
			{
				tmp.x = maxWidth;
			} // end if
			
			// Check if the height (y) is within the valid map positions and Clamp if not
            if (tmp.y < maxHeight)
			{
				tmp.y = maxHeight;
			} // end if
			
			// Everything should be fine now so return the result
			return tmp;
		} // end ToUnits

        // Gets the tile's Size
        public static int TileSize
        {
            get { return tileSize; }
        } // end TileSize

        // Gets the number of tiles in width
        public static int NumTilesWide
        {
            get { return numTilesWide; }
        } // end NumTilesWide

        // Gets the number of tiles in width
        public static int NumTilesHigh
        {
            get { return numTilesHigh; }
        } // end NumTilesWide

        // Gets the max height you can place a tile
        public static int MaxHeight
        {
            get
            {
                // Calculate the height of the map; Then subtract half the tile size
                // This is the highest coord the tile can be placed at; This is because we start at 32
                int temp = NumTilesHigh * TileSize - (TileSize / 2);

                // Since we're in the fourth quadrant, the y value is negative
                return (temp * -1);
            } // end get
        } // end MaxHeight

        // Gets the max width you can place a tile
        public static int MaxWidth
        {
            get
            {
                // Calculate the width of the map; Then subtract half the tile size
                // This is the highest coord the tile can be placed at; This is because we start at 32.
                return (NumTilesWide * TileSize - (TileSize / 2));
            } // end get
        } // end MaxHeight

        // Gets the min height you can place a tile
        public static int MinHeight
        {
            get { return (TileSize / 2) * -1; }
        } // end MinWidth

        // Gets the min width you can place a tile
        public static int MinWidth
        {
            get { return TileSize / 2; }
        } // end MaxWidth

        // Gets the min height you can place a tile in units
        public static float MinHeightUnits
        {
            get { return ToUnits(new Vector3(MinHeight, 0, 0)).x; }
        } // end MinHeightUnits

        // Gets the min width you can place a tile in units
        public static float MinWidthUnits
        {
            get { return ToUnits(new Vector3(MinWidth, 0, 0)).x; }
        } // end MinWidthUnits

        // Gets the max height you can place a tile in units
        public static float MaxHeightUnits
        {
            get { return ToUnits(new Vector3(MaxHeight, 0, 0)).x; }
        } // end MaxHeightUnits

        // Gets the max width you can place a tile in units
        public static float MaxWidthUnits
        {
            get { return ToUnits(new Vector3(MaxWidth, 0, 0)).x; }
        } // end MaxWidthUnits

        // Gets the map's size
        public static Vector2 MapSize
        {
            get { return new Vector2(NumTilesWide * TileSize, NumTilesHigh * TileSize); }
        } // end MapSize

        // Gets the standard move distance for a player
        public static float PlayerMoveDistance
        {
            get
            {
                // Originally ToUnits is a Vector3 operation. So we pass in zeroes for y and z here.
                // All we care about is the x component.
                return ToUnits(new Vector3(TileSize, 0, 0)).x;
            } // end get
        } // end PlayerMoveDistance
	} // end TileManager
} // end GSP.Tiles
