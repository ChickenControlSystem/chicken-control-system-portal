using BLL.Common.Contract;
using BLL.Common.Sequence;
using NSubstitute;
using Threading;
using UnitTest;

namespace BLL.Common.Tests.Unit.BuildSerialSequence
{
    public class Given_A_SerialSequenceIsBuilt : GenericGivenWhenThenTests<ISequence>
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

        protected override void Given()
        {
            SetUpTasks();

            var sequenceBuilder = new SequenceBuilder(
                new SequenceFactory(
                    Substitute.For<IThreadOperations>()
                )
            );

            SUT = sequenceBuilder
                .EnqueueTask(MockFirstTask)
                .EnqueueTask(MockSecondTask)
                .EnqueueTask(MockThirdTask)
                .EndQueue()
                .MakeSerial()
                .AddFailAction(() =>
                {
                    //PRETEND TO LOG ERROR
                })
                .AddRecoveryFunc(() => SequenceResultEnum.Success)
                .Build();
        }
    }
}