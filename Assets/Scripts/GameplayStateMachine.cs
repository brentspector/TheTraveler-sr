/*******************************************************************************
 *
 *  File Name: GameplayStateMachine.cs
 *
 *  Description: The brains of the program, controls the flow
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Core;
using GSP.Entities.Neutrals;
using GSP.Items;
using GSP.Tiles;
using System;
using UnityEngine;
using UnityEngine.UI;

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
		// The states that the GPSM runs on
		enum GamePlayState
		{
			BeginTurn,
			RollDice,
			CalcDistance,
			DisplayDistance,
			SelectPathToTake,
			DoAction,
			EndTurn,
			EndGame
		} //end GamePlayState

		// State Machine management variables
		GamePlayState gamePlayState;            	// The current state
		bool canInitAfterStart;			         	// Initialising values after Start()
		bool canRunEndStuff;                    	// Whether the end scene stuff should be ran during that state.
		int guiDiceDistVal;	                		// The dice value which is then onverted into a distance value

		// State Machine input/output variables
		int guiNumOfPlayers; 	           			// The number of players playing
		string mapEventResultString; 		    	// If mapEvent is Item, what Resource is picked up? Null if not a resource event
		int guiPlayerTurn;  	                	// Whos turn is it
		int guiMaxWeight;							// Maximum weight player can carry
		int guiCurrentWeight;						// Total weight of player at the moment

		// HUD Elements
		// Current Player
		Text guiPlayerName;							// Name of current player
		Text guiTurnText; 			       			// The turn or event currently happening
		Text guiGold;		            			// The player's Gold Value
       	Text guiWeight;		                		// The player's actual weight
		Button actionButton;						// Button user presses to advance turn phase
		Text actionButtonText;						// Text of actionButton 
		GameObject acceptPanel;						// Panel for accepting an ally
		// All Players
		GameObject imageParent;						// Panel with all player images
		GameObject textParent;						// Panel with all player names and gold

		// Game Objects
		Die die;       			                    // The Die to be used in the game
        GUIMovement guiMovement;		            // The GUIMovement component reference
		MapEvent guiMapEvent;						// The MapEvent component reference
		string mapEventResult;						// Result of the map event

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

			//Get HUD elements
			guiPlayerName = GameObject.Find ("PlayerName").GetComponent<Text> ();
			guiTurnText = GameObject.Find ("TurnPhase").GetComponent<Text> ();
			guiGold = GameObject.Find ("Gold").GetComponent<Text> ();
			guiWeight = GameObject.Find ("Weight").GetComponent<Text> ();
			actionButton = GameObject.Find ("ActionButton").GetComponent<Button> ();
			actionButtonText = GameObject.Find ("ActionButton").GetComponentInChildren<Text> ();
			acceptPanel = GameObject.Find ("Accept");
			acceptPanel.SetActive (false);
			imageParent = GameObject.Find ("ImageOrganizer");
			textParent = GameObject.Find ("TextOrganizer");

			// Running the start stuff defaults to false
			canInitAfterStart = false;

            // Running the end stuff defaults to true
            canRunEndStuff = true;

            // Get the number of players
            guiNumOfPlayers = GameMaster.Instance.NumPlayers;

            // Set the turn
            guiPlayerTurn = GameMaster.Instance.Turn;

			//Set starting values             		            
			int guiDiceDistVal = 0;	                

			// Create a die
			die = new Die();
			die.Reseed ((int)Time.time);

			// Get movement and map event components
			guiMovement = GameObject.Find ("Canvas").GetComponent<GUIMovement> ();
			guiMapEvent = GameObject.Find ("Canvas").GetComponent<MapEvent> ();

			// Reseed the random number generator
			die.Reseed (Environment.TickCount);

			// There isn't a map event in the beginning
			mapEventResultString = string.Empty;

			// Initialise other things after Start() runs
			canInitAfterStart = true;
            
            // Set the state to the BeginTurn state
            gamePlayState = GamePlayState.BeginTurn;
		} // end Start

		// Initialises things after the Start() function runs
        void InitAfterStart()
		{
            // Add the player instances
            AddPlayers(guiNumOfPlayers);
            
            // Add the player instances
			AddItems(guiNumOfPlayers);

			// Update the "All Players" HUD to match active players
			// Disable unused players
			for(int i = 3; i > guiNumOfPlayers - 1 ; i--)
			{
				imageParent.transform.GetChild(i).gameObject.SetActive(false);
				textParent.transform.GetChild(i).gameObject.SetActive(false);
			} //end for

			// Update the remaining fields with name and gold
			for(int i = 0; i < guiNumOfPlayers; i++)
			{
				// Set the player's merchant entity
				Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(i).Entity;

				// Get text component and update with player name and gold
				textParent.transform.GetChild(i).GetComponent<Text>().text =
					GameMaster.Instance.GetPlayerName(i) + " - " + playerMerchant.Currency;
			} //end for
		} // end InitAfterStart

        // Adds the players to the game
        void AddPlayers(int numPlayers)
		{
			// Create the players
            GameMaster.Instance.CreatePlayers();

            // Loop over the number of players to add their instances
            for (int count = 0; count < numPlayers; count++) 
			{
                //TODO: Damien: Change this later when you do the player renaming
                int playerNum = count + 1;
                GameMaster.Instance.SetPlayerName(count, playerNum.ToString());
                
                // Set the player's script
                Player playerScript = GameMaster.Instance.GetPlayerScript(count);

                // Set the players's sprite sheet sprites
                playerScript.SetCharacterSprites(count + 1);

                // Set the player's facing
                playerScript.Face(FacingDirection.South);
			} // end for
		} // end AddPlayers

        // Adds the starting items to the players
        void AddItems(int numPlayers)
		{
			// Loop over the number of players to give them the items
            for (int count = 0; count < numPlayers; count++) 
			{
                // Set the player's merchant entity
                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(count).Entity;

                // Equip a sword and chainlegs on the player
                playerMerchant.EquipWeapon(GameMaster.Instance.CreateWeapon(WeaponType.Sword));
                playerMerchant.EquipArmor(GameMaster.Instance.CreateArmor(ArmorType.Chainlegs));
			} // end for
		} // end AddItems

        // Gets the player's values for display on the In-Game UI; At the beginning of each turn the values are grabbed
        // from each player and stored into respective GUI variables.
        void GetPlayerValues()
		{
            // Set the player's merchant entity
            Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(guiPlayerTurn).Entity;
            
			//Set Input variables
			// Total Weight
			guiCurrentWeight = playerMerchant.TotalWeight;

			// Max Weight
			guiMaxWeight = playerMerchant.MaxWeight;

			// Set HUD elements
			// Name
			guiPlayerName.text = GameMaster.Instance.GetPlayerName(guiPlayerTurn);

            // Gold value
            guiGold.text = "Gold: " + playerMerchant.Currency;

			// The Weight Values; Current weight / Max weight
            guiWeight.text = "Weight: " + guiCurrentWeight + "/" + guiMaxWeight;
		} // end GetPlayerValues

        // Updates the state machine and things; runs every frame
        void Update()
        {
            // This was set to true at the end of Start()
            if (canInitAfterStart)
            {
                // DON'T change this value to true. ever! This is meant to only run once.
                canInitAfterStart = false;

                // Initialise stuff after start
                InitAfterStart();
            } // end if
            else
            {
                // Update any values that affect GUI before creating GUI
                StateMachine();
            } // end else
        } // end Update

        // Controls the flow of the game through various states
        void StateMachine()
		{
            // Switch over the GamePlayState's
			switch (gamePlayState) 
			{
                // The player begins their turn.
                case GamePlayState.BeginTurn:
                    {
                        // Get the player's values and update them
                        GetPlayerValues();

						// Clear the map event text
						mapEventResult = "";

						//Verify the action button is enabled
						actionButton.interactable = true;

                        // Switch the state to the RollDie state
                        gamePlayState = GamePlayState.RollDice;
                        break;
                    } // end Case BeginTurn
                
                // The player rolls the dice
                case GamePlayState.RollDice:
                    {
                        // Set the turn text
                        guiTurnText.text = "Player " + guiPlayerName.text + ", roll dice.";

                        // Set the action button's text to roll dice
                        actionButtonText.text = "Roll Dice";
				        break;
                    } // end Case RollDice
                
                // Calculate the allowed disance to travel
                case GamePlayState.CalcDistance:
                    {
                        // Set the turn text
                        guiTurnText.text = "Calculating distance...";

                        // Get the dice's value and calculate the allowed movement
                        guiDiceDistVal = guiDiceDistVal * (guiMaxWeight - guiCurrentWeight) / guiMaxWeight;

                        // Change the state to the DisplayDistance state
                        gamePlayState = GamePlayState.DisplayDistance;
                        break;
                    } // end Case CalcDistance
                
                // Display the distance allowed to travel
                case GamePlayState.DisplayDistance:
                    {
                        // Set the player's Player script
                        Player player = GameMaster.Instance.GetPlayerScript(guiPlayerTurn);

                        // Set the state text
                        guiTurnText.text = "Displaying distance...";

                        // Display the movement arrows
                        guiMovement.InitThis(player, guiDiceDistVal);

						// Make the action button clickable again
						actionButton.interactable = true;

						// Change the state to the SelectPathToTake state
						gamePlayState = GamePlayState.SelectPathToTake;
                        break;
                    } // end Case DisplayDistance
                
                // The player selects their path to take on the map
                case GamePlayState.SelectPathToTake:
                    {
                        // Set the state text
                        guiTurnText.text = "Movement Left: " + guiDiceDistVal + "\nPress 'End Turn' when finished.";

                        // Set the action button's text to end turn
                        actionButtonText.text = "End Turn";

                        // Update the value of allowed travel distance upon the player pressing a move button
                        guiDiceDistVal = guiMovement.RemainingTravelDistance;
                        break;
                    } // end Case SelectPathToTake
                
                // Deal with a MapEvent action
                case GamePlayState.DoAction:
                    {
                        // Set the state text
                        guiTurnText.text = "Resolving map event...";

						// Get and resolve the map event
						mapEventResult = guiMapEvent.DetermineEvent(guiPlayerTurn, die);

						// If it's an ally, allow player to accept
						if(mapEventResult.Contains("Ally"))
						{
							// Enable the panel
							acceptPanel.SetActive(true);
					
							// Update the text
							Text eventText = GameObject.Find("EventText").GetComponent<Text>();
							eventText.text = "Porter ally found.\nWould you like to add them?";
						} //end if
						else 
				   		{
							// Return the result for now
							guiTurnText.text = mapEventResult;

							// Enable Action Button
							actionButton.interactable = true;
						} //end if

						// Get the player's values
						GetPlayerValues();

						// Change the state to the EndTurn state
						gamePlayState = GamePlayState.EndTurn;
                        break;
                    } // end Case DoAction
                
                // The player has ended their turn
                case GamePlayState.EndTurn:
                    {
						// Set Action Button text to prompt next player
						actionButtonText.text = "Next Player";
                    
						// Clear the board of any highlight tiles
                        Highlight.ClearHighlight();
                        break;
                    } // end Case EndTurn
                
                // Its the end of the game
                case GamePlayState.EndGame:
                    {
                        // Set the state text
                        guiTurnText.text = "Universe Ending";

                        // Check whether to run the end stuff
                        if (canRunEndStuff)
                        {
                            // Only run this once
                            canRunEndStuff = false;

                            // Loop through and sell the character's resources and their ally's resources
                            // Note: Ally resources are not setup to pickup or sell right now
                            for (int playerSellIndex = 0; playerSellIndex < guiNumOfPlayers; playerSellIndex++)
                            {
                                // Set the player's merchant entity
                                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(playerSellIndex).Entity;
                                
                                // We need to access the character script at the given index and sell the resources
                                playerMerchant.SellResources();
                            } // end for

                            // Save the players
                            GameMaster.Instance.SavePlayers();

                            // Finally, tell the GameMaster to load the end scene
                            GameMaster.Instance.LoadLevel("EndScene");
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

		// Changes the state to EndGame which ends the game.
		public void EndGame()
		{
			gamePlayState = GamePlayState.EndGame;
		} // end EndGame
        
		// Resets values (For testing, not useful practically)
        void ResetValues()
		{
			actionButtonText.text = "N/A";
            guiGold.text = "-1";
            guiWeight.text = "-1";
            guiMaxWeight = -1;
            guiDiceDistVal = 0;
		} // end ResetValues

        // Gets the current player from the players list
        public GameObject GetCurrentPlayer()
		{
			return	GameMaster.Instance.GetPlayerObject(guiPlayerTurn);
		} // end GetCurrentPlayer

		// Button Functions
		// Action Button - Used to move the turn through its phases
		public void ActionButton()
		{
			if(gamePlayState == GamePlayState.RollDice)
			{
				// Roll the die
				guiDiceDistVal = die.Roll(2, 5);
				
				//Play dieRoll sound
				AudioManager.Instance.PlayDie();

				// Set Action Button text while disabled
				actionButtonText.text = "Processing...";

				//Disable button while distance is being calculated
				actionButton.interactable = false;

				// Change to the CalcDistance State
				gamePlayState = GamePlayState.CalcDistance;
			} //end if
			else if(gamePlayState == GamePlayState.SelectPathToTake)
			{
				// Set Action Button text while disabled
				actionButtonText.text = "Processing...";

				//Disable button while turn is ending
				actionButton.interactable = false;

				// Change the state to the DoAction state
				gamePlayState = GamePlayState.DoAction;
			} //end else if
			else if(gamePlayState == GamePlayState.EndTurn)
			{
				// Update the turn
				guiPlayerTurn = GameMaster.Instance.NextTurn();

				// Change the state to the BeginTurn state
				gamePlayState = GamePlayState.BeginTurn;
			} //end else if
		} //end ActionButton

		// Inventory button - Displays equipment and resources
		public void ShowInventory()
		{
			
		} // end ShowInventory
		
		// Ally button - Displays allies and their inventories
		public void ShowAllies()
		{
			
		} //end ShowAllies

		// Yes Button - Accepts whatever is presented
		public void Yes()
		{
			if(mapEventResult.Contains("Ally"))
			{
				guiTurnText.text = guiMapEvent.ResolveAlly("YES");
			} //end if
			acceptPanel.SetActive (false);

			// Enable Action Button
			actionButton.interactable = true;
		} //end Yes

		// No Button - Declines whatever is presented
		public void No()
		{
			if(mapEventResult.Contains("Ally"))
			{
				guiTurnText.text = guiMapEvent.ResolveAlly("NO");
			} //end if
			acceptPanel.SetActive (false);

			// Enable Action Button
			actionButton.interactable = true;
		} //end No
	} // end GameplayStateMachine
} // end GSP
