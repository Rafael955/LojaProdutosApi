using System;

namespace ProdutosApp.Api.Configurations;

public static class CorsConfiguration
{
    public static void AddCorsConfiguration(this IServiceCollection service)
    {
        //Configuração do CORS (permissão para o Agular acessar os endpoints da API)
        service.AddCors(options =>
        {
            options.AddPolicy(name: "DefaultPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:4200", "http://localhost:5051") // URL da aplicação Angular e aplicação Blazor
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void UseCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors("DefaultPolicy");
    }
}
