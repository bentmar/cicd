using cicdApp.PersonApi.Models;
using cicdApp.PersonApi.Repos.Definitions;
using cicdApp.PersonApi.Services;
using cicdApp.PersonApi.Services.Definitions;
using cicdApp.Tests.MockRepos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cicdApp.Tests
{
    [TestClass]
    public class PersonTests
    {
        private IPersonService personService;

        public PersonTests()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IPersonService, PersonService>()
                .AddSingleton<IPersonRepository, PersonMockRepository>();

            var serviceProvider = services.BuildServiceProvider();

            personService = serviceProvider.GetService<IPersonService>();
        }

        [TestMethod]
        public void When_Creating_Person_Id_Should_Be_Generated()
        {
            var person = new Person();
            person.FirstName = "Test";
            person.LastName = "Testsson";

            var id = personService.CreatePerson(person).GetAwaiter().GetResult();
            Assert.IsNotNull(id);
        }
    }
}