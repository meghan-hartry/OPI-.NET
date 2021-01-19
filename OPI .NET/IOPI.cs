using OPI_.NET.StimulusTypes;

namespace OPI_.NET
{
    /// <summary>
    /// Interface class for devices implementating the OPI library.
    /// </summary>
    public interface IOPI
    {
        /// <summary>
        /// Device OPI is implemented on.
        /// </summary>
        IDevice Device { get; set; }

        /// <summary>
        /// Type of device OPI is implemented on.
        /// </summary>
        string DeviceType { get; set; }

        /// <summary>
        /// Eye being tested.
        /// </summary>
        Eye Eye { get; set; }

        /// <summary>
        /// Generic function for presentation of a stimulus.
        /// </summary>
        /// <param name="stimulus">Stimulus of any type.</param>
        /// <returns><see cref="Response"/> to presentation.</returns>
        Response Present(Stimulus stimulus);

        /// <summary>
        /// Set the stimulus luminance level.
        /// </summary>
        /// <param name="cd">Luminance in cd/m2</param>
        void SetLevel(double cd);

        /// <summary>
        /// Set the stimulus positioning.
        /// </summary>
        /// <param name="x">Field of Vision Coordinate X</param>
        /// <param name="y">Field of Vision Coordinate Y</param>
        void SetPosition(double x, double y);
    }
}
