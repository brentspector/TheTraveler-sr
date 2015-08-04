/*******************************************************************************
 *
 *  File Name: GUIAlly.cs
 *
 *  Description: Old GUI logic for the Ally MapEvent event
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.JAVIERGUI
{
    //TODO: Brent: Replace this with the new In-Game UI later; probably not in the same namespace
    /*******************************************************************************
     *
     * Name: GUIAlly
     * 
     * Description: Creates the GUI for Ally MapEvent.
     * 
     * Steps:
     *       1. GetPlayer Values
     *       2. Would you like to add Ally?
     *       3. Yes, increase player's MAXWEIGHT
     *       4. Show results and display done button
     * 
     *******************************************************************************/
    public class GUIAlly : MonoBehaviour 
	{
		GameObject playerEntity; 			// Will initialize to the actual player in InitThis
		GUIMapEvents guiMapEventsScript;    // The GUIMapEvents script reference
		MapEvent mapEventScript;            // The MapEvent script reference
		GUIBottomBar guiBottomBarScript;    // The GUIBottomBar script reference
		
		const int allyHelpMaxWeightIncrease = 150;      // The maximum weight increase an ally can give
		string headerString;                            // The text in the OnGUI UI; the selection question text

		int mainStartX 	= -1;   // The starting x value
        int mainStartY  = -1;   // The starting y value
        int mainWidth   = -1;   // The starting width value
        int mainHeight  = -1;   // The starting height value

		bool hasSelectionMadeAddRemove = false;	// For internal use, determines if player wants to add Ally or not
		bool isActionRunning = false;           // Whether the ally action is running


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Use this for initialization
		void Start()
        {
			// Get the GUIBottomBar reference
            guiBottomBarScript = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag").GetComponent<GSP.JAVIERGUI.GUIBottomBar>();
		} // end Start


        //TODO: Damien: Replace with the GameMaster functionality later.
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Initialise things sort of like a custom constructor
        public void InitGUIAlly(GameObject player, int startX, int startY, int startWidth, int startHeight)
		{
			// Get the player reference
            playerEntity = player;
			// Get the GUIMapEvents and MapEvent script references
            guiMapEventsScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.GUIMapEvents>();
			mapEventScript = GameObject.FindGameObjectWithTag ("DieTag").GetComponent<GSP.MapEvent>();

			// The action is running
            isActionRunning = true;

			// GUIMapEvents values transferred over
			mainStartX = startX;
			mainStartY = startY;
			mainWidth = startWidth;
			mainHeight = startHeight;
		} // end InitGUIAlly

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The old style GUI functioned using an OnGUI function; Runs each frame
        void OnGUI()
		{
			GUI.backgroundColor = Color.red;
			// Check if the action is running
            if (isActionRunning == true) 
			{
				// Check if a selection has been made
                if(hasSelectionMadeAddRemove == false)
				{
                    ConfigHeader();
                    ConfigAddButton();
                    ConfigCancelButton();
				} // end if
				else
				{
					ConfigHeader();
					ConfigDoneButton();
				} // end else
			} // end if
		} // end OnGUI

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the header of the OnGUI UI system
        void ConfigHeader()
		{
            // Check if a selection has been made
            if (hasSelectionMadeAddRemove == false)
			{
                // Set the header string
                headerString = "Would You Like\nto Add An Ally?";
			} // end if

            int headWdth = mainWidth - 2;
            int headHght = mainHeight / 6;
            int headX = mainStartX + ((mainWidth - headWdth) / 2);
            int headY = mainStartY + (headHght * 2);

            GUI.Box(new Rect(headX, headY, headWdth, headHght * 2), headerString);
		} // end ConfigHeader

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the add button of the OnGUI UI system
        void ConfigAddButton()
		{
            int newWdth = mainWidth / 5;
            int newHght = mainHeight / 6;
            int newX = mainStartX + (newWdth * 1);
            int newY = mainStartY + (newHght * 4);

            if (GUI.Button(new Rect(newX, newY, newWdth, newHght * 2), "Yes"))
			{
				#region Add Ally Sound
				guiBottomBarScript.AnimateAllyButton();
				//TODO: Ally sound is added here?
				#endregion

                headerString = mapEventScript.ResolveAlly(playerEntity, "YES");

				hasSelectionMadeAddRemove = true;
			} // end if
		} // end ConfigAddButton

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the cancel button of the OnGUI UI system
        void ConfigCancelButton()
		{
            int newWdth = mainWidth / 5;
            int newHght = mainHeight / 6;
            int newX = mainStartX + (newWdth * 3);
            int newY = mainStartY + (newHght * 4);

            if (GUI.Button(new Rect(newX, newY, newWdth, newHght * 2), "No"))
			{
                headerString = mapEventScript.ResolveAlly(playerEntity, "NO");

				hasSelectionMadeAddRemove = true;
			} // end if
		} // end ConfigCancelButton


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the done button of the OnGUI UI system
        private void ConfigDoneButton()
		{
			// done button
            int doneWidth = mainWidth / 2;
            int doneHeight = mainHeight / 8;
            int doneStartX = mainStartX + (mainWidth - doneWidth) / 2;
            int doneStartY = mainStartY + (doneHeight * 7);
            GUI.backgroundColor = Color.red;

            if (GUI.Button(new Rect(doneStartX, doneStartY, doneWidth, doneHeight), "DONE"))
			{
				// stop animation
				guiBottomBarScript.StopAnimation();

				isActionRunning = false;
				hasSelectionMadeAddRemove = false;
				// once nothing is happening, program returns to Controller's End Turn State
				guiMapEventsScript.MapEventDone();
			} // end if
        } // end ConfigDoneButton
    } // end GUIAlly
} // end GSP.JAVIERGUI
