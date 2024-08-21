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
            this.R = Math.Clamp(R, 0f, 1f);
            this.G = Math.Clamp(G, 0f, 1f);
            this.B = Math.Clamp(B, 0f, 1f);
            this.A = Math.Clamp(A, 0f, 1f);
        }
    }
}
