using System.Drawing;

namespace OPI_.NET.StimulusTypes
{
    public class TemporalStimulus : Stimulus
    {
        /// <summary>
        /// Public constructor set default values of stimulus.
        /// </summary>
        public TemporalStimulus(double x, double y, double level = 5, double size = 0.43, string color = "White", double duration = 200, double responseWindow = 1500)
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
        /// Frequency with which lut is processed in Hz.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Total length of stimulus flash in milliseconds. There is no guarantee that duration \%\% length(lut)/rate == 0. 
        /// That is, the onus is on the user to ensure the duration is a multiple of the period of the stimuli.
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Maximum time (>= 0) in milliseconds to wait for a response from the onset of the stimulus presentation.
        /// </summary>
        public double ResponseWindow { get; set; }
    }
}
