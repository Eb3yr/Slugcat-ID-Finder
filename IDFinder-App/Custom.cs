using System.Runtime.CompilerServices;
using System.Drawing;

namespace IDFinder_App
{
    public static class Consts
    {
        public const float DefFontSize = 32f;
        public const string DefWeightLabel = "\t\tWeight: ";
    }
    public static class Custom
    {
        public static float InverseLerp(float left, float right, float val)
        {
			return float.Clamp((val - left) / (right - left), 0f, 1f);
		}
    }
}
