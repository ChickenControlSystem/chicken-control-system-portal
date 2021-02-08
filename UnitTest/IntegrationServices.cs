using NUnit.Framework;

namespace UnitTest
{
    [SetUpFixture]
    public abstract class IntegrationServices
    {
        [OneTimeSetUp]
        public void SetUpForNamespace()
        {
            GivenServices();
        }

        protected virtual void GivenServices()
        {
        }
    }
}