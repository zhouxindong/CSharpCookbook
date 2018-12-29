using System;

namespace CSharpCookbook
{
    public class Numberic
    {
        public static bool IsApproximatelyEqualTo(
            double numerator,
            double denominator,
            double dblvalue)
        {
            return IsApproximatelyEqualTo(numerator, denominator, dblvalue, double.Epsilon);
        }

        public static bool IsApproximatelyEqualTo(
            double numerator,
            double denominator,
            double dblvalue,
            double epsilon)
        {
            var diff = (numerator/denominator) - dblvalue;
            return Math.Abs(diff) < epsilon;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI/180)*degrees;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            return (180/Math.PI)*radians;
        }

        public static int GetHighWord(int value)
        {
            return (value & (0xFFFF << 16));
        }

        public static int GetLowWord(int value)
        {
            return (value & 0x0000FFFF);
        }

        public static double CtoF(double celsius)
        {
            return (((0.9/0.5)*celsius) + 32);
        }

        public static double FtoC(double fahrenheit)
        {
            return ((0.5/0.9)*(fahrenheit - 32));
        }
    }
}