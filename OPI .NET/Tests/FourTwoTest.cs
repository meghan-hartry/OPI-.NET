using System;
using System.Collections.Generic;
using OPI_.NET.StimulusTypes;

namespace OPI_.NET.Tests
{
    /// <summary>
    ///  'Not' if staircase has not finished, or one of 'Rev' (finished due to 2 reversals), 'Max' (finished due to 2 
    ///  maxStimulus seen), 'Min' (finished due to 2 minStimulus not seen).
    /// </summary>
    public enum Status
    {
        Not,
        Max,
        Min,
        Rev
    }

    public class FourTwoTest
    {
        /// <summary>
        /// Starting estimate in dB, initialized in constructor. 
        /// Defaults to 25 if not specified.
        /// </summary>
        public double StartingEstimate { get; set; }

        /// <summary>
        /// Current stimulus level in dB, initialized in constructor. 
        /// Defaults to 25 if not specified.
        /// </summary>
        public double CurrentLevel { get; set; }

        /// <summary>
        /// Last stimulus level that was seen.
        /// </summary>
        public double LastSeenLevel { get; set; }

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
        /// Reference to device class.
        /// </summary>
        public IOPI Implementation { get; set; }

        /// <summary>
        /// Public constructor. Sets default parameters.
        /// </summary>
        /// <param name="implementation">Reference to Unity class device</param>
        /// <param name="est">Starting estimate in dB.</param>
        public FourTwoTest(IOPI implementation, double est = 25)
        {
            this.Implementation = implementation;
            this.StartingEstimate = est;
            this.CurrentLevel = est;
            this.Stimuli = new List<StaticStimulus>();
            this.Responses = new List<Response>();
        }

        public void Test()
        {
            //### res <- lapply(0:40, function(tt) {
            //###     lapply(1:1000, function(i) {
            //###         s <- fourTwo.start(makeStim=makeStim, tt=tt, fpr=0.15, fnr=0.3)
            //###         s <- fourTwo.step(s)
            //###         while(!fourTwo.stop(s$state)) {
            //###             s <- fourTwo.step(s$state)
            //###         }
            //###         fourTwo.final(s$state)
            //###     })

            //final?
            //Step();
            //this.VIVE.Present(this.MakeStimulus(25));
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

            // Create new stimulus and add to list.
            this.Stimuli.Add(this.MakeStimulus(this.CurrentLevel));

            // Call present with the new stimulus, and add response to the list of responses.
            this.Implementation.Present(this.Stimuli[this.Stimuli.Count - 1]);
        }

        /// <summary>
        /// Record results of stimulus presentation.
        /// </summary>
        public void RecordResults()
        {
            //while (!is.null(opiResp$err))
            //    opiResp <- do.call(opiPresent, params)

            //state$stimuli          <- c(state$stimuli, state$currentLevel)

            this.NumPresentations++;
            var seen = this.Responses[this.Responses.Count - 1].Seen;

            // If last response was seen
            if (seen)
            {
                this.LastSeenLevel = this.CurrentLevel;
            }
            
            
            // If last response was not seen, and we are at min stimulus
            if (this.CurrentLevel == this.Implementation.Device.MinStimulus && !seen)
            {
                this.CurrNotSeenLimit++;
            }

            // If last response was seen, and we are at max stimulus
            if (this.CurrentLevel == this.Implementation.Device.MaxStimulus && seen)
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
                //    state$stairResult <- mean(tail(state$stimuli, 2)) # mean of last two
                //mean of last two stimulus
                //this.stairResult = (this.stimuli[stimulusCount] + this.stimuli[stimulusCount - 1]) / 2;
            }
            else if (this.CurrNotSeenLimit >= 2)
            {
                this.Finished = Status.Min;
                this.StairResult = this.Implementation.Device.MinStimulus;
            }
            else if (this.CurrSeenLimit >= 2)
            {
                this.Finished = Status.Max;
                this.StairResult = this.Implementation.Device.MaxStimulus;
            }
            else
            {
                // Update stimulus for next presentation.
                var delta = (NumberOfReversals == 0 ? 4 : 2) * (seen ? 1 : -1);
                this.CurrentLevel = System.Math.Min(this.Implementation.Device.MaxStimulus, System.Math.Max(this.Implementation.Device.MinStimulus, this.CurrentLevel + delta));
            }
        }

        /// <summary>
        /// >A function that takes a dB value and numPresentations and returns an OPI datatype ready for passing to Present.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private StaticStimulus MakeStimulus(double db)
        {
            return new StaticStimulus()
            {
                X = 1,
                Y = 1,
                Level = Conversions.ToCandela(db),
                Size = 0.43,
                Color = "White",
                Duration = 200,
                ResponseWindow = 1500
            };
        }
    }
}