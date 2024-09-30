using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IDFinder
{
	public static class Custom
	{
		public static float Lerp(float value1, float value2, float amount) => (value1 * (1.0f - amount)) + (value2 * amount);
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
				return Custom.Lerp(fromB, toB, float.Pow(InverseLerp(fromA, toA, val), exponent.Value));
			}
			else
			{
				return Custom.Lerp(fromB, toB, InverseLerp(fromA, toA, val));
			}
		}
        public static float ClampedRandomVariation(float baseValue, float maxDeviation, float k)
        {
            return float.Clamp(baseValue + Custom.RandomDeviation(k) * maxDeviation, 0f, 1f);
        }
		internal static float ClampedRandomVariationRNGParam(float baseValue, float maxDeviation, float k, InstanceXORShift128 XORShift128)
		{
			return float.Clamp(baseValue + Custom.RandomDeviationRNGParam(k, XORShift128) * maxDeviation, 0f, 1f);
		}
		private static float RandomDeviation(float k)
        {
            return Custom.SCurve(XORShift128.NextFloat() * 0.5f, k) * 2f * ((XORShift128.NextFloat() < 0.5f) ? 1f : -1f);
        }
		private static float RandomDeviationRNGParam(float k, InstanceXORShift128 XORShift128)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 DegToFloat2(float ang)
        {
            float x = ang * 57.29577951f;  // Degrees to radians = multiply by 180 / pi = 57.29577951f
			return new Vector2(float.Sin(x), float.Cos(x));
        }
        public static Color HSL2RGB(float h, float sl, float l)
        {
            float r = l;
            float g = l;
            float b = l;
            float num = ((double)l <= 0.5) ? (l * (1f + sl)) : (l + sl - l * sl);
            if (num > 0f)
            {
                float num2 = l + l - num;
                float num3 = (num - num2) / num;
                h *= 6f;
                int num4 = (int)h;
                float num5 = h - (float)num4;
                float num6 = num * num3 * num5;
                float num7 = num2 + num6;
                float num8 = num - num6;
                switch (num4)
                {
                    case 0:
                        r = num;
                        g = num7;
                        b = num2;
                        break;
                    case 1:
                        r = num8;
                        g = num;
                        b = num2;
                        break;
                    case 2:
                        r = num2;
                        g = num;
                        b = num7;
                        break;
                    case 3:
                        r = num2;
                        g = num8;
                        b = num;
                        break;
                    case 4:
                        r = num7;
                        g = num2;
                        b = num;
                        break;
                    case 5:
                        r = num;
                        g = num2;
                        b = num8;
                        break;
                }
            }
            return new Color(r, g, b);
        }
        public static float Decimal(float f)
        {
            return f - float.Floor(f);
        }
        public static float DistanceBetweenZeroToOneFloats(float a, float b)
        {
            return Math.Min(Math.Min(Math.Abs(a - b), Math.Abs(a + 1f - b)), Math.Abs(a - 1f - b));
        }
        public static float InverseLerp(float a, float b, float value)
		{
            // A value between zero and one, representing where value falls inbetween the range between a and b.
			return float.Clamp((value - a) / (b - a), 0f, 1f);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 RNV()
		{
			return Custom.DegToVec(XORShift128.NextFloat() * 360f);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 RNVRNGParam(InstanceXORShift128 XORShift128)
		{
			return Custom.DegToVec(XORShift128.NextFloat() * 360f);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 DegToVec(float ang)
		{
			return new Vector2(float.Sin(ang * 0.017453292f), float.Cos(ang * 0.017453292f));
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float HueDiff(float targetH, float H)
        {
            if (targetH > H)
                return HueDiff(H, targetH); // Enforce targetH < H

            float diff = H - targetH;
            float diffPlusOne = targetH + 1 - H;
            return float.Min(diff, diffPlusOne);
        }
	}
}
