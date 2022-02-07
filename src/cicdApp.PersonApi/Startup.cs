using Azure.Identity;
using cicdApp.PersonApi.Repos;
using cicdApp.PersonApi.Repos.Definitions;
using cicdApp.PersonApi.Services;
using cicdApp.PersonApi.Services.Definitions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(cicdApp.PersonApi.Startup))]
namespace cicdApp.PersonApi 
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot config;
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder configBuilder)
        {
            var builder = configBuilder.ConfigurationBuilder;
            bool isLocalDev = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));
            var kvEndpoint = Environment.GetEnvironmentVariable("KeyvaultEndpoint");

            if (kvEndpoint == null)
            {
                throw new ArgumentNullException("KeyVaultEndpoint", "No setting for keyvault found");
            }

            builder.AddAzureKeyVault(new Uri(kvEndpoint), new DefaultAzureCredential());
            builder.AddEnvironmentVariables();
            config = builder.Build();
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
           .AddSingleton<IPersonService, PersonService>()
           .AddSingleton<IPersonRepository, PersonRepository>()
           .AddAzureClients(a =>
           {
               a.AddBlobServiceClient(config["ConnectionString:Storage"]).WithName("Persons");
           });
        }
    }
}
