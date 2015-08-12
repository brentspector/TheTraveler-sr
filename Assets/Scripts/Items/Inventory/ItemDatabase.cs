using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Core;

namespace GSP.Items.Inventory
{
    public class ItemDatabase : BaseSingleton<ItemDatabase>
    {
        List<Item> items;   // The list of items

        // Initialise the list and other values
        public override void Awake()
        {
            // Call Awake on the parent first
            base.Awake();

            // Initialise our list
            items = new List<Item>();
        } // end Awake

        // Use this for initialisation
        void Start()
        {
            // Add the items to the list
            items.Add(new Weapon("Broadsword", WeaponType.Broadsword, null, 9, 1));
            items.Add(new Weapon("Mace", WeaponType.Mace, null, 7, 1));
            items.Add(new Weapon("Spear", WeaponType.Spear, null, 8, 1));
            items.Add(new Weapon("Sword", WeaponType.Sword, null, 5, 1));
            items.Add(new Armor("Chainlegs", ArmorType.Chainlegs, null, 2, 1));
            items.Add(new Armor("Chainmail", ArmorType.Chainmail, null, 5, 1));
            items.Add(new Armor("Fullsuit", ArmorType.Fullsuit, null, 11, 1));
            items.Add(new Armor("Platebody", ArmorType.Platebody, null, 8, 1));
            items.Add(new Armor("Platelegs", ArmorType.Platelegs, null, 3, 1));
            items.Add(new Bonus("RubberBoots", BonusType.RubberBoots, null, 0, 10, 1));
            items.Add(new Bonus("Sachel", BonusType.Sachel, null, 3, 0, 1));
            items.Add(new Resource("Fish", ResourceType.Fish, null, 25, 5, 15));
            items.Add(new Resource("Ore", ResourceType.Ore, null, 20, 5, 10));
            items.Add(new Resource("Wood", ResourceType.Wood, null, 15, 5, 20));
            items.Add(new Resource("Wool", ResourceType.Wool, null, 10, 5, 15));
        } // end Start

        public List<Item> Items
        {
            get
            { 
                // Create a copy of the list
                List<Item> tempItems = new List<Item>(items);

                // Return the copy
                return tempItems;
            } // end get
        } // end Items
    } // end ItemDatabase
} // end GSP.Items.Inventory
