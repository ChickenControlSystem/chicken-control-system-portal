using Crosscutting.Sequencing.Sequence;
using Crosscutting.Sequencing.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Integration.Crosscutting.Sequencing.RunSequenceWithinSequence.RunTests
{
    public class When_Lower_Sequence_Fails_But_Sequence_Recovery_Succeeds : Given_Two_Sequences_Are_Built
    {
        private SequenceResultEnum _result;

        public override void When()
        {
            MockLowerSequenceTask
                .Run()
                .Returns(SequenceResultEnum.Fail);
            MockUpperSequenceSecondTask
                .Run()
                .Returns(SequenceResultEnum.Success);
            MockLowerSequenceTask
                .RecoveryOptions
                .Returns(new RecoveryOptionsDto());
            MockLowerSequenceRecoveryTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Task_In_Lower_Sequence_Ran_5_Times()
        {
            MockLowerSequenceTask
                .Received(5)
                .Run();
        }

        [Test]
        public void Then_Second_Task_In_Upper_Sequence_Ran_Once()
        {
            MockUpperSequenceSecondTask
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Sequence_Succeeds()
        {
            Assert.AreEqual(_result, SequenceResultEnum.Success);
        }
    }
}