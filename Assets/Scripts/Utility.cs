/*******************************************************************************
 *
 *  File Name: Utility.cs
 *
 *  Description: Static class for housing utility functions
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: Utility
     * 
     * Description: Used to contain utility functions
     * 
     *******************************************************************************/
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
        } // end ClampInt

        // Truncates a float to two decimal places
        public static float TruncateFloat(float toTruncate)
        {
            // Get the float to two decimal places then truncate the rest converting it into an integer
            int temp = System.Convert.ToInt32(toTruncate * 100.0f);

            // Return the truncated float
            return temp / 100.0f;
        } // end TruncateFloat

        // Returns the colour representative to the enumeration value. If the colour can't be parsed, it returns white
        public static Color InterfaceColorToColor(InterfaceColors interfaceColor)
        {
            string hexValue = string.Empty;    // The string for the hex value
            Color result;                      // The resulting colour

            // Switch over the InterfaceColors
            switch (interfaceColor)
            {
                case InterfaceColors.Black:
                    {
                        // Set the hex string
                        hexValue = "484848FF";
                        break;
                    } // end case Black
                case InterfaceColors.Blue:
                    {
                        // Set the hex string
                        hexValue = "579FF9FF";
                        break;
                    } // end case Blue
                case InterfaceColors.Green:
                    {
                        // Set the hex string
                        hexValue = "038000FF";
                        break;
                    } // end case Green
                case InterfaceColors.Orange:
                    {
                        // Set the hex string
                        hexValue = "FF7611FF";
                        break;
                    } // end case Orange
                case InterfaceColors.Pink:
                    {
                        // Set the hex string
                        hexValue = "FF9DFFFF";
                        break;
                    } // end case Pink
                case InterfaceColors.Purple:
                    {
                        // Set the hex string
                        hexValue = "B502C4FF";
                        break;
                    } // end case Purple
                case InterfaceColors.Red:
                    {
                        // Set the hex string
                        hexValue = "F40000FF";
                        break;
                    } // end case Red
                case InterfaceColors.Yellow:
                    {
                        // Set the hex string
                        hexValue = "F9FE18FF";
                        break;
                    } // end case Yellow
            } // end switch interfaceColor

            if (Color.TryParseHexString(hexValue, out result))
            {
                // Return the resulting colour
                return result;
            } // end if
            else
            {
                return Color.white;
            } // end else
        } // end InterfaceColorToColor
    } // end Utility
} // end GSP
