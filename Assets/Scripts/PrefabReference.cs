/*******************************************************************************
 *
 *  File Name: PrefabReference.cs
 *
 *  Description: Loads and deals with the prefabs
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: PrefabReference
     * 
     * Description: Manages the references to any prefabs loaded from Resources.
     * 
     *******************************************************************************/
    public static class PrefabReference
	{
		/*
		 * Object Prefabs
		 */

        // This is the reference to the player prefab
        public static GameObject prefabPlayer = Resources.Load("Player") as GameObject;

        // This is the reference to the enemy prefab
        public static GameObject prefabEnemy = Resources.Load("Enemy") as GameObject;

        // This is the reference to the ally prefab
        public static GameObject prefabAlly = Resources.Load("Ally") as GameObject;

		// This is the reference to the ore resource prefab
        public static GameObject prefabResource_Ore = Resources.Load("Resource_Ore") as GameObject;

		// This is the reference to the wood resource prefab
        public static GameObject prefabResource_Wood = Resources.Load("Resource_Wood") as GameObject;

		// This is the reference to the wool resource prefab
        public static GameObject prefabResource_Wool = Resources.Load("Resource_Wool") as GameObject;

		// This is the reference to the fish resource prefab
        public static GameObject prefabResource_Fish = Resources.Load("Resource_Fish") as GameObject;

		// This is the reference to the highlight prefab
        public static GameObject prefabHighlight = Resources.Load("Highlight") as GameObject;

        // This is the reference to the inventory slot prefab
        public static GameObject prefabInventorySlot = Resources.Load("Inventory/InventorySlot") as GameObject;

	} // end PrefabReference
} // end GSP

