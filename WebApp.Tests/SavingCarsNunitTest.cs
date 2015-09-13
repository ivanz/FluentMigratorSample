using Domain.Data;
using Domain.Model;
using NUnit.Framework;

namespace WebApp.Tests
{
    public class SavingCarsNUnitTest : DatabaseTestFixtureBase
    {

        [Test]
        public void When_a_car_is_saved_in_the_database_it_should_succeed()
        {
            CarsDbContext context = new CarsDbContext(base.ConnectionString);
            context.Cars.Add(new Car() {
                Make = "make",
                Model = "model",
            });
            context.SaveChanges();
        }
    }

    [TestFixture]
    public abstract class DatabaseTestFixtureBase
    {
        private IntegrationTestLifecycle _testLifeCycle = new IntegrationTestLifecycle();

        protected string ConnectionString {
            get { return _testLifeCycle.ConnectionString; }
        }

        [TestFixtureSetUp]
        public virtual void SetUpFixture()
        {
            _testLifeCycle.SetUp(Settings.Default.ConnectionStringTemplate);
        }

        [TestFixtureTearDown]
        public virtual void TearDownFixture()
        {
            _testLifeCycle.TearDown();
            _testLifeCycle.Dispose();
        }
    }
}
