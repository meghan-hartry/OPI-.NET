using System.Drawing;

namespace OPI_.NET.StimulusTypes
{
    /// <summary>
    /// An implementation of a KineticStimulus will be a collection of KinecticStimulus to show movement.
    /// </summary>
    public class KineticStimulus : Stimulus
    {
        /// <summary>
        /// Public constructor set default values of stimulus.
        /// </summary>
        public KineticStimulus(double x, double y, double level = 5, double size = 0.43, string color = "White")
        {
            this.X = x;
            this.Y = y;
            this.Level = level;
            this.Size = size;
            this.Color = color;
        }

        /// <summary>
        /// The speed (degrees per second) for the stimulus to traverse the <see cref="Coordinate"/>s.
        /// </summary>
        public double Speeds { get; set; }
    }
}
