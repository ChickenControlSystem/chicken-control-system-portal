using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using Crosscutting.Sequencing.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Integration.Crosscutting.Sequencing.BuildSerialSequence.RunTests.WithRecovery
{
    [TestFixture(1, 1, 1)]
    [TestFixture(1, 1, 3)]
    [TestFixture(1, 3, 1)]
    [TestFixture(1, 3, 3)]
    [TestFixture(3, 1, 1)]
    [TestFixture(3, 1, 3)]
    [TestFixture(3, 3, 1)]
    [TestFixture(3, 3, 3)]
    public class When_First_Task_Fails_And_Recovery_Action_Succeeds : Given_A_Serial_Sequence_Is_Built
    {
        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;
        private IRunnable _mockRecoveryTask;

        private readonly int _runCountSecond;
        private readonly int _runCountFirst;
        private readonly int _runCountThird;

        public When_First_Task_Fails_And_Recovery_Action_Succeeds(int runCountSecond, int runCountFirst,
            int runCountThird)
        {
            _runCountSecond = runCountSecond;
            _runCountFirst = runCountFirst;
            _runCountThird = runCountThird;
        }

        protected override void When()
        {
            _mockRecoveryTask = Substitute.For<IRunnable>();
            _mockRecoveryTask
                .Run()
                .Returns(SequenceResultEnum.Success);
            _recoveryOptions = new RecoveryOptionsDto(true, _mockRecoveryTask.Run);

            MockFirstTask
                .RecoveryOptions
                .Returns(_recoveryOptions);
            MockFirstTask
                .GetRunCount()
                .Returns(_runCountFirst);
            MockFirstTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

            MockSecondTask
                .GetRunCount()
                .Returns(_runCountSecond);
            MockSecondTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            MockThirdTask
                .GetRunCount()
                .Returns(_runCountThird);
            MockThirdTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Recovery_Action_Is_Run_Once()
        {
            _mockRecoveryTask
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Fisrt_Task_Is_Run_For_The_Ammount_Of_Times_Determined_In_The_Run_Count()
        {
            MockFirstTask
                .Received(_runCountFirst)
                .Run();
        }

        [Test]
        public void Then_Second_Task_Is_Run_Once()
        {
            MockSecondTask
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Third_Task_Is_Run_Once()
        {
            MockThirdTask
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Fail_Action_Is_Not_Run()
        {
            MockFirstTask
                .DidNotReceive()
                .HandleFail();
        }

        [Test]
        public void Then_Seqeuence_Succeeds()
        {
            Assert.AreEqual(SequenceResultEnum.Success, _result);
        }
    }
}