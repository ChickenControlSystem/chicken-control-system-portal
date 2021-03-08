using System.Net;

namespace Crosscutting.ApplicationConfiguration.Enviroment.Configuration
{
    public class ControlLineSettings
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public IPEndPoint GetEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(Ip), Port);
        }
    }
}