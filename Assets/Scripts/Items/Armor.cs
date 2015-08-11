/*******************************************************************************
 *
 *  File Name: Armor.cs
 *
 *  Description: The logic of the armour items
 *
 *******************************************************************************/

namespace GSP.Items
{
    /*******************************************************************************
     *
     * Name: Armor
     * 
     * Description: The logic of the armour type items.
     * 
     *******************************************************************************/
    public class Armor : Equipment
    {
        // Create a piece of armour
        public Armor(string itemName, ArmorType itemType, int defence, int cost) :
            base(itemName, itemType.ToString(), 0, defence, 0, 0, cost)
        { 
            // Leave empty
        } // end Armors constructor
    } // end Armnor
} // end GSP.Items
