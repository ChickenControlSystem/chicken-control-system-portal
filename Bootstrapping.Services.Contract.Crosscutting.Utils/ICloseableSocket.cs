namespace Bootstrapping.Services.Contract.Crosscutting.Utils
{
    public interface ICloseableSocket
    {
        /// <summary>
        ///     closes client socket
        /// </summary>
        void Close();
    }
}