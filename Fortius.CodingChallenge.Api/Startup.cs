using Fortius.CodingChallenge.SearchService.SampleData;

namespace Fortius.CodingChallenge.Api;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDefaultServices();
        services.AddScoped<ISearchEngine, SearchEngine>();
        services.AddScoped<ISampleDataBuilder, SampleDataBuilder?>();
        services.AddSingleton<IConfiguration>(_ => configuration);
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDefaultAppConfig();
    }
}
