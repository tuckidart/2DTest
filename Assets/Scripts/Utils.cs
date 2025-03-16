public static class Utils
{
    public static float ScaleRange(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;
        float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;

        return newValue;
    }
}