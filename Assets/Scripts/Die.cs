/*******************************************************************************
 *
 *  File Name: Die.cs
 *
 *  Description: Wrapper for the random number generator
 *
 *******************************************************************************/

namespace GSP
{
    /*******************************************************************************
     *
     * Name: Die
     * 
     * Description: Random number generator similar to die rolls.
     * 
     *******************************************************************************/
    public class Die
	{
		SafeRandom rand;  // The class used for generating the numbers

		// Constructor.
		public Die()
		{
			// Initialise the SafeRandom class for die rolls
            // Don't worry about the seed as it'll use TickCount.Now for that
			rand = new SafeRandom();
		} // end Die

		// Roll the die which defaults to a single six-sided die
		// Stuff can be done to the result after this is called
		public int Roll()
		{
			// Roll the default die
			return Roll(1, 6);
		} // end Roll

		// Roll the die which takes the number of dice and its number of sides.
		// Stuff can be done to the result after this is called
		public int Roll(int numDie, int numSides)
		{
			// Since the algorithm we are using excludes the maximum value, we need to add one to it
            // The minimum value is always one
			var dieMinValue = 1;
			var dieMaxValue = numSides + 1;

			// The sum of all the dice rolled in this call
			var dieSum = 0;

			// Roll the die for each die
            for (int index = 0; index < numDie; index++)
			{
				// Add the roll to the sum
				dieSum += Rand.Next(dieMinValue, dieMaxValue);
			} // end for

			// Return the sum of the rolls to be dealt with later
			return dieSum;
		} // end Roll

		// Reseeds the Die's random number generator
		public void Reseed(int seed)
		{
			// Call the reseed funtion of the random generator
			rand.Reseed(seed);
		} // end Reseed

        // Gets the SafeRandom object
        public SafeRandom Rand
        {
            get { return rand; }
        } // end Rand
	} // end Die
} // end GSP