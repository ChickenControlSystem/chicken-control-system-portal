using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace BLL.Common.Tests.Integration.BuildSerialSequence.RunTests.WithRecovery
{
    [TestFixture(1, 1, 1)]
    [TestFixture(1, 1, 3)]
    [TestFixture(1, 3, 1)]
    [TestFixture(1, 3, 3)]
    [TestFixture(3, 1, 1)]
    [TestFixture(3, 1, 3)]
    [TestFixture(3, 3, 1)]
    [TestFixture(3, 3, 3)]
    public class When_Second_Task_Fails_And_Recovery_Action_Succeeds : Given_A_Serial_Sequence_Is_Built
    {
        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;
        private IRunnable _mockRecoveryTask;

        private readonly int _runCountSecond;
        private readonly int _runCountFirst;
        private readonly int _runCountThird;

        public When_Second_Task_Fails_And_Recovery_Action_Succeeds(int runCountSecond, int runCountFirst,
            int runCountThird)
        {
            _runCountSecond = runCountSecond;
            _runCountFirst = runCountFirst;
            _runCountThird = runCountThird;
        }

        public override void When()
        {
            _mockRecoveryTask = Substitute.For<IRunnable>();
            _mockRecoveryTask
                .Run()
                .Returns(SequenceResultEnum.Success);
            _recoveryOptions = new RecoveryOptionsDto(true, _mockRecoveryTask.Run);

            MockFirstTask
                .RunCount
                .Returns(_runCountFirst);
            MockFirstTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            MockSecondTask
                .RecoveryOptions
                .Returns(_recoveryOptions);
            MockSecondTask
                .RunCount
                .Returns(_runCountSecond);
            MockSecondTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

            MockThirdTask
                .RunCount
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
        public void Then_Fisrt_Task_Is_Run_Once()
        {
            MockFirstTask
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
        public void Then_Second_Task_Is_Run_For_The_Ammount_Of_Times_Determined_In_The_Run_Count()
        {
            MockSecondTask
                .Received(_runCountSecond)
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