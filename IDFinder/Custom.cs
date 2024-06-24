using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
	public static class Custom
	{
		// PushFromHalf and LerpMap are both functions found in the Rain World codebase, in the Custom class within the RWCustom namespace. 
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
		public static float LerpMap(float val, float fromA, float toA, float fromB, float toB, float? exponent)
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
		public static float InverseLerp(float a, float b, float value)
		{
			// A value between zero and one, representing where value falls inbetween the range between a and b. 
			return (value - a) / (b - a);
		}
	}
}
