using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Utils;
using Crosscutting.Sequencing.Sequence;
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