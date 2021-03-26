using Bootstrapping.Services.Contract.Crosscutting.Utils;
using NUnit.Framework;

namespace Crosscutting.UnitTest
{
    [SetUpFixture]
    public abstract class IntegrationService<TService> where TService : ITestService
    {
        protected TService Service;

        [OneTimeSetUp]
        public void SetUpForNamespace()
        {
            GivenServices();
            Service.Run();
        }

        protected virtual void GivenServices()
        {
        }
    }
}