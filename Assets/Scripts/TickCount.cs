/*******************************************************************************
 *
 *  File Name: TickCount.cs
 *
 *  Description: Replacement for Environment.TickCount
 *
 *******************************************************************************/
using System;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: TickCount
     * 
     * Description: Represents how long the application has been running in
     *              milliseconds.
     *              
     * Note: This was pulled elsewhere so comments is limited.
     * 
     *******************************************************************************/
    public struct TickCount : IEquatable<TickCount>
	{
        // Stores the time that the application started. Or, more precisely, the time that this struct was first called
        static readonly int _startupTime = Environment.TickCount;
		readonly uint _value;

        // Initializes a new instance of the TickCount struct
		public TickCount(uint value)
		{
			_value = value;
		} // end TickCount

        // Gets the largest possible value for a TickCount
        public static TickCount MaxValue
		{
            get { return new TickCount(uint.MaxValue); }
		} // end MaxValue

        // Gets the smallest possible value for a TickCount
		public static TickCount MinValue
		{
            get { return new TickCount(uint.MinValue); }
		} // end MinValue

		// Gets the amount of time that has elapsed in milliseconds since this application has started. This value will initially
		// start at 0 when the application starts up. After approximately 49.71 days of application up-time, the tick count will roll
        // back over back to 0. This is intended to be used as a replacement to Environment.TickCount since it guarantees rolling over
        // only after approximately 49.71 days of application up-time, whereas Environment.TickCount roll-over depends on how long the
        // system has been running and can roll over at any time relative to when the application started
		public static TickCount Now
		{
			get
			{
				// Instead of using TickCount.Now directly, we find the difference in the tick count compared to when the application
				// started, which allows us to start at 0
                return (uint)(Environment.TickCount - _startupTime);
			}
		} // end Now

        #region IEquatable Members

		/// Performs an implicit conversion from TickCount to Uint32
		public static implicit operator uint(TickCount time)
		{
			return time._value;
		} // end uint

		/// Performs an implicit conversion from TickCount to int64
		public static implicit operator long(TickCount time)
		{
			return time._value;
		} // end long

		/// Performs an implicit conversion from TickCount to float
		public static implicit operator float(TickCount time)
		{
			return time._value;
		} // end float

		/// Performs an implicit conversion from Uint32 to TickCount
		public static implicit operator TickCount(uint value)
		{
            return new TickCount(value);
		} // end TickCount

		/// Performs an implicit conversion from TickCount to int32
		public static explicit operator int(TickCount time)
		{
			return (int)time._value;
		} // end int

		/// Performs an explicit conversion from int32 to TickCount
		public static explicit operator TickCount(int value)
		{
            return new TickCount((uint)value);
		} // end TickCount

		// Performs an explicit conversion from int64 to TickCount
		public static explicit operator TickCount(long value)
		{
            return new TickCount((uint)value);
		} // end TickCount

		// Indicates whether the current object is equal to another object of the same type
		public bool Equals(TickCount other)
		{
			return other._value == _value;
		} // end Equals

		// Indicates whether this instance and a specified object are equal
		public override bool Equals(object obj)
		{
            return obj is TickCount && this == (TickCount)obj;
		} // end Equals

        /// Returns a 32-bit signed integer that is the hash code for this instance
		public override int GetHashCode()
		{
			return _value.GetHashCode();
		} // end GetHashCode

		// Implements the operator ==
		public static bool operator ==(TickCount left, TickCount right)
		{
            return left.Equals(right);
		} // end ==

		// Implements the operator !=
		public static bool operator !=(TickCount left, TickCount right)
		{
            return !left.Equals(right);
		} // end !=
        #endregion
    } // end TickCount
} // end GSP