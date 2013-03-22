using System;

namespace MathFunctions
{
    public class GaussianFunction
    {
        private float currentY = 0;
        private float u;
        private double a;
        private double twofi2;

        public GaussianFunction(float u, float fi)
        {
            this.u = u;
            this.a = 1/(fi*Math.Sqrt(2*Math.PI));
            twofi2 = 2*Math.Pow(fi, 2);
        }

        public float GetFunctionValue(float x)
        {
            currentY = (float)(a*Math.Exp(-Math.Pow(x - u, 2)/twofi2));

            return currentY;
        }

        public float getStepValue(float x)
        {
            var stepY = currentY;

            currentY = (float)(a * Math.Exp(-Math.Pow(x - u, 2) / twofi2));

            stepY = currentY - stepY;

            return stepY;
        }

        public void Reset()
        {
            currentY = 0;
        }
    }
}
