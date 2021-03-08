using NUnit.Framework;

namespace Crosscutting.UnitTest
{
    public class GivenWhenThenTests
    {
        [SetUp]
        public void SetUpClass()
        {
            Given();
        }

        [SetUp]
        public void SetUpMethod()
        {
            When();
        }

        /// <summary>
        ///     set up for test fixture
        /// </summary>
        protected virtual void Given()
        {
        }

        /// <summary>
        ///     set up for test method, SUT operation can be run (it will ignore exceptions)
        /// </summary>
        protected virtual void When()
        {
        }
    }
}