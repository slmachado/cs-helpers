namespace Helpers;

public static class MathHelper
{
    /// <summary>
    /// Computing the max, min, avg and the trend values of an array
    /// use Least squares algorithm to compute trend
    /// </summary>
    /// <param name="serie"> list of dataPoint with X and Y coordinates, in float. 
    /// TODO TIM & MGN indexes scale determines the output trend scale (0,0001 vs 1 for example), this needs to be re-thought.</param>
    public static double GetLeastSquaresTrend(IList<Tuple<float, float>> serie)
    {
        double sumXi = 0, sumYi = 0;
        float xn = 0;
        double avgYi = 0;
        double trend = 0;

        if (serie.Any())
        {
            foreach (var point in serie)
            {
                //calculates the sum of all values
                sumYi += point.Item2;
                sumXi += point.Item1;

                xn = point.Item1;
            }

            //Compute the average values
            avgYi = sumYi/serie.Count;
            double avgXi = sumXi/serie.Count;

            //calculate the trend (E (xi - xiMean) * (yi - yiMean))/(E(xi-xiMean)²) // E <=> Sum of
            double sumOfXiYi = 0;
            double sumOfPow = 0;

            // we must go through the loop before to compute avg, so we need to make 2 loops
            foreach (var point in serie)
            {
                sumOfXiYi += (point.Item1 - avgXi)*(point.Item2 - avgYi);
                sumOfPow += Math.Pow(point.Item1 - avgXi, 2);
            }

            double a = sumOfXiYi / sumOfPow; // the value is not scaled to Y values /!\
            double b = avgYi - (a * avgXi);
            const int Scale = 100;

            trend = b.NearlyZero() ? (a * xn) * Scale : (a * xn) / Math.Abs(b) * Scale;

            trend = Math.Round(trend, 2);
        }

        return trend;
    }

    public static double GetLeastSquaresTrend2(IList<Tuple<float, float>> serie)
    {
        double sumXi = 0, sumYi = 0, sumXY = 0, sumXPow = 0;
        float Xn = 0;
        double trend = 0;

        if (serie.Any())
        {
            foreach (var point in serie)
            {
                //Calculate the sum of Index
                sumXi += point.Item1;
                //Calculate the sum of Values
                sumYi += point.Item2;
                sumXY += (point.Item1 * point.Item2);
                //Calculate the sum(index²)
                sumXPow += Math.Pow(point.Item1, 2);

                Xn = point.Item1;
            }

            var nbData = serie.Count;
            //Calculate a_1=((N∑XY)-(∑X∙∑Y))/((N∑X^2 )-(∑X)^2 )
            var a1 = ((nbData*sumXY) - (sumXi*sumYi))/((nbData*sumXPow) - (Math.Pow(sumXi, 2)));

            //Calculate b=((∑Y∙∑X^2 )-(∑X∙∑XY))/((N∑X^2 )-(∑X)^2 )  
            var b = ((sumYi*sumXPow) - (sumXi*sumXY))/((nbData*sumXPow) - ((Math.Pow(sumXi, 2))));

            //the equation for the trend line as y = a1x + b
            //	Calculate the percentage % = (a_1•X)/b• 100   where x = nbData
            trend = (a1*Xn)/b*100;
            trend = Math.Round(trend, 4);
        }

        return trend;
    }

    /// <summary>
    /// Computing the max, min, avg and the trend values of an array
    /// use Least squares algorithm to compute trend
    /// </summary>
    /// <param name="serie"> list of dataPoint with X and Y coordinates, in float. 
    /// TODO TIM & MGN indexes scale determines the output trend scale (0,0001 vs 1 for example), this needs to be re-thought.</param>
    /// <param name="min">Out : the min value</param>
    /// <param name="max">Out : the max value</param>
    /// <param name="avg">Out : the avg value</param>
    public static void GetMinMaxAvg(IList<double> serie, out double min, out double max, out double avg)
    {
        double sum = 0;
        max = -Double.MaxValue;
        min = Double.MaxValue;
        avg = 0;

        if (!serie.Any()) return;

        foreach (var value in serie)
        {
            //calculates the sum of all values
            sum += value;

            //Compute the max value
            max = Math.Max(max, value);
            //Compute the min value
            min = Math.Min(min, value);
        }

        //Compute the average values
        avg = sum / serie.Count;
    }


    /// <summary>
    /// return discrepancy between values
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static double GetDiscrepancy(IEnumerable<double> values)
    {
        if (values != null && values.Any())
        {
            return values.Max() - values.Min();
        }

        return 0;
    }


