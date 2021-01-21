using ControlLine.Threading;

namespace ControlLineIntegrationTests.ThreadOperationsTests.WaitUntilTimeout
{
    public class WaitUntilTimeoutTests
    {
        protected ThreadOperations Sut;
        protected const int Timeout = 5000;
        protected byte[] Return;

        protected void Init()
        {
            Return = new byte[] {10, 230};
            Sut = new ThreadOperations();
        }
    }
}