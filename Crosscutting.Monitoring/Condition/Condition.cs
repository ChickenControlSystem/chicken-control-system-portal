using System;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Monitoring;
using Crosscutting.Threading;

namespace Crosscutting.Monitoring.Condition
{
    public abstract class Condition : DataMemory<bool>, ICondition
    {
        public bool IsValid => Value;
        public event Action ConditionValid;

        protected Condition()
        {
            ValueChangedEvent += ReEvalulate;
        }

        public abstract bool Evaluate();

        private void ReEvalulate()
        {
            Value = Evaluate();
            if (IsValid) ConditionValid?.Invoke();
        }
    }
}