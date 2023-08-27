using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
    public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
    {
        var isFirstIteration = true;
        var smoothedSum = 0.0d;
        foreach (var dataPoint in data)
        {
            var newValue = dataPoint.OriginalY;
            smoothedSum = alpha * newValue + (1 - alpha) * smoothedSum;
            if (isFirstIteration)
            {
                smoothedSum = newValue;
                isFirstIteration = false;
            }
            yield return new DataPoint(dataPoint).WithExpSmoothedY(smoothedSum);
        }
    }
}