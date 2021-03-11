using System;

namespace Tests.Fakes.HAL.FakeHardwareComs.RequestResponses
{
    public class MoveDoorRelative : RequestResponseBase
    {
        public MoveDoorRelative() : base(new Tuple<byte[], byte[], byte[]>(
            new byte[] {3, 2, 2, 120, 111, 0, 0, 0},
            new byte[] {1, 3, 0, 0, 0, 0, 0, 0},
            new byte[] {2, 3, 0, 0, 0, 0, 0, 0}
        ))
        {
        }
    }
}