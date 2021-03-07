using Crosscutting.Sequencing.Sequence;
using NSubstitute;
using NUnit.Framework;

namespace BLL.FunctionalModules.Door.Tests.Integration.SequenceBuilders.CloseDoorWhenMorningSequenceBuilderTests.
    RunTests
{
    [TestFixture(57600001d, 50399999d)]
    [TestFixture(72000000d, 36000000d)]
    [TestFixture(0d, 21600000d)]
    [TestFixture(1800000d, 19800000d)]
    [TestFixture(3600000d, 18000000d)]
    [TestFixture(14400000d, 7200000d)]
    [TestFixture(21599999d, 1d)]
    public class When_It_Is_After_4Pm_And_Before_6Am : Given_CloseDoorWhenMorningSequenceBuilder
    {
        private readonly double _timeInMilli;
        private readonly double _calculatedDelay;

        public When_It_Is_After_4Pm_And_Before_6Am(double timeInMilli, double calculatedDelay)
        {
            _timeInMilli = timeInMilli;
            _calculatedDelay = calculatedDelay;
        }

        public override void When()
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
        public void Then_Open_Door_Command_Was_Run_Once()
        {
            MockOpenDoorCommand
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Delay_Was_Set_Once()
        {
            MockDelay
                .Received(1)
                .WaitUntil(Arg.Any<double>());
        }

        [Test]
        public void Then_Delay_Was_Set_To_Difference_Between_6Am_And_Current_Time()
        {
            MockDelay
                .Received()
                .WaitUntil(Arg.Is(_calculatedDelay));
        }

        [Test]
        public void Then_Delay_Was_Run_Once()
        {
            MockDelay
                .Received(1)
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
        public void Then_Check_For_Light_Command_Run_Count_Was_12()
        {
            MockCheckForLightCommand
                .Received()
                .SetRunCountBeforeMorning(12);
        }

        [Test]
        public void Then_Command_Was_Successful()
        {
            Assert.AreEqual(SequenceResultEnum.Success, Result);
        }
    }
}