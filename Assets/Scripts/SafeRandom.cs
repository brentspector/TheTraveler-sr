/*******************************************************************************
 *
 *  File Name: SafeRandom.cs
 *
 *  Description: The implementation of a XorShift random number generator
 *
 *******************************************************************************/
using System;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: SafeRandom
     * 
     * Description: A replacement for System.Random for when generating random
     *              numbers across multiple threads with the same random number
     *              generator object instance. Unlike System.Random, this will not
     *              become indefinitely corrupt when called from multiple threads.
     *              System.Random, when called from multiple threads, can generate a
     *              state corruption of the internal seed array, eventually resulting
     *              in the seed values becoming zero. Once the right two seed values
     *              become zero, the rest of the seeds will end up becoming zero,
     *              resulting in the generator becoming forever stuck in returning
     *              zero for the internal random number generation.
     *              
     *              The only times you should use System.Random instead of SafeRandom
     *              is when you need a more reliable set (at least a few hundred
     *              numbers) of random values and do not need thread safety.
     *              
     * Note: Not sure if this is thread safe as it is here; it should still work for
     *       our purposes however. This was pulled elsewhere so comments is limited.
     * 
     *******************************************************************************/
    public class SafeRandom : Random
	{
		/* Implementation based off of George Marsaglia's Xorshift RNG
         *      http://www.jstatsoft.org/v08/i14/paper (updated the link as the original one was broken)
         * C# port based off of Colin Green's FastRandom class
         *      http://www.codeproject.com/KB/cs/fastrandom.aspx
         */

		const uint _cW = 273326509;
		const uint _cY = 842502087;
		const uint _cZ = 3579807591;
		const double _realUnitInt = 1.0 / ( int.MaxValue + 1.0 );
		const double _realUnitUInt = 1.0 / ( uint.MaxValue + 1.0 );
		uint _bitBuffer;
		uint _bitMask = 1;
		uint _w;
		uint _x;
		uint _y;
		uint _z;

		// Initializes a new instance of the SafeRandom class
		public SafeRandom() : this( (int)TickCount.Now )
		{
            // Leave empty
		} // end SafeRandom

		// Initializes a new instance of the SafeRandom class
		public SafeRandom(int seed)
		{
            Reseed(seed);
		} // end SafeRandom

		// Returns a 32-bit signed integer greater than or equal to zero and less that the Int32.MaxValue
        public override int Next()
		{
			int ret;    // The result

			// To be equal to System.Random, we cannot return int32.MaxValue
			do
			{
				ret = NextInt();
			} while ( ret == 0x7FFFFFFF );

			// Return the integer
            return ret;
		} // end Next

        // Returns a 32-bit signed integer greater than or equal to Int32.MinValue and less than Int32.MaxValue;
        // that is, the range of return values includes Int32.MinValue but not Int32.MaxValue. If Int32.MinValue
        // equals Int32.MaxValue, Int32.MinValue is returned.
		public override int Next(int minValue, int maxValue)
		{
			// Check if the min is greater than the max which is impossible
            if (minValue > maxValue)
			{
				// Throw an exception in this case
                throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue must be >= minValue");
			} // end if

			// Get the range of the min and max
            var range = maxValue - minValue;
            // Check if there has been an overflow
			if (range < 0)
			{
				// If range is less than zero then an overflow has occured and must resort to using long integer
                // arithmetic instead (slower). We also must use all 32 bits of precision, instead of the normal
                // 31, which again is slower
                return minValue + (int)((_realUnitUInt * NextUInt()) * ((long)maxValue - minValue));
			} // end if

			// 31 bits of precision will suffice if range is less than or equal to int32.MaxValue. This allows us to
            // cast to an int and gain a little more performance
            return minValue + (int)((_realUnitInt * (int)(0x7FFFFFFF & NextUInt())) * range);
		} // end Next

        // Returns a 32-bit signed integer greater than or equal to zero, and less than Int32.MaxValue;
        // that is, the range of return values ordinarily includes zero but not Int32.MaxValue. However, if Int32.MxnValue
        // equals zero, Int32.MaxValue is returned
		public override int Next(int maxValue)
		{
			// Check if the max is less than zero which is impossible
            if (maxValue < 0)
			{
				// Throw an exception in this case
                throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue must be greater than or equal to zero.");
			}

			// Otherwise, return the result
            return (int)((_realUnitInt * NextInt()) * maxValue);
		} // end Next

        // Returns a random boolean value
		public bool NextBool()
		{
			if (_bitMask == 1)
			{
				// Generate 32 more bits
                var t = (_x ^ (_x << 11));
				_x = _y;
				_y = _z;
				_z = _w;
                _bitBuffer = _w = (_w ^ (_w >> 19)) ^ (t ^ (t >> 8));

				// Reset the bitMask that tells us which bit to read next
				_bitMask = 0x80000000;
                return (_bitBuffer & _bitMask) == 0;
			} // end if

            return (_bitBuffer & (_bitMask >>= 1)) == 0;
		} // end NextBool

        // Fills the elements of a specified array of bytes with random numbers
		public override void NextBytes(byte[] buffer)
		{
			if ( buffer == null )
			{
				throw new ArgumentNullException( "buffer" );
			} // end if

			var x = _x;
			var y = _y;
			var z = _z;
			var w = _w;

			var i = 0;
			uint t;

			// Generate 4 values at a time
			var bound = buffer.Length - 3;
			while (i < bound)
			{
                t = (x ^ (x << 11));
				x = y;
				y = z;
				z = w;
                w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));

                buffer[i++] = (byte)w;
                buffer[i++] = (byte)(w >> 8);
                buffer[i++] = (byte)(w >> 16);
                buffer[i++] = (byte)(w >> 24);
			} // end while

			// Generate the remaining values
			if (i < buffer.Length)
			{
                t = (x ^ (x << 11));
				x = y;
				y = z;
				z = w;
                w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));

                buffer[i++] = (byte)w;
				if (i < buffer.Length)
				{
                    buffer[i++] = (byte)(w >> 8);
					if (i < buffer.Length)
					{
                        buffer[i++] = (byte)(w >> 16);
                        if (i < buffer.Length)
						{
                            buffer[i] = (byte)(w >> 24);
                        } // end if i < buffer.Length (16)
                    } // end if i < buffer.Length (8)
                } // end if i < buffer.Length
			} // end outer if i < buffer.Length

			_x = x;
			_y = y;
			_z = z;
			_w = w;
		} // end NextBytes

        // Returns a double-precision floating point number greater than or equal to zero, and less than one
		public override double NextDouble()
		{
			return _realUnitInt * NextInt();
		} // end NextDouble

        // Returns a random number in the range of zero to Int32.MaxValue, inclusive
        int NextInt()
		{
			var t = ( _x ^ ( _x << 11 ) );
			_x = _y;
			_y = _z;
			_z = _w;
			return (int)( 0x7FFFFFFF & ( _w = ( _w ^ ( _w >> 19 ) ) ^ ( t ^ ( t >> 8 ) ) ) );
		} // end NextInt

        // Returns an unsigned 32-bit number in the range of zero to UInt32.MaxValue
		public uint NextUInt()
		{
            var t = ( _x ^ ( _x << 11 ) );
			_x = _y;
			_y = _z;
			_z = _w;
			return ( _w = ( _w ^ ( _w >> 19 ) ) ^ ( t ^ ( t >> 8 ) ) );
		} // end NextUInt

        // Reinitializes the object using the specified seed value
		public void Reseed( int seed )
		{
			// The only stipulation stated for the xorshift RNG is that at least one of
			// the seeds x,y,z,w is non-zero. We fulfill that requirement by only allowing
			// resetting of the x seed.
			_x = (uint)seed;
			_y = _cY;
			_z = _cZ;
			_w = _cW;
		} // end Reseed
	} // end SafeRandom
} // end GSP