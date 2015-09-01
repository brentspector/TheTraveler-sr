﻿/*******************************************************************************
 *
 *  File Name: Armor.cs
 *
 *  Description: The logic of the bonus items such as weight and inventory
 *               mods
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: Bonus
     * 
     * Description: Contains the logic for the bonus type of items such as
     *              inventory and weight mods.
     * 
     *******************************************************************************/
    public class Bonus : Equipment
    {
        // Create a bonus item
        public Bonus(string itemName, BonusType itemType, Sprite itemIcon, int space, int weight, int cost) :
            base(itemName, itemType.ToString(), itemIcon, 0, 0, space, weight, cost)
        {
            // Leave empty
        } // end Bonus constructor
    } // end Bonus
} // end GSP.Items
