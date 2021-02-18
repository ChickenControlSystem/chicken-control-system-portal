using System.Collections.Generic;
using BLL.HardwareModules.Common.Contract;

namespace BLL.HardwareModules.Common.Sequence
{
    public abstract class Sequence
    {
        protected readonly List<IRunnable> Tasks;

        protected Sequence(List<IRunnable> tasks)
        {
            Tasks = tasks;
        }
    }
}