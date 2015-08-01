
namespace GSP.Items
{
    public class Bonus : Item
    {
        // Create a bonus item
        public Bonus(string name, BonusType type, int space, int weight, int cost) :
            base(name, type.ToString(), 0, 0, space, weight, cost)
        {
            // Leave empty
        } // end Bonus constructor
    } // end Bonus
} // end GSP.Items
