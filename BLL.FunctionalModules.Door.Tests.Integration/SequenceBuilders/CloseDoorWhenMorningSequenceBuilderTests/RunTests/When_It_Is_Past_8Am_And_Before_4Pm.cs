using Crosscutting.Sequencing.Sequence;
using NSubstitute;
using NUnit.Framework;

namespace BLL.FunctionalModules.Door.Tests.Integration.SequenceBuilders.CloseDoorWhenMorningSequenceBuilderTests.
    RunTests
{
    [TestFixture(28800001d)]
    [TestFixture(43200000d)]
    [TestFixture(50400000d)]
    [TestFixture(57599999d)]
    public class When_It_Is_Past_8Am_And_Before_4Pm : Given_CloseDoorWhenMorningSequenceBuilder
    {
        private readonly double _timeInMilli;

        public When_It_Is_Past_8Am_And_Before_4Pm(double timeInMilli)
        {
            _timeInMilli = timeInMilli;
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
        public void Then_Delay_Was_Not_Run()
        {
            MockDelay
                .DidNotReceive()
                .Run();
        }

        [Test]
        public void Then_Check_For_Light_Command_Was_Not_Run()
        {
            MockCheckForLightCommand
                .DidNotReceive()
                .Run();
        }

        [Test]
        public void Then_Command_Was_Successful()
        {
            Assert.AreEqual(SequenceResultEnum.Success, Result);
        }
    }
}