using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;

namespace Tests.Fakes.HAL.FakeHardwareComs.RequestResponses
{
    public class FluentNSubsituteRequestResponseCollectionBuilder
    {
        private IRequestResponseCollection _requestResponseCollection;
        private readonly NSubsituteRequestResponseCollectionFactory _nSubsituteRequestResponseCollectionFactory;
        private readonly List<Tuple<byte[], byte[]>> _singleRequestResponses;
        private readonly List<Tuple<byte[], byte[], byte[]>> _doubleRequestResponses;
        private readonly List<Tuple<byte[], byte[], byte[], byte[]>> _trippleRequestResponses;

        public FluentNSubsituteRequestResponseCollectionBuilder(
            NSubsituteRequestResponseCollectionFactory nSubsituteRequestResponseCollectionFactory)
        {
            _nSubsituteRequestResponseCollectionFactory = nSubsituteRequestResponseCollectionFactory;
            _singleRequestResponses = new List<Tuple<byte[], byte[]>>();
            _doubleRequestResponses = new List<Tuple<byte[], byte[], byte[]>>();
            _trippleRequestResponses = new List<Tuple<byte[], byte[], byte[], byte[]>>();
        }

        public FluentNSubsituteRequestResponseCollectionBuilder AddUpRequestResponse(
            byte[] request,
            byte[] response
        )
        {
            _singleRequestResponses.Add(new Tuple<byte[], byte[]>(request, response));
            return this;
        }

        public FluentNSubsituteRequestResponseCollectionBuilder AddUpRequestResponse(
            byte[] request,
            byte[] response1,
            byte[] response2
        )
        {
            _doubleRequestResponses.Add(new Tuple<byte[], byte[], byte[]>(request, response1, response2));
            return this;
        }

        public FluentNSubsituteRequestResponseCollectionBuilder AddUpRequestResponse(
            byte[] request,
            byte[] response1,
            byte[] response2,
            byte[] response3
        )
        {
            _trippleRequestResponses.Add(
                new Tuple<byte[], byte[], byte[], byte[]>(request, response1, response2, response3));
            return this;
        }

        private FluentNSubsituteRequestResponseCollectionBuilder SetUpRequestResponse(
            byte[] request,
            byte[] response
        )
        {
            _requestResponseCollection
                .GetResponse(Arg.Is<byte[]>(
                    x => x.SequenceEqual(request)
                ))
                .Returns(response);
            return this;
        }

        private FluentNSubsituteRequestResponseCollectionBuilder SetUpRequestResponse(
            byte[] request,
            byte[] response1,
            byte[] response2
        )
        {
            _requestResponseCollection
                .GetResponse(Arg.Is<byte[]>(
                    x => x.SequenceEqual(request)
                ))
                .Returns(
                    response1,
                    response2
                );
            return this;
        }

        private FluentNSubsituteRequestResponseCollectionBuilder SetUpRequestResponse(
            byte[] request,
            byte[] response1,
            byte[] response2,
            byte[] response3
        )
        {
            _requestResponseCollection
                .GetResponse(Arg.Is<byte[]>(
                    x => x.SequenceEqual(request)
                ))
                .Returns(
                    response1,
                    response2,
                    response3
                );
            return this;
        }

        public IRequestResponseCollection Build()
        {
            _requestResponseCollection = _nSubsituteRequestResponseCollectionFactory
                .GetMockRequestResponseCollection();
            _singleRequestResponses
                .ForEach(x => SetUpRequestResponse(
                        x.Item1,
                        x.Item2
                    )
                );
            _doubleRequestResponses
                .ForEach(x => SetUpRequestResponse(
                        x.Item1,
                        x.Item2,
                        x.Item3
                    )
                );
            _trippleRequestResponses
                .ForEach(x => SetUpRequestResponse(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        x.Item4
                    )
                );
            return _requestResponseCollection;
        }
    }
}