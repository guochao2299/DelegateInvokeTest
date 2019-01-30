using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCalcLib
{
    public static class MathLib
    {
        public static bool IsSupportSubOpt = false;

        public static int Add(int a,int b)
        {
            return a + b;
        }

#if DEBUG
        public static int Sub(int a, int b)
        {
            return a - b;
        }
#endif

        public static float Div(int a,int b)
        {
            return a * 1.0f / b;
        }

        public static int Multy(int a ,int b)
        {
            return a * b;
        }
    }
}
