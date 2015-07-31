/*******************************************************************************
 *
 *  File Name: FacingDirection.cs
 *
 *  Description: The enumeration describing what direction the character is
 *               facing
 *
 *******************************************************************************/
using UnityEngine;
using System.Collections;

namespace GSP.Char
{
    // The enumeration of the directions a character can be facing.
    // This is used for displaying the correct sprite.
    public enum FacingDirection
    {
        North,
        East,
        South,
        West
    };
}
