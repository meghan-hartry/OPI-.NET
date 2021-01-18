namespace OPI_.NET
{
    public interface IDevice
    {
        /// <summary>
        /// Minimum dynamic range of the device in dB.
        /// Initialized in constructor. Defaults to 0 if not specified.
        /// </summary>
        double MinStimulus { get; set; }

        /// <summary>
        /// Maximum dynamic range of the device in dB.
        /// Initialized in constructor. Defaults to 40 if not specified.
        /// </summary>
        double MaxStimulus { get; set; }

        /// <summary>
        /// Vertical pixel resolution of the device.
        /// </summary>
        double VerticalResolution { get; set; }

        /// <summary>
        /// Horizontal pixel resolution for the device.
        /// </summary>
        double HorizontalResolution { get; set; }
        
        /// <summary>
        /// Convert from cd/m2 to the necessary display alpha.
        /// </summary>
        /// <param name="cd">cd/m2 to convert</param>
        /// <returns>Alpha value to display</returns>
        double ToAlpha(double cd);

        /// <summary>
        /// Convert from cd/m2 to the necessary display alpha.
        /// </summary>
        /// <param name="cd">cd/m2 to convert</param>
        /// <returns>Alpha value to display</returns>
        //double ToVector(double cd);
    }
}