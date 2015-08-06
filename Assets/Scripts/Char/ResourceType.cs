/*******************************************************************************
 *
 *  File Name: ResourceType.cs
 *
 *  Description: The enumeration for the types of resources
 *
 *******************************************************************************/
using UnityEngine;
using System.Collections;

namespace GSP.Char
{
    // Resource enumeration
    // NOTE!!
    // SIZE must be the last item in the enum so that anything based
    // on the length of the enum can be used as normal. It is best to
    // add items to the left of None but after the current 3rd to last
    // item in the enum. For instance if the list was {Wool, Fish, None, Size}
    // you should enter the new item between Fish and None. Create name
    // here and then define it under "SetResource" function.
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
} // end GSP.Char