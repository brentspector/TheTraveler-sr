/*******************************************************************************
 *
 *  File Name: GameplayStateMachine.cs
 *
 *  Description: The brains of the program, controls the flow
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace GSP
{

    /*******************************************************************************
     *
     * Name: GameplayStateMachine
     * 
     * Description: Controls the flow of the game through a finite state machine.
     * 
     *******************************************************************************/
    public class GameplayStateMachine : MonoBehaviour 
	{
		GamePlayState gamePlayState;  // The current state

		bool canInitAfterStart = false;  // Initialising values after Start()

		bool isColorActionButtonDefault = true;   // The default colour of the action button
		bool isColorResourceButtonDefault = true; // The default colour of the resource button
        bool isGUIActionPressed = false;	        // Determines if the Action Button has been pressed
        bool canGUIShowResources = false;	        // GUI for Resources can be hidden at push of button
		double animTimer = 0.0;                 // The animater timer
		string guiActionString;			        // Changes the String in the Action button According to State player is in
		string mapEventString; 			        // The type of action event will occur
		string mapEventResultString; 		    // If mapEvent is Item, what Resource is picked up? Null if not a resource event
		int guiPlayerTurn = 0;	                // Whos turn is it
		int guiGoldVal = -1;		            // The player's Gold Value; if GUI displays -1, value was not received from player
        int guiWeight = -1;		                // The player's actual weight; if GUI displays -1, value was not received from player
        int guiMaxWeight = -1;	                // The player's max weight; if GUI displays -1, value was not received from player
        int guiOre = -1;			            // The player's number of ore; if GUI displays -1, value was not received from player
        int guiWool = -1;			            // The player's number of wool; if GUI displays -1, value was not received from player
        int guiWood = -1;			            // The player's number of wood; if GUI displays -1, value was not received from player
        int guiFish = -1;			            // The player's number of fish; if GUI displays -1, value was not received from player
        int guiDiceDistVal = 0;	                // The dice value which is then onverted into a distance value
        int guiNumOfPlayers = 2; 	            // The number of players playing
		
		DieInput dieScript;                         // The DieInput script reference
		GUIMapEvents guiMapEventsScript;            // The GUIMapEvent script reference
        GUIMovement guiMovementScript;              // The GUIMovementScript reference
        MapEvent mapEventScript;                    // The MapEvent script reference
		JAVIERGUI.GUIBottomBar guiBottomBarScript;  // The GUIBottomBat reference that contains the item and ally buttons

        GameObject endSceneCharData;    // The GameObject reference for the EndSceneCharData
        bool canRunEndStuff;            // Whether the end scene stuff should be ran during that state.
		
		GameObject playerEntity;                            // The player GameObject
        List<GameObject> players;                           // The list of the player GameObject's
		List<GSP.Char.Character> playerScripts;             // The list of the player's Character scripts
		List<GSP.Char.ResourceList> playerResourceLists;    // The list of the player's ResourceList scripts

		GameObject audioSrc;    // The GameObject that contains the AudioSource

		// Runs when the object if first instantiated, because this object will occur once through the game,
        // these values are the beginning of game values
        // Note: Values should be updated at the EndTurn State
        void Start()
		{
            //TODO: Damien: Replace Tile stuff later
            // Clear the tile dictionary
			TileDictionary.Clean();
			// Set the dimensions and generate/add the tiles
			TileManager.SetDimensions(64, 20, 16);
			TileManager.GenerateAndAddTiles();

            //TODO: Damien: Replace with the GameMaster functionality later.
            // Create an empty GameObject for the EndSceneCharData
            endSceneCharData = new GameObject("EndSceneCharData");
            // Tag it as EndSceneCharData
            endSceneCharData.tag = "EndSceneCharDataTag";
            // Add the EndSceneCharData component
            endSceneCharData.AddComponent<EndSceneData>();
            // Set it to not destroy on load
            DontDestroyOnLoad(endSceneCharData);
            // Running the end stuff defaults to true
            canRunEndStuff = true;

            //TODO: Damien: Replace with the GameMaster functionality later.
            // Get the GameObject with the MenuDataTag tag
			GameObject menuData = GameObject.FindGameObjectWithTag("MenuDataTag");
            // Only proceed if the MenuData object isn't null
			if (menuData != null)
			{
				// Get its MenuData script reference
				MenuData menuDataScript = menuData.GetComponent<MenuData>();
				
				// Copy the value into the state machine
				guiNumOfPlayers = menuDataScript.NumberPlayers;
			}
			// Finally, destroy the menu data object
			Destroy(menuData);

            //TODO: Damien: Replace with the GameMaster functionality later
            // Initialise the lists
			players = new List<GameObject>();
			playerScripts = new List<Character>();
            playerResourceLists = new List<ResourceList>();

            //TODO: Damien: Replace with the GameMaster functionality later
            // Add the player instances
			AddPlayers(guiNumOfPlayers);

			// Get script references
			dieScript = GameObject.FindGameObjectWithTag("DieTag").GetComponent<DieInput>();
            //TODO: Brent: Replace the four scripts below with the new In-Game UI later
			guiMapEventsScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GUIMapEvents>();
            guiMovementScript = GameObject.FindGameObjectWithTag("GUIMovementTag").GetComponent<GSP.GUIMovement>();
            mapEventScript = GameObject.FindGameObjectWithTag("DieTag").GetComponent<GSP.MapEvent>();
            guiBottomBarScript = this.GetComponent<GSP.JAVIERGUI.GUIBottomBar>();

			// Reseed the random number generator
            dieScript.Dice.Reseed(System.Environment.TickCount);

			//TODO: Brent: This is probably obsolete now
            // Text for the action button
			guiActionString = "Action\nButton";

			// There isn't a map event in the beginning
			mapEventString = "Nothing";
			mapEventResultString = string.Empty;

			// Initialise other things after Start() runs
			canInitAfterStart = true;

			// Get the AudioSource GameObject
			audioSrc = GameObject.FindGameObjectWithTag("AudioSourceTag");
            
            // Set the state to the BeginTurn state
            gamePlayState = GamePlayState.BeginTurn;
		} // end Start

		// Initialises things after the Start() function runs
        void InitAfterStart()
		{
			// Add the player instances
			AddItems(guiNumOfPlayers);
		} // end InitAfterStart

        //TODO: Damien: Replace with the GameMaster functionality later
        // Adds the players to the game
        void AddPlayers(int numPlayers)
		{
			Vector3 startingPos = new Vector3(.32f, -(TileManager.MaxHeight / 2.0f), -1.6f);   // The first tile

			// Loop over the number of players to add their instances
            for (int count = 0; count < numPlayers; count++) 
			{
				// Calculate the y starting position
                startingPos.y = .32f -((count + 1) * .64f); 
				// Create the player
				playerEntity = Instantiate(PrefabReference.prefabCharacter, startingPos, Quaternion.identity) as GameObject;
				playerEntity.transform.localScale = new Vector3(1, 1, 1);

				// Give the player a RigidBody2D component to allow for collisions
				var rigidBody2D = playerEntity.AddComponent<Rigidbody2D>();
				// This is a 2D game so turn off gravity
				rigidBody2D.gravityScale = 0.0f;

				// Give the player a BoxCollider2D component to add collisions
				playerEntity.AddComponent<BoxCollider2D>();

                // Add the player to the players list
                players.Add(playerEntity);
				
				// Add the player's character script to the playerScripts list
                playerScripts.Add(playerEntity.GetComponent<Character>());
                // Add the player's character script to the playerResourceLists list
				playerResourceLists.Add(playerEntity.GetComponent<ResourceList>());

				// Set the character's sprite sheet sprites
				playerScripts[count].SetCharacterSprites(count + 1);

				// Set the character's facing
				playerScripts[count].Face(FacingDirection.South);
			} // end for
		} // end AddPlayers

		//TODO: Damien: Replace with the GameMaster functionality later
        // Adds the starting items to the players
        void AddItems(int numPlayers)
		{
			// Loop over the number of players to give them the items
            for (int count = 0; count < numPlayers; count++) 
			{
				playerScripts[count].EquipItem("SWORD", 0);
				playerScripts[count].EquipItem("CHAINLEGS", 0);
			} // end for
		} // end AddItems

		//TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Gets the player's values for display on the In-Game UI; At the beginning of each turn the values are grabbed
        // from each player and stored into respective GUI variables.
        void GetPlayerValues()
		{
			// The gold value
            guiGoldVal = playerScripts[guiPlayerTurn].Currency;

			// The Weight Values; Max and Current weight
            guiMaxWeight = playerScripts[guiPlayerTurn].MaxWeight;
            guiWeight = playerScripts[guiPlayerTurn].ResourceWeight;
			
			// The resource values
            guiOre = playerResourceLists[guiPlayerTurn].GetResourcesByType("Ore").Count;
            guiWool = playerResourceLists[guiPlayerTurn].GetResourcesByType("Wool").Count;
            guiWood = playerResourceLists[guiPlayerTurn].GetResourcesByType("Wood").Count;
            guiFish = playerResourceLists[guiPlayerTurn].GetResourcesByType("Fish").Count;
		} // end GetPlayerValues

        //TODO: Damien Replace other functionality with GameMaster functionality or something later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The old style GUI functioned using an OnGUI function; Runs each frame
        void OnGUI()
		{
            // This was set to true at the end of Start()
            if(canInitAfterStart)
			{
				// Initialise stuff after start
                InitAfterStart();

                // DON'T change this value to true. ever! This is meant to only run once.
                canInitAfterStart = false;
			} // end if
			else
			{
				// Otherwise, Check if it's the end of the game
                IsItEndOfGame();

                // Update any values that affect GUI before creating GUI
                StateMachine();
				
				// Buttons will be red
				GUI.backgroundColor = Color.red;
				
				// This is the tool bar container
				GUI.Box(new Rect(0, 0, Screen.width ,64), " ");
				
				//Scalable values for GUI mini-containers
				int gap = 2;
				int width = (Screen.width / 4) - 2;
				int height = 28;
				
				//..................................
				//   ...PLAYER AND GOLD COLUMN...
				//..................................
				int col = 0;
				GUIFirstColumn(col, gap, width, height);
				
				//..................................
				// ...WEIGHT AND RESOURCE COLUMN...
				//..................................
				col++;
				GUISecondColumn(col, gap , width, height);

				
				//..................................
				//      ...DICE ROLL COLUMN...
				//..................................
				col++;
				GUIThirdColumn(col, gap, width, height);

				
				//..................................
				//      ...ACTION COLUMN...
				//..................................
				col++;
				GUIFourthColumn(col, gap, width, height);
			} // end else
		} // end OnGUI

        //TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The first column for the OnGUI system
        void GUIFirstColumn (int col, int gap, int width, int height)
		{
			// Temporary player based on the turn
            int tmpPlayer = guiPlayerTurn + 1;

			// The player container tells whos turn it is
			GUI.Box(new Rect((col * width) + (col + 1) * gap, 2, width, height), "Player: " + tmpPlayer.ToString());

			// The gold Container tell how much gold they have
			GUI.Box(new Rect((col * width) + (col + 1) * gap, 32, width, height), "Gold: $" + guiGoldVal.ToString());
		
		} // end GUIFirstColumn

        //TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The second column for the OnGUI system
        void GUISecondColumn (int col, int gap, int width, int height)
		{
			// The weight container
			GUI.Box(new Rect((col * width) + (col + 1) * gap, 2, width, height), "Weight:" + guiWeight.ToString() + "/" + guiMaxWeight.ToString());

			// The resource button
			ResourceButtonConfig(gap, col, width, height);

			// Show/hide the resources UI
			ShowResources();
		} // end GUISecondColumn

        //TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The third column of the OnGUI system
        void GUIThirdColumn (int col, int gap, int width, int height)
		{
			// Confiure the dice box
            DiceBoxConfig(gap, col, width, height);	
		} // end GUIThirdColumn

        //TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The fourth column of the OnGUI system
        void GUIFourthColumn (int col, int gap, int width, int height)
		{
			// Configure the action button
            ActionButtonConfig (gap, col, width, height);
		} // end GUIFourthColumn

        //TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Shows or hides the resources UI for the OnGUI system
        void ShowResources()
		{
			// Show we show the resources UI?
            if (canGUIShowResources) 
			{
				// Resources GUI container attributes
				const int numberOfResources = 4;
				int gap = 2;
                int width = (Screen.width / numberOfResources) - 2;
				int height = 28;
				
				//...............
				//   ...Ore...
				//..............
				int col = 0;
                GUI.Box(new Rect((col * width) + (col + 1) * gap, 64, width, height), "Ore: " + guiOre.ToString());
				
				//...............
				//  ...Wool...
				//...............
				col++;
                GUI.Box(new Rect((col * width) + (col + 1) * gap, 64, width, height), "Wool: " + guiWool.ToString());

				//..............
				// ...Wood....
				//..............
				col++;
                GUI.Box(new Rect((col * width) + (col + 1) * gap, 64, width, height), "Wood: " + guiWood.ToString());

				//.............
				// ...Fish...
				//.............
				col++;
                GUI.Box(new Rect((col * width) + (col + 1) * gap, 64, width, height), "Fish: " + guiFish.ToString());
			} // end if
			
		} // end ShowResources

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the resource button for the OnGUI system
		void ResourceButtonConfig(int p_gap, int p_col, int p_width, int p_height)
		{
			// Animate according to whether the color is default or not
            AnimTimer(isColorResourceButtonDefault);

            // Create the resource button's functionality for toggling the resource UI
			if (GUI.Button (new Rect ((p_col * p_width) + (p_col + 1) * p_gap, 32, p_width, p_height),"Resources"))
			{
				// Set the default value
				isColorResourceButtonDefault = true;

				// Toggle between showing and hiding the resource UI
                if (!canGUIShowResources)
                {
                    canGUIShowResources = true;
                } // end if canGUIShowResources
				else
                {
                    canGUIShowResources = false;
                } // end else
			} // end if
		} // end ResourceButtonConfig


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the dice box for the OnGUI system
        // When the Dice has not been rolled, is should display "DICE ROLL [Press Action Button]"
        private void DiceBoxConfig(int p_gap, int p_col, int p_width, int p_height)
		{
			// If in the RollDice state, tell the player to push the action button to begin rolling the Dice
			if (gamePlayState == GamePlayState.RollDice) 
			{
                GUI.Box(new Rect((p_col * p_width) + (p_col + 1) * p_gap, 2, p_width, 2 * p_height), "Dice Roll\n[Press Action\nButton]");
			} // end if 
			else
			{
                // Otherwise, display the rolled Value
                GUI.Box(new Rect((p_col * p_width) + (p_col + 1) * p_gap, 2, p_width, 2 * p_height), "Travel Dist.\n\n" + guiDiceDistVal);
			} // end else
		} // end DiceBoxConfig

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the action button for the OnGUI system
        private void ActionButtonConfig( int gap, int col, int width, int height )
		{
            // Animate according to whether the color is default or not
            AnimTimer(isColorActionButtonDefault);

			// Ensures the action button only gets pressed once per RollDie state
			if (!isGUIActionPressed) 
			{
                if (GUI.Button(new Rect((col * width) + (col + 1) * gap, 2, width, 2 * height), guiActionString))
				{
					// The action button has been pressed
                    isGUIActionPressed = true;
				} // end if OnGUI-button-press
			} 
			else 
			{
                // Create a box instead of a button, this makes it unclickable
                GUI.Box(new Rect((col * width) + (col + 1) * gap, 2, width, 2 * height), guiActionString);
			} // end else
		} // end ActionButtonConfig

		//TODO: I think this is obsolete, don't need a key to end the game anymore
        // Checks if the end of the game
        void IsItEndOfGame()
		{
			// Check if the q key has been pressed
            if (Input.GetKeyDown(KeyCode.Q))
			{
				// Trigger the end of the game
				EndGame();
			} // end if
		} // end IsItEndOfGame

		// Changes the state to EndGame which ends the game.
        public void EndGame()
		{
			gamePlayState = GamePlayState.EndGame;
		} // end EndGame

        //TODO: Damien: Replace with the GameMaster functionality later
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Controls the flow of the game through various states
        void StateMachine()
		{
			// Get the GameObject with the GUITextTag tag to display the state
            var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>();

            // Switch over the GamePlayState's
			switch (gamePlayState) 
			{
                // The player begins their turn.
                case GamePlayState.BeginTurn:
                    {
                        //Get the player's values and update them
                        GetPlayerValues();
                        guiBottomBarScript.RefreshBottomBarGUI(players[guiPlayerTurn]);

                        // Switch the state to the RollDie state
                        gamePlayState = GamePlayState.RollDice;
                        break;
                    } // end Case BeginTurn
                
                // The player rolls the dice
                case GamePlayState.RollDice:
                    {
                        // The players list is zero-index based but the GUI needs to start Display with player "1"
                        int tmpPlayerTurn = guiPlayerTurn + 1;

                        // Set the state text
                        state.text = "Player " + tmpPlayerTurn.ToString() + " roll dice";

                        // Set the action button's text to roll dice
                        guiActionString = "Action\nRoll Dice";

                        // Highlight the action button
                        isColorActionButtonDefault = false;

                        // Check if the button is clicked, destroy button
                        if (isGUIActionPressed)
                        {
                            // Stop the animation
                            isColorActionButtonDefault = true;

                            // Roll the die
                            guiDiceDistVal = dieScript.Dice.Roll();

                            //TODO: Brent: Replace with AudioManager later
                            //Play die roll sound
                            //audioSrc.GetComponent<AudioSource>().PlayOneShot(GSP.AudioReference.sfxDice);

                            // Change to the CalcDistance State
                            gamePlayState = GamePlayState.CalcDistance;
                        } // end if
                        break;
                    } // end Case RollDice
                
                // Calculate the allowed disance to travel
                case GamePlayState.CalcDistance:
                    {
                        // Set the state text
                        state.text = "Calculate Distance";

                        // Get the dice's value and calculate the allowed movement
                        guiDiceDistVal = guiDiceDistVal * (guiMaxWeight - guiWeight) / guiMaxWeight;

                        // Make the action button clickable again
                        isGUIActionPressed = false;

                        // Change the state to the DisplayDistance state
                        gamePlayState = GamePlayState.DisplayDistance;
                        break;
                    } // end Case CalcDistance
                
                // Display the distance allowed to travel
                case GamePlayState.DisplayDistance:
                    {
                        // Set the state text
                        state.text = "DisplayDistance";

                        // Change the state to the SelectPathToTake state
                        gamePlayState = GamePlayState.SelectPathToTake;

                        // Display the movement arrows
                        guiMovementScript.InitThis(players[guiPlayerTurn], guiDiceDistVal);
                        break;
                    } // end Case DisplayDistance
                
                // The player selects their path to take on the map
                case GamePlayState.SelectPathToTake:
                    {
                        // Set the state text
                        state.text = "Select Path To Take\nPress Action butotn to End Turn\nor X to start over.";
                        // Set the action button's text to end turn
                        guiActionString = "End Turn";

                        // Update the value of allowed travel distance upon the player pressing a move button
                        guiDiceDistVal = guiMovementScript.RemainingTravelDistance;

                        // Check if the action button has been pressed
                        if (isGUIActionPressed)
                        {
                            // Change the state to the DoAction state
                            gamePlayState = GamePlayState.DoAction;

                            // Determine the MapEvent event
                            mapEventString = mapEventScript.DetermineEvent(players[guiPlayerTurn]);
                            mapEventResultString = mapEventScript.GetResultString();
                            // Initialise the OnGUI UI for the MapEvent event
                            guiMapEventsScript.InitThis(players[guiPlayerTurn], mapEventString, mapEventResultString);
                        } // end if
                        break;
                    } // end Case SelectPathToTake
                
                // Deal with a MapEvent action
                case GamePlayState.DoAction:
                    {
                        // Set the state text
                        state.text = "";

                        // Get the player's values
                        GetPlayerValues();

                        // Check if a MapEvent's action is running
                        if (!guiMapEventsScript.IsActionRunning())
                        {
                            // The MapEvent's action isn't running so reset the values.
                            mapEventString = "Nothing";
                            mapEventResultString = string.Empty;

                            // Change the state to the EndTurn state
                            gamePlayState = GamePlayState.EndTurn;
                            // Make the action button clickable again
                            isGUIActionPressed = false;
                        }
                        break;
                    } // end Case DoAction
                
                // The player has ended their turn
                case GamePlayState.EndTurn:
                    {
                        // Set the state text
                        state.text = "End Turn";

                        // Clear the board of any highlight tiles.
                        Highlight.ClearHighlight();

                        // Its now thw next players turn
                        guiPlayerTurn++;

                        // Loop back to player 1 if we reached the end of the players turns
                        if (guiPlayerTurn >= guiNumOfPlayers)
                        {
                            // The player's list is zero-index based so setting to zero is player 1.
                            guiPlayerTurn = 0;
                        }

                        // Change the state to the BeginTurn state
                        gamePlayState = GamePlayState.BeginTurn;
                        break;
                    } // end Case EndTurn
                
                // Its the end of the game
                case GamePlayState.EndGame:
                    {
                        // Set the state text
                        state.text = "Universe Ending";

                        // Check whether to run the end stuff
                        if (canRunEndStuff)
                        {
                            // Only run this once
                            canRunEndStuff = false;

                            // Get the number of players
                            int numPlayers = GetNumOfPlayers();

                            // Loop through and sell the character's resources and their ally's resources
                            for (int playerSellIndex = 0; playerSellIndex < numPlayers; playerSellIndex++)
                            {
                                // We need to access the character script at the given index and sell the resources
                                playerScripts[playerSellIndex].SellResource();
                            } // end for

                            // Get the EndSceneData object's script
                            EndSceneData endSceneScript = endSceneCharData.GetComponent<EndSceneData>();

                            // Loop over the number of players and add the player stuff
                            for (int endSceneIndex = 0; endSceneIndex < numPlayers; endSceneIndex++)
                            {
                                // Add the current player to the EndScenedata object
                                // Note: Be sure to add 1 to index to get the player number correct. This is because the players list is zero-index based
                                int playerNum = endSceneIndex + 1;

                                // Check if the key doesn't exist. Only proceed if it doesn't.
                                if (!endSceneScript.KeyExists(playerNum))
                                {
                                    endSceneScript.AddData(playerNum, players[endSceneIndex]);
                                } // end if
                            } // end for

                            // Finally, load the end scene
                            Application.LoadLevel("EndScene");
                        }
                        break;
                    } // end Case EndGame
                
                // Shouldn't reach here, this is reached if the state isn't listed above
                default:
                    {
                        Debug.LogWarning("Hit the default case in GamePlayStateMachine!");
                        break;
                    } // end Case default
            } // end switch gamePlayState
		} // end StateMachine
		
		// Gets the current state
        public int GetState()
		{
            return (int)gamePlayState;
		} // end GetState
		
		// Changes the current state to the next state
        public void NextState()
		{
			gamePlayState++;
		} // end NextState

		// Does an action; used for dealing with MapEvent actions
        public void DoAction(string mapEventType, string typeOfResource)
		{
			// Change to the DoAction state
			gamePlayState = GamePlayState.DoAction;

			// Set MapEvent types
			mapEventString = mapEventType;
			mapEventResultString = typeOfResource;
		} // end DoAction

		// Change to the EndTurn state
        public void EndTurn()
		{
			gamePlayState = GamePlayState.EndTurn;
			
		} // end EndTurn


        //TODO: Damien: Replace with the GameMaster functionality later; probably obsolete
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later; probably obsolete
        // Resets values at the end of a turn right before a new player's turn
        void ResetValues()
		{
			guiActionString = "Action\nButton";
            isGUIActionPressed = false;
            guiGoldVal = 0;
            guiWeight = 0;
            guiMaxWeight = 100;
            canGUIShowResources = false;
            guiOre = 0;
            guiWool = 0;
            guiDiceDistVal = 0;
		} // end ResetValues

        //TODO: Damien: Replace with the GameMaster functionality later; probably obsolete
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later; probably obsolete
        // Gets the current player from the players list
        public GameObject GetCurrentPlayer()
		{
			return	players[guiPlayerTurn];
		} // end GetCurrentPlayer

        //TODO: Damien: Replace with the GameMaster functionality later; probably obsolete
        // Gets the number of players
        public int GetNumOfPlayers()
		{
			return guiNumOfPlayers;
		} // end GetNumOfPlayers
	
		//TODO: probably obsolete
        // Animates the action button for the OnGUI system
        public void AnimateActionButton(bool isPlayRed)
		{
			isColorActionButtonDefault = isPlayRed;
		} // end AnimateActionButton

        //TODO: probably obsolete
        // Animates the resource button for the OnGUI system
        public void AnimateResourceButton(bool isPlayRed)
		{
			isColorResourceButtonDefault = isPlayRed;
		} // end AnimateResourceButton

		// TODO: probably obsolete
        // Times the animation between switching background colours for the OnGUI system
        void AnimTimer(bool isAnimating)
		{
			// Accumulate the delta time
            animTimer += Time.deltaTime;

            // Reset every two seconds
			if(animTimer > 2.0f)
			{
                animTimer = 0.0f;
			} // end if

			// Proceed if we are animating or the time is less than a second
            if((isAnimating == true) || (animTimer < 1.0f))
			{
				// Colour the GUI background red.
                GUI.backgroundColor = Color.red;
			} // end if
			else
			{
				// Otherwise, colour the GUI background yellow
                GUI.backgroundColor = Color.yellow;
			} // end else
		} // end AnimTimer

        // TODO: probably obsolete
        // Stops the animation for the OnGUI system
        public void StopAnimation()
		{
			// Set both colour booleans to true to stop the animation
            isColorResourceButtonDefault = true;
			isColorActionButtonDefault = true;
		} // end StopAnimation
	} // end GameplayStateMachine
} // end GSP
