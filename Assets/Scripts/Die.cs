using System.Collections;

namespace GSP
{
	public class Die
	{
		// Declare the SafeRandom variable.
		private SafeRandom m_rand;

		// Constructor for the die behaviour.
		public Die()
		{
			// Initialise the SafeRandom class for die rolls. Don't worry about the seed; it'll use TickCount.Now for that.
			m_rand = new SafeRandom();
		} // end Die constructor

		// Gets the SafeRandom object.
		public SafeRandom Rand
		{
			get { return m_rand; }
		} // end Rand property

		// Roll the die. This defaults to a single six-sided die.
		// Stuff can be done to the result after this is called.
		public int Roll()
		{
			// Roll the default die.
			return Roll( 1, 6 );
		} // end Roll integer function

		// Roll the die. This takes the number of di(c)e and its number of sides.
		// Stuff can be done to the result after this is called.
		public int Roll( int numDie, int numSides )
		{
			// Since the algorithm we are using excludes the maximum value, we need to add one to it.
			var dieMinValue = 1;
			var dieMaxValue = numSides + 1;

			// Holds the sum of all the di(c)e rolled in this call.
			var dieSum = 0;

			// Roll the die for each die needed.
			for ( int i = 0; i < numDie; i++ )
			{
				// Add the roll to the sum.
				dieSum += Rand.Next( dieMinValue, dieMaxValue );
			} // end for statement

			// Return the sum of the rolls to be dealt with later.
			return dieSum;
		} // end Roll integer function

		// Reseeds the die's random number generator.
		public void Reseed( int seed )
		{
			// Call the reseed funtion of the random generator.
			m_rand.Reseed( seed );
		} // end Reseed function
	} // end Die class
} // end namespace