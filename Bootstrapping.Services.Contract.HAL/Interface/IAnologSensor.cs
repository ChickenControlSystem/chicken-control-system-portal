namespace Bootstrapping.Services.Contract.HAL.Interface
{
    public interface IAnologSensor : IDevice
    {
        public double Value { get; set; }
    }
}