using System.Drawing;

namespace OPI_.NET.StimulusTypes
{
    /// <summary>
    /// Properties shared by all types of stimulus.
    /// </summary>
    public class Stimulus
    {
        /// <summary>
        /// Coordinate of the center of stimulus in degrees relative to fixation.
        /// </summary>
        public Point Coordinate { get; set; }

        /// <summary>
        /// The stimulus level in cd/m^2. 
        /// </summary>
        public double Level { get; set; }

        /// <summary>
        /// The image to display. Can be null.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The size of stimulus (diameter in degrees), or a scaling factor for <see cref="Image"/>.
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// The color to use for the stimuli. Default is "White".
        /// </summary>
        public string Color { get; set; } = "White";
    }
}
