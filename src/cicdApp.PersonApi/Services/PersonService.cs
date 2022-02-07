using cicdApp.PersonApi.Models;
using cicdApp.PersonApi.Repos.Definitions;
using cicdApp.PersonApi.Services.Definitions;
using System;
using System.Threading.Tasks;

namespace cicdApp.PersonApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepo;

        public PersonService(IPersonRepository personRepo)
        {
            this.personRepo = personRepo;
        }
        public async Task<string> CreatePerson(Person person)
        {
            person.Id = Guid.NewGuid().ToString();
            await personRepo.CreatePerson(person);
            return person.Id;
        }
    }
}
