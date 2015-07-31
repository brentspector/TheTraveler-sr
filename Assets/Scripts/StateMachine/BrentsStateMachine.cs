/*******************************************************************************
 *
 *  File Name: BrentsStateMachine.cs
 *
 *  Description: Brains of the menu's logic; controls the flow of the menu
 *
 *******************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: BrentsStateMachine
     * 
     * Description: The logic for the menu.
     * 
     *******************************************************************************/
    public class BrentsStateMachine : MonoBehaviour
	{
        // Note: Class variables should always be private. For outside access, use properties with get and/or set - Damien
        //       An exception is setting assets/values through the unity editor; however not always the best way to do it
        
        // State machine variables
		OverallState programState;          // Current overall state
		MenuState menuState;				// Current menu state
		float timeHolder;					// Waiting time

        // Audio prefab holder
        public GameObject audioManager;

		// Menu Objects
		GameObject introSet;				// Intro panel
		GameObject menuSet;					// Main Menu panel
		GameObject mapSet;					// Map selection panel
		GameObject creditsSet;				// Credits panel
		GameObject instructionsSet;			// Instructions panel
		GameObject backButton;				// Back button
		Text 	   guideText;				// Map selection text
		bool isMenuDisplayed;				// Whether menu is displayed or not
		bool isInstructionsDisplayed;		// Whether instructions are displayed or not
		bool isCreditsDisplayed;			// Whether Credits are displayed or not
		string mapSelection;		        // Scene name of map
		bool isMapsDisplayed;				// Whether selection maps are displayed or not

		#region Menu Data Declaration Stuff

		// The reference to the MenuData GameObject
		GameObject menuData;

		// The reference to the menuData's script
		MenuData menuDataScript;

		#endregion
		
		// Initialize variables
		void Start()
		{
            // Init Menu variables
			programState = OverallState.Intro;				// Initial beginning of game
			menuState = MenuState.Home;						// Prevents triggers from occuring before called
			isMenuDisplayed = true;							// Menu begins displayed
			isCreditsDisplayed = false;						// Credits begins hidden
			isInstructionsDisplayed = false;				// Instructions begin hidden
			timeHolder = Time.time + 3.0f;					// Initialize first wait period
			mapSelection = "nothing";						// Nothing has been chosen yet
			isMapsDisplayed = false;						// Map selection begins hidden

			// Obtain references to hierarchy panels and objects
            introSet = GameObject.Find("Intro");
            menuSet = GameObject.Find("MainMenu");
            mapSet = GameObject.Find("MapSelection");
            creditsSet = GameObject.Find("Credits");
            instructionsSet = GameObject.Find("Instructions");
            backButton = GameObject.Find("BackButton");
            guideText = GameObject.Find("GuideText").GetComponent<Text>();

			// Disable everything but intro and menu
            DisableMapSelection();
            DisableInstructions();
            DisableCredits();
            backButton.SetActive(false);
            guideText.gameObject.SetActive(false);

			#region Menu Data Initialisation Stuff
			
			// Create the empty GameObject
            menuData = new GameObject("MenuData");
			
			// Tag it as MenuDataTag
			menuData.tag = "MenuDataTag";
			
			// Add the MenuData component
			menuData.AddComponent<MenuData>();
			
			// Set it to not destroy on load
            DontDestroyOnLoad(menuData);

			// Get the MenuData GameObject's script
            menuDataScript = menuData.GetComponent<MenuData>();

			#endregion
		} // end Start
		
		// Main function for controlling game
		void Update()
		{
			// PROGRAM ENTRY POINT
			switch(programState)
			{
				// Intro
                case OverallState.Intro:
                    {
                        // INTRO ENTRY POINT
                        
                        // Make sure an AudioManager exists
                        if (AudioManager.Instance == null)
                        {
                            Instantiate(audioManager);
                        } // end if
                        
                        // After intro finishes, move to menu
                        if (Time.time > timeHolder)
                        {
                            // Change state
                            programState = OverallState.Menu;

                            // Show main menu 
                            DisableTitle();
                        } // end if
                        break;
                    } // end case Intro
				// Menu
                case OverallState.Menu:
                    {
                        // MENU ENTRY POINT
                        switch (menuState)
                        {
                            case MenuState.Home:
                                {
                                    // Home - Menu hub, displays all buttons for menu
                                    if (!isMenuDisplayed)
                                    {
                                        EnableMainMenu();
                                    } // end if
                                    break;
                                } // end case Home
                            // Solo - Single Player game
                            case MenuState.Solo:
                                {
                                    // Hide menu if not cleared yet
                                    if (isMenuDisplayed)
                                    {
                                        DisableMainMenu();
                                    } // end if

                                    // Display maps for selection if not done yet
                                    if (!isMapsDisplayed)
                                    {
                                        EnableMapSelection();
                                    } // end if

                                    // Only continue once a map has been chosen
                                    if (mapSelection != "nothing")
                                    {
                                        // Pick player amount
                                        #region Menu Data Adding Stuff

                                        // Set the number of players to one for solo mode
                                        menuDataScript.NumberPlayers = 1;

                                        #endregion

                                        // Display loading text
                                        guideText.text = "Loading, please wait.";

                                        // Transition into game state
                                        programState = OverallState.Game;
                                    } // end if
                                    break;
                                } // end case Solo
                            //Multi - Multiplayer game
                            case MenuState.Multi:
                                {
                                    // Clear buttons if not cleared yet
                                    // Hide menu if not cleared yet
                                    if (isMenuDisplayed)
                                    {
                                        DisableMainMenu();
                                    } // end if

                                    // Display maps for selection
                                    if (!isMapsDisplayed)
                                    {
                                        EnableMapSelection();
                                    } // end if

                                    // Only continue once a map has been chosen
                                    if (mapSelection != "nothing")
                                    {
                                        // Pick player amount
                                        guideText.text = "Please select a map...Done!\nPlease enter the number of players [2-4]";

                                        #region Menu Data Adding Stuff

                                        // Set number of players
                                        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                                        {
                                            menuDataScript.NumberPlayers = 2;
                                        } //end if
                                        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                                        {
                                            menuDataScript.NumberPlayers = 3;
                                        } //end else if
                                        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                                        {
                                            menuDataScript.NumberPlayers = 4;
                                        } //end else if
                                        #endregion
                                        
                                        // Note: No need to repeat code if it's the same and not dependent on things inside such circumstances- Damien
                                        // Note: I screwed this up previously, but it still shouldn't have to be repeated so do a simple conditional check
                                        if (menuDataScript.NumberPlayers >= 2 && menuDataScript.NumberPlayers <= 4)
                                        {
                                            // Display loading text
                                            guideText.text = "Loading, please wait.";

                                            // Transition into game state
                                            programState = OverallState.Game;
                                        }
                                    } // end if
                                    break;
                                } // end case Multi
                            // Credits - Display names and instructions
                            case MenuState.Credits:
                                {
                                    // Hide menu if not cleared yet
                                    if (isMenuDisplayed)
                                    {
                                        DisableMainMenu();
                                    } // end if

                                    if (!isCreditsDisplayed)
                                    {
                                        EnableCredits();
                                    } // end if
                                    break;
                                } // end case Credits
                            // Help - Displays the instructions for the game
                            case MenuState.Help:
                                {
                                    // Clear buttons and display instructions if not cleared yet
                                    if (isMenuDisplayed)
                                    {
                                        DisableMainMenu();
                                    } // end if

                                    if (!isInstructionsDisplayed)
                                    {
                                        EnableInstructions();
                                    } // end if
                                    break;
                                } // end case Help
                            // Quit - Change program to End to wrap up any loose ends
                            case MenuState.Quit:
                                {
                                    // Hide menu if not cleared yet
                                    if (isMenuDisplayed)
                                    {
                                        DisableMainMenu();
                                    } // end if

                                    // Move to end of program
                                    programState = OverallState.End;
                                    break;
                                } // end case Quit
                        } // end switch menuState
                        break;
                    } // end case Menu
				// Game
                case OverallState.Game:
                    {
                        // GAMEPLAY ENTRY POINT
                        // Reset states
                        programState = OverallState.Menu;
                        menuState = MenuState.Home;

                        // Play correct background music
                        if (mapSelection == "area01")
                        {
                            AudioManager.Instance.PlayDesert();
                        } // end if
                        else if (mapSelection == "area02")
                        {
                            AudioManager.Instance.PlayEuro();
                        } // end else if
                        else if (mapSelection == "area03")
                        {
                            AudioManager.Instance.PlayMetro();
                        } // end else if
                        else if (mapSelection == "area04")
                        {
                            AudioManager.Instance.PlaySnow();
                        } // end else if 
                        else
                        {
                            AudioManager.Instance.PlayMenu();
                        } // end else

                        // Load selected level
                        Application.LoadLevel(mapSelection);
                        break;
                    } // end case Game
				// End
                case OverallState.End:
                    {
                        // END ENTRY POINT - WATCH OUT UNIVERSE!
                        // Wrap up any loose ends here since the program is now exiting.

                        // Destroy Audio Manager
                        if (AudioManager.Instance != null)
                        {
                            Destroy(AudioManager.Instance.gameObject);
                        } // end if

                        // Reset state machine to initial
                        menuState = MenuState.Home;
                        programState = OverallState.Intro;

                        // Reset objected to initial point
                        EnableTitle();
                        DisableMainMenu();

                        // Reset time holder to initial
                        timeHolder = Time.time + 3.0f;

                        // Exit program if possible
                        Application.Quit();

                        break;
                    } // end case End
				// DEFAULT - Catches any exceptions end prints errored state
                default:
                    {
                        Debug.LogErrorFormat("Error - No program state {0} found.", programState);
                        break;
                    } // end case default
            } // end switch programState
		} // end Update

        // Note: Function names always start with a capital letter - Damien
        
        // State machine functions
		// Program state
		public int GetState()
		{
			return (int)programState;
		} // end GetState
		
		// Menu state
		public int GetMenu()
		{
			return (int)menuState;
		} // end GetMenu

		// Menu state settings
        // Enable the intro screen
		void EnableTitle()
		{
			introSet.SetActive (true);
		} // end EnableTitle

        // Disable the intro screen
        void DisableTitle()
		{
			introSet.SetActive (false);
		} // end DisableTitle

        // Enable the main menu screen
        void EnableMainMenu()
		{
			menuSet.SetActive (true);
			backButton.SetActive (false);
			isMenuDisplayed = true;
		} // end EnableMainMenu

        // Disable the main menu screen
        void DisableMainMenu()
		{
			menuSet.SetActive (false);
			backButton.SetActive (true);
			isMenuDisplayed = false;
		} // end DisableMainMenu

        // Enable the level select screen
        void EnableMapSelection()
		{
			mapSet.SetActive (true);
			guideText.gameObject.SetActive(true);
			isMapsDisplayed = true;
		} // end EnableMapSelection

        // Disable the level select screen
        void DisableMapSelection()
		{
			mapSet.SetActive (false);
			guideText.gameObject.SetActive (false);
			isMapsDisplayed = false;
		} // end DisableMapSelection

        // Enable the instructions screen
        void EnableInstructions()
		{
			instructionsSet.SetActive (true);
			isInstructionsDisplayed = true;
		} // end EnableInstructions

        // Disable the instructions screen
        void DisableInstructions()
		{
			instructionsSet.SetActive (false);
			isInstructionsDisplayed = false;
		} // end DisableInstructions

        // Enable the credits screen
        void EnableCredits()
		{
			creditsSet.SetActive (true);
			isCreditsDisplayed = true;
		} // end EnableCredits

        // Disable the credits screen
        void DisableCredits()
		{
			creditsSet.SetActive (false);
			isCreditsDisplayed = false;
		} // end DisableCredits

		// Menu Button functions
        // Solo button
		public void SoloGame()
		{
			menuState = MenuState.Solo;
		} // end SoloGame

        // Multi button
        public void MultiGame()
		{
			menuState = MenuState.Multi;
		} // end MultiGame

        // Help button
        public void Help()
		{
			menuState = MenuState.Help;
		} // end Help

        // Credits button
        public void Credits()
		{
			menuState = MenuState.Credits;
		} // end Credits

        // Quit button
        public void QuitGame()
		{
			menuState = MenuState.Quit;
		} // end QuitGame

        // Back button
        public void Back()
		{
			if(menuState == MenuState.Credits)
			{
				DisableCredits();
				menuState = MenuState.Home;
			} // end if
			else if(menuState == MenuState.Help)
			{
				DisableInstructions();
				menuState = MenuState.Home;
			} // end else if
			else if(menuState == MenuState.Solo || menuState == MenuState.Multi)
			{
				DisableMapSelection();
				mapSelection = "nothing";
				guideText.text = "Please select a map";
				menuState = MenuState.Home;
			} // end else if
			else
			{
				menuState = MenuState.Home;
			} // end else
		} // end Back()

		// Desert map button
        public void DesertMap()
		{
			mapSelection = "area01";
		} // end DesertMap

        // Euro map button
        public void EuroMap()
		{
			mapSelection = "area02";
		} // end EuroMap

        // Metro map button
        public void MetroMap()
		{
			mapSelection = "area03";
		} // end MetroMap

        // Snow map button
        public void SnowMap()
		{
			mapSelection = "area04";
		} // end SnowMap
    } // end BrentsStateMachine
} //end GSP
