using System;

namespace ProdutosApp.Api.Configurations;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        //Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder builder)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI();
    }
}
