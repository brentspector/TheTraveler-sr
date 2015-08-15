/*******************************************************************************
 *
 *  File Name: GUIMovement.cs
 *
 *  Description: Old GUI for the movement
 *
 *******************************************************************************/
using GSP.Char;
using UnityEngine;
using UnityEngine.UI;

namespace GSP
{
    //TODO: Brent: Replace this with the new In-Game UI later; probably not in the same namespace
    /*******************************************************************************
     *
     * Name: GUIMovement
     * 
     * Description: Creates the GUI for the player's movement.
     * 
     *******************************************************************************/
    public class GUIMovement : MonoBehaviour
    {
        Player playerScript;        // This is the current player's Player script
		Vector3 displacementVector; // value player moves relative to Space.World
		Vector3 origPlayerPosition; // If player cancels movement, player resets to this original poisition
		int initialTravelDist;      // Initial dice roll
		int currTravelDist;         // Dice roll left
		bool isMoving = false;      // If the player is moving, one dice roll value is taken off
		Button upButton;		
		Button downButton;
		Button leftButton;
		Button rightButton;

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Initialise things sort of like a custom constructor
		public void InitThis(Player player, int travelDistance)
		{
			// Get HUD Arrows
			upButton = GameObject.Find ("UpButton").GetComponent<Button> ();
			downButton = GameObject.Find ("DownButton").GetComponent<Button> ();
			leftButton = GameObject.Find ("LeftButton").GetComponent<Button> ();
			rightButton = GameObject.Find ("RightButton").GetComponent<Button> ();

			// Make sure they are enabled
			upButton.interactable = true;
			downButton.interactable = true;
			leftButton.interactable = true;
			rightButton.interactable = true;

			// Player script
            playerScript = player;

			// Store original position
			origPlayerPosition = player.transform.position;

			// How much can the player move
			initialTravelDist = travelDistance;
			currTravelDist = initialTravelDist;

			// Initialize displacement vector
			displacementVector = Vector3.zero;

			// Generate the initial set of highlight objects
            Highlight.GenerateHighlight(origPlayerPosition, currTravelDist);
		} // end InitThis

		public void MoveUp()
		{
			//If we're not at the top end of the map, allow movement
			if(playerScript.gameObject.transform.position.y < GSP.Tiles.TileManager.MinHeightUnits)
			{
				displacementVector = new Vector3(0, GSP.Tiles.TileManager.PlayerMoveDistance, 0);
			} //end if
			else
			{
				displacementVector = Vector3.zero;
			} //end else

			// Face the character to the north (up), and move them
			playerScript.Face(FacingDirection.North);
			MovePlayer();

			// Clear the highlight objects
			Highlight.ClearHighlight();

			// Recreate the highlights with the new values
			Highlight.GenerateHighlight(playerScript.gameObject.transform.position, currTravelDist);

			//Play walking sound
			AudioManager.Instance.PlayWalk ();
		} //end MoveUp

		public void MoveDown()
		{
			//If we're not at the bottom end of the map, allow movement
			if(playerScript.gameObject.transform.position.y > GSP.Tiles.TileManager.MaxHeightUnits)
			{
				displacementVector = new Vector3(0, -GSP.Tiles.TileManager.PlayerMoveDistance, 0);
			} //end if
			else
			{
				displacementVector = Vector3.zero;
			} //end else

			// Face the character to the south (down), and move them
			playerScript.Face(FacingDirection.South);
			MovePlayer();

			// Clear the highlight objects
			Highlight.ClearHighlight();

			// Recreate the highlights with the new values
			Highlight.GenerateHighlight(playerScript.gameObject.transform.position, currTravelDist);

			//Play walking sound
			AudioManager.Instance.PlayWalk ();
		} //end MoveDown

		public void MoveLeft()
		{
			//If we're not at the left end of the map, allow movement
			if(playerScript.gameObject.transform.position.x > GSP.Tiles.TileManager.MinWidthUnits)
			{
				displacementVector = new Vector3(-GSP.Tiles.TileManager.PlayerMoveDistance, 0, 0);
			} //end if
			else
			{
				displacementVector = Vector3.zero;
			} //end else

			// Face the character to the west (left), and move them
			playerScript.Face(FacingDirection.West);
			MovePlayer();

			// Clear the highlight objects
			Highlight.ClearHighlight();

			// Recreate the highlights with the new values
			Highlight.GenerateHighlight(playerScript.gameObject.transform.position, currTravelDist);

			//Play walking sound
			AudioManager.Instance.PlayWalk ();
		} //end MoveLeft

		public void MoveRight()
		{
			// If we're not at the right end of the map, allow movement
			if(playerScript.gameObject.transform.position.x < GSP.Tiles.TileManager.MaxWidthUnits)
			{
				displacementVector = new Vector3(GSP.Tiles.TileManager.PlayerMoveDistance, 0, 0);
			} //end if
			else
			{
				displacementVector = Vector3.zero;
			} //end else

			// Face the character to the east (right), and move them
			playerScript.Face(FacingDirection.East);
			MovePlayer();

			// Clear the highlight objects
			Highlight.ClearHighlight();

			// Recreate the highlights with the new values
			Highlight.GenerateHighlight(playerScript.gameObject.transform.position, currTravelDist);

			//Play walking sound
			AudioManager.Instance.PlayWalk ();
		} //end MoveRight

		// Cancels a move sending the player back to the original position
		public void CancelMove()
		{
			// Enable movement arrows
			upButton.interactable = true;
			downButton.interactable = true;
			leftButton.interactable = true;
			rightButton.interactable = true;

			// Face the character to the south; This is the default facing
			playerScript.Face(FacingDirection.South);

			// Return player to original position
			playerScript.gameObject.transform.position = origPlayerPosition;

			// Update the entity's position
			playerScript.Position = playerScript.gameObject.transform.position;

			// Return to original distance
			currTravelDist = initialTravelDist;

			// Make sure player is not moving
			isMoving = false;
			displacementVector = Vector3.zero;

			// Clear the highlight objects
			Highlight.ClearHighlight();

			// Recreate the highlights with the new values
			Highlight.GenerateHighlight(playerScript.gameObject.transform.position, currTravelDist);

			//Play walking sound
			AudioManager.Instance.PlayWalk ();
		} // end CancelMove

        // Moves the player
        void MovePlayer()
		{
			// If no move is possible, end the function
            if (displacementVector == Vector3.zero)
			{
				isMoving = false;
				return;
			} // end if

			// If player is not already moving, allow them to move and remove a movement unit
			if (!isMoving)
			{
				isMoving = true;
				currTravelDist = currTravelDist -1;
			} //end if

			// Move player
            playerScript.gameObject.transform.Translate(displacementVector, Space.World);

            // Update the entity's position
            playerScript.Position = playerScript.gameObject.transform.position;

			// Reset movement to none and return player to not moving state
			displacementVector = Vector3.zero;
			isMoving = false;

			//See if player still has distance to travel
			TravelDistanceLeft ();
		} // end MovePlayer

		// Disable buttons if out of travel distance
		private void TravelDistanceLeft()
		{
			//If out of distance to move, disable movement arrows
			if(currTravelDist <= 0)
			{
				upButton.interactable = false;
				downButton.interactable = false;
				leftButton.interactable = false;
				rightButton.interactable = false;
			} //end if
		} //end TravelDistanceLeft

		// Gets the current travel distance remaining
        public int RemainingTravelDistance
        {
            get { return currTravelDist; }
        } // end RemainingTravelDistance
    } // end GUIMovement
} // end GSP