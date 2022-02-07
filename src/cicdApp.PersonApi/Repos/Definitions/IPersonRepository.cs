using cicdApp.PersonApi.Models;
using System.Threading.Tasks;

namespace cicdApp.PersonApi.Repos.Definitions
{
    public interface IPersonRepository
    {
        Task CreatePerson(Person person);
    }
}
