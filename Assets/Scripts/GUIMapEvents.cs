/*******************************************************************************
 *
 *  File Name: GUIMapEvents.cs
 *
 *  Description: Old GUI for MapEvent events
 *
 *******************************************************************************/
using GSP.JAVIERGUI;
using System;
using UnityEngine;

namespace GSP
{
    //TODO: Brent: Replace this with the new In-Game UI later
    /*******************************************************************************
     *
     * Name: GUIMapEvents
     * 
     * Description: GUI features for all the MapEvent events.
     * 
     *******************************************************************************/
    public class GUIMapEvents : MonoBehaviour
	{
        // Enumeration for the types of MapEvent events
        enum MapEvent { Enemy, Ally, Resource, Item, Nothing, Done };
		
		MapEvent currMapEvent;  // The current map event

		bool canShowGUI = false;        // Whether to show or hise the OnGUI UI
		bool isActionRunning = true;    // Whether the MapEvent action is running
		
		bool canInitScript = true;                          // Ensures each script is initialized only once per state
        GUIEnemy guiEnemyScript;                            // The GUIEnemy component reference
        GUIAlly guiAllyScript;                              // The GUIAlly component reference
        GUIItem guiItemScript;                              // The GUIItem component reference
        GUIResource guiResourceScript;                      // The GUIResource component reference
        GUINothing guiNothingScript;                        // The GUINothing component reference
		GameplayStateMachine gameplayStateMachineScript;    // The GamePlayStateMachine component reference
        GUIBottomBar guiBottomBarScript;                    // The GUIBottomBar component reference

		int mainStartX = 0;                 // The main container's starting x
		int mainStartY = 65 + 32;           // The main container's starting y; 65 is just below the main GUI, and I added a gap of 32 from the end of that
        int mainWidth = Screen.width / 3;   // The main container's width
        int mainHeight = Screen.height / 2; // The main container's height
		string resultString;                // The resulting string
		
		// Use this for initialisation
		void Start ()
        {
			// Initialise the map event to nothing
            currMapEvent = MapEvent.Nothing;

			// Get the component references
            guiEnemyScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GUIEnemy>();
            guiAllyScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GUIAlly>();
            guiItemScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GUIItem>();
            guiResourceScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GUIResource>();
            guiNothingScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GUINothing>();
			gameplayStateMachineScript = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag").GetComponent<GameplayStateMachine>();
			guiBottomBarScript = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag").GetComponent<GUIBottomBar>();
		} // end Start


		// Initialise things sort of like a custom constructor
        public void InitThis(string mapEventType, string result)
		{
			// The results of parsing below.
			MapEvent tmpEnumMapEvent = MapEvent.Nothing;

			try
			{
				// Attempt to parse the string into the enum value.
                tmpEnumMapEvent = (MapEvent)Enum.Parse(typeof(MapEvent), mapEventType);

			} // end try
			catch (Exception)
			{
				// The parsing failed so make sure the resource is null.
                Debug.LogWarningFormat("Requested MapEvent type '{0}' was not found.", mapEventType);
			} // end catch
			
			// Set the current event
			currMapEvent = tmpEnumMapEvent;

			// Tell the StateMachine that the action is running and show the onGUI UI
            isActionRunning = true;
			canShowGUI = true;

			// Set the resulting string
            resultString = result;
			
		} // end InitThis


		// Returns if the action is running
        // true if running; otherwise, false
        public bool IsActionRunning()
		{
			return isActionRunning;
		} // end IsActionRunning


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The old style GUI functioned using an OnGUI function; Runs each frame
		void OnGUI()
		{
			// Check if we should show the OnGUI UI for the MapEvent
            if (canShowGUI) 
			{
				GUIContainer();
			} // end if
		} // end OnGUI


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The state machine for the MapEvent OnGUI system
        private void GUIMapEventsMachine()
		{
			// Switch over the current MapEvent
            switch (currMapEvent) 
			{
                case MapEvent.Enemy:
                    {
                        // Ensure we only init once
                        if (canInitScript)
                        {
                            guiEnemyScript.InitThis(mainStartX, mainStartY, mainWidth, mainHeight, resultString);
                            canInitScript = false;
                        } // end if
                        break;
                    } // end Case Enemy

                case MapEvent.Ally:
                    {
                        // Ensure we only init once
                        if (canInitScript)
				        {
                            guiAllyScript.InitGUIAlly(mainStartX, mainStartY, mainWidth, mainHeight);
                            canInitScript = false;
                        } // end if
				        break;
                    } // end Case Ally

                case MapEvent.Item:
                    {
                        // Ensure we only init once
                        if (canInitScript)
                        {
                            guiItemScript.InitGUIItem(mainStartX, mainStartY, mainWidth, mainHeight, resultString);
                            canInitScript = false;
                        } // end if
                        break;
                    } // end Case Item

                case MapEvent.Resource:
                    {
                        // Ensure we only init once
                        if (canInitScript)
                        {
                            // Animate the resource button
                            gameplayStateMachineScript.AnimateResourceButton(false);
                            guiResourceScript.InitThis(mainStartX, mainStartY, mainWidth, mainHeight, resultString);
                            canInitScript = false;
                        } // end if
                        break;
                    } // end Case Resource

                case MapEvent.Nothing:
                    {
                        // Ensure we only init once
                        if (canInitScript)
                        {
                            guiNothingScript.InitThis(mainStartX, mainStartY, mainWidth, mainHeight, resultString);
                            canInitScript = false;
                        } // end if
                        break;
                    } // end Case Nothing

                case MapEvent.Done:
                    {
                        // We're done here so close the OnGUI UI, the action is no longer running, and nothing is being initialised
                        canShowGUI = false;
                        isActionRunning = false;
                        canInitScript = true;
                        break;
                    } // end Case Done

                // Shouldn't reach here, this is reached if the state isn't listed above
                default:
                    {
                        Debug.LogWarning("Hit the default case in GUIMapEvents!");
                        break;
                    } // end Case default
            } // end switch currMapEvent
		} // end GUIMapEventsMachine


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Creates the OnGUI systems main MapEvent container and runs the statemachine to find which UI to show
        void GUIContainer()
		{
			//The main container
            GUI.Box(new Rect(mainStartX, mainStartY, mainWidth, mainHeight), currMapEvent.ToString());

			// Check which GUI to Display
            // Runs every frame to check what state we are in
            GUIMapEventsMachine();
		} // end GUIContainer

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The MapEvent action is finished
		public void MapEventDone()
		{
            // Change the state to the Done state
            currMapEvent = MapEvent.Done;
            // Stop any animations
			guiBottomBarScript.StopAnimation();
			gameplayStateMachineScript.StopAnimation();
		} // end MapEventDone
	} // end GUIMapEvents
} //end GSP

