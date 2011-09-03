﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper math methods used by the engine.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Approximate value of Pi.
        /// </summary>
        public const float Pi = 3.141592653589793239f;

        /// <summary>
        /// Approximate value of Pi multiplied by two.
        /// </summary>
        public const float TwoPi = 6.283185307179586477f;

        /// <summary>
        /// Approximate value of Pi divided by two.
        /// </summary>
        public const float PiOver2 = 1.570796326794896619f;

        /// <summary>
        /// Approximate value of Pi divided by four.
        /// </summary>
        public const float PiOver4 = 0.785398163397448310f;

        /// <summary>
        /// Reduces the angle into a range from -Pi to Pi.
        /// </summary>
        /// <param name="angle">Angle to wrap.</param>
        /// <returns>Wrapped angle.</returns>
        public static float WrapAngle(float angle)
        {
            angle = (float)Math.IEEERemainder(angle, TwoPi);
            if (angle < -Pi)
            {
                angle += TwoPi;
                return angle;
            }
            if (angle >= Pi)
            {
                angle -= TwoPi;
            }
            return angle;

        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            return value;
        }

        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
    }
}
