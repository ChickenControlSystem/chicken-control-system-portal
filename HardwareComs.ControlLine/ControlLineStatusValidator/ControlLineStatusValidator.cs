using System;
using ControlLine.Contract;
using ControlLine.Exception.Hardware;
using ControlLine.Exception.Hardware.Axis;

namespace ControlLine.ControlLineStatusValidator
{
    //TODO: remove hardcode
    public class ControlLineStatusValidator : IControlLineStatusValidator
    {
        public DeviceFailiure ValidateError(byte status)
        {
            return status switch
            {
                2 => new AxisObstruction(),
                3 => new AxisSearchTimeOut(),
                4 => new DeviceOffline(),
                _ => throw new ArgumentException()
            };
        }

        public bool IsError(byte status)
        {
            try
            {
                ValidateError(status);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}