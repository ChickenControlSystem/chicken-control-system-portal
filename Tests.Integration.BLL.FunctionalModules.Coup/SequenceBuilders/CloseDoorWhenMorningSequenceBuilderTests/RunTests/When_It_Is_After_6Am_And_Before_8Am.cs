using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Integration.BLL.FunctionalModules.Coup.SequenceBuilders.CloseDoorWhenMorningSequenceBuilderTests.
    RunTests
{
    [TestFixture(21600001d, 12)]
    [TestFixture(25200000d, 6)]
    [TestFixture(28799999d, 1)]
    public class When_It_Is_After_6Am_And_Before_8Am : Given_CloseDoorWhenMorningSequenceBuilder
    {
        private readonly double _timeInMilli;
        private readonly int _runCount;

        public When_It_Is_After_6Am_And_Before_8Am(double timeInMilli, int runCount)
        {
            _timeInMilli = timeInMilli;
            _runCount = runCount;
        }

        protected override void When()
        {
            MockTimeService
                .MilisecondsNow()
                .Returns(_timeInMilli);
            MockOpenDoorCommand
                .Run()
                .Returns(SequenceResultEnum.Success);
            MockCheckForLightCommand
                .Run()
                .Returns(SequenceResultEnum.Success);

            base.When();
        }

        [Test]
        public void Then_Delay_Was_Not_Run()
        {
            MockDelay
                .DidNotReceive()
                .Run();
        }

        [Test]
        public void Then_Check_For_Light_Command_Was_Run_Once()
        {
            MockCheckForLightCommand
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Open_Door_Command_Was_Run_Once()
        {
            MockOpenDoorCommand
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Check_For_Light_Command_Run_Count_Was_Valid()
        {
            MockCheckForLightCommand
                .Received()
                .SetRunCountBeforeMorning(_runCount);
        }

        [Test]
        public void Then_Command_Was_Successful()
        {
            Assert.AreEqual(SequenceResultEnum.Success, Result);
        }
    }
}