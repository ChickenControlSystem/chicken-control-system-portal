using System;
using Bootstrapping.Services.Contract.Crosscutting.Utils;

namespace Bootstrapping.Services.Contract.Crosscutting.Interface.Monitoring
{
    public interface ICondition : IDataMemory<bool>
    {
        public bool IsValid { get; }

        public event Action ConditionValid;

        bool Evaluate();
    }
}