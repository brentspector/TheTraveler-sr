using UnityEngine;
using System.Collections;

namespace GSP.Core
{
    // Container class for a clean version of the serialisable player data.
    [System.Serializable]
    public class HighScores
    {

        /*
         * Variables go here.
         */

        
        // Default constructor.
        public HighScores()
        {
            // Reset the container. This prevents the initialisation being in 2 places.
            Reset();
        }

        // Resets the contents of the container.
        public void Reset()
        {
            // Initialise/Reset variables here.
        }
    }
}
