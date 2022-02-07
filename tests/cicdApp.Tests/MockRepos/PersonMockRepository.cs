using cicdApp.PersonApi.Models;
using cicdApp.PersonApi.Repos.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cicdApp.Tests.MockRepos
{
    public class PersonMockRepository : IPersonRepository
    {
        private readonly Dictionary<string, Person> persons;
        public PersonMockRepository()
        {
            persons = new Dictionary<string, Person>();
        }
        public Task CreatePerson(Person person)
        {
            persons.Add(person.Id, person);
            return Task.CompletedTask;
        }
    }
}
