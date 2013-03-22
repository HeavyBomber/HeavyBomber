namespace MathFunctions
{
    public class QadraticFunction
    {
        private float currentY = 0;
        private float start;
        private float end;
        private float amplitude;

        public QadraticFunction(float start, float end, float amplitude)
        {
            this.start = start;
            this.end = end;
            this.amplitude = amplitude;
        }

        public float getFunctionValue(float x)
        {
            //f(x) = a(x - x1)(x - x2)
            currentY = amplitude * (x - start) * (x - end);

            return currentY;
        }

        public float getStepValue(float x)
        {
            var stepY = currentY;

            currentY = amplitude * (x - start) * (x - end);

            stepY = currentY - stepY;

            return stepY;
        }

        public void Reset()
        {
            currentY = 0;
        }
    }
}
