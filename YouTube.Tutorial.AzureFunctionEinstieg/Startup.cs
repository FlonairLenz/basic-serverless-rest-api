using System;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(YouTube.Tutorial.AzureFunctionEinstieg.Startup))]
namespace YouTube.Tutorial.AzureFunctionEinstieg;

public class Startup : FunctionsStartup
{
    private IConfiguration configuration;


    public override void Configure(IFunctionsHostBuilder builder)
    {
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        this.configuration = builder.ConfigurationBuilder.Build();
        var keyVaultEndpoint = this.configuration.GetValue<string>("KeyVaultEndpoint");

        builder.ConfigurationBuilder
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", true);
        
        builder.ConfigurationBuilder.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
    }
}