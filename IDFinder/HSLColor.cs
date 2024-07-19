using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFinder
{
    // I don't like having this as a class since it's a small object being put onto the heap, but this avoids compiler error CS1612. At some point this should be rewritten to function correctly as a struct. 
    public class HSLColor
    {
        public float H;
        public float S;
        public float L;
        public Color rgb
        {
            get
            {
                return Custom.HSL2RGB(H, S, L);
            }
        }
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
    }
    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;
        public Color(float R, float G, float B, float A = 1f)
        {
            this.R = float.Clamp(R, 0f, 1f);
            this.G = float.Clamp(G, 0f, 1f);
            this.B = float.Clamp(B, 0f, 1f);
            this.A = float.Clamp(A, 0f, 1f);
        }
    }
}
