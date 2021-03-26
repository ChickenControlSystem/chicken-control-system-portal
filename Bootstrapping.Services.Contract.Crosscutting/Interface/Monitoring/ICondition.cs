using System;

namespace Bootstrapping.Services.Contract.Crosscutting.Interface.Monitoring
{
    public interface ICondition
    {
        public bool IsValid { get; set; }

        public event Action ConditionIsValidEvent;
    }
}