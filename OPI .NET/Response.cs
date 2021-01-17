using System;

namespace OPI_.NET
{
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
}
