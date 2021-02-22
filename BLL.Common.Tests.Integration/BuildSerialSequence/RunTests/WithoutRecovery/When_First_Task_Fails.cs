using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace BLL.Common.Tests.Integration.BuildSerialSequence.RunTests.WithoutRecovery
{
    [TestFixture(1)]
    [TestFixture(3)]
    public class When_First_Task_Fails : Given_A_Serial_Sequence_Is_Built
    {
        private readonly int _runCount;
        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;

        public When_First_Task_Fails(int runCount)
        {
            _runCount = runCount;
        }

        public override void When()
        {
            _recoveryOptions = new RecoveryOptionsDto();
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