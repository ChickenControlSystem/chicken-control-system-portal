﻿using BLL.Common.Contract;
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
    public class When_Third_Task_Fails_And_Recovery_Action_Fails : Given_A_Serial_Sequence_Is_Built
    {
        private readonly int _runCountSecond;
        private readonly int _runCountFirst;
        private readonly int _runCountThird;

        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;

        private IRunnable _mockRecoveryTask;

        public When_Third_Task_Fails_And_Recovery_Action_Fails(int runCountSecond, int runCountFirst, int runCountThird)
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
                .Returns(SequenceResultEnum.Fail);
            _recoveryOptions = new RecoveryOptionsDto(true, _mockRecoveryTask.Run);

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
                .RecoveryOptions
                .Returns(_recoveryOptions);
            MockThirdTask
                .RunCount
                .Returns(_runCountThird);
            MockThirdTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

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
        public void Then_Third_Task_Is_Run_For_The_Ammount_Of_Times_Determined_In_The_Run_Count()
        {
            MockThirdTask
                .Received(_runCountThird)
                .Run();
        }

        [Test]
        public void Then_Fail_Action_Is_Run_Once()
        {
            MockThirdTask
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