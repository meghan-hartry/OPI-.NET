using System;

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
    /// Possible units of measurement for <see cref="Distance"/>.
    /// </summary>
    public enum DistanceUnit
    {
        /// <summary>
        /// HFA dB units.
        /// </summary>
        dB,

        /// <summary>
        ///  Sigma is the standard deviation of the fitted FoS.
        /// </summary>
        sigma
    }

    /// <summary>
    /// Response times to white-on-white Goldmann Size III targets for 12 subjects. 
    /// </summary>
    public class ResponseTime
    {
        /// <summary>
        /// Reaction time in ms.
        /// </summary>
        public double ReactionTime { get; set; }

        /// <summary>
        /// Distance of stimuli from measured threshold in <see cref="DistanceUnit"/>.
        /// The threshold was determined by post-hoc fit of a cummulative gaussian FoS curve to the data for each location and subject. 
        /// </summary>
        public double Distance { private get; set; }

        /// <summary>
        /// <see cref="Distance"/> is measured in dB or sigma units. Default is dB.
        /// </summary>
        public DistanceUnit DistanceUnit { get; set; } = DistanceUnit.dB;

        /// <summary>
        /// Identifier of each subject.
        /// </summary>
        public string Subject { get; set; }
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
        public static double ToCandelaPerSquareMeter(this double db, double maxStim = 10000 / Math.PI)
        {
            return Math.Pow(maxStim * 10, -db / 10);
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
