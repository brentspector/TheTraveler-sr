/*******************************************************************************
 *
 *  File Name: Weapon.cs
 *
 *  Description: The logic of the weapon items
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: Weapon
     * 
     * Description: The logic of the weapon type items.
     * 
     *******************************************************************************/
    public class Weapon : Equipment
    {
        // Create a weapon
        public Weapon(string itemName, WeaponType itemType, Sprite itemIcon, int attack, int cost) :
            base(itemName, itemType.ToString(), itemIcon, attack, 0, 0, 0, cost)
        {
            // Leave empty
        } // end Weapon constructor
    } // end Weapon
} // end GSP.Items
