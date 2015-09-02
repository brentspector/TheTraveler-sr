/*******************************************************************************
 *
 *  File Name: Cake.cs
 *
 *  Description: Easter egg content
 *
 *******************************************************************************/
using GSP.Core;
using System.Collections;
using UnityEngine;

namespace GSP.Cake
{
    /*******************************************************************************
     *
     * Name: Cake
     * 
     * Description: Adds a cake chart as an easter egg. This is part of an easter
     *              egg scene.
     * 
     *******************************************************************************/
    public class Cake : MonoBehaviour
	{
		GameObject cakeChart;		    // The CakeChart GameObject reference
		Sprite[] cakeSprites;		    // The Sprite's for the CakeChart
		SpriteRenderer spriteRenderer;  // The SpriteRenderer component reference
		BoxCollider2D boxCollider;	    // The BoxCollider2D component reference
		int slicesLeft;			    	// The number of slices left
		GameObject guiTextObj;			// The child GameObject that holds the GUI text
		
		// Used for initialisation
		void Start()
		{
			// Load all the Sprite's
			cakeSprites = Resources.LoadAll<Sprite>("cake_spritesheet");
			
			// Get the CakeChart
			cakeChart = GameObject.FindGameObjectWithTag("CakeChartTag");

			// Give the CakeChart a SpriteRenderer component
            spriteRenderer = cakeChart.AddComponent<SpriteRenderer>();
			
			// The CakeChart is whole by default
            spriteRenderer.sprite = cakeSprites[0];
			
			// Give the CakeChart a RigidBody2D component to allow for collisions
			var rigidBody2D = cakeChart.AddComponent<Rigidbody2D>();
			// This is a 2D project so turn off gravity.
			rigidBody2D.gravityScale = 0.0f;
			
			// Give the CakeChart a BoxCollider2D component to add collisions
			boxCollider = cakeChart.AddComponent<BoxCollider2D>();

			// The CakeChart has five slices left when whole
			slicesLeft = 5;

			// Get the GUIText child
            guiTextObj = GameObject.Find("CakeChart/CakeText");

			// Set the GUIText
            guiTextObj.GetComponent<GUIText>().text = "Have some Cake Chart! (Click to eat)";
		} // end Start

		// Mouse event that triggers when the mouse button is in the down position
        void OnMouseDown()
		{
			// Reduce the slices left
			slicesLeft--;

			// When clicked, update things to progress to the next slice
			if (slicesLeft > 0 )
			{
				// Get the cake sprite index
				int cakeIndex = 5 - slicesLeft;

				// Advance the sprite
                spriteRenderer.sprite = cakeSprites[cakeIndex];

				// Next we need to ajust the collider
				boxCollider.size = new Vector2(cakeSprites[cakeIndex].bounds.size.x, cakeSprites[cakeIndex].bounds.size.y);
			} // end if
			else
			{
                spriteRenderer.sprite = null;

				// There are no more slices of cake left so start the quit coroutine
				StartCoroutine("Quit");
			} // end else
		} // end OnMouseDown

		// Coroutine for the ending
		IEnumerator Quit()
		{
            guiTextObj.GetComponent<GUIText>().text = "The universe is ending!";

            // Play an explosion sound
            AudioManager.Instance.PlayExplosion();

			// Wait for three seconds
			yield return new WaitForSeconds(3.0f);

            Entities.EntityManager.Instance.Dispose();
            while (GameMaster.Instance.Turn != 0)
            {
                GameMaster.Instance.NextTurn();
            } //end while
            GameMaster.Instance.NumPlayers = 0;
            AudioManager.Instance.PlayMenu();
            GameMaster.Instance.LoadLevel("MenuScene");
		} // end Quit
	} // end Cake
} // end GSP.Cake