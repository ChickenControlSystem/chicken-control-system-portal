using BLL.Common.Contract;
using BLL.Common.Sequence;
using NSubstitute;
using Threading;
using UnitTest;

namespace BLL.Common.Tests.Integration.RunSequenceWithinSequence
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
            MockLowerSequenceRecoveryTask = Substitute.For<IRunnable>();
            MockLowerSequenceTask = Substitute.For<IRunnable>();
        }

        public override void Given()
        {
            SetUpTasks();

            FirstTask = GetSequenceBuilder()
                .Queue(MockLowerSequenceTask)
                .Serial()
                .Fail(MockFail)
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

        private ISequenceBuilder GetSequenceBuilder()
        {
            return new SequenceBuilder(
                new SequenceFactory(
                    Substitute.For<IThreadOperations>()
                )
            );
        }
    }
}