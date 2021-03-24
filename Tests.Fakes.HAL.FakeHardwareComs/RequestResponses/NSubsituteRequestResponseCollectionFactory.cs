using NSubstitute;

namespace Tests.Fakes.HAL.FakeHardwareComs.RequestResponses
{
    public class NSubsituteRequestResponseCollectionFactory
    {
        public IRequestResponseCollection GetMockRequestResponseCollection()
        {
            return Substitute.For<IRequestResponseCollection>();
        }
    }
}