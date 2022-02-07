using cicdApp.PersonApi.Models;
using System.Threading.Tasks;

namespace cicdApp.PersonApi.Services.Definitions
{
    public interface IPersonService
    {
        public Task<string> CreatePerson(Person person);
    }
}
