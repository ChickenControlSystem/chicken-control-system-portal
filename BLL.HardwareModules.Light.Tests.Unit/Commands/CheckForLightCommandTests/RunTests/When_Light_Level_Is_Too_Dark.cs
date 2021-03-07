﻿using Crosscutting.Sequencing.Sequence;
using HAL.Models.Contract;
using HAL.Operations.Dto;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace BLL.HardwareModules.Light.Tests.Unit.Commands.CheckForLightCommandTests.RunTests
{
    [TestFixture(99)]
    [TestFixture(50)]
    [TestFixture(10)]
    [TestFixture(0)]
    public class When_Light_Level_Is_Too_Dark : Given_CheckForLightCommand
    {
        private readonly int _lightLuxLevel;
        private SequenceResultEnum _result;

        public When_Light_Level_Is_Too_Dark(int lightLuxLevel)
        {
            _lightLuxLevel = lightLuxLevel;
        }

        public override void When()
        {
            MockAnalogOperations
                .Read(Arg.Any<IDevice>())
                .Returns(new OperationResult
                {
                    ResultStatus = OperationResultEnum.Succeess,
                    Return = _lightLuxLevel
                });
            MockValidateOperationService
                .GetSequenceResult(Arg.Any<OperationResultEnum>())
                .Returns(SequenceResultEnum.Success);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Command_Is_Not_Successful()
        {
            Assert.AreEqual(SequenceResultEnum.Fail, _result);
        }

        [Test]
        public void Then_10_Minute_Delay_Occurs_Once()
        {
            MockThreadingOperations
                .Received()
                .SyncronousDelay(Arg.Is(600000d));
            MockThreadingOperations
                .Received(1)
                .SyncronousDelay(Arg.Any<double>());
        }

        [Test]
        public void Then_Light_Value_Is_Read_Once()
        {
            MockAnalogOperations
                .Received()
                .Read(Arg.Is(MockLightSensor));
            MockAnalogOperations
                .Received(1)
                .Read(Arg.Any<IDevice>());
        }
    }
}