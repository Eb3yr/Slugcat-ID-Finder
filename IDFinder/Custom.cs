using System;
using System.Runtime.CompilerServices;
using Unity_XORShift;

namespace IDFinder
{
	public static class Custom
	{
		// All methods but InverseLerp are both functions found in the Rain World codebase, in the Custom class within the RWCustom namespace. InverseLerp is a recreation of the equivalent function in the Mathf Unity namespace based on Unity's documentation.
		public static float PushFromHalf(float val, float pushExponent)
		{
			if (val == 0.5f)
			{
				return 0.5f;
			}
			if (val < 0.5f)
			{
				return LerpMap(val, 0f, 0.5f, 0f, 0.5f, pushExponent);
			}
			return LerpMap(val, 1f, 0.5f, 1f, 0.5f, pushExponent);
		}
		public static float LerpMap(float val, float fromA, float toA, float fromB, float toB, float? exponent = null)
		{
			if (exponent != null)
			{
				return float.Lerp(fromB, toB, float.Pow(InverseLerp(fromA, toA, val), (float)exponent));
			}
			else
			{
				return float.Lerp(fromB, toB, InverseLerp(fromA, toA, val));
			}
		}
        public static float ClampedRandomVariation(float baseValue, float maxDeviation, float k)
        {
            return float.Clamp(baseValue + Custom.RandomDeviation(k) * maxDeviation, 0f, 1f);
        }
        private static float RandomDeviation(float k)
        {
            return Custom.SCurve(XORShift128.NextFloat() * 0.5f, k) * 2f * ((XORShift128.NextFloat() < 0.5f) ? 1f : -1f);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SCurve(float x, float k)
        {
            x = x * 2f - 1f;
            if (x < 0f)
            {
                x = float.Abs(1f + x);
                return k * x / (k - x + 1f) * 0.5f;
            }
            k = -1f - k;
            return 0.5f + k * x / (k - x + 1f) * 0.5f;
        }

        public static float InverseLerp(float a, float b, float value)
		{
			// A value between zero and one, representing where value falls inbetween the range between a and b. 
			return (value - a) / (b - a);
		}
	}
}
