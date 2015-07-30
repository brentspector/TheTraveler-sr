/*******************************************************************************
 *
 *  File Name: HighScores.cs
 *
 *  Description: A clean serialisable class for saving and loading the high
 *               scores
 *
 *******************************************************************************/
using System;

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

        /*
         * Variables go here.
         */

        
        // Default constructor; This will create an empty object
        public HighScores()
        {
            // Reset the container; This prevents the initialisation being in 2 places
            Reset();
        } // end HighScores

        // Resets the contents of the container
        public void Reset()
        {
            // Initialise/Reset variables here
        } // end Reset
    } // end HighScores
} // end GSP.Core
