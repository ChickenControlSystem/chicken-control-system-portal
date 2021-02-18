using System.Collections.Generic;

namespace BLL.HardwareModules.Common.Contract
{
    public interface ISequenceFactory
    {
        public const int Serial = 0;
        public const int Parelell = 1;

        public ISequence CreateSequence(int type, List<IRunnable> tasks);
    }
}