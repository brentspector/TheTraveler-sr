/*******************************************************************************
 *
 *  File Name: ItemDatabase.cs
 *
 *  Description: Contains all the items available in the game.
 *
 *******************************************************************************/
using GSP.Core;
using System.Collections.Generic;

namespace GSP.Items.Inventories
{
    /*******************************************************************************
     *
     * Name: ItemDatabase
     * 
     * Description: A singleton storage class. It holds a list of all the available
     *              items in the game. Used in conjunction with the Inventory
     *              system.
     * 
     *******************************************************************************/
    public class ItemDatabase : BaseSingleton<ItemDatabase>
    {
        List<Item> items;   // The list of items

        // Initialise the list and other values
        public override void Awake()
        {
            // Call Awake on the parent first
            base.Awake();

            // Set the object's name
            gameObject.name = "ItemDatabase";

            // Initialise our list
            items = new List<Item>();
        } // end Awake

        // Use this for initialisation
        void Start()
        {
            // Add the items to the list
            items.Add(new EmptyItem());
            items.Add(new Weapon("Broadsword", WeaponType.Broadsword, SpriteReference.spriteBroadsword, 9, 1));
            items.Add(new Weapon("Mace", WeaponType.Mace, SpriteReference.spriteMace, 7, 1));
            items.Add(new Weapon("Spear", WeaponType.Spear, SpriteReference.spriteSpear, 8, 1));
            items.Add(new Weapon("Sword", WeaponType.Sword, SpriteReference.spriteSword, 5, 1));
            items.Add(new Armor("Chainlegs", ArmorType.Chainlegs, SpriteReference.spriteChainlegs, 2, 1));
            items.Add(new Armor("Chainmail", ArmorType.Chainmail, SpriteReference.spriteChainmail, 5, 1));
            items.Add(new Armor("Fullsuit", ArmorType.Fullsuit, SpriteReference.spriteFullsuit, 11, 1));
            items.Add(new Armor("Platebody", ArmorType.Platebody, SpriteReference.spritePlatebody, 8, 1));
            items.Add(new Armor("Platelegs", ArmorType.Platelegs, SpriteReference.spritePlatelegs, 3, 1));
            items.Add(new Bonus("Rubber Boots", BonusType.RubberBoots, SpriteReference.spriteRubberBoots, 0, 10, 1));
            items.Add(new Bonus("Sachel", BonusType.Sachel, SpriteReference.spriteSachel, 3, 0, 1));
            items.Add(new Resource("Fish", ResourceType.Fish, SpriteReference.spriteFishResource, 25, 5, 15));
            items.Add(new Resource("Ore", ResourceType.Ore, SpriteReference.spriteOreResource, 20, 5, 10));
            items.Add(new Resource("Wood", ResourceType.Wood, SpriteReference.spriteWoodResource, 15, 5, 20));
            items.Add(new Resource("Wool", ResourceType.Wool, SpriteReference.spriteWoolResource, 10, 5, 15));
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
