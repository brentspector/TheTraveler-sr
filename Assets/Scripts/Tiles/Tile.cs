using UnityEngine;
using System.Collections;
using GSP.Char;

namespace GSP.Tiles
{
	// Used for the tiles on the map. These aren't real tiles, just fake ones.
	public class Tile
	{
		// Declare our private variables.
		Vector3 m_position;				// The position on the map the tile is on.
		ResourceType m_resourceType;	// The resource type the resource on the map is.
		GameObject m_resource;			// The resource game object.

		// Gets the tile's position as a vector 3.
		public Vector3 Position
		{
			get { return m_position; }
		} // end Position property

		// Gets the tile's resource type.
		public ResourceType ResourceType
		{
			get { return m_resourceType; }
		} // end ResourceType property

		// Gets the tile's resource game object.
		public GameObject Resource
		{
			get { return m_resource; }
		} // end Resource property


		// Default Constructor
		public Tile()
		{
			// Initialise the variables to a default state.
			m_position = new Vector3( 0.0f, 0.0f, 0.0f );
			m_resourceType = ResourceType.NONE;
			m_resource = null;
		} // end default constructor function

		// Constructor
		public Tile( Vector3 position, ResourceType resourceType, GameObject resource )
		{
			// Initialise the variables to the given values.
			m_position = position;
			m_resourceType = resourceType;
			m_resource = resource;
		} // end contstructor function

		// Updates the tiles resource type and object.
		public void UpdateTile( ResourceType resourceType, GameObject resource )
		{
			// Update the variables.
			m_resourceType = resourceType;
			m_resource = resource;
		} // end UpdateTile

	} // end Tile class
} // end namespace
