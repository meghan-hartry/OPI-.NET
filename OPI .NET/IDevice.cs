namespace OPI_.NET
{
    /// <summary>
    /// Defines properties shared by any device that implements the OPI library.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Minimum dynamic range of the device in cd/m2.
        /// Initialized in constructor. Defaults to 0 if not specified.
        /// </summary>
        double MinStimulus { get; set; }

        /// <summary>
        /// Maximum dynamic range of the device in cd/m2.
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
    }
}