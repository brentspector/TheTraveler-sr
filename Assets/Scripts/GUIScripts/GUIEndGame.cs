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

        
        // Called before Start; used for initialisation
        void Awake()
        {
            // Get the references
            highScores = GameObject.Find("Canvas").transform.Find("HighScoresTable").gameObject;
            rankings = GameObject.Find("Canvas").transform.Find("RankingTable").gameObject;
            highScoreTable = new HighScoreTable();
            rankTable = rankings.GetComponent<RankTable>();
        } // end Awake
        
        // Use this for initialization
		void Start() 
		{
            // Create the players in data only mode
            GameMaster.Instance.LoadPlayers(true);

            // Start the EndGame coroutine
            StartCoroutine(EndGame());
		} // end Start

        IEnumerator EndGame()
        {
            // Wait for a second
            Debug.Log("Waiting");
            yield return new WaitForSeconds(2);
            Debug.Log("Done");

            // Check if the game was single player
            if (GameMaster.Instance.IsSinglePlayer)
            {
                // Get the player's merchant
                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(0).Entity;

                // Add the player's score to the table
                highScoreTable.AddScore(playerMerchant.Name, playerMerchant.Currency);

                // Show the high scores table
                highScores.SetActive(true);
            } // end if
            else
            {
                // Display the rankings
                rankTable.DisplayRanks(GameMaster.Instance.NumPlayers);

                // Show the ranking table
                rankings.SetActive(true);
            } // end else
        } // end EndGame

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
            // Destroy the audio manager
            Destroy(AudioManager.Instance.gameObject);

            // Tell the GameMaster to load a level
            GameMaster.Instance.LoadLevel(0);
        } // end BackToMenu
	} // end GUIEndGame
} // end GSP