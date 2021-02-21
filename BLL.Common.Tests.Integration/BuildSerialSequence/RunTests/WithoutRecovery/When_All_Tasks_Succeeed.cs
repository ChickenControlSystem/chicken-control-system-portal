using BLL.Common.Contract;
using BLL.Common.Sequence;
using NSubstitute;
using NUnit.Framework;

namespace BLL.Common.Tests.Integration.BuildSerialSequence.RunTests.WithoutRecovery
{
    [TestFixture(1, 1, 1)]
    [TestFixture(1, 1, 3)]
    [TestFixture(1, 3, 1)]
    [TestFixture(1, 3, 3)]
    [TestFixture(3, 1, 1)]
    [TestFixture(3, 1, 3)]
    [TestFixture(3, 3, 1)]
    [TestFixture(3, 3, 3)]
    public class When_All_Tasks_Succeed : Given_A_SerialSequenceIsBuilt
    {
        private SequenceResultEnum _result;
        private IRunnable _mockRecoveryTask;

        private readonly int _runCountSecond;
        private readonly int _runCountFirst;
        private readonly int _runCountThird;

        public When_All_Tasks_Succeed(int runCountSecond, int runCountFirst, int runCountThird)
        {
            _runCountSecond = runCountSecond;
            _runCountFirst = runCountFirst;
            _runCountThird = runCountThird;
        }

        protected override void When()
        {
            MockFirstTask
                .RunCount
                .Returns(_runCountFirst);
            MockFirstTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            MockSecondTask
                .RunCount
                .Returns(_runCountSecond);
            MockSecondTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            MockThirdTask
                .RunCount
                .Returns(_runCountThird);
            MockThirdTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Fisrt_Task_Is_Run_Once()
        {
            MockFirstTask
                .Received(1)
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