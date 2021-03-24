namespace Tests.Fakes.HAL.FakeHardwareComs.RequestResponses
{
    public interface IRequestResponseCollection
    {
        public byte[] GetResponse(byte[] request);
    }
}