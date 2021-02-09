using System;

namespace HardwareComs.Tests.Common.FakeHardwareComs.RequestResponses
{
    public interface IRequestResponse
    {
        public Tuple<byte[], byte[]> GetRequestResponse(bool isResponseFail = false);
    }
}