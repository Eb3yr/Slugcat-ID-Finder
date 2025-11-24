namespace IDFinder
{
    public struct HSLColor
    {
        public float H;
        public float S;
        public float L;
        public HSLColor(float H, float S, float L)
        {
            this.H = H;
            this.S = S;
            this.L = L;
        }
        public HSLColor()
        {
            H = 0;
            S = 0;
            L = 0;
        }
        public Color RGB()
        {
            return Custom.HSL2RGB(H, S, L);
        }
    }
    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;
        public Color(float R, float G, float B, float A = 1f)
        {
#if DEBUG
            // Investigate whether float.Clamp can be excluded from release build by throwing if out-of-range arguments exist during debug builds
            if (R > 1f || R < 0f) throw new ArgumentException($"R out of range [0f, 1f]: {R}");
			if (G > 1f || G < 0f) throw new ArgumentException($"R out of range [0f, 1f]: {G}");
			if (B > 1f || B < 0f) throw new ArgumentException($"R out of range [0f, 1f]: {B}");
            if (A > 1f || A < 0f) throw new ArgumentException($"R out of range [0f, 1f]: {A}");
#endif
			this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
    }
}
