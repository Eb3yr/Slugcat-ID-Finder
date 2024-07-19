using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
