/*******************************************************************************
 *
 *  File Name: BonusType.cs
 *
 *  Description: The enumeration for the the various bonus types
 *
 *******************************************************************************/

namespace GSP.Items
{
    // The types of bonuses the game uses.
    // NOTE!!
    // Size must be the last item in the enum so that anything based the length of
    // the enum can be used as normal. It is best to add items to the left of Size
    // but after the current second-to-last item in the enum. For instance if the
    // list was {Sachel, RubberBoots, Size}; you should enter the new item between
    // RubberBoots and Size. Create name here and then define it under "SetItem"
    // function of the Item class.
    public enum BonusType
    {
        Sachel,
        RubberBoots,
        Size
    } // end BonusType
} // end GSP.Items