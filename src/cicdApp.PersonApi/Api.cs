using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using cicdApp.PersonApi.Services.Definitions;
using cicdApp.PersonApi.Models;
using System.Text.Json;

namespace cicdApp.PersonApi
{
    public class Api
    {
        private readonly IPersonService personService;
         
        public Api(IPersonService personService) //he j
        {
            this.personService = personService;
        }

        [FunctionName("Person")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var personToSave = JsonSerializer.Deserialize<Person>(requestBody);

            var id = await personService.CreatePerson(personToSave);

            return new CreatedResult($"{req.Path}/{id}", null);
        }
    }
}
