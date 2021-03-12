using Tests.Fakes.HAL.FakeHardwareComs.RequestResponses;

namespace Tests.Fakes.HAL.FakeHardwareComs
{
    public class DefaultMockRequestResponseBuilder
    {
        private readonly FluentNSubsituteRequestResponseBuilder _fluentNSubsituteRequestResponseBuilder;

        public DefaultMockRequestResponseBuilder(
            FluentNSubsituteRequestResponseBuilder fluentNSubsituteRequestResponseBuilder)
        {
            _fluentNSubsituteRequestResponseBuilder = fluentNSubsituteRequestResponseBuilder;
        }

        public IRequestResponseCollection Build()
        {
            return _fluentNSubsituteRequestResponseBuilder
                .AddUpRequestResponse(
                    //move door absolute
                    new byte[] {2, 2, 1, 120, 0, 0, 0, 0},
                    new byte[] {1, 3, 0, 0, 0, 0, 0, 0}
                ).AddUpRequestResponse(
                    //move door relative
                    new byte[] {3, 2, 2, 120, 111, 0, 0, 0},
                    new byte[] {1, 3, 0, 0, 0, 0, 0, 0}
                ).AddUpRequestResponse(
                    //move door search
                    new byte[] {4, 2, 1, 3, 1, 1, 0, 0},
                    new byte[] {1, 3, 0, 0, 0, 0, 0, 0}
                ).AddUpRequestResponse(
                    //read light
                    new byte[] {1, 1, 0, 0, 0, 0, 0, 0},
                    new byte[] {1, 1, 200, 0, 0, 0, 0, 0}
                ).Build();
        }
    }
}