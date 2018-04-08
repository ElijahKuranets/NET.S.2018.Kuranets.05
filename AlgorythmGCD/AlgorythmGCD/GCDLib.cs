using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorythmGCD
{
    /// <summary>
    /// Class calculate GCD of any set of numbers using two different algorythms
    /// </summary>
    public static class GCDLib
    {
        #region Public methods
        /// <summary>
        /// Calculates GCD of any set of integers using Euclidean algorithm
        /// </summary>
        /// <param name="numbers">input integers</param>
        /// <returns></returns>
        public static uint CalcEuclideanGCD(params int[] numbers)
        {
            return CalcGCD(out _, EuclideanAlgorythm, numbers);
        }
        /// <summary>
        /// Calculates GCD of any set of integers using Euclidean algorithm
        /// </summary>
        /// <param name="ticks">elapsed ticks</param>
        /// <param name="numbers">input numbers</param>
        /// <returns></returns>
        public static uint CalcEuclideanGCD(out long ticks, params int[] numbers)
        {
            return CalcGCD(out ticks, EuclideanAlgorythm, numbers);
        }
        /// <summary>
        /// Calculates GCD of any set of integers using Stein's algorithm
        /// </summary>
        /// <param name="numbers">input integers</param>
        /// <returns></returns>
        public static uint CalcSteinsGCD(params int[] numbers)
        {
            return CalcGCD(out _, SteinsAlgorithm, numbers);
        }

        public static uint CalcSteinsGCD(out long ticks, params int[] numbers)
        {
            return CalcGCD(out ticks, SteinsAlgorithm, numbers);
        }
        #endregion

        #region Private methods
        private static uint CalcGCD(out long ticks, Func<uint, uint, uint> algorythm, params int[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentException("Method requires at least 2 numbers");
            }

            var sw = Stopwatch.StartNew();
            uint gcd = (uint)Math.Abs(numbers[0]);
            for (int i = 1; i < numbers.Length && gcd > 1; i++)
            {
                gcd = algorythm.Invoke(gcd, (uint)Math.Abs(numbers[i]));
            }

            sw.Stop();
            ticks = sw.ElapsedTicks;

            return gcd;
        }

        private static uint EuclideanAlgorythm(uint a, uint b)
        {
            if (b == 0)
            {
                return a;
            }

            return EuclideanAlgorythm(b, a % b);
        }

        private static uint SteinsAlgorithm(uint a, uint b)
        {
            if (a == b)
            {
                return a;
            }
            if (a == 0)
            {
                return b;
            }
            if (b == 0)
            {
                return a;
            }
            if ((~a & 1) != 0)
            {
                if ((b & 1) != 0)
                {
                    return SteinsAlgorithm(a >> 1, b);
                }
                else
                {
                    return SteinsAlgorithm(a >> 1, b >> 1) << 1;
                }
            }

            if ((~b & 1) != 0)
            {
                return SteinsAlgorithm(a, b >> 1);
            }

            if (a > b)
            {
                return SteinsAlgorithm((a - b) >> 1, b);
            }

            return SteinsAlgorithm((b - a) >> 1, a);
        }
        #endregion
    }
}
