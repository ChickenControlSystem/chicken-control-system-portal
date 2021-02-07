using System;
using NUnit.Framework;

namespace UnitTest
{
    public abstract class GivenWhenThenTests<T>
    {
        protected T SUT;

        [SetUp]
        public void SetUpClass()
        {
            Given();
        }

        [SetUp]
        public void SetUpMethod()
        {
            try
            {
                When();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// set up for test fixture
        /// </summary>
        protected abstract void Given();

        /// <summary>
        /// set up for test method, SUT operation can be run (it will ignore exceptions)
        /// </summary>
        protected abstract void When();
    }
}