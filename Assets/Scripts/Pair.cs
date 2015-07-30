/*******************************************************************************
 *
 *  File Name: Pair.cs
 *
 *  Description: Pairs two values together
 *
 *******************************************************************************/

namespace GSP
{
    /*******************************************************************************
     *
     * Name: Pair
     * 
     * Description: Creates a pair similar to the C++ construct and similar to the
     *              KeyValuePair, but without the extra functionality.
     * 
     *******************************************************************************/
    public class Pair<T,U>
	{
		// Default constructor; use this if you want to set them manually
        public Pair()
		{
            // Leave empty
		} // end Pair

		// Constructs a pair from two arguments of a given type
        public Pair(T first, U second)
		{
			// Set the variables
            First = first;
			Second = second;
		} // end Pair

        // Gets and Sets the First or key of the pair
		public T First { get; set; }
        // Gets and Sets the Second or the value of the pair
		public U Second { get; set; }
	} // end Pair
} // end GSP
