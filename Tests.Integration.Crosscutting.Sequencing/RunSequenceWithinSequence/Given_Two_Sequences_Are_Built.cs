using Crosscutting.Sequencing.Sequence;
using Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing;
using Crosscutting.Threading;
using Crosscutting.UnitTest;
using NSubstitute;

namespace Tests.Integration.Crosscutting.Sequencing.RunSequenceWithinSequence
{
    public class Given_Two_Sequences_Are_Built : GenericGivenWhenThenTests<ISequence>
    {
        protected ISequence FirstTask;
        protected IRunnable MockUpperSequenceSecondTask;

        protected IRunnable MockLowerSequenceTask;
        protected IRunnable MockLowerSequenceRecoveryTask;

        private void SetUpTasks()
        {
            MockUpperSequenceSecondTask = Substitute.For<IRunnable>();
            MockUpperSequenceSecondTask
                .GetRunCount()
                .Returns(1);
            MockLowerSequenceRecoveryTask = Substitute.For<IRunnable>();
            MockLowerSequenceRecoveryTask
                .GetRunCount()
                .Returns(1);
            MockLowerSequenceTask = Substitute.For<IRunnable>();
            MockLowerSequenceTask
                .GetRunCount()
                .Returns(1);
        }

        protected override void Given()
        {
            SetUpTasks();

            FirstTask = GetSequenceBuilder()
                .Queue(MockLowerSequenceTask)
                .Serial()
                .Fail(MockFail)
                .RunCount(5)
                .Recovery(MockLowerSequenceRecoveryTask.Run)
                .Build();

            SUT = GetSequenceBuilder()
                .Queue(FirstTask)
                .Queue(MockUpperSequenceSecondTask)
                .Serial()
                .Fail(MockFail)
                .Build();
        }

        private static void MockFail()
        {
            //PRETEND TO LOG ERROR
        }

        private IFluentSequenceBuilder GetSequenceBuilder()
        {
            return new FluentSequenceBuilder(
                new SequenceFactory(
                    Substitute.For<IThreadOperations>()
                )
            );
        }
    }
}