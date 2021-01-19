using System;
using System.Collections.Generic;
using OPI_.NET.StimulusTypes;

namespace OPI_.NET.Tests
{
    /// <summary>
    ///  'Not' if staircase has not finished, or one of 'Rev' (finished due to 2 reversals), 'Max' (finished due to 2 
    ///  maxStimulus seen), 'Min' (finished due to 2 stimulus of minimum dB not seen).
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Test is not finished.
        /// </summary>
        Not,

        /// <summary>
        /// Finished due to 2 stimulus of the device's maximum dB not being seen.
        /// </summary>
        Max,

        /// <summary>
        /// Finished due to 2 stimulus of the device's minimum dB not being seen.
        /// </summary>
        Min,

        /// <summary>
        /// Finished due to 2 reversals
        /// </summary>
        Rev
    }

    public class FourTwoTest
    {
        /// <summary>
        /// Maximum dB level achievable by the device.
        /// </summary>
        public double MaxdB { get; set; }

        /// <summary>
        /// Minimum dB level achievable by the device.
        /// </summary>
        public double MindB { get; set; }

        /// <summary>
        /// Current stimulus level in dB, initialized in constructor. 
        /// Defaults to 5 if not specified.
        /// </summary>
        public double CurrentLevel { get; set; }

        /// <summary>
        /// Result of test.
        /// </summary>
        public double StairResult { get; set; }

        /// <summary>
        /// Current state of the test. Initialized to "Not".
        /// See possible values of enum <see cref="Status"/>.
        /// </summary>
        public Status Finished { get; set; } = Status.Not;

        /// <summary>
        /// Number of reversals. Initialized to 0.
        /// </summary>
        public double NumberOfReversals { get; set; } = 0;

        /// <summary>
        /// Number of times <see cref="MaxStimulus"/> has been seen. Initialized to 0.
        /// </summary>
        public double CurrSeenLimit { get; set; } = 0;

        /// <summary>
        /// Number of times <see cref="MinStimulus"/> has not been seen. Initialized to 0.
        /// </summary>
        public double CurrNotSeenLimit { get; set; } = 0;

        /// <summary>
        /// Number of presentations. Initialized to 0.
        /// </summary>
        public double NumPresentations { get; set; } = 0;

        /// <summary>
        /// List of stimulus that have been presented.
        /// </summary>
        public List<StaticStimulus> Stimuli { get; set; }

        /// <summary>
        /// List of responses received. 
        /// <see cref="Response"/> contains whether seen and the time of response.
        /// </summary>
        public List<Response> Responses { get; set; }

        /// <summary>
        /// Reference to OPI implementation class.
        /// </summary>
        public IOPI Implementation { get; set; }

        /// <summary>
        /// First stimulus passed in by constructor.
        /// </summary>
        public StaticStimulus FirstStimulus { get; set; }

        /// <summary>
        /// Public constructor. Sets default parameters.
        /// </summary>
        /// <param name="implementation">Reference to Unity class device</param>
        /// <param name="firstStimulus">First stimulus to present</param>
        public FourTwoTest(IOPI implementation, StaticStimulus firstStimulus)
        {
            this.Implementation = implementation;
            this.FirstStimulus = firstStimulus;
            this.CurrentLevel = firstStimulus.Level;
            this.Responses = new List<Response>();
            this.MindB = Conversions.ToDecibel(this.Implementation.Device.MinStimulus, this.Implementation.Device.MaxStimulus);
            this.MaxdB = Conversions.ToDecibel(this.Implementation.Device.MaxStimulus, this.Implementation.Device.MaxStimulus);
        }

        /// <summary>
        /// Present the next stimulus.
        /// </summary>
        public void StartPresentation()
        {
            if (this.Finished != Status.Not)
            {
                throw new Exception("Error: Stepping fourTwo staircase when it has already terminated");
            }

            // If this is the first presentation, use the first stimulus.
            if(this.Stimuli == null)
            {
                this.Stimuli = new List<StaticStimulus>() { this.FirstStimulus };
            }
            else
            {
                // Create new stimulus and add to list.
                this.Stimuli.Add(this.MakeStimulus(this.CurrentLevel));
            }
            
            // Call present with the new stimulus, and add response to the list of responses.
            this.Implementation.Present(this.Stimuli[this.Stimuli.Count - 1]);
        }

        /// <summary>
        /// Record results of stimulus presentation.
        /// </summary>
        public void RecordResults()
        {
            this.NumPresentations++;
            var seen = this.Responses[this.Responses.Count - 1].Seen;

            // If last response was not seen, and we are at min stimulus
            if (this.CurrentLevel <= this.MindB && !seen)
            {
                this.CurrNotSeenLimit++;
            }

            // If last response was seen, and we are at max stimulus
            if (this.CurrentLevel >= this.MaxdB && seen)
            {
                this.CurrSeenLimit++;
            }

            // Check for reversals
            if (this.NumPresentations > 1 && seen != this.Responses[this.Responses.Count - 2].Seen)
            {
                this.NumberOfReversals++;
            }

            // Check if the staircase is finished.
            if (this.NumberOfReversals >= 2)
            {
                this.Finished = Status.Rev;

                //mean of last two stimulus levels in dB
                var secondLastdB = Conversions.ToDecibel(this.Stimuli[this.Stimuli.Count - 1].Level, this.Implementation.Device.MaxStimulus);
                var lastdB = Conversions.ToDecibel(this.Stimuli[this.Stimuli.Count - 2].Level, this.Implementation.Device.MaxStimulus);
                this.StairResult = (secondLastdB + lastdB) / 2;
            }
            else if (this.CurrNotSeenLimit >= 2)
            {
                this.Finished = Status.Min;
                this.StairResult = this.MindB;
            }
            else if (this.CurrSeenLimit >= 2)
            {
                this.Finished = Status.Max;
                this.StairResult = this.MaxdB;
            }
            else
            {
                // Update stimulus for next presentation.
                var delta = (NumberOfReversals == 0 ? 4 : 2) * (seen ? 1 : -1);
                this.CurrentLevel = Math.Min(this.MaxdB, Math.Max(this.MindB, this.CurrentLevel + delta));
            }
        }

        /// <summary>
        /// A function that takes a dB value and numPresentations and returns an OPI datatype ready for passing to Present.
        /// Only level should differ from initial stimulus.
        /// </summary>
        /// <param name="dB">The stimulus level in dB</param>
        private StaticStimulus MakeStimulus(double dB)
        {
            return new StaticStimulus(this.Stimuli[0].X,
                                      this.Stimuli[0].Y,
                                      Conversions.ToCandela(dB, this.Implementation.Device.MaxStimulus),
                                      this.Stimuli[0].Size,
                                      this.Stimuli[0].Color,
                                      this.Stimuli[0].Duration,
                                      this.Stimuli[0].ResponseWindow);
        }

        /// <summary>
        /// A function that takes a dB value and numPresentations and returns an OPI datatype ready for passing to Present.
        /// </summary>
        /// <param name="dB">Level of luminance in dB</param>
        private void MakeStimulusHelper(double dB, int n, double x, double y)
        {
            //ff < -function(db, n) db + n
            //#'   body(ff) <- substitute({
            //#'     s <- list(x=x, y=y, level=dbTocd(db), size=0.43, color="white",
            //#'               duration=200, responseWindow=1500)
            //#'     class(s) <- "opiStaticStimulus"
            //#'     return(s)}, list(x=x,y=y))
            //#'   return(ff)
        }
    }
}