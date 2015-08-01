
namespace GSP.Items
{
    public class Armor : Item
    {
        // Create a piece of armour
        public Armor(string name, ArmorType type, int defence, int cost) :
            base(name, type.ToString(), 0, defence, 0, 0, cost)
        { 
            // Leave empty
        } // end Armors constructor
    } // end Armnor
} // end GSP.Items
