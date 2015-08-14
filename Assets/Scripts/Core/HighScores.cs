/*******************************************************************************
 *
 *  File Name: HighScores.cs
 *
 *  Description: A clean serialisable class for saving and loading the high
 *               scores
 *
 *******************************************************************************/
using System;
using System.Collections.Generic;

namespace GSP.Core
{
    [Serializable]
    /*******************************************************************************
     *
     * Name: HighScores
     * 
     * Description: Container class for a clean version of the serialisable high
     *              scores.
     * 
     *******************************************************************************/
    public class HighScores
    {
        List<string> names; // The names of the entries
        List<int> scores;   //  The scores of the entries
        
        // Default constructor; This will create an empty object
        public HighScores()
        {
            // Initialise the lists
            names = new List<string>();
            scores = new List<int>();
            
            // Reset the container; This prevents the initialisation being in 2 places
            Reset();
        } // end HighScores

        // Resets the contents of the container
        public void Reset()
        {
            // Clear the lists
            names.Clear();
            scores.Clear();
        } // end Reset

        // Gets the name of an entry at the given index
        public string GetName(int index)
        {
            return names[index];
        } // end GetName

        // Gets the score of an entry at the given index
        public int GetScore(int index)
        {
            return scores[index];
        } // end GetScore

        // Adds a name of an entry to the list
        public void AddName(string name)
        {
            names.Add(name);
        } // end AddName

        // Adds a score of an entry to the list
        public void AddScore(int score)
        {
            scores.Add(score);
        } // end AddScore
    } // end HighScores
} // end GSP.Core
