﻿/*******************************************************************************
 *
 *  File Name: TileImporter.cs
 *
 *  Description: Custom importer for the Tiled2Unity library
 *
 *******************************************************************************/
using GSP.Items;
using System;
using System.Collections.Generic;
using Tiled2Unity;
using UnityEngine;

/*
 * This file must remain in an Editor folder to work
 */

namespace GSP.Tiles
{
    /*******************************************************************************
     *
     * Name: TileImporter
     * 
     * Description: Imports the tiles from Tiled2Unity.
     * 
     *******************************************************************************/
    [CustomTiledImporter]
	class TileImporter : ICustomTiledImporter
	{
		// Handle the custom properties
		public void HandleCustomProperties (GameObject gameObject, IDictionary<string, string> customProperties)
		{
			// Does this game object have a ResType property?
            if (!customProperties.ContainsKey("ResType"))
			{
				// Simply return
				return;
			} // end if

			// Now we meed to determine which type of resource it is
			ResourceType tmp; 			// The results of parsing

			GameObject instance = null;	// The GameObject instance

			try
			{
				// Attempt to parse the string into the enum value
                tmp = (ResourceType)Enum.Parse(typeof(ResourceType), customProperties["ResType"]);

				// Switch over the possible values
				switch (tmp)
				{
					case ResourceType.Wool:
					    {
						    // Instantiate the resource prefab
                            instance = GameObject.Instantiate(PrefabReference.prefabResource_Wool) as GameObject;
						
						    // Set the instances name to the name of the prefab
                            instance.name = PrefabReference.prefabResource_Wool.name;
						
						    // Add the ResourceTile component to the instance
						    ResourceTile resTile = instance.AddComponent<ResourceTile>();
					
						    // Add the resource type to the script component
						    resTile.Type = ResourceType.Wool;
						    break;
					    } // end case Wool
					case ResourceType.Wood:
					    {
						    // Instantiate the resource prefab
                            instance = GameObject.Instantiate(PrefabReference.prefabResource_Wood) as GameObject;	
						
						    // Set the instances name to the name of the prefab
						    instance.name = PrefabReference.prefabResource_Wood.name;
					
						    // Add the ResourceTile component to the instance
						    ResourceTile resTile = instance.AddComponent<ResourceTile>();
						
						    // Add the resource type to the script component
                            resTile.Type = ResourceType.Wood;
						    break;
					    } // end case Wood
					case ResourceType.Fish:
					{
						// Instantiate the resource prefab
                        instance = GameObject.Instantiate(PrefabReference.prefabResource_Fish) as GameObject;
						
						// Set the instances name to the name of the prefab
						instance.name = PrefabReference.prefabResource_Fish.name;
					
						// Add the ResourceTile component to the instance
						ResourceTile resTile = instance.AddComponent<ResourceTile>();
						
						// Add the resource type to the script component
                        resTile.Type = ResourceType.Fish;
						break;
					} // end case Fish
					case ResourceType.Ore:
					{
						// Instantiate the resource prefab.
                        instance = GameObject.Instantiate(PrefabReference.prefabResource_Ore) as GameObject;
						
						// Set the instances name to the name of the prefab
						instance.name = PrefabReference.prefabResource_Ore.name;
					
						// Add the ResourceTile component to the instance
						ResourceTile resTile = instance.AddComponent<ResourceTile>();
						
						// Add the resource type to the script component
						resTile.Type = ResourceType.Ore;
						break;
					} // end case Ore
					default:
					{
						// Couldn't parse correctly so set the instance to null
						instance = null;
						break;
					} // end default case
                } // end switch tmp
			} // end try
			catch (Exception ex)
			{
				// The parsing failed so set the instance to null
				Debug.LogErrorFormat("Something went wrong. Exception: {0}", ex.Message);
				instance = null;
			} // end catch

			// Make sure instance exists
            if (instance == null)
			{
				// Simply return.
				return;
			} // end if

			// Tag the instance as a Resource
			instance.tag = "Resource";

			// Use the position of the game object we're attached to
            instance.transform.parent = gameObject.transform;
            instance.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);

			// Scale by a factor of 100. For some reason they're 1/100th the size and this make them big enough to be visible
            instance.transform.localScale = new Vector3(100.0f, 100.0f, 0.0f);

			// Add to the parent transform's local position. This corrects the placement
            instance.transform.parent.localPosition += new Vector3(32.0f, 32.0f, 0.0f);
		} // end HandleCustomProperties

		// Customise the prefab, this is the last chance to do this in code
		public void CustomizePrefab (GameObject prefab)
		{
			// Add an empty game object to the prefab
			// This is to compensate for tiled2unity not supporting image layers
            GameObject obj = new GameObject("MapBackgroundImage");

			// Add a sprite renderer component to the game object; This is for the background image of the map
			obj.AddComponent<SpriteRenderer>();

			// Set the prefab as the game object's parent
			obj.transform.parent = prefab.transform;

			// Set the object at 640, -512
            obj.transform.localPosition = new Vector3(640.0f, -512.0f, 1.0f);

			// Set the object's scale to 100
            obj.transform.localScale = new Vector3(100.0f, 100.0f, 0.0f);

            var collisionTransform = prefab.transform.FindChild("Foreground_00/Collision");

            collisionTransform.gameObject.layer = 8;
		} // end CustomizePrefab
	} // end TileImporter
} // end GSP.Tiles