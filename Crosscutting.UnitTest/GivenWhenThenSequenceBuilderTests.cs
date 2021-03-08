using Crosscutting.Sequencing.Sequence;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing;
using Crosscutting.Threading;
using NSubstitute;

namespace Crosscutting.UnitTest
{
    public abstract class GivenWhenThenSequenceBuilderTests<T> : GenericGivenWhenThenTests<T>
        where T : ISimpleSequenceBuilder
    {
        protected FluentSequenceBuilder FluentSequenceBuilder;
        protected SequenceResultEnum Result;

        protected override void Given()
        {
            FluentSequenceBuilder = new FluentSequenceBuilder(new SequenceFactory(Substitute.For<IThreadOperations>()));
        }

        protected override void When()
        {
            Result = SUT
                .Build()
                .Run();
        }
    }
}