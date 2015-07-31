/*******************************************************************************
 *
 *  File Name: Movement.cs
 *
 *  Description: Describes the movement amounts allowed in Vector3 format
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: Movement
     * 
     * Description: Responsible for calculating the movement amounts.
     * 
     *******************************************************************************/
    public class Movement : MonoBehaviour
    {
        // Calculates the left movement amount
        public Vector3 MoveLeft(Vector3 position)
        {
            // Make sure we're not at the edge of the map
            if (position.x > GSP.Tiles.TileManager.MinWidthUnits)
            {
                return new Vector3(-GSP.Tiles.TileManager.PlayerMoveDistance, 0, 0);
            } // end if
            else
            {
                // Otherwise, return a zero vector
                return Vector3.zero;
            } // end else
        } // end MoveLeft

        // Calculates the right movement amount
        public Vector3 MoveRight(Vector3 position)
        {
            // Make sure we're not at the edge of the map
            if (position.x < GSP.Tiles.TileManager.MaxWidthUnits)
            {
                return new Vector3(GSP.Tiles.TileManager.PlayerMoveDistance, 0, 0);
            } // end if
            else
            {
                // Otherwise, return a zero vector
                return Vector3.zero;
            } // end else
        } // end MoveRight

        // Calculates the up movement amount
        public Vector3 MoveUp(Vector3 position)
        {
            // Make sure we're not at the edge of the map
            if (position.y < GSP.Tiles.TileManager.MinHeightUnits)
            {
                return new Vector3(0, GSP.Tiles.TileManager.PlayerMoveDistance, 0);
            } // end if
            else
            {
                // Otherwise, return a zero vector
                return Vector3.zero;
            } // end else
        } // end MoveUp

        // Calculates the down movement amount
        public Vector3 MoveDown(Vector3 position)
        {
            // Make sure we're not at the edge of the map
            if (position.y > GSP.Tiles.TileManager.MaxHeightUnits)
            {
                return new Vector3(0, -GSP.Tiles.TileManager.PlayerMoveDistance, 0);
            } // end if
            else
            {
                // Otherwise, return a zero vector
                return Vector3.zero;
            } // end else
        } // end MoveDown
    } // end Movement
} // end GSP