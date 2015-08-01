
namespace GSP
{
    public static class Utility
    {
        // Clamps an integer to zero; used to make sure something isn't below zero
        public static int ZeroClampInt(int toClamp)
        {
            // Check if the given value is below zero
            if (toClamp < 0)
            {
                // Clamp to zero
                return 0;
            } // end if

            // Otherwise, return the given value unchanged
            return toClamp;
        } // end ZeroClampInt

        // Clamps an integer between a min and max value
        public static int ClampInt(int toClamp, int min, int max)
        {
            // Check if the given value is less than the min
            if (toClamp < min)
            {
                // Clamp to the min value
                return min;
            } // end if
            else if (toClamp > max)
            {
                // Clamp to the max value
                return max;
            } // end else if
            else
            {
                // Otherwise, return the given value unchanged
                return toClamp;
            } // end else
        }
    } // end Utility
} // end GSP
