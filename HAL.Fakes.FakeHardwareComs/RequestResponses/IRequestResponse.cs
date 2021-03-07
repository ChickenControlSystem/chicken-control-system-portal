using System;

namespace HAL.Fakes.FakeHardwareComs.RequestResponses
{
    public interface IRequestResponse
    {
        public Tuple<byte[], byte[]> GetRequestResponse(bool isResponseFail = false);
    }
}