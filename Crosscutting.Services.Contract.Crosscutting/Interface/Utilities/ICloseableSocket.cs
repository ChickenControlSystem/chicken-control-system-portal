namespace Crosscutting.Services.Contract.Crosscutting.Interface.Utilities
{
    public interface ICloseableSocket
    {
        /// <summary>
        ///     closes client socket
        /// </summary>
        void Close();
    }
}