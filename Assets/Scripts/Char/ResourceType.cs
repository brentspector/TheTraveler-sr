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
    // add items to the left of NONE but after the current 3rd to last
    // item in the enum. For instance if the list was {SWORD, MACE, NONE, SIZE}
    // you should enter the new item between MACE and NONE. Create name
    // here and then define it under "SetResource" function.
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