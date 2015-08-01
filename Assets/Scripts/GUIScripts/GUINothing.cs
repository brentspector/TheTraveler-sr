/*******************************************************************************
 *
 *  File Name: GUINothing.cs
 *
 *  Description: Old GUI for the Nothing MapEvent event
 *
 *******************************************************************************/
using UnityEngine;


namespace GSP.JAVIERGUI
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    //TODO: Brent: Replace this with the new In-Game UI later; probably not in the same namespace
    /*******************************************************************************
     *
     * Name: GUINothing
     * 
     * Description: Creates the GUI for the Nothing MapEvent.
     * 
     *******************************************************************************/
    public class GUINothing : MonoBehaviour 
	{

        GUIMapEvents guiMapEventsScript;    // The GUIMapEvents script reference

		// main container values
        int mainStartX = -1;   // The starting x value
        int mainStartY = -1;   // The starting y value
        int mainWidth  = -1;   // The starting width value
        int mainHeight = -1;   // The starting height value

        bool isActionRunning = false;   // Whether the ally action is running
        string headerString;            // The text in the OnGUI UI; the status text
		
		// Use this for initialization
		void Start()
        {
            guiMapEventsScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.GUIMapEvents>();
		} // end Start

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Initialise things sort of like a custom constructor
        public void InitThis(int startX, int startY, int startWidth, int startHeight, string result)
		{
            mainStartX = startX;
			mainStartY = startY;
			mainWidth = startWidth;
			mainHeight = startHeight;
			
			isActionRunning = true;
			
			headerString = "No map event.\nClick done to\nend your turn.";
		} // end InitThis

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The old style GUI functioned using an OnGUI function; Runs each frame
		void OnGUI() 
		{
            if (isActionRunning)
			{
				// DEFAULT color
				GUI.backgroundColor = Color.red;
				
				ConfigHeader();
				ConfigDoneButton();
			} // end if
		} // end OnGUI()

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the header of the OnGUI UI system
        void ConfigHeader()
		{
            int headWdth = mainWidth - 2;
            int headHght = mainHeight / 5;
            int headX = mainStartX + ((mainWidth - headWdth) / 2);
            int headY = mainStartY + (headHght * 2);

            GUI.Box(new Rect(headX, headY, headWdth, headHght * 2), headerString);
		} // end ConfigHeader

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the done button of the OnGUI UI system
        void ConfigDoneButton()
		{
			// done button
            int doneWidth = mainWidth / 2;
            int doneHeight = mainHeight / 8;
            int doneStartX = mainStartX + (mainWidth - doneWidth) / 2;
            int doneStartY = mainStartY + (doneHeight * 7);
			GUI.backgroundColor = Color.red;

            if (GUI.Button(new Rect(doneStartX, doneStartY, doneWidth, doneHeight), "DONE"))
			{
				isActionRunning = false;
				// once nothing is happening, program returns to Controller's End Turn State
				guiMapEventsScript.MapEventDone();
			} // end if
        } // end ConfigDoneButton
	} // end GUINothing
} // end GSP.JAVIERGUI