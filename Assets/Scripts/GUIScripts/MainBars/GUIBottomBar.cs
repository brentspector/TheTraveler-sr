/*******************************************************************************
 *
 *  File Name: GUIBottomBar.cs
 *
 *  Description: Old GUI for the bottom UI bar
 *
 *******************************************************************************/
using GSP.Char;
using UnityEngine;

namespace GSP.JAVIERGUI
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    //TODO: Brent: Replace this with the new In-Game UI later; probably not in the same namespace
    /*******************************************************************************
     *
     * Name: GUIBottomBar
     * 
     * Description: Creates the GUI for the bottom bar containing the item and
     *              ally buttons.
     * 
     *******************************************************************************/
    public class GUIBottomBar : MonoBehaviour 
	{
		// ...Scripts...
		Character characterScript;

		// ....Bottom Bar Configuration values....
		int gapInYdirection;    // The gap between the buttons on the y-axis
		int gapInXdirection;    // The gap between the buttons on the x-axis
		int buttonHeight;       // The button's height
		int buttonWidth;        // The button's width
		int barStartX;          // The button's starting position on the x-axis
		int barStartY;          // The button's starting position on the y-axis


		//....Feedback Values....
		double animTimer = 0.0f;        // The button's flashing animation timer
		bool canRunAnimation = false;   // Whether to run the animation

		bool canViewItems = false;      // Whether to show the Items UI
		bool canViewAllies = false;     // Whether to show the Allies UI

		bool canRunItemAnim = false;    // Whether to run the Items animation
		bool canRunAllyAnim = false;    // Wheter to run the Allies animation

		// Use this for initialization
		void Awake ()
        {
			ReScaleValues();
		} // end Awake

		//TODO: Damien: Replace with the GameMaster functionality later.
        // Sets the Character script reference to the Character component of the given player
        public void RefreshBottomBarGUI(GameObject player)
		{
			characterScript = player.GetComponent<Character>();
			
		} // end RefreshBottomBarGUI


		//TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Rescales to the values to fit with the OnGUI system
        public void ReScaleValues()
		{
			gapInYdirection = 1;
			gapInXdirection = 2;
			buttonHeight = 32;
			buttonWidth = 64;
			barStartX = (buttonWidth / 4);
			barStartY = (Screen.height - (buttonHeight * 1));
		} // end ReScaleValues

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The old style GUI functioned using an OnGUI function; Runs each frame
        void OnGUI()
		{
			// Rescale values
            ReScaleValues();
			// default button color
			GUI.backgroundColor = Color.red;

			int col;    // The columns
			int row;    // The rows

			//.............
			//   Row 0
			//.............
			row = 0;
				//,,,,,,,,,,,,
				//   Col 0
				//,,,,,,,,,,,,
			col = 0;
            ConfigItemButton((barStartX + (col * gapInXdirection)), (barStartY - (row * gapInYdirection)), buttonWidth, buttonHeight);
				//,,,,,,,,,,,,
				//	Col 1
				//,,,,,,,,,,,,
            col++;
            ConfigAllyButton((barStartX + (col * buttonWidth + gapInXdirection)), (barStartY - (row * buttonHeight + gapInYdirection)), buttonWidth, buttonHeight);


			//.............
			//	Row 1
			//.............
            row++;
				//,,,,,,,,,,,,
				//	Col 0
				//,,,,,,,,,,,,
			col = 0;
            ConfigItemBarDisplay((barStartX + (col * buttonWidth + gapInXdirection)), (barStartY - (row * buttonHeight + gapInYdirection)), buttonWidth, buttonHeight);
            ConfigAllyBarDisplay((barStartX + (col * buttonWidth + gapInXdirection)), (barStartY - (row * buttonHeight + gapInYdirection)), (2 * buttonWidth), buttonHeight);
		} // end OnGUI

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the item button of the OnGUI UI system
        void ConfigItemButton(int startX, int startY, int startWidth, int startHeight)
		{
			if(!canRunAnimation)
			{
                PickButtonColor(canViewItems);
			} // end if
			else
			{
                AnimTimer(canRunItemAnim);
			} // end else


            if (GUI.Button(new Rect(startX, startY, startWidth, startHeight), "ITEMS"))
			{
				// stop animation
				canRunItemAnim = false;
				canRunAnimation = false;

				// set view values
				if(!canViewItems)
				{
					canViewAllies = false;
					canViewItems = true;
				} // end if
				else
				{
					canViewItems = false;
				} // end else
			} // end if
		} // end ConfigItemButton

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the item bar of the OnGUI UI system
        void ConfigItemBarDisplay(int startX, int startY, int startWidth, int startHeight)
		{
            if (canViewItems)
			{
				int row = 0;
				int col = 0;
                string resultString = (characterScript.AttackPower).ToString();
                GUI.Box(new Rect(startX + (col * startWidth), startY + (row * startHeight), startWidth, startHeight), "AP: " + resultString);

				col++;
				resultString = (characterScript.DefencePower).ToString();
                GUI.Box(new Rect(startX + (col * startWidth), startY + (row * startHeight), startWidth, startHeight), "DP: " + resultString);

				col++;
                resultString = (characterScript.ResourceValue.ToString() + "/" + characterScript.MaxInventory.ToString());
                GUI.Box(new Rect(startX + (col * startWidth), startY + (row * startHeight), startWidth, startHeight), "DP: " + resultString);
			} // end if
		} // end ConfigItemBarDisplay


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the item button of the OnGUI UI system
        void ConfigAllyButton(int startX, int startY, int startWidth, int startHeight)
		{
			if(!canRunAnimation)
			{
                PickButtonColor(canViewAllies);
			} // end if
			else
			{
                AnimTimer(canRunAllyAnim);
			} // end else


            if (GUI.Button(new Rect(startX, startY, startWidth, startHeight), "ALLIES"))
			{
				// stop animation
				canRunAllyAnim = false;
				canRunAnimation = false;

				// set view values
				if(!canViewAllies)
				{
					canViewItems = false;
					canViewAllies = true;
				} // end if
				else
				{
					canViewAllies = false;
				} // end else
			} // end if
		} // end ConfigAllyButton

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the ally bar of the OnGUI UI system
        void ConfigAllyBarDisplay(int startX, int startY, int startWidth, int startHeight)
		{
			if(canViewAllies)
			{
				int row = 0;
				int col = 0;
                string resultString = (characterScript.NumAllies).ToString();
                GUI.Box(new Rect(startX + (col * startWidth), startY + (row * startHeight), startWidth, startHeight), "# of Allies: " + resultString);
			} // end if

		} // end ConfigAllyBarDisplay

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Picks a background colour based upon if the button is selected for the OnGUI system
        void PickButtonColor(bool isSelected)
		{
			if (isSelected) 
			{
				GUI.backgroundColor = Color.yellow;
			} // end if
			else
			{
				GUI.backgroundColor = Color.red;
			} // end else
		} // end PickButtonColor

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Animates the ally button for the OnGUI system
        public void AnimateAllyButton()
		{
			canRunAllyAnim = true;
			canRunAnimation = true;
		} // end AnimateAllyButton

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Animate the item button for the OnGUI system
        public void AnimateItemButton()
		{
			canRunItemAnim = true;
			canRunAnimation = true;
		} // end AnimateAllyButton

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Manages the animation timer for the OnGUI system
        void AnimTimer(bool isAnimating)
		{
            animTimer = animTimer + Time.deltaTime;
			if(animTimer > 2.0f)
			{
				animTimer = 0.0f;
			} // end if
			
			if(isAnimating)
			{
				if (animTimer < 1.0f)
				{
					GUI.backgroundColor = Color.red;
				} // end if
				else
				{
					GUI.backgroundColor = Color.yellow;
				} // end else
			} // end if
			else
			{
				GUI.backgroundColor = Color.red;
			} // end else
		} // end AnimTimer

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Stops the animation for the OnGUI system
        public void StopAnimation()
		{
			canRunAllyAnim = false;
			canRunItemAnim = false;
			canRunAnimation = false;
		} // end StopAnimation
	} // end GUIBottomBar
} // end GSP.JAVIERGUI