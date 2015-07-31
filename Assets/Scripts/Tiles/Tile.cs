/*******************************************************************************
 *
 *  File Name: Tile.cs
 *
 *  Description: Simulates tiles on the map
 *
 *******************************************************************************/
using GSP.Char;
using UnityEngine;

namespace GSP.Tiles
{
    /*******************************************************************************
     *
     * Name: Tile
     * 
     * Description: Used for the tiles on the map. These aren't real tiles, just
     *              fake ones.
     * 
     *******************************************************************************/
    public class Tile
	{
		Vector3 position;			// The position on the map the tile is on.
		ResourceType resourceType;  // The ResourceType the resource on the map is.
		GameObject resourceObj;		// The resourceObj GameObject.

		// Default Constructor used for creating an empty Tile object
		public Tile()
		{
			// Initialise the variables to a default state
			position = Vector3.zero;
			resourceType = ResourceType.None;
			resourceObj = null;
		} // end Tile

		// Constructor used for creating a specific type of Tile object
		public Tile(Vector3 location, ResourceType type, GameObject resource)
		{
			// Initialise the variables to the given values
			position = location;
			resourceType = type;
            resourceObj = resource;
		} // end Tile

		// Updates the tiles ResourceType and GameObject
		public void UpdateTile(ResourceType type, GameObject resource)
		{
			// Update the variables
			resourceType = type;
            resourceObj = resource;
		} // end UpdateTile

        // Gets the Tile's Position
        public Vector3 Position
        {
            get { return position; }
        } // end Position

        // Gets the Tile's ResourceType
        public ResourceType ResourceType
        {
            get { return resourceType; }
        } // end ResourceType

        // Gets the Tile's esource GameObject
        public GameObject Resource
        {
            get { return resourceObj; }
        } // end Resource
	} // end Tile
} // end GSP.Tiles
