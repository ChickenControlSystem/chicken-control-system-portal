using System;

namespace HAL.Fakes.FakeHardwareComs.RequestResponses
{
    public class MoveDoorAbsolute : RequestResponseBase
    {
        public MoveDoorAbsolute() : base(new Tuple<byte[], byte[], byte[]>(
            new byte[] {2, 2, 1, 120, 0, 0, 0, 0},
            new byte[] {1, 3, 0, 0, 0, 0, 0, 0},
            new byte[] {4, 3, 0, 0, 0, 0, 0, 0}
        ))
        {
        }
    }
}