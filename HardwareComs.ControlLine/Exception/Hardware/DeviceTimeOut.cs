namespace ControlLine.Exception.Hardware
{
    /// <summary>
    /// when a device fails to return status in a specified time
    /// </summary>
    public abstract class DeviceTimeOut : DeviceFailiure
    {
    }
}