using System;

namespace HAL.Fakes.FakeHardwareComs.RequestResponses
{
    public class MoveDoorSearch : RequestResponseBase
    {
        public MoveDoorSearch() : base(new Tuple<byte[], byte[], byte[]>(
            new byte[] {4, 2, 1, 3, 1, 1, 0, 0},
            new byte[] {1, 3, 0, 0, 0, 0, 0, 0},
            new byte[] {3, 3, 0, 0, 0, 0, 0, 0}
        ))
        {
        }
    }
}