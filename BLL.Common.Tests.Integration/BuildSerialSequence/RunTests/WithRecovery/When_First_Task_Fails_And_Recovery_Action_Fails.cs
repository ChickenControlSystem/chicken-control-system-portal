using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace BLL.Common.Tests.Integration.BuildSerialSequence.RunTests.WithRecovery
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

        protected override void When()
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
                .RunCount
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