    /// <summary>
    /// compute standard deviation
    /// </summary>
    /// <param name="values"></param>
    /// <param name="average"></param>
    /// <param name="stdDeviation"></param>
    /// <returns></returns>
    public static void CalculateStandardDeviation(IEnumerable<double> values, out double? average, out double? stdDeviation)
    {
        if (values != null && values.Any())
        {
            var avg = values.Average();
            average = avg;
            stdDeviation = Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
        else
        {
            average = null;
            stdDeviation = null;
        }
    }

 
    public static double? GetAverage(IList<KeyValuePair<DateTimeOffset, double>> values)
    {
        if (values == null || values.Any() == false)
        {
            return null;
        }

        var dataValues = values.ToList();

        var previousData = dataValues.First();
        DateTimeOffset dateStart = previousData.Key;
        DateTimeOffset dateEnd = dataValues.Last().Key;
        if (dateStart == dateEnd)
        {
            return previousData.Value;
        }

        dataValues.RemoveAt(0);

        double value = 0;
        foreach (var data in dataValues)
        {
            // Vn * (tn - t(n-1))
            value += ComputeAvgValue(previousData.Value, previousData.Key, data.Value, data.Key);
            previousData = data;
        }


        value = (1 / (dateEnd - dateStart).TotalMinutes) * value;

        return value;
    }


    public static double? GetAverageExcludeNullValue(IList<KeyValuePair<DateTimeOffset, double?>> values)
    {
        if (values == null || values.Any(x => x.Value.HasValue) == false)
        {
            return null;
        }

        var dataValues = values.ToList();

        var previousData = dataValues.First();
        DateTimeOffset dateStart = previousData.Key;
        DateTimeOffset dateEnd = dataValues.Last().Key;
        if (dateStart == dateEnd)
        {
            return previousData.Value;
        }

        dataValues.RemoveAt(0);

        double value = 0;
        double duration = 0;
        foreach (var data in dataValues)
        {
            if (previousData.Value.HasValue && data.Value.HasValue)
            {
                // Vn * (tn - t(n-1))
                value += ComputeAvgValue(previousData.Value.Value, previousData.Key, data.Value.Value, data.Key);
                duration += (data.Key - previousData.Key).TotalMinutes;
            }
            previousData = data;
        }

        return duration.NearlyZero() ? (double?) null : (1 / duration) * value;
    }


    private static double ComputeAvgValue(double previousValue, DateTimeOffset previousDate, double value, DateTimeOffset currentDate)
    {
        return ((previousValue + value) / 2) * (currentDate - previousDate).TotalMinutes;
    }


    public static double? GetAverageSquare(IList<KeyValuePair<DateTimeOffset, double>> values)
    {
        if (values == null || values.Any() == false)
        {
            return null;
        }

        var dataValues = values.ToList();

        var previousData = dataValues.First();
        DateTimeOffset dateStart = previousData.Key;
        DateTimeOffset dateEnd = dataValues.Last().Key;
        if (dateStart == dateEnd)
        {
            return previousData.Value;
        }

        dataValues.RemoveAt(0);

        double value = 0;
        foreach (var data in dataValues)
        {
            // Vn * (tn - t(n-1))
            value += ComputeAvgSquareValue(previousData.Value, previousData.Key, data.Value, data.Key);
            previousData = data;
        }

        value = (((double)1 / (double)3) * value) / (dateEnd - dateStart).TotalMinutes;

        return value;
    }


    public static double? GetAverageSquareExcludeNullValue(IList<KeyValuePair<DateTimeOffset, double?>> values)
    {
        if (values == null || values.Any() == false)
        {
            return null;
        }

        var dataValues = values.ToList();

        var previousData = dataValues.First();
        DateTimeOffset dateStart = previousData.Key;
        DateTimeOffset dateEnd = dataValues.Last().Key;
        if (dateStart == dateEnd)
        {
            return previousData.Value;
        }

        dataValues.RemoveAt(0);

        double value = 0;
        double duration = 0;
        foreach (var data in dataValues)
        {
            if (previousData.Value.HasValue && data.Value.HasValue)
            {
                value += ComputeAvgSquareValue(previousData.Value.Value, previousData.Key, data.Value.Value, data.Key);
                duration += (data.Key - previousData.Key).TotalMinutes;
            }
            previousData = data;
        }

        if (duration.NearlyZero())
        {
            return null;
        }

        value = (1 / duration) * value;

        value = (((double)1 / (double)3) * value);

        return value;

    }


    public static double? GetStandardDeviation(IList<KeyValuePair<DateTimeOffset, double>> values, double average)
    {
        double? avgSquare = GetAverageSquare(values);

        if (avgSquare.HasValue == false)
        {
            return null;
        }

        double value = avgSquare.Value - Math.Pow(average, 2);

        return Math.Sqrt(value);
    }


    public static double? GetStandardDeviationExcludeNullValue(IList<KeyValuePair<DateTimeOffset, double?>> values, double average)
    {
        double? avgSquare = GetAverageSquareExcludeNullValue(values);

        if (avgSquare.HasValue == false)
        {
            return null;
        }

        double value = avgSquare.Value - Math.Pow(average, 2);

        return Math.Sqrt(value);
    }


    private static double ComputeAvgSquareValue(double previousValue, DateTimeOffset previousDate, double value, DateTimeOffset currentDate)
    {
        return (currentDate - previousDate).TotalMinutes * (Math.Pow(previousValue, 2) + Math.Pow(value, 2) + (previousValue * value));
    }


}