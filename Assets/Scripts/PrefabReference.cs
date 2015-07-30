using UnityEngine;
using System.Collections;

namespace GSP
{
	public static class PrefabReference
	{
		/*
		 * Object Prefabs.
		 */

		// This is the reference to the character prefab.
		public static GameObject prefabCharacter = Resources.Load( "Character" ) as GameObject;

        // This is the reference to the player prefab.
        public static GameObject prefabPlayer = Resources.Load("Player") as GameObject;

        // This is the reference to the enemy prefab.
        public static GameObject prefabEnemy = Resources.Load("Enemy") as GameObject;

        // This is the reference to the ally prefab.
        public static GameObject prefabAlly = Resources.Load("Ally") as GameObject;

		// This is the reference to the dice button prefab.
		public static GameObject prefabDiceButton = Resources.Load( "DiceButton" ) as GameObject;

		// This is the reference to the ore resource prefab.
		public static GameObject prefabResource_Ore = Resources.Load( "Resource_Ore" ) as GameObject;

		// This is the reference to the wood resource prefab.
		public static GameObject prefabResource_Wood = Resources.Load( "Resource_Wood" ) as GameObject;

		// This is the reference to the wool resource prefab.
		public static GameObject prefabResource_Wool = Resources.Load( "Resource_Wool" ) as GameObject;

		// This is the reference to the fish resource prefab.
		public static GameObject prefabResource_Fish = Resources.Load( "Resource_Fish" ) as GameObject;

		// This is the reference to the item prefab.
		public static GameObject prefabItem = Resources.Load ("Item") as GameObject;

		// This is the reference to the item prefab.
		public static GameObject prefabHighlight = Resources.Load ("Highlight") as GameObject;
	} // end PrefabReference class
} // end namespace

