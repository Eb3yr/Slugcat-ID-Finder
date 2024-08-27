﻿using System.Runtime.CompilerServices;

namespace IDFinder
{
    internal static class XORShift128 // From https://gist.github.com/macklinb/a00be6b616cbf20fa95e4227575fe50b
	{
        public static uint x = 0, y = 0, z = 0, w = 0;

        const uint MT19937 = 1812433253;

        // Initialize Xorshift using a signed integer seed, calculating the state values using the initialization method from Mersenne Twister (MT19937)
        // https://en.wikipedia.org/wiki/Mersenne_Twister#Initialization
        public static void InitSeed(int seed)
        {
            x = (uint)seed;
            y = (uint)(MT19937 * x + 1);
            z = (uint)(MT19937 * y + 1);
            w = (uint)(MT19937 * z + 1);
        }

        // Explicitly set the state parameters
        public static void InitState(uint x, uint y, uint z, uint w)
        {
            XORShift128.x = x;
            XORShift128.y = y;
            XORShift128.z = z;
            XORShift128.w = w;
        }

		// XORShift, returns an unsigned 32-bit integer
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint _XORShift()
        {
            uint t = x ^ (x << 11);
            x = y; y = z; z = w;
            return w = w ^ (w >> 19) ^ t ^ (t >> 8);
        }
        public static uint XORShift() => _XORShift();
        // UInt32 / uint

        // UnityEngine.Random doesn't have any uint functions so these functions behave exactly like int Random.Range

        // Alias of base Next/XORShift
        public static uint NextUInt()
        {
            return _XORShift();
        }

        // Generate a random unsigned 32-bit integer value in the range 0 (inclusive) to max (exclusive)
        public static uint NextUIntMax(uint max)
        {
            if (max == 0) return 0;
            return _XORShift() % max;
        }

        // Generate random unsigned 32-bit integer value in the range min (inclusive) to max (exclusive)
        public static uint NextUIntRange(uint min, uint max)
        {
            if (max - min == 0) return min;

            if (max < min)
                return min - _XORShift() % (max + min);
            else
                return min + _XORShift() % (max - min);
        }

        // Int32 / int

        // Generate a random signed 32-bit integer value in the range -2,147,483,648 (inclusive) to 2,147,483,647 (inclusive)
        public static int NextInt()
        {
            return (int)(_XORShift() % int.MaxValue);
        }

        public static int NextIntMax(int max)
        {
            return NextInt() % max;
        }

        // Generate a random signed 32-bit integer value in the range min (inclusive) to max (exclusive)
        // If you only need to generate positive integers, use NextUIntRange instead
        public static int NextIntRange(int min, int max)
        {
            // If max and min are the same, just return min since it will result in a DivideByZeroException
            if (max - min == 0) return min;

            // Do operations in Int64 to prevent overflow that might be caused by any of the following operations
            // I'm sure there's a faster/better way to do this and avoid casting, but we prefer equivalence to Unity over performance
            long minLong = (long)min;
            long maxLong = (long)max;
            long r = _XORShift();

            // Flip the first operator if the max is lower than the min,
            if (max < min)
                return (int)(minLong - r % (maxLong - minLong));
            else
                return (int)(minLong + r % (maxLong - minLong));
        }

        // Single / float

        // Generate a random floating point between 0.0 and 1.0 (inclusive?)
        public static float NextFloat()
        {
            return 1.0f - NextFloatRange(0.0f, 1.0f);
        }

        // Generate a random floating point between min (inclusive) and max (exclusive) 
        public static float NextFloatRange(float min, float max)
        {
            return (min - max) * ((float)(_XORShift() << 9) / 0xFFFFFFFF) + max;
        }
    }
	internal class InstanceXORShift128  // While the ThreadStatic attribute could be used in XORShift128, this approach is ~10% faster. 
	{
		public uint x = 0, y = 0, z = 0, w = 0;
		const uint MT19937 = 1812433253;
		public void InitSeed(int seed)
		{
			x = (uint)seed;
			y = (uint)(MT19937 * x + 1);
			z = (uint)(MT19937 * y + 1);
			w = (uint)(MT19937 * z + 1);
		}
		public void InitState(uint x, uint y, uint z, uint w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private uint _XORShift()
		{
			uint t = x ^ (x << 11);
			x = y; y = z; z = w;
			return w = w ^ (w >> 19) ^ t ^ (t >> 8);
		}
        public uint XORShift() => _XORShift();
		public uint NextUInt()
		{
			return _XORShift();
		}
		public uint NextUIntMax(uint max)
		{
			if (max == 0) return 0;
			return _XORShift() % max;
		}
		public uint NextUIntRange(uint min, uint max)
		{
			if (max - min == 0) return min;

			if (max < min)
				return min - _XORShift() % (max + min);
			else
				return min + _XORShift() % (max - min);
		}
		public int NextInt()
		{
			return (int)(_XORShift() % int.MaxValue);
		}
		public int NextIntMax(int max)
		{
			return NextInt() % max;
		}
		public int NextIntRange(int min, int max)
		{
			if (max - min == 0) return min;

			long minLong = (long)min;
			long maxLong = (long)max;
			long r = _XORShift();

			if (max < min)
				return (int)(minLong - r % (maxLong - minLong));
			else
				return (int)(minLong + r % (maxLong - minLong));
		}
		public float NextFloat()
		{
			return 1.0f - NextFloatRange(0.0f, 1.0f);
		}
		public float NextFloatRange(float min, float max)
		{
			return (min - max) * ((float)(_XORShift() << 9) / 0xFFFFFFFF) + max;
		}
	}
}
