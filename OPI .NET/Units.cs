﻿using System;

namespace OPI_.NET
{
    /// <summary>
    /// Possible eyes to test.
    /// </summary>
    public enum Eye 
    {
        Left,
        Right
    }

    /// <summary>
    /// Class definition of a response object.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Constructor requires Seen parameter to be set.
        /// Time can also be set, but defaults to double.NaN.
        /// </summary>
        public Response(bool seen, double time = double.NaN)
        {
            if (seen && double.IsNaN(time)) throw new Exception("If the stimulus is seen, response time cannot be null.");
            if (seen && time <= 0) throw new Exception("Invalid response time.");

            this.Seen = seen;
            this.Time = time;
        }

        /// <summary>
        /// Response was detected in the allowed ResponseWindow.
        /// </summary>
        public bool Seen { get; set; }

        /// <summary>
        /// The time in milliseconds from the onset (or offset) of the presentation until the response from the subject. 
        /// Value should be double.NaN if <see cref="Seen"/> is false.
        /// </summary>
        public double Time { get; set; }
    }

    /// <summary>
    /// Defines conversion methods for relevant units.
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// Given a value in dB, return the cd/m^2 equivalent. Default is to use HFA units, so maximum stimulus is 10000 apostilbs.
        /// </summary>
        /// <param name="db">Value to convert to cd/m2</param>
        /// <param name="maxStim">Stimulus value for 0dB in cd/m2</param>
        /// <returns>Returns cd/m2 value</returns>
        public static double ToCandela(this double dB, double maxStim = 10000 / Math.PI)
        {
            return Math.Pow(maxStim * 10, -dB / 10);
        }

        /// <summary>
        /// Given a value in cd/m2, return the dB equivalent. Default is to use HFA units, so maximum stimulus is 10000 apostilbs.
        /// </summary>
        /// <param name="cd">Value to convert to dB in cd/m2</param>
        /// <param name="maxStim">Stimulus value for 0dB in cd/m2</param>
        /// <returns>Returns a dB value</returns>
        public static double ToDecibel(this double cd, double maxStim = 10000 / Math.PI)
        {
            if(cd <= 0) throw new Exception("Invalid input: cd/m2 value must be greater than 0.");

            return -10 * Math.Log10(cd / maxStim);
        }
    }
}
