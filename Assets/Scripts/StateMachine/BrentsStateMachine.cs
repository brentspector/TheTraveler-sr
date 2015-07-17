using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace GSP
{
	public class BrentsStateMachine : MonoBehaviour
	{
		//Contains overall states of program
		public enum OVERALLSTATES {INTRO, MENU, GAME, END};
		//Contains states of menu
		public enum MENUSTATES {HOME, SOLO, MULTI, CREDITS, HELP, QUIT};
		
		//State machine variables
		OVERALLSTATES m_programState;		//Current overall state
		MENUSTATES m_menuState;				//Current menu state
		float timeHolder;					//Holds waiting time

		//Menu Objects
		GameObject introSet;				//Intro panel
		GameObject menuSet;					//Main Menu panel
		GameObject mapSet;					//Map selection panel
		GameObject creditsSet;				//Credits panel
		GameObject instructionsSet;			//Instructions panel
		GameObject backButton;				//Back button
		Text 	   guideText;				//Map selection text
		bool menuDisplayed;					//Whether menu is displayed or not
		bool instructionsDisplayed;			//Whether instructions are displayed or not
		bool creditsDisplayed;				//Whether credits are displayed or not
		public string m_mapSelection;		//Holds scene name of map
		bool mapsDisplayed;					//Whether selection maps are displayed or not
		#region Menu Data Declaration Stuff

		// Holds the reference to the game object.
		GameObject m_menuData;

		// Holds the reference to the menu data's script.
		MenuData m_menuDataScript;

		#endregion
		
		//Initialize variables
		void Start()
		{
			//Init Menu variables
			m_programState = OVERALLSTATES.INTRO;				//Initial beginning of game
			m_menuState = MENUSTATES.HOME;						//Prevents triggers from occuring before called
			menuDisplayed = true;								//Menu begins displayed
			creditsDisplayed = false;							//Credits begins hidden
			instructionsDisplayed = false;						//Instructions begin hidden
			timeHolder = Time.time + 3.0f;						//Initialize first wait period
			m_mapSelection = "nothing";							//Nothing has been chosen yet
			mapsDisplayed = false;								//Map selection begins hidden

			//Obtain references to hierarchy panels and objects
			introSet = GameObject.Find ("Intro");	
			menuSet = GameObject.Find ("MainMenu");
			mapSet = GameObject.Find ("MapSelection");
			creditsSet = GameObject.Find ("Credits");
			instructionsSet = GameObject.Find ("Instructions");
			backButton = GameObject.Find ("BackButton");
			guideText = GameObject.Find ("GuideText").GetComponent<Text> ();

			//Disable everything but intro and menu
			disableMapSelection ();
			disableInstructions ();
			disableCredits ();
			backButton.SetActive (false);
			guideText.gameObject.SetActive (false);

			#region Menu Data Initialisation Stuff
			
			// Create the empty game object.
			m_menuData = new GameObject( "MenuData" );
			
			// Tag it as menu data.
			m_menuData.tag = "MenuDataTag";
			
			// Add the menu data component.
			m_menuData.AddComponent<MenuData>();
			
			// Set it to not destroy on load.
			DontDestroyOnLoad( m_menuData );

			// Get the menu data object's script.
			m_menuDataScript = m_menuData.GetComponent<MenuData>();

			#endregion
		} //end Start
		
		//Main function for controlling game
		void Update()
		{
			//PROGRAM ENTRY POINT
			switch(m_programState)
			{
				//INTRO
			case OVERALLSTATES.INTRO:
				//INTRO ENTRY POINT
				//After intro finishes, move to menu
				if(Time.time > timeHolder)
				{
					//Change state
					m_programState = OVERALLSTATES.MENU;

					//Show main menu 
					disableTitle();
				} //end wait if
				break;
				//MENU
			case OVERALLSTATES.MENU:
				//MENU ENTRY POINT
				switch(m_menuState)
				{
				case MENUSTATES.HOME:
					//HOME - Menu hub, displays all buttons for menu
					if(!menuDisplayed)
					{
						enableMainMenu();
					} //end if
					break;
					//SOLO - Single Player game
				case MENUSTATES.SOLO:
					//Hide menu if not cleared yet
					if(menuDisplayed)
					{
						disableMainMenu();
					} //end if

					//Display maps for selection if not done yet
					if(!mapsDisplayed)
					{
						enableMapSelection();
					} //end if

					//Only continue once a map has been chosen
					if(m_mapSelection != "nothing")
					{
						//Pick player amount
						#region Menu Data Adding Stuff

						// Set the number of players to one for solo mode.
						m_menuDataScript.NumberPlayers = 1;

						#endregion

						//Display loading text
						guideText.text = "Loading, please wait.";

						//Transition into game state
						m_programState = OVERALLSTATES.GAME;
					} //end if
					break;
					//MULTI - Multiplayer game
				case MENUSTATES.MULTI:
					//Clear buttons if not cleared yet
					//Hide menu if not cleared yet
					if(menuDisplayed)
					{
						disableMainMenu();
					} //end if
					
					//Display maps for selection
					if(!mapsDisplayed)
					{
						enableMapSelection();
					} //end if
					
					//Only continue once a map has been chosen
					if(m_mapSelection != "nothing")
					{
						//Pick player amount
						#region Menu Data Adding Stuff

						guideText.text = "Please select a map...Done!\nPlease enter the number of players [1-4]";

						//Set number of players
						if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
						{
							m_menuDataScript.NumberPlayers = 1;

							//Display loading text
							guideText.text = "Loading, please wait.";

							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						else if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
						{
							m_menuDataScript.NumberPlayers = 2;

							//Display loading text
							guideText.text = "Loading, please wait.";

							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						else if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
						{
							m_menuDataScript.NumberPlayers = 3;

							//Display loading text
							guideText.text = "Loading, please wait.";

							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						else if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
						{
							m_menuDataScript.NumberPlayers = 4;

							//Display loading text
							guideText.text = "Loading, please wait.";

							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						#endregion
					} //end if
					break;
					//CREDITS - Display names and instructions
				case MENUSTATES.CREDITS:
					//Hide menu if not cleared yet
					if(menuDisplayed)
					{
						disableMainMenu();
					} //end if

					if(!creditsDisplayed)
					{
						enableCredits();
					} //end if
					break;
					//HELP - Displays the instructions for the game
				case MENUSTATES.HELP:
					//Clear buttons and display instructions if not cleared yet
					if(menuDisplayed)
					{
						disableMainMenu();
					} //end if

					if(!instructionsDisplayed)
					{
						enableInstructions();
					} //end if
					break;
					//QUIT - Change program to END to wrap up any loose ends
				case MENUSTATES.QUIT:
					//Hide menu if not cleared yet
					if(menuDisplayed)
					{
						disableMainMenu();
					} //end if

					//Move to end of program
					m_programState = OVERALLSTATES.END;
					break;
				} //end Menu Switch
				break;
				//GAME
			case OVERALLSTATES.GAME:
				//GAMEPLAY ENTRY POINT
				//Reset states
				m_programState = OVERALLSTATES.MENU;
				m_menuState = MENUSTATES.HOME;

				//Load selected level
				Application.LoadLevel(m_mapSelection);
				break;
				//END
			case OVERALLSTATES.END:
				//END ENTRY POINT - WATCH OUT UNIVERSE!
				//Wrap up any loose ends here since the program is now exiting.

				//Delete exit button if it exists
				if(GameObject.Find("BackButton") != null)
				{
					Destroy (GameObject.Find("BackButton"));
				} //end if

				//Reset state machine to initial
				m_menuState = MENUSTATES.HOME;
				m_programState = OVERALLSTATES.INTRO;

				//Reset objected to initial point
				enableTitle();
				disableMainMenu();

				//Reset time holder to initial
				timeHolder = Time.time + 3.0f;

				//Exit program if possible
				Application.Quit();

				break;
				//DEFAULT - Catches any exceptions end prints errored state
			default:
				Debug.Log ("Error - No program state " + m_programState + " found.");
				break;
			} //end Program Switch
		} //end Update

		//State machine functions
		//Program state
		public int GetState()
		{
			return (int)m_programState;
		} //end GetState()
		
		//Menu state
		public int GetMenu()
		{
			return (int)m_menuState;
		} //end GetMenu()

		//Menu state settings
		void enableTitle()
		{
			introSet.SetActive (true);
		} //end enableTitle()

		void disableTitle()
		{
			introSet.SetActive (false);
		} //end disableTitle()

		void enableMainMenu()
		{
			menuSet.SetActive (true);
			backButton.SetActive (false);
			menuDisplayed = true;
		} //end enableMainMenu()

		void disableMainMenu()
		{
			menuSet.SetActive (false);
			backButton.SetActive (true);
			menuDisplayed = false;
		} //end disableMainMenu()

		void enableMapSelection()
		{
			mapSet.SetActive (true);
			guideText.gameObject.SetActive(true);
			mapsDisplayed = true;
		} //end enableMapSelection()

		void disableMapSelection()
		{
			mapSet.SetActive (false);
			guideText.gameObject.SetActive (false);
			mapsDisplayed = false;
		} //end disableMapSelection()

		void enableInstructions()
		{
			instructionsSet.SetActive (true);
			instructionsDisplayed = true;
		} //end enableInstructions()
		
		void disableInstructions()
		{
			instructionsSet.SetActive (false);
			instructionsDisplayed = false;
		} //end disableInstructions()

		void enableCredits()
		{
			creditsSet.SetActive (true);
			creditsDisplayed = true;
		} //end enableCredits()
		
		void disableCredits()
		{
			creditsSet.SetActive (false);
			creditsDisplayed = false;
		} //end disableCredits()

		//Menu Button functions
		public void soloGame()
		{
			m_menuState = MENUSTATES.SOLO;
		} //end soloGame()

		public void multiGame()
		{
			m_menuState = MENUSTATES.MULTI;
		} //end multiGame()

		public void help()
		{
			m_menuState = MENUSTATES.HELP;
		} //end help()

		public void credits()
		{
			m_menuState = MENUSTATES.CREDITS;
		} //end credits

		public void quitGame()
		{
			m_menuState = MENUSTATES.QUIT;
		} //end quitGame

		public void back()
		{
			if(m_menuState == MENUSTATES.CREDITS)
			{
				disableCredits();
				m_menuState = MENUSTATES.HOME;
			} //end if
			else if(m_menuState == MENUSTATES.HELP)
			{
				disableInstructions();
				m_menuState = MENUSTATES.HOME;
			} //end else if
			else if(m_menuState == MENUSTATES.SOLO || m_menuState == MENUSTATES.MULTI)
			{
				disableMapSelection();
				m_mapSelection = "nothing";
				guideText.text = "Please select a map";
				m_menuState = MENUSTATES.HOME;
			} //end else if
			else
			{
				m_menuState = MENUSTATES.HOME;
			} //end else
		} //end back()

		public void desertMap()
		{
			m_mapSelection = "area01";
		} //end desertMap()

		public void euroMap()
		{
			m_mapSelection = "area02";
		} //end euroMap()

		public void metroMap()
		{
			m_mapSelection = "area03";
		} //end metroMap()

		public void snowMap()
		{
			m_mapSelection = "area04";
		} //end snowMap()
	} //end StateMachine class
} //end namespace GSP
