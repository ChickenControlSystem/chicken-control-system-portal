using Tests.Fakes.HAL.FakeHardwareComs.RequestResponses;

namespace Tests.Fakes.HAL.FakeHardwareComs
{
    public static class FakeHardwareComsHelper
    {
        public static IRequestResponseCollection GetDefaultMockRequestResponseCollection()
        {
            var defaultMockBuilder = new DefaultMockRequestResponseBuilder(
                new FluentNSubsituteRequestResponseBuilder(
                    new NSubsituteRequestResponseCollectionFactory()
                )
            );
            return defaultMockBuilder.Build();
        }
    }
}