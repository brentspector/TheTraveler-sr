using GSP.Char;
/*******************************************************************************
 *
 *  File Name: GUIEndGane.cs
 *
 *  Description: GUI for the end game screen
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Interfaces;
using GSP.Entities.Neutrals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: GUIEndGame
     * 
     * Description: Creates the GUI for the end game screen.
     * 
     *******************************************************************************/
    public class GUIEndGame : MonoBehaviour 
	{
        GameObject highScores;          // The HighScores UI object
        GameObject rankings;            // The Rankings UI object
        HighScoreTable highScoreTable;  // The reference for the HighScoreTable
        RankTable rankTable;            // THe script refernece for the RankTable

        GameObject toggleButton;        // The reference to the toggle button

        // Use this for initialisation
		void Awake() 
		{
            // Disable the inventories
            GameObject.Find("Canvas").transform.Find("PlayerInventory").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("AllyInventory").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Tooltip").gameObject.SetActive(false);
            
            // Create the players in data only mode
            GameMaster.Instance.LoadPlayers(true);

            // Get the references
            highScores = GameObject.Find("Canvas").transform.Find("HighScoresTable").gameObject;
            rankings = GameObject.Find("Canvas").transform.Find("RankingTable").gameObject;
            toggleButton = GameObject.Find("Canvas").transform.Find("ButtonPanel/ToggleHighScore").gameObject;

            // Play the winning sound
            AudioManager.Instance.PlayVictory();

            // Create the rank table instance
            RankTable rankTable = new RankTable();
            // Get the sorted list
            rankTable.DetermineWinner(GameMaster.Instance.NumPlayers);

            // Loop over the currencies and apply the penalties
            var currencies = rankTable.Currencies;
            for (int index = 0; index < currencies.Count; index++)
            {
                // Apply the penalties; since the list is sorted, the index is the rank
                int currency = Utility.ApplyPenalty(index, currencies[index].Value);

                // Modify the player's currency to match the penalty version
                ((Merchant)GameMaster.Instance.GetPlayerScript(currencies[index].Key).Entity).Currency = currency;
            } // end for

            // Clear the player's currency dictionary in the rank table to make room for the new values
            rankTable.ClearCurrency();
            // Display the rankings
            rankTable.DisplayRanks(GameMaster.Instance.NumPlayers);

            // Check if the game was single player
            if (GameMaster.Instance.IsSinglePlayer)
            {
                // Load the Score table
                GameMaster.Instance.LoadHighScores();
                HighScoreTable highScoreTable = GameMaster.Instance.ScoresTable;

                // Get the player's merchant
                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(0).Entity;

                // Add the player's score to the table
                highScoreTable.AddScore(playerMerchant.Name, playerMerchant.Currency);

                // Show the high scores table
                highScores.SetActive(true);
            } // end if
            else
            {
                // Show the ranking table
                rankings.SetActive(true);

                // Hide the toggle button
                toggleButton.SetActive(false);
            } // end else

            // Reset the turn through the game master
            GameMaster.Instance.ResetTurn();
		} // end Awake

		void Update()
		{
            // Check for the 'c' key being pressed
            if (Input.GetKeyDown(KeyCode.C))
            {
                // Tell the GameMaster to change to the cake scene!
                GameMaster.Instance.LoadLevel("cake");
            } // end if
            
            if(Input.GetKeyDown(KeyCode.D))
			{
				AudioManager.Instance.PlayDraw();
			} // end if

			if(Input.GetKeyDown(KeyCode.L))
			{
				AudioManager.Instance.PlayLoss();
			} // end if
		} // end Update

        // Goes back to the menu
        public void BackToMenu()
        {
            Entities.EntityManager.Instance.Dispose();
            while (GameMaster.Instance.Turn != 0)
            {
                GameMaster.Instance.NextTurn();
            } //end while
            GameMaster.Instance.NumPlayers = 0;
            AudioManager.Instance.PlayMenu();
            GameMaster.Instance.LoadLevel("MenuScene");
        } // end BackToMenu

        // Toggles between the rank summary window and the high scores window
        public void ToggleWindow()
        {
            // Switch to the rank summary window if we're on the high scores window
            if (highScores.activeInHierarchy)
            {
                highScores.SetActive(false);
                rankings.SetActive(true);
                toggleButton.transform.GetChild(0).GetComponent<Text>().text = "High Scores";
            } // end if
            // Switch to the high scores window if we're on the rank summary window
            else if (rankings.activeInHierarchy)
            {
                rankings.SetActive(false);
                highScores.SetActive(true);
                toggleButton.transform.GetChild(0).GetComponent<Text>().text = "Summary";
            } // end else if
        } // end ToggleWindow
	} // end GUIEndGame
} // end GSP