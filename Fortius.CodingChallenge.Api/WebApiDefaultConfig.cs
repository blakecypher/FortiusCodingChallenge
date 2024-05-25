using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Fortius.CodingChallenge.Api;

public static class WebApiDefaultConfig
{
    public static void AddDefaultServices(this IServiceCollection services)
    {
        services.AddMvc();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fortius Search Engine", Version = "v1" });
            c.UseInlineDefinitionsForEnums();
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "FortiusSearchEngine.xml"));
        });

        services.AddHttpContextAccessor();
        
        services.Configure<JsonOptions>(o =>
        {
            o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            o.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            o.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                 o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                 o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        
        services.ConfigureHttpJsonOptions(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    public static void UseDefaultAppConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fortius Search Engine v1");
            c.DocExpansion(DocExpansion.List);
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}