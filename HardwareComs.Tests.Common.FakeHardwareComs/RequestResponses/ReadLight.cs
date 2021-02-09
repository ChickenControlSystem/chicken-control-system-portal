using System;

namespace HardwareComs.Tests.Common.FakeHardwareComs.RequestResponses
{
    public class ReadLight : RequestResponseBase
    {
        public ReadLight() : base(new Tuple<byte[], byte[], byte[]>(
            new byte[] {1, 1, 0, 0, 0, 0, 0, 0},
            new byte[] {1, 1, 200, 0, 0, 0, 0, 0},
            new byte[] {4, 3, 0, 0, 0, 0, 0, 0}
        ))
        {
        }
    }
}