/*******************************************************************************
 *
 *  File Name: DieInput.cs
 *
 *  Description: Wrapper for the Die class; This is able to be placed in the
 *               scene
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: DieInput
     * 
     * Description: Add to a GameObject on the scene for a global place to access
     *              the dice.
     * 
     *******************************************************************************/
    public class DieInput : MonoBehaviour
	{
        Die dice;             // The Die reference
        //Sprite[] dieFaces;  // The Die face's Sprite's

        // Use this for initialisation
        void Awake()
        {
            // Create a new Die object
            dice = new Die();
        } // end Awake
        
        //// Gets the Die face's Sprite's
        //public Sprite GetFace(int index)
        //{
        //    return dieFaces[index];
        //} // end GetFace

		// Update is called once per frame
		void Update()
		{
			// Note: Kept in for historical reasons for now.
            // Check if the space key is pressed to roll the dice.
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    // Get the die roll
            //    var dieRoll = Dice.Roll();

            //    // Get the sprite renderer component
            //    var spriteRenderer = GetComponent<SpriteRenderer>();

            //    // Set the die's sprite to the roll
            //    spriteRenderer.sprite = GetFace(dieRoll - 1);

            //    // Print the roll to the console
            //    Debug.LogFormat("Die Roll: {0}", dieRoll);
            //} // end if
		} // end Update

        // Gets the Die object
        public Die Dice
        {
            get { return dice; }
        } // end Dice
	} // end DieInput
} // end GSP
