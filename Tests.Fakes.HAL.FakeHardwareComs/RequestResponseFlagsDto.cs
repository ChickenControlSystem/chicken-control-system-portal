namespace Tests.Fakes.HAL.FakeHardwareComs
{
    public class RequestResponseFlagsDto
    {
        public bool ReadLightFail { get; set; }

        public bool AbsoluteMoveDoorFail { get; set; }

        public bool RelativeMoveDoorFail { get; set; }

        public bool SearchMoveDoorFail { get; set; }
    }
}