namespace OPI_.NET
{
    /// <summary>
    /// Abstract class for OPI implementations.
    /// </summary>
    public interface IOPI
    {
        /// <summary>
        /// Device OPI is implemented on.
        /// </summary>
        string DeviceType { get; set; }

        /// <summary>
        /// Eye being tested.
        /// </summary>
        Eye Eye { get; set; }

    //    /// <summary>
    //    /// State of initialization.
    //    /// </summary>
    //    bool IsInitialized { get; private set; }

    //    /// <summary>
    //    /// Initialize the OPI implementation.
    //    /// </summary>
    //    public virtual void Initialize() 
    //    {
    //        this.IsInitialized = true;
    //    }

    //    /// <summary>
    //    /// Release the OPI implementation.
    //    /// </summary>
    //    public virtual void Release() 
    //    {
    //        this.IsInitialized = false;
    //    }
    }
}
