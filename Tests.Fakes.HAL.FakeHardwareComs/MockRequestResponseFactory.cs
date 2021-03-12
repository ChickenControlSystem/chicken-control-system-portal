using NSubstitute;
using Tests.Fakes.HAL.FakeHardwareComs.RequestResponses;

namespace Tests.Fakes.HAL.FakeHardwareComs
{
    public class NSubsituteRequestResponseCollectionFactory
    {
        public IRequestResponseCollection GetMockRequestResponseCollection()
        {
            return Substitute.For<IRequestResponseCollection>();
        }
    }
}