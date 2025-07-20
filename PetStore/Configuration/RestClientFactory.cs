using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace PetStore.Configuration;

public static class RestClientFactory
{
    public static RestClient CreateClient(string baseUrl)
    {
        var client = new RestClient(baseUrl, configureSerialization: config =>
            config.UseNewtonsoftJson());
        
        client.AddDefaultHeader("Content-Type", "application/json");

        return client;
    }
}
