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
using X_UniTMX;
using System.Collections.Generic;

namespace GSP.Tiles
{
    /*******************************************************************************
     *
     * Name: TileManager
     * 
     * Description: Manages all the tiles in the game.
     * 
     *******************************************************************************/
    public class TileManager : MonoBehaviour
	{
        Map tiledMap;                       // The map that was loaded for the game
        List<Vector2> resourcePositions;    // List of positions of resources on the map

        public TextAsset mapFile;           // The map file the TileManager is managing, set through the editor
        public Material defaultMaterial;    // The default material for the sprites, set through the editor

    	// Used for initialisation
        void Awake()
        {
            // Initialise the resource positions list
            resourcePositions = new List<Vector2>();
        } // end Awake
        
        // Sets the dimensions of the Tiled map; This should only be called once when initialising the map
		void SetDimensions(int size, int tilesWide, int tilesHigh)
		{
            // Update the map values of the TileUtils class
            TileUtils.Update(size, tilesWide, tilesHigh);
		} // end SetDimensions

		// Generates a map for the game's level.
		public void GenerateMap()
		{
            // Create a new map and supply a callback for when the map is loaded.
            new Map(mapFile, "", OnMapLoaded);

            //// Get all the game objects tagged as resources
            //GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag("Resource");

            //// Loop over the map; Width first
            //for (int width = 32; width < (int)MapSize.x; width += 64)
            //{
            //    // Then height
            //    for (int height = 32; height < (int)MapSize.y; height += 64)
            //    {
            //        // We are in the fourth quadrant so the y is negative
            //        Vector3 key = new Vector3(width, height * -1, -1.0f);

            //        // Create an empty Tile at the given position
            //        Tile newTile = new Tile(key, ResourceType.None, null);

            //        // Add the Tile to the dictionary
            //        TileDictionary.AddEntry(key, newTile);
            //    } // end inner for
            //} // end outer for

            //// Check if the game is new
            //if (GameMaster.Instance.IsNew)
            //{
            //    // Now loop over the resourceObjects array and set the Tiles to resources
            //    for (int index = 0; index < resourceObjects.Length; index++)
            //    {
            //        // Get the position of the resource
            //        Vector3 key = ToPixels(resourceObjects[index].transform.position);

            //        // Add the position to the list
            //        TileDictionary.ResourcePositions.Add(key);

            //        // Holds the Tile's ResourceType
            //        ResourceType resourceType = resourceObjects[index].GetComponent<ResourceTile>().Type;

            //        // Update the tile at the given key
            //        TileDictionary.UpdateTile(key, resourceType, resourceObjects[index]);
            //    } // end for
            //} // end if
            //else
            //{
            //    // Otherwise the game isn't new so load the resource positions
            //    GameMaster.Instance.LoadResources();

            //    // Get the list's length
            //    int length = resourceObjects.Length;

            //    // Now loop over the resourceObjects array and set the Tiles to resources
            //    for (int index = 0; index < length; index++)
            //    {
            //        // Get the position of the resource
            //        Vector3 key = ToPixels(resourceObjects[index].transform.position);

            //        // Check if the key is in the list
            //        if (TileDictionary.ResourcePositions.Contains(key))
            //        {
            //            // Holds the Tile's ResourceType
            //            ResourceType resourceType = resourceObjects[index].GetComponent<ResourceTile>().Type;

            //            // Update the tile at the given key
            //            TileDictionary.UpdateTile(key, resourceType, resourceObjects[index]);
            //        } // end if
            //        else
            //        {
            //            // Otherwise, we want to get rid of the resource object
            //            GameObject.Destroy(resourceObjects[index]);
            //        } // end else
            //    } // end for
            //} // end else
		} // end GenerateAndAddTiles

        // Callback for when the map has been loaded.
        void OnMapLoaded(Map map)
        {
            // Set the loaded map
            tiledMap = map;

            /*
             * Generate the map with the following options:
             * 
             * TileManager's GameObject is the parent in the hierarchy
             * The default material is the default sprite's material
             */
            tiledMap.Generate(this.gameObject, defaultMaterial);

            // Generate any tile collisions
            tiledMap.GenerateTileCollisions();

            MapObjectLayer resourcesObjectLayer;    // The object layer for resources

            // Check if the map has a resources object layer
            if ((resourcesObjectLayer = tiledMap.GetObjectLayer("Resources")) != null)
            {
                // Get the list of MapObjects on the resources layer
                List<MapObject> resourceObjects = resourcesObjectLayer.Objects;

                // Check if the game is new
                if (GameMaster.Instance.IsNew)
                {
                    // The game is new so add all the resources to the list
                    foreach (var resource in resourceObjects)
                    {
                        resourcePositions.Add(new Vector2(resource.Bounds.x, resource.Bounds.y));
                    } // end foreach

                    // Finally, save the resource positions list
                    GameMaster.Instance.SaveResources();
                } // end if
                else
                {
                    // The game isn't new so load all the saved resources
                    resourcePositions = GameMaster.Instance.LoadResources();
                } // end else
                
                // Generate the colliders from this layer
                tiledMap.GenerateCollidersFromLayer("Resources");

                // Loop through the objects to check if they're on the resources list
                foreach (var resource in resourceObjects)
                {
                    // Make sure the resource is in the list
                    if (resourcePositions.Contains(new Vector2(resource.Bounds.x, resource.Bounds.y)))
                    {
                        // Generate the prefab for the resource
                        tiledMap.GeneratePrefab(resource, Vector2.up, null, false, true);
                    } // end if
                } // end foreach
            } // end if

            // Check if the map has a markets object layer
            if (tiledMap.GetObjectLayer("Markets") != null)
            {
                // Generate the colliders from this layer
                tiledMap.GenerateCollidersFromLayer("Markets");
                // Generate the prefabs from this layer
                tiledMap.GeneratePrefabsFromLayer("Markets", Vector2.up, false, true);
            } // end if

            // Finally, Update the TileUtils static class
            SetDimensions(tiledMap.MapRenderParameter.TileHeight, tiledMap.MapRenderParameter.Width,
                tiledMap.MapRenderParameter.Height);
        } // end OnMapLoaded

        // Gets the resource's positions
        public List<Vector2> ResourcePositions
        {
            get
            {
                // Create a temp list based upon the resource positions
                List<Vector2> tempPositions = resourcePositions;

                // Return the temp list
                return tempPositions;
            } // end get
        } // end ResourcePositions
	} // end TileManager
} // end GSP.Tiles
