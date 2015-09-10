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

        // Use this for initialisation
		void Awake() 
		{
            GameObject.Find("Canvas").transform.Find("PlayerInventory").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Tooltip").gameObject.SetActive(false);
            
            // Create the players in data only mode
            GameMaster.Instance.LoadPlayers(true);

            // Get the references
            highScores = GameObject.Find("Canvas").transform.Find("HighScoresTable").gameObject;
            rankings = GameObject.Find("Canvas").transform.Find("RankingTable").gameObject;

            // Play the winning sound
            AudioManager.Instance.PlayVictory();

            // Check if the game was single player
            if (GameMaster.Instance.IsSinglePlayer)
            {
                // Get the player's merchant
                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(0).Entity;

                // Load the Score table
                GameMaster.Instance.LoadHighScores();

                // Add the player's score to the table
                HighScoreTable highScoreTable = GameMaster.Instance.ScoresTable;
                highScoreTable.AddScore(playerMerchant.Name, playerMerchant.Currency);

                // Show the high scores table
                highScores.SetActive(true);
            } // end if
            else
            {
                // Display the rankings
                RankTable rankTable = new RankTable();
                rankTable.DisplayRanks(GameMaster.Instance.NumPlayers);

                // Show the ranking table
                rankings.SetActive(true);
            } // end else
		} // end Start

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
	} // end GUIEndGame
} // end GSP