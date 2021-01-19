using System.Drawing;

namespace OPI_.NET.StimulusTypes
{
    /// <summary>
    /// A single, motionless static stimulus.
    /// </summary>
    public class StaticStimulus : Stimulus
    {
        /// <summary>
        /// Public constructor set default values of stimulus.
        /// </summary>
        public StaticStimulus(double x, double y, double level, double size = 0.43, string color = "White", double duration = 200, double responseWindow = 1500)
        {
            this.X = x;
            this.Y = y;
            this.Level = level;
            this.Size = size;
            this.Color = color;
            this.Duration = duration;
            this.ResponseWindow = responseWindow;
        }

        /// <summary>
        /// Total stimulus duration in milliseconds (>= 0).
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Maximum time (>= 0) in milliseconds to wait for a response.
        /// </summary>
        public double ResponseWindow { get; set; }
    }
}
