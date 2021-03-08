using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using Crosscutting.Sequencing.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Integration.Crosscutting.Sequencing.BuildSerialSequence.RunTests.WithRecovery
{
    [TestFixture(1)]
    [TestFixture(3)]
    public class When_First_Task_Fails_And_Recovery_Action_Fails : Given_A_Serial_Sequence_Is_Built
    {
        private readonly int _runCount;

        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;

        private IRunnable _mockRecoveryTask;

        public When_First_Task_Fails_And_Recovery_Action_Fails(int runCount)
        {
            _runCount = runCount;
        }

        public override void When()
        {
            _mockRecoveryTask = Substitute.For<IRunnable>();
            _mockRecoveryTask
                .Run()
                .Returns(SequenceResultEnum.Fail);
            _recoveryOptions = new RecoveryOptionsDto(true, _mockRecoveryTask.Run);

            MockFirstTask
                .RecoveryOptions
                .Returns(_recoveryOptions);
            MockFirstTask
                .GetRunCount()
                .Returns(_runCount);
            MockFirstTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Fisrt_Task_Is_Run_For_The_Ammount_Of_Times_Determined_In_The_Run_Count()
        {
            MockFirstTask
                .Received(_runCount)
                .Run();
        }

        [Test]
        public void Then_Recovery_Action_Is_Run_Once()
        {
            _mockRecoveryTask
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Fail_Action_Is_Run_Once()
        {
            MockFirstTask
                .Received(1)
                .HandleFail();
        }

        [Test]
        public void Then_Seqeuence_Fails()
        {
            Assert.AreEqual(SequenceResultEnum.Fail, _result);
        }
    }
}