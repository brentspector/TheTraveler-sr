/*******************************************************************************
 *
 *  File Name: ResourceType.cs
 *
 *  Description: The enumeration for the types of resources
 *
 *******************************************************************************/

namespace GSP.Items
{
    // Resource enumeration
    // NOTE!!
    // SIZE must be the last item in the enum so that anything based
    // on the length of the enum can be used as normal. It is best to
    // add items to the left of None but after the current 3rd to last
    // item in the enum. For instance if the list was {Wool, Fish, None, Size}
    // you should enter the new item between Fish and None.
    // NOTE: DON'T EDIT THIS LIST BECAUSE IT SCREWS UP THE RESOURCES ON THE MAP :P
    public enum ResourceType
    {
        Wool,
        Wood,
        Fish,
        Ore,
        None,
        Size
    }; // end ResourceType
} // end GSP.Items