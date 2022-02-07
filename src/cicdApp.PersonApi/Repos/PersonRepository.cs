using Azure.Storage.Blobs;
using cicdApp.PersonApi.Models;
using cicdApp.PersonApi.Repos.Definitions;
using Microsoft.Extensions.Azure;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace cicdApp.PersonApi.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private BlobContainerClient container;
        public PersonRepository(IAzureClientFactory<BlobServiceClient> blobServiceFactory)
        {
            var blobService = blobServiceFactory.CreateClient("Persons");
            container = blobService.GetBlobContainerClient("persons");
            container.CreateIfNotExists();
        }

        public Task CreatePerson(Person person)
        {
            var json = JsonSerializer.Serialize(person);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            var blob = container.GetBlobClient($"{person.Id}.json");
            return blob.UploadAsync(stream);
        }
    }
}
