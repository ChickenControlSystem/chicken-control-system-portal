using System;

namespace Tests.Fakes.HAL.FakeHardwareComs.RequestResponses
{
    public interface IRequestResponse
    {
        public Tuple<byte[], byte[]> GetRequestResponse(bool isResponseFail = false);
    }
}