using System;
using System.Collections.Generic;
using Tests.Fakes.HAL.FakeHardwareComs.RequestResponses;

namespace Tests.Fakes.HAL.FakeHardwareComs
{
    internal static class RequestResponseCollection
    {
        public static IEnumerable<Tuple<byte[], byte[]>> GetRequestResponseCollection(
            RequestResponseFlagsDto requestResponseFlags
        )
        {
            var requestResponseCollection = new List<Tuple<byte[], byte[]>>
            {
                new ReadLight().GetRequestResponse(requestResponseFlags.ReadLightFail),
                new MoveDoorAbsolute().GetRequestResponse(requestResponseFlags.AbsoluteMoveDoorFail),
                new MoveDoorRelative().GetRequestResponse(requestResponseFlags.RelativeMoveDoorFail),
                new MoveDoorSearch().GetRequestResponse(requestResponseFlags.SearchMoveDoorFail)
            };

            return requestResponseCollection;
        }
    }
}