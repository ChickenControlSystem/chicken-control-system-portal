using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using NSubstitute;
using Crosscutting.Threading;
using Crosscutting.UnitTest;

namespace Crosscutting.Sequencing.Tests.Integration.BuildSerialSequence
{
    public class Given_A_Serial_Sequence_Is_Built : GenericGivenWhenThenTests<ISequence>
    {
        protected IRunnable MockFirstTask;
        protected IRunnable MockSecondTask;
        protected IRunnable MockThirdTask;

        private void SetUpTasks()
        {
            MockFirstTask = Substitute.For<IRunnable>();
            MockSecondTask = Substitute.For<IRunnable>();
            MockThirdTask = Substitute.For<IRunnable>();
        }

        public override void Given()
        {
            SetUpTasks();

            var sequenceBuilder = new FluentSequenceBuilder(
                new SequenceFactory(
                    Substitute.For<IThreadOperations>()
                )
            );

            SUT = sequenceBuilder
                .Queue(MockFirstTask)
                .Queue(MockSecondTask)
                .Queue(MockThirdTask)
                .Serial()
                .Fail(MockFail)
                .Build();
        }

        private static void MockFail()
        {
            //PRETEND TO LOG ERROR
        }
    }
}