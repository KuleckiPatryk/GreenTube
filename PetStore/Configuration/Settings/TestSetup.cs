using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace PetStore.Configuration.Settings;

public class TestSetup
{
    [ScenarioDependencies]
    public static IServiceCollection InjectDependencies()
    {
        var services = new ServiceCollection();

        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

        var settings = new Settings();
        config.GetSection(nameof(Settings)).Bind(settings);

        var restClient = RestClientFactory.CreateClient(settings.BaseUrl);
        services.AddSingleton(restClient);

        return services;
    }
}