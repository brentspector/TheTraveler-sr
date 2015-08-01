
namespace GSP.Items
{
    public class Weapon : Item
    {
        // Create a weapon
        public Weapon(string name, WeaponType type, int attack, int cost) :
            base(name, type.ToString(), attack, 0, 0, 0, cost)
        {
            // Leave empty
        } // end Weapon constructor
    } // end Weapon
} // end GSP.Items
