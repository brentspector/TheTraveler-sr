/*******************************************************************************
 *
 *  File Name: HighScoreTable.cs
 *
 *  Description: Contains the logic for the High Scores Table for single player.
 *
 *******************************************************************************/
using GSP.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: HighScoreTable
     * 
     * Description: Contains the logic for the High Scores Table for single player.
     * 
     *******************************************************************************/
    public class HighScoreTable
    {
        List<Pair<string, int>> scores; // The entries in the table
        int maxScores;                  // The maximum number of scores used
        Transform body;                 // The panel at HighScoresTable/Body

        // Creates a HighScoreTable object to deal with the scores
        public HighScoreTable(bool isSaveLoad = false)
        {
            // Initialise the list
            scores = new List<Pair<string, int>>();

            // Get the reference to the body panel
            body = GameObject.Find("Canvas").transform.Find("HighScoresTable/Body").transform;

            // Set the max number of entries
            maxScores = 10;

            // Make sure we're not saving or loading
            if (!isSaveLoad)
            {
                // Load the highscores from GameMaster
                GameMaster.Instance.LoadHighScores();

                // Check if there aren't ten entries in the list
                if (scores.Count < maxScores)
                {
                    // Initialise the table to empty scores
                    for (int index = 0; index < maxScores; index++)
                    {
                        // Using this function so we don't save ten times
                        AddScoreFromSave("Empty", 0);
                    } // end for

                    // Now sort the scores
                    SortScores();

                    // Finally, save the scores
                    GameMaster.Instance.SaveHighScores();
                } // end if
            } // end if

            // Set the table's interface colour to the player's colour
            GameObject.Find("Canvas").transform.Find("HighScoresTable").GetComponent<Image>().color =
                Utility.InterfaceColorToColor(GameMaster.Instance.GetPlayerColor(0));

            // Now display the scores
            DisplayScores();
        } // end HighScoreTable

        // Gets a score from the list
        public Pair<string, int> GetScore(int index)
        {
            return scores[index];
        } // end GetScore

        // Adds a score to the list
        public void AddScore(string name, int score)
        {
            // Create the score entry
            Pair<string, int> entry = new Pair<string, int>(name, score);

            // Add the score
            scores.Add(entry);

            // Now sort the scores
            SortScores();

            // Save the scores
            GameMaster.Instance.SaveHighScores();

            // Finally, update the display of the scores
            DisplayScores();
        } // end AddScore

        // Adds a score from a save file
        public void AddScoreFromSave(string name, int score)
        {
            // Add the score
            scores.Add(new Pair<string, int>(name, score));

            // Finally, update the display of the scores
            DisplayScores();
        } // end AddScoreFromSave
        
        // Sorts the list of scores
        void SortScores()
        {
            // Sort the list by the score in descending order; this places the higer value at the top
            var sortedScores = from entry in scores orderby entry.Second descending select entry;

            // Convert the result to a list and store it back into scores
            scores = sortedScores.ToList();
        } // end SortScores

        // Sets the scores for display on the table.
        void DisplayScores()
        {
            Debug.LogFormat("maxScores: {0}", maxScores);
            Debug.LogFormat("scores.Count: {0}", scores.Count);
            
            // Loop over the table to set the values
            for (int index = 0; index < maxScores; index++)
            {
                // Get the child at index + 1
                Transform entry = body.GetChild(index + 1);
                Debug.LogFormat("Name: {0}", entry.name);

                // Get the children of the entry and their text components and set their text to the entry values
                entry.GetChild(0).GetComponent<Text>().text = scores[index].First;
                entry.GetChild(1).GetComponent<Text>().text = scores[index].Second.ToString();
            } // end for
        } // end DisplayScores

        // Gets the maximum number of scores
        public int MaxScores
        {
            get { return maxScores; }
        } // end MaxScores
    } // end HighScoreTable
} // end GSP
