using OPI_.NET.StimulusTypes;

namespace OPI_.NET
{
    /// <summary>
    /// Interface class for devices implementating the OPI library.
    /// </summary>
    public interface IOPI
    {
        /// <summary>
        /// Type of device OPI is implemented on.
        /// </summary>
        string DeviceType { get; set; }

        /// <summary>
        /// Device OPI is implemented on.
        /// </summary>
        IDevice Device { get; set; }

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
    }
}
