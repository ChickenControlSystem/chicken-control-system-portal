namespace ControlLine.Contract
{
    /// <summary>
    /// performs all the actions to send/recieve data on the control line
    /// </summary>
    public interface IControlLine
    {
        /// <summary>
        /// waits on the control line until data is received
        /// </summary>
        string Recieve();
        
        /// <summary>
        /// sends data over the control line
        /// </summary>
        void Send(string data);
    }
}