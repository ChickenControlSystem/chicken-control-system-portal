using System;

namespace HardwareComs.Tests.Common.FakeHardwareComs.RequestResponses
{
    public abstract class RequestResponseBase : IRequestResponse
    {
        protected readonly Tuple<byte[], byte[], byte[]> RequestResponse;

        protected RequestResponseBase(Tuple<byte[], byte[], byte[]> requestResponse)
        {
            RequestResponse = requestResponse;
        }

        public Tuple<byte[], byte[]> GetRequestResponse(bool isResponseFail = false)
        {
            if (isResponseFail)
                return new Tuple<byte[], byte[]>(
                    RequestResponse.Item1,
                    RequestResponse.Item3
                );
            return new Tuple<byte[], byte[]>(
                RequestResponse.Item1,
                RequestResponse.Item2
            );
        }
    }
}