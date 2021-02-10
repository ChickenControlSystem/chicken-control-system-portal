using System;

namespace CodeContracts
{
    /// <summary>
    ///     defines the functions for contractual requirements (pre/post conditions) in methods
    /// </summary>
    public static class CodeContract
    {
        /// <summary>
        ///     throws an exception if the specified pre condition is not met
        /// </summary>
        public static void PreCondition<TException>(bool predicate)
            where TException : Exception, new()
        {
            Condition<TException>(predicate);
        }

        /// <summary>
        ///     throws an exception if the specified pre condition is not met
        /// </summary>
        public static void PreCondition<TException>(Func<bool> predicate)
            where TException : Exception, new()
        {
            Condition<TException>(predicate());
        }

        /// <summary>
        ///     throws an exception if the specified post condition is not met
        /// </summary>
        public static void PostCondition<TException>(bool predicate)
            where TException : Exception, new()
        {
            Condition<TException>(predicate);
        }

        private static void Condition<TException>(bool predicate)
            where TException : Exception, new()
        {
            if (!predicate) throw new TException();
        }
    }
}