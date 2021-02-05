using ControlLine.Threading;

namespace ControlLineIntegrationTests.ThreadOperationsTests.WaitUntilTimeout
{
    public class WaitUntilTimeoutTests
    {
        protected const int Timeout = 5000;
        protected byte[] Return;
        protected ThreadOperations Sut;

        protected void Init()
        {
            Return = new byte[] {10, 230};
            Sut = new ThreadOperations();
        }
    }
}