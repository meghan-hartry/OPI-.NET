namespace OPI_.NET
{
    /// <summary>
    /// Defines properties shared by any device using the Unity Engine that implements the OPI library.
    /// Inherits from <see cref="IDevice"/>.
    /// </summary>
    public interface IUnityDevice : IDevice
    {
        /// <summary>
        /// Convert from cd/m2 to the necessary display alpha (transparency).
        /// </summary>
        /// <param name="cd">Luminance in cd/m2 to convert</param>
        /// <returns>Alpha value to display</returns>
        double ToAlpha(double cd);

        /// <summary>
        /// Convert from visual field coordinates to a Unity transform vector.
        /// </summary>
        /// <param name="x">Field of Vision Coordinate X</param>
        /// <param name="y">Field of Vision Coordinate Y</param>
        /// <returns>Float array of x, y</returns>
        float[] ToVector(double x, double y);
    }
}